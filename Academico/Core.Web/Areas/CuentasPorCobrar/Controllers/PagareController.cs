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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class PagareController : Controller
    {
        #region Variables
        cxc_Pagare_Bus bus_pagare = new cxc_Pagare_Bus();
        cxc_cobro_Bus bus_cobro = new cxc_cobro_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        cxc_Pagare_List Lista_Pagare = new cxc_Pagare_List();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo bajo demanda Persona
        public ActionResult Cmb_Persona()
        {
            var model = new cxc_Pagare_Info();
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
            var model = new cxc_Pagare_Info();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var x = bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
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

            List<cxc_Pagare_Info> lista = bus_pagare.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_Pagare.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

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
            List<cxc_Pagare_Info> lista = bus_pagare.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_Pagare.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
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

            cxc_Pagare_Info model = new cxc_Pagare_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = IdEmpresa,
                FechaAPagar = DateTime.Now.Date,
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cxc_Pagare_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!bus_pagare.GuardarDB(model))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdPagare = model.IdPagare, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, int IdPagare = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ViewBag.MostrarBoton = true;
            cxc_Pagare_Info model = bus_pagare.GetInfo(IdEmpresa, IdPagare);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

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
        public ActionResult Modificar(int IdEmpresa = 0, int IdPagare = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ViewBag.MostrarBoton = true;
            cxc_Pagare_Info model = bus_pagare.GetInfo(IdEmpresa, IdPagare);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_Pagare_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_pagare.ModificarDB(model))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdPagare = model.IdPagare, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdPagare = 0)
        {
            ViewBag.MostrarBoton = true;

            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_Pagare_Info model = bus_pagare.GetInfo(IdEmpresa, IdPagare);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "Pagare", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_Pagare_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_pagare.AnularDB(model))
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
        public ActionResult GridViewPartial_Pagare(bool Nuevo=false)
        {
            ViewBag.Nuevo = Nuevo;
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Pagare.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_Pagare", model);
        }
        #endregion

        #region Json
        public JsonResult SetDatosCobranza(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            string Resultado = string.Empty;
            var Saldo = (double?)null;
            var IdPersonaFactura = (decimal?)null;
            var lst_SaldoAlumno = bus_cobro.GetSaldoAlumno(IdEmpresa,IdAlumno);
            if (lst_SaldoAlumno!=null)
            {
                Saldo = lst_SaldoAlumno.Sum(q=>q.cr_saldo);
            }

            var info_PesonaFactura = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.ECON.ToString());
            IdPersonaFactura = (info_PesonaFactura == null ? (decimal?)null : info_PesonaFactura.IdPersona);
            return Json(new { Saldo= Saldo, IdPersonaFactura = IdPersonaFactura }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class cxc_Pagare_List
    {
        string Variable = "cxc_Pagare_Info";
        public List<cxc_Pagare_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_Pagare_Info> list = new List<cxc_Pagare_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_Pagare_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_Pagare_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}