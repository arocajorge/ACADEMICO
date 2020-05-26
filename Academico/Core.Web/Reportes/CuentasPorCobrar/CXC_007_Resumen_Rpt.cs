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
    public partial class CXC_007_Resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_007_Resumen_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_007_Resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime fechaCorte = p_fechaCorte.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaCorte.Value);

            CXC_007_Bus bus_rpt = new CXC_007_Bus();
            List<CXC_007_Info> lst_rpt = bus_rpt.Get_list(IdEmpresa, fechaCorte);
            this.DataSource = lst_rpt;
        }

        private void CXC_007_Resumen_Rpt_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {

        }
    }
}
