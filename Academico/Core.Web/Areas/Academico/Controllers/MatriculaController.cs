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
        aca_AnioLectivo_Jornada_Curso_Bus bus_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Bus();
        aca_Plantilla_Bus bus_plantilla = new aca_Plantilla_Bus();
        aca_AlumnoDocumento_Bus bus_alumno_documento = new aca_AlumnoDocumento_Bus();
        aca_AnioLectivo_Curso_Documento_List Lista_DocumentosMatricula = new aca_AnioLectivo_Curso_Documento_List();
        aca_AnioLectivo_Jornada_Curso_Bus bus_aniolectivo_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Bus();
        aca_AnioLectivo_Curso_Documento_Bus bus_curso_documento = new aca_AnioLectivo_Curso_Documento_Bus();
        aca_SocioEconomico_Bus bus_socioeconomico = new aca_SocioEconomico_Bus();
        aca_AnioLectivo_Paralelo_Profesor_Bus bus_materias_x_paralelo = new aca_AnioLectivo_Paralelo_Profesor_Bus();
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);

            aca_Matricula_Info model = new aca_Matricula_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = info_anio.IdAnio,
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Matricula_Info> lista = bus_matricula.GetList(model.IdEmpresa, model.IdAnio, model.IdSede, true);
            Lista_Matricula.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Matricula_Info model)
        {
            List<aca_Matricula_Info> lista = bus_matricula.GetList(model.IdEmpresa, model.IdAnio, model.IdSede, true);
            Lista_Matricula.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

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

        #region GridDetalle alumnos paralelo
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnosPorParalelo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_Matricula_PorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnosPorParalelo", model);
        }
        #endregion

        #region DetallePlantilla (nuevo)
        [ValidateInput(false)]
        public ActionResult GridViewPartial_DetallePlantilla()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Rubro_Info> model = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q => q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdString;
                    else
                        selectedIDs += "," + item.IdString;
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }

            return PartialView("_GridViewPartial_DetallePlantilla", model);
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

        #region Grid documentos
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnoDocumentos()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Curso_Documento_Info> model = Lista_DocumentosMatricula.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewData["documentoIDs"] = Request.Params["documentoIDs"];
            if (ViewData["documentoIDs"] == null)
            {
                int x = 0;
                string documentoIDs = "";
                foreach (var item in model.Where(q => q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        documentoIDs = Convert.ToString(item.IdStringDoc);
                    else
                        documentoIDs += "," + item.IdStringDoc;
                    x++;
                }
                ViewData["documentoIDs"] = documentoIDs;
            }

            return PartialView("_GridViewPartial_AlumnoDocumentos", model);
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
            decimal TotalProntoPago = 0;
            decimal ValorDescuento = 0;
            decimal ValorRubro = 0;

            List<aca_Matricula_Rubro_Info> lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            if (IdMatricula == 0)
            {
                var info_plantilla = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
                lst_MatriculaRubro = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                var IdPrimerPeriodo = lst_MatriculaRubro.Min(q => q.IdPeriodo);

                foreach (var item in lst_MatriculaRubro)
                {
                    if (item.IdPeriodo == IdPrimerPeriodo)
                    {
                        item.seleccionado = true;

                        if (item.AplicaProntoPago == true)
                        {
                            if (info_plantilla.TipoDescuento == "%")
                            {
                                ValorDescuento = (item.Total * (info_plantilla.Valor / 100));
                                ValorRubro = item.Total - ValorDescuento;
                                TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                ValorRubro = (item.Total - info_plantilla.Valor);
                                TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                            }

                            Total = Total + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            Total = Total + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                            TotalProntoPago = TotalProntoPago + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                }

                ListaMatriculaRubro.set_list(lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            else
            {
                lst_MatriculaRubro = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                var lst_nueva_plantilla = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                var lista_no_cobrado = lst_MatriculaRubro.Where(q=>q.EnMatricula==false).ToList();

                List<aca_Matricula_Rubro_Info> lista_nueva = new List<aca_Matricula_Rubro_Info>();

                foreach (var item in lst_MatriculaRubro)
                {
                    if (item.EnMatricula== true)
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
                            item2.IdMecanismo = item1.IdMecanismo;
                            lista_nueva.Add(item2);
                        }
                    }
                }

                ListaMatriculaRubro.set_list(lista_nueva, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            return Json(new { Valor =Total, ProntoPago =TotalProntoPago}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SumarValores(int IdAnio = 0, int IdPlantilla = 0, string Seleccionados = "")
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            decimal Total = 0;
            decimal TotalProntoPago = 0;
            decimal ValorRubro = 0;
            decimal ValorDescuento = 0;

            if (Seleccionados != null && Seleccionados != "")
            {
                string[] array = Seleccionados.Split(',');
                var info_plantilla = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
                var lst_detalle = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                foreach (var item in array)
                {
                    var info = lst_detalle.Where(q=>q.IdString== item).FirstOrDefault();
                    if (info.AplicaProntoPago == true)
                    {
                        if (info_plantilla.TipoDescuento == "%")
                        {
                            ValorDescuento = (info.Total * (info_plantilla.Valor / 100));
                            ValorRubro = info.Total - ValorDescuento;
                            TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            ValorRubro = (info.Total - info_plantilla.Valor);
                            TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                        }

                        Total = Total + Math.Round((info.Total), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Total = Total + Math.Round((info.Total), 2, MidpointRounding.AwayFromZero);
                        TotalProntoPago = TotalProntoPago + Math.Round((info.Total), 2, MidpointRounding.AwayFromZero);
                    }
                    
                }
            }
            return Json(new { ValorPlantilla= Total, ValorPlantillaProntoPago  = TotalProntoPago }, JsonRequestBehavior.AllowGet);
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
                        mensaje = "No se puede matricular al alumno, Usuario: "+ NegarMatricula.IdUsuarioCreacion + " , detalle de negacion: " + NegarMatricula.Observacion;
                    }
                }
            }            

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardar(DateTime Fecha, int IdEmpresa = 0, int IdAnio = 0, decimal IdAlumno = 0, string IdComboCurso = "", int IdParalelo = 0, int IdPlantilla = 0,
            int IdMecanismo = 0, int IdMecanismoDet = 0,string Observacion = "",string Ids = "", string IDs_Doc="", decimal IdTransaccionSession = 0)
        {
            decimal Matricula = 0;
            int Empresa = 0;
            int Plantilla = 0;
            int Periodo = 0;
            int Rubro = 0;
            var lst_DetallePlantilla = ListaMatriculaRubro.get_list(Convert.ToDecimal(IdTransaccionSession));
            var lst_DetalleDocumentos = Lista_DocumentosMatricula.get_list(Convert.ToDecimal(IdTransaccionSession));

            if (IdAnio!=0 && IdAlumno != 0 && IdComboCurso!="" && IdParalelo!=0 && IdPlantilla!=0 && IdMecanismo!=0 && IdMecanismoDet!=0 && lst_DetallePlantilla.Count>0 && lst_DetalleDocumentos.Count >0)
            {
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
                    Fecha = Fecha.Date,
                    Observacion = Observacion,
                    IdUsuarioCreacion = SessionFixed.IdUsuario,
                    lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>()
                };

                string[] array = Ids.Split(',');
                if (lst_DetallePlantilla.Count > 0)
                {
                    foreach (var item_det in lst_DetallePlantilla)
                    {
                        aca_Matricula_Rubro_Info info_mat_rubro = new aca_Matricula_Rubro_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdPeriodo = item_det.IdPeriodo,
                            IdRubro = item_det.IdRubro,
                            IdMecanismo = IdMecanismoDet,
                            IdProducto = item_det.IdProducto,
                            Subtotal = item_det.Subtotal,
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
                            Empresa = Convert.ToInt32(item.Substring(0, 4));
                            Plantilla = Convert.ToInt32(item.Substring(4, 6));
                            Periodo = Convert.ToInt32(item.Substring(10, 4));
                            Rubro = Convert.ToInt32(item.Substring(14, 6));
                            var info_detalle = lst_DetallePlantilla.Where(q => q.IdPlantilla == Plantilla && q.IdRubro == Rubro && q.IdPeriodo == Periodo).FirstOrDefault();

                            foreach (var item1 in info_matricula.lst_MatriculaRubro)
                            {
                                if (item1.IdPeriodo == Periodo && item1.IdRubro == Rubro)
                                {
                                    item1.EnMatricula = true;
                                    item1.IdMecanismo = info_matricula.IdMecanismo;
                                }

                            }

                        }
                    }
                }

                string[] array_doc = IDs_Doc.Split(',');
                var lst_alumno_documentos = new List<aca_AlumnoDocumento_Info>();
                if (IDs_Doc!="")
                {
                    foreach (var item in array_doc)
                    {
                        var existe_documento = bus_alumno_documento.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAlumno, Convert.ToInt32(item));
                        if (existe_documento == null)
                        {
                            var info_doc = new aca_AlumnoDocumento_Info
                            {
                                IdEmpresa = info_matricula.IdEmpresa,
                                IdAlumno = info_matricula.IdAlumno,
                                IdDocumento = Convert.ToInt32(item),
                                EnArchivo = true
                            };

                            lst_alumno_documentos.Add(info_doc);
                        }
                    }
                }
                
                info_matricula.lst_documentos = lst_alumno_documentos;

                #region Calificacion y conducta
                var lst_materias_x_curso = bus_materias_x_paralelo.GetList(IdEmpresa, IdSede,IdAnio,IdNivel,IdJornada,IdCurso,IdParalelo);
                info_matricula.lst_calificacion = new List<aca_MatriculaCalificacion_Info>();
                info_matricula.lst_conducta = new List<aca_MatriculaConducta_Info>();
                if (lst_materias_x_curso!= null && lst_materias_x_curso.Count >0)
                {
                    foreach (var item in lst_materias_x_curso)
                    {
                        var info_calificacion = new aca_MatriculaCalificacion_Info
                        {
                            IdProfesor= item.IdProfesor,
                            IdMateria = item.IdMateria
                        };

                        info_matricula.lst_calificacion.Add(info_calificacion);

                        var info_conducta = new aca_MatriculaConducta_Info
                        {
                            IdProfesor = item.IdProfesor,
                            IdMateria = item.IdMateria
                        };

                        info_matricula.lst_conducta.Add(info_conducta);
                    }
                }
                #endregion

                if (info_matricula.IdPersonaR != 0 && info_matricula.IdPersonaF != 0)
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
                    mensaje = "Verique que el alumno tenga asignados representantes";
                }
            }
            else
            {
                mensaje = "Ingrese la información solicitada";
            }
                
            return Json(new { msg = mensaje, IdEmpresa = Empresa, IdMatricula = Matricula }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult actualizar(DateTime Fecha, int IdEmpresa = 0, int IdAnio = 0, decimal IdAlumno = 0, string IdComboCurso = "", int IdParalelo = 0, int IdPlantilla = 0,
            string Observacion = "", decimal IdTransaccionSession = 0, int IdMatricula=0, string IDs_Doc = "")
        {
            decimal Matricula = 0;
            int Empresa = 0;
            var lst_DetallePlantilla = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var lst_DetalleDocumentos = Lista_DocumentosMatricula.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if (IdAnio != 0 && IdAlumno != 0 && IdComboCurso != "" && IdParalelo != 0 && IdPlantilla != 0 && lst_DetallePlantilla.Count > 0)
            {
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
                    IdMatricula = IdMatricula,
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
                    Fecha = Fecha.Date,
                    Observacion = Observacion,
                    IdUsuarioModificacion = SessionFixed.IdUsuario,
                    lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>()
                };

                if (lst_DetallePlantilla.Count > 0)
                {
                    foreach (var item_det in lst_DetallePlantilla)
                    {
                        aca_Matricula_Rubro_Info info_mat_rubro = new aca_Matricula_Rubro_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdMatricula = IdMatricula,
                            IdPeriodo = item_det.IdPeriodo,
                            IdMecanismo = item_det.IdMecanismo,
                            IdRubro = item_det.IdRubro,
                            IdProducto = item_det.IdProducto,
                            Subtotal = item_det.Subtotal,
                            IdCod_Impuesto_Iva = item_det.IdCod_Impuesto_Iva,
                            Porcentaje = item_det.Porcentaje,
                            ValorIVA = item_det.ValorIVA,
                            FechaFacturacion = item_det.FechaFacturacion,
                            EnMatricula = item_det.EnMatricula,
                            Total = item_det.Total
                        };

                        info_matricula.lst_MatriculaRubro.Add(info_mat_rubro);
                    }
                }

                string[] array_doc = IDs_Doc.Split(',');
                var lst_alumno_documentos = new List<aca_AlumnoDocumento_Info>();
                if (IDs_Doc != "")
                {
                    foreach (var item in array_doc)
                    {
                        var info_doc = new aca_AlumnoDocumento_Info
                        {
                            IdEmpresa = info_matricula.IdEmpresa,
                            IdAlumno = info_matricula.IdAlumno,
                            IdDocumento = Convert.ToInt32(item),
                            EnArchivo = true
                        };
                        lst_alumno_documentos.Add(info_doc);
                    }
                }
                info_matricula.lst_documentos = lst_alumno_documentos;

                if (info_matricula.IdPersonaR != 0 && info_matricula.IdPersonaF != 0)
                {
                    if (!bus_matricula.ModificarDB(info_matricula))
                    {
                        mensaje = "No se ha podido guardar el registro";
                    }
                    Empresa = info_matricula.IdEmpresa;
                    Matricula = info_matricula.IdMatricula;
                }
                else
                {
                    mensaje = "Verique que el alumno tenga asignados representantes";
                }
            }
            else
            {
                mensaje = "Ingrese la información solicitada";
            }

            return Json(new { msg = mensaje, IdEmpresa = Empresa, IdMatricula = Matricula }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditarMecanismo(string IdString = "")
        {
            string resultado = string.Empty;
            int IdEmpresa = Convert.ToInt32(IdString.Substring(0, 4));
            int IdMatricula = Convert.ToInt32(IdString.Substring(4, 6));
            int IdPeriodo = Convert.ToInt32(IdString.Substring(10, 4));
            int IdRubro = Convert.ToInt32(IdString.Substring(14, 6));

            var model = bus_matricula_rubro.GetInfo(IdEmpresa, IdMatricula, IdPeriodo, IdRubro);
            var lst_detalle = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (model != null)
            {
                if (lst_detalle.Count >0)
                {
                    foreach (var item in lst_detalle)
                    {
                        if (item.IdEmpresa == IdEmpresa && item.IdMatricula == IdMatricula && item.IdPeriodo == IdPeriodo && item.IdRubro == IdRubro)
                        { 

                        }
                    }
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerMatriculaAnterior(int IdEmpresa = 0, int IdAnio = 0, int IdAlumno = 0)
        {
            mensaje = "";
            decimal IdMatricula = 0;
            var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            var Anioanterior = info_anio.FechaDesde.Year - 1;
            var info_anio_anterior = bus_anio.GetInfo_AnioAnterior(IdEmpresa, Anioanterior);
            if (info_anio_anterior!= null)
            {
                var info_matricula_anterior = bus_matricula.GetInfo_ExisteMatricula(IdEmpresa, info_anio_anterior.IdAnio, IdAlumno);

                if (info_matricula_anterior == null)
                {
                    mensaje = "El alumno no tiene matricula anterior";
                }
                else
                {
                    IdMatricula = info_matricula_anterior.IdMatricula;
                }
            }
            else
            {
                mensaje = "El alumno no tiene matricula anterior";
            }

            return Json(new { msg=mensaje, Matricula = IdMatricula }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetAlumnoDocumentos(int IdEmpresa = 0, int IdAnio = 0, string IdCurso = "", decimal IdAlumno = 0)
        {
            var IdSede = Convert.ToInt32(IdCurso.Substring(8, 4));
            var IdNivel = Convert.ToInt32(IdCurso.Substring(12, 4));
            var IdJornada = Convert.ToInt32(IdCurso.Substring(16, 4));
            var IdCursoMat = Convert.ToInt32(IdCurso.Substring(20, 4));
            List<aca_AnioLectivo_Curso_Documento_Info> lst_Documentos = new List<aca_AnioLectivo_Curso_Documento_Info>();

            var lst_doc_curso = bus_curso_documento.GetList_Matricula(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCursoMat);
            var lst_doc_alumno = bus_alumno_documento.GetList(IdEmpresa, IdAlumno, true);

            if (lst_doc_curso!= null && lst_doc_curso.Count>0)
            {
                foreach (var item in lst_doc_curso)
                {
                    item.seleccionado = false;
                    item.IdStringDoc = Convert.ToString(item.IdDocumento);

                    foreach (var item1 in lst_doc_alumno)
                    {
                        if (item.IdDocumento == item1.IdDocumento)
                        {
                            item.seleccionado = true;
                            break;
                        }
                    }
                    lst_Documentos.Add(item);

                }
            }

            Lista_DocumentosMatricula.set_list(lst_Documentos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(lst_Documentos, JsonRequestBehavior.AllowGet);
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
            model.lst_alumno_documentos = new List<aca_AnioLectivo_Curso_Documento_Info>();
            
            Lista_Matricula_PorCurso.set_list(model.lst_matricula_curso, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdMatricula = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Matricula_Info model = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            model.Validar = "N";

            var info_curso = bus_jornada_curso.GetInfoCursoMatricula(model.IdEmpresa, model.IdAnio, model.IdMatricula);
            model.IdComboCurso = (info_curso==null ? "" : info_curso.IdComboCurso);
            model.NomCurso = (info_curso == null ? "" : info_curso.NomCurso);

            var IdAnio = Convert.ToInt32(model.IdComboCurso.Substring(4, 4));
            var IdSede = Convert.ToInt32(model.IdComboCurso.Substring(8, 4));
            var IdNivel = Convert.ToInt32(model.IdComboCurso.Substring(12, 4));
            var IdJornada = Convert.ToInt32(model.IdComboCurso.Substring(16, 4));
            var IdCursoMat = Convert.ToInt32(model.IdComboCurso.Substring(20, 4));

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

            model.lst_alumno_documentos = new List<aca_AnioLectivo_Curso_Documento_Info>();
            var lst_doc_curso = bus_curso_documento.GetList_Matricula(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCursoMat);
            var lst_doc_alumno = bus_alumno_documento.GetList(IdEmpresa, model.IdAlumno, true);

            if (lst_doc_curso != null && lst_doc_curso.Count > 0)
            {
                foreach (var item in lst_doc_curso)
                {
                    item.seleccionado = false;
                    item.IdStringDoc = Convert.ToString(item.IdDocumento);

                    foreach (var item1 in lst_doc_alumno)
                    {
                        if (item.IdDocumento == item1.IdDocumento)
                        {
                            item.seleccionado = true;
                            break;
                        }     
                    }
                    model.lst_alumno_documentos.Add(item);
                }
            }

            Lista_DocumentosMatricula.set_list(model.lst_alumno_documentos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

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

        public void UpdateRow(aca_Matricula_Rubro_Info info_det, decimal IdTransaccion)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_Matricula_Rubro_Info edited_info = get_list(IdTransaccion).Where(q => q.IdString == info_det.IdString).FirstOrDefault();
            edited_info.IdMecanismo = info_det.IdMecanismo;
        }
    }
}