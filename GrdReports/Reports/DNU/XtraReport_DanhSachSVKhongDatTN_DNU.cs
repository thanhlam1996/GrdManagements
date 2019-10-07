using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports
{
    public partial class XtraReport_DanhSachSVKhongDatTN_DNU : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_DanhSachSVKhongDatTN_DNU()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtTenTruong.Text = _CollegeName.ToUpper();
            txtDVHC.Text = _AdministrativeUnit;
            xrTblNgayKi.Text = _NgayIn;
            xrTblCapBac.Text = _CapBac;
            xrTblNguoiKy.Text = _NguoiKy;

            // Changes a string to titlecase.
            //xrTblTruong.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(myString.ToLower())+")";
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TenLopSinhVien", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        }


    }
}
