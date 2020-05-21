using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;
using Core.Bus.General;
using Core.Bus.Academico;
using Core.Info.Helps;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_004_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        ACA_004_Bus bus_rpt = new ACA_004_Bus();
        public ACA_004_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_004_Rpt_Before_Print(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
                int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
                int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
                int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
                int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
                int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);

                List<ACA_004_Info> Lista = bus_rpt.Getlist(IdEmpresa,IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
                this.DataSource = Lista;
            }
            catch (Exception EX)
            {
                throw;
            }
        }
    }
}
