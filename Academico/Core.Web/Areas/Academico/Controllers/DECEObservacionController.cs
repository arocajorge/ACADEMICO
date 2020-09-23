using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class DECEObservacionController : Controller
    {
        #region Variables
        aca_AnioLectivo_Curso_Paralelo_Dece_Bus bus_dece = new aca_AnioLectivo_Curso_Paralelo_Dece_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Curso_Paralelo_Dece_List Lista_Dece = new aca_AnioLectivo_Curso_Paralelo_Dece_List();
        aca_MatriculaCambios_Bus bus_cambios = new aca_MatriculaCambios_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
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

            aca_AnioLectivo_Curso_Paralelo_Dece_Info model = new aca_AnioLectivo_Curso_Paralelo_Dece_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = info_anio.IdAnio,
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> lista = bus_dece.getList(model.IdEmpresa, model.IdAnio, model.IdSede, true);
            Lista_Dece.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "DECEObservacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Curso_Paralelo_Dece_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> lista = bus_dece.getList(model.IdEmpresa, model.IdAnio, model.IdSede, true);
            Lista_Dece.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "DECEObservacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sede = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sede = lst_sede;
        }
        #endregion

        #region Combos bajo demanada
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        #endregion

        #region FuncionesDetalle

        [ValidateInput(false)]
        public ActionResult GridViewPartial_DECEObservacion(bool Modificar = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ViewBag.Modificar = Modificar;
            List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> model = Lista_Dece.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_DECEObservacion", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AsignacionTutorInspector_Consultar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Dece.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_DECEObservacion", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Curso_Paralelo_Dece_Info info_det)
        {
            Lista_Dece.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_Dece.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_DECEObservacion", model);
        }
        #endregion
    }

    public class aca_AnioLectivo_Curso_Paralelo_Dece_List
    {
        string Variable = "aca_AnioLectivo_Curso_Paralelo_Dece_Info";
        aca_AnioLectivo_Curso_Paralelo_Dece_Bus bus_dece = new aca_AnioLectivo_Curso_Paralelo_Dece_Bus();

        public List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> list = new List<aca_AnioLectivo_Curso_Paralelo_Dece_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Curso_Paralelo_Dece_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_AnioLectivo_Curso_Paralelo_Dece_Info info_det, decimal IdTransaccionSession)
        {
            aca_AnioLectivo_Curso_Paralelo_Dece_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdString == info_det.IdString).FirstOrDefault();
            info_det.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            edited_info.ObservacionQ1 = info_det.ObservacionQ1;
            edited_info.ObservacionQ2 = info_det.ObservacionQ2;

            var existe_registro = bus_dece.getInfo(edited_info.IdEmpresa, edited_info.IdAnio, edited_info.IdSede, edited_info.IdJornada, edited_info.IdNivel, edited_info.IdCurso, edited_info.IdParalelo);
            if (existe_registro == null)
            {
                edited_info.IdUsuarioCreacion = SessionFixed.IdUsuario;
                bus_dece.guardarDB(edited_info);
            }
            else
            {
                edited_info.IdUsuarioModificacion = SessionFixed.IdUsuario;
                bus_dece.modificarDB(edited_info);
            }

        }
    }
}