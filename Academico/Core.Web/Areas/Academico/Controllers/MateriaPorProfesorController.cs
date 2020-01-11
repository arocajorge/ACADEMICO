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
    public class MateriaPorProfesorController : Controller
    {
        #region Variables
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        aca_Jornada_Bus bus_jornada = new aca_Jornada_Bus();
        aca_Curso_Bus bus_curso = new aca_Curso_Bus();
        aca_Paralelo_Bus bus_paralelo = new aca_Paralelo_Bus();
        aca_Materia_Bus bus_materia = new aca_Materia_Bus();
        aca_AnioLectivo_Paralelo_Profesor_Bus bus_MateriaPorProfesor = new aca_AnioLectivo_Paralelo_Profesor_Bus();
        aca_AnioLectivo_Paralelo_Profesor_List Lista_MateriaPorProfesor = new aca_AnioLectivo_Paralelo_Profesor_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
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
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivo_Paralelo_Profesor_Info model = new aca_AnioLectivo_Paralelo_Profesor_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdParalelo = 0,
                IdMateria = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Paralelo_Profesor_Info> lista = bus_MateriaPorProfesor.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo);
            Lista_MateriaPorProfesor.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Paralelo_Profesor_Info model)
        {
            List<aca_AnioLectivo_Paralelo_Profesor_Info> lista = bus_MateriaPorProfesor.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo);
            Lista_MateriaPorProfesor.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MateriaPorProfesor()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Paralelo_Profesor_Info> model = Lista_MateriaPorProfesor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MateriaPorProfesor", model);
        }
        #endregion

        #region Combos
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }
        public ActionResult ComboBoxPartial_Nivel()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult ComboBoxPartial_Jornada()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }

        public ActionResult ComboBoxPartial_Paralelo()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdCurso"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;
            return PartialView("_ComboBoxPartial_Paralelo", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
        }

        public ActionResult ComboBoxPartial_Materia()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdCurso"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;
            return PartialView("_ComboBoxPartial_Materia", new aca_AnioLectivo_Curso_Materia_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso  });
        }
        #endregion

        #region Combos bajo demanda
        public ActionResult Cmb_Profesor()
        {
            aca_AnioLectivo_Paralelo_Profesor_Info model = new aca_AnioLectivo_Paralelo_Profesor_Info();
            return PartialView("_Cmb_Profesor", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROFESOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROFESOR.ToString());
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sede = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sede = lst_sede;

            var lst_anio = bus_anio.GetList(IdEmpresa, false);
            ViewBag.lst_anio = lst_anio;

            var lst_nivel = bus_nivel.GetList(IdEmpresa, false);
            ViewBag.lst_nivel = lst_nivel;

            var lst_jornada = bus_jornada.GetList(IdEmpresa, false);
            ViewBag.lst_jornada = lst_jornada;

            var lst_curso = bus_curso.GetList(IdEmpresa, false);
            ViewBag.lst_curso = lst_curso;
        }
        #endregion

        #region Funciones del Grid
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Paralelo_Profesor_Info info_det)
        {

            if (ModelState.IsValid)
                Lista_MateriaPorProfesor.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<aca_AnioLectivo_Paralelo_Profesor_Info> model = new List<aca_AnioLectivo_Paralelo_Profesor_Info>();
            model = Lista_MateriaPorProfesor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_MateriaPorProfesor", model);
        }
        #endregion
        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, decimal IdTransaccionSession = 0)
        {
            var resultado = 1;
            List<aca_AnioLectivo_Paralelo_Profesor_Info> lista = new List<aca_AnioLectivo_Paralelo_Profesor_Info>();
            lista = Lista_MateriaPorProfesor.get_list(IdTransaccionSession);

            if (!bus_MateriaPorProfesor.GuardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, lista))
            {
                resultado = 0;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetListMateriaPorProfesor(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdMateria = 0, decimal IdTransaccionSession = 0)
        //{
        //    List<aca_AnioLectivo_Paralelo_Profesor_Info> lista = new List<aca_AnioLectivo_Paralelo_Profesor_Info>();
        //    lista = bus_MateriaPorProfesor.GetListAsignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria);
        //    Lista_MateriaPorProfesor.set_list(lista, IdTransaccionSession);
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }

    public class aca_AnioLectivo_Paralelo_Profesor_List
    {
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        string Variable = "aca_AnioLectivo_Paralelo_Profesor_Info";
        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Paralelo_Profesor_Info> list = new List<aca_AnioLectivo_Paralelo_Profesor_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Paralelo_Profesor_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Paralelo_Profesor_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_AnioLectivo_Paralelo_Profesor_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_AnioLectivo_Paralelo_Profesor_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdMateria == info_det.IdMateria).FirstOrDefault();
            edited_info.IdProfesor = info_det.IdProfesor;

            var profesor = bus_profesor.GetInfo(IdEmpresa, Convert.ToDecimal(info_det.IdProfesor));
            if (profesor != null)
                info_det.pe_nombreCompleto = profesor.pe_nombreCompleto;
            edited_info.pe_nombreCompleto = info_det.pe_nombreCompleto;
        }
    }
}