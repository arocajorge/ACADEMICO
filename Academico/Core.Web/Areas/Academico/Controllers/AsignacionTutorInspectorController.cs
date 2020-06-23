using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
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

namespace Core.Web.Areas.Academico.Controllers
{
    public class AsignacionTutorInspectorController : Controller
    {
        #region Variables
        aca_AnioLectivo_Curso_Paralelo_List Lista_Paralelo = new aca_AnioLectivo_Curso_Paralelo_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Curso_Paralelo_Bus bus_ParaleloPorCurso = new aca_AnioLectivo_Curso_Paralelo_Bus();
        aca_AnioLectivo_Curso_Paralelo_List Lista_ParaleloPorCurso = new aca_AnioLectivo_Curso_Paralelo_List();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_ProfesorTutor()
        {
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info();
            return PartialView("_CmbProfesorTutor", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_tutor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_tutor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }

        public ActionResult Cmb_ProfesorInspector()
        {
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info();
            return PartialView("_CmbProfesorInspector", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_inspector(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_inspector(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
        }

        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }
        public ActionResult ComboBoxPartial_Nivel()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult ComboBoxPartial_Jornada()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
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

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Curso_Paralelo_Info> lista = bus_ParaleloPorCurso.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_ParaleloPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Curso_Paralelo_Info model)
        {
            List<aca_AnioLectivo_Curso_Paralelo_Info> lista = bus_ParaleloPorCurso.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_ParaleloPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AnioLectivoCurso()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Curso_Paralelo_Info> model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AnioLectivoCurso", model);
        }
        #endregion

        #region FuncionesDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AsignacionTutorInspector()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AsignacionTutorInspector", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AsignacionTutorInspector_Consultar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AsignacionTutorInspector_Consultar", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Curso_Paralelo_Info info_det)
        {
            Lista_ParaleloPorCurso.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AsignacionTutorInspector", model);
        }
        #endregion

        #region Acciones
        public ActionResult Consultar(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info
            {
                IdEmpresa = IdEmpresa,
                IdAnio = IdAnio,
                IdSede = IdSede,
                IdNivel = IdNivel,
                IdJornada = IdJornada,
                IdCurso = IdCurso,
            };
            model.lst_detalle = new List<aca_AnioLectivo_Curso_Paralelo_Info>();
            model.lst_detalle = bus_ParaleloPorCurso.GetListByCurso(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);

            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "AsignacionTutorInspector", "Index");
            var info_anio = bus_anio.GetInfo(model.IdEmpresa, model.IdAnio);
            if (info_anio.BloquearMatricula == true)
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
            Lista_ParaleloPorCurso.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info
            {
                IdEmpresa = IdEmpresa,
                IdAnio = IdAnio,
                IdSede = IdSede,
                IdNivel = IdNivel,
                IdJornada = IdJornada,
                IdCurso = IdCurso,
            };
            model.lst_detalle = new List<aca_AnioLectivo_Curso_Paralelo_Info>();
            model.lst_detalle = bus_ParaleloPorCurso.GetListByCurso(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lista_ParaleloPorCurso.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }
        #endregion
    }
}