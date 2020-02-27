using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Caja;
using Core.Info.Reportes.Caja;
using System.Collections.Generic;

namespace Core.Web.Reportes.Caja
{
    public partial class CAJ_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        CAJ_001_Bus bus_rpt = new CAJ_001_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CAJ_001_Rpt()
        {
            InitializeComponent();
        }

        private void CAJ_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdTipoCbte = string.IsNullOrEmpty(p_IdTipoCbte.Value.ToString()) ? 0 : Convert.ToInt32(p_IdTipoCbte.Value);
            decimal IdCbteCble = string.IsNullOrEmpty(p_IdCbteCble.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCbteCble.Value);

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            List<CAJ_001_Info> Lista = bus_rpt.GetList(IdEmpresa, IdTipoCbte, IdCbteCble);

            this.DataSource = Lista;
        }
    }
}
