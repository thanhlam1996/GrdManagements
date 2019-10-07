using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport1()
        {
            InitializeComponent();
        }

        private void XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = XtraReport_BangDiemTotNghiepDayDu_DNU.dtPrint_M1;
        }
    }
}
