namespace Core.Web.Reportes.Academico
{
    partial class ACA_003_Rpt
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
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lbl_imagen = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lbl_sede = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lbl_titulo = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl_usuario = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbl_fecha = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.p_IdEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdAlumno = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdSede = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
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
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lbl_imagen});
            this.ReportHeader.HeightF = 130.5F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lbl_imagen
            // 
            this.lbl_imagen.LocationFloat = new DevExpress.Utils.PointFloat(300.3178F, 0F);
            this.lbl_imagen.Name = "lbl_imagen";
            this.lbl_imagen.SizeF = new System.Drawing.SizeF(135.3146F, 61.50002F);
            this.lbl_imagen.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(2.543131E-05F, 61.50002F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(751.0001F, 40.00001F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lbl_sede});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // lbl_sede
            // 
            this.lbl_sede.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_sede.Name = "lbl_sede";
            this.lbl_sede.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_sede.StylePriority.UseFont = false;
            this.lbl_sede.StylePriority.UsePadding = false;
            this.lbl_sede.StylePriority.UseTextAlignment = false;
            this.lbl_sede.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_sede.Weight = 1.99871841953339D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lbl_titulo});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbl_titulo.StylePriority.UseFont = false;
            this.lbl_titulo.StylePriority.UsePadding = false;
            this.lbl_titulo.StylePriority.UseTextAlignment = false;
            this.lbl_titulo.Text = "CONTRATO DE PRESTACIÓN DE SERVICIO EDUCATIVO";
            this.lbl_titulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbl_titulo.Weight = 1.99871841953339D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.PageFooter.HeightF = 20F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrTable8.SizeF = new System.Drawing.SizeF(751.0001F, 20F);
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseFont = false;
            this.xrTable8.StylePriority.UseTextAlignment = false;
            this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell50,
            this.lbl_usuario,
            this.xrTableCell51,
            this.lbl_fecha,
            this.xrTableCell52});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.StylePriority.UseBorders = false;
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell50.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UsePadding = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.Text = "Usuario:";
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell50.Weight = 1.3333334347064D;
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
            this.lbl_usuario.Weight = 2.2399996626659D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell51.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UsePadding = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "Fecha de impresión:";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell51.Weight = 1.49333377781982D;
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
            this.lbl_fecha.Weight = 1.84679797253295D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell52.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2});
            this.xrTableCell52.Font = new System.Drawing.Font("Verdana", 7F);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.Weight = 1.0865525875519D;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrPageInfo2.Font = new System.Drawing.Font("Verdana", 7F);
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(101.9999F, 20F);
            this.xrPageInfo2.StylePriority.UseBorders = false;
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UsePadding = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPageInfo2.TextFormatString = "Página {0} de {1}";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Core.Info.Reportes.Academico.ACA_003_Info);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // p_IdEmpresa
            // 
            this.p_IdEmpresa.Name = "p_IdEmpresa";
            this.p_IdEmpresa.Visible = false;
            // 
            // p_IdAlumno
            // 
            this.p_IdAlumno.Name = "p_IdAlumno";
            this.p_IdAlumno.Visible = false;
            // 
            // p_IdSede
            // 
            this.p_IdSede.Name = "p_IdSede";
            this.p_IdSede.Visible = false;
            // 
            // ACA_003_Rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageFooter});
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
            this.p_IdAlumno,
            this.p_IdSede});
            this.Version = "19.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ACA_003_Rpt_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox lbl_imagen;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow22;
        private DevExpress.XtraReports.UI.XRTableCell lbl_sede;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell lbl_titulo;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable8;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow16;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell50;
        private DevExpress.XtraReports.UI.XRTableCell lbl_usuario;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell51;
        private DevExpress.XtraReports.UI.XRTableCell lbl_fecha;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell52;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        public DevExpress.XtraReports.Parameters.Parameter p_IdEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter p_IdAlumno;
        public DevExpress.XtraReports.Parameters.Parameter p_IdSede;
    }
}
