using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

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

    }
}
