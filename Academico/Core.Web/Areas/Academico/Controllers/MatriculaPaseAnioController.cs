using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaPaseAnioController : Controller
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
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_MatriculaCalificacionParcial_Bus bus_calificacion_parcial = new aca_MatriculaCalificacionParcial_Bus();
        aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
        aca_MatriculaConducta_Bus bus_conducta = new aca_MatriculaConducta_Bus();
        aca_MatriculaPaseAnio_List Lista_MatriculaPaseAnio = new aca_MatriculaPaseAnio_List();
        aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        string mensaje = string.Empty;
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
            Lista_MatriculaPaseAnio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_MatriculaCalificacion_Info model)
        {
            List<aca_Matricula_Info> lista = bus_matricula.GetList_Calificaciones(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo, model.IdAlumno);
            List<aca_MatriculaCalificacionParcial_Info> lst_calificacion_parcial = new List<aca_MatriculaCalificacionParcial_Info>();
            List<aca_MatriculaCalificacion_Info> lst_calificacion = new List<aca_MatriculaCalificacion_Info>();
            List<aca_MatriculaConducta_Info> lst_conducta = new List<aca_MatriculaConducta_Info>();

            lst_calificacion = bus_calificacion.GetList_PaseAnio(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo, model.IdAlumno);
            var info_anio = bus_anio.GetInfo(model.IdEmpresa, model.IdAnio);
            List<aca_MatriculaCalificacionParcial_Info> ListaCalificacionParcial = new List<aca_MatriculaCalificacionParcial_Info>();
            if (lista.Count() > 0)
            {
                foreach (var item in lista)
                {
                    var lst_x_matricula = lst_calificacion.Where(q=>q.IdEmpresa == item.IdEmpresa && q.IdMatricula==item.IdMatricula);
                    var info_matricula = bus_matricula.GetInfo(item.IdEmpresa,item.IdMatricula);
                    var info_alumno = bus_alumno.GetInfo(item.IdEmpresa, info_matricula.IdAlumno);
                    var lst_agrupada = lst_x_matricula.GroupBy(q=>new {q.IdEmpresa, q.IdMatricula, q.IdMateria }).ToList();
                    decimal PromedioFinal = 0;
                    decimal PromedioGeneral = 0;
                    decimal PromedioFinalTemp = 0;
                    decimal PromedioMinimoPromocion = Math.Round(Convert.ToDecimal(info_anio.PromedioMinimoPromocion),2,MidpointRounding.AwayFromZero);

                    foreach (var item_x_matricula in lst_x_matricula)
                    {
                        decimal PromedioFinalQ1 = Convert.ToDecimal(item_x_matricula.PromedioFinalQ1);
                        decimal PromedioFinalQ2 = Convert.ToDecimal(item_x_matricula.PromedioFinalQ2);
                        decimal ExamenMejoramiento = Convert.ToDecimal(item_x_matricula.ExamenMejoramiento);
                        decimal ExamenSupletorio = Convert.ToDecimal(item_x_matricula.ExamenSupletorio);
                        decimal ExamenRemedial = Convert.ToDecimal(item_x_matricula.ExamenRemedial);
                        decimal ExamenGracia = Convert.ToDecimal(item_x_matricula.ExamenGracia);
                        PromedioFinalTemp = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);

                        if (PromedioFinalTemp < PromedioMinimoPromocion)
                        {
                            if (ExamenSupletorio >= PromedioMinimoPromocion)
                            {
                                PromedioFinal = PromedioMinimoPromocion;
                            }
                            else if (ExamenRemedial >= PromedioMinimoPromocion)
                            {
                                PromedioFinal = PromedioMinimoPromocion;
                            }
                            else if (ExamenGracia >= PromedioMinimoPromocion)
                            {
                                PromedioFinal = PromedioMinimoPromocion;
                            }
                            else
                            {
                                PromedioFinal = PromedioFinalTemp;
                            }

                        }
                        else
                        {
                            if (ExamenMejoramiento > 0)
                            {
                                if (PromedioFinalQ1 < PromedioFinalQ2)
                                {
                                    PromedioFinalQ1 = ExamenMejoramiento;
                                }
                                else if (PromedioFinalQ2 < PromedioFinalQ1)
                                {
                                    PromedioFinalQ2 = ExamenMejoramiento;
                                }

                                PromedioFinal = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                PromedioFinal = PromedioFinalTemp;
                            }
                        }

                        var info_calificacion = new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = item_x_matricula.IdEmpresa,
                            IdMatricula = item_x_matricula.IdMatricula,
                            pe_nombreCompletoAlumno = item_x_matricula.pe_nombreCompletoAlumno,
                            IdMateria = item_x_matricula.IdMateria,
                            IdCatalogoParcial = item_x_matricula.IdCatalogoParcial,
                            IdProfesor = item_x_matricula.IdProfesor,
                            CalificacionP1 = item_x_matricula.CalificacionP1,
                            CalificacionP2 = item_x_matricula.CalificacionP2,
                            CalificacionP3 = item_x_matricula.CalificacionP3,
                            PromedioQ1 = item_x_matricula.PromedioQ1,
                            ExamenQ1 = item_x_matricula.ExamenQ1,
                            PromedioFinalQ1 = PromedioFinalQ1,
                            CalificacionP4 = item_x_matricula.CalificacionP4,
                            CalificacionP5 = item_x_matricula.CalificacionP5,
                            CalificacionP6 = item_x_matricula.CalificacionP6,
                            PromedioQ2 = item_x_matricula.PromedioQ2,
                            ExamenQ2 = item_x_matricula.ExamenQ2,
                            PromedioFinalQ2 = PromedioFinalQ2,
                            ExamenMejoramiento = item_x_matricula.ExamenMejoramiento,
                            ExamenSupletorio = item_x_matricula.ExamenSupletorio,
                            ExamenRemedial = item_x_matricula.ExamenRemedial,
                            ExamenGracia = item_x_matricula.ExamenGracia,
                            PromedioFinal = PromedioFinal
                        };
                        PromedioGeneral = PromedioGeneral + PromedioFinal;

                        if (!bus_calificacion.ModicarPaseAnioDB(info_calificacion))
                        {
                        }
                    }

                    PromedioGeneral = Math.Round((PromedioGeneral / lst_agrupada.Count()), 2, MidpointRounding.AwayFromZero);
                    item.PromedioFinal = PromedioGeneral;
                    var PaseAnio = true;
                    foreach (var item1 in lst_x_matricula)
                    {
                        info_alumno.IdUsuarioModificacion = SessionFixed.IdUsuario;
                        info_alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.PROMOVIDO);
                        if (item1.PromedioFinal < PromedioMinimoPromocion)
                        {       
                            info_alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.NO_PROMOVIDO);
                            bus_alumno.PaseAnioDB(info_alumno);
                            PaseAnio = false;
                            break;
                        }
                    }
                    var info_catalogo = bus_catalogo.GetInfo(info_alumno.IdCatalogoESTMAT);
                    item.NomCatalogoESTMAT = info_catalogo.NomCatalogo;
                    item.PaseAnio = PaseAnio;
                }
            }
            Lista_MatriculaPaseAnio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaPaseAnio()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_MatriculaPaseAnio.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaPaseAnio", model);
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

    public class aca_MatriculaPaseAnio_List
    {
        string Variable = "aca_MatriculaMatriculaPaseAnio";
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