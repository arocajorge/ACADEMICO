using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_013_Equivalencia_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_013_Equivalencia_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_013_Equivalencia_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);

            ACA_013_Bus bus_rpt = new ACA_013_Bus();
            List<ACA_013_EquivalenciaPromedio_Info> lst_rpt = new List<ACA_013_EquivalenciaPromedio_Info>();
            lst_rpt = bus_rpt.GetList_Equivalencia(IdEmpresa, IdAnio);

            this.DataSource = lst_rpt;
        }
    }
}
