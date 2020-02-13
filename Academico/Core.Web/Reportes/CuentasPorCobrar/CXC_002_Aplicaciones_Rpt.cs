using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Core.Bus.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;

namespace Core.Web.Reportes.CuentasPorCobrar
{

    public partial class CXC_002_Aplicaciones_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        #region Variables
        List<CXC_002_Aplicaciones_Info> Lista = new List<CXC_002_Aplicaciones_Info>();
        CXC_002_Aplicaciones_Bus busRpt = new CXC_002_Aplicaciones_Bus();
        #endregion

        public CXC_002_Aplicaciones_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_002_Aplicaciones_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
                decimal IdCobro = string.IsNullOrEmpty(p_IdCobro.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCobro.Value);

                Lista = busRpt.get_list(IdEmpresa, IdSucursal, IdCobro);
                this.DataSource = Lista;
            }
            catch (Exception)
            {
                
            }
        }
    }
}
