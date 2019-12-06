using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using Core.Info.Reportes;
using System.Collections.Generic;
using Core.Bus.Reportes;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public ACA_001_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdMatricula = string.IsNullOrEmpty(p_IdMatricula.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMatricula.Value);

            ACA_001_Bus bus_rpt = new ACA_001_Bus();
            List<ACA_001_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdMatricula);

            this.DataSource = lst_rpt;
        }
    }
}
