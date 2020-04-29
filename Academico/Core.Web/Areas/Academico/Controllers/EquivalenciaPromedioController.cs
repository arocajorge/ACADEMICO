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
    public class EquivalenciaPromedioController : Controller
    {
        #region Variables
        aca_AnioLectivoEquivalenciaPromedio_List Lista_EquivalenciaPromedio = new aca_AnioLectivoEquivalenciaPromedio_List();
        aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia_promedio = new aca_AnioLectivoEquivalenciaPromedio_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivoEquivalenciaPromedio_Info());
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivoEquivalenciaPromedio_Info model = new aca_AnioLectivoEquivalenciaPromedio_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivoEquivalenciaPromedio_Info> lista = bus_equivalencia_promedio.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_EquivalenciaPromedio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "ConductaEquivalencia", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivoEquivalenciaPromedio_Info model)
        {
            List<aca_AnioLectivoEquivalenciaPromedio_Info> lista = bus_equivalencia_promedio.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_EquivalenciaPromedio.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "ConductaEquivalencia", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;

            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_EquivalenciaPromedio(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivoEquivalenciaPromedio_Info> model = Lista_EquivalenciaPromedio.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_EquivalenciaPromedio", model);
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
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivoEquivalenciaPromedio_Info model = new aca_AnioLectivoEquivalenciaPromedio_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "ConductaEquivalencia", "Index");
            if (!inf.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_AnioLectivoEquivalenciaPromedio_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_equivalencia_promedio.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, Secuencia = model.IdEquivalenciaPromedio, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio = 0, int IdEquivalenciaPromedio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivoEquivalenciaPromedio_Info model = bus_equivalencia_promedio.GetInfo(IdEmpresa, IdAnio, IdEquivalenciaPromedio);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "ConductaEquivalencia", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_AnioLectivoEquivalenciaPromedio_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_equivalencia_promedio.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdEquivalenciaPromedio = model.IdEquivalenciaPromedio, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAnio = 0, int IdEquivalenciaPromedio = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivoEquivalenciaPromedio_Info model = bus_equivalencia_promedio.GetInfo(IdEmpresa, IdAnio, IdEquivalenciaPromedio);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "ConductaEquivalencia", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(aca_AnioLectivoEquivalenciaPromedio_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_equivalencia_promedio.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_AnioLectivoEquivalenciaPromedio_List
    {
        string Variable = "aca_AnioLectivoEquivalenciaPromedio_Info";
        public List<aca_AnioLectivoEquivalenciaPromedio_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivoEquivalenciaPromedio_Info> list = new List<aca_AnioLectivoEquivalenciaPromedio_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivoEquivalenciaPromedio_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivoEquivalenciaPromedio_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}