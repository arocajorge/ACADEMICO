using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Info.Academico;
using Core.Web.Helps;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Helps;
using Core.Info.General;

namespace Core.Web.Areas.Academico.Controllers
{
    public class PlantillaTipoController : Controller
    {
        #region Variables
        aca_PlantillaTipo_Bus busPlantillaTipo = new aca_PlantillaTipo_Bus();
        aca_PlantillaTipo_List Lista_Plantilla = new aca_PlantillaTipo_List();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_PlantillaTipo_Bus bus_tipo_plantilla = new aca_PlantillaTipo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            List<aca_PlantillaTipo_Info> lista = busPlantillaTipo.GetList(model.IdEmpresa, true);
            Lista_Plantilla.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "PlantillaTipo", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_PlantillaTipo(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_PlantillaTipo_Info> model = Lista_Plantilla.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_PlantillaTipo", model);
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_PlantillaTipo_Info model = new aca_PlantillaTipo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "PlantillaTipo", "Index");
            if (!inf.Nuevo)
                return RedirectToAction("Index");
            #endregion
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_PlantillaTipo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!busPlantillaTipo.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdTipoPlantilla = model.IdTipoPlantilla, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdTipoPlantilla = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_PlantillaTipo_Info model = busPlantillaTipo.getInfo(IdEmpresa, IdTipoPlantilla);

            if (model == null)
                return RedirectToAction("Index");
            
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "PlantillaTipo", "Index");
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
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipoPlantilla = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_PlantillaTipo_Info model = busPlantillaTipo.getInfo(IdEmpresa, IdTipoPlantilla);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "PlantillaTipo", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_PlantillaTipo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!busPlantillaTipo.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                
                return View(model);
            }
            
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdTipoPlantilla = model.IdTipoPlantilla, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdTipoPlantilla = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_PlantillaTipo_Info model = busPlantillaTipo.getInfo(IdEmpresa, IdTipoPlantilla);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "PlantillaTipo", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_PlantillaTipo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!busPlantillaTipo.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
           
            var lst_tipo_plantilla = bus_tipo_plantilla.GetList(IdEmpresa, false);
            ViewBag.lst_tipo_plantilla = lst_tipo_plantilla;
        }
        #endregion
    }
    public class aca_PlantillaTipo_List
    {
        string Variable = "aca_PlantillaTipo_Info";
        public List<aca_PlantillaTipo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_PlantillaTipo_Info> list = new List<aca_PlantillaTipo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_PlantillaTipo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_PlantillaTipo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}