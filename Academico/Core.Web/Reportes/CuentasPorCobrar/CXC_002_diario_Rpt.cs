using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_002_diario_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public CXC_002_diario_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_diario_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int cbr_IdEmpresa = p_cbr_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_cbr_IdEmpresa.Value);
            int cbr_IdSucursal = p_cbr_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_cbr_IdSucursal.Value);
            decimal cbr_IdCobro = p_cbr_IdCobro.Value == null ? 0 : Convert.ToDecimal(p_cbr_IdCobro.Value);

            CXC_002_diario_Bus bus_rpt = new CXC_002_diario_Bus();
            List<CXC_002_diario_Info> lst_rpt = bus_rpt.get_list(cbr_IdEmpresa, cbr_IdSucursal, cbr_IdCobro);
            this.DataSource = lst_rpt;
        }
    }
}
