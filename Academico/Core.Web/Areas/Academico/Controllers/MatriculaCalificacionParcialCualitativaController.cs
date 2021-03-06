﻿using Core.Bus.Academico;
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
    public class MatriculaCalificacionParcialCualitativaController : Controller
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
        aca_MatriculaCalificacionCualitativa_Bus bus_calificacion_parcial = new aca_MatriculaCalificacionCualitativa_Bus();
        aca_MatriculaCalificacionParcial_Cualitativa_List Lista_CalificacionParcial = new aca_MatriculaCalificacionParcial_Cualitativa_List();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
        aca_AnioLectivoCalificacionCualitativa_Bus bus_CalificacionCualitativa = new aca_AnioLectivoCalificacionCualitativa_Bus();
        aca_MatriculaCalificacionCualitativa_Combos_List ListaCombos = new aca_MatriculaCalificacionCualitativa_Combos_List();
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
            aca_MatriculaCalificacionCualitativa_Info model = new aca_MatriculaCalificacionCualitativa_Info
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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion_parcial.getList_CombosCualitativa(model.IdEmpresa, model.IdSede, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.EsSuperAdmin = EsSuperAdmin;
            cargar_combos(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacionParcialCualitativa()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacionCualitativa_Info> model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var info = model.FirstOrDefault();
            cargar_combos_detalle(info);

            return PartialView("_GridViewPartial_MatriculaCalificacionParcialCualitativa", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCalificacionParcialCualitativa_SuperAdmin()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacionCualitativa_Info> model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var info = model.FirstOrDefault();

            cargar_combos_detalle(info);

            return PartialView("_GridViewPartial_MatriculaCalificacionParcialCualitativa_SuperAdmin", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_MatriculaCalificacionCualitativa_Info model)
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

        private void cargar_combos_detalle(aca_MatriculaCalificacionCualitativa_Info info)
        {
            //var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            //var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa, 0);
            //int IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            var IdEmpresa = info == null ? 0 : info.IdEmpresa;
            var IdAnio = info == null ? 0 : info.IdAnio;

            var lst_calificacion_cualitativa = bus_CalificacionCualitativa.getList(IdEmpresa, IdAnio, false);
            ViewBag.lst_calificacion_cualitativa = lst_calificacion_cualitativa;

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

        public List<aca_Paralelo_Info> CargarParalelo(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0, int IdCurso = 0)
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

        public List<aca_Materia_Info> CargarMateria(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0, int IdCurso = 0, int IdParalelo = 0)
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
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpress.Web.Mvc.DevExpressEditorsBinder))] aca_MatriculaCalificacionCualitativa_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_MatriculaCalificacionCualitativa_Info registro_editar = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(m => m.IdEmpresa == IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();

            if (ModelState.IsValid)
            {
                
                var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
                var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info_det.Conducta));
                
                info_det.RegistroValido = true;
                ViewBag.MostrarError = "";
                var RegistroValidoConducta = true;
                var RegistroValidoCalificacion = true;
                var info_parcial = bus_parcial.GetInfo(IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, registro_editar.IdCatalogoParcial);

                if ((info_parcial.Orden - 1) > 0)
                {
                    var OrdenAnterior = info_parcial.Orden - 1;
                    var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(OrdenAnterior));
                    var info_cal_anteriores = bus_calificacion_parcial.get_Info(info_matricula.IdEmpresa, info_matricula.IdMatricula, info_parcial_anterior.IdCatalogoParcial, registro_editar.IdMateria, Convert.ToDecimal(registro_editar.IdProfesor));

                    //if (info_cal_anteriores.IdCalificacionCualitativa == null)
                    //{
                    //    ViewBag.MostrarError = "El estudiante tiene calificaciones pendientes de ingresar en el parcial anterior.";
                    //    RegistroValidoCalificacion = false;
                    //}
                }

                if (registro_editar.IdProfesor > 0)
                {
                    if (info_conducta != null)
                    {
                        if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info_det.MotivoConducta))
                        {
                            RegistroValidoConducta = false;
                            ViewBag.MostrarError = "Debe de ingresar el motivo por el cual el estudiante tiene baja conducta.";
                        }
                        else
                        {
                            info_det.RegistroValidoConducta = true;
                        }
                    }
                    else
                    {
                        RegistroValidoConducta = false;
                        ViewBag.MostrarError = "Debe de ingresar la conducta del estudiante.";
                    }
                }
                else
                {
                    ViewBag.MostrarError = "El registro no tiene asignado profesor.";
                }

                if (RegistroValidoCalificacion == true && RegistroValidoConducta == true)
                {
                    info_det.RegistroValido = true;
                    Lista_CalificacionParcial.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
                else
                {
                    info_det.RegistroValido = false;
                }
            }

            List<aca_MatriculaCalificacionCualitativa_Info> model = new List<aca_MatriculaCalificacionCualitativa_Info>();
            model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle(registro_editar);
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCalificacionParcialCualitativa", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return PartialView("_GridViewPartial_MatriculaCalificacionParcialCualitativa", model);
        }
        #endregion

        #region Json
        public JsonResult cargar_calificaciones(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdMateria = 0, int IdCatalogoParcial = 0)
        {
            List<aca_MatriculaCalificacionCualitativa_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionCualitativa_Info>();
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            ViewBag.EsSuperAdmin = EsSuperAdmin;
            if (Convert.ToBoolean(SessionFixed.EsSuperAdmin) == true)
            {
                ListaCalificaciones = bus_calificacion_parcial.GetList_SuperAdmin(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial);
            }
            else
            {
                ListaCalificaciones = bus_calificacion_parcial.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            }

            ListaCalificaciones.ForEach(q=> { q.IdSede = IdSede; q.IdAnio = IdAnio;  });
            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(EsSuperAdmin, JsonRequestBehavior.AllowGet);
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
            var resultado = bus_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdCatalogoTipo, DateTime.Now);
            return Json(resultado, JsonRequestBehavior.AllowGet);
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

        public JsonResult LimpiarLista(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_MatriculaCalificacionCualitativa_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionCualitativa_Info>();
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);

            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(EsSuperAdmin, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_ParcialCualitativa.UploadValidationSettings, UploadControlSettings_ParcialCualitativa.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdMateria = 0, int IdCatalogoTipo = 0, int IdCatalogoParcial = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MatriculaCalificacionCualitativa_Info model = new aca_MatriculaCalificacionCualitativa_Info
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
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion_parcial.getList_CombosCualitativa(model.IdEmpresa,model.IdSede, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<aca_MatriculaCalificacionCualitativa_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionCualitativa_Info>();
            ListaCalificaciones = bus_calificacion_parcial.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            Lista_CalificacionParcial.set_list(ListaCalificaciones, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(aca_MatriculaCalificacionCualitativa_Info model)
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
                var Lista_CalificacionesGuardar = new List<aca_MatriculaCalificacionCualitativa_Info>();

                var info_parcial = bus_parcial.GetInfo(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdCatalogoParcial);

                bool guardar = true;
                ViewBag.mensaje = null;
                ViewBag.MensajeSuccess = null;

                Lista_Calificaciones.ForEach(q => {
                    q.IdAnio = model.IdAnio; q.IdSede = model.IdSede; q.IdNivel = model.IdNivel; q.IdJornada = model.IdJornada; q.IdCurso = model.IdCurso;
                    q.IdParalelo = model.IdParalelo; q.IdMateria = model.IdMateria; q.IdCatalogoParcial = model.IdCatalogoParcial; q.IdProfesor = IdProfesor;
                });
                Lista_CalificacionesGuardar = Lista_Calificaciones.Where(q => q.RegistroValido == true).ToList();

                foreach (var item in Lista_CalificacionesGuardar)
                {
                    //if ((info_parcial.Orden - 1) != 0)
                    //{
                    //    var OrdenAnterior = info_parcial.Orden - 1;
                    //    var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(item.IdEmpresa, item.IdSede, item.IdAnio, Convert.ToInt32(OrdenAnterior));
                    //    var info_cal_anteriores = bus_calificacion_parcial.get_Info(item.IdEmpresa, item.IdMatricula, info_parcial_anterior.IdCatalogoParcial, item.IdMateria, Convert.ToDecimal(item.IdProfesor));

                    //    if (info_cal_anteriores.IdCalificacionCualitativa==null)
                    //    {
                    //        ViewBag.mensaje = "El estudiante con IdMatricula " + item.IdMatricula + " tiene calificaciones pendientes de ingresar en el parcial anterior.";
                    //        guardar = false;
                    //        break;
                    //    }
                    //}

                    if (item.IdProfesor == 0)
                    {
                        ViewBag.mensaje = "El usuario no tiene asignado código de profesor. ";
                        guardar = false;
                        break;
                    }

                    if (item.RegistroValido == false)
                    {
                        ViewBag.mensaje += "Existen registros con promedio de calificación o conducta por debajo del mínimo aceptado, debe ingresar motivo y acción remedial de la calificación.";
                        guardar = false;
                        break;
                    }
                }

                if (guardar == true)
                {
                    ViewBag.mensaje = null;

                    foreach (var item in Lista_CalificacionesGuardar)
                    {
                        if (!bus_calificacion_parcial.modificarDB(item))
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
                return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel = model.IdNivel, IdJornada = model.IdJornada, IdCurso = model.IdCurso, IdParalelo = model.IdParalelo, IdMateria = model.IdMateria, IdCatalogoTipo = model.IdCatalogoTipo, IdCatalogoParcial = model.IdCatalogoParcial });
            }
        }

        public ActionResult GridViewPartial_MatriculaCalificacionParcialCualitativaImportacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var info = model.FirstOrDefault();
            cargar_combos_detalle(info);
            return PartialView("_GridViewPartial_MatriculaCalificacionParcialCualitativaImportacion", model);
        }
        #endregion
    }

    public class aca_MatriculaCalificacionParcial_Cualitativa_List
    {
        aca_MatriculaCalificacionCualitativa_Bus bus_calificacion_parcial = new aca_MatriculaCalificacionCualitativa_Bus();
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_AnioLectivoCalificacionHistorico_Bus bus_historico_calificacion = new aca_AnioLectivoCalificacionHistorico_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        aca_AnioLectivoCalificacionCualitativa_Bus bus_equivalecia = new aca_AnioLectivoCalificacionCualitativa_Bus();

        string Variable = "aca_MatriculaCalificacionParcialCualitativa_Info";
        public List<aca_MatriculaCalificacionCualitativa_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCalificacionCualitativa_Info> list = new List<aca_MatriculaCalificacionCualitativa_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCalificacionCualitativa_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCalificacionCualitativa_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaCalificacionCualitativa_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);

            aca_MatriculaCalificacionCualitativa_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpresa == IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();

            if (edited_info.IdProfesor > 0)
            {
                edited_info.IdCalificacionCualitativa = info_det.IdCalificacionCualitativa;
                var info_equivalencia = bus_equivalecia.getInfo(edited_info.IdEmpresa,edited_info.IdAnio, Convert.ToInt32(edited_info.IdCalificacionCualitativa));
                edited_info.Calificacion = info_equivalencia == null ? null : info_equivalencia.Calificacion;
                edited_info.Conducta = info_det.Conducta;
                edited_info.MotivoConducta = info_det.MotivoConducta;

                bus_calificacion_parcial.modificarDB(edited_info);
            }

        }
    }
    public class aca_MatriculaCalificacionCualitativa_Combos_List
    {
        string Variable = "aca_MatriculaCalificacionCualitativa_Combos_Info";
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

    public class UploadControlSettings_ParcialCualitativa
    {
        public static aca_MatriculaCalificacionCualitativa_Info validar_calificaciones(aca_MatriculaCalificacionCualitativa_Info info)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
            aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
            aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Bus();
            aca_AnioLectivoCalificacionCualitativa_Bus bus_cualitativa = new aca_AnioLectivoCalificacionCualitativa_Bus();
            aca_MatriculaCalificacionCualitativa_Bus bus_calificacion_cualitativa = new aca_MatriculaCalificacionCualitativa_Bus();
            aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
            var mensaje = string.Empty;
            var RegistroValidoConducta = true;
            var RegistroValido = true;
            var info_matricula = bus_matricula.GetInfo(info.IdEmpresa, info.IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info.Conducta));

            var info_parcial = bus_parcial.GetInfo(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, info.IdCatalogoParcial);
            //if ((info_parcial.Orden - 1) != 0)
            //{
            //    var OrdenAnterior = info_parcial.Orden - 1;
            //    var info_parcial_anterior = bus_parcial.GetInfo_x_Orden(info_matricula.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(OrdenAnterior));
            //    var info_cal_anteriores = bus_calificacion_cualitativa.GetInfo_X_Matricula(info.IdEmpresa, info.IdMatricula, info.IdMateria, info_parcial_anterior.IdCatalogoParcial);
            //    decimal? Promedio_CatalogoParcial = null;

            //    if (info_cal_anteriores != null)
            //    {
            //        if (info_parcial_anterior != null)
            //        {
            //            Promedio_CatalogoParcial = info_cal_anteriores.IdCalificacionCualitativa;

            //            if (Promedio_CatalogoParcial == null)
            //            {
            //                RegistroValido = false;
            //            }
            //        }
            //        else
            //        {
            //            RegistroValido = false;
            //        }
            //    }
            //    else
            //    {
            //        RegistroValido = false;
            //    }
            //}

            if (info_conducta != null)
            {
                if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info.MotivoConducta))
                {
                    RegistroValidoConducta = false;
                }
            }
            else
            {
                RegistroValidoConducta = false;
            }

            info.RegistroValidoConducta = RegistroValidoConducta;

            if (info.RegistroValidoConducta == true && RegistroValido==true)
            {
                info.RegistroValido = true;
            }
            else
            {
                info.RegistroValido = false;
            }

            return info;
        }

        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            aca_MatriculaCalificacionParcial_Cualitativa_List Lista_CalificacionParcial = new aca_MatriculaCalificacionParcial_Cualitativa_List();
            List<aca_MatriculaCalificacionCualitativa_Info> Lista_Calificaciones = new List<aca_MatriculaCalificacionCualitativa_Info>();
            aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
            aca_AnioLectivoCalificacionCualitativa_Bus bus_cualitativa = new aca_AnioLectivoCalificacionCualitativa_Bus();
            aca_AnioLectivoEquivalenciaPromedio_Bus bus_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Bus();
            aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
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
                        //var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                        var IdMatricula = (Convert.ToInt32(reader.GetValue(1)));
                        var info_matricula = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
                        var pe_nombreCompleto = (Convert.ToString(reader.GetValue(2)).Trim());
                        string CodigoCalificacionCualitativa = Convert.ToString(reader.GetValue(3));
                        string Conducta = Convert.ToString(reader.GetValue(4));
                        string MotivoConducta = Convert.ToString(reader.GetValue(5));
                        var IdCatalogoParcial = Convert.ToInt32(reader.GetValue(6));
                        var IdMateria = Convert.ToInt32(reader.GetValue(7));
                        string IdUsuario = SessionFixed.IdUsuario;
                        bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
                        var info_profesor = bus_profesor.GetInfo_x_Usuario(IdEmpresa, IdUsuario);
                        var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);

                        var info_cualitativa = bus_cualitativa.getInfo_Codigo(info_matricula.IdEmpresa, info_matricula.IdAnio, CodigoCalificacionCualitativa);
                        var info_conducta = bus_conducta.GetInfo_x_Letra(IdEmpresa, info_anio.IdAnio, Conducta);

                        aca_MatriculaCalificacionCualitativa_Info info = new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdMatricula = IdMatricula,
                            IdSede = (info_matricula==null ? 0 : Convert.ToInt32(info_matricula.IdSede)),
                            IdAnio = (info_matricula == null ? 0 : Convert.ToInt32(info_matricula.IdAnio)),
                            pe_nombreCompleto = pe_nombreCompleto,
                            IdCalificacionCualitativa = (info_cualitativa==null ? (int?)null : Convert.ToInt32(info_cualitativa.IdCalificacionCualitativa)),
                            Calificacion = (info_cualitativa == null ? (decimal?)null : Convert.ToDecimal(info_cualitativa.Calificacion)),
                            Conducta = (info_conducta == null ? (int?)null : info_conducta.Secuencia),
                            MotivoConducta = MotivoConducta,
                            IdCatalogoParcial = IdCatalogoParcial,
                            IdMateria = IdMateria,
                            IdProfesor = IdProfesor
                            //RegistroValidoCalificacion = (info_cualitativa==null ? false : true),
                            //RegistroValido = true,
                            //RegistroValidoConducta = true
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