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
    public partial class CXC_018_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public CXC_018_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_018_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);
            bool mostrarAnulados = p_mostrarAnulados.Value == null ? false : Convert.ToBoolean(p_mostrarAnulados.Value);

            CXC_018_Bus bus_rpt = new CXC_018_Bus();
            List<CXC_018_Info> lst_rpt = new List<CXC_018_Info>();

            lst_rpt = bus_rpt.get_list(IdEmpresa, IdAlumno, fecha_ini, fecha_fin, mostrarAnulados);
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }
    }
}
