using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.General;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_002_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_002_Rpt_AfterPrint(object sender, EventArgs e)
        {

        }

        private void ACA_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);

            ACA_002_Bus bus_rpt = new ACA_002_Bus();
            List<ACA_002_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdAlumno, IdAnio);

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                //lblDireccion.Text = emp.em_direccion;
                //lblTelefono.Text = string.IsNullOrEmpty(emp.em_telefonos) ? "" : "Tel. " + emp.em_telefonos;
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
