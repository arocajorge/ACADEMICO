using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCalificacionController : Controller
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
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_MatriculaCalificacionParcial_Bus bus_calificacion_parcial = new aca_MatriculaCalificacionParcial_Bus();
        aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
        aca_AnioLectivo_Paralelo_Profesor_Bus bus_materias_x_paralelo = new aca_AnioLectivo_Paralelo_Profesor_Bus();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_MatriculaConducta_Bus bus_conducta = new aca_MatriculaConducta_Bus();
        aca_MatriculaGeneracionCalificaciones_List Lista_MatriculaCalificaciones = new aca_MatriculaGeneracionCalificaciones_List();
        string mensaje = string.Empty;
        string MensajeSuccess = string.Empty;
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
            aca_MatriculaCalificacion_Info model = new aca_MatriculaCalificacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdParalelo = 0,
                IdAlumno = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            List<aca_Matricula_Info> lista = new List<aca_Matricula_Info>();
            Lista_MatriculaCalificaciones.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_MatriculaCalificacion_Info model)
        {
            List<aca_Matricula_Info> lista = bus_matricula.GetList_Calificaciones(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo, model.IdAlumno);
            List<aca_MatriculaCalificacionParcial_Info> lst_calificacion_parcial_existente = new List<aca_MatriculaCalificacionParcial_Info>();
            List<aca_MatriculaCalificacion_Info> lst_calificacion_existente = new List<aca_MatriculaCalificacion_Info>();
            List<aca_MatriculaConducta_Info> lst_conducta_existente = new List<aca_MatriculaConducta_Info>();

            List<aca_MatriculaCalificacionParcial_Info> lst_calificacion_parcial = new List<aca_MatriculaCalificacionParcial_Info>();
            List<aca_MatriculaCalificacion_Info> lst_calificacion = new List<aca_MatriculaCalificacion_Info>();
            List<aca_MatriculaConducta_Info> lst_conducta = new List<aca_MatriculaConducta_Info>();

            if (lista.Count()>0)
            {
                foreach (var item in lista)
                {
                    #region Calificacion y conducta
                    lst_calificacion_parcial_existente = bus_calificacion_parcial.GetList(model.IdEmpresa, item.IdMatricula);
                    lst_calificacion_existente = bus_calificacion.GetList(model.IdEmpresa, item.IdMatricula);
                    lst_conducta_existente = bus_conducta.GetList(model.IdEmpresa, item.IdMatricula);

                    var lst_materias_x_curso = bus_materias_x_paralelo.GetList(item.IdEmpresa, item.IdSede, item.IdAnio, item.IdNivel, item.IdJornada, item.IdCurso, item.IdParalelo);
                    var lst_parcial = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);

                    if (lst_materias_x_curso != null && lst_materias_x_curso.Count > 0)
                    {
                        foreach (var item_materias in lst_materias_x_curso)
                        {
                            var calificacion = lst_calificacion_existente.Where(q => q.IdMateria == item_materias.IdMateria).FirstOrDefault();
                            var conducta = lst_conducta_existente.Where(q => q.IdMateria == item_materias.IdMateria).FirstOrDefault();

                            if (lst_parcial.Count()>0)
                            {
                                foreach (var item_p in lst_parcial)
                                {
                                    var calificacion_parcial = lst_calificacion_parcial_existente.Where(q => q.IdCatalogoParcial== item_p.IdCatalogoParcial && q.IdMateria == item_materias.IdMateria).FirstOrDefault();

                                    var info_calificacion_parcial = new aca_MatriculaCalificacionParcial_Info
                                    {
                                        IdEmpresa = item.IdEmpresa,
                                        IdMatricula=item.IdMatricula,
                                        IdMateria = item_materias.IdMateria,
                                        IdCatalogoParcial = item_p.IdCatalogoParcial,
                                        IdProfesor = item_materias.IdProfesor,
                                        Calificacion1 = (calificacion_parcial==null ? null : calificacion_parcial.Calificacion1),
                                        Calificacion2 = (calificacion_parcial == null ? null : calificacion_parcial.Calificacion2),
                                        Calificacion3 = (calificacion_parcial == null ? null : calificacion_parcial.Calificacion3),
                                        Calificacion4 = (calificacion_parcial == null ? null : calificacion_parcial.Calificacion4),
                                        Evaluacion = (calificacion_parcial == null ? null : calificacion_parcial.Evaluacion),
                                        Remedial1 = (calificacion_parcial == null ? null : calificacion_parcial.Remedial1),
                                        Remedial2 = (calificacion_parcial == null ? null : calificacion_parcial.Remedial2),
                                        Conducta = (calificacion_parcial == null ? null : calificacion_parcial.Conducta),
                                        MotivoCalificacion = (calificacion_parcial == null ? null : calificacion_parcial.MotivoCalificacion),
                                        MotivoConducta = (calificacion_parcial == null ? null : calificacion_parcial.MotivoConducta),
                                        AccionRemedial = (calificacion_parcial == null ? null : calificacion_parcial.AccionRemedial),
                                        IdUsuarioCreacion = SessionFixed.IdUsuario
                                    };

                                    lst_calificacion_parcial.Add(info_calificacion_parcial);
                                }
                            }

                            var info_calificacion = new aca_MatriculaCalificacion_Info
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdMatricula = item.IdMatricula,
                                IdMateria = item_materias.IdMateria,
                                IdProfesor = item_materias.IdProfesor,
                                CalificacionP1 = (calificacion == null ? null : calificacion.CalificacionP1),
                                CalificacionP2 = (calificacion == null ? null : calificacion.CalificacionP2),
                                CalificacionP3 = (calificacion == null ? null : calificacion.CalificacionP3),
                                CalificacionP4 = (calificacion == null ? null : calificacion.CalificacionP4),
                                CalificacionP5 = (calificacion == null ? null : calificacion.CalificacionP5),
                                CalificacionP6 = (calificacion == null ? null : calificacion.CalificacionP6),
                                PromedioQ1 = (calificacion == null ? null : calificacion.PromedioQ1),
                                PromedioQ2 = (calificacion == null ? null : calificacion.PromedioQ2),
                                ExamenQ1 = (calificacion == null ? null : calificacion.ExamenQ1),
                                ExamenQ2 = (calificacion == null ? null : calificacion.ExamenQ2),
                                PromedioFinalQ1 = (calificacion == null ? null : calificacion.PromedioFinalQ1),
                                PromedioFinalQ2 = (calificacion == null ? null : calificacion.PromedioFinalQ2),
                                ExamenMejoramiento = (calificacion == null ? null : calificacion.ExamenMejoramiento),
                                ExamenSupletorio = (calificacion == null ? null : calificacion.ExamenSupletorio),
                                ExamenRemedial = (calificacion == null ? null : calificacion.ExamenRemedial),
                                ExamenGracia = (calificacion == null ? null : calificacion.ExamenGracia),
                                PromedioFinal = (calificacion == null ? null : calificacion.PromedioFinal)
                            };

                            lst_calificacion.Add(info_calificacion);

                            var info_conducta = new aca_MatriculaConducta_Info
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdMatricula = item.IdMatricula,
                                IdMateria = item_materias.IdMateria,
                                IdProfesor = item_materias.IdProfesor,
                                CalificacionP1 = (conducta == null ? null : conducta.CalificacionP1),
                                CalificacionP2 = (conducta == null ? null : conducta.CalificacionP2),
                                CalificacionP3 = (conducta == null ? null : conducta.CalificacionP3),
                                CalificacionP4 = (conducta == null ? null : conducta.CalificacionP4),
                                CalificacionP5 = (conducta == null ? null : conducta.CalificacionP5),
                                CalificacionP6 = (conducta == null ? null : conducta.CalificacionP6),
                                PromedioFinalQ1 = (conducta == null ? null : conducta.PromedioFinalQ1),
                                PromedioFinalQ2 = (conducta == null ? null : conducta.PromedioFinalQ2),
                                PromedioQ1 = (conducta == null ? null : conducta.PromedioQ1),
                                PromedioQ2 = (conducta == null ? null : conducta.PromedioQ2),
                                PromedioGeneral = (conducta == null ? null : conducta.PromedioGeneral),
                                PromedioFinal = (conducta == null ? null : conducta.PromedioFinal)
                            };

                            lst_conducta.Add(info_conducta);
                        }
                    }
                    #endregion
                }

                if (bus_calificacion_parcial.GenerarCalificacion(lst_calificacion_parcial))
                {
                    if (bus_calificacion.GenerarCalificacion(lst_calificacion))
                    {
                        if (bus_conducta.GenerarCalificacion(lst_conducta))
                        {
                            MensajeSuccess = "Registros generados exitosamente";
                            ViewBag.MensajeSuccess = MensajeSuccess;
                            var ListaGenerada = new List<aca_Matricula_Info>();
                            //var ListaGenerada = bus_calificacion.GetList(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo, model.IdAlumno);

                            var lst_matricula = (from q in lst_conducta
                                                 group q by new
                                                 {
                                                     q.IdEmpresa,
                                                     q.IdMatricula
                                                 } into mat
                                                 select new aca_Matricula_Info
                                                 {
                                                     IdEmpresa = mat.Key.IdEmpresa,
                                                     IdMatricula = mat.Key.IdMatricula
                                                 }).ToList();

                            foreach (var item in lst_matricula)
                            {
                                var matricula = lista.Where(q=>q.IdEmpresa==item.IdEmpresa && q.IdMatricula==item.IdMatricula).FirstOrDefault();
                                if (matricula!=null)
                                {
                                    ListaGenerada.Add(matricula);
                                }                             
                            }
                            Lista_MatriculaCalificaciones.set_list(ListaGenerada, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                        }
                        else
                        {
                            mensaje = "Ha ocurrido un error al generar las calificaciones de conducta";
                            ViewBag.mensaje = mensaje;
                        }
                    }
                    else
                    {
                        mensaje = "Ha ocurrido un error al generar las calificaciones";
                        ViewBag.mensaje = mensaje;
                    }
                }
                else
                {
                    mensaje = "Ha ocurrido un error al generar las calificaciones parciales";
                    ViewBag.mensaje = mensaje;
                }
            }

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_MatriculaCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaCalificacion", model);
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

        public ActionResult ComboBoxPartial_Alumno()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdCurso"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;
            var IdParalelo = (Request.Params["IdParalelo"] != null) ? int.Parse(Request.Params["IdParalelo"]) : -1;
            return PartialView("_ComboBoxPartial_Alumno", new aca_Matricula_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso, IdParalelo = IdParalelo });
        }
        #endregion
    }

    public class aca_MatriculaCalificacion_List
    {
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        string Variable = "aca_MatriculaCalificacion_Info";
        public List<aca_MatriculaCalificacion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCalificacion_Info> list = new List<aca_MatriculaCalificacion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCalificacion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCalificacion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaCalificacion_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_MatriculaCalificacion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdAlumno == info_det.IdAlumno).FirstOrDefault();
            edited_info.IdProfesor = info_det.IdProfesor;

            var profesor = bus_profesor.GetInfo(IdEmpresa, Convert.ToDecimal(info_det.IdProfesor));
            if (profesor != null)
                info_det.pe_nombreCompleto = profesor.pe_nombreCompleto;
            edited_info.pe_nombreCompleto = info_det.pe_nombreCompleto;
        }
    }

    public class aca_MatriculaGeneracionCalificaciones_List
    {
        string Variable = "aca_MatriculaGeneracionCalificaciones";
        public List<aca_Matricula_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Matricula_Info> list = new List<aca_Matricula_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Matricula_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Matricula_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}