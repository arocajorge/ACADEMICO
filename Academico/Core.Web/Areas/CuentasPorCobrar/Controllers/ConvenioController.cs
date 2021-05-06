using Core.Bus.Academico;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.CuentasPorCobrar;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class ConvenioController : Controller
    {
        #region Variables
        cxc_Convenio_Bus bus_convenio = new cxc_Convenio_Bus();
        cxc_Convenio_Det_Bus bus_convenio_det = new cxc_Convenio_Det_Bus();
        cxc_cobro_Bus bus_cobro = new cxc_cobro_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        cxc_Convenio_List Lista_Convenio = new cxc_Convenio_List();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        cxc_Convenio_Det_List Lista_Convenio_Det = new cxc_Convenio_Det_List();
        aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo bajo demanda Persona
        public ActionResult Cmb_Persona()
        {
            var model = new cxc_Convenio_Info();
            return PartialView("_CmbPersona", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_persona(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PERSONA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_persona(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PERSONA.ToString());
        }
        #endregion

        #region Combo bajo demanda Alumno
        public ActionResult Cmb_Alumno()
        {
            var model = new cxc_Convenio_Info();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            var z = bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            List<cxc_Convenio_Info> lista = bus_convenio.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_Convenio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<cxc_Convenio_Info> lista = bus_convenio.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_Convenio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        #endregion

        #region Metodos
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_catalogo = bus_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.ESTCONV), false);
        }
        private bool validar(cxc_Convenio_Info model, ref string msg)
        {
            var suma_det = Math.Round(model.lst_detalle.Sum(q => q.TotalCuota),2, MidpointRounding.AwayFromZero);
            if (model.Valor != suma_det)
            {
                msg = "La valor total de las cuotas no es el mismo que el valor total del convenio";
                return false;
            }

            if (model.lst_detalle.Count() == 0 )
            {
                msg = "El convenio de pago debe de tener al menos un registro en el detalle";
                return false;
            }

            return true;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Cobranza", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion

            cxc_Convenio_Info model = new cxc_Convenio_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = IdEmpresa,
                Fecha = DateTime.Now.Date,
                FechaPrimerPago = DateTime.Now.Date,
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cxc_Convenio_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.lst_detalle = Lista_Convenio_Det.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            if (!bus_convenio.GuardarDB(model))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdConvenio = model.IdConvenio, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, int IdConvenio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_Convenio_Info model = bus_convenio.GetInfo(IdEmpresa, IdConvenio);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = new List<cxc_Convenio_Det_Info>();
            model.lst_detalle = bus_convenio_det.GetList(model.IdEmpresa, model.IdConvenio);
            Lista_Convenio_Det.set_list(model.lst_detalle, model.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Cobranza", "Index");
            if (model.Estado == false)
            {
                info.Modificar = false;
                info.Anular = false;
            }

            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdConvenio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_Convenio_Info model = bus_convenio.GetInfo(IdEmpresa, IdConvenio);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = new List<cxc_Convenio_Det_Info>();
            model.lst_detalle = bus_convenio_det.GetList(model.IdEmpresa, model.IdConvenio);
            Lista_Convenio_Det.set_list(model.lst_detalle, model.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_Convenio_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_detalle = Lista_Convenio_Det.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            if (!bus_convenio.ModificarDB(model))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdConvenio = model.IdConvenio, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdConvenio = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_Convenio_Info model = bus_convenio.GetInfo(IdEmpresa, IdConvenio);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = new List<cxc_Convenio_Det_Info>();
            model.lst_detalle = bus_convenio_det.GetList(model.IdEmpresa, model.IdConvenio);
            Lista_Convenio_Det.set_list(model.lst_detalle, model.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_Convenio_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            model.lst_detalle = Lista_Convenio_Det.get_list(model.IdTransaccionSession);

            if (!bus_convenio.AnularDB(model))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Grids
        [ValidateInput(false)]
        public ActionResult GridViewPartial_Convenio(bool Nuevo = false)
        {
            ViewBag.Nuevo = Nuevo;
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Convenio.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_Convenio", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Convenio_Det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Convenio_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Convenio_Det", model);
        }
        #endregion

        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_Convenio_Det_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_Convenio_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Convenio_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Convenio_Det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_Convenio_Det_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_Convenio_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Convenio_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Convenio_Det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_Convenio_Det_Info info_det)
        {
            Lista_Convenio_Det.DeleteRow(info_det.NumCuota, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Convenio_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_Convenio_Det", model);
        }


        #endregion

        #region Json
        public JsonResult SetDatosCobranza(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            string Resultado = string.Empty;
            var Saldo = (double?)null;
            var IdPersonaFactura = (decimal?)null;
            var IdMatricula = (decimal?)null;

            var lst_SaldoAlumno = bus_cobro.GetSaldoAlumno(IdEmpresa, IdAlumno);
            if (lst_SaldoAlumno != null)
            {
                Saldo = lst_SaldoAlumno.Sum(q => q.cr_saldo);
            }

            var info_PesonaFactura = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.ECON.ToString());
            IdPersonaFactura = (info_PesonaFactura == null ? (decimal?)null : info_PesonaFactura.IdPersona);

            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa,0);
            var info_matricula = bus_matricula.GetInfo_ExisteMatricula(IdEmpresa, info_anio.IdAnio,IdAlumno);
            IdMatricula = info_matricula == null ? (decimal?)null : info_matricula.IdMatricula;

            return Json(new { Saldo = Saldo, IdPersonaFactura = IdPersonaFactura, IdMatricula = IdMatricula }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerarConvenioDet(double Valor, DateTime FechaPrimerPago, int NumCuotas = 0)
        {
            //IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            //var lst_detalle = new cxc_Convenio_Det_Info();

            double ValorCouta = Valor / NumCuotas;
            double saldo = Valor;
            DateTime fecha_pago = FechaPrimerPago;

            List<cxc_Convenio_Det_Info> lst_detalle = new List<cxc_Convenio_Det_Info>();
            for (int i = 1; i <= NumCuotas; i++)
            {
                cxc_Convenio_Det_Info item = new cxc_Convenio_Det_Info();

                if (i == 1)
                {
                    var fecha_pago_sgte = fecha_pago;
                    fecha_pago = new DateTime(fecha_pago_sgte.Year, fecha_pago_sgte.Month, fecha_pago_sgte.Day);
                }
                else
                {
                    var fecha_pago_sgte = fecha_pago.AddMonths(1);
                    fecha_pago = new DateTime(fecha_pago_sgte.Year, fecha_pago_sgte.Month, fecha_pago_sgte.Day);
                }

                item.FechaPago = FechaPrimerPago;
                item.NumCuota = i;
                item.TotalCuota = ValorCouta;
                item.SaldoInicial = Valor;
                item.Saldo = saldo - item.TotalCuota;
                item.FechaPago = fecha_pago;
                item.IdCatalogoEstadoPago = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoConvenio.PENDIENTE);
                item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");

                saldo = saldo - ValorCouta;
                item.TotalCuota = Math.Round(item.TotalCuota, 2);
                item.Saldo = Math.Round(item.Saldo, 2);

                lst_detalle.Add(item);

            }

            double diferencia = Valor - lst_detalle.Sum(v => v.TotalCuota);
            if (diferencia != 0)
            {
                foreach (var item in lst_detalle)
                {
                    item.TotalCuota = item.TotalCuota + diferencia;
                    break;
                }
            }

            Lista_Convenio_Det.set_list(lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class cxc_Convenio_List
    {
        string Variable = "cxc_Convenio_Info";
        public List<cxc_Convenio_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_Convenio_Info> list = new List<cxc_Convenio_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_Convenio_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_Convenio_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cxc_Convenio_Det_List
    {
        string Variable = "cxc_Convenio_Det_Info";
        public List<cxc_Convenio_Det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_Convenio_Det_Info> list = new List<cxc_Convenio_Det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_Convenio_Det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_Convenio_Det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_Convenio_Det_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_Convenio_Det_Info> list = get_list(IdTransaccionSession);
            info_det.NumCuota = list.Count == 0 ? 1 : list.Max(q => q.NumCuota) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(cxc_Convenio_Det_Info info_det, decimal IdTransaccionSession)
        {
            cxc_Convenio_Det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.NumCuota == info_det.NumCuota).FirstOrDefault();
            edited_info.FechaPago = info_det.FechaPago;
            edited_info.TotalCuota = info_det.TotalCuota;
            edited_info.IdCatalogoEstadoPago = info_det.IdCatalogoEstadoPago;
            edited_info.Observacion_det = "Cuota número " + info_det.NumCuota + " fecha pago " + info_det.FechaPago.ToString("dd/MM/yyyy");
        }

        public void DeleteRow(int NumCuota, decimal IdTransaccionSession)
        {
            List<cxc_Convenio_Det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.NumCuota == NumCuota).FirstOrDefault());
        }
    }
}