using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GrdReports.Reports.Yersin.Sub
{
    public partial class SubXtraReport_BangDiemNienChe_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public SubXtraReport_BangDiemNienChe_1()
        {
            InitializeComponent();
        }

        private void SubXtraReport_BangDiemNienChe_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe.dt1;
        }
    }
}
