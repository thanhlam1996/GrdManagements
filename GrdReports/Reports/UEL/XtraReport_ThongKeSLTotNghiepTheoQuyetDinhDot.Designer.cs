namespace GrdReports.Reports.UEL
{
    partial class XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lbl_title = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_DinhKemCongVanSo = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.txt_NguoiKy = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_CapBac = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCapBac = new DevExpress.XtraReports.Parameters.Parameter();
            this.txtNguoiKyTen = new DevExpress.XtraReports.Parameters.Parameter();
            this.lblNgayIn = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
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
            this.TopMargin.HeightF = 48F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.HeightF = 0F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNgayIn,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel1,
            this.xrLabel2,
            this.xrLine1,
            this.lbl_title,
            this.lbl_DinhKemCongVanSo});
            this.GroupHeader2.HeightF = 246.875F;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // lbl_title
            // 
            this.lbl_title.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.LocationFloat = new DevExpress.Utils.PointFloat(0F, 120.2084F);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 20, 0, 0, 100F);
            this.lbl_title.SizeF = new System.Drawing.SizeF(757F, 75.08336F);
            this.lbl_title.StylePriority.UseFont = false;
            this.lbl_title.StylePriority.UsePadding = false;
            this.lbl_title.StylePriority.UseTextAlignment = false;
            this.lbl_title.Text = "THỐNG KÊ SỐ LƯỢNG TỐT NGHIỆP THEO QUYẾT ĐỊNH ĐỢT TỐT NGHIỆP THÁNG";
            this.lbl_title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_DinhKemCongVanSo
            // 
            this.lbl_DinhKemCongVanSo.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lbl_DinhKemCongVanSo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 195.2916F);
            this.lbl_DinhKemCongVanSo.Name = "lbl_DinhKemCongVanSo";
            this.lbl_DinhKemCongVanSo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_DinhKemCongVanSo.SizeF = new System.Drawing.SizeF(757.0001F, 29.25F);
            this.lbl_DinhKemCongVanSo.StylePriority.UseFont = false;
            this.lbl_DinhKemCongVanSo.StylePriority.UseTextAlignment = false;
            this.lbl_DinhKemCongVanSo.Text = "(Đính kèm công văn số ... ngày ... của Hiệu trưởng trường)";
            this.lbl_DinhKemCongVanSo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 0F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txt_NguoiKy,
            this.txt_CapBac});
            this.GroupFooter2.HeightF = 187.5F;
            this.GroupFooter2.Level = 1;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // txt_NguoiKy
            // 
            this.txt_NguoiKy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.txt_NguoiKy.LocationFloat = new DevExpress.Utils.PointFloat(457.0001F, 154.5F);
            this.txt_NguoiKy.Name = "txt_NguoiKy";
            this.txt_NguoiKy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_NguoiKy.SizeF = new System.Drawing.SizeF(300F, 23F);
            this.txt_NguoiKy.StylePriority.UseFont = false;
            this.txt_NguoiKy.StylePriority.UseTextAlignment = false;
            this.txt_NguoiKy.Text = "txtNguoiKyTen";
            this.txt_NguoiKy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txt_CapBac
            // 
            this.txt_CapBac.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.txt_CapBac.LocationFloat = new DevExpress.Utils.PointFloat(457.0001F, 24.95836F);
            this.txt_CapBac.Name = "txt_CapBac";
            this.txt_CapBac.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_CapBac.SizeF = new System.Drawing.SizeF(300F, 23F);
            this.txt_CapBac.StylePriority.UseFont = false;
            this.txt_CapBac.StylePriority.UseTextAlignment = false;
            this.txt_CapBac.Text = "txtCapBac";
            this.txt_CapBac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // txtCapBac
            // 
            this.txtCapBac.Description = "Parameter1";
            this.txtCapBac.Name = "txtCapBac";
            // 
            // txtNguoiKyTen
            // 
            this.txtNguoiKyTen.Description = "Parameter1";
            this.txtNguoiKyTen.Name = "txtNguoiKyTen";
            this.txtNguoiKyTen.Type = typeof(int);
            this.txtNguoiKyTen.ValueInfo = "0";
            // 
            // lblNgayIn
            // 
            this.lblNgayIn.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Italic);
            this.lblNgayIn.LocationFloat = new DevExpress.Utils.PointFloat(422F, 67.00007F);
            this.lblNgayIn.Name = "lblNgayIn";
            this.lblNgayIn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNgayIn.SizeF = new System.Drawing.SizeF(335F, 23F);
            this.lblNgayIn.StylePriority.UseFont = false;
            this.lblNgayIn.StylePriority.UseTextAlignment = false;
            this.lblNgayIn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(422F, 43.84267F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(335F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Độc lập - Tự do - Hạnh phúc";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(422F, 20.84265F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(335F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20.84265F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(325F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "ĐẠI HỌC QUỐC GIA TP.HCM";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 43.84267F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(325F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "TRƯỜNG ĐẠI HỌC KINH TẾ - LUẬT";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLine1
            // 
            this.xrLine1.BackColor = System.Drawing.Color.Transparent;
            this.xrLine1.BorderColor = System.Drawing.Color.Black;
            this.xrLine1.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(75.63887F, 67.00007F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 3, 100F);
            this.xrLine1.SizeF = new System.Drawing.SizeF(169.1527F, 3.375004F);
            this.xrLine1.StylePriority.UseBackColor = false;
            this.xrLine1.StylePriority.UseBorderColor = false;
            this.xrLine1.StylePriority.UseBorders = false;
            this.xrLine1.StylePriority.UsePadding = false;
            // 
            // XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupFooter1,
            this.GroupFooter2});
            this.Margins = new System.Drawing.Printing.Margins(43, 27, 48, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.txtCapBac,
            this.txtNguoiKyTen});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRLabel lbl_title;
        private DevExpress.XtraReports.UI.XRLabel lbl_DinhKemCongVanSo;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.XRLabel txt_CapBac;
        private DevExpress.XtraReports.UI.XRLabel txt_NguoiKy;
        private DevExpress.XtraReports.Parameters.Parameter txtCapBac;
        private DevExpress.XtraReports.Parameters.Parameter txtNguoiKyTen;
        private DevExpress.XtraReports.UI.XRLabel lblNgayIn;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
    }
}
