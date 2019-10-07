using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_Yersin_GiayChungNhanHoanThanhCTDT : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_Yersin_GiayChungNhanHoanThanhCTDT()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, byte[] _CollegeLogo, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtTenTruong.Text = _CollegeName;
            txtDVCQ.Text = _AdministrativeUnit;
            txtChucVu.Text = _CapBac;
            txtNguoiKy.Text = _NguoiKy;
            txtTieuDe.Text = "HIỆU TRƯỞNG " + _CollegeName.ToUpper();
        }

    }
}
