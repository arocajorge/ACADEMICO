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
    public partial class FAC_005_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        FAC_005_Bus bus_rpt = new FAC_005_Bus();
        public FAC_005_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_005_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_empresa.Text = empresa;
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                DateTime FechaDesde = string.IsNullOrEmpty(p_FechaDesde.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaDesde.Value);
                DateTime FechaHasta = string.IsNullOrEmpty(p_FechaHasta.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaHasta.Value);
                int IdTipoNota = string.IsNullOrEmpty(p_IdTipoNota.Value.ToString()) ? 0 : Convert.ToInt32(p_IdTipoNota.Value);
                string CreDeb = string.IsNullOrEmpty(p_CreDeb.Value.ToString()) ? null : Convert.ToString(p_CreDeb.Value);
                string NaturalezaNota = string.IsNullOrEmpty(p_Naturaleza.Value.ToString()) ? null : Convert.ToString(p_Naturaleza.Value);
                
                List<FAC_005_Info> Lista = bus_rpt.GetList(IdEmpresa, FechaDesde, FechaHasta, IdTipoNota, CreDeb, NaturalezaNota);

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

                throw;
            }
        }
    }
}
