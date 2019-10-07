using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_Yersin_ThongKeSoLuongSVTotNghiep : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_Yersin_ThongKeSoLuongSVTotNghiep()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _NguoiLap, bool groupBacHe, bool groupKhoa, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            xrLabel_NgayIn.Text = _NgayIn;
            xrLabel_NguoiLap.Text = _NguoiLap;
            //txt_NguoiKy.Text = _NguoiKy;

            // Changes a string to titlecase.
            this.GroupHeader3.Visible = true;
            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("TenDotXet", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            if (groupBacHe == true)
            {
                // Changes a string to titlecase.
                this.GroupHeader1.Visible = true;
                this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("BacHeDaoTao", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

                this.xrTableCell_Tong_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tong")});
                this.xrTableCell_Nam_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nam") });
                this.xrTableCell_Nu_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nu") });
                this.xrTableCell_DanToc_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DanToc") });
                this.xrTableCell_TB_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "TB") });
                this.xrTableCell_Kha_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kha") });
                this.xrTableCell_Gioi_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gioi") });
                this.xrTableCell_XuatSac_1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "XuatSac") });
            }

            if (groupKhoa == true)
            {
                // Changes a string to titlecase.
                //this.GroupHeader2.Visible = true;
                //this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                //new DevExpress.XtraReports.UI.GroupField("TenDonVi", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

                //this.xrTableCell_Tong_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tong") });
                //this.xrTableCell_Nam_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nam") });
                //this.xrTableCell_Nu_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nu") });
                //this.xrTableCell_DanToc_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DanToc") });
                //this.xrTableCell_TB_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "TB") });
                //this.xrTableCell_Kha_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kha") });
                //this.xrTableCell_Gioi_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gioi") });
                //this.xrTableCell_XuatSac_2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "XuatSac") });
            }

            this.xrTableCell_Tong_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Tong") });
            this.xrTableCell_Nam_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nam") });
            this.xrTableCell_Nu_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Nu") });
            this.xrTableCell_DanToc_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DanToc") });
            this.xrTableCell_TB_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "TB") });
            this.xrTableCell_Kha_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Kha") });
            this.xrTableCell_Gioi_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gioi") });
            this.xrTableCell_XuatSac_3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "XuatSac") });
        }

        private void xrLabel_khoaQuanLy_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //e.Text = String.Format("{0} ({1} SV)", xrLabel_khoaQuanLy.Text, e.Value);
        }

        private void xrLabel_nganhHoc_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //e.Text = String.Format("{0} ({1} SV)", xrLabel_nganhHoc.Text, e.Value);
        }

        private void xrLabel_soQD_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
           // e.Text = String.Format("{0} ({1} SV)", xrLabel_soQD.Text, e.Value);
        }
    }
}
