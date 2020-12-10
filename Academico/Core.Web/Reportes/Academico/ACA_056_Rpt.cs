using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Academico;
using Core.Bus.General;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_056_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        
        ACA_056_Bus bus_rpt = new ACA_056_Bus();
        public ACA_056_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_056_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
                List<ACA_056_Info> Lista = bus_rpt.GetList(IdEmpresa);
                this.DataSource = Lista;

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

                aca_Sede_Bus bus_sede = new aca_Sede_Bus();
                var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
                var NomSede = "";
                if (info_sede != null)
                {
                    NomSede = info_sede.NomSede;

                }

                lbl_sede.Text = NomSede;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
