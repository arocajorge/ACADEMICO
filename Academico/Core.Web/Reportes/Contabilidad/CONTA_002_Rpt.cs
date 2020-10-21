using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using System.Collections.Generic;
using Core.Info.Reportes.Contabilidad;
using Core.Bus.Reportes.Contabilidad;

namespace Core.Web.Reportes.Contabilidad
{
    public partial class CONTA_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CONTA_002_Rpt()
        {
            InitializeComponent();
        }

        private void CONTA_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            string Tipo = string.IsNullOrEmpty(p_Tipo.Value.ToString()) ? "" : p_Tipo.Value.ToString();
            DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaIni.Value);
            DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaFin.Value);

            CONTA_002_Bus bus_rpt = new CONTA_002_Bus();
            List<CONTA_002_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, FechaIni, FechaFin, Tipo);
            this.DataSource = lst_rpt;

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
        }
    }
}
