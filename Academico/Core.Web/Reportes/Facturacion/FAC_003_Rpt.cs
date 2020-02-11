using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Web.Reportes.Facturacion
{
    public partial class FAC_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_003_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdNota = p_IdNota.Value == null ? 0 : Convert.ToDecimal(p_IdNota.Value);

            FAC_003_Bus bus_rpt = new FAC_003_Bus();
            List<FAC_003_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            lst_rpt.ForEach(q => q.nomReporte = q.CreDeb.Trim() == "C" ? "NOTA DE CRÉDITO" : "NOTA DE DÉBITO");
            this.DataSource = lst_rpt;
        }

        private void Subreporte_apliaciones_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa_nt"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal_nt"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdBodega_nt"].Value = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdNota_nt"].Value = p_IdNota.Value == null ? 0 : Convert.ToDecimal(p_IdNota.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
