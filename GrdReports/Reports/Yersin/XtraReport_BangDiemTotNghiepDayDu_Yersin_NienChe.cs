using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using GrdReports.Reports.Yersin.Sub;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtPrint_goc = new DataTable();
        public static DataTable dt1 = new DataTable();
        public static DataTable dt2 = new DataTable();
        public XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            dtPrint_goc = tbPrint;
            txt_NgayKy.Text = _NgayIn;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
            xrLabel_NguoiLap.Text = _NguoiLap;
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                DataTable dtPrint = dtPrint_goc.Clone();
                dtPrint_goc.Select("STT = '" + xrLabel_MaSV.Summary.GetResult().ToString() + "'").CopyToDataTable(dtPrint, LoadOption.OverwriteChanges);

                //Cat bang
                int i = dtPrint.Rows.Count;
                if (i > 0)
                {
                    if (i % 2 > 0)
                    {
                        dt1 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon <= " + Convert.ToString(i / 2 + 1)).CopyToDataTable(dt1, LoadOption.OverwriteChanges);

                        dt2 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon > " + Convert.ToString(i / 2 + 1)).CopyToDataTable(dt2, LoadOption.OverwriteChanges);
                    }
                    else
                    {
                        dt1 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon <= " + Convert.ToString(i / 2)).CopyToDataTable(dt1, LoadOption.OverwriteChanges);

                        dt2 = dtPrint.Clone();
                        dtPrint.Copy().Select("STT_Mon > " + Convert.ToString(i / 2)).CopyToDataTable(dt2, LoadOption.OverwriteChanges);
                    }
                }
                else
                {
                    dt1.Clear();
                    dt2.Clear();
                    return;
                }

                xrSubreport_1.ReportSource = new SubXtraReport_BangDiemNienChe_1();
                xrSubreport_1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport_1_BeforePrint);

                xrSubreport_2.ReportSource = new SubXtraReport_BangDiemNienChe_2();
                xrSubreport_2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport_2_BeforePrint);
            }
            catch (Exception ex) { }
        }

        private void xrSubreport_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }
    }
}
