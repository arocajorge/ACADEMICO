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
    public partial class ACA_001_Padre_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_001_Padre_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_001_Padre_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);

            ACA_001_Bus bus_rpt = new ACA_001_Bus();
            List<ACA_001_Info> lst_rpt = new List<ACA_001_Info>();
            //List<ACA_001_Info> lst_rpt = bus_rpt.GetListPadres(IdEmpresa, IdAlumno);

            this.DataSource = lst_rpt;
        }
    }
}
