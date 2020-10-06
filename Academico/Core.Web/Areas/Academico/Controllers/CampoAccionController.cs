using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class CampoAccionController : Controller
    {
        #region Variables
        aca_CampoAccion_Bus bus_CampoAccion = new aca_CampoAccion_Bus();
        aca_CampoAccion_List Lista_CampoAccion = new aca_CampoAccion_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
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

            aca_CampoAccion_Info model = new aca_CampoAccion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_CampoAccion_Info> lista = bus_CampoAccion.GetList(model.IdEmpresa, true);
            Lista_CampoAccion.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CampoAccion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CampoAccion(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_CampoAccion_Info> model = Lista_CampoAccion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_CampoAccion", model);
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
            var orden = bus_CampoAccion.GetOrden(Convert.ToInt32(SessionFixed.IdEmpresa));
            aca_CampoAccion_Info model = new aca_CampoAccion_Info
            {
                IdEmpresa = IdEmpresa,
                OrdenCampoAccion = orden
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CampoAccion", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_CampoAccion_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_CampoAccion.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCampoAccion = model.IdCampoAccion, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdCampoAccion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_CampoAccion_Info model = bus_CampoAccion.GetInfo(IdEmpresa, IdCampoAccion);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CampoAccion", "Index");
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

        public ActionResult Modificar(int IdEmpresa = 0, int IdCampoAccion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_CampoAccion_Info model = bus_CampoAccion.GetInfo(IdEmpresa, IdCampoAccion);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CampoAccion", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_CampoAccion_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_CampoAccion.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCampoAccion = model.IdCampoAccion, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdCampoAccion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_CampoAccion_Info model = bus_CampoAccion.GetInfo(IdEmpresa, IdCampoAccion);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CampoAccion", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_CampoAccion_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_CampoAccion.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_CampoAccion_List
    {
        string Variable = "aca_CampoAccion_Info";
        public List<aca_CampoAccion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_CampoAccion_Info> list = new List<aca_CampoAccion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_CampoAccion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_CampoAccion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}