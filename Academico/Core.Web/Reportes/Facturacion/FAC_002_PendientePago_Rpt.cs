using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Web.Reportes.Facturacion
{
    public partial class FAC_002_PendientePago_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public FAC_002_PendientePago_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_002_PendientePago_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdAlumno = p_IdAlumno.Value == null ? 0 : Convert.ToDecimal(p_IdAlumno.Value);

            FAC_002_Pendiente_Pago_Bus bus_rpt = new FAC_002_Pendiente_Pago_Bus();
            List<FAC_002_Pendiente_Pago_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdAlumno);

            this.DataSource = lst_rpt;

        }
    }
}
