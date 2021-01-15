using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System.Collections.Generic;
using Core.Bus.General;
using System.Linq;

namespace Core.Web.Reportes.Facturacion
{
    public partial class FAC_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_002_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            //lbl_usuario.Text = usuario;
            //lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = string.IsNullOrEmpty(p_IdBodega.Value.ToString()) ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdCbteVta = string.IsNullOrEmpty(p_IdCbteVta.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCbteVta.Value);

            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            DateTime FechaIni = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime FechaFin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_fin.Value);

            FAC_002_Bus bus_rpt = new FAC_002_Bus();
            List<FAC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, IdAnio, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo, FechaIni, FechaFin);
            this.DataSource = lst_rpt;


            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = empresa.em_nombre;
            lbl_direccion.Text = empresa.em_direccion;
            lbl_telefono.Text = empresa.em_telefonos;
            lbl_correo.Text = "cobranzas@liceocristiano.edu.ec";
            lbl_ruc.Text = empresa.em_ruc;

            if (empresa != null && empresa.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(empresa.em_logo);
            }

            var Primero = lst_rpt.FirstOrDefault();
            if (Primero != null)
            {
                string Cadena = lblReemplaza.Text;
                Cadena = Cadena.Replace("{0}", Primero.cli_cedulaRuc).Replace("{1}", Primero.cli_Nombre);
                lblReemplaza.Text = Cadena;
            }

            FAC_002_Pendiente_Pago_Bus bus_cxc = new FAC_002_Pendiente_Pago_Bus();
            var IdAlumnoRpt = (lst_rpt.Count==0 ? 0 : lst_rpt.FirstOrDefault().IdAlumno);
            var lst_cxc = bus_cxc.get_list(IdEmpresa, IdSucursal, Convert.ToDecimal(IdAlumnoRpt));

            if (lst_cxc.Count>0 && lst_cxc.Sum(q=>q.Saldo) > 0)
            {
                xrSubreport1.Visible = true;
                SinDeuda.Visible = false;
            }
            else
            {
                xrSubreport1.Visible = false;
                SinDeuda.Visible = true;
            }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToDecimal(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdAlumno"].Value = IdAlumno.Value == null ? 0 : Convert.ToDecimal(IdAlumno.Text);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }

        private void xrSubreport1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToDecimal(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdAlumno"].Value = IdAlumno.Value == null ? 0 : Convert.ToDecimal(IdAlumno.Text);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
