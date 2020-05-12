using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.Academico;
using Core.Bus.Reportes.Academico;
using System.Collections.Generic;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_014_Conducta_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_014_Conducta_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_014_Conducta_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
