using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.General;
using Core.Info.Helps;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_016_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_016_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_016_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdPagare = string.IsNullOrEmpty(p_IdPagare.Value.ToString()) ? 0 : Convert.ToInt32(p_IdPagare.Value);

            CXC_016_Bus bus_rpt = new CXC_016_Bus();
            List<CXC_016_Info> lst_rpt = new List<CXC_016_Info>();
            lst_rpt = bus_rpt.get_list(IdEmpresa, IdPagare);

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                if (emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }

            this.DataSource = lst_rpt;
        }
    }
}
