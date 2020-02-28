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
using Core.Info.Helps;

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
            aca_Familia_Bus bus_familia = new aca_Familia_Bus();
            ACA_003_Bus bus_rpt = new ACA_003_Bus();

            List<ACA_003_Info> lst_rpt = new List<ACA_003_Info>();
            ACA_003_Info info = bus_rpt.GetInfo(IdEmpresa, IdAlumno);

            var CedulaRepLegal = "";
            var NombreRepLegal = "";
            var CedulaSeFactura = "";
            var CedulaAlumno = "";
            var NombreEstudiante = "";
            var CursoEstudiante = "";
            var NombreRepEconomico = "";
            var NombrePersonaFactura = "";
            var DescripcionPensiones = "";
            var DireccionRepLegal = "";
            var NacionalidadRepLegal = "";
            var TelefonoRepLegal = "";
            var SectorRepLegal = "";
            var BarrioRepLegal = "";
            var NomAnioLectivo = "";
            var NomAnioLectivoAnterior = "";
            var CorreoRepLegal = "";
            var CorreoSeFactura = "";

            if (info != null)
            {
                NombreRepLegal = info.NomRepresentante;
                CedulaRepLegal = info.CedulaRepresentante;
                CedulaSeFactura = info.CedulaSeFactura;
                CedulaAlumno = info.CedulaAlumno;
                NombreEstudiante = info.NomAlumno;
                CursoEstudiante = info.NomCurso;
                NombrePersonaFactura = info.NomSeFactura;
                NomAnioLectivoAnterior = info.DescripcionAnterior;
                NomAnioLectivo = info.DescripcionActual;
                DescripcionPensiones = info.DescripcionPensiones;
                DireccionRepLegal = info.Direccion;
                NacionalidadRepLegal = info.NacionalidadRepresentante;
                SectorRepLegal = info.SectorRepresentante;
                CorreoRepLegal = info.CorreoRepresentante;
                CorreoSeFactura = info.CorreoSeFactura;
                TelefonoRepLegal = info.CelularRepresentante;
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

            var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
            var NomSede = "";
            var NomRector = "";
            var CedulaRector = "";
            var DireccionSede = "";
            if (info_sede!=null)
            {
                NomSede = info_sede.NomSede;
                NomRector = info_sede.NombreRector;
                DireccionSede = info_sede.Direccion;
            }

            lbl_sede.Text = NomSede;

            lblSedeFirma.Text = NomSede;
            lblNombreRectorFirma.Text = NomRector;
            lblCedulaRectorFirma.Text = "CI. "+ CedulaRector;


            var info_rep = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.LEGAL.ToString());
            string RepLegalFirma = "";
            string CedRepLegalFirma = "";
            if (info_rep != null)
            {
                RepLegalFirma = info_rep.pe_nombreCompleto;
                CedRepLegalFirma = info_rep.pe_cedulaRuc;
            }

            lblNombreRepLegalFirma.Text = RepLegalFirma;
            lblCedulaRepLegalFirma.Text = "CI. "+ CedRepLegalFirma;


            txtContrato.Rtf = txtContrato.Rtf.Replace("[NombreRepLegal]", NombreRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NacionalidadRepLegal]", NacionalidadRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[CedulaRepLegal]", CedulaRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[DireccionRepLegal]", DireccionRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[SectorRepLegal]", SectorRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[BarrioRepLegal]", BarrioRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[TelefonoRepLegal]", TelefonoRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[CorreoRepLegal]", CorreoRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NombreEstudiante]", NombreEstudiante);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[CursoEstudiante]", CursoEstudiante);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NomAnioLectivo]", NomAnioLectivo);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[DescripcionPensiones]", DescripcionPensiones);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NomAnioLectivoAnterior]", NomAnioLectivoAnterior);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NombreRepLegal]", NombreRepLegal);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NombreRepEconomico]", NombreRepEconomico);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[NombrePersonaFactura]", NombrePersonaFactura);
            txtContrato.Rtf = txtContrato.Rtf.Replace("[DireccionSede]", DireccionSede);


            this.DataSource = lst_rpt;
        }
    }
}
