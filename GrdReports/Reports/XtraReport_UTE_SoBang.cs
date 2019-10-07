using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_UTE_SoBang : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_UTE_SoBang()
        {
            InitializeComponent();
        }

        public void InitReports(DataTable _dtPrints)
        {
            this.DataSource = _dtPrints;
        }

        private void xrLabel_khoaQuanLy_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            e.Text = String.Format("{0} ({1} SV)", xrLabel_khoaQuanLy.Text, e.Value);
        }

        private void xrLabel_nganhHoc_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            e.Text = String.Format("{0} ({1} SV)", xrLabel_nganhHoc.Text, e.Value);
        }

        private void xrLabel_soQD_Count_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            e.Text = String.Format("{0} ({1} SV)", xrLabel_soQD.Text, e.Value);
        }
    }
}
