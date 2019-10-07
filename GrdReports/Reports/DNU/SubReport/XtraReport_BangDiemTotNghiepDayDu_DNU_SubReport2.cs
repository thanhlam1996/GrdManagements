using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using GrdCore.BLL;
namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable _dtPrint;
        public XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport2()
        {
            InitializeComponent();
        }

        private void XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = XtraReport_BangDiemTotNghiepDayDu_DNU.dtPrint_M2;
        }
    }
}
