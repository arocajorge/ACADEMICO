using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraPrinting;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_003_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public string[] StringArray { get; set; }
        public CXC_003_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_003_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            DateTime FechaIni = string.IsNullOrEmpty(p_FechaIni.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_FechaIni.Value);
            DateTime FechaFin = string.IsNullOrEmpty(p_FechaFin.Value.ToString()) ? DateTime.Now : Convert.ToDateTime(p_FechaFin.Value);

            CXC_003_Bus bus_rpt = new CXC_003_Bus();
            List<CXC_003_Info> lst_rpt = new List<CXC_003_Info>();
            if (StringArray != null)
            {
                for (int i = 0; i < StringArray.Count();  i++)
                {
                    lst_rpt.AddRange(bus_rpt.get_list(IdEmpresa, FechaIni, FechaFin, StringArray[i]));
                }
            }

            this.DataSource = lst_rpt;
        }

        private void xrPivotGrid1_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            try
            {
                if (e.Field != null && (e.Field.FieldName == "cr_fecha" || e.Field.FieldName == "GrupoTipoCobro") && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea)
                {
                    LabelBrick lb = new LabelBrick();
                    lb.Padding = new PaddingInfo(2, 2, 5, 2, GraphicsUnit.Pixel);
                    lb.Angle = 90;
                    lb.Text = e.Field.FieldName == "cr_fecha" ? Convert.ToDateTime(e.Text.Replace("Total","")).Date.ToString("dd/MM/yyyy") : e.Text;
                    lb.Rect = GraphicsUnitConverter.DocToPixel(e.Brick.Rect);
                    e.Brick = lb;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
