namespace Core.Web.Reportes.Academico
{
    partial class ACA_019_Rpt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACA_019_Rpt));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lbl_imagen = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lbl = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtSolicitud = new DevExpress.XtraReports.UI.XRRichText();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow45 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Rector = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Colecturia = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow46 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.p_IdEmpresa = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdAnio = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdSede = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdJornada = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdNivel = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdCurso = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdParalelo = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_IdAlumno = new DevExpress.XtraReports.Parameters.Parameter();
            this.p_MostrarRetirado = new DevExpress.XtraReports.Parameters.Parameter();
            this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.groupFooterBand1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSolicitud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl_imagen,
            this.xrTable1});
            this.PageHeader.HeightF = 310F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lbl_imagen
            // 
            this.lbl_imagen.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lbl_imagen.Name = "lbl_imagen";
            this.lbl_imagen.SizeF = new System.Drawing.SizeF(140F, 60F);
            this.lbl_imagen.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(727F, 250F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lbl});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1.8659516815610444D;
            // 
            // lbl
            // 
            this.lbl.Font = new System.Drawing.Font("Verdana", 22F, System.Drawing.FontStyle.Bold);
            this.lbl.Name = "lbl";
            this.lbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 10, 100F);
            this.lbl.StylePriority.UseFont = false;
            this.lbl.StylePriority.UsePadding = false;
            this.lbl.StylePriority.UseTextAlignment = false;
            this.lbl.Text = "CERTIFICACION";
            this.lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.lbl.Weight = 1.99871841953339D;
            // 
            // txtSolicitud
            // 
            this.txtSolicitud.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtSolicitud.LocationFloat = new DevExpress.Utils.PointFloat(0.0002034505F, 0F);
            this.txtSolicitud.Name = "txtSolicitud";
            this.txtSolicitud.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
            this.txtSolicitud.SerializableRtfString = resources.GetString("txtSolicitud.SerializableRtfString");
            this.txtSolicitud.SizeF = new System.Drawing.SizeF(726.9998F, 320F);
            this.txtSolicitud.StylePriority.UseFont = false;
            this.txtSolicitud.StylePriority.UsePadding = false;
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Verdana", 10F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0.0002034505F, 60F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow45,
            this.xrTableRow46});
            this.xrTable5.SizeF = new System.Drawing.SizeF(726.9998F, 40F);
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow45
            // 
            this.xrTableRow45.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Rector,
            this.xrTableCell16,
            this.Colecturia});
            this.xrTableRow45.Name = "xrTableRow45";
            this.xrTableRow45.StylePriority.UseTextAlignment = false;
            this.xrTableRow45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrTableRow45.Weight = 0.875912270864986D;
            // 
            // Rector
            // 
            this.Rector.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.Rector.Font = new System.Drawing.Font("Verdana", 9F);
            this.Rector.Multiline = true;
            this.Rector.Name = "Rector";
            this.Rector.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.Rector.StylePriority.UseBorders = false;
            this.Rector.StylePriority.UseFont = false;
            this.Rector.StylePriority.UsePadding = false;
            this.Rector.StylePriority.UseTextAlignment = false;
            this.Rector.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Rector.Weight = 5.9574590444390338D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Multiline = true;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UsePadding = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 3.5531986227577725D;
            // 
            // Colecturia
            // 
            this.Colecturia.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.Colecturia.Font = new System.Drawing.Font("Verdana", 9F);
            this.Colecturia.Multiline = true;
            this.Colecturia.Name = "Colecturia";
            this.Colecturia.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.Colecturia.StylePriority.UseBorders = false;
            this.Colecturia.StylePriority.UseFont = false;
            this.Colecturia.StylePriority.UsePadding = false;
            this.Colecturia.StylePriority.UseTextAlignment = false;
            this.Colecturia.Text = "CPA. EDISON GELLIBERT VILLAGOMEZ";
            this.Colecturia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Colecturia.Weight = 5.9574549106885915D;
            // 
            // xrTableRow46
            // 
            this.xrTableRow46.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell143,
            this.xrTableCell3,
            this.xrTableCell2});
            this.xrTableRow46.Name = "xrTableRow46";
            this.xrTableRow46.StylePriority.UseTextAlignment = false;
            this.xrTableRow46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow46.Weight = 0.875912327640394D;
            // 
            // xrTableCell143
            // 
            this.xrTableCell143.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell143.Multiline = true;
            this.xrTableCell143.Name = "xrTableCell143";
            this.xrTableCell143.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.xrTableCell143.StylePriority.UseFont = false;
            this.xrTableCell143.StylePriority.UsePadding = false;
            this.xrTableCell143.StylePriority.UseTextAlignment = false;
            this.xrTableCell143.Text = "RECTOR";
            this.xrTableCell143.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell143.Weight = 9.2375453119033146D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "\r\n";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 5.50953592074102D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "COORDINADOR DE COLECTURIA";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 9.2375398017564869D;
            // 
            // p_IdEmpresa
            // 
            this.p_IdEmpresa.Name = "p_IdEmpresa";
            this.p_IdEmpresa.Visible = false;
            // 
            // p_IdAnio
            // 
            this.p_IdAnio.Name = "p_IdAnio";
            this.p_IdAnio.Visible = false;
            // 
            // p_IdSede
            // 
            this.p_IdSede.Name = "p_IdSede";
            this.p_IdSede.Visible = false;
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
            // p_IdAlumno
            // 
            this.p_IdAlumno.Name = "p_IdAlumno";
            this.p_IdAlumno.Visible = false;
            // 
            // p_MostrarRetirado
            // 
            this.p_MostrarRetirado.Name = "p_MostrarRetirado";
            this.p_MostrarRetirado.Visible = false;
            // 
            // groupHeaderBand1
            // 
            this.groupHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtSolicitud});
            this.groupHeaderBand1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("OrdenJornada", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("OrdenNivel", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("OrdenCurso", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("OrdenJornada", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("NombreAlumno", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("IdAlumno", DevExpress.XtraReports.UI.XRColumnSortOrder.None)});
            this.groupHeaderBand1.HeightF = 320F;
            this.groupHeaderBand1.Name = "groupHeaderBand1";
            this.groupHeaderBand1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            // 
            // groupFooterBand1
            // 
            this.groupFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.groupFooterBand1.Name = "groupFooterBand1";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(Core.Info.Reportes.Academico.ACA_019_Info);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // ACA_019_Rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.PageHeader,
            this.groupHeaderBand1,
            this.groupFooterBand1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.p_IdEmpresa,
            this.p_IdAnio,
            this.p_IdSede,
            this.p_IdJornada,
            this.p_IdNivel,
            this.p_IdCurso,
            this.p_IdParalelo,
            this.p_IdAlumno,
            this.p_MostrarRetirado});
            this.Version = "20.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ACA_019_Rpt_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSolicitud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell lbl;
        private DevExpress.XtraReports.UI.XRRichText txtSolicitud;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow45;
        private DevExpress.XtraReports.UI.XRTableCell Rector;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell Colecturia;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow46;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell143;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        public DevExpress.XtraReports.Parameters.Parameter p_IdEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter p_IdAnio;
        public DevExpress.XtraReports.Parameters.Parameter p_IdSede;
        public DevExpress.XtraReports.Parameters.Parameter p_IdJornada;
        public DevExpress.XtraReports.Parameters.Parameter p_IdNivel;
        public DevExpress.XtraReports.Parameters.Parameter p_IdCurso;
        public DevExpress.XtraReports.Parameters.Parameter p_IdParalelo;
        public DevExpress.XtraReports.Parameters.Parameter p_IdAlumno;
        public DevExpress.XtraReports.Parameters.Parameter p_MostrarRetirado;
        private DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand1;
        private DevExpress.XtraReports.UI.GroupFooterBand groupFooterBand1;
        private DevExpress.XtraReports.UI.XRPictureBox lbl_imagen;
    }
}
