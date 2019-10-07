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
    public partial class XtraReport_KetQuaXetTN_DNU : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint = new DataTable();
        public static DataTable dtPrint_goc = new DataTable();
        public static DataTable dtPrint_M1 = new DataTable();
        public static DataTable dtPrint_M2 = new DataTable();
        public static DataTable dtPrint_MonTN = new DataTable();
        double tongDauKhoa, tongXetThi, slTN, slHong, slHoanXet, slXS, slGioi, slKha, slTBK, slTB = 0;
        XtraReport rep;
        Band band;
        public XtraReport_KetQuaXetTN_DNU()
        {
            InitializeComponent();
        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _NgayThi, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txt_NgayKy.Text = _NgayIn;
            dtPrint = tbPrint;
            txtDVCQ.Text = _AdministrativeUnit;
            txtTenTruong.Text = _CollegeName;
            dtPrint_goc = tbPrint;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
            txtText.Text= "Đợt xét tốt nghiệp ngày "+_NgayThi;
            txtTieuDe.Text = "Đợt xét tốt nghiệp ngày " + _NgayThi;
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
            txtTongDauKhoa.Text = tongDauKhoa.ToString();
            txtXetThi.Text = tongXetThi.ToString();
            txtSLTN.Text = slTN.ToString();
            txtTLTN.Text = ((slTN * 100) / tongXetThi).ToString("0.00")+"%";
            txtSLHong.Text = slHong.ToString();
            txtTLHong.Text= ((slHong * 100) / tongXetThi).ToString("0.00") + "%";
            //txtSLHoanXet.Text = slHoanXet.ToString();
            //txtTLHoanXet.Text= ((slHoanXet * 100) / tongDauKhoa).ToString("0.0") + "%";
            txtSLG.Text = slXS.ToString();
            txtTLG.Text= ((slXS * 100) / slTN).ToString("0.00") + "%";
            txtSLK.Text = slGioi.ToString();
            txtTLK.Text= ((slGioi * 100) / slTN).ToString("0.00") + "%";
            txtSLTBK.Text = slKha.ToString();
            txtTLTBK.Text= ((slKha * 100) / slTN).ToString("0.00") + "%";
            txtSLTB.Text = slTB.ToString();
            txtTLTB.Text = ((slTB * 100) / slTN).ToString("0.00") + "%";
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
            //slHoanXet+= Convert.ToDouble(GetCurrentColumnValue("SLSVHoanXet"));
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
