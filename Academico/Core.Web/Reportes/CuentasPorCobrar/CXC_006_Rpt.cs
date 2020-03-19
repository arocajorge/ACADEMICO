using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using System.Collections.Generic;
using Core.Info.Reportes.CuentasPorCobrar;
using Core.Bus.Reportes.CuentasPorCobrar;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_006_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public int IdEmpresa { get; set; }
        public int[] IntArray { get; set; }
        tb_empresa_Bus busEmpresa = new tb_empresa_Bus();
        public CXC_006_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_006_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;

                DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaIni.Value);
                DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaFin.Value);

                CXC_006_Bus bus_rpt = new CXC_006_Bus();
                List<CXC_006_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IntArray ?? new int[0], FechaIni,FechaFin);
                this.DataSource = lst_rpt;

                var empresa = busEmpresa.get_info(IdEmpresa);
                if (empresa != null)
                {
                    lbl_empresa.Text = empresa.em_nombre;
                    if (empresa.em_logo != null)
                    {
                        ImageConverter obj = new ImageConverter();
                        logo.Image = (Image)obj.ConvertFrom(empresa.em_logo);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
