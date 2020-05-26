using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.Reportes.CuentasPorCobrar;
using DevExpress.XtraPrinting;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_008_Resumen_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        CXC_008_Bus bus_rpt = new CXC_008_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }

        public CXC_008_Resumen_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_008_Resumen_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdAlumno.Value);
            DateTime FechaFin = string.IsNullOrEmpty(p_FechaCorte.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaCorte.Value);

            List<CXC_008_Info> Lista = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, FechaFin);
            this.DataSource = Lista;

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

        }

        private void xrPivotGrid_Paralelo_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            try
            {
                if (e.Field != null && (e.Field.FieldName == "NomSede") && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea && e.ValueType != DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.ValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                {
                    LabelBrick lb = new DevExpress.XtraPrinting.LabelBrick();
                    lb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 2, GraphicsUnit.Pixel);
                    lb.Angle = 90;
                    lb.Text = e.Field.FieldName == "Num" ? Convert.ToString(e.Text.Replace("Alumnos", "")) : e.Text;
                    lb.Rect = DevExpress.XtraPrinting.GraphicsUnitConverter.DocToPixel(e.Brick.Rect);
                    e.Brick = lb;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void xrPivotGrid_Nivel_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            if (e.Field != null && (e.Field.FieldName == "NomSede") && e.Field.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea && e.ValueType != DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.ValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Total)
            {
                LabelBrick lb = new DevExpress.XtraPrinting.LabelBrick();
                lb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 5, 2, GraphicsUnit.Pixel);
                lb.Text = e.Field.FieldName == "Num" ? Convert.ToString(e.Text.Replace("Alumnos", "")) : e.Text;
                lb.Angle = 90;
                lb.Rect = DevExpress.XtraPrinting.GraphicsUnitConverter.DocToPixel(e.Brick.Rect);
                e.Brick = lb;
            }
        }
    }
}
