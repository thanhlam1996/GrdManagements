using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using GrdReports.Reports.UEL;
using System.Drawing.Printing;
using GrdReports.Reports.UEL.Sub;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_BangDiemTotNghiepAnhVietA3 : DevExpress.XtraReports.UI.XtraReport
    {
        #region Variables
        public static DataTable dtPrint = new DataTable();
        public static DataTable dtPrint_GDTC = new DataTable();
        public static DataTable dtPrint_GDTC_TA = new DataTable();
        public static DataTable dtPrint_Rong = new DataTable();
        public static DataTable dtPrint_TA = new DataTable();
        public static DataTable dtPrint_Trai_Goc = new DataTable();
        public static DataTable dtPrint_Phai_Goc = new DataTable();
        public static DataTable dtPrint_Trai = new DataTable();
        public static DataTable dtPrint_Trai_Cot1 = new DataTable();
        public static DataTable dtPrint_Trai_Cot2 = new DataTable();
        public static DataTable dtPrint_Phai = new DataTable();
        public static DataTable dtPrint_Phai_Cot1 = new DataTable();
        public static DataTable dtPrint_Phai_Cot2 = new DataTable();
        public static DataTable dtPrint_TiengAnh_GDTC = new DataTable();
        public static DataTable dtPrint_TiengViet_GDTC = new DataTable();

        #endregion

        #region public XtraReport_BangDiemTotNghiepAnhVietA3()
        public XtraReport_BangDiemTotNghiepAnhVietA3()
        {
            InitializeComponent();
        }
        #endregion

        #region InitReports
        public void Init_Report(DataTable _dtPrints, DataTable _dtPrints_TA, string ngayIn, string _ngayIn_TA, string _CapBacNguoiKyTenDongDau, string _nguoiKyTen, string _nguoiKy_TA)
        {
            try
            {
                base.DataSource = _dtPrints;
                this.txt_NgayIn.Text = ngayIn;
                txt_NguoiKy_TA.Text = string.Empty;
                txt_KyTenDongDau.Text = _nguoiKyTen.Replace(@"#", "\n");
                txt_CapBac.Text = _CapBacNguoiKyTenDongDau.Replace(@"#", "\n");
                txt_NgayIn_TA.Text = _ngayIn_TA;
                txt_NguoiKy_TA.Text = _nguoiKy_TA.Replace(@"#", "\n");
                

                dtPrint_Trai_Goc = _dtPrints_TA;
                dtPrint_Phai_Goc = _dtPrints;
                dtPrint_Phai = dtPrint_Rong.Clone();
                dtPrint_TiengViet_GDTC = dtPrint_Rong.Clone();
                dtPrint_Trai = dtPrint_Rong.Clone();
                dtPrint_TiengAnh_GDTC = dtPrint_Rong.Clone();
                dtPrint_Phai.Columns.Add("STT", typeof(string));
                dtPrint_Phai.Columns.Add("MaMH", typeof(string));
                dtPrint_Phai.Columns.Add("TenMH", typeof(string));
                dtPrint_Phai.Columns.Add("SoTC", typeof(string));
                dtPrint_Phai.Columns.Add("DiemSo", typeof(string));
                dtPrint_Phai.Columns.Add("HeChu", typeof(string));
                dtPrint_TiengViet_GDTC.Columns.Add("TenMonHoc", typeof(string));
                dtPrint_TiengViet_GDTC.Columns.Add("SoTC", typeof(string));
                dtPrint_TiengViet_GDTC.Columns.Add("DiemSo", typeof(string));
                dtPrint_Trai.Columns.Add("STT", typeof(string));
                dtPrint_Trai.Columns.Add("MaMH", typeof(string));
                dtPrint_Trai.Columns.Add("TenMH", typeof(string));
                dtPrint_Trai.Columns.Add("SoTC", typeof(string));
                dtPrint_Trai.Columns.Add("DiemSo", typeof(string));
                dtPrint_Trai.Columns.Add("HeChu", typeof(string));
                dtPrint_TiengAnh_GDTC.Columns.Add("TenMonHoc", typeof(string));
                dtPrint_TiengAnh_GDTC.Columns.Add("SoTC", typeof(string));
                dtPrint_TiengAnh_GDTC.Columns.Add("DiemSo", typeof(string));
            }
            catch { }         

        }
        #endregion

        #region xrSubreport_BeforePrint
        private void xrSubreport_TiengAnh_Trai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_TiengAnh_Phai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_TiengViet_Trai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_TiengViet_Phai_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }


        private void xrSubreport_GDTC_TA_BeforePrint(object sender, PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_GDTC_BeforePrint(object sender, PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }
        #endregion

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                DataRow row2;
                DataRow row3;
                dtPrint = dtPrint_Phai_Goc.Clone();
                dtPrint_Phai_Goc.Select("STTSV = '" + this.txt_MaSV.Summary.GetResult().ToString() + "' and NhomMonHoc <> 'PC'").CopyToDataTable<DataRow>(dtPrint, LoadOption.OverwriteChanges);
                DataView defaultView = new DataView();
                defaultView = dtPrint.DefaultView;
                defaultView.Sort = "ID asc";
                dtPrint = defaultView.ToTable();
                dtPrint_GDTC = dtPrint_Phai_Goc.Clone();
                dtPrint_Phai_Goc.Select("STTSV = '" + this.txt_MaSV.Summary.GetResult().ToString() + "' and NhomMonHoc = 'PC'").CopyToDataTable<DataRow>(dtPrint_GDTC, LoadOption.OverwriteChanges);
                dtPrint_TA = dtPrint_Trai_Goc.Clone();
                dtPrint_Trai_Goc.Select("STTSV = '" + this.txt_MaSV.Summary.GetResult().ToString() + "' and NhomMonHoc <> 'PC'").CopyToDataTable<DataRow>(dtPrint_TA, LoadOption.OverwriteChanges);
                DataView view2 = new DataView();
                view2 = dtPrint_TA.DefaultView;
                view2.Sort = "ID asc";
                dtPrint_TA = view2.ToTable();
                dtPrint_GDTC_TA = dtPrint_Trai_Goc.Clone();
                dtPrint_Trai_Goc.Select("STTSV = '" + this.txt_MaSV.Summary.GetResult().ToString() + "' and NhomMonHoc = 'PC'").CopyToDataTable<DataRow>(dtPrint_GDTC_TA, LoadOption.OverwriteChanges);
                int num = 0;
                int count = 0;
                count = dtPrint.Rows.Count;
                if ((count % 2) == 0)
                {
                    num = count / 2;
                }
                else
                {
                    num = (count / 2) + 1;
                }
                dtPrint_Trai_Cot1 = dtPrint_Trai.Clone();
                dtPrint_Trai_Cot2 = dtPrint_Trai.Clone();
                dtPrint_Phai_Cot1 = dtPrint_Phai.Clone();
                dtPrint_Phai_Cot2 = dtPrint_Phai.Clone();
                int num3 = 1;
                DataTable table = dtPrint.Copy();
                foreach (DataRow row in table.Rows)
                {
                    if (num3 <= num)
                    {
                        row2 = dtPrint_Phai_Cot1.NewRow();
                        row2["STT"] = num3;
                        row2["MaMH"] = row["MaMonHoc"].ToString();
                        row2["TenMH"] = row["TenMonHoc"].ToString();
                        row2["SoTC"] = row["SoTinChi"].ToString();
                        row2["DiemSo"] = row["DiemSo"].ToString();
                        row2["HeChu"] = row["DiemChu"].ToString();
                        dtPrint_Phai_Cot1.Rows.Add(row2);
                    }
                    else
                    {
                        row3 = dtPrint_Phai_Cot2.NewRow();
                        row3["STT"] = num3;
                        row3["MaMH"] = row["MaMonHoc"].ToString();
                        row3["TenMH"] = row["TenMonHoc"].ToString();
                        row3["SoTC"] = row["SoTinChi"].ToString();
                        row3["DiemSo"] = row["DiemSo"].ToString();
                        row3["HeChu"] = row["DiemChu"].ToString();
                        dtPrint_Phai_Cot2.Rows.Add(row3);
                    }
                    num3++;
                }
                table = dtPrint_GDTC.Copy();
                dtPrint_TiengViet_GDTC.Clear();
                foreach (DataRow row in table.Select("NhomMonHoc = 'PC'"))
                {
                    row2 = dtPrint_TiengViet_GDTC.NewRow();
                    row2["TenMonHoc"] = row["TenMonHoc"].ToString();
                    row2["SoTC"] = row["SoTinChi"].ToString();
                    row2["DiemSo"] = row["DiemSo"].ToString();
                    dtPrint_TiengViet_GDTC.Rows.Add(row2);
                }
                num3 = 1;
                table = dtPrint_TA.Copy();
                foreach (DataRow row in table.Rows)
                {
                    if (num3 <= num)
                    {
                        row2 = dtPrint_Trai_Cot1.NewRow();
                        row2["STT"] = num3;
                        row2["MaMH"] = row["MaMonHoc"].ToString();
                        row2["TenMH"] = row["TenMonHoc"].ToString();
                        row2["SoTC"] = row["SoTinChi"].ToString();
                        row2["DiemSo"] = row["DiemSo"].ToString();
                        row2["HeChu"] = row["DiemChu"].ToString();
                        dtPrint_Trai_Cot1.Rows.Add(row2);
                    }
                    else
                    {
                        row3 = dtPrint_Trai_Cot2.NewRow();
                        row3["STT"] = num3;
                        row3["MaMH"] = row["MaMonHoc"].ToString();
                        row3["TenMH"] = row["TenMonHoc"].ToString();
                        row3["SoTC"] = row["SoTinChi"].ToString();
                        row3["DiemSo"] = row["DiemSo"].ToString();
                        row3["HeChu"] = row["DiemChu"].ToString();
                        dtPrint_Trai_Cot2.Rows.Add(row3);
                    }
                    num3++;
                }

                table = dtPrint_GDTC_TA.Copy();
                dtPrint_TiengAnh_GDTC.Clear();
                foreach (DataRow row in table.Select("NhomMonHoc = 'PC'"))
                {
                    row2 = dtPrint_TiengAnh_GDTC.NewRow();
                    row2["TenMonHoc"] = row["TenMonHoc"].ToString();
                    row2["SoTC"] = row["SoTinChi"].ToString();
                    row2["DiemSo"] = row["DiemSo"].ToString();
                    dtPrint_TiengAnh_GDTC.Rows.Add(row2);
                }

                this.xrSubreport_TiengAnh_Trai.ReportSource = new XtraReport_BangDiemTNA3_TiengAnh_Trai();
                this.xrSubreport_TiengAnh_Trai.BeforePrint += new PrintEventHandler(this.xrSubreport_TiengAnh_Trai_BeforePrint);

                this.xrSubreport_TiengAnh_Phai.ReportSource = new XtraReport_BangDiemTNA3_TiengAnh_Phai();
                this.xrSubreport_TiengAnh_Trai.BeforePrint += new PrintEventHandler(this.xrSubreport_TiengAnh_Phai_BeforePrint);

                this.xrSubreport_GDTC_TA.ReportSource = new XtraReport_GDTC_TiengAnh();
                this.xrSubreport_GDTC_TA.BeforePrint += new PrintEventHandler(this.xrSubreport_GDTC_TA_BeforePrint);

                this.xrSubreport_TiengViet_Trai.ReportSource = new XtraReport_BangDiemTNA3_TiengViet_Trai();
                this.xrSubreport_TiengViet_Trai.BeforePrint += new PrintEventHandler(this.xrSubreport_TiengViet_Trai_BeforePrint);

                this.xrSubreport_TiengViet_Phai.ReportSource = new XtraReport_BangDiemTNA3_TiengViet_Phai();
                this.xrSubreport_TiengViet_Phai.BeforePrint += new PrintEventHandler(this.xrSubreport_TiengViet_Phai_BeforePrint);

                this.xrSubreport_GDTC.ReportSource = new XtraReport_GDTC_TiengViet();
                this.xrSubreport_GDTC.BeforePrint += new PrintEventHandler(this.xrSubreport_GDTC_BeforePrint);

            }
            catch { }
        }



    }
}


