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
    public partial class ACA_046_Promedios_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_046_Promedios_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_046_Promedios_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);

            ACA_046_Promedios_Bus bus_rpt = new ACA_046_Promedios_Bus();
            List<ACA_046_Promedios_Info> lst_rpt = new List<ACA_046_Promedios_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAlumno);

            this.DataSource = lst_rpt;
        }
    }
}
