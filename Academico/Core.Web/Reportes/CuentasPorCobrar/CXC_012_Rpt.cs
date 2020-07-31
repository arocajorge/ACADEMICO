using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.General;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_012_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        CXC_012_Bus bus_rpt = new CXC_012_Bus();
        public CXC_012_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_012_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaIni.Value);
                DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaFin.Value);
                List<CXC_012_Info> Lista = bus_rpt.Get_list(IdEmpresa, FechaIni, FechaFin);
                this.DataSource = Lista;

                tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
                var emp = bus_empresa.get_info(IdEmpresa);
                if (emp != null)
                {
                    lblDireccion.Text = emp.em_direccion;
                    if (emp.em_logo != null)
                    {
                        ImageConverter obj = new ImageConverter();
                        lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
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
