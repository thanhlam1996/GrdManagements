using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_Yersin_TieuChuanTotNghiep : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_Yersin_TieuChuanTotNghiep()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txtTenTruong.Text = _CollegeName;
            txtDVCQ.Text = _AdministrativeUnit;
            txtChucVu.Text = _CapBac;
            txtNguoiKy.Text = _NguoiKy;
            this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("BatBuoc", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            this.GroupHeader6.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("TuChon", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

        }
    }
}
