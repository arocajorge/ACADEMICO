using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Facturacion;
using Core.Bus.General;
using Core.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Web.Reportes.Facturacion
{
    public partial class FAC_007_Resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        FAC_007_Bus bus_rpt = new FAC_007_Bus();
        public FAC_007_Resumen_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_007_Resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                DateTime FechaDesde = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaIni.Value);
                DateTime FechaHasta = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaFin.Value);
                int IdEmpresa_rol = string.IsNullOrEmpty(p_IdEmpresa_rol.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa_rol.Value);

                List<FAC_007_Info> Lista = bus_rpt.GetList(IdEmpresa, FechaDesde, FechaHasta, IdEmpresa_rol);

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

                xrPivotGrid1.DataSource = Lista;
                xrPivotGrid2.DataSource = Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
