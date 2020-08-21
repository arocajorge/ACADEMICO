using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_013_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            int IdCatalogoParcial = string.IsNullOrEmpty(p_IdCatalogoParcial.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoParcial.Value);

            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                if (emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }

            ACA_013_Bus bus_rpt = new ACA_013_Bus();
            List<ACA_013_Info> lst_rpt = new List<ACA_013_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial);

            this.DataSource = lst_rpt;
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdAnio"].Value = p_IdAnio.Value == null ? 0 : Convert.ToInt32(p_IdAnio.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

        private void Asistencia_Subreporte_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdMatricula"].Value = IdMatricula.Value == null ? 0 : Convert.ToDecimal(IdMatricula.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCatalogoParcial"].Value = p_IdCatalogoParcial.Value == null ? 0 : Convert.ToInt32(p_IdCatalogoParcial.Value);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
