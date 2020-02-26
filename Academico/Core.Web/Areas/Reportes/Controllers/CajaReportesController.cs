using Core.Bus.General;
using Core.Bus.Reportes.Caja;
using Core.Web.Helps;
using Core.Web.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class CajaReportesController : Controller
    {
        
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";
        // GET: Reportes/CajaReportes
        public ActionResult CAJ_001( int IdTipoCbte=0, int IdCbteCble = 0)
        {
            CAJ_001_Rpt model = new CAJ_001_Rpt();
            CAJ_001_Bus bus_rpt = new CAJ_001_Bus();

            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CAJ_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            //model.p_Fecha.Value = cm_fecha;
            //model.p_IdCaja.Value = IdCaja;
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            model.RequestParameters = false;
            return View(model);
        }
    }
}