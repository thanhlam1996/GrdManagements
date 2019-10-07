using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports
{
    public partial class XtraReport_DanhSachCongNhanTN_DNU : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_DanhSachCongNhanTN_DNU()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtTenTruong.Text = _CollegeName;
            txtxNgayKy.Text = _NgayIn;
            xrTblCapBac.Text = _CapBac;
            xrTblNguoiKy.Text = _NguoiKy;
            string myString = _CollegeName;
            txtDVCQ.Text = _AdministrativeUnit;

            // Changes a string to titlecase.
            xrTblTruong.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(myString.ToLower())+")";
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenNganh", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
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
        private string ToCapFirst(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.Length == 1)
                return value.ToUpper();

            return string.Concat(char.ToUpper(value[0]), value.Substring(1).ToLower());
        }
    }
}
