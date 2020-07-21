using Core.Bus.Academico;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.General;
using Core.Bus.SeguridadAcceso;
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
    public class SeguimientoCarteraController : Controller
    {
        #region Variables
        cxc_SeguimientoCartera_Bus bus_seguimiento = new cxc_SeguimientoCartera_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        string mensaje = string.Empty;
        string mensajeInfo = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        cxc_SeguimientoCartera_List Lista_Seguimiento = new cxc_SeguimientoCartera_List();
        cxc_SeguimientoCartera_x_Alumno_List Lista_Seguimiento_x_Alumno = new cxc_SeguimientoCartera_x_Alumno_List();
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
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
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAlumno = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            var Lista = bus_seguimiento.getList(model.IdEmpresa, model.IdAlumno, true, model.fecha_ini, model.fecha_fin);
            Lista_Seguimiento.set_list(Lista, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            var Lista = bus_seguimiento.getList(model.IdEmpresa, model.IdAlumno, true, model.fecha_ini, model.fecha_fin);
            Lista_Seguimiento.set_list(Lista, model.IdTransaccionSession);

            return View(model);
        }
        #endregion

        #region Grids
        [ValidateInput(false)]
        public ActionResult GridViewPartial_SeguimientoCartera(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdAlumno = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_SeguimientoCartera_Info> model = Lista_Seguimiento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_SeguimientoCartera", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_SeguimientoCarteraDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_SeguimientoCartera_Info> model = Lista_Seguimiento_x_Alumno.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_SeguimientoCarteraDet", model);
        }
        #endregion

        #region Json
        public JsonResult GetList_x_Alumno(decimal IdTransaccionSession = 0, int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            var lst = bus_seguimiento.getList_x_Alumno(IdEmpresa, IdAlumno);

            Lista_Seguimiento_x_Alumno.set_list(lst, IdTransaccionSession);

            return Json(lst.Count, JsonRequestBehavior.AllowGet);
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
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cxc_SeguimientoCartera_Info model = new cxc_SeguimientoCartera_Info
            {
                IdEmpresa = IdEmpresa,
                Fecha = DateTime.Now.Date,
                lst_det = new List<cxc_SeguimientoCartera_Info>(),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            Lista_Seguimiento_x_Alumno.set_list(model.lst_det, model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_SeguimientoCartera_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario.ToString();
            var ListaDetalle = Lista_Seguimiento_x_Alumno.get_list(model.IdTransaccionSession);
            model.lst_det = ListaDetalle.ToList();

            if (!bus_seguimiento.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                Lista_Seguimiento_x_Alumno.set_list(Lista_Seguimiento_x_Alumno.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";

                return View(model);
            };

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdSeguimiento = model.IdSeguimiento, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdSeguimiento = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_SeguimientoCartera_Info model = bus_seguimiento.get_info(IdEmpresa, IdSeguimiento);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (model.Estado == false)
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSeguimiento = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_SeguimientoCartera_Info model = bus_seguimiento.get_info(IdEmpresa, IdSeguimiento);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_SeguimientoCartera_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;

            if (!bus_seguimiento.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class cxc_SeguimientoCartera_List
    {
        string Variable = "cxc_SeguimientoCartera_Info";
        public List<cxc_SeguimientoCartera_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_SeguimientoCartera_Info> list = new List<cxc_SeguimientoCartera_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_SeguimientoCartera_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_SeguimientoCartera_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cxc_SeguimientoCartera_x_Alumno_List
    {
        string Variable = "cxc_SeguimientoCartera_x_Alumno_Info";
        public List<cxc_SeguimientoCartera_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_SeguimientoCartera_Info> list = new List<cxc_SeguimientoCartera_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_SeguimientoCartera_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_SeguimientoCartera_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}