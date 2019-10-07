using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using GrdReports.Reports;
using DevExpress.PivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;

namespace GrdReports.Reports
{
    public partial class XtraReport_Yersin_ThongKeKetQuaXetTN : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint_MonTN = new DataTable();
        double tongDauKhoa, tongXetThi, slTN, slHong, slHoanXet, slXS, slGioi, slKha, slTBK, slTB = 0;
        XtraReport rep;
        Band band;
        public XtraReport_Yersin_ThongKeKetQuaXetTN()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _NgayThi, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txt_NgayKy.Text = _NgayIn;
            txtDVCQ.Text = _AdministrativeUnit;
            txtTenTruong.Text = _CollegeName;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
            txtTieuDe.Text = "Khóa thi ngày: " + _NgayThi;
        }
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
        
        private void txtTongDauKhoa_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }       

        private void txtTongDauKhoa_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void txtTongDauKhoa_SummaryRowChanged(object sender, EventArgs e)
        {
        }

        private void xrTableCell48_TextChanged(object sender, EventArgs e)
        {
            tongDauKhoa += Convert.ToDouble(GetCurrentColumnValue("SLSVDauKhoa"));
            tongXetThi += Convert.ToDouble(GetCurrentColumnValue("SLSVXetTN"));
            slTN += Convert.ToDouble(GetCurrentColumnValue("SLDatTN"));
            slHong += Convert.ToDouble(GetCurrentColumnValue("SLKhongDatTN"));
            slHoanXet+= Convert.ToDouble(GetCurrentColumnValue("SLSVHoanXet"));
            slXS += Convert.ToDouble(GetCurrentColumnValue("XLTN_XuatSac"));
            slGioi += Convert.ToDouble(GetCurrentColumnValue("XLTN_Gioi"));
            slKha += Convert.ToDouble(GetCurrentColumnValue("XLTN_Kha"));
            slTBK += Convert.ToDouble(GetCurrentColumnValue("XLTN_TBKha"));
            slTB += Convert.ToDouble(GetCurrentColumnValue("XLTN_TB"));
        }

        private void xrTableCell48_SummaryReset(object sender, EventArgs e)
        {

        }
    }
}
