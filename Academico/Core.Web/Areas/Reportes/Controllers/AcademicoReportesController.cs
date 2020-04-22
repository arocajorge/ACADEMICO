﻿using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using Core.Web.Reportes.Academico;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class AcademicoReportesController : Controller
    {
        #region Variables
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Academico.repx";
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_MatriculaCalificacion_Bus bus_calificacion = new aca_MatriculaCalificacion_Bus();
        aca_ReporteCalificacion_Combos_List Lista_CombosCalificaciones = new aca_ReporteCalificacion_Combos_List();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_Rubro_Bus bus_rubro = new aca_Rubro_Bus();
        #endregion

        #region Combos
        public ActionResult Cmb_Alumno()
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
        public ActionResult CmbSede()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_CmbSede", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }
        public ActionResult CmbNivel()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_CmbNivel", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult CmbCurso()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_CmbCurso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }
        public ActionResult CmbJornada()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_CmbJornada", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
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
        #endregion

        public ActionResult ACA_001(int IdEmpresa = 0, decimal IdAlumno = 0, int IdAnio=0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            var info_anio = new aca_AnioLectivo_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            if (IdAnio == 0)
            {
                info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            }

            model.IdAnio = (IdAnio == 0 ? info_anio.IdAnio : IdAnio);
            model. IdAlumno = IdAlumno;

            ACA_001_Rpt Report = new ACA_001_Rpt();

            #region Cargo diseño desde base
            var report = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_001");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                Report.LoadLayout(RootReporte);
            }
            #endregion

            Report.p_IdEmpresa.Value = model.IdEmpresa;
            Report.p_IdSede.Value = model.IdSede;
            Report.p_IdAnio.Value = model.IdAnio;
            Report.p_IdAlumno.Value = IdAlumno;
            Report.usuario = SessionFixed.IdUsuario;
            Report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = Report;

            ACA_002_Rpt ReportSolicitud = new ACA_002_Rpt();
            #region Cargo diseño desde base
            var reportSol = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_002");
            if (reportSol != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportSol.ReporteDisenio);
                ReportSolicitud.LoadLayout(RootReporte);
            }
            #endregion
            ReportSolicitud.p_IdEmpresa.Value = model.IdEmpresa;
            ReportSolicitud.p_IdAlumno.Value = model.IdAlumno;
            ReportSolicitud.p_IdAnio.Value = model.IdAnio;
            ReportSolicitud.usuario = SessionFixed.IdUsuario;
            ReportSolicitud.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportSolicitud = ReportSolicitud;

            ACA_003_Rpt ReportContrato = new ACA_003_Rpt();
            #region Cargo diseño desde base
            var reportCont = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_003");
            if (reportCont != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportCont.ReporteDisenio);
                ReportContrato.LoadLayout(RootReporte);
            }
            #endregion
            ReportContrato.p_IdEmpresa.Value = model.IdEmpresa;
            ReportContrato.p_IdAlumno.Value = model.IdAlumno;
            ReportContrato.p_IdSede.Value = model.IdSede;
            ReportContrato.usuario = SessionFixed.IdUsuario;
            ReportContrato.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportContrato = ReportContrato;

            ACA_005_Rpt ReportSocioEconomica = new ACA_005_Rpt();
            #region Cargo diseño desde base
            var reportSocioEco = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_005");
            if (reportSocioEco != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportSocioEco.ReporteDisenio);
                ReportSocioEconomica.LoadLayout(RootReporte);
            }
            #endregion
            ReportSocioEconomica.p_IdEmpresa.Value = model.IdEmpresa;
            ReportSocioEconomica.p_IdAlumno.Value = model.IdAlumno;
            ReportSocioEconomica.p_IdSede.Value = model.IdSede;
            ReportSocioEconomica.usuario = SessionFixed.IdUsuario;
            ReportSocioEconomica.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportSocioEconomica = ReportSocioEconomica;

            return View(model);
        }

        [HttpPost]
        public ActionResult ACA_001(cl_filtros_Info model)
        {
            ACA_001_Rpt Report = new ACA_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var report = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_001");
            if (report != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, report.ReporteDisenio);
                Report.LoadLayout(RootReporte);
            }
            #endregion
            Report.p_IdEmpresa.Value = model.IdEmpresa;
            Report.p_IdAlumno.Value = model.IdAlumno;
            Report.p_IdAnio.Value = model.IdAnio;
            Report.p_IdSede.Value = model.IdSede;
            Report.usuario = SessionFixed.IdUsuario;
            Report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = Report;

            ACA_002_Rpt ReportSolicitud = new ACA_002_Rpt();
            #region Cargo diseño desde base
            var reportSol = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_002");
            if (reportSol != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportSol.ReporteDisenio);
                ReportSolicitud.LoadLayout(RootReporte);
            }
            #endregion
            ReportSolicitud.p_IdEmpresa.Value = model.IdEmpresa;
            ReportSolicitud.p_IdAlumno.Value = model.IdAlumno;
            ReportSolicitud.p_IdAnio.Value = model.IdAnio;
            ReportSolicitud.usuario = SessionFixed.IdUsuario;
            ReportSolicitud.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportSolicitud = ReportSolicitud;

            ACA_003_Rpt ReportContrato = new ACA_003_Rpt();
            #region Cargo diseño desde base
            var reportCont = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_003");
            if (reportCont != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportCont.ReporteDisenio);
                ReportContrato.LoadLayout(RootReporte);
            }
            #endregion
            ReportContrato.p_IdEmpresa.Value = model.IdEmpresa;
            ReportContrato.p_IdAlumno.Value = model.IdAlumno;
            ReportContrato.p_IdSede.Value = model.IdSede;
            ReportContrato.usuario = SessionFixed.IdUsuario;
            ReportContrato.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportContrato = ReportContrato;


            ACA_005_Rpt ReportSocioEconomica = new ACA_005_Rpt();
            #region Cargo diseño desde base
            var reportSocioEco = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_005");
            if (reportSocioEco != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportSocioEco.ReporteDisenio);
                ReportSocioEconomica.LoadLayout(RootReporte);
            }
            #endregion
            ReportSocioEconomica.p_IdEmpresa.Value = model.IdEmpresa;
            ReportSocioEconomica.p_IdAlumno.Value = model.IdAlumno;
            ReportSocioEconomica.p_IdSede.Value = model.IdSede;
            ReportSocioEconomica.usuario = SessionFixed.IdUsuario;
            ReportSocioEconomica.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportSocioEconomica = ReportSocioEconomica;

            return View(model);
        }

        public ActionResult ACA_002(int IdEmpresa = 0, decimal IdAlumno = 0, int IdAnio = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = new aca_AnioLectivo_Info();

            if (IdAnio == 0)
            {
                info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            }

            model.IdAnio = (IdAnio == 0 ? info_anio.IdAnio : IdAnio);
            model.IdAlumno = IdAlumno;
            ACA_002_Rpt report = new ACA_002_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdAlumno.Value = IdAlumno;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult ACA_002(cl_filtros_Info model)
        {
            ACA_002_Rpt report = new ACA_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_IdAnio.Value = model.IdAnio;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ACA_003(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.IdAlumno = IdAlumno;

            ACA_003_Rpt report = new ACA_003_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAlumno.Value = IdAlumno;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult ACA_003(cl_filtros_Info model)
        {
            ACA_003_Rpt report = new ACA_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_IdSede.Value = model.IdSede;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ACA_004(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = new aca_AnioLectivo_Info();
            if (IdAnio == 0)
            {
                info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            }

            model.IdAnio = (IdAnio == 0 ? info_anio.IdAnio : IdAnio);
            model.IdNivel = IdNivel;
            model.IdJornada = IdJornada;
            model.IdCurso = IdCurso;
            

            ACA_004_Rpt report = new ACA_004_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_004(cl_filtros_Info model )
        {
            ACA_004_Rpt report = new ACA_004_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            return View(model);
        }

        public ActionResult ACA_005(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.IdAlumno = IdAlumno;

            ACA_005_Rpt report = new ACA_005_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAlumno.Value = IdAlumno;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        [HttpPost]
        public ActionResult ACA_005(cl_filtros_Info model)
        {
            ACA_005_Rpt report = new ACA_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "ACA_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_IdSede.Value = model.IdSede;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }

        public ActionResult ACA_006()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.fecha_ini = DateTime.Now.AddMonths(-1);
            model.fecha_fin = DateTime.Now;

            ACA_006_Rpt report = new ACA_006_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;

            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_006(cl_filtros_Info model)
        {
            ACA_006_Rpt report = new ACA_006_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;

            ViewBag.Report = report;

            return View(model);
        }

        #region ACA_007
        public ActionResult ACA_007()
        {
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa),0);
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            model.fecha_ini = DateTime.Now.AddMonths(-1);
            model.fecha_fin = DateTime.Now;
            model.IdAnio = info_anio.IdAnio;
            model.IdJornada = 0;
            model.IdNivel = 0;
            model.IdCurso = 0;
            model.IdParalelo = 0;

            ACA_007_Rpt report = new ACA_007_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;

            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_007(cl_filtros_Info model)
        {
            ACA_007_Rpt report = new ACA_007_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;

            ViewBag.Report = report;

            return View(model);
        }
        #endregion

        public ActionResult ACA_008(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdNivel = 0, int IdJornada = 0, int IdCurso = 0, int IdParalelo = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.IdNivel = IdNivel;
            model.IdJornada = IdJornada;
            model.IdCurso = IdCurso;
            model.IdParalelo = IdParalelo;
            model.mostrar_observacion_completa = true;

            ACA_008_Rpt report = new ACA_008_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_MostrarPlantilla.Value = model.mostrar_observacion_completa;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            ACA_008_Resumen_Rpt ReportResumen = new ACA_008_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportCont = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_008");
            if (reportCont != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportCont.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_IdAnio.Value = model.IdAnio;
            ReportResumen.p_IdSede.Value = model.IdSede;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_008(cl_filtros_Info model)
        {
            ACA_008_Rpt report = new ACA_008_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_MostrarPlantilla.Value = model.mostrar_observacion_completa;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            ACA_008_Resumen_Rpt ReportResumen = new ACA_008_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportCont = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_008");
            if (reportCont != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportCont.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_IdAnio.Value = model.IdAnio;
            ReportResumen.p_IdSede.Value = model.IdSede;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;

            return View(model);
        }

        public ActionResult ACA_009(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.fecha_ini = DateTime.Now.AddMonths(-1);
            model.fecha_fin = DateTime.Now;
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            model.IdAlumno = IdAlumno;

            ACA_009_Rpt report = new ACA_009_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_IdAnio.Value = model.IdAnio;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_009(cl_filtros_Info model)
        {

            ACA_009_Rpt report = new ACA_009_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_fecha_fin.Value = model.fecha_fin;
            report.p_fecha_ini.Value = model.fecha_ini;
            report.p_IdAnio.Value = model.IdAnio;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }

        #region Combos
        public ActionResult ComboBoxPartial_Anio_Cal()
        {
            return PartialView("_ComboBoxPartial_Anio_Cal", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        public ActionResult ComboBoxPartial_Sede_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede_Cal", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }
        public ActionResult ComboBoxPartial_Nivel_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel_Cal", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        public ActionResult ComboBoxPartial_Jornada_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada_Cal", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Curso_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Curso_Cal", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }

        public ActionResult ComboBoxPartial_Paralelo_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdCurso"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;
            return PartialView("_ComboBoxPartial_Paralelo_Cal", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso });
        }

        public ActionResult ComboBoxPartial_Materia_Cal()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            int IdCurso = (Request.Params["IdCurso"] != null) ? int.Parse(Request.Params["IdCurso"]) : -1;
            var IdParalelo = (Request.Params["IdParalelo"] != null) ? int.Parse(Request.Params["IdParalelo"]) : -1;
            return PartialView("_ComboBoxPartial_Materia_Cal", new aca_AnioLectivo_Paralelo_Profesor_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada, IdCurso = IdCurso, IdParalelo = IdParalelo });
        }
        #endregion
        #region Combos Funciones Agrupados
        public List<aca_AnioLectivo_Info> CargarAnio(int IdEmpresa = 0)
        {
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.IdEmpresa == IdEmpresa).ToList();
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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
            List<aca_MatriculaCalificacion_Info> lst_combos = Lista_CombosCalificaciones.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q =>
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

        #region ACA_010
        private void cargar_combos_ACA_010(aca_MatriculaCalificacionParcial_Info model)
        {
            var lst_parcial = new List<aca_AnioLectivoParcial_Info>();
            var lst_quim1 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1), DateTime.Now.Date);
            var lst_quim2 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2), DateTime.Now.Date);
            lst_parcial.AddRange(lst_quim1);
            lst_parcial.AddRange(lst_quim2);

            ViewBag.lst_parcial = lst_parcial;
        }
        public ActionResult ACA_010(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            aca_MatriculaCalificacionParcial_Info model = new aca_MatriculaCalificacionParcial_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            Lista_CombosCalificaciones.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ACA_010_Rpt report = new ACA_010_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_IdMateria.Value = model.IdMateria;
            report.p_IdCatalogoParcial.Value = model.IdCatalogoParcial;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            cargar_combos_ACA_010(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_010(aca_MatriculaCalificacionParcial_Info model)
        {
            ACA_010_Rpt report = new ACA_010_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_IdMateria.Value = model.IdMateria;
            report.p_IdCatalogoParcial.Value = model.IdCatalogoParcial;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            cargar_combos_ACA_010(model);
            return View(model);
        }

        #endregion

        #region ACA_011
        private void cargar_combos_ACA_011(aca_MatriculaCalificacion_Info model)
        {
            var lst_parcial = new List<aca_AnioLectivoParcial_Info>();
            var lst_quim1 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1), DateTime.Now.Date);
            var lst_quim2 = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2), DateTime.Now.Date);
            lst_parcial.AddRange(lst_quim1);
            lst_parcial.AddRange(lst_quim2);

            ViewBag.lst_parcial = lst_parcial;
        }

        public ActionResult ACA_011()
        {
            aca_MatriculaCalificacion_Info model = new aca_MatriculaCalificacion_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);

            string IdUsuario = SessionFixed.IdUsuario;
            bool EsSuperAdmin = Convert.ToBoolean(SessionFixed.EsSuperAdmin);
            var info_profesor = bus_profesor.GetInfo_x_Usuario(model.IdEmpresa, IdUsuario);
            var IdProfesor = (info_profesor == null ? 0 : info_profesor.IdProfesor);
            List<aca_MatriculaCalificacion_Info> lst_combos = bus_calificacion.GetList_Combos(model.IdEmpresa, IdProfesor, EsSuperAdmin);
            Lista_CombosCalificaciones.set_list(lst_combos, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ACA_011_Rpt report = new ACA_011_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            cargar_combos_ACA_011(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_011(aca_MatriculaCalificacion_Info model)
        {
            ACA_011_Rpt report = new ACA_011_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_011");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            cargar_combos_ACA_011(model);
            return View(model);
        }

        #endregion

        #region ACA_012
        private void cargar_combos_ACA_012(cl_filtros_Info model)
        {
            var lst_rubro = bus_rubro.GetList(model.IdEmpresa, false);
            ViewBag.lst_rubro = lst_rubro;
        }
        public ActionResult ACA_012()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = new aca_AnioLectivo_Info();
            info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = info_anio == null ? 0 : info_anio.IdAnio;
            model.mostrarAnulados = true;
            model.IdRubro = 3;
            ACA_012_Rpt report = new ACA_012_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdRubro.Value = model.IdRubro;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.p_QuebrarPorParalelo.Value = model.mostrarAnulados;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_combos_ACA_012(model);
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult ACA_012(cl_filtros_Info model)
        {
            ACA_012_Rpt report = new ACA_012_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "ACA_012");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdRubro.Value = model.IdRubro;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.p_QuebrarPorParalelo.Value = model.mostrarAnulados;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_combos_ACA_012(model);
            ViewBag.Report = report;

            return View(model);
        }
        #endregion
    }

    public class aca_ReporteCalificacion_Combos_List
    {
        string Variable = "aca_ReporteCalificacion_Combos_Info";
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
}