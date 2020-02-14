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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class CambioPlantillaMatriculaController : Controller
    {
        #region Variables
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_Matricula_List Lista_Matricula = new aca_Matricula_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        aca_Matricula_PorCurso_List Lista_Matricula_PorCurso = new aca_Matricula_PorCurso_List();
        aca_MecanismoDePago_Bus bus_mecanismo = new aca_MecanismoDePago_Bus();
        aca_Plantilla_Rubro_Bus bus_plantilla_rubro = new aca_Plantilla_Rubro_Bus();
        //aca_Plantilla_Rubro_List Lista_DetallePlantilla = new aca_Plantilla_Rubro_List();
        aca_PermisoMatricula_Bus bus_permiso = new aca_PermisoMatricula_Bus();
        aca_Matricula_Rubro_List ListaMatriculaRubro = new aca_Matricula_Rubro_List();
        aca_Matricula_Rubro_Bus bus_matricula_rubro = new aca_Matricula_Rubro_Bus();
        aca_AnioLectivo_Jornada_Curso_Bus bus_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Bus();
        aca_Plantilla_Bus bus_plantilla = new aca_Plantilla_Bus();
        aca_AlumnoDocumento_Bus bus_alumno_documento = new aca_AlumnoDocumento_Bus();
        aca_AnioLectivo_Curso_Documento_List Lista_DocumentosMatricula = new aca_AnioLectivo_Curso_Documento_List();
        aca_AnioLectivo_Jornada_Curso_Bus bus_aniolectivo_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Bus();
        aca_AnioLectivo_Curso_Documento_Bus bus_curso_documento = new aca_AnioLectivo_Curso_Documento_Bus();
        aca_SocioEconomico_Bus bus_socioeconomico = new aca_SocioEconomico_Bus();
        aca_AnioLectivo_Paralelo_Profesor_Bus bus_materias_x_paralelo = new aca_AnioLectivo_Paralelo_Profesor_Bus();
        aca_AnioLectivo_Periodo_Bus bus_anio_periodo = new aca_AnioLectivo_Periodo_Bus();
        aca_Paralelo_Bus bus_paralelo = new aca_Paralelo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion

        #region Combobox bajo demanda de paralelo
        public List<aca_Paralelo_Info> get_list_bajo_demanda_paralelo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnioBajoDemanda ?? "-1");
            int IdSede = Convert.ToInt32(SessionFixed.IdSedeBajoDemanda ?? "-1");
            int IdJornada = Convert.ToInt32(SessionFixed.IdJornadaBajoDemanda ?? "-1");
            int IdNivel = Convert.ToInt32(SessionFixed.IdNivelBajoDemanda ?? "-1");
            int IdCurso = Convert.ToInt32(SessionFixed.IdCursoBajoDemanda ?? "-1");
            return bus_paralelo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
        }
        public aca_Paralelo_Info get_info_bajo_demanda_paralelo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_paralelo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_MatriculaAlumno()
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
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            decimal IdAlumno = (Request.Params["IdAlumno"] != null) ? decimal.Parse(Request.Params["IdAlumno"]) : -1;
            string Validar = (Request.Params["Validar"] != null) ? Convert.ToString(Request.Params["Validar"]) : "N";
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdAlumno = IdAlumno, Validar = Validar });
        }

        public ActionResult ComboBoxPartial_Plantilla()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            string IdComboCurso = (Request.Params["IdCurso"] != null) ? (Request.Params["IdCurso"]).ToString() : null;
            int IdSede = -1;
            int IdNivel = -1;
            int IdJornada = -1;
            int IdCurso = -1;

            if (!string.IsNullOrEmpty(IdComboCurso))
            {
                var regex = new Regex(@".{4}");
                string result = regex.Replace(IdComboCurso, "$&" + Environment.NewLine);
                string[] array = result.Split('\n');
                if (array.Count() >= 5)
                {
                    IdAnio = Convert.ToInt32(array[1]);
                    IdSede = Convert.ToInt32(array[2]);
                    IdNivel = Convert.ToInt32(array[3]);
                    IdJornada = Convert.ToInt32(array[4]);
                    IdCurso = Convert.ToInt32(array[5]);
                }
            }
            return PartialView("_ComboBoxPartial_Plantilla", new aca_AnioLectivo_Curso_Plantilla_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
        }

        public ActionResult ComboBoxPartial_Paralelo(int? id)
        {
            return PartialView("_ComboBoxPartial_Paralelo", id);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_mecanismo = bus_mecanismo.GetList(IdEmpresa, false);
            ViewBag.lst_mecanismo = lst_mecanismo;
        }
        #endregion

        #region Funciones del detalle (modificar)
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_mecanismo = bus_mecanismo.GetList(IdEmpresa, false);
            ViewBag.lst_mecanismo = lst_mecanismo;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaRubro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_Matricula_Rubro_Info> model = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaRubro", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_Matricula_Rubro_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (ModelState.IsValid)
                ListaMatriculaRubro.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaRubro", model);
        }
        #endregion

        #region Json

        #endregion
        public JsonResult SetDatosMatricula(int IdEmpresa = 0, decimal IdAlumno = 0, int IdAnio=0)
        {
            var model = bus_matricula.GetInfo_ExisteMatricula(IdEmpresa,IdAnio,IdAlumno);
            if (model == null) model = new aca_Matricula_Info();
            model.Validar = "S";
            //model.IdComboCurso = model.IdEmpresa.ToString("0000") + model.IdAnio.ToString("0000")+ model.IdSede.ToString("0000")+ model.IdNivel.ToString("0000")+ model.IdJornada.ToString("0000") + model.IdCurso.ToString("0000");
            var info_curso = bus_jornada_curso.GetInfoCursoMatricula(model.IdEmpresa, model.IdAnio, model.IdMatricula);
            if (info_curso == null) info_curso = new aca_AnioLectivo_Jornada_Curso_Info();
            model.IdComboCurso = (info_curso == null ? "" : info_curso.IdComboCurso);
            model.NomCurso = (info_curso == null ? "" : info_curso.NomCurso);


            if (!string.IsNullOrEmpty(model.IdComboCurso))
            {
                var regex = new Regex(@".{4}");
                string result = regex.Replace(model.IdComboCurso, "$&" + Environment.NewLine);
                string[] array = result.Split('\n');
                if (array.Count() >= 5)
                {
                    SessionFixed.IdAnioBajoDemanda = Convert.ToInt32(array[1]).ToString();
                    SessionFixed.IdSedeBajoDemanda = Convert.ToInt32(array[2]).ToString();
                    SessionFixed.IdNivelBajoDemanda = Convert.ToInt32(array[3]).ToString();
                    SessionFixed.IdJornadaBajoDemanda = Convert.ToInt32(array[4]).ToString();
                    SessionFixed.IdCursoBajoDemanda = Convert.ToInt32(array[5]).ToString();
                }
            }

            model.lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            model.lst_MatriculaRubro = bus_matricula_rubro.GetList(model.IdEmpresa, model.IdMatricula);
            ListaMatriculaRubro.set_list(model.lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #region Acciones
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_Matricula_Info model = new aca_Matricula_Info();
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa, 0);
            var IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.IdEmpresa = IdEmpresa;
            model.IdAnio = IdAnio;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Validar = "S";
            model.lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            model.lst_MatriculaRubro = bus_matricula_rubro.GetList(model.IdEmpresa, model.IdMatricula);
            ListaMatriculaRubro.set_list(model.lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            SessionFixed.IdAnioBajoDemanda = "-1";
            SessionFixed.IdSedeBajoDemanda = "-1";
            SessionFixed.IdNivelBajoDemanda = "-1";
            SessionFixed.IdJornadaBajoDemanda = "-1";
            SessionFixed.IdCursoBajoDemanda = "-1";
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Matricula_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_MatriculaRubro = ListaMatriculaRubro.get_list(model.IdTransaccionSession);

            if (!bus_matricula.ModificarPlantillaDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMatricula = model.IdMatricula, Exito = true });
        }
        #endregion
    }
}