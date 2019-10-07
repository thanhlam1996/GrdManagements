using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_BienBanKiemKeTinhSuDungVanBang : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtDecision = new DataTable(), dtDecisionDetail = new DataTable(), dtTotal=new DataTable();
        int _totalconlai = 0;
        public XtraReport_BienBanKiemKeTinhSuDungVanBang()
        {
            InitializeComponent();
        }
        public void Init_Report(DataSet tbPrint,DataTable _dtSluong, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName, string _Nguoilapbang)
        {
            //this.DataSource = tbPrint;           
            dtDecisionDetail = tbPrint.Tables["DecisionDetail"].Copy();
            dtDecision = tbPrint.Tables["Decision"].Copy();
            dtTotal = tbPrint.Tables["Total"].Copy();
            lblNgayIn.Text = _NgayIn;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;
            lbl_Nguoilapbang.Text = _Nguoilapbang;

            //Tieu de
            lbl_title.Text = "BIÊN BẢN KIỂM KÊ TÌNH HÌNH SỬ DỤNG VĂN BẰNG HỆ CHÍNH QUY";//+ dtDecision.Rows[0]["LoaiDaoTao"];
            lbl_DinhKemCongVanSo.Text = "(Đính kèm công văn số    /ĐHKTL-ĐT ngày    /   /      của Hiệu trưởng trường)";
            try
            {
                xrTable_titleQD.Rows.Clear();             
                float _withQDtitle = xrTable_titleQD.SizeF.Width;//with tong
                float _subWithQDtitle = _withQDtitle; //sub
                if(dtDecision.Rows.Count>0)
                {
                    _subWithQDtitle = _withQDtitle / dtDecision.Rows.Count;
                }

                float w1 = 45F, w2 = 170F, w3 = 70F, w4 = 70F, w6 = 71F, w7 = 82F, w8 = 97F;
                
                XRTableRow _trow = new XRTableRow();
                _trow.Weight = 1D;
                xrTable_titleQD.Rows.Add(_trow);

                XRTableRow _drow = new XRTableRow();
                _drow.Weight = 1D;
                xrTable_titleQD.Rows.Add(_drow);

                XRTableCell t_cell2 = new XRTableCell();
                t_cell2.RowSpan = 1;
                t_cell2.WidthF = _withQDtitle;
                t_cell2.Text = "SL đã in theo";
                t_cell2.Multiline = true;
                t_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom|DevExpress.XtraPrinting.BorderSide.Right;
                GroupHeader1.WidthF = t_cell2.WidthF;
                t_cell2.Font = new System.Drawing.Font("Times New Roman", 10F, FontStyle.Bold);
                t_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0);
                t_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            
                ((XRTableRow)xrTable_titleQD.Rows[0]).Cells.Add(t_cell2);

                if(dtDecision.Rows.Count>0)
                {
                    for (int i = 0; i < dtDecision.Rows.Count; i++)
                    {
                        XRTableCell d_cell2 = new XRTableCell();
                        d_cell2.RowSpan = 1;
                        d_cell2.Text = dtDecision.Rows[i]["QuyetDinh"].ToString();
                        d_cell2.Multiline = true;
                        d_cell2.CanGrow = false;
                        d_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                        GroupHeader1.WidthF = d_cell2.WidthF;
                        d_cell2.Font = new System.Drawing.Font("Times New Roman", 9F);
                        d_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                        d_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        d_cell2.WidthF = _subWithQDtitle;
                        ((XRTableRow)xrTable_titleQD.Rows[1]).Cells.Add(d_cell2);
                    }
                }
                else
                {
                    XRTableCell d_cell2 = new XRTableCell();
                    d_cell2.RowSpan = 1;
                    d_cell2.Text = "";
                    d_cell2.Multiline = true;
                    d_cell2.CanGrow = false;
                    d_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                    GroupHeader1.WidthF = d_cell2.WidthF;
                    d_cell2.Font = new System.Drawing.Font("Times New Roman", 9F);
                    d_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    d_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    d_cell2.WidthF = _subWithQDtitle;
                    ((XRTableRow)xrTable_titleQD.Rows[1]).Cells.Add(d_cell2);
                }
                xrTable_titleQD.Rows[0].HeightF = 20F;
                xrTable_titleQD.Rows[1].HeightF = 61.5F;

                foreach (XRTableCell cell in xrTable_titleQD.Rows[1].Cells)
                {
                    cell.WidthF = _subWithQDtitle;
                }


                //Detail
                int tong = 0;
                XRTable detail = new XRTable();
                detail.BeginInit();
                Detail.Controls.Add(detail);
                detail.SuspendLayout();
                detail.Font = new System.Drawing.Font("Times New Roman", 12F);

                for (int j = 0; j < dtTotal.Rows.Count; j++)
                {
                    XRTableRow _row = new XRTableRow();
                    _row.Weight = 1D;
                    detail.Rows.Add(_row);
                    // ==
                    XRTableCell d_cell1 = new XRTableCell();
                    d_cell1.RowSpan = 1;
                    d_cell1.WidthF = w1;
                    d_cell1.Text = dtTotal.Rows[j]["STT"].ToString();
                    d_cell1.Multiline = true;
                    d_cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                    detail.WidthF = d_cell1.WidthF;
                    d_cell1.Font = new System.Drawing.Font("Times New Roman", 10F);
                    //d_cell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    d_cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell1);

                    //==
                    XRTableCell d_cell2 = new XRTableCell();
                    d_cell2.RowSpan = 1;
                    d_cell2.WidthF = w2;
                    d_cell2.Text = dtTotal.Rows[j]["DiplomasTypeName"].ToString();
                    d_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0);
                    d_cell2.Multiline = true;
                    d_cell2.Font = new System.Drawing.Font("Times New Roman", 10F);
                    d_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell2.WidthF;
                    d_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell2);
                    //==  

                    XRTableCell d_cell3 = new XRTableCell();
                    d_cell3.RowSpan = 1;
                    d_cell3.WidthF = w3;
                    d_cell3.Text = dtTotal.Rows[j]["SLTon"].ToString();
                    d_cell3.Multiline = true;
                    d_cell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell3.WidthF;
                    d_cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell3);
                    //==                    

                    XRTableCell d_cell4 = new XRTableCell();
                    d_cell4.RowSpan = 1;
                    d_cell4.WidthF = w4;
                    d_cell4.Text = dtTotal.Rows[j]["TongSL"].ToString();
                    d_cell4.Multiline = true;
                    d_cell4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell4.WidthF;
                    d_cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell4);
                    //==                    
                    if(dtDecision.Rows.Count>0)
                    {
                        for (int k = 0; k < dtDecision.Rows.Count; k++)
                        {
                            XRTableCell d_cell5 = new XRTableCell();
                            d_cell5.RowSpan = 1;
                            d_cell5.WidthF = _subWithQDtitle;
                            string tamp = string.Empty;
                            for (int i = 0; i < dtDecisionDetail.Rows.Count; i++)
                            {
                                if (dtDecisionDetail.Rows[i]["DiplomasTypeID"].ToString() == dtTotal.Rows[j]["DiplomasTypeID"].ToString())
                                {
                                    if (dtDecisionDetail.Rows[i]["DecisionNumber"].ToString() == dtDecision.Rows[k]["DecisionNumber"].ToString())
                                    {
                                        tamp = dtDecisionDetail.Rows[i]["SLType"].ToString();
                                    }
                                }
                            }
                            if (tamp != string.Empty)
                            {
                                d_cell5.Text = tamp;
                            }
                            else
                            {
                                d_cell5.Text = "0";
                            }
                            //  d_cell5.Text = "";
                            d_cell5.Multiline = true;
                            d_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                            detail.WidthF = d_cell5.WidthF;
                            d_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell5);
                        }
                    }
                    else
                    {
                        XRTableCell d_cell5 = new XRTableCell();
                        d_cell5.RowSpan = 1;
                        d_cell5.WidthF = _subWithQDtitle;
                        //string tamp = string.Empty;
                        //for (int i = 0; i < dtDecisionDetail.Rows.Count; i++)
                        //{
                        //    if (dtDecisionDetail.Rows[i]["DiplomasTypeID"].ToString() == dtTotal.Rows[j]["DiplomasTypeID"].ToString())
                        //    {
                        //        if (dtDecisionDetail.Rows[i]["DecisionNumber"].ToString() == dtDecision.Rows[k]["DecisionNumber"].ToString())
                        //        {
                        //            tamp = dtDecisionDetail.Rows[i]["SLType"].ToString();
                        //        }
                        //    }
                        //}
                        //if (tamp != string.Empty)
                        //{
                        //    d_cell5.Text = tamp;
                        //}
                       // else
                      //  {
                            d_cell5.Text = "0";
                       // }
                        //  d_cell5.Text = "";
                        d_cell5.Multiline = true;
                        d_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                        detail.WidthF = d_cell5.WidthF;
                        d_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell5);
                    }
                    //==                    
                    XRTableCell d_cell6 = new XRTableCell();
                    d_cell6.RowSpan = 1;
                    d_cell6.WidthF = w6;
                    d_cell6.Text = dtTotal.Rows[j]["SLHuy"].ToString();
                    d_cell6.Multiline = true;
                    d_cell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell6.WidthF;
                    d_cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell6);
                    //==                    
                    XRTableCell d_cell7 = new XRTableCell();
                    d_cell7.RowSpan = 1;
                    d_cell7.WidthF = w7;

                    tong = int.Parse(_row.Cells[2].Text) + int.Parse(_row.Cells[3].Text);
                    int tong2 = 0;
                    for (int i = 4; i < 3 + dtDecision.Rows.Count; i++)
                    {
                        tong2 += int.Parse(_row.Cells[i].Text);
                    }
                    _totalconlai += (tong - (tong2 + int.Parse(_row.Cells[4 + dtDecision.Rows.Count].Text)));
                    d_cell7.Text = (tong-(tong2 + int.Parse(_row.Cells[4 + dtDecision.Rows.Count].Text))).ToString();
                    //d_cell7.Text = "";
                    d_cell7.Multiline = true;
                    d_cell7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell7.WidthF;
                    d_cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell7);
                    //==       
                    XRTableCell d_cell8 = new XRTableCell();
                    d_cell8.RowSpan = 1;
                    d_cell8.WidthF = w8;
                    d_cell8.Text = _dtSluong.Rows[j]["Quantity"].ToString();
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
                detail.WidthF = 1065L;

                ////Total                  
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
                f_cell1.WidthF = w1 + w2;
                f_cell1.Text = "Tổng";
                f_cell1.Multiline = true;
                f_cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                GroupFooter1.WidthF = f_cell1.WidthF;
                f_cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell1);

                //==
                XRTableCell f_cell2 = new XRTableCell();
                f_cell2.RowSpan = 1;
                f_cell2.WidthF = w3;
                tong = 0;
                for (int i = 0; i < dtTotal.Rows.Count; i++)
                {
                    tong += int.Parse(dtTotal.Rows[i]["SLTon"].ToString());
                }
                f_cell2.Text = tong.ToString();
                f_cell2.Multiline = true;
                f_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell2.WidthF;
                f_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell2);
                //==
                XRTableCell f_cell3 = new XRTableCell();
                f_cell3.RowSpan = 1;
                tong = 0;
                for (int i = 0; i < dtTotal.Rows.Count; i++)
                {
                    tong += int.Parse(dtTotal.Rows[i]["TongSL"].ToString());
                }
                f_cell3.WidthF = w4;
                f_cell3.Text = tong.ToString();
                f_cell3.Multiline = true;
                f_cell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell3.WidthF;
                f_cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell3);
                //==

                if(dtDecision.Rows.Count>0)
                {
                    for (int k = 0; k < dtDecision.Rows.Count; k++)
                    {
                        XRTableCell d_cell5 = new XRTableCell();
                        d_cell5.RowSpan = 1;
                        d_cell5.WidthF = _subWithQDtitle;
                        tong = 0;

                        for (int i = 0; i < dtDecisionDetail.Rows.Count; i++)
                        {
                            if (dtDecisionDetail.Rows[i]["DecisionNumber"].ToString() == dtDecision.Rows[k]["DecisionNumber"].ToString())
                            {
                                tong += int.Parse(dtDecisionDetail.Rows[i]["SLType"].ToString());
                            }
                        }
                        d_cell5.Text = tong.ToString();
                        d_cell5.Multiline = true;
                        d_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        GroupFooter1.WidthF = d_cell5.WidthF;
                        d_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        ((XRTableRow)total.Rows[0]).Cells.Add(d_cell5);
                    }
                }
                else
                {
                    XRTableCell d_cell5 = new XRTableCell();
                    d_cell5.RowSpan = 1;
                    d_cell5.WidthF = _subWithQDtitle;
                    d_cell5.Text = "0";
                    d_cell5.Multiline = true;
                    d_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    GroupFooter1.WidthF = d_cell5.WidthF;
                    d_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    ((XRTableRow)total.Rows[0]).Cells.Add(d_cell5);
                }

       
                //==
                XRTableCell f_cell5 = new XRTableCell();
                f_cell5.RowSpan = 1;
                f_cell5.WidthF = w6;
                tong = 0;
                for (int i = 0; i < dtTotal.Rows.Count; i++)
                {
                    tong += int.Parse(dtTotal.Rows[i]["SLHuy"].ToString());
                }
                f_cell5.Text = tong.ToString();
                f_cell5.Multiline = true;
                f_cell5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell5.WidthF;
                f_cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell5);
                //==
                XRTableCell f_cell6 = new XRTableCell();
                f_cell6.RowSpan = 1;
                f_cell6.WidthF = w7;
                f_cell6.Text = _totalconlai.ToString();
                f_cell6.Multiline = true;
                f_cell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell6.WidthF;
                f_cell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell6);
                //==
                XRTableCell f_cell7 = new XRTableCell();
                f_cell7.RowSpan = 1;
                f_cell7.WidthF = w8;
                tong = 0;
                for (int i = 0; i < _dtSluong.Rows.Count; i++)
                {
                    tong += int.Parse(_dtSluong.Rows[i]["Quantity"].ToString());
                }
                f_cell7.Text = tong.ToString();
                f_cell7.Multiline = true;
                f_cell7.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                GroupFooter1.WidthF = f_cell7.WidthF;
                f_cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell7);
                //==

                total.PerformLayout();
                total.AdjustSize();
                total.EndInit();

                total.HeightF = 40F;
                total.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                total.WidthF = 1065L;
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

