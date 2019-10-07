using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;

namespace GrdReports
{
    public partial class XtraReport_ThonkeTotNghiep_TheoNganh : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_ThonkeTotNghiep_TheoNganh()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            lblNgayIn.Text = _NgayIn;
            //xrTblCapBac.Text = _CapBac;
            //xrTblNguoiKy.Text = _NguoiKy;
        }
    }
}
