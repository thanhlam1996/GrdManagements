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
    public partial class XtraReport_BangDiemTotNghiepDayDu_DNU : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint = new DataTable();
        public static DataTable dtPrint_goc = new DataTable();
        public static DataTable dtPrint_M1 = new DataTable();
        public static DataTable dtPrint_M2 = new DataTable();
        public static DataTable dtPrint_MonTN = new DataTable();
        XtraReport rep;
        Band band;
        public XtraReport_BangDiemTotNghiepDayDu_DNU()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            txt_NgayKy.Text = _NgayIn;
            dtPrint = tbPrint;
            txtDVCQ.Text = _AdministrativeUnit;
            txtTenTruong.Text = _CollegeName.ToUpper();
            dtPrint_goc = tbPrint;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
        }
       
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        { 
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                DataTable dtPrint = dtPrint_goc.Clone();
                dtPrint_goc.Select("STT_SV = '" + txt_MaSV.Summary.GetResult().ToString() + "'").CopyToDataTable(dtPrint, LoadOption.OverwriteChanges);

                //Cat bang
                int i = dtPrint.Rows.Count;
                if (i > 0)
                {
                    if (i % 2 > 0)
                    {
                        dtPrint_M1 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon <= " + Convert.ToString(i / 2 + 1)).CopyToDataTable(dtPrint_M1, LoadOption.OverwriteChanges);

                        dtPrint_M2 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon > " + Convert.ToString(i / 2 + 1)).CopyToDataTable(dtPrint_M2, LoadOption.OverwriteChanges);
                    }
                    else
                    {
                        dtPrint_M1 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon <= " + Convert.ToString(i / 2)).CopyToDataTable(dtPrint_M1, LoadOption.OverwriteChanges);

                        dtPrint_M2 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon > " + Convert.ToString(i / 2)).CopyToDataTable(dtPrint_M2, LoadOption.OverwriteChanges);
                    }
                }
                else
                {
                    dtPrint_M1.Clear();
                    dtPrint_M2.Clear();
                    return;
                }

                xrSubreport1.ReportSource = new XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport1();
                xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport1_BeforePrint);

                xrSubreport2.ReportSource = new XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport2();
                xrSubreport2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport2_BeforePrint);
            }
            catch (Exception ex) { }
        }

 
    }
}
