namespace Core.Web.Reportes.CuentasPorCobrar
{
    partial class CXC_007_Resumen_Rpt
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lbl_empresa = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl_usuario = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl_fecha = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.p_IdEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_fechaCorte = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdSucursal = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdCliente = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_MostrarSoloCarteraVencida = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_Idtipo_cliente = new DevExpress.XtraReports.Parameters.Parameter();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldAnio = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldNomRubro = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldNomJornada = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldNomNivel = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSaldo = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldCantidad = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 38F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 38F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1,
            this.xrTable1});
            this.PageHeader.HeightF = 208.3333F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow3,
            this.xrTableRow4});
            this.xrTable1.SizeF = new System.Drawing.SizeF(2424F, 75F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lbl_empresa,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // lbl_empresa
            // 
            this.lbl_empresa.Font = new System.Drawing.Font("Verdana", 12F);
            this.lbl_empresa.Name = "lbl_empresa";
            this.lbl_empresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(120, 0, 0, 0, 100F);
            this.lbl_empresa.StylePriority.UseFont = false;
            this.lbl_empresa.StylePriority.UsePadding = false;
            this.lbl_empresa.StylePriority.UseTextAlignment = false;
            this.lbl_empresa.Text = "lbl_empresa";
            this.lbl_empresa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_empresa.Weight = 1.7873100983020556D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 6F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "CXC_007";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.14864598586994821D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Italic);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "RESUMEN DE ANTIGUEDAD DE CARTERA";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 1.9359560841720036D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell66,
            this.xrTableCell61});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UsePadding = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "Fecha de corte:";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 0.21257750221434904D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters].[p_fechaCorte]")});
            this.xrTableCell61.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.StylePriority.UsePadding = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            this.xrTableCell61.Text = "xrTableCell61";
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell61.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrTableCell61.Weight = 1.7233785819576548D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable9});
            this.PageFooter.HeightF = 20F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrTable9
            // 
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable9.SizeF = new System.Drawing.SizeF(2424F, 20F);
            this.xrTable9.StylePriority.UseBorders = false;
            this.xrTable9.StylePriority.UseFont = false;
            this.xrTable9.StylePriority.UseTextAlignment = false;
            this.xrTable9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.lbl_usuario,
            this.xrTableCell21,
            this.lbl_fecha,
            this.xrTableCell34});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.StylePriority.UseBorders = false;
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "Usuario:";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell20.Weight = 1.2800001627063851D;
            // 
            // lbl_usuario
            // 
            this.lbl_usuario.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.lbl_usuario.Font = new System.Drawing.Font("Verdana", 7F);
            this.lbl_usuario.Name = "lbl_usuario";
            this.lbl_usuario.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.lbl_usuario.StylePriority.UseBorders = false;
            this.lbl_usuario.StylePriority.UseFont = false;
            this.lbl_usuario.StylePriority.UsePadding = false;
            this.lbl_usuario.StylePriority.UseTextAlignment = false;
            this.lbl_usuario.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbl_usuario.Weight = 3.2000001225822503D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell21.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UsePadding = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "Fecha de impresión:";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell21.Weight = 1.5999999824981335D;
            // 
            // lbl_fecha
            // 
            this.lbl_fecha.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.lbl_fecha.Font = new System.Drawing.Font("Verdana", 7F);
            this.lbl_fecha.Name = "lbl_fecha";
            this.lbl_fecha.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.lbl_fecha.StylePriority.UseBorders = false;
            this.lbl_fecha.StylePriority.UseFont = false;
            this.lbl_fecha.StylePriority.UsePadding = false;
            this.lbl_fecha.StylePriority.UseTextAlignment = false;
            this.lbl_fecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbl_fecha.Weight = 4.6819892575370723D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell34.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
            this.xrTableCell34.Font = new System.Drawing.Font("Verdana", 7F);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.Weight = 0.89504752457758885D;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(0.0002441406F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(186.1189F, 20F);
            this.xrPageInfo2.StylePriority.UseBorders = false;
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UsePadding = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPageInfo2.TextFormatString = "Página {0} de {1}";
            // 
            // p_IdEmpresa
            // 
            this.p_IdEmpresa.Name = "p_IdEmpresa";
            this.p_IdEmpresa.Type = typeof(int);
            this.p_IdEmpresa.ValueInfo = "0";
            this.p_IdEmpresa.Visible = false;
            // 
            // p_fechaCorte
            // 
            this.p_fechaCorte.Name = "p_fechaCorte";
            this.p_fechaCorte.Visible = false;
            // 
            // p_IdSucursal
            // 
            this.p_IdSucursal.Name = "p_IdSucursal";
            this.p_IdSucursal.Type = typeof(int);
            this.p_IdSucursal.ValueInfo = "0";
            this.p_IdSucursal.Visible = false;
            // 
            // p_IdCliente
            // 
            this.p_IdCliente.Name = "p_IdCliente";
            this.p_IdCliente.Type = typeof(int);
            this.p_IdCliente.ValueInfo = "0";
            this.p_IdCliente.Visible = false;
            // 
            // p_MostrarSoloCarteraVencida
            // 
            this.p_MostrarSoloCarteraVencida.Name = "p_MostrarSoloCarteraVencida";
            this.p_MostrarSoloCarteraVencida.Visible = false;
            // 
            // p_Idtipo_cliente
            // 
            this.p_Idtipo_cliente.Name = "p_Idtipo_cliente";
            this.p_Idtipo_cliente.Visible = false;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Core.Info.Reportes.CuentasPorCobrar.CXC_007_Info);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(Core.Info.Reportes.CuentasPorCobrar.CXC_007_Resumen_Info);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldAnio,
            this.fieldNomRubro,
            this.fieldNomJornada,
            this.fieldNomNivel,
            this.fieldCantidad,
            this.fieldSaldo});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(350.8333F, 133.3333F);
            // 
            // fieldAnio
            // 
            this.fieldAnio.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldAnio.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldAnio.AreaIndex = 0;
            this.fieldAnio.FieldName = "Anio";
            this.fieldAnio.Name = "fieldAnio";
            this.fieldAnio.Width = 50;
            // 
            // fieldNomRubro
            // 
            this.fieldNomRubro.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldNomRubro.AreaIndex = 1;
            this.fieldNomRubro.FieldName = "NomRubro";
            this.fieldNomRubro.Name = "fieldNomRubro";
            this.fieldNomRubro.Width = 200;
            // 
            // fieldNomJornada
            // 
            this.fieldNomJornada.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.fieldNomJornada.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldNomJornada.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldNomJornada.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldNomJornada.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldNomJornada.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldNomJornada.AreaIndex = 0;
            this.fieldNomJornada.FieldName = "NomJornada";
            this.fieldNomJornada.Name = "fieldNomJornada";
            // 
            // fieldNomNivel
            // 
            this.fieldNomNivel.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldNomNivel.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldNomNivel.AreaIndex = 1;
            this.fieldNomNivel.FieldName = "NomNivel";
            this.fieldNomNivel.Name = "fieldNomNivel";
            // 
            // fieldSaldo
            // 
            this.fieldSaldo.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldSaldo.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldSaldo.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldSaldo.AreaIndex = 0;
            this.fieldSaldo.Caption = "Total";
            this.fieldSaldo.FieldName = "Saldo";
            this.fieldSaldo.Name = "fieldSaldo";
            // 
            // fieldCantidad
            // 
            this.fieldCantidad.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldCantidad.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.fieldCantidad.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldCantidad.AreaIndex = 1;
            this.fieldCantidad.Caption = "Cant";
            this.fieldCantidad.FieldName = "Cantidad";
            this.fieldCantidad.Name = "fieldCantidad";
            this.fieldCantidad.Width = 50;
            // 
            // CXC_007_Resumen_Rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1,
            this.objectDataSource2});
            this.DataSource = this.objectDataSource2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(38, 38, 38, 38);
            this.PageHeight = 2000;
            this.PageWidth = 2500;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.p_IdEmpresa,
            this.p_fechaCorte,
            this.p_IdSucursal,
            this.p_IdCliente,
            this.p_MostrarSoloCarteraVencida,
            this.p_Idtipo_cliente});
            this.Version = "19.1";
            this.DesignerLoaded += new DevExpress.XtraReports.UserDesigner.DesignerLoadedEventHandler(this.CXC_007_Resumen_Rpt_DesignerLoaded);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.CXC_007_Resumen_Rpt_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.Parameters.Parameter p_IdEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter p_fechaCorte;
        public DevExpress.XtraReports.Parameters.Parameter p_IdSucursal;
        public DevExpress.XtraReports.Parameters.Parameter p_IdCliente;
        public DevExpress.XtraReports.Parameters.Parameter p_MostrarSoloCarteraVencida;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell lbl_empresa;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell66;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell61;
        private DevExpress.XtraReports.UI.XRTable xrTable9;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTableCell lbl_usuario;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.XRTableCell lbl_fecha;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        public DevExpress.XtraReports.Parameters.Parameter p_Idtipo_cliente;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAnio;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldNomRubro;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldNomJornada;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldNomNivel;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSaldo;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldCantidad;
    }
}
