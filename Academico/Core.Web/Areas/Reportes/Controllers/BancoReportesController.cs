using Core.Bus.General;
using Core.Web.Helps;
using Core.Web.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class BancoReportesController : Controller
    {
        #region Variables
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Cheque.repx";
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        #endregion
        // GET: Reportes/BancoReportes
        public ActionResult BAN_001(int IdTipoCbte = 0, decimal IdCbteCble = 0)
        {
            BAN_001_Rpt model = new BAN_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_002(int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            BAN_002_Rpt model = new BAN_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipocbte.Value = IdTipocbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        public ActionResult BAN_003(int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            BAN_003_Rpt model = new BAN_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipocbte.Value = IdTipocbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }

        #region BAN_004
        public ActionResult BAN_004(int IdEmpresa = 0, decimal IdArchivo = 0)
        {
            BAN_004_Rpt model = new BAN_004_Rpt();
            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "BAN_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdArchivo.Value = IdArchivo;
            model.usuario = SessionFixed.IdUsuario.ToString();
            model.empresa = SessionFixed.NomEmpresa.ToString();
            return View(model);
        }
        #endregion
    }
}