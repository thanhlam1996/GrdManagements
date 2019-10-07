using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_Yersin_KQXepLoaiBangTN : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_Yersin_KQXepLoaiBangTN()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName)
        {
            this.DataSource = tbPrint;
            xrTblTenTruong.Text = _CollegeName;
            //txt_NgayKy.Text = _NgayIn;
            //txt_CapBac.Text = _CapBac;
            //txt_NguoiKy.Text = _NguoiKy;
        }

        private void xrLabel_khoaQuanLy_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //e.Text = String.Format("{0} ({1} SV)", xrLabel_khoaQuanLy.Text, e.Value);
        }

        private void xrLabel_nganhHoc_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //e.Text = String.Format("{0} ({1} SV)", xrLabel_nganhHoc.Text, e.Value);
        }

        private void xrLabel_soQD_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
           // e.Text = String.Format("{0} ({1} SV)", xrLabel_soQD.Text, e.Value);
        }
    }
}
