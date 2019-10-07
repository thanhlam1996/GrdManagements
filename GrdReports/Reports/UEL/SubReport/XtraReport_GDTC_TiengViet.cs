using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GrdReports.Reports.UEL.Sub
{
    public partial class XtraReport_GDTC_TiengViet : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_GDTC_TiengViet()
        {
            InitializeComponent();
        }

        private void XtraReport_GDTC_TiengViet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = GrdReports.Reports.UEL.XtraReport_BangDiemTotNghiepAnhVietA3.dtPrint_TiengViet_GDTC;
        }




    }
}
