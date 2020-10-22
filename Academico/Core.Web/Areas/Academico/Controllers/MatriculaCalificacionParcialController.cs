using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCalificacionParcialController : Controller
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
        aca_MatriculaCalificacionParcial_List Lista_CalificacionParcial = new aca_MatriculaCalificacionParcial_List();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_MatriculaCalificacion_Combos_List ListaCombos = new aca_MatriculaCalificacion_Combos_List();
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia_promedio = new aca_AnioLectivoEquivalenciaPromedio_Bus();
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MatriculaCalificacionParcial_Info model = new aca_MatriculaCalificacionParcial_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdParalelo = 0,
                IdMateria = 0,
                IdCatalogoTipo = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1),
                //IdCatalogoParcial = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, model.IdAnio, model.IdSede, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.EsSuperAdmin = EsSuperAdmin;
            cargar_combos(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacionParcial()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacionParcial_Info> model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_MatriculaCalificacionParcial", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacionParcial_SuperAdmin()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacionParcial_Info> model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_MatriculaCalificacionParcial_SuperAdmin", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_MatriculaCalificacionParcial_Info model)
        {
            Dictionary<string, string> lst_quimestres = new Dictionary<string, string>();
            lst_quimestres.Add("6", "QUIMESTRE 1");
            lst_quimestres.Add("7", "QUIMESTRE 2");
            ViewBag.lst_quimestres = lst_quimestres;

            var lst_parcial = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdCatalogoTipo, DateTime.Now.Date);
            ViewBag.lst_parcial = lst_parcial;
            //var lst_parcial = new List<aca_AnioLectivoParcial_Info>();
            //var lst_quim1 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1), DateTime.Now.Date);
            //var lst_quim2 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2), DateTime.Now.Date);
            //lst_parcial.AddRange(lst_quim1);
            //lst_parcial.AddRange(lst_quim2);
            //ViewBag.lst_parcial = lst_parcial;
        }
        
        private void cargar_combos_detalle()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa,0);
            int IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);

            var lst_conducta = bus_conducta.GetList_Profesor(IdEmpresa, IdAnio);
            ViewBag.lst_conducta = lst_conducta;
        }

        #endregion

        #region Combos
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
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }

        public ActionResult ComboBoxPartial_Paralelo()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = !string.IsNullOrEmpty(Request.Params["IdCurso"]) ? int.Parse(Request.Params["IdCurso"]) : -1;
            return PartialView("_ComboBoxPartial_Paralelo", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
        }

        public ActionResult ComboBoxPartial_Materia()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = !string.IsNullOrEmpty(Request.Params["IdCurso"]) ? int.Parse(Request.Params["IdCurso"]) : -1;
            var IdParalelo = !string.IsNullOrEmpty(Request.Params["IdParalelo"]) ? int.Parse(Request.Params["IdParalelo"]) : -1;
            return PartialView("_ComboBoxPartial_Materia", new aca_AnioLectivo_Paralelo_Profesor_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso, IdParalelo = IdParalelo });
        }
        #endregion

        #region FuncionesCombos
        public List<aca_AnioLectivo_Info> CargarAnio(int IdEmpresa=0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q=>q.IdEmpresa == IdEmpresa).ToList();
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
                                 }).OrderBy(q=>q.Descripcion).ToList();

            var ListaAnio = new List<aca_AnioLectivo_Info>();

            foreach (var item in lst_anio)
            {
                ListaAnio.Add(new aca_AnioLectivo_Info {
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

        public List<aca_Jornada_Info> CargarJornada(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

            var lst_jornada = (from q in lst_combos
                               group q by new
                               {
                                   q.IdEmpresa,
                                   q.IdAnio,
                                   q.IdSede,
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

        public List<aca_NivelAcademico_Info> CargarNivel(int IdEmpresa = 0, int IdAnio = 0, int IdSede=0)
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

        public List<aca_Curso_Info> CargarCurso(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0)
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
                               }).OrderBy(q=> q.OrdenCurso).ToList();

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

        public List<aca_Paralelo_Info> CargarParalelo(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0, int IdCurso=0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso==IdCurso).ToList();

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
                             }).OrderBy(q=> q.OrdenParalelo).ToList();

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

        public List<aca_Materia_Info> CargarMateria(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0, int IdCurso = 0, int IdParalelo=0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = ListaCombos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo==IdParalelo).ToList();

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
                                }).OrderBy(q=>q.OrdenMateria).ToList();

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
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpress.Web.Mvc.DevExpressEditorsBinder))] aca_MatriculaCalificacionParcial_Info info_det)
        {

            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
                var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info_det.Conducta));
                var info_anio = bus_anio.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAnio);
                var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
                var lst_conducta = bus_conducta.GetList_IngresaMotivo(IdEmpresa, info_matricula.IdAnio);
                aca_MatriculaCalificacionParcial_Info registro_editar = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(m => m.IdEmpresa == IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();
                info_det.RegistroValido = true;
                info_det.RegistroconPromedioBajo = false;

                ViewBag.MostrarError = "";
                var RegistroValidoConducta = true;
                var RegistroValidoCalificacion = true;
                var info_parcial = bus_parcial.GetInfo(IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, registro_editar.IdCatalogoParcial);
                var actualizar = true;
            
                if ((info_parcial.Orden - 1) > 0)
                {
                    var Promedio_CatalogoParcial = (decimal?)null;
                    var OrdenAnterior = info_parcial.Orden - 1;
                    var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(OrdenAnterior));
                    //var info_cal_anteriores = bus_calificacion_parcial.GetInfo(info_matricula.IdEmpresa, info_matricula.IdMatricula, info_parcial_anterior.IdCatalogoParcial, registro_editar.IdMateria, Convert.ToDecimal(registro_editar.IdProfesor));
                    var info_cal_anteriores = bus_calificacion.GetInfo_X_Matricula(info_matricula.IdEmpresa, info_matricula.IdMatricula, registro_editar.IdMateria);

                    //if (info_cal_anteriores.Calificacion1==null  || info_cal_anteriores.Calificacion2==null || info_cal_anteriores.Calificacion3==null || info_cal_anteriores.Calificacion4==null  || info_cal_anteriores.Evaluacion == null)
                    //{
                    //    ViewBag.MostrarError = "El estudiante tiene calificaciones pendientes de ingresar en el parcial anterior.";
                    //    actualizar = false;
                    //}

                    if (info_cal_anteriores!=null)
                    {
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP1;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP2;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP3;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI1))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.ExamenQ1;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP4;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP5;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP6;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI2))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.ExamenQ2;
                        }

                        if (Promedio_CatalogoParcial==null)
                        {
                            ViewBag.MostrarError = "El estudiante tiene calificaciones pendientes de ingresar en el parcial anterior.";
                            actualizar = false;
                        }
                    }
                }

                if (actualizar==true)
                {
                    if (registro_editar.IdProfesor>0)
                    {
                        if (info_conducta != null)
                        {
                            if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info_det.MotivoConducta))
                            {
                                RegistroValidoConducta = false;
                                ViewBag.MostrarError = "Debe de ingresar el motivo por el cual el estudiante tiene baja conducta.";
                            }
                            else if (info_conducta.IngresaMotivo == true)
                            {
                                info_det.RegistroconPromedioBajo = true;
                            }
                        }
                        else
                        {
                            RegistroValidoConducta = false;
                            ViewBag.MostrarError = "Debe de ingresar la conducta del estudiante.";
                        }

                        if (info_det.Calificacion1 !=null && info_det.Calificacion2 != null && info_det.Calificacion3 != null && info_det.Calificacion4 != null && info_det.Evaluacion != null)
                        {
                            if (info_det.PromedioParcial > 0 && info_det.PromedioParcial < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                            {
                                info_det.RegistroconPromedioBajo = true;

                                if (string.IsNullOrEmpty(info_det.MotivoCalificacion) || string.IsNullOrEmpty(info_det.AccionRemedial))
                                {
                                    RegistroValidoCalificacion = false;
                                    ViewBag.MostrarError = "El promedio parcial del estudiante es menor al promedio establecido, por favor ingresar el motivo y la accion remedial";
                                }
                            }
                            else if (info_det.PromedioParcial > 0 && info_det.PromedioParcial > Convert.ToDecimal(info_anio_lectivo.CalificacionMaxima))
                            {
                                RegistroValidoCalificacion = false;
                                ViewBag.MostrarError = "El promedio parcial del estudiante es mayor al promedio establecido.";
                            }
                            else
                            {
                                info_det.RegistroconPromedioBajo = false;
                            }
                        }
                        else
                        {

                        }

                        //if (info_det.PromedioParcial!=null)
                        //{
                        //    var info_equivalencia_promedio = bus_equivalencia_promedio.GetInfo_x_Promedio(IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(info_det.PromedioParcial));
                        //    info_det.IdEquivalenciaPromedioParcial = info_equivalencia_promedio.IdEquivalenciaPromedio;
                        //}
                    }
                    else
                    {
                        ViewBag.MostrarError = "El registro no tiene asignado profesor.";
                    }

                    if (RegistroValidoCalificacion==true && RegistroValidoConducta==true)
                    {
                        info_det.RegistroValido = true;
                        var info_equivalencia_promedio = bus_equivalencia_promedio.GetInfo_x_Promedio(IdEmpresa, info_matricula.IdAnio, info_det.PromedioParcial);
                        info_det.IdEquivalenciaPromedioParcial = (info_equivalencia_promedio==null ? (int?)null : info_equivalencia_promedio.IdEquivalenciaPromedio);

                        Lista_CalificacionParcial.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    }
                    else
                    {
                        info_det.RegistroValido = false;
                    }
                }
            }

            List<aca_MatriculaCalificacionParcial_Info> model = new List<aca_MatriculaCalificacionParcial_Info>();
            model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCalificacionParcial", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return PartialView("_GridViewPartial_MatriculaCalificacionParcial", model);
        }
        #endregion

        #region Json
        public JsonResult cargar_calificaciones(int IdEmpresa=0, int IdSede = 0, int IdAnio=0, int IdNivel=0, int IdJornada=0, int IdCurso=0, int IdParalelo = 0, int IdMateria=0, int IdCatalogoParcial = 0)
        {
            List<aca_MatriculaCalificacionParcial_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            ViewBag.EsSuperAdmin = EsSuperAdmin;
            if (Convert.ToBoolean(SessionFixed.EsSuperAdmin)==true)
            {
                ListaCalificaciones = bus_calificacion_parcial.GetList_SuperAdmin(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial);
            }
            else{
                ListaCalificaciones = bus_calificacion_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            }

            foreach (var item in ListaCalificaciones)
            {
                //var calificacion_promediada = bus_calificacion.GetInfo(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, item.IdAlumno);
                item.IdCatalogoParcial = IdCatalogoParcial;
                item.IdMateria = IdMateria;

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1)) 
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP1);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP2);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP3);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP4);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP5);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP6);
                
                var info_conducta = bus_conducta.GetInfo(IdEmpresa, IdAnio, Convert.ToInt32(item.Conducta));

                if (item.PromedioParcial> 0 && item.PromedioParcial < Convert.ToDecimal(info_anio.PromedioMinimoParcial))
                {
                    item.RegistroconPromedioBajo = true;
                }
                else if (info_conducta!=null && info_conducta.IngresaMotivo==true)
                {
                    item.RegistroconPromedioBajo = true;
                }
            }

            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(EsSuperAdmin, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularPromedio(int IdEmpresa = 0, decimal IdMatricula = 0, decimal? Calificacion1=null, decimal? Calificacion2=null, decimal? Calificacion3=null, decimal? Calificacion4=null, decimal? Evaluacion=null, decimal? Remedial1=null, decimal? Remedial2=null)
        {
            var resultado = (decimal?)null;
            var info_matricula = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);

            if (info_anio_lectivo != null)
            {
                if (Calificacion1!=null && Calificacion2 != null && Calificacion3 != null && Calificacion4!=null && Evaluacion!=null)
                {
                    decimal suma_calificaciones = Convert.ToDecimal(Calificacion1) + Convert.ToDecimal(Calificacion2) + Convert.ToDecimal(Calificacion3) + Convert.ToDecimal(Calificacion4) + Convert.ToDecimal(Evaluacion);
                    decimal promedio = suma_calificaciones / 5;
                    resultado = (decimal)Math.Round(promedio, 2, MidpointRounding.AwayFromZero);

                    if (Remedial1 != null)
                    {
                        suma_calificaciones = suma_calificaciones + Convert.ToDecimal(Remedial1);
                        resultado = (decimal)Math.Round((suma_calificaciones / 6), 2, MidpointRounding.AwayFromZero);
                    }

                    if (Remedial2 != null)
                    {
                        suma_calificaciones = suma_calificaciones + Convert.ToDecimal(Remedial1) + Convert.ToDecimal(Remedial2);
                        resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
  
            return Json(new { promedio= resultado}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarPromedio(int IdEmpresa = 0, decimal IdMatricula = 0, decimal? Calificacion1 = 0, decimal? Calificacion2 = 0, decimal? Calificacion3 = 0, decimal? Calificacion4 = 0, decimal? Evaluacion = 0, decimal? Remedial1 = null, decimal? Remedial2 = null, int Conducta=0, string MotivoCalificacion="", string AccionRemedial = "", string MotivoConducta = "" )
        {
            decimal resultado = 0;
            var mensaje = "";
            var info_matricula = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Conducta);

            if (Calificacion1>0 && Calificacion2>0 && Calificacion3>0 && Calificacion4>0 && Evaluacion>0)
            {
                if (info_anio_lectivo != null)
                {
                    if (Calificacion1 != null && Calificacion2 != null && Calificacion3 != null && Calificacion4 != null && Evaluacion != null)
                    {
                        decimal suma_calificaciones = Convert.ToDecimal(Calificacion1) + Convert.ToDecimal(Calificacion2) + Convert.ToDecimal(Calificacion3) + Convert.ToDecimal(Calificacion4) + Convert.ToDecimal(Evaluacion);
                        decimal promedio = suma_calificaciones / 5;
                        resultado = (decimal)Math.Round(promedio, 2, MidpointRounding.AwayFromZero);

                        if (Remedial1 != null)
                        {
                            suma_calificaciones = suma_calificaciones + Convert.ToDecimal(Remedial1);
                            resultado = (decimal)Math.Round((suma_calificaciones / 6), 2, MidpointRounding.AwayFromZero);
                        }

                        if (Remedial2 != null)
                        {
                            suma_calificaciones = suma_calificaciones + Convert.ToDecimal(Remedial1) + Convert.ToDecimal(Remedial2);
                            resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
                        }
                    }

                    if (resultado < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                    {
                        if (string.IsNullOrEmpty(MotivoCalificacion) || string.IsNullOrEmpty(AccionRemedial))
                        {
                            mensaje = "Promedio por debajo del mínimo aceptado, debe ingresar motivo y accion remedial de la calificación.";
                        }
                    }

                    if (info_conducta != null)
                    {
                        if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(MotivoConducta))
                        {
                            mensaje += " Debe de ingresar el motivo por el cual el estudiante tiene esa conducta.";
                        }
                    }
                }
            }

            return Json(new { promedio = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarConducta(int IdEmpresa = 0, decimal IdMatricula = 0, int Conducta = 0, string MotivoConducta = "")
        {
            var resultado = "";
            var info_matricula = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Conducta);

            if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(MotivoConducta))
            {
                resultado = "Debe de ingresar el motivo por el cual el estudiante tiene esa conducta";
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarParciales_X_Quimestre(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdCatalogoTipo = 0)
        {
            var resultado = bus_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdCatalogoTipo, DateTime.Now.Date);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LimpiarLista(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_MatriculaCalificacionParcial_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);

            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(EsSuperAdmin, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_Parcial.UploadValidationSettings, UploadControlSettings_Parcial.FileUploadComplete);
            return null;
        }
        public ActionResult Importar( int IdEmpresa = 0, int IdSede=0, int IdAnio=0, int IdNivel=0, int IdJornada=0, int IdCurso=0, int IdParalelo=0, int IdMateria=0, int IdCatalogoTipo = 0, int IdCatalogoParcial=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MatriculaCalificacionParcial_Info model = new aca_MatriculaCalificacionParcial_Info
            {
                IdEmpresa = IdEmpresa,
                IdSede = IdSede,
                IdAnio = IdAnio,
                IdNivel = IdNivel,
                IdJornada = IdJornada,
                IdCurso = IdCurso,
                IdParalelo = IdParalelo,
                IdMateria = IdMateria,
                IdCatalogoTipo = IdCatalogoTipo,
                IdCatalogoParcial = IdCatalogoParcial,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, model.IdAnio, model.IdSede, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<aca_MatriculaCalificacionParcial_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            ListaCalificaciones = bus_calificacion_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            foreach (var item in ListaCalificaciones)
            {
                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP1);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP2);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP3);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP4);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP5);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    item.PromedioParcial = Convert.ToDecimal(item.CalificacionP6);

                var info_conducta = bus_conducta.GetInfo(IdEmpresa, IdAnio, Convert.ToInt32(item.Conducta));
                if (item.PromedioParcial > 0 && item.PromedioParcial < Convert.ToDecimal(info_anio.PromedioMinimoParcial))
                {
                    item.RegistroconPromedioBajo = true;
                }
                else if (info_conducta != null && info_conducta.IngresaMotivo == true)
                {
                    item.RegistroconPromedioBajo = true;
                }
            }      

            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(aca_MatriculaCalificacionParcial_Info model)
        {
            try
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                string IdUsuario = SessionFixed.IdUsuario;
                bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
                var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
                var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
                var Lista_Calificaciones = Lista_CalificacionParcial.get_list(model.IdTransaccionSession);
                var Lista_CalificacionesGuardar = new List<aca_MatriculaCalificacionParcial_Info>();
                var info_parcial = bus_parcial.GetInfo(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdCatalogoParcial);
                
                bool guardar = true;
                ViewBag.mensaje = null;
                ViewBag.MensajeSuccess = null;

                Lista_Calificaciones.ForEach(q => {
                    q.IdAnio = model.IdAnio; q.IdSede = model.IdSede; q.IdNivel = model.IdNivel; q.IdJornada = model.IdJornada; q.IdCurso = model.IdCurso;
                    q.IdParalelo = model.IdParalelo; q.IdMateria = model.IdMateria; q.IdCatalogoParcial = model.IdCatalogoParcial; q.IdProfesor = IdProfesor;
                });

                Lista_CalificacionesGuardar = Lista_Calificaciones.Where(q=>q.RegistroValido==true).ToList();
                var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, model.IdAnio);
                if (info_anio_lectivo.PromedioMinimoParcial!=null)
                {
                    foreach (var item in Lista_CalificacionesGuardar)
                    {
                        if (item.IdProfesor == 0)
                        {
                            ViewBag.mensaje = "El usuario no tiene asignado código de profesor. ";
                            guardar = false;
                            break;
                        }

                        if ((info_parcial.Orden - 1) != 0)
                        {
                            var OrdenAnterior = info_parcial.Orden - 1;
                            var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(item.IdEmpresa, item.IdSede, item.IdAnio, Convert.ToInt32(OrdenAnterior));
                            if (info_parcial_anterior!=null)
                            {
                                if (info_parcial_anterior.EsExamen ==true)
                                {
                                    if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI1))
                                    {
                                        var info_cal_anteriores = bus_calificacion.GetInfo_X_Matricula(item.IdEmpresa, item.IdMatricula, item.IdMateria);

                                        if (info_cal_anteriores.ExamenQ1 == null)
                                        {
                                            ViewBag.mensaje = "El estudiante con IdMatricula " + item.IdMatricula + " tiene calificaciones pendientes de ingresar en el parcial anterior.";
                                            guardar = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    var info_cal_anteriores = bus_calificacion_parcial.GetInfo(item.IdEmpresa, item.IdMatricula, info_parcial_anterior.IdCatalogoParcial, item.IdMateria, Convert.ToDecimal(item.IdProfesor));

                                    if (info_cal_anteriores.Calificacion1 == null || info_cal_anteriores.Calificacion2 == null || info_cal_anteriores.Calificacion3 == null || info_cal_anteriores.Calificacion4 == null || info_cal_anteriores.Evaluacion == null)
                                    {
                                        ViewBag.mensaje = "El estudiante con IdMatricula " + item.IdMatricula + " tiene calificaciones pendientes de ingresar en el parcial anterior.";
                                        guardar = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (item.RegistroValido == false)
                        {
                            ViewBag.mensaje += "Existen registros con promedio de calificación o conducta por debajo del mínimo aceptado, debe ingresar motivo y acción remedial de la calificación.";
                            guardar = false;
                            break;
                        }
                    }
                }
                else
                {
                    guardar = false;
                    ViewBag.mensaje = "No se ha establecido el promedio mínimo aceptado para este año lectivo";
                }

                if (guardar == true)
                {
                    ViewBag.mensaje = null;
                    
                    foreach (var item in Lista_CalificacionesGuardar)
                    {
                        if (!bus_calificacion_parcial.ModicarDB(item))
                        {
                            ViewBag.mensaje = "Error al importar el archivo";
                            cargar_combos(model);
                            return View(model);
                        }
                    }
                    ViewBag.MensajeSuccess = MensajeSuccess;
                }
                cargar_combos(model);
                return View(model);
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                cargar_combos(model);
                return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel=model.IdNivel, IdJornada=model.IdJornada, IdCurso=model.IdCurso, IdParalelo=model.IdParalelo, IdMateria=model.IdMateria, IdCatalogoTipo = model.IdCatalogoTipo, IdCatalogoParcial = model.IdCatalogoParcial });
            }
        }

        public ActionResult GridViewPartial_MatriculaCalificacionParcialImportacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaCalificacionParcialImportacion", model);
        }
        #endregion
    }

    public class aca_MatriculaCalificacionParcial_List
    {
        aca_MatriculaCalificacionParcial_Bus bus_parcial = new aca_MatriculaCalificacionParcial_Bus();
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_AnioLectivoCalificacionHistorico_Bus bus_historico_calificacion = new aca_AnioLectivoCalificacionHistorico_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();

        string Variable = "aca_MatriculaCalificacionParcial_Info";
        public List<aca_MatriculaCalificacionParcial_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCalificacionParcial_Info> list = new List<aca_MatriculaCalificacionParcial_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCalificacionParcial_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCalificacionParcial_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaCalificacionParcial_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);

            aca_MatriculaCalificacionParcial_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpresa==IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();

            if (edited_info.IdProfesor>0)
            {
                edited_info.Calificacion1 = info_det.Calificacion1;
                edited_info.Calificacion2 = info_det.Calificacion2;
                edited_info.Calificacion3 = info_det.Calificacion3;
                edited_info.Calificacion4 = info_det.Calificacion4;
                edited_info.Evaluacion = info_det.Evaluacion;
                edited_info.Remedial1 = info_det.Remedial1;
                edited_info.Remedial2 = info_det.Remedial2;
                edited_info.Conducta = info_det.Conducta;
                edited_info.IdEquivalenciaPromedioParcial = info_det.IdEquivalenciaPromedioParcial;
                edited_info.PromedioParcial = info_det.PromedioParcial;
                edited_info.MotivoCalificacion = info_det.MotivoCalificacion;
                edited_info.MotivoConducta = info_det.MotivoConducta;
                edited_info.AccionRemedial = info_det.AccionRemedial;
                edited_info.IdProfesor = IdProfesor;
                edited_info.IdUsuarioModificacion = SessionFixed.IdUsuario;
                edited_info.RegistroValido = info_det.RegistroValido;
                edited_info.RegistroconPromedioBajo = info_det.RegistroconPromedioBajo;

                bus_parcial.ModicarDB(edited_info);
            }
            
        }
    }

    public class aca_MatriculaCalificacion_Combos_List
    {
        string Variable = "aca_MatriculaCalificacion_Combos_Info";
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
    }

    public class UploadControlSettings_Parcial
    {
        public static aca_MatriculaCalificacionParcial_Info validar_calificaciones(aca_MatriculaCalificacionParcial_Info info)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
            aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
            aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Bus();
            aca_MatriculaCalificacion_Bus bus_calificacion= new aca_MatriculaCalificacion_Bus();
            aca_MatriculaCalificacionParcial_Bus bus_calificacion_parcial = new aca_MatriculaCalificacionParcial_Bus();
            var RegistroValidoConducta = true;
            var RegistroValidoCalificacion = true;
            var RegistroconPromedioBajo = false;
            var RegistroconConductaBaja = false;
            var mensaje = string.Empty;
            var actualizar = true;
            var info_matricula = bus_matricula.GetInfo(info.IdEmpresa, info.IdMatricula);

            #region CalcularPromedio
            decimal? resultado =null;
            if (info.Calificacion1 != null && info.Calificacion2 != null && info.Calificacion3 != null && info.Calificacion4 != null && info.Evaluacion != null)
            {
                decimal suma_calificaciones = Convert.ToDecimal(info.Calificacion1) + Convert.ToDecimal(info.Calificacion2) + Convert.ToDecimal(info.Calificacion3) + Convert.ToDecimal(info.Calificacion4) + Convert.ToDecimal(info.Evaluacion);
                decimal promedio = suma_calificaciones / 5;
                resultado = (decimal)Math.Round(promedio, 2, MidpointRounding.AwayFromZero);

                if (info.Remedial1 != null)
                {
                    suma_calificaciones = suma_calificaciones + Convert.ToDecimal(info.Remedial1);
                    resultado = (decimal)Math.Round((suma_calificaciones / 6), 2, MidpointRounding.AwayFromZero);
                }

                if (info.Remedial2 != null)
                {
                    suma_calificaciones = suma_calificaciones + Convert.ToDecimal(info.Remedial1) + Convert.ToDecimal(info.Remedial2);
                    resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
                }
            }
            info.PromedioParcial = resultado;
            if (resultado != null)
            {
                var info_equivalencia = bus_equivalencia.GetInfo_x_Promedio(info_matricula.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(info.PromedioParcial));
                info.IdEquivalenciaPromedioParcial = (info_equivalencia == null ? (int?)null : info_equivalencia.IdEquivalenciaPromedio);
            }
            #endregion

            var info_parcial = bus_parcial.GetInfo(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, info.IdCatalogoParcial);
            if ((info_parcial.Orden - 1) != 0)
            {
                var OrdenAnterior = info_parcial.Orden - 1;
                var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(OrdenAnterior));
                var info_cal_anteriores = bus_calificacion.GetInfo_X_Matricula(info.IdEmpresa, info.IdMatricula, info.IdMateria);
                decimal? Promedio_CatalogoParcial = null;
                
                if (info_cal_anteriores!=null)
                {
                    if (info_parcial_anterior != null)
                    {
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP1;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP2;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP3;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI1))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.ExamenQ1;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP4;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP5;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.CalificacionP6;
                        }
                        if (info_parcial_anterior.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI2))
                        {
                            Promedio_CatalogoParcial = info_cal_anteriores.ExamenQ2;
                        }

                        if (Promedio_CatalogoParcial==null)
                        {
                            RegistroValidoCalificacion = false;
                        }
                    }
                    else
                    {
                        RegistroValidoCalificacion = false;
                    }
                }
                else
                {
                    RegistroValidoCalificacion = false;
                }
            }

            #region ValidarPromedio
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info.Conducta));
            var lst_conducta = bus_conducta.GetList_IngresaMotivo(IdEmpresa, info_matricula.IdAnio);

            if (info_anio_lectivo.PromedioMinimoParcial != null)
            {
                if (info_conducta != null)
                {
                    if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info.MotivoConducta))
                    {
                        RegistroValidoConducta = false;
                    }
                    else if (info_conducta.IngresaMotivo == true)
                    {
                        RegistroconConductaBaja = true;
                    }
                }
                else
                {
                    RegistroValidoConducta = false;
                    RegistroconConductaBaja = true;
                }

                if (info.PromedioParcial!= null )
                {
                    if (info.PromedioParcial >= 0 && info.PromedioParcial < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                    {
                        RegistroconPromedioBajo = true;

                        if (string.IsNullOrEmpty(info.MotivoCalificacion) || string.IsNullOrEmpty(info.AccionRemedial))
                        {
                            RegistroValidoCalificacion = false;
                        }
                    }
                    else if (info.PromedioParcial > 0 && info.PromedioParcial > Convert.ToDecimal(info_anio_lectivo.CalificacionMaxima))
                    {
                        RegistroValidoCalificacion = false;
                    }
                    else
                    {
                        RegistroconPromedioBajo = false;
                    }
                }
                else
                {
                    RegistroconPromedioBajo = false;
                }

                if (RegistroValidoCalificacion == true && RegistroValidoConducta == true)
                {
                    info.RegistroValido = true;
                }
                else
                {
                    info.RegistroValido = false;
                }

                if (RegistroconConductaBaja == true || RegistroconPromedioBajo == true)
                {
                    info.RegistroconPromedioBajo = true;
                }
                else
                {
                    info.RegistroconPromedioBajo = false;
                }
            }
            else
            {
                info.RegistroValido = false;
                info.RegistroconPromedioBajo = true;
            }

            //var info_parcial = bus_parcial.GetInfo(info.IdEmpresa, info.IdSede, info.IdAnio, info.IdCatalogoParcial);
            //if ((info_parcial.Orden - 1) != 0)
            //{
            //    var OrdenAnterior = info_parcial.Orden - 1;
            //    var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(OrdenAnterior));
            //    var info_cal_anteriores = bus_calificacion_parcial.GetInfo(item.IdEmpresa, item.IdMatricula, info_parcial_anterior.IdCatalogoParcial, item.IdMateria, Convert.ToDecimal(item.IdProfesor));

            //    if (info_cal_anteriores.Calificacion1 == null || info_cal_anteriores.Calificacion2 == null || info_cal_anteriores.Calificacion3 == null || info_cal_anteriores.Calificacion4 == null || info_cal_anteriores.Evaluacion == null)
            //    {
            //        guardar = false;
            //    }
            //}

            return info;
        }
        #endregion

        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            aca_MatriculaCalificacionParcial_List Lista_CalificacionParcial = new aca_MatriculaCalificacionParcial_List();
            List<aca_MatriculaCalificacionParcial_Info> Lista_Calificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Bus();
            aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
            aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                while (reader.Read())
                {
                    if (!reader.IsDBNull(1) && cont > 0)
                    {
                        var IdMatricula = (Convert.ToInt32(reader.GetValue(1)));
                        var pe_nombreCompleto = (Convert.ToString(reader.GetValue(2)).Trim());
                        decimal? Calificacion1 = reader.GetValue(3)==null ? (decimal?)null : Convert.ToDecimal(reader.GetValue(3));
                        decimal? Calificacion2 = reader.GetValue(4) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(4)));
                        decimal? Calificacion3 = reader.GetValue(5) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(5)));
                        decimal? Calificacion4 = reader.GetValue(6) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(6)));
                        decimal? Evaluacion = reader.GetValue(7) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(7)));
                        decimal? Remedial1 = reader.GetValue(8) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(8)));
                        decimal? Remedial2 = reader.GetValue(9) == null ? (decimal?)null : (Convert.ToDecimal(reader.GetValue(9)));
                        var Conducta = (Convert.ToString(reader.GetValue(10)).Trim());
                        var MotivoCalificacion = (Convert.ToString(reader.GetValue(11)).Trim());
                        var AccionRemedial = (Convert.ToString(reader.GetValue(12)).Trim());
                        var MotivoConducta = (Convert.ToString(reader.GetValue(13)).Trim());
                        var IdCatalogoParcial = Convert.ToInt32(reader.GetValue(15));
                        var IdMateria = Convert.ToInt32(reader.GetValue(16));
                        string IdUsuario = SessionFixed.IdUsuario;
                        bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
                        var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
                        var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
                        var info_matricula = bus_matricula.GetInfo(IdEmpresa,IdMatricula);
                        var info_anio = bus_anio.GetInfo(Convert.ToInt32(SessionFixed.IdEmpresa), info_matricula.IdAnio);

                        var info_conducta = bus_conducta.GetInfo_x_Letra(IdEmpresa, info_anio.IdAnio, Conducta);
                        aca_MatriculaCalificacionParcial_Info info = new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdMatricula = IdMatricula,
                            pe_nombreCompleto = pe_nombreCompleto,
                            Calificacion1 = Calificacion1,
                            Calificacion2 = Calificacion2,
                            Calificacion3 = Calificacion3,
                            Calificacion4 = Calificacion4,
                            Evaluacion = Evaluacion,
                            Remedial1 = Remedial1,
                            Remedial2 = Remedial2,
                            Conducta = (info_conducta == null ? (int?)null : info_conducta.Secuencia),
                            MotivoCalificacion = MotivoCalificacion,
                            MotivoConducta = MotivoConducta,
                            AccionRemedial = AccionRemedial,
                            IdUsuarioModificacion = SessionFixed.IdUsuario,
                            FechaModificacion = DateTime.Now,
                            RegistroValido=true,
                            RegistroconPromedioBajo = false,
                            IdCatalogoParcial = IdCatalogoParcial,
                            IdMateria = IdMateria,
                            IdProfesor=IdProfesor
                    };

                        var info_valida = validar_calificaciones(info);
                        Lista_Calificaciones.Add(info_valida);
                    }
                    else
                        cont++;
                }

                Lista_CalificacionParcial.set_list(Lista_Calificaciones, IdTransaccionSession);
            }
        }
    }
}