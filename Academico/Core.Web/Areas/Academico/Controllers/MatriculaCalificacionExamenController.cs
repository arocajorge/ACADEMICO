using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCalificacionExamenController : Controller
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
        aca_MatriculaExamen_List Lista_CalificacionExamen = new aca_MatriculaExamen_List();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_MatriculaCalificacion_Combos_List ListaCombos = new aca_MatriculaCalificacion_Combos_List();
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
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
            aca_MatriculaCalificacion_Info model = new aca_MatriculaCalificacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdParalelo = 0,
                IdMateria = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacionExamen()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacion_Info> model = Lista_CalificacionExamen.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_MatriculaCalificacionExamen", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_MatriculaCalificacion_Info model)
        {
            var lst_examen = new List<aca_AnioLectivoParcial_Info>();
            lst_examen = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.EXSUP), DateTime.Now.Date);

            ViewBag.lst_examen = lst_examen;
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
            var IdParalelo = (Request.Params["IdParalelo"] != null) ? int.Parse(Request.Params["IdParalelo"]) : -1;
            return PartialView("_ComboBoxPartial_Materia", new aca_AnioLectivo_Paralelo_Profesor_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso, IdParalelo = IdParalelo });
        }
        #endregion

        #region FuncionesCombos
        public List<aca_AnioLectivo_Info> CargarAnio(int IdEmpresa = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.IdEmpresa == IdEmpresa).ToList();
            var lst_anio = (from q in lst_combos
                            group q by new
                            {
                                q.IdEmpresa,
                                q.IdAnio,
                                q.Descripcion
                            } into a
                            select new aca_AnioLectivo_Info
                            {
                                IdEmpresa = a.Key.IdEmpresa,
                                IdAnio = a.Key.IdAnio,
                                Descripcion = a.Key.Descripcion
                            }).OrderBy(q => q.Descripcion).ToList();

            var ListaAnio = new List<aca_AnioLectivo_Info>();

            foreach (var item in lst_anio)
            {
                ListaAnio.Add(new aca_AnioLectivo_Info
                {
                    IdAnio = item.IdAnio,
                    Descripcion = item.Descripcion
                });
            }

            return ListaAnio;
        }

        public List<aca_Sede_Info> CargarSede(int IdEmpresa = 0, int IdAnio = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

            var lst_sede = (from q in lst_combos
                            group q by new
                            {
                                q.IdEmpresa,
                                q.IdAnio,
                                q.IdSede,
                                q.NomSede
                            } into a
                            select new aca_Sede_Info
                            {
                                IdEmpresa = a.Key.IdEmpresa,
                                IdSede = a.Key.IdSede,
                                NomSede = a.Key.NomSede
                            }).OrderBy(q => q.NomSede).ToList();

            var ListaSede = new List<aca_Sede_Info>();

            foreach (var item in lst_sede)
            {
                ListaSede.Add(new aca_Sede_Info
                {
                    IdSede = item.IdSede,
                    NomSede = item.NomSede
                });
            }

            return ListaSede;
        }

        public List<aca_NivelAcademico_Info> CargarNivel(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

            var lst_nivel = (from q in lst_combos
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdAnio,
                                 q.IdSede,
                                 q.IdNivel,
                                 q.NomNivel,
                                 q.OrdenNivel
                             } into a
                             select new aca_NivelAcademico_Info
                             {
                                 IdEmpresa = a.Key.IdEmpresa,
                                 IdNivel = a.Key.IdNivel,
                                 NomNivel = a.Key.NomNivel,
                                 Orden = a.Key.OrdenNivel
                             }).OrderBy(q => q.Orden).ToList();

            var ListaNivel = new List<aca_NivelAcademico_Info>();

            foreach (var item in lst_nivel)
            {
                ListaNivel.Add(new aca_NivelAcademico_Info
                {
                    IdNivel = item.IdNivel,
                    NomNivel = item.NomNivel
                });
            }

            return ListaNivel;
        }

        public List<aca_Jornada_Info> CargarJornada(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel).ToList();

            var lst_jornada = (from q in lst_combos
                               group q by new
                               {
                                   q.IdEmpresa,
                                   q.IdAnio,
                                   q.IdSede,
                                   q.IdNivel,
                                   q.IdJornada,
                                   q.NomJornada,
                                   q.OrdenJornada
                               } into a
                               select new aca_Jornada_Info
                               {
                                   IdEmpresa = a.Key.IdEmpresa,
                                   IdJornada = a.Key.IdJornada,
                                   NomJornada = a.Key.NomJornada,
                                   OrdenJornada = a.Key.OrdenJornada
                               }).OrderBy(q => q.OrdenJornada).ToList();

            var ListaJornada = new List<aca_Jornada_Info>();

            foreach (var item in lst_jornada)
            {
                ListaJornada.Add(new aca_Jornada_Info
                {
                    IdJornada = item.IdJornada,
                    NomJornada = item.NomJornada
                });
            }

            return ListaJornada;
        }

        public List<aca_Curso_Info> CargarCurso(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada).ToList();

            var lst_curso = (from q in lst_combos
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdAnio,
                                 q.IdSede,
                                 q.IdNivel,
                                 q.IdJornada,
                                 q.IdCurso,
                                 q.NomCurso,
                                 q.OrdenCurso
                             } into a
                             select new aca_Curso_Info
                             {
                                 IdEmpresa = a.Key.IdEmpresa,
                                 IdCurso = a.Key.IdCurso,
                                 NomCurso = a.Key.NomCurso,
                                 OrdenCurso = a.Key.OrdenCurso
                             }).OrderBy(q => q.OrdenCurso).ToList();

            var ListaCurso = new List<aca_Curso_Info>();

            foreach (var item in lst_curso)
            {
                ListaCurso.Add(new aca_Curso_Info
                {
                    IdCurso = item.IdCurso,
                    NomCurso = item.NomCurso
                });
            }

            return ListaCurso;
        }

        public List<aca_Paralelo_Info> CargarParalelo(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();

            var lst_paralelo = (from q in lst_combos
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdAnio,
                                    q.IdSede,
                                    q.IdNivel,
                                    q.IdJornada,
                                    q.IdCurso,
                                    q.IdParalelo,
                                    q.NomParalelo,
                                    q.OrdenParalelo
                                } into a
                                select new aca_Paralelo_Info
                                {
                                    IdEmpresa = a.Key.IdEmpresa,
                                    IdParalelo = a.Key.IdParalelo,
                                    NomParalelo = a.Key.NomParalelo,
                                    OrdenParalelo = a.Key.OrdenParalelo,
                                }).OrderBy(q => q.OrdenParalelo).ToList();

            var ListaParalelo = new List<aca_Paralelo_Info>();

            foreach (var item in lst_paralelo)
            {
                ListaParalelo.Add(new aca_Paralelo_Info
                {
                    IdParalelo = item.IdParalelo,
                    NomParalelo = item.NomParalelo
                });
            }

            return ListaParalelo;
        }

        public List<aca_Materia_Info> CargarMateria(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).ToList();

            var lst_materia = (from q in lst_combos
                               group q by new
                               {
                                   q.IdEmpresa,
                                   q.IdAnio,
                                   q.IdSede,
                                   q.IdNivel,
                                   q.IdJornada,
                                   q.IdCurso,
                                   q.IdParalelo,
                                   q.IdMateria,
                                   q.NomMateria,
                                   q.OrdenMateria
                               } into a
                               select new aca_Materia_Info
                               {
                                   IdEmpresa = a.Key.IdEmpresa,
                                   IdMateria = a.Key.IdMateria,
                                   NomMateria = a.Key.NomMateria,
                                   OrdenMateria = a.Key.OrdenMateria
                               }).OrderBy(q => q.OrdenMateria).ToList();

            var ListaMateria = new List<aca_Materia_Info>();

            foreach (var item in lst_materia)
            {
                ListaMateria.Add(new aca_Materia_Info
                {
                    IdMateria = item.IdMateria,
                    NomMateria = item.NomMateria
                });
            }

            return ListaMateria;
        }
        #endregion

        #region Funciones del Grid
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_MatriculaCalificacion_Info info_det)
        {
            var IdCatalogoParcial = Request.Params["IdCatalogoParcial"] != null ? Request.Params["IdCatalogoParcial"].ToString() : "-1";
            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
                info_det.IdCatalogoParcial = Convert.ToInt32(IdCatalogoParcial);
                if (info_det != null)
                {
                    Lista_CalificacionExamen.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
            }

            List<aca_MatriculaCalificacion_Info> model = new List<aca_MatriculaCalificacion_Info>();
            model = Lista_CalificacionExamen.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaCalificacionExamen", model);
        }
        #endregion

        #region Json
        public JsonResult cargar_calificaciones(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdMateria = 0, int IdCatalogoParcial = 0)
        {
            List<aca_MatriculaCalificacion_Info> ListaCalificacionExamen = new List<aca_MatriculaCalificacion_Info>();

            ListaCalificacionExamen = bus_calificacion.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria);
            foreach (var item in ListaCalificacionExamen)
            {
                var calificacion_examen = bus_calificacion.GetInfo(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, item.IdAlumno);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI1))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenQ1);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI2))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenQ2);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXMEJ))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenMejoramiento);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXSUP))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenSupletorio);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXREM))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenRemedial);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXGRA))
                    item.CalificacionExamen = Convert.ToDecimal(calificacion_examen == null ? 0 : calificacion_examen.ExamenGracia);

                item.CalificacionExamen = (decimal)Math.Round(item.CalificacionExamen, 2, MidpointRounding.AwayFromZero);
            }
            ListaCalificacionExamen.ForEach(q=> q.IdCatalogoParcial = IdCatalogoParcial);
            Lista_CalificacionExamen.set_list(ListaCalificacionExamen, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(ListaCalificacionExamen, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

    public class aca_MatriculaExamen_List
    {
        aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
        string Variable = "aca_MatriculaExamen_Info";
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

            aca_MatriculaCalificacion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdMatricula == info_det.IdMatricula).FirstOrDefault();
            edited_info.CalificacionExamen = info_det.CalificacionExamen;

            bus_calificacion.ModicarDB(edited_info);
        }
    }
}