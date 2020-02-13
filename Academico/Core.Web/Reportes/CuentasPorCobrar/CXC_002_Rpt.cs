using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.CuentasPorCobrar;
using Core.Bus.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.General;
using Core.Bus.CuentasPorCobrar;
using System.Linq;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        tb_empresa_Bus busEmpresa = new tb_empresa_Bus();
        cxc_cobro_Bus busCobro = new cxc_cobro_Bus();
        public CXC_002_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCobro = string.IsNullOrEmpty(p_IdCobro.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCobro.Value);

            CXC_002_Bus bus_rpt = new CXC_002_Bus();
            List<CXC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdCobro);
            this.DataSource = lst_rpt;

            var empresa = busEmpresa.get_info(IdEmpresa);
            if (empresa != null)
            {
                lbl_empresa.Text = empresa.em_nombre;
                lblDireccion.Text = empresa.em_direccion;
                if (empresa.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    logo.Image = (Image)obj.ConvertFrom(empresa.em_logo);
                }
            }
            var Primero = lst_rpt.FirstOrDefault();
            lblSaldo.Text = busCobro.GetSaldoAlumno(IdEmpresa,(Primero == null ? 0 : Primero.IdAlumno ?? 0)).ToString("c2");
            if (Primero != null)
                lblReemplaza.Text.Replace("{0}", Primero.CedulaCliente).Replace("{1}", Primero.NomCliente);
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdSucursal"].Value = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdCobro"].Value = p_IdCobro.Value == null ? 0 : Convert.ToDecimal(p_IdCobro.Value);
            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
