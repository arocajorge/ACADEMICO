using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.CuentasPorCobrar;
using Core.Bus.Reportes.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_002_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCobro = string.IsNullOrEmpty(p_IdCobro.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCobro.Value);

            CXC_002_Bus bus_rpt = new CXC_002_Bus();
            List<CXC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdCobro);
            this.DataSource = lst_rpt;
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCobro"].Value = p_IdCobro.Value == null ? 0 : Convert.ToDecimal(p_IdCobro.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
