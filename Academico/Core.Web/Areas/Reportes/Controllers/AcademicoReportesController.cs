using Core.Bus.Academico;
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
        #endregion

        public ActionResult ACA_001(int IdEmpresa = 0, decimal IdAlumno = 0, int IdAnio=0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);
            
            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
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
            Report.p_IdAnio.Value = IdAnio;
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

            return View(model);
        }

        public ActionResult ACA_002(int IdEmpresa = 0, decimal IdAlumno = 0, int IdAnio = 0)
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
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
            report.p_IdAnio.Value = IdAnio;
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
    }
}