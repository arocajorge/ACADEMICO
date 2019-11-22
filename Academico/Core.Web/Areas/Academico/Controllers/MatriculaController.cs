using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaController : Controller
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
        string MensajeSuccess = "La transacción se ha realizado con éxito";
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

            aca_Matricula_Info model = new aca_Matricula_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Matricula_Info> lista = bus_matricula.GetList(model.IdEmpresa, true);
            Lista_Matricula.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Matricula()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_Matricula.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Matricula", model);
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
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
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
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdAlumno = IdAlumno, Validar = Validar });
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

        public ActionResult ComboBoxPartial_Paralelo()
        {
            string IdComboCurso = (Request.Params["IdCurso"] != null) ? (Request.Params["IdCurso"]).ToString() : null;
            int IdAnio = -1;
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
            
            return PartialView("_ComboBoxPartial_Paralelo", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
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

        #region GridDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnosPorParalelo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_Matricula_PorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnosPorParalelo", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_DetallePlantilla()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Rubro_Info> model = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_DetallePlantilla", model);
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaRubro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Rubro_Info> model = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaRubro", model);
        }
        #endregion

        #region Json
        public JsonResult SetMatricula_PorCurso(int IdEmpresa=0, int IdAnio = 0, string IdComboCurso = "", int IdParalelo = 0)
        {
            int IdSede = 0;
            int IdNivel = 0;
            int IdJornada = 0;
            int IdCurso = 0;

            //public JsonResult SetMatricula_PorCurso(int IdEmpresa=0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0, decimal IdTransaccionSession = 0)
            //IdEmpresa = Convert.ToInt32(model.IdComboCurso.Substring(0, 4));
            //IdAnio = Convert.ToInt32(model.IdComboCurso.Substring(4, 4));
            IdSede = Convert.ToInt32(IdComboCurso.Substring(8, 4));
            IdNivel = Convert.ToInt32(IdComboCurso.Substring(12, 4));
            IdJornada = Convert.ToInt32(IdComboCurso.Substring(16, 4));
            IdCurso = Convert.ToInt32(IdComboCurso.Substring(20, 4));

            var lista_PorCurso = bus_matricula.GetList_PorCurso(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo);
            Lista_Matricula_PorCurso.set_list(lista_PorCurso, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(lista_PorCurso, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetMatriculaRubro(int IdEmpresa = 0, int IdAnio = 0, int IdPlantilla = 0, int IdMatricula=0)
        {
            decimal Total = 0;
            List<aca_Matricula_Rubro_Info> lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            if (IdMatricula == 0)
            {
                lst_MatriculaRubro = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                var IdPrimerPeriodo = lst_MatriculaRubro.Min(q => q.IdPeriodo);
                foreach (var item in lst_MatriculaRubro)
                {
                    if (item.IdPeriodo == IdPrimerPeriodo)
                    {
                        item.seleccionado = true;
                    }
                }

                Total = (lst_MatriculaRubro == null ? 0 : lst_MatriculaRubro.Where(q => q.seleccionado == true).Sum(q => q.Total));
                ListaMatriculaRubro.set_list(lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            else
            {
                lst_MatriculaRubro = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                var lst_nueva_plantilla = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                var lista_no_cobrado = lst_MatriculaRubro.Where(q=>q.FechaFacturacion==null).ToList();

                List<aca_Matricula_Rubro_Info> lista_nueva = new List<aca_Matricula_Rubro_Info>();

                foreach (var item in lst_MatriculaRubro)
                {
                    if (item.FechaFacturacion!= null)
                    {
                        lista_nueva.Add(item);
                    }
                }

                
                foreach (var item1 in lista_no_cobrado)
                {
                    foreach (var item2 in lst_nueva_plantilla)
                    {
                        if (item1.IdPeriodo == item2.IdPeriodo)
                        {
                            lista_nueva.Add(item2);
                        }
                    }
                }

                ListaMatriculaRubro.set_list(lista_nueva, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            return Json(Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SumarValores(string TotalRows)
        {
            double Total = 0;
            if (TotalRows != null && TotalRows != "")
            {
                string[] array = TotalRows.Split(',');
                foreach (var item in array)
                {
                    Total = Math.Round((Total + Convert.ToDouble(item)), 2, MidpointRounding.AwayFromZero);
                }
            }
            return Json(Total, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarMatricula(int IdEmpresa = 0, int IdAnio = 0, int IdAlumno = 0, string Nuevo = "S")
        {
            var IdCatalogoPERNEG_Negar = Convert.ToInt32(cl_enumeradores.eCatalogoPermisoMatricula.NEGAR);
            var IdCatalogoPERNEG_Permitir = Convert.ToInt32(cl_enumeradores.eCatalogoPermisoMatricula.PERMITIR);
            var mensaje = "";
            var Validacion = 0;

            if (Nuevo == "S")
            {
                var existe_matricula = bus_matricula.GetInfo_ExisteMatricula(IdEmpresa, IdAnio, IdAlumno);
                if (existe_matricula != null)
                {
                    mensaje = "El alumno ya se encuentra matriculado en el año lectivo seleccionado";
                    Validacion = 1;
                }
            }

            if (Validacion == 0)
            {
                var NegarMatricula = bus_permiso.GetInfo_ByMatricula(IdEmpresa, IdAnio, IdAlumno, IdCatalogoPERNEG_Negar);

                if (NegarMatricula != null)
                {
                    var PermitirMatricula = bus_permiso.GetInfo_ByMatricula(IdEmpresa, IdAnio, IdAlumno, IdCatalogoPERNEG_Permitir);

                    if (PermitirMatricula == null)
                    {
                        mensaje = "No se puede matricular al alumno, OBS: " + NegarMatricula.Observacion;
                    }
                }
            }            

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardar(DateTime Fecha, int IdEmpresa = 0, int IdAnio = 0, decimal IdAlumno = 0, string IdComboCurso = "", int IdParalelo = 0, int IdPlantilla = 0,
            int IdMecanismo = 0, string Observacion = "",string Ids = "", decimal IdTransaccionSession = 0)
        {
            decimal Matricula = 0;
            int Empresa = 0;
            int Plantilla = 0;
            int Periodo = 0;
            int Rubro = 0;

            //IdEmpresa = Convert.ToInt32(model.IdComboCurso.Substring(0, 4));
            //IdAnio = Convert.ToInt32(model.IdComboCurso.Substring(4, 4));
            int IdSede = Convert.ToInt32(IdComboCurso.Substring(8, 4));
            int IdNivel = Convert.ToInt32(IdComboCurso.Substring(12, 4));
            int IdJornada = Convert.ToInt32(IdComboCurso.Substring(16, 4));
            int IdCurso = Convert.ToInt32(IdComboCurso.Substring(20, 4));

            var info_rep_eco = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.ECON.ToString());
            var info_rep_legal = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.LEGAL.ToString());

            aca_Matricula_Info info_matricula = new aca_Matricula_Info
            {
                IdEmpresa = IdEmpresa,
                IdAlumno = IdAlumno,
                IdAnio = IdAnio,
                IdSede = IdSede,
                IdNivel = IdNivel,
                IdJornada = IdJornada,
                IdCurso = IdCurso,
                IdParalelo = IdParalelo,
                IdPersonaF = (info_rep_eco == null ? 0 : info_rep_eco.IdPersona),
                IdPersonaR = (info_rep_legal == null ? 0 : info_rep_legal.IdPersona),
                IdPlantilla = IdPlantilla,
                IdMecanismo = IdMecanismo,
                Fecha = Fecha,
                Observacion = Observacion,
                lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>()
            };

            string[] array = Ids.Split(',');
            var lst_DetallePlantilla = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if (lst_DetallePlantilla.Count >0)
            {
                foreach (var item_det in lst_DetallePlantilla)
                {
                    aca_Matricula_Rubro_Info info_mat_rubro = new aca_Matricula_Rubro_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdPeriodo = item_det.IdPeriodo,
                        IdRubro = item_det.IdRubro,
                        IdProducto = item_det.IdProducto,
                        Subtotal = item_det.IdProducto,
                        IdCod_Impuesto_Iva = item_det.IdCod_Impuesto_Iva,
                        Porcentaje = item_det.Porcentaje,
                        ValorIVA = item_det.ValorIVA,
                        Total = item_det.Total
                    };

                    info_matricula.lst_MatriculaRubro.Add(info_mat_rubro);
                }

                if (Ids != "")
                {
                    foreach (var item in array)
                    {
                        Plantilla = Convert.ToInt32(item.Substring(0, 6));
                        Periodo = Convert.ToInt32(item.Substring(6, 4));
                        Rubro = Convert.ToInt32(item.Substring(10, 6));
                        var info_detalle = lst_DetallePlantilla.Where(q => q.IdPlantilla == Plantilla && q.IdRubro == Rubro && q.IdPeriodo == Periodo).FirstOrDefault();

                        foreach (var item1 in info_matricula.lst_MatriculaRubro)
                        {
                            if (item1.IdPeriodo== Periodo && item1.IdRubro==Rubro)
                            {
                                item1.FechaFacturacion = info_detalle.FechaDesde;
                            }
                            
                        }
                        
                    }
                }
            }

            if (info_matricula.IdAnio!=0 && info_matricula.IdAlumno!=0 && info_matricula.IdPersonaR!=0 && info_matricula.IdPersonaF!=0)
            {
                if (!bus_matricula.GuardarDB(info_matricula))
                {
                    mensaje = "No se ha podido guardar el registro";
                }
                Empresa = info_matricula.IdEmpresa;
                Matricula = info_matricula.IdMatricula;
            }
            else
            {
                if (info_matricula.IdPersonaR == 0 || info_matricula.IdPersonaF == 0)
                {
                    mensaje = "Verique que el alumno tenga asignados representantes";
                }
                else
                {
                    mensaje = "Ingrese la información solicitada";
                }
                
            }

            return Json(new { msg = mensaje, IdEmpresa = Empresa, IdMatricula = Matricula }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_Matricula_Info model = new aca_Matricula_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Fecha = DateTime.Now.Date,
                Validar = "S"
            };

            model.lst_matricula_curso = new List<aca_Matricula_Info>();
            Lista_Matricula_PorCurso.set_list(model.lst_matricula_curso, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Nuevo(aca_Matricula_Info model)
        //{
        //    model.IdUsuarioCreacion = SessionFixed.IdUsuario;
        //    var IdEmpresa = 0;
        //    int IdAnio = 0;
        //    int IdSede = 0;
        //    int IdNivel = 0;
        //    int IdJornada = 0;
        //    int IdCurso = 0;

        //    IdEmpresa = Convert.ToInt32(model.IdComboCurso.Substring(0,4));
        //    IdAnio = Convert.ToInt32(model.IdComboCurso.Substring(4, 4));
        //    IdSede = Convert.ToInt32(model.IdComboCurso.Substring(8, 4));
        //    IdNivel = Convert.ToInt32(model.IdComboCurso.Substring(12, 4));
        //    IdJornada = Convert.ToInt32(model.IdComboCurso.Substring(16, 4));
        //    IdCurso = Convert.ToInt32(model.IdComboCurso.Substring(20, 4));

        //    model.IdSede = IdSede;
        //    model.IdNivel = IdNivel;
        //    model.IdJornada = IdJornada;
        //    model.IdCurso = IdCurso;

        //    var info_rep_eco = bus_familia.GetInfo_Representante(model.IdEmpresa, model.IdAlumno, cl_enumeradores.eTipoRepresentante.ECON.ToString());
        //    var info_rep_legal = bus_familia.GetInfo_Representante(model.IdEmpresa, model.IdAlumno, cl_enumeradores.eTipoRepresentante.LEGAL.ToString());

        //    model.IdPersonaF = (info_rep_eco == null ? 0 : info_rep_eco.IdPersona);
        //    model.IdPersonaR = (info_rep_legal == null ? 0 : info_rep_legal.IdPersona);

        //    if (model.IdPersonaF == 0)
        //    {
        //        ViewBag.mensaje = "Debe de ingresar familiar como representante económico";
        //        SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
        //        cargar_combos();
        //        return View(model);
        //    }

        //    if (model.IdPersonaR == 0)
        //    {
        //        ViewBag.mensaje = "Debe de ingresar familiar como representante legal";
        //        SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
        //        cargar_combos();
        //        return View(model);
        //    }

        //    if (!bus_matricula.GuardarDB(model))
        //    {
        //        ViewBag.mensaje = "No se ha podido guardar el registro";
        //        SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
        //        cargar_combos();
        //        return View(model);
        //    }
        //    return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMatricula = model.IdMatricula, Exito = true });
        //}
        public ActionResult Modificar(int IdEmpresa = 0, int IdMatricula = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Matricula_Info model = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            model.Validar = "S";
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_matricula_curso = new List<aca_Matricula_Info>();
            model.lst_matricula_curso = bus_matricula.GetList_PorCurso(model.IdEmpresa, model.IdAnio, model.IdSede, model.IdNivel, model.IdJornada, model.IdCurso, model.IdParalelo);
            Lista_Matricula_PorCurso.set_list(model.lst_matricula_curso, model.IdTransaccionSession);

            model.lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            model.lst_MatriculaRubro = bus_matricula_rubro.GetList(model.IdEmpresa, model.IdMatricula);
            ListaMatriculaRubro.set_list(model.lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos();
            return View(model);
        }
        #endregion
    }

    public class aca_Matricula_List
    {
        string Variable = "aca_Matricula_Info";
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

    public class aca_Matricula_PorCurso_List
    {
        string Variable = "aca_Matricula_PorCurso_Info";
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

    public class aca_Matricula_Rubro_List
    {
        string Variable = "aca_Matricula_Rubro_Info";
        public List<aca_Matricula_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Matricula_Rubro_Info> list = new List<aca_Matricula_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Matricula_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Matricula_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}