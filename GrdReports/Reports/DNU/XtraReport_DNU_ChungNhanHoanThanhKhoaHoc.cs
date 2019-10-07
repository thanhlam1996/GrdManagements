using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports
{
    public partial class XtraReport_DNU_ChungNhanHoanThanhKhoaHoc : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_DNU_ChungNhanHoanThanhKhoaHoc()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtDVHC.Text = _AdministrativeUnit.ToUpper();
            txtTenTruong.Text = _CollegeName.ToUpper();
            txtNgayKy.Text = _NgayIn;
            txtChucVu.Text = _CapBac;
            txtHoTen.Text = _NguoiKy;
        }

        private void XtraReport_DNU_ChungNhanHoanThanhKhoaHoc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
