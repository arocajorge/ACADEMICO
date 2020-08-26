using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Info.Reportes.Facturacion;
using Core.Bus.General;

namespace Core.Web.Reportes.Facturacion
{
    public partial class FAC_009_Resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        FAC_009_Bus bus_rpt = new FAC_009_Bus();
        public FAC_009_Resumen_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_009_Resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdTipoNota = string.IsNullOrEmpty(p_IdTipoNota.Value.ToString()) ? 0 : Convert.ToInt32(p_IdTipoNota.Value);
                DateTime FechaDesde = string.IsNullOrEmpty(p_FechaDesde.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaDesde.Value);
                DateTime FechaHasta = string.IsNullOrEmpty(p_FechaHasta.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaHasta.Value);


                List<FAC_009_Info> Lista = bus_rpt.GetList(IdEmpresa, IdTipoNota, FechaDesde, FechaHasta);

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

                this.DataSource = Lista;
            }
            catch (Exception)
            {

            }
        }
    }
}
