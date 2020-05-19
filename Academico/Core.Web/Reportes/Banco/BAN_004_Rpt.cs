using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.Banco;
using Core.Bus.Reportes.Banco;
using System.Collections.Generic;
using Core.Bus.General;

namespace Core.Web.Reportes.Banco
{
    public partial class BAN_004_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public BAN_004_Rpt()
        {
            InitializeComponent();
        }

        private void BAN_004_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdArchivo = p_IdArchivo.Value == null ? 0 : Convert.ToInt32(p_IdArchivo.Value);

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

            BAN_004_Bus bus_rpt = new BAN_004_Bus();
            List<BAN_004_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdArchivo);
            this.DataSource = lst_rpt;
        }
    }
}
