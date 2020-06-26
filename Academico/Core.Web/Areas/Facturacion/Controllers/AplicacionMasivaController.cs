using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.CuentasPorCobrar;
using Core.Info.Facturacion;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class AplicacionMasivaController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_AplicacionMasiva_List Lista = new fa_AplicacionMasiva_List();
        fa_AplicacionMasivaDet_List ListaDet = new fa_AplicacionMasivaDet_List();
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_AplicacionMasiva_Bus bus_masiva = new fa_AplicacionMasiva_Bus();
        fa_AplicacionMasivaDet_Bus bus_masiva_det = new fa_AplicacionMasivaDet_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        fa_notaCreDeb_Bus bus_acreedor = new fa_notaCreDeb_Bus();
        cxc_cobro_Bus bus_deudor = new cxc_cobro_Bus();
        cxc_cobro_det_Bus bus_deudor_det = new cxc_cobro_det_Bus();
        fa_notaCreDeb_AP_List ListaSaldoAcreedor = new fa_notaCreDeb_AP_List();
        cxc_cobro_det_AP_List ListaSaldoDeudor = new cxc_cobro_det_AP_List();
        //fa_AplicacionMasivaDet_Modal_List Lista_ValorCero = new fa_AplicacionMasivaDet_Modal_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo bajo demanda
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
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
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = DateTime.Now.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            List<fa_AplicacionMasiva_Info> lista = bus_masiva.Get_list(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "AplicacionMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            List<fa_AplicacionMasiva_Info> lista = bus_masiva.Get_list(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "AplicacionMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AplicacionMasiva(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<fa_AplicacionMasiva_Info> model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            return PartialView("_GridViewPartial_AplicacionMasiva", model);
        }
        #endregion

        #region CargaDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AplicacionMasivaDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AplicacionMasivaDet", model);
        }
        #endregion

        #region Metodos
        private bool validar(fa_AplicacionMasiva_Info info, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.Fecha, cl_enumeradores.eModulo.FAC, info.IdSucursal, ref msg))
            {
                return false;
            }

            if (info.lst_det.Count() == 0)
            {
                msg = "Debe de ingresar al menos 1 item válido en el detalle";
                return false;
            }
            return true;
        }
        #endregion

        #region Json
        public JsonResult Buscar(int IdEmpresa= 0, decimal IdTransaccionSession=0)
        {
            var resultado = "";

            var lstNC = ListaSaldoAcreedor.get_list(IdTransaccionSession);
            var lstCxC = ListaSaldoDeudor.get_list(IdTransaccionSession);

            var lst_acreedora_agrupada = lstNC.GroupBy(q => new { q.IdEmpresa, q.IdAlumno, q.NomAlumno }).Select(q => new fa_notaCreDeb_Info
            {
                IdEmpresa = q.Key.IdEmpresa,
                IdAlumno = q.Key.IdAlumno,
                NomAlumno = q.Key.NomAlumno,
                sc_saldo = (double?)q.Sum(h => h.sc_saldo)
            }).ToList();

            var lst_deudora_agrupada = lstCxC.GroupBy(q => new { q.IdEmpresa, q.IdAlumno, q.NomCliente }).Select(q => new cxc_cobro_det_Info
            {
                IdEmpresa = q.Key.IdEmpresa,
                IdAlumno = q.Key.IdAlumno,
                NomCliente = q.Key.NomCliente,
                Saldo = (double?)q.Sum(h => h.Saldo)
            }).ToList();

            var lst_Final = from NC in lst_acreedora_agrupada
                            join CxC in lst_deudora_agrupada on new { NC.IdAlumno } equals new { CxC.IdAlumno }
                            select new
                            {
                                IdEmpresa = NC.IdEmpresa,
                                IdAlumno = Convert.ToDecimal(NC.IdAlumno),
                                pe_nombreCompleto = NC.NomAlumno,
                                Saldo = Convert.ToDouble(NC.sc_saldo)
                            };

            var cantidad = lst_Final.Count();
            var lst_det = new List<fa_AplicacionMasivaDet_Info>();
            foreach (var item in lst_Final)
            {
                lst_det.Add(new fa_AplicacionMasivaDet_Info
                {
                    IdEmpresa = item.IdEmpresa,
                    IdAlumno = item.IdAlumno,
                    pe_nombreCompleto = item.pe_nombreCompleto,
                    Saldo = item.Saldo
                });
            }

            ListaDet.set_list(lst_det, IdTransaccionSession);
            return Json(lst_det, JsonRequestBehavior.AllowGet);
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
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "AplicacionMasiva", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            fa_AplicacionMasiva_Info model = new fa_AplicacionMasiva_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                Fecha = DateTime.Now,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                lst_det = new List<fa_AplicacionMasivaDet_Info>()
            };

            var lst_acreedora = bus_acreedor.get_list_aplicacion_masiva(model.IdEmpresa);
            var lst_deudora = bus_deudor_det.get_list_AP(model.IdEmpresa);

            ListaSaldoAcreedor.set_list(lst_acreedora, model.IdTransaccionSession);
            ListaSaldoDeudor.set_list(lst_deudora, model.IdTransaccionSession);

            //var lst_acreedora_agrupada = lst_acreedora.GroupBy(q=> new { q.IdEmpresa, q.IdAlumno});
            //var lst_deudora_agrupada = lst_deudora.GroupBy(q => new { q.IdEmpresa, q.IdAlumno });

            var lst_acreedora_agrupada = lst_acreedora.GroupBy(q => new { q.IdEmpresa, q.IdAlumno, q.NomAlumno }).Select(q => new fa_notaCreDeb_Info
            {
                IdEmpresa = q.Key.IdEmpresa,
                IdAlumno = q.Key.IdAlumno,
                NomAlumno = q.Key.NomAlumno,
                sc_saldo = (double?)q.Sum(h => h.sc_saldo)
            }).ToList();

            var lst_deudora_agrupada = lst_deudora.GroupBy(q => new { q.IdEmpresa, q.IdAlumno, q.NomCliente }).Select(q => new cxc_cobro_det_Info
            {
                IdEmpresa = q.Key.IdEmpresa,
                IdAlumno = q.Key.IdAlumno,
                NomCliente = q.Key.NomCliente,
                Saldo = (double?)q.Sum(h => h.Saldo)
            }).ToList();

            var lst_Final = from NC in lst_acreedora_agrupada join CxC in lst_deudora_agrupada on new { NC.IdAlumno } equals new { CxC.IdAlumno } select new
            {
                IdEmpresa = NC.IdEmpresa,
                IdAlumno = Convert.ToDecimal(NC.IdAlumno),
                pe_nombreCompleto = NC.NomAlumno,
                Saldo = Convert.ToDouble(NC.sc_saldo)
            };

            var cantidad = lst_Final.Count();
            foreach (var item in lst_Final)
            {
                model.lst_det.Add(new fa_AplicacionMasivaDet_Info
                {
                    IdEmpresa = item.IdEmpresa,
                    IdAlumno = item.IdAlumno,
                    pe_nombreCompleto = item.pe_nombreCompleto,
                    Saldo = item.Saldo
                });
            }

            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_AplicacionMasiva_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario.ToString();
            var ListaDetalleMasiva = ListaDet.get_list(model.IdTransaccionSession);

            model.lst_det = ListaDetalleMasiva.ToList();

            if (!validar(model, ref mensaje))
            {
                ListaDet.set_list(ListaDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_masiva.GuardarDB(model))
            {
                ListaDet.set_list(ListaDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            };

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdAplicacion = model.IdAplicacion, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, decimal IdAplicacion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_AplicacionMasiva_Info model = bus_masiva.Get_info(IdEmpresa, IdAplicacion);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_masiva_det.GetList(IdEmpresa, IdAplicacion);
            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            #region Permisos
            var MostrarSRI = true;
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "AplicacionMasiva", "Index");
            if (model.Estado == false)
            {
                info.Anular = false;
                info.Modificar = false;
                MostrarSRI = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Anular = info.Anular;
            ViewBag.MostrarSRI = MostrarSRI;
            #endregion

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdAplicacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_AplicacionMasiva_Info model = bus_masiva.Get_info(IdEmpresa, IdAplicacion);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "AplicacionMasiva", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_masiva_det.GetList(IdEmpresa, IdAplicacion);
            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);


            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_AplicacionMasiva_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            model.lst_det = ListaDet.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_masiva.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class fa_AplicacionMasiva_List
    {
        string Variable = "fa_AplicacionMasiva_Info";
        public List<fa_AplicacionMasiva_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_AplicacionMasiva_Info> list = new List<fa_AplicacionMasiva_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_AplicacionMasiva_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_AplicacionMasiva_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class fa_AplicacionMasivaDet_List
    {
        string Variable = "fa_AplicacionMasivaDet_Info";
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public List<fa_AplicacionMasivaDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_AplicacionMasivaDet_Info> list = new List<fa_AplicacionMasivaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_AplicacionMasivaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_AplicacionMasivaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class fa_AplicacionMasivaDet_Modal_List
    {
        string Variable = "fa_AplicacionMasivaDet_Modal_Info";
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public List<fa_AplicacionMasivaDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_AplicacionMasivaDet_Info> list = new List<fa_AplicacionMasivaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_AplicacionMasivaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_AplicacionMasivaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    #region SALDO ACREEDOR
    public class fa_notaCreDeb_AP_List
    {
        string Variable = "fa_notaCreDeb_AP_Info";
        public List<fa_notaCreDeb_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_Info> list = new List<fa_notaCreDeb_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    #endregion

    #region SALDO DEUDOR
    public class cxc_cobro_det_AP_List
    {
        string Variable = "cxc_cobro_det_AP_Info";
        public List<cxc_cobro_det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_cobro_det_Info> list = new List<cxc_cobro_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_cobro_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_cobro_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
        #endregion
    }