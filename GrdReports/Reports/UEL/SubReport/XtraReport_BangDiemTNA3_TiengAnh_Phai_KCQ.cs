using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GrdReports.Reports.UEL.Sub
{
    public partial class XtraReport_BangDiemTNA3_TiengAnh_Phai_KCQ : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_BangDiemTNA3_TiengAnh_Phai_KCQ()
        {
            InitializeComponent();
        }

        private void XtraReport_BangDiemTNA3_TiengAnh_Phai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = GrdReports.Reports.UEL.XtraReport_BangDiemTotNghiepAnhVietA3_KCQ.dtPrint_Trai_Cot2;
        }


    }
}
