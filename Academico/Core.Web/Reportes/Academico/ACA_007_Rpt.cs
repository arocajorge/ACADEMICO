using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;
using Core.Bus.Academico;
using Core.Bus.General;
using DevExpress.XtraPrinting;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_007_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        ACA_007_Bus bus_rpt = new ACA_007_Bus();
        public ACA_007_Rpt()
        {
            InitializeComponent();
        }
        private void ACA_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {

                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;

                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
                DateTime fecha_ini = string.IsNullOrEmpty(p_fecha_ini.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_ini.Value);
                DateTime fecha_fin = string.IsNullOrEmpty(p_fecha_fin.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_fecha_fin.Value);
                aca_Sede_Bus bus_sede = new aca_Sede_Bus();

                List<ACA_007_Info> Lista = bus_rpt.GetList(IdEmpresa, fecha_ini, fecha_fin);
                this.DataSource = Lista;

                var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
                var NomSede = "";
                if (info_sede != null)
                {
                    NomSede = info_sede.NomSede;

                }

                lbl_sede.Text = NomSede;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrPivotGrid1_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            try
            {
                if (e.Field != null && (e.Field.FieldName == "NomPlantilla") && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                {
                    LabelBrick lb = new LabelBrick();
                    lb.Padding = new PaddingInfo(2, 2, 5, 2, GraphicsUnit.Pixel);
                    lb.Angle = 90;
                    lb.Text = e.Text;
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
