using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_SoGocCapChungChi_DNU : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_SoGocCapChungChi_DNU()
        {
            InitializeComponent();
        }
        //sp_Graduate_SoGocCapChungChi_DanhSachSinhVienTN
        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtNgayKy.Text = _NgayIn;
            txtChucVu.Text = _CapBac;
        }

    }
}
