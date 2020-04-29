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
                IdCatalogoParcial = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1),
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
        public ActionResult GridViewPartial_MatriculaCalificacionParcial()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaCalificacionParcial_Info> model = Lista_CalificacionParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_MatriculaCalificacionParcial", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_MatriculaCalificacionParcial_Info model)
        {
            var lst_parcial = new List<aca_AnioLectivoParcial_Info>();
            var lst_quim1 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1), DateTime.Now.Date);
            var lst_quim2 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2), DateTime.Now.Date);
            lst_parcial.AddRange(lst_quim1);
            lst_parcial.AddRange(lst_quim2);

            ViewBag.lst_parcial = lst_parcial;
        }
        
        private void cargar_combos_detalle()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa,0);
            int IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);

            var lst_conducta = bus_conducta.GetList(IdEmpresa, IdAnio, false);
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

        public List<aca_Jornada_Info> CargarJornada(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel=0)
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
                            }).OrderBy(q=>q.OrdenJornada).ToList();

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

        public List<aca_Curso_Info> CargarCurso(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada=0)
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

        public List<aca_Paralelo_Info> CargarParalelo(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso=0)
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

        public List<aca_Materia_Info> CargarMateria(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo=0)
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
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_MatriculaCalificacionParcial_Info info_det)
        {

            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
                var info_conducta = bus_conducta.GetInfo(IdEmpresa,info_matricula.IdAnio, Convert.ToInt32(info_det.Conducta));
                var info_anio = bus_anio.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAnio);

                if (info_conducta!=null)
                {
                    if (info_det.PromedioParcial >= Convert.ToDecimal(info_anio.PromedioMinimoParcial) && (info_conducta.IngresaMotivo == false || (info_conducta.IngresaMotivo == true && !string.IsNullOrEmpty(info_det.MotivoConducta)) ) )
                    {
                        Lista_CalificacionParcial.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(info_det.MotivoCalificacion) && !string.IsNullOrEmpty(info_det.AccionRemedial))
                        {
                            Lista_CalificacionParcial.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                        }
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

            ListaCalificaciones = bus_calificacion_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            foreach (var item in ListaCalificaciones)
            {
                var calificacion_promediada = bus_calificacion.GetInfo(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, item.IdAlumno);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1)) 
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP1);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP2);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP3);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP4);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP5);

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    item.PromedioParcial = Convert.ToDecimal(calificacion_promediada == null ? 0 : calificacion_promediada.CalificacionP6);

                item.PromedioParcial = (decimal)Math.Round(item.PromedioParcial, 2, MidpointRounding.AwayFromZero);
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

            return Json(ListaCalificaciones, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularPromedio(int IdEmpresa = 0, decimal IdMatricula = 0, decimal Calificacion1 = 0, decimal Calificacion2 = 0, decimal Calificacion3 = 0, decimal Calificacion4 = 0, decimal Evaluacion = 0, decimal Remedial1 = 0, decimal Remedial2 = 0)
        {
            decimal resultado = 0;
            var info_matricula = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);

            if (info_anio_lectivo != null)
            {
                decimal suma_calificaciones = (Calificacion1+Calificacion2+Calificacion3+Calificacion4+Evaluacion);
                decimal promedio = (Calificacion1 + Calificacion2 + Calificacion3 + Calificacion4 + Evaluacion) / 5;
                resultado = promedio;

                if (Remedial1!=0)
                {
                    suma_calificaciones = suma_calificaciones + Remedial1;
                    resultado = (decimal)Math.Round((suma_calificaciones /6), 2, MidpointRounding.AwayFromZero);
                }
                else if (Remedial2!=0)
                {
                    suma_calificaciones = suma_calificaciones + Remedial1 + Remedial2;
                    resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    resultado = promedio;
                }

                resultado= (decimal)Math.Round(resultado, 2, MidpointRounding.AwayFromZero);
            }
  
            return Json(new { promedio= resultado}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarPromedio(int IdEmpresa = 0, decimal IdMatricula = 0, decimal Calificacion1 = 0, decimal Calificacion2 = 0, decimal Calificacion3 = 0, decimal Calificacion4 = 0, decimal Evaluacion = 0, decimal Remedial1 = 0, decimal Remedial2 = 0, int Conducta=0, string MotivoCalificacion="", string AccionRemedial = "", string MotivoConducta = "" )
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
                    decimal suma_calificaciones = (Calificacion1 + Calificacion2 + Calificacion3 + Calificacion4 + Evaluacion);
                    decimal promedio = (Calificacion1 + Calificacion2 + Calificacion3 + Calificacion4 + Evaluacion) / 5;
                    resultado = promedio;

                    if (Remedial1 != 0)
                    {
                        suma_calificaciones = suma_calificaciones + Remedial1;
                        resultado = (decimal)Math.Round((suma_calificaciones / 6), 2, MidpointRounding.AwayFromZero);
                    }
                    else if (Remedial2 != 0)
                    {
                        suma_calificaciones = suma_calificaciones + Remedial1 + Remedial2;
                        resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        resultado = promedio;
                    }

                    resultado = (decimal)Math.Round(resultado, 2, MidpointRounding.AwayFromZero);

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
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_Parcial.UploadValidationSettings, UploadControlSettings_Parcial.FileUploadComplete);
            return null;
        }
        public ActionResult Importar( int IdEmpresa = 0, int IdSede=0, int IdAnio=0, int IdNivel=0, int IdJornada=0, int IdCurso=0, int IdParalelo=0, int IdMateria=0, int IdCatalogoParcial=0)
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
                IdCatalogoParcial = IdCatalogoParcial,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            ListaCombos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<aca_MatriculaCalificacionParcial_Info> ListaCalificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            ListaCalificaciones = bus_calificacion_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            foreach (var item in ListaCalificaciones)
            {
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
                var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                string IdUsuario = SessionFixed.IdUsuario;
                bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
                var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
                var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
                var Lista_Calificaciones = Lista_CalificacionParcial.get_list(model.IdTransaccionSession);
                bool guardar = true;
                ViewBag.mensaje = null;
                ViewBag.MensajeSuccess = null;

                foreach (var item in Lista_Calificaciones)
                {
                    item.IdAnio = model.IdAnio;
                    item.IdSede = model.IdSede;
                    item.IdNivel = model.IdNivel;
                    item.IdJornada = model.IdJornada;
                    item.IdCurso = model.IdCurso;
                    item.IdParalelo = model.IdParalelo;
                    item.IdMateria = model.IdMateria;
                    item.IdCatalogoParcial = model.IdCatalogoParcial;
                    item.IdProfesor = IdProfesor;               
                }

                foreach (var item in Lista_Calificaciones)
                {
                    if (item.IdProfesor==0 || item.RegistroValido==false)
                    {
                        if (item.IdProfesor == 0)
                        {
                            ViewBag.mensaje = "El usuario no tiene asignado código de profesor. ";
                        }

                        if (item.RegistroValido == false)
                        {
                            ViewBag.mensaje += "Existen registros con promedio de calificación o conducta por debajo del mínimo aceptado, debe ingresar motivo y acción remedial de la calificación.";
                        }
                        
                        guardar = false;
                        break;
                    }
                }

                if (guardar == true)
                {
                    ViewBag.mensaje = null;
                    
                    foreach (var item in Lista_Calificaciones)
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
                return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel=model.IdNivel, IdJornada=model.IdJornada, IdCurso=model.IdCurso, IdParalelo=model.IdParalelo, IdMateria=model.IdMateria, IdCatalogoParcial=model.IdCatalogoParcial });
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

            var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info_det.Conducta));
            var lst_conducta = bus_conducta.GetList_IngresaMotivo(IdEmpresa, info_matricula.IdAnio);

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
                edited_info.PromedioParcial = info_det.PromedioParcial;
                edited_info.MotivoCalificacion = info_det.MotivoCalificacion;
                edited_info.MotivoConducta = info_det.MotivoConducta;
                edited_info.AccionRemedial = info_det.AccionRemedial;
                edited_info.IdProfesor = IdProfesor;
                edited_info.IdUsuarioModificacion = SessionFixed.IdUsuario;

                if (info_anio_lectivo != null)
                {
                    if (edited_info.PromedioParcial > 0 && edited_info.PromedioParcial < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                    {
                        edited_info.RegistroconPromedioBajo = true;
                    }

                    if (edited_info.PromedioParcial > 0 && edited_info.PromedioParcial < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                    {
                        if (string.IsNullOrEmpty(edited_info.MotivoCalificacion) || string.IsNullOrEmpty(edited_info.AccionRemedial))
                        {
                            edited_info.RegistroValido = false;
                        }
                    }

                    if (info_conducta != null)
                    {
                        if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info_det.MotivoConducta))
                        {
                            edited_info.RegistroValido = false;
                        }
                        else if (info_conducta.IngresaMotivo==true)
                        {
                            edited_info.RegistroconPromedioBajo = true;
                        }
                    }
                }
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
            var mensaje = string.Empty;

            #region CalcularPromedio
            decimal resultado = 0;
            decimal suma_calificaciones = Convert.ToDecimal(info.Calificacion1 + info.Calificacion2 + info.Calificacion3 + info.Calificacion4 + info.Evaluacion);
            decimal promedio = Convert.ToDecimal((info.Calificacion1 + info.Calificacion2 + info.Calificacion3 + info.Calificacion4 + info.Evaluacion) / 5);
            resultado = promedio;

            if (info.Remedial1 != 0)
            {
                suma_calificaciones = Convert.ToDecimal(suma_calificaciones + info.Remedial1);
                resultado = (decimal)Math.Round((suma_calificaciones / 6), 2, MidpointRounding.AwayFromZero);
            }
            else if (info.Remedial2 != 0)
            {
                suma_calificaciones = Convert.ToDecimal(suma_calificaciones + info.Remedial1 + info.Remedial2);
                resultado = (decimal)Math.Round((suma_calificaciones / 7), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                resultado = promedio;
            }

            info.PromedioParcial = resultado;
            #endregion

            #region ValidarPromedio
            var info_matricula = bus_matricula.GetInfo(IdEmpresa, info.IdMatricula);
            var info_anio_lectivo = bus_anio.GetInfo(IdEmpresa, info_matricula.IdAnio);
            var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info.Conducta));
            var lst_conducta = bus_conducta.GetList_IngresaMotivo(IdEmpresa, info_matricula.IdAnio);

            //if (info.Calificacion1 > 0 && info.Calificacion2 > 0 && info.Calificacion3 > 0 && info.Calificacion4 > 0 && info.Evaluacion > 0)
            //{
                if (info_anio_lectivo != null)
                {
                    if (resultado >0 && resultado < Convert.ToDecimal(info_anio_lectivo.PromedioMinimoParcial))
                    {
                        if (string.IsNullOrEmpty(info.MotivoCalificacion) || string.IsNullOrEmpty(info.AccionRemedial))
                        {
                            //ViewBag.mensaje = "Promedio por debajo del mínimo aceptado, debe ingresar motivo y accion remedial de la calificación.";
                            info.RegistroValido = false;
                        }
                    }

                    if (info_conducta != null)
                    {
                        if (info_conducta.IngresaMotivo == true && string.IsNullOrEmpty(info.MotivoConducta))
                        {
                            //ViewBag.mensaje += " Debe de ingresar el motivo por el cual el estudiante tiene esa conducta.";
                            info.RegistroValido = false;
                        }
                        else if (info_conducta.IngresaMotivo==true)
                        {
                        info.RegistroconPromedioBajo = true;
                        }
                    }
                }
            //}
            #endregion

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
            aca_MatriculaCalificacionParcial_List Lista_CalificacionParcial = new aca_MatriculaCalificacionParcial_List();
            List<aca_MatriculaCalificacionParcial_Info> Lista_Calificaciones = new List<aca_MatriculaCalificacionParcial_Info>();
            aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();

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
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var IdMatricula = (Convert.ToInt32(reader.GetValue(0)));
                        var pe_nombreCompleto = (Convert.ToString(reader.GetValue(1)).Trim());
                        var Calificacion1 = Convert.ToDecimal(reader.GetValue(2));
                        var Calificacion2 = (Convert.ToDecimal(reader.GetValue(3)));
                        var Calificacion3 = (Convert.ToDecimal(reader.GetValue(4)));
                        var Calificacion4 = (Convert.ToDecimal(reader.GetValue(5)));
                        var Evaluacion = (Convert.ToDecimal(reader.GetValue(6)));
                        var Remedial1 = (Convert.ToDecimal(reader.GetValue(7)));
                        var Remedial2 = (Convert.ToDecimal(reader.GetValue(8)));
                        var Conducta = (Convert.ToString(reader.GetValue(9)).Trim());
                        var MotivoCalificacion = (Convert.ToString(reader.GetValue(10)).Trim());
                        var AccionRemedial = (Convert.ToString(reader.GetValue(11)).Trim());
                        var MotivoConducta = (Convert.ToString(reader.GetValue(12)).Trim());

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
                            RegistroconPromedioBajo = false
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