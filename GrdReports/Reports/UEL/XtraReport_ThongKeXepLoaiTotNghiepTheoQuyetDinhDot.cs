using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_ThongKeXepLoaiTotNghiepTheoQuyetDinhDot : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtDecision = new DataTable(), dtOlogyDetail = new DataTable(), dtTotal=new DataTable();
        public XtraReport_ThongKeXepLoaiTotNghiepTheoQuyetDinhDot()
        {
            InitializeComponent();
        }
        public void Init_Report(DataSet tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            //this.DataSource = tbPrint;           
            dtDecision = tbPrint.Tables["Decision"].Copy();
            dtOlogyDetail = tbPrint.Tables["OlogyDetail"].Copy();
            dtTotal = tbPrint.Tables["Total"].Copy();
            lblNgayIn.Text = _NgayIn;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;

            //Tieu de
            lbl_title.Text = "THỐNG KÊ XẾP LOẠI TỐT NGHIỆP THEO NGÀNH ĐÀO TẠO ĐỢT TỐT NGHIỆP THÁNG " + dtDecision.Rows[0]["DotTotNghiepThang"].ToString()+" HỆ "+ dtDecision.Rows[0]["LoaiDaoTao"];
            lbl_DinhKemCongVanSo.Text = "(Đính kèm công văn số    /ĐHKTL-ĐT ngày    /   /      của Hiệu trưởng trường)";
            try
            {
                float _withH1 = 44F, _withH2 = 300F, _withH3 = 70F, _withH4 = 68F, _withH5 = 69.5F, _withH6 = 68F, _withH7 = 68F, _withH8 = 69.5F;
                //Detail
                XRTable detail = new XRTable();
                detail.BeginInit();
                Detail.Controls.Add(detail);
                detail.SuspendLayout();
                detail.Font = new System.Drawing.Font("Times New Roman", 12F);

                for (int j = 0; j < dtOlogyDetail.Rows.Count; j++)
                {
                    XRTableRow _drow = new XRTableRow();
                    _drow.Weight = 1D;
                    detail.Rows.Add(_drow);

                    XRTableCell d_cell1 = new XRTableCell();
                    d_cell1.RowSpan = 1;
                    d_cell1.WidthF = _withH1;
                    d_cell1.Text = dtOlogyDetail.Rows[j]["STT"].ToString();
                    d_cell1.Multiline = true;
                    d_cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                    detail.WidthF = d_cell1.WidthF;
                    d_cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell1);
                    // ==
                    XRTableCell d_cell2 = new XRTableCell();
                    d_cell2.RowSpan = 1;
                    d_cell2.WidthF = _withH2;
                    d_cell2.Text = dtOlogyDetail.Rows[j]["OlogyName"].ToString() + " (" + dtOlogyDetail.Rows[j]["OlogyID"].ToString()+")";
                    d_cell2.Multiline = true;
                    d_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell2.WidthF;
                    d_cell2.Font= new System.Drawing.Font("Times New Roman", 10F);
                    d_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    d_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell2);

                    //==
                    XRTableCell d_cell3 = new XRTableCell();
                    d_cell3.RowSpan = 1;
                    d_cell3.WidthF = _withH3;
                    d_cell3.Text = dtOlogyDetail.Rows[j]["TongSL"].ToString();
                    d_cell3.Multiline = true;
                    d_cell3.Font= new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
                    d_cell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell3.WidthF;
                    d_cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell3);
                    //==  

                    XRTableCell d_cell4 = new XRTableCell();
                    d_cell4.RowSpan = 1;
                    d_cell4.WidthF = _withH4;
                    d_cell4.Text = dtOlogyDetail.Rows[j]["TrungBinh"].ToString();
                    d_cell4.Multiline = true;
                    d_cell4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell4.WidthF;
                    d_cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell4);
                    //==                    

                    XRTableCell d_cell5 = new XRTableCell();
                    d_cell5.RowSpan = 1;
                    d_cell5.WidthF = _withH5;
                    d_cell5.Text = dtOlogyDetail.Rows[j]["TrungBinhKha"].ToString();
                    d_cell5.Multiline = true;
                    d_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell5.WidthF;
                    d_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell5);
                    //==                    
                    XRTableCell d_cell6 = new XRTableCell();
                    d_cell6.RowSpan = 1;
                    d_cell6.WidthF = _withH6;
                    d_cell6.Text = dtOlogyDetail.Rows[j]["Kha"].ToString();
                    d_cell6.Multiline = true;
                    d_cell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell6.WidthF;
                    d_cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell6);
                    //==                    
                    XRTableCell d_cell7 = new XRTableCell();
                    d_cell7.RowSpan = 1;
                    d_cell7.WidthF = _withH7;
                    d_cell7.Text = dtOlogyDetail.Rows[j]["Gioi"].ToString();
                    d_cell7.Multiline = true;
                    d_cell7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell7.WidthF;
                    d_cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell7);
                    //==                    
                    XRTableCell d_cell8 = new XRTableCell();
                    d_cell8.RowSpan = 1;
                    d_cell8.WidthF = _withH8;
                    d_cell8.Text = dtOlogyDetail.Rows[j]["XuatSac"].ToString();
                    d_cell8.Multiline = true;
                    d_cell8.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell8.WidthF;
                    d_cell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell8);
                    //==                                      

                }
                detail.PerformLayout();
                detail.AdjustSize();
                detail.EndInit();

                detail.HeightF = 40F;
                detail.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                detail.WidthF = 757L;

                //Total                  
                XRTable total = new XRTable();
                total.BeginInit();
                GroupFooter1.Controls.Add(total);
                total.SuspendLayout();
                total.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);

                XRTableRow _frow = new XRTableRow();
                _frow.Weight = 1D;
                total.Rows.Add(_frow);


                XRTableCell f_cell1 = new XRTableCell();
                f_cell1.RowSpan = 1;
                f_cell1.WidthF = _withH1 + _withH2;
                f_cell1.Text = "Tổng";
                f_cell1.Multiline = true;
                f_cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                GroupFooter1.WidthF = f_cell1.WidthF;
                f_cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell1);
                
                //==
                XRTableCell f_cell2 = new XRTableCell();
                f_cell2.RowSpan = 1;
                f_cell2.WidthF = _withH3;
                f_cell2.Text = dtDecision.Rows[0]["TongSL"].ToString();
                f_cell2.Multiline = true;
                f_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell2.WidthF;
                f_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell2);
                //==
                XRTableCell f_cell3 = new XRTableCell();
                f_cell3.RowSpan = 1;
                f_cell3.WidthF = _withH4;
                f_cell3.Text = dtTotal.Rows[0]["TongTB"].ToString();
                f_cell3.Multiline = true;
                f_cell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell3.WidthF;
                f_cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell3);
                //==

                XRTableCell f_cell4 = new XRTableCell();
                f_cell4.RowSpan = 1;
                f_cell4.WidthF = _withH5;
                f_cell4.Text = dtTotal.Rows[0]["TongTBK"].ToString();
                f_cell4.Multiline = true;
                f_cell4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right ;
                GroupFooter1.WidthF = f_cell4.WidthF;
                f_cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell4);
                //==
                XRTableCell f_cell5 = new XRTableCell();
                f_cell5.RowSpan = 1;
                f_cell5.WidthF = _withH6;
                f_cell5.Text = dtTotal.Rows[0]["TongKha"].ToString();
                f_cell5.Multiline = true;
                f_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell5.WidthF;
                f_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell5);
                //==
                XRTableCell f_cell6 = new XRTableCell();
                f_cell6.RowSpan = 1;
                f_cell6.WidthF = _withH7;
                f_cell6.Text = dtTotal.Rows[0]["TongGioi"].ToString();
                f_cell6.Multiline = true;
                f_cell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell6.WidthF;
                f_cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell6);
                //==
                XRTableCell f_cell7 = new XRTableCell();
                f_cell7.RowSpan = 1;
                f_cell7.WidthF = _withH8;
                f_cell7.Text = dtTotal.Rows[0]["TongXS"].ToString();
                f_cell7.Multiline = true;
                f_cell7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right ;
                GroupFooter1.WidthF = f_cell7.WidthF;
                f_cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell7);
                //==

                total.PerformLayout();
                total.AdjustSize();
                total.EndInit();

                total.HeightF = 40F;
                total.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                total.WidthF = 757L;
                Detail.HeightF = 40F;
                GroupFooter1.HeightF = 40F;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }

}

