using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTotNghiepDayDu_Yersin : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_BangDiemTotNghiepDayDu_Yersin()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txt_NgayKy.Text = _NgayIn;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
            xrLabel_NguoiLap.Text = _NguoiLap;
        }

    }
}
