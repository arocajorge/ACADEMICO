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
    public class MateriaPorCursoController : Controller
    {
        #region Variables
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        aca_Jornada_Bus bus_jornada = new aca_Jornada_Bus();
        aca_Curso_Bus bus_curso = new aca_Curso_Bus();
        aca_Paralelo_Bus bus_paralelo = new aca_Paralelo_Bus();
        aca_Materia_Bus bus_materia = new aca_Materia_Bus();
        aca_AnioLectivo_Curso_Materia_Bus bus_MateriaPorCurso = new aca_AnioLectivo_Curso_Materia_Bus();
        aca_AnioLectivo_Curso_Materia_List Lista_MateriaPorCurso = new aca_AnioLectivo_Curso_Materia_List();
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
            aca_AnioLectivo_Curso_Materia_Info model = new aca_AnioLectivo_Curso_Materia_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            List<aca_AnioLectivo_Curso_Materia_Info> lista = bus_MateriaPorCurso.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel, model.IdJornada, model.IdCurso);
            Lista_MateriaPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Curso_Materia_Info model)
        {
            List<aca_AnioLectivo_Curso_Materia_Info> lista = bus_MateriaPorCurso.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel, model.IdJornada, model.IdCurso);
            Lista_MateriaPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MateriaPorCurso()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Curso_Materia_Info> model = Lista_MateriaPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MateriaPorCurso", model);
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
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var resultado = 1;
            List<aca_AnioLectivo_Curso_Materia_Info> lista = new List<aca_AnioLectivo_Curso_Materia_Info>();
            string[] array = Ids.Split(',');

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    var info_materia = bus_materia.GetInfo(IdEmpresa, Convert.ToInt32(item));

                    aca_AnioLectivo_Curso_Materia_Info info = new aca_AnioLectivo_Curso_Materia_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdSede = IdSede,
                        IdAnio = IdAnio,
                        IdNivel = IdNivel,
                        IdJornada = IdJornada,
                        IdCurso = IdCurso,
                        IdMateria = Convert.ToInt32(item),
                        EsObligatorio = info_materia.EsObligatorio,
                        EsCompartida = info_materia.EsCompartida,
                        NomMateria = info_materia.NomMateria,
                        NomMateriaGrupo = info_materia.NomMateriaGrupo,
                        OrdenMateria = info_materia.OrdenMateria
                    };
                    lista.Add(info);
                }
            }

            if (!bus_MateriaPorCurso.GuardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, lista))
            {
                resultado = 0;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListMateriaPorCurso(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_AnioLectivo_Curso_Materia_Info> lista = new List<aca_AnioLectivo_Curso_Materia_Info>();
            lista = bus_MateriaPorCurso.GetListAsignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso);
            Lista_MateriaPorCurso.set_list(lista, IdTransaccionSession);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_AnioLectivo_Curso_Materia_List
    {
        string Variable = "aca_AnioLectivo_Curso_Materia_Info";
        public List<aca_AnioLectivo_Curso_Materia_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Curso_Materia_Info> list = new List<aca_AnioLectivo_Curso_Materia_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Curso_Materia_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Curso_Materia_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}