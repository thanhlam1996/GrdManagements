using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports.Reports
{
    public partial class XtraReport_BangDiemTNCaNhan_2Cot_DHDN : DevExpress.XtraReports.UI.XtraReport
    {
        public static DataTable dtPrint = new DataTable();
        public static DataTable dtPrint_goc = new DataTable();
        public static DataTable dtPrint_M1=new DataTable();
        public static DataTable dtPrint_M2=new DataTable();
        public static DataTable dtPrint_MonTN = new DataTable();

        public XtraReport_BangDiemTNCaNhan_2Cot_DHDN()
        {
            InitializeComponent();
        }
        //tbPrint, _NgayIn, _CapBac, _NguoiKy, _NguoiLap, _AdministrativeUnit, _CollegeName
        public void Init_Report(DataTable _dtPrints, string ngayIn, string _CapBacNguoiKyTenDongDau, string _nguoiKyTen, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = _dtPrints;

            //txt_AdministrativeUnit.Text = _AdministrativeUnit.ToUpper();
            //txt_CollegeName.Text = _CollegeName.ToUpper();

          //  string _ngayIn = string.Empty, _thangIn = string.Empty, _namIn = DateTime.Now.Year.ToString();

            //if (DateTime.Now.Month < 10)
            //    _thangIn = "0" + DateTime.Now.Month.ToString();
            //else
            //    _thangIn = DateTime.Now.Month.ToString();

            //if (DateTime.Now.Day < 10)
            //    _ngayIn = "0" + DateTime.Now.Day.ToString();
            //else
            //    _ngayIn = DateTime.Now.Day.ToString();

            txt_NgayIn.Text = ngayIn;
            txt_CapBac.Text = _CapBacNguoiKyTenDongDau;
             txt_nguoiKy.Text = _nguoiKyTen;

            dtPrint_goc = _dtPrints;
            dtPrint_goc.Columns.Add("STT", typeof(int));
        }

        private void xrSubreport_Mat1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_Mat2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void xrSubreport_thiTotNghiep_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRSubreport subreport = sender as XRSubreport;
            XtraReport mainReport = subreport.Report as XtraReport;
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                dtPrint = dtPrint_goc.Clone();
                 dtPrint_goc.Select("STT_SV = '" + txt_MaSV.Summary.GetResult().ToString() + "'").CopyToDataTable(dtPrint, LoadOption.OverwriteChanges);              

                dtPrint_MonTN = dtPrint.Clone();
                dtPrint.Select("MonThiTN = 'x'").CopyToDataTable(dtPrint_MonTN, LoadOption.OverwriteChanges);

                DataTable dtPrintCopy = new DataTable();
                dtPrintCopy.Columns.Add("STT", typeof(int));
                dtPrintCopy.Columns.Add("MaMonHoc", typeof(string));
                dtPrintCopy.Columns.Add("TenMonHoc", typeof(string));
                dtPrintCopy.Columns.Add("SoTinChi", typeof(int));
                dtPrintCopy.Columns.Add("DiemSo", typeof(decimal));
                dtPrintCopy.Columns.Add("Diem4", typeof(decimal));

                dtPrint_M1 = dtPrintCopy.Clone();
                dtPrint_M2 = dtPrintCopy.Clone();

                int k = 0;
                int _soDong = 0;
                _soDong = dtPrint.Rows.Count;

                if (dtPrint_MonTN.Rows.Count > 0)
                {
                    xrSubreport_thiTotNghiep.Visible = true;
                    _soDong = _soDong + 1;
                }
                else
                    xrSubreport_thiTotNghiep.Visible = false;

                if (_soDong % 2 == 0)
                    k = _soDong / 2;
                else
                    k = _soDong / 2 + 1;
                

                int i = 1;

                DataTable dtTemp = dtPrint.Clone();
                dtPrint.Select("MonThiTN <> 'x' or MonThiTN is null").CopyToDataTable(dtTemp, LoadOption.OverwriteChanges);

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        if (i <= k)
                        {
                            DataRow dr1 = dtPrint_M1.NewRow();
                            dr1["STT"] = i;
                            dr1["MaMonHoc"] = dr["MaMonHoc"].ToString();
                            dr1["TenMonHoc"] = dr["TenMonHoc"].ToString();
                            dr1["SoTinChi"] = dr["SoTinChi"].ToString();
                            dr1["DiemSo"] = dr["DiemSo"].ToString();
                            dr1["Diem4"] = dr["Diem4"].ToString();
                            dtPrint_M1.Rows.Add(dr1);
                        }
                        else
                        {
                            DataRow dr2 = dtPrint_M2.NewRow();
                            dr2["STT"] = i;
                            dr2["MaMonHoc"] = dr["MaMonHoc"].ToString();
                            dr2["TenMonHoc"] = dr["TenMonHoc"].ToString();
                            dr2["SoTinChi"] = dr["SoTinChi"].ToString();
                            dr2["DiemSo"] = dr["DiemSo"].ToString();
                            dr2["Diem4"] = dr["Diem4"].ToString();
                            dtPrint_M2.Rows.Add(dr2);
                        }

                        i++;
                    }

                    foreach (DataRow dr in dtPrint_MonTN.Rows)
                    {
                        dr["STT"] = i;
                        i++;
                    }
                
               
                
                xrSubreport_Mat1.ReportSource = new Sub_XtraReport_BangDiem2Cot_Mat1_DHDN();
                xrSubreport_Mat1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport_Mat1_BeforePrint);

                xrSubreport_Mat2.ReportSource = new Sub_XtraReport_BangDiem2Cot_Mat2_DHDN();
                xrSubreport_Mat2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport_Mat2_BeforePrint);

                xrSubreport_thiTotNghiep.ReportSource = new Sub_XtraReport_BangDiem2Cot_ThiTotNghiep_DHDN();
                xrSubreport_thiTotNghiep.BeforePrint += new System.Drawing.Printing.PrintEventHandler(xrSubreport_thiTotNghiep_BeforePrint);
            }
            catch(Exception ex) {}
        }
    }
}
