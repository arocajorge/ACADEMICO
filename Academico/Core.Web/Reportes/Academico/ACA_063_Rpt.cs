using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.General;
using Core.Bus.Academico;
using Core.Web.Helps;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_063_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_063_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_063_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);

            ACA_063_Bus bus_rpt = new ACA_063_Bus();
            List<ACA_063_Info> lst_rpt = new List<ACA_063_Info>();
            ACA_063_Info info = bus_rpt.GetInfo(IdEmpresa, IdAlumno, IdAnio);
            var RepLegal = "";
            var Estudiante = "";
            var Curso = "";
            var Jornada = "";
            var Paralelo = "";
            var Total = "";
            var NomPlantillaTipo = "";
            var Descripcion = "";
            var FechaActual = "";

            if (info!=null)
            {
                RepLegal = info.NombreRep;
                Estudiante = info.NombreAlumno;
                Curso = info.NomCurso;
                Jornada = info.NomJornada;
                Paralelo = info.NomParalelo;
                NomPlantillaTipo = info.NomPlantillaTipo;
                Total = info.Total.ToString();
                Descripcion = info.Descripcion.ToString();
                FechaActual = info.FechaActual.ToString();

                lst_rpt.Add(info);
            }

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

            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[RepLegal]", RepLegal);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Estudiante]", Estudiante);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Curso]", Curso);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Jornada]", Jornada);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Paralelo]", Paralelo);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[NomPlantillaTipo]", NomPlantillaTipo);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Total]", Total);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[FechaActual]", FechaActual);
            txtSolicitud.Rtf = txtSolicitud.Rtf.Replace("[Descripcion]", Descripcion);

            this.DataSource = lst_rpt;

            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            var sede = bus_sede.GetInfo(IdEmpresa, Convert.ToInt32(IdSede));
            if (sede != null)
            {
                Rector.Text = sede.NombreRector;
            }
        }
    }
}
