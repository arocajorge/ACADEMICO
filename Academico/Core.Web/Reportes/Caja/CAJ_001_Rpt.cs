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
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdCaja = string.IsNullOrEmpty(p_IdCaja.Value.ToString()) ? 0:  Convert.ToInt32(p_IdCaja.Value);
            DateTime cm_fecha = string.IsNullOrEmpty(p_Fecha.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_Fecha.Value);
            List<CAJ_001_Info> Lista = bus_rpt.GetList(IdEmpresa, IdCaja, cm_fecha);


        }
    }
}
