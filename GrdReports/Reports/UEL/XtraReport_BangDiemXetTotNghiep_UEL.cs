using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemXetTotNghiep_UEL : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint = new DataTable();

        public XtraReport_BangDiemXetTotNghiep_UEL()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;

            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("STT_SV", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("SelectionParentID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("SelectionID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});

            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SoTC")});
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Sum;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;

            this.xrTableCell33.Summary = xrSummary2;
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "MaMonHoc") });
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "TenMonHoc") });
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "SoTC") });
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DiemTK_10") });
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DiemTK_4") });
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "DiemTK_Chu") });
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "GhiChu") });
        }

    }
}
