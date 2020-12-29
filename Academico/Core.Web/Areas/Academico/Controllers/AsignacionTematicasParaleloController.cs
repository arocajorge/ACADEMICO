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
    public class AsignacionTematicasParaleloController : Controller
    {
        #region Variables
        aca_AnioLectivo_Curso_Paralelo_List Lista_Paralelo = new aca_AnioLectivo_Curso_Paralelo_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Curso_Paralelo_Bus bus_ParaleloPorCurso = new aca_AnioLectivo_Curso_Paralelo_Bus();
        aca_AnioLectivo_Tematica_Bus bus_anio_tematica = new aca_AnioLectivo_Tematica_Bus();
        aca_MatriculaCalificacionParticipacion_Bus bus_calificacion_participacion = new aca_MatriculaCalificacionParticipacion_Bus();
        aca_MatriculaCalificacionParticipacion_List Lista_ParaleloPorCurso = new aca_MatriculaCalificacionParticipacion_List();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_Profesor()
        {
            aca_MatriculaCalificacionParticipacion_Info model = new aca_MatriculaCalificacionParticipacion_Info();
            return PartialView("_CmbProfesor", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROFESOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROFESOR.ToString());
        }

        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_MatriculaCalificacionParticipacion_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_MatriculaCalificacionParticipacion_Info { IdAnio = IdAnio });
        }
        public ActionResult ComboBoxPartial_Nivel()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel", new aca_MatriculaCalificacionParticipacion_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult ComboBoxPartial_Jornada()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada", new aca_MatriculaCalificacionParticipacion_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_MatriculaCalificacionParticipacion_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }
        #endregion

        #region Metodos
        private void cargar_combos_detalle(int IdEmpresa, int IdAnio)
        {
            var lst_tematicas = bus_anio_tematica.GetList(IdEmpresa, IdAnio);
            ViewBag.lst_tematicas = lst_tematicas;
            
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
            aca_MatriculaCalificacionParticipacion_Info model = new aca_MatriculaCalificacionParticipacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            ViewBag.IdEmpresa = model.IdEmpresa;
            ViewBag.IdAnio = model.IdAnio;
            model.lst_detalle = new List<aca_MatriculaCalificacionParticipacion_Info>();
            model.lst_detalle = bus_calificacion_participacion.GetListParalelo(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso);

            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "AsignacionTematicasParalelo", "Index");

            if (info_anio.BloquearMatricula == true)
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lista_ParaleloPorCurso.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_MatriculaCalificacionParticipacion_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            ViewBag.IdEmpresa = model.IdEmpresa;
            ViewBag.IdAnio = model.IdAnio;
            model.lst_detalle = new List<aca_MatriculaCalificacionParticipacion_Info>();
            model.lst_detalle = bus_calificacion_participacion.GetListParalelo(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso);

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lista_ParaleloPorCurso.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            return View(model);
        }
        #endregion

        #region FuncionesDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AsignacionTematicasParalelo(int IdEmpresa, int IdAnio)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAnio = IdAnio;
            cargar_combos_detalle(IdEmpresa, IdAnio);
            return PartialView("_GridViewPartial_AsignacionTematicasParalelo", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_MatriculaCalificacionParticipacion_Info info_det)
        {
            var IdEmpresa = Convert.ToInt32(info_det.IdString.Substring(0,3));
            var IdAnio = Convert.ToInt32(info_det.IdString.Substring(3, 3));

            if (info_det.IdTematicaParticipacion!=0 && info_det.IdProfesor!=0)
            {
                Lista_ParaleloPorCurso.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle(IdEmpresa, IdAnio);
            return PartialView("_GridViewPartial_AsignacionTematicasParalelo", model);
        }
        #endregion
    }

    public class aca_MatriculaCalificacionParticipacion_List
    {
        string Variable = "aca_MatriculaCalificacionParticipacion_Info";
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_AnioLectivo_Tematica_Bus bus_anio_tematica = new aca_AnioLectivo_Tematica_Bus();
        aca_MatriculaCalificacionParticipacion_Bus bus_participacion = new aca_MatriculaCalificacionParticipacion_Bus();
        public List<aca_MatriculaCalificacionParticipacion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCalificacionParticipacion_Info> list = new List<aca_MatriculaCalificacionParticipacion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCalificacionParticipacion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCalificacionParticipacion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaCalificacionParticipacion_Info info_det, decimal IdTransaccionSession)
        {
            aca_MatriculaCalificacionParticipacion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdString == info_det.IdString).FirstOrDefault();
            //info_det.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var anio_tematica = bus_anio_tematica.getInfo(edited_info.IdEmpresa, edited_info.IdAnio, Convert.ToInt32(info_det.IdTematicaParticipacion));
            edited_info.IdProfesor = info_det.IdProfesor;
            edited_info.IdCampoAccion = anio_tematica.IdCampoAccion;
            edited_info.IdTematica = anio_tematica.IdTematica;
            var lst_calificacion_participacion = new List<aca_MatriculaCalificacionParticipacion_Info>();

            var lst_matricula = bus_matricula.GetList_PorCurso(edited_info.IdEmpresa,edited_info.IdAnio, edited_info.IdSede,edited_info.IdNivel, edited_info.IdJornada,edited_info.IdCurso,edited_info.IdParalelo);
            foreach (var item in lst_matricula)
            {
                var existe_participacion = bus_participacion.GetInfo_X_Matricula(edited_info.IdEmpresa, Convert.ToDecimal(item.IdMatricula));
                var info = new aca_MatriculaCalificacionParticipacion_Info
                {
                    IdEmpresa = edited_info.IdEmpresa,
                    IdAnio = edited_info.IdAnio,
                    IdMatricula = item.IdMatricula,
                    IdAlumno = item.IdAlumno,
                    IdTematica = edited_info.IdTematica,
                    IdCampoAccion = edited_info.IdCampoAccion,
                    IdProfesor = edited_info.IdProfesor,
                    CalificacionP1 = existe_participacion==null ? null : existe_participacion.CalificacionP1,
                    CalificacionP2= existe_participacion == null ? null : existe_participacion.CalificacionP2,
                    CalificacionP3= existe_participacion == null ? null : existe_participacion.CalificacionP3,
                    CalificacionP4 = existe_participacion == null ? null : existe_participacion.CalificacionP4,
                    PromedioQ1 = existe_participacion == null ? null : existe_participacion.PromedioQ1,
                    PromedioQ2 = existe_participacion == null ? null : existe_participacion.PromedioQ2,
                    PromedioFinal = existe_participacion == null ? null : existe_participacion.PromedioFinal,
                    IdUsuarioCreacion = existe_participacion==null ? SessionFixed.IdUsuario : existe_participacion.IdUsuarioCreacion,
                    FechaCreacion = existe_participacion == null ? DateTime.Now : existe_participacion.FechaCreacion,
                    IdUsuarioModificacion = existe_participacion==null ? null :SessionFixed.IdUsuario,
                    FechaModificacion = existe_participacion == null ? (DateTime?)null : DateTime.Now
                };

                lst_calificacion_participacion.Add(info);
            }

            if (bus_participacion.Guardar(lst_calificacion_participacion))
            {

            }


        }
    }
}