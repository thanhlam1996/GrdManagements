using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports
{
    public partial class XtraReport_USSH_DanhSachSV_DoAnTN : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_USSH_DanhSachSV_DoAnTN()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName)
        {
            this.DataSource = tbPrint;
            //xrTblTenTruong.Text = _CollegeName;
            xrTblNgayKi.Text = _NgayIn;
            xrTblCapBac.Text = _CapBac;
            xrTblNguoiKy.Text = _NguoiKy;
            //string myString = _CollegeName;

            // Changes a string to titlecase.
            //xrTblTruong.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(myString.ToLower())+")";
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenLopSinhVien", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
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
            //e.Text = String.Format("{0} ({1} SV)", xrLabel_soQD.Text, e.Value);
        }

    }
}
