using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;
using System.IO;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_GiayChungNhanHoanThanhKhoaHoc_UEL : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_GiayChungNhanHoanThanhKhoaHoc_UEL()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, byte[] _CollegeLogo, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtNgayKy.Text = _NgayIn;
            lblChucVu.Text = _CapBac;
            txtNguoiKy.Text = _NguoiKy;
            LoGo.Value = _CollegeLogo;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("MaSV_ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

        }

    }
}
