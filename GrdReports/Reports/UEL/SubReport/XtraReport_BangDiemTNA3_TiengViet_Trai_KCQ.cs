using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using GrdReports.Reports;

namespace GrdReports.Reports.UEL.Sub
{
    public partial class XtraReport_BangDiemTNA3_TiengViet_Trai_KCQ : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_BangDiemTNA3_TiengViet_Trai_KCQ()
        {
            InitializeComponent();
        }

        private void XtraReport_BangDiemTNA3_TiengViet_Trai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = GrdReports.Reports.UEL.XtraReport_BangDiemTotNghiepAnhVietA3_KCQ.dtPrint_Phai_Cot1;
        }


    }
}
