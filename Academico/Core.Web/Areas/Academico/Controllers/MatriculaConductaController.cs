using Core.Bus.Academico;
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
    public class MatriculaConductaController : Controller
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
        aca_MatriculaConducta_Combos_List Lista_Combos = new aca_MatriculaConducta_Combos_List();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_MatriculaConducta_List ListaConducta = new aca_MatriculaConducta_List();
        aca_MatriculaConducta_Bus bus_conducta_cal = new aca_MatriculaConducta_Bus();
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
            aca_MatriculaConducta_Info model = new aca_MatriculaConducta_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdNivel = 0,
                IdJornada = 0,
                IdCurso = 0,
                IdParalelo = 0,
                IdCatalogoTipo = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaConducta_Info> lst_combos = bus_conducta_cal.GetList_Combos_Inspector(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            Lista_Combos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            //List<aca_MatriculaConducta_Info> lst_datos_combos = bus_conducta_cal.GetList_Combos(model.IdEmpresa, model.IdSede, model.IdAnio,model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo);
            //Lista_Combos.set_list(lst_datos_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaConducta()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_MatriculaConducta_Info> model = ListaConducta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_MatriculaConducta", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_MatriculaConducta_Info model)
        {
            Dictionary<string, string> lst_quimestres = new Dictionary<string, string>();
            lst_quimestres.Add("6", "QUIMESTRE 1");
            lst_quimestres.Add("7", "QUIMESTRE 2");
            ViewBag.lst_quimestres = lst_quimestres;

            var lst_parcial = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdCatalogoTipo, DateTime.Now.Date);
            ViewBag.lst_parcial = lst_parcial;
            //aca_CatalogoTipo_Bus bus_catalogo = new aca_CatalogoTipo_Bus();
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa, 0);
            int IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);

            var lst_conducta = bus_conducta.GetList_Inspector(IdEmpresa, IdAnio);
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
        public ActionResult ComboBoxPartial_Jornada()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult ComboBoxPartial_Nivel()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede, IdJornada = IdJornada });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdJornada = IdJornada, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Paralelo()
        {
            int IdAnio = !string.IsNullOrEmpty(Request.Params["IdAnio"]) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = !string.IsNullOrEmpty(Request.Params["IdSede"]) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdJornada = !string.IsNullOrEmpty(Request.Params["IdJornada"]) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdNivel = !string.IsNullOrEmpty(Request.Params["IdNivel"]) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdCurso = !string.IsNullOrEmpty(Request.Params["IdCurso"]) ? int.Parse(Request.Params["IdCurso"]) : -1;
            return PartialView("_ComboBoxPartial_Paralelo", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdJornada = IdJornada, IdNivel = IdNivel, IdCurso = IdCurso });
        }

        #endregion

        #region FuncionesCombos
        public List<aca_AnioLectivo_Info> CargarAnio(int IdEmpresa = 0)
        {
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.IdEmpresa == IdEmpresa).ToList();
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
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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

        public List<aca_NivelAcademico_Info> CargarNivel(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0)
        {
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
            q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada==IdJornada).ToList();

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
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaConducta_Info> lst_combos = Lista_Combos.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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

        #endregion

        #region Funciones del Grid
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpress.Web.Mvc.DevExpressEditorsBinder))] aca_MatriculaConducta_Info info_det)
        {
            ViewBag.MostrarError = "";
            if (ModelState.IsValid)
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                var info_matricula = bus_matricula.GetInfo(IdEmpresa, info_det.IdMatricula);
                var info_conducta = bus_conducta.GetInfo(IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(info_det.SecuenciaConductaPromedioParcialFinal));

                if (info_conducta != null)
                {
                    if (info_conducta.IngresaMotivo == true && (info_det.SecuenciaConductaPromedioParcialFinal == 0 || string.IsNullOrEmpty(info_det.MotivoPromedioParcialFinal)))
                    {
                        ViewBag.MostrarError = "Debe de ingresar el promedio final y el motivo por el cual el estudiante tiene baja conducta.";
                    }
                    else
                    {
                        info_det.ConductaPromedioParcialFinal = Convert.ToDouble(info_conducta.Calificacion);
                        ListaConducta.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    }
                }
            }

            List<aca_MatriculaConducta_Info> model = new List<aca_MatriculaConducta_Info>();
            model = ListaConducta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaConducta", model);
        }
        #endregion

        #region Json
        public JsonResult cargar_calificaciones_conducta(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdCatalogoParcial = 0)
        {
            List<aca_MatriculaConducta_Info> Lista_CalificacionConducta= new List<aca_MatriculaConducta_Info>();
            if (IdCatalogoParcial>0)
            {
                Lista_CalificacionConducta = bus_conducta_cal.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
                foreach (var item in Lista_CalificacionConducta)
                {
                    item.IdCatalogoParcial = IdCatalogoParcial;
                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP1);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP1);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP1);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP1);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP1;
                    }

                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP2);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP1);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP2);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP2);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP2;
                    }

                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP3);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP3);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP3);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP3);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP3;
                    }

                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP4);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP4);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP4);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP4);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP4;
                    }

                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP5);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP5);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP5);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP5);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP5;
                    }

                    if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        item.SecuenciaConductaPromedioParcial = Convert.ToInt32(item.SecuenciaPromedioP6);
                        item.ConductaPromedioParcial = Convert.ToDouble(item.PromedioP6);
                        item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP6);
                        item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP6);
                        item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP6;
                    }
                }
            }

            ListaConducta.set_list(Lista_CalificacionConducta, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(Lista_CalificacionConducta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarParciales_X_Quimestre(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdCatalogoTipo = 0)
        {
            var resultado = bus_parcial.GetList(IdEmpresa, IdSede, IdAnio, IdCatalogoTipo, DateTime.Now.Date);
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
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_ConductaParcial.UploadValidationSettings, UploadControlSettings_ConductaParcial.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, int IdCatalogoTipo = 0, int IdCatalogoParcial = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MatriculaConducta_Info model = new aca_MatriculaConducta_Info
            {
                IdEmpresa = IdEmpresa,
                IdSede = IdSede,
                IdAnio = IdAnio,
                IdNivel = IdNivel,
                IdJornada = IdJornada,
                IdCurso = IdCurso,
                IdParalelo = IdParalelo,
                IdCatalogoTipo = IdCatalogoTipo,
                IdCatalogoParcial = IdCatalogoParcial,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaConducta_Info> lst_combos = bus_conducta_cal.GetList_Combos_Inspector(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            Lista_Combos.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<aca_MatriculaConducta_Info> ListaCalificacionesConducta = new List<aca_MatriculaConducta_Info>();
            ListaCalificacionesConducta = bus_conducta_cal.GetList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);

            foreach (var item in ListaCalificacionesConducta)
            {
                item.IdCatalogoParcial = IdCatalogoParcial;
                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP1);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP1);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP1;
                }

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP2);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP2);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP2;
                }

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP3);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP3);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP3;
                }

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP4);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP4);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP4;
                }

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP5);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP5);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP5;
                }

                if (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                {
                    item.SecuenciaConductaPromedioParcialFinal = Convert.ToInt32(item.SecuenciaPromedioFinalP6);
                    item.ConductaPromedioParcialFinal = Convert.ToDouble(item.PromedioFinalP6);
                    item.MotivoPromedioParcialFinal = item.MotivoPromedioFinalP6;
                }
            }

            ListaConducta.set_list(ListaCalificacionesConducta, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(aca_MatriculaConducta_Info model)
        {
            try
            {
                var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                string IdUsuario = SessionFixed.IdUsuario;
                bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
                var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
                var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
                var Lista_CalificacionesConducta = ListaConducta.get_list(model.IdTransaccionSession);
                var info_parcial = bus_parcial.GetInfo(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdCatalogoParcial);

                bool guardar = true;
                ViewBag.mensaje = null;
                ViewBag.MensajeSuccess = null;

                Lista_CalificacionesConducta.ForEach(q =>
                {
                    q.IdAnio = model.IdAnio; q.IdSede = model.IdSede; q.IdNivel = model.IdNivel; q.IdJornada = model.IdJornada; q.IdCurso = model.IdCurso;
                    q.IdParalelo = model.IdParalelo; q.IdMateria = model.IdMateria; q.IdCatalogoParcial = model.IdCatalogoParcial;
                });


                foreach (var item in Lista_CalificacionesConducta)
                {
                    if (item.ValidoImportacion == false)
                    {
                        ViewBag.mensaje += "Existen registros con conducta por debajo del mínimo aceptado, debe ingresar motivo de la conducta.";
                        guardar = false;
                        break;
                    }
                }

                if (guardar == true)
                {
                    ViewBag.mensaje = null;

                    foreach (var item in Lista_CalificacionesConducta)
                    {
                        if (!bus_conducta_cal.ModicarPromedioFinal(item))
                        {
                            ViewBag.mensaje = "Error al importar el archivo";
                            cargar_combos_detalle();
                            return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel = model.IdNivel, IdJornada = model.IdJornada, IdCurso = model.IdCurso, IdParalelo = model.IdParalelo, IdCatalogoTipo = model.IdCatalogoTipo, IdCatalogoParcial = model.IdCatalogoParcial });
                        }
                    }
                    ViewBag.MensajeSuccess = MensajeSuccess;
                }
                cargar_combos(model);
                return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel = model.IdNivel, IdJornada = model.IdJornada, IdCurso = model.IdCurso, IdParalelo = model.IdParalelo, IdCatalogoTipo = model.IdCatalogoTipo, IdCatalogoParcial = model.IdCatalogoParcial });
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                cargar_combos_detalle();
                return RedirectToAction("Importar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, IdAnio = model.IdAnio, IdNivel = model.IdNivel, IdJornada = model.IdJornada, IdCurso = model.IdCurso, IdParalelo = model.IdParalelo, IdCatalogoTipo = model.IdCatalogoTipo, IdCatalogoParcial = model.IdCatalogoParcial });
            }
        }

        public ActionResult GridViewPartial_MatriculaConductaImportacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaConducta.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaConductaImportacion", model);
        }
        #endregion
    }

    public class aca_MatriculaConducta_Combos_List
    {
        string Variable = "aca_MatriculaConducta_Combos_Info";
        public List<aca_MatriculaConducta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaConducta_Info> list = new List<aca_MatriculaConducta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaConducta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaConducta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    public class aca_MatriculaConducta_List
    {
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_MatriculaConducta_Bus bus_conducta = new aca_MatriculaConducta_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta_equivalencia = new aca_AnioLectivoConductaEquivalencia_Bus();
        string Variable = "aca_MatriculaConducta_Info";
        public List<aca_MatriculaConducta_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaConducta_Info> list = new List<aca_MatriculaConducta_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaConducta_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaConducta_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_MatriculaConducta_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_MatriculaConducta_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpresa == IdEmpresa && m.IdMatricula == info_det.IdMatricula).FirstOrDefault();
            var info_matricula = bus_matricula.GetInfo(edited_info.IdEmpresa, edited_info.IdMatricula);
            //var info_conducta = bus_conducta_equivalencia.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAnio, info_det.SecuenciaConductaPromedioParcialFinal);

            if (edited_info!=null)
            {
                edited_info.SecuenciaConductaPromedioParcialFinal = info_det.SecuenciaConductaPromedioParcialFinal;
                edited_info.ConductaPromedioParcialFinal = info_det.ConductaPromedioParcialFinal;
                edited_info.MotivoPromedioParcialFinal = info_det.MotivoPromedioParcialFinal;

                bus_conducta.ModicarPromedioFinal(edited_info);
            }

        }
    }

    public class UploadControlSettings_ConductaParcial
    {

        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            aca_MatriculaConducta_List Lista_ConductaParcial = new aca_MatriculaConducta_List();
            List<aca_MatriculaConducta_Info> Lista_Conducta = new List<aca_MatriculaConducta_Info>();
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
                    if (!reader.IsDBNull(1) && cont > 0)
                    {
                        var IdMatricula = (Convert.ToInt32(reader.GetValue(1)));
                        var pe_nombreCompleto = (Convert.ToString(reader.GetValue(2)).Trim());
                        var Conducta = (Convert.ToString(reader.GetValue(3)).Trim());
                        var MotivoConducta = (Convert.ToString(reader.GetValue(4)).Trim());

                        var info_conducta = bus_conducta.GetInfo_x_Letra(IdEmpresa, info_anio.IdAnio, Conducta);
                        //item.SecuenciaConductaPromedioParcialFinal = item.SecuenciaImportacion ?? 0;
                        //item.ConductaPromedioParcialFinal = item.ConductaImportacion ?? 0;
                        //item.MotivoPromedioParcialFinal = item.MotivoConductaImportacion;
                        aca_MatriculaConducta_Info info = new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdMatricula = IdMatricula,
                            pe_nombreCompleto = pe_nombreCompleto,
                            SecuenciaConductaPromedioParcialFinal = (info_conducta == null ? (int?)null : info_conducta.Secuencia),
                            ConductaPromedioParcialFinal = (info_conducta == null ? (double?)null : Convert.ToDouble(info_conducta.Calificacion)),
                            MotivoPromedioParcialFinal = MotivoConducta
                        };

                        if (info.SecuenciaConductaPromedioParcialFinal == null)
                        {
                            info.ValidoImportacion = true;
                        }
                        else
                        {
                            if (info_conducta.IngresaMotivo==true)
                            {
                                if (string.IsNullOrEmpty(info.MotivoPromedioParcialFinal))
                                {
                                    info.ValidoImportacion = false;
                                }
                                else
                                {
                                    info.ValidoImportacion = true;
                                }
                            }
                            else
                            {
                                info.ValidoImportacion = true;
                            }
                        }

                        Lista_Conducta.Add(info);
                    }
                    else
                        cont++;
                }

                Lista_ConductaParcial.set_list(Lista_Conducta, IdTransaccionSession);
            }
        }
    }
}