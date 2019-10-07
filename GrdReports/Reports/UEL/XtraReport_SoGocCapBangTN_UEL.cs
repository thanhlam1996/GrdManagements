using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_SoGocCapBangTN_UEL : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_SoGocCapBangTN_UEL()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)

        {
            string i = DateTime.Now.ToString("dd/MM/yyyy");
            this.DataSource = tbPrint;

            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Lop", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            xrTableCell5.Text = "Ngày in: "+ i;
            xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            xrTableCell8.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
        }

    }
}
