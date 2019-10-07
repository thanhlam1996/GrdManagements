using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GrdReports.Reports
{
    public partial class Sub_XtraReport_BangDiem2Cot_Mat2_DHDN : DevExpress.XtraReports.UI.XtraReport
    {
        public Sub_XtraReport_BangDiem2Cot_Mat2_DHDN()
        {
            InitializeComponent();
        }

        private void Sub_XtraReport_BangDiem_Mat2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = XtraReport_BangDiemTNCaNhan_2Cot_DHDN.dtPrint_M2;
        }
    }
}
