﻿using Core.Bus.Academico;
using Core.Bus.Facturacion;
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
    public class CambioPlantillaMatriculaController : Controller
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
        aca_AnioLectivo_Periodo_Bus bus_anio_periodo = new aca_AnioLectivo_Periodo_Bus();
        aca_Paralelo_Bus bus_paralelo = new aca_Paralelo_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string mensaje = string.Empty;
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
            return PartialView("_ComboBoxPartial_Paralelo", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso=IdCurso});
        }

        public ActionResult ComboBoxPartial_Plantilla()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;

            return PartialView("_ComboBoxPartial_Plantilla", new aca_AnioLectivo_Curso_Plantilla_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
        }

        #endregion

        #region Combos bajo demanada Empleado
        public ActionResult Cmb_CambioPlantillaEmpleado()
        {
            int IdEmpresa_rol = (Request.Params["IdEmpresa_rol"] != null) ? int.Parse(Request.Params["IdEmpresa_rol"]) : -1;
            return PartialView("_CmbEmpleado", new aca_Matricula_Info { IdEmpresa_rol = IdEmpresa_rol });
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_mecanismo = bus_mecanismo.GetList(IdEmpresa, false);
            ViewBag.lst_mecanismo = lst_mecanismo;

            var lst_empresa = bus_empresa.get_list(false);
            ViewBag.lst_empresa = lst_empresa;
        }

        private bool validar(aca_Matricula_Info info, ref string msg)
        {
            if (string.IsNullOrEmpty(info.ObservacionCambio))
            {
                msg = "Debe de ingresar observación";
                return false;
            }

            var IdEmpresa_rol = info.IdEmpresa_rol;
            var IdEmpleado = info.IdEmpleado;

            info.IdEmpleado = null;
            if (info.lst_MatriculaRubro.Count()>0)
            {
                foreach (var item in info.lst_MatriculaRubro)
                {
                    var info_mecanismo = bus_mecanismo.GetInfo(info.IdEmpresa, item.IdMecanismo);
                    var info_termino_pago = bus_termino_pago.get_info((info_mecanismo == null ? "" : info_mecanismo.IdTerminoPago));

                    if (info_termino_pago != null && info_termino_pago.AplicaDescuentoNomina == true)
                    {
                        info.IdEmpresa_rol = IdEmpresa_rol;
                        info.IdEmpleado = IdEmpleado;

                        if (info.IdEmpresa_rol== null || info.IdEmpleado==null)
                        {
                            msg = "Debe de ingresar empresa y empleado para descuento en rol";
                            return false;
                        }

                        break;
                    }
                }
            }


            return true;
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
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_Matricula_Info> lista = bus_matricula.GetList(model.IdEmpresa, model.IdAnio, model.IdSede, true);
            Lista_Matricula.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CambioPlantillaMatricula()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Matricula_Info> model = Lista_Matricula.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CambioPlantillaMatricula", model);
        }
        #endregion

        #region Json
        public JsonResult SetMatriculaRubro(int IdEmpresa = 0, int IdAnio = 0, int IdPlantilla = 0, int IdMatricula = 0)
        {
            decimal Total = 0;
            decimal TotalProntoPago = 0;
            decimal ValorDescuento = 0;
            decimal ValorRubro = 0;
            decimal ValorTotal = 0;
            decimal ValorTotalPP = 0;
            List<aca_Matricula_Rubro_Info> lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            if (IdMatricula == 0)
            {
                var info_plantilla = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
                lst_MatriculaRubro = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                if (lst_MatriculaRubro.Count() > 0)
                {
                    var IdPrimerPeriodo = lst_MatriculaRubro.Min(q => q.IdPeriodo);

                    foreach (var item in lst_MatriculaRubro)
                    {
                        var info_anio_periodo = bus_anio_periodo.GetInfo(IdEmpresa, IdAnio, item.IdPeriodo);

                        if (item.IdPeriodo == IdPrimerPeriodo)
                        {
                            item.seleccionado = true;
                        }

                        if (item.AplicaProntoPago == true)
                        {
                            if (DateTime.Now.Date <= info_anio_periodo.FechaProntoPago)
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
                            }
                            else
                            {
                                ValorRubro = (item.Total);
                                TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                            }

                            Total = Total + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            ValorRubro = (item.Total);
                            Total = Total + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                            TotalProntoPago = TotalProntoPago + Math.Round((item.Total), 2, MidpointRounding.AwayFromZero);
                        }

                        item.ValorProntoPago = ValorRubro;
                        item.FechaProntoPago = Convert.ToDateTime(info_anio_periodo.FechaProntoPago);
                    }
                    ValorTotal = lst_MatriculaRubro.Where(q => q.seleccionado == true).Sum(q => q.Total);
                    ValorTotalPP = lst_MatriculaRubro.Where(q => q.seleccionado == true).Sum(q => q.ValorProntoPago);
                    ListaMatriculaRubro.set_list(lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
                else
                {
                    ValorTotal = Total;
                    ValorTotalPP = TotalProntoPago;
                    ListaMatriculaRubro.set_list(new List<aca_Matricula_Rubro_Info>(), Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }

            }
            else
            {
                lst_MatriculaRubro = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                var lst_nueva_plantilla = bus_matricula_rubro.GetList_Matricula(IdEmpresa, IdAnio, IdPlantilla);
                var lista_no_cobrado = lst_MatriculaRubro.Where(q => q.FechaFacturacion == null).ToList();

                List<aca_Matricula_Rubro_Info> lista_nueva = new List<aca_Matricula_Rubro_Info>();

                foreach (var item in lst_MatriculaRubro)
                {
                    if (item.EnMatricula == true)
                    {
                        lista_nueva.Add(item);
                    }
                }


                foreach (var item1 in lista_no_cobrado)
                {
                    foreach (var item2 in lst_nueva_plantilla)
                    {
                        if (item1.IdPeriodo == item2.IdPeriodo && item1.IdRubro== item2.IdRubro)
                        {
                            item2.IdMecanismo = item1.IdMecanismo;
                            lista_nueva.Add(item2);
                        }
                    }
                }

                ListaMatriculaRubro.set_list(lista_nueva, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }


            return Json(new { Valor = ValorTotal, ProntoPago = ValorTotalPP }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDatosMecanismo(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            bool resultado = false;
            var lst_detalle = ListaMatriculaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var lst_modificar = lst_detalle.Where(q => q.FechaFacturacion == null).ToList();

            if (lst_modificar!=null)
            {
                foreach (var item in lst_modificar)
                {
                    var info_mecanismo = bus_mecanismo.GetInfo(IdEmpresa, item.IdMecanismo);
                    var info_termino_pago = bus_termino_pago.get_info((info_mecanismo == null ? "" : info_mecanismo.IdTerminoPago));

                    if (info_termino_pago != null && info_termino_pago.AplicaDescuentoNomina == true)
                    {
                        resultado = info_termino_pago.AplicaDescuentoNomina ?? false;
                        break;
                    }
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
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

        #region Acciones
        public ActionResult Consultar(int IdEmpresa = 0, int IdMatricula = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Matricula_Info model = bus_matricula.GetInfo(IdEmpresa, IdMatricula);
            model.IdEmpresa_rol = (model.IdEmpresa_rol == null ? Convert.ToInt32(SessionFixed.IdEmpresa) : model.IdEmpresa_rol);
            model.Validar = "N";

            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CambioPlantillaMatricula", "Index");
            if (model.BloquearMatricula == true)
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            model.lst_MatriculaRubro = bus_matricula_rubro.GetList(model.IdEmpresa, model.IdMatricula);
            ListaMatriculaRubro.set_list(model.lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

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
            model.IdEmpresa_rol = (model.IdEmpresa_rol==null ? Convert.ToInt32(SessionFixed.IdEmpresa): model.IdEmpresa_rol);
            model.Validar = "N";

            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "CambioPlantillaMatricula", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>();
            model.lst_MatriculaRubro = bus_matricula_rubro.GetList(model.IdEmpresa, model.IdMatricula);
            ListaMatriculaRubro.set_list(model.lst_MatriculaRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Matricula_Info model)
        {
            model.info_MatriculaCambios = new aca_MatriculaCambios_Info();
            aca_Matricula_Info info_matricula = bus_matricula.GetInfo(model.IdEmpresa, model.IdMatricula);

            model.info_MatriculaCambios = new aca_MatriculaCambios_Info {
                IdEmpresa = info_matricula.IdEmpresa,
                IdMatricula = info_matricula.IdMatricula,
                IdAnio = info_matricula.IdAnio,
                IdSede = info_matricula.IdSede,
                IdNivel = info_matricula.IdNivel,
                IdJornada = info_matricula.IdJornada,
                IdCurso = info_matricula.IdCurso,
                IdParalelo = info_matricula.IdParalelo,
                IdPlantilla = info_matricula.IdPlantilla,
                TipoCambio = "PLANTILLA",
                IdUsuarioCreacion = SessionFixed.IdUsuario
            };

            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_MatriculaRubro = ListaMatriculaRubro.get_list(model.IdTransaccionSession);

            foreach (var item in model.lst_MatriculaRubro)
            {
                item.IdSede = info_matricula.IdSede;
                item.IdNivel = info_matricula.IdNivel;
                item.IdJornada = info_matricula.IdJornada;
                item.IdCurso = info_matricula.IdCurso;
                item.IdParalelo = info_matricula.IdParalelo;
            }

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                model.lst_MatriculaRubro = ListaMatriculaRubro.get_list(model.IdTransaccionSession);
                cargar_combos();
                return View(model);
            }

            if (!bus_matricula.ModificarPlantillaDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdMatricula = model.IdMatricula, Exito = true });
        }
        #endregion
    }
}