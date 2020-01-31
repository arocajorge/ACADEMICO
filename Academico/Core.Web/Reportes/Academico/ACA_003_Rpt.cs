using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using System.Collections.Generic;
using Core.Info.Reportes.Academico;
using Core.Bus.General;
using Core.Bus.Academico;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_003_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);

            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            ACA_003_Bus bus_rpt = new ACA_003_Bus();
            List<ACA_003_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdAlumno);

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

            var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
            lbl_sede.Text = info_sede.NomSede;

            this.DataSource = lst_rpt;
        }
    }
}
