namespace Core.Web.Reportes.Academico
{
    partial class ACA_043_Promedios_Rpt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.p_IdEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdSede = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdAnio = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdJornada = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdNivel = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdCurso = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdParalelo = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_MostrarRetirados = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.pivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField8 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 38F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 38F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            // 
            // p_IdEmpresa
            // 
            this.p_IdEmpresa.Name = "p_IdEmpresa";
            this.p_IdEmpresa.Visible = false;
            // 
            // p_IdSede
            // 
            this.p_IdSede.Name = "p_IdSede";
            this.p_IdSede.Visible = false;
            // 
            // p_IdAnio
            // 
            this.p_IdAnio.Name = "p_IdAnio";
            this.p_IdAnio.Visible = false;
            // 
            // p_IdJornada
            // 
            this.p_IdJornada.Name = "p_IdJornada";
            this.p_IdJornada.Visible = false;
            // 
            // p_IdNivel
            // 
            this.p_IdNivel.Name = "p_IdNivel";
            this.p_IdNivel.Visible = false;
            // 
            // p_IdCurso
            // 
            this.p_IdCurso.Name = "p_IdCurso";
            this.p_IdCurso.Visible = false;
            // 
            // p_IdParalelo
            // 
            this.p_IdParalelo.Name = "p_IdParalelo";
            this.p_IdParalelo.Visible = false;
            // 
            // p_MostrarRetirados
            // 
            this.p_MostrarRetirados.Name = "p_MostrarRetirados";
            this.p_MostrarRetirados.Visible = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid2});
            this.PageHeader.HeightF = 265.5417F;
            this.PageHeader.Name = "PageHeader";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Core.Info.Reportes.Academico.ACA_043_Promedios_Info);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.FieldName = "PromedioFinal";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // xrPivotGridField8
            // 
            this.xrPivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField8.AreaIndex = 0;
            this.xrPivotGridField8.FieldName = "pe_cedulaRuc";
            this.xrPivotGridField8.MinWidth = 0;
            this.xrPivotGridField8.Name = "xrPivotGridField8";
            this.xrPivotGridField8.Width = 0;
            // 
            // xrPivotGridField2
            // 
            this.xrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField2.AreaIndex = 1;
            this.xrPivotGridField2.FieldName = "NomCurso";
            this.xrPivotGridField2.Name = "xrPivotGridField2";
            this.xrPivotGridField2.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.xrPivotGridField2.Width = 150;
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.ColumnValueLineCount = 20;
            this.xrPivotGridField1.FieldName = "pe_nombreCompleto";
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.xrPivotGridField1.Width = 80;
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid2.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid2.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid2.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.Lines.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPivotGrid2.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.DataSource = this.objectDataSource1;
            this.xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.xrPivotGridField1,
            this.xrPivotGridField2,
            this.xrPivotGridField8,
            this.pivotGridField1});
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 0F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintUnusedFilterFields = false;
            this.xrPivotGrid2.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.xrPivotGrid2.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid2.OptionsView.ShowRowHeaders = false;
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(750.9999F, 86.87505F);
            // 
            // ACA_043_Promedios_Rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.PageHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(38, 38, 38, 38);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.p_IdEmpresa,
            this.p_IdSede,
            this.p_IdAnio,
            this.p_IdJornada,
            this.p_IdNivel,
            this.p_IdCurso,
            this.p_IdParalelo,
            this.p_MostrarRetirados});
            this.Version = "20.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ACA_043_Promedios_Rpt_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        public DevExpress.XtraReports.Parameters.Parameter p_IdEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter p_IdSede;
        public DevExpress.XtraReports.Parameters.Parameter p_IdAnio;
        public DevExpress.XtraReports.Parameters.Parameter p_IdJornada;
        public DevExpress.XtraReports.Parameters.Parameter p_IdNivel;
        public DevExpress.XtraReports.Parameters.Parameter p_IdCurso;
        public DevExpress.XtraReports.Parameters.Parameter p_IdParalelo;
        public DevExpress.XtraReports.Parameters.Parameter p_MostrarRetirados;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField8;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField1;
    }
}
