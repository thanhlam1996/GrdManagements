using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;
using GrdReports.Reports;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTotNghiepDayDu_DNU1 : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint = new DataTable();
        public static DataTable dtPrint_goc = new DataTable();
        public static DataTable dtPrint_M1 = new DataTable();
        public static DataTable dtPrint_M2 = new DataTable();
        public static DataTable dtPrint_MonTN = new DataTable();
        public XtraReport_BangDiemTotNghiepDayDu_DNU1()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy)
        {
            this.DataSource = tbPrint;
            txtNgayKy.Text = _NgayIn;
            dtPrint = tbPrint;
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

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                //dtPrint_goc.Select("MaSV like '" + "%" + txt_MaSV.Summary.GetResult().ToString() + "'").CopyToDataTable(dtPrint, LoadOption.OverwriteChanges);

                //dtPrint_MonTN = dtPrint.Clone();
                //dtPrint.Select("MonThiTN = 'x'").CopyToDataTable(dtPrint_MonTN, LoadOption.OverwriteChanges);

                int k = 0;
                int _soDong = 0;
                _soDong = dtPrint.Rows.Count;

                if (_soDong % 2 == 0)
                    k = _soDong / 2;
                else
                    k = _soDong / 2 + 1;

                dtPrint_M1 = dtPrint.Clone();
                dtPrint_M2 = dtPrint.Clone();

                int i = 0;
                foreach (DataRow dr in dtPrint.Select("MonThiTN <> 'x'"))
                {
                    if (i < k)
                    {
                        DataRow dr1 = dtPrint_M1.NewRow();
                        dr1["STT"] = i + 1;
                        dr1["TenMonHoc"] = dr["TenMonHoc"].ToString();
                        dr1["SoTinChi"] = dr["SoTinChi"].ToString();
                        dr1["DiemSo"] = dr["DiemSo"].ToString();
                        dtPrint_M1.Rows.Add(dr1);
                    }
                    else
                    {
                        DataRow dr2 = dtPrint_M2.NewRow();
                        dr2["STT"] = i + 1; //dr["STT"].ToString();
                        dr2["TenMonHoc"] = dr["TenMonHoc"].ToString();
                        dr2["SoTinChi"] = dr["SoTinChi"].ToString();
                        dr2["DiemSo"] = dr["DiemSo"].ToString();
                        dtPrint_M2.Rows.Add(dr2);
                    }
                    i++;
                }
                xrSubreport1.ReportSource = new XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport1();
                xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport1_BeforePrint);

                xrSubreport2.ReportSource = new XtraReport_BangDiemTotNghiepDayDu_DNU_SubReport2();
                xrSubreport2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport2_BeforePrint);
            }
            catch { }
        }
    }
}
