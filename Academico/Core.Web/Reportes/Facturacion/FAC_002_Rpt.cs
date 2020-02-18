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

            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdBodega = p_IdBodega.Value == null ? 0 : Convert.ToInt32(p_IdBodega.Value);
            decimal IdCbteVta = p_IdCbteVta.Value == null ? 0 : Convert.ToDecimal(p_IdCbteVta.Value);

            FAC_002_Bus bus_rpt = new FAC_002_Bus();
            List<FAC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            this.DataSource = lst_rpt;


            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var empresa = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = empresa.em_nombre;
            lbl_direccion.Text = empresa.em_direccion;
            lbl_telefono.Text = empresa.em_telefonos;
            lbl_correo.Text = empresa.em_Email;
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
        }
    }
}
