using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Academico;

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

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrPivotGrid1_PrintHeader(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportHeaderEventArgs e)
        {
            try
            {
                if (e.Field != null && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                {
                    LabelBrick lb = new LabelBrick();
                    lb.BackColor = Color.Beige;
                    lb.Padding = new PaddingInfo(2, 2, 5, 2, GraphicsUnit.Pixel);
                    lb.Angle = 90;
                    lb.Rect = GraphicsUnitConverter.DocToPixel(e.Brick.Rect);
                    e.Brick = lb;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ACA_007_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {

                lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                lbl_usuario.Text = usuario;

                int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
                int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
                int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
                int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
                int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
                int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
                aca_Sede_Bus bus_sede = new aca_Sede_Bus();

                List<ACA_007_Info> Lista = bus_rpt.Getlist(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
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
    
    }
}
