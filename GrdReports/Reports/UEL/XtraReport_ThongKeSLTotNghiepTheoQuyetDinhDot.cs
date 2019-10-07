using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Windows.Forms;

namespace GrdReports.Reports.UEL
{
    public partial class XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtDType = new DataTable(), dtDecision = new DataTable(), dtQuantity = new DataTable();
        int count = 0, _total=0;
        public XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot()
        {
            InitializeComponent();
        }
        public void Init_Report(DataSet tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            //this.DataSource = tbPrint;
            dtDType = tbPrint.Tables["DiplomasType"].Copy();
            dtDecision = tbPrint.Tables["Decision"].Copy();
            dtQuantity = tbPrint.Tables["DiplomasTypeQuantity"].Copy();
            lblNgayIn.Text = _NgayIn;
            txt_CapBac.Text = _CapBac;
            txt_NguoiKy.Text = _NguoiKy;

            //Tieu de
            lbl_title.Text = "THỐNG KÊ SỐ LƯỢNG TỐT NGHIỆP THEO QUYẾT ĐỊNH ĐỢT TỐT NGHIỆP THÁNG " + dtDecision.Rows[0]["DotTotNghiepThang"].ToString()+" HỆ "+ dtDecision.Rows[0]["LoaiDaoTao"];
            lbl_DinhKemCongVanSo.Text = "(Đính kèm công văn số    /ĐHKTL-ĐT ngày    /   /      của Hiệu trưởng trường)";
            try
            {
                float _withH1 = 46F, _withH2 = 194F, _withH3 = 65F, _withH4 = 452F, _withH4_1 = 452F / (float)dtDType.Rows.Count;

                XRTable header = new XRTable();
                header.BeginInit();
                header.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
                GroupHeader1.Controls.Add(header);
                GroupHeader1.RepeatEveryPage = true;
                header.SuspendLayout();

                XRTableRow tbrow = new XRTableRow();
                tbrow.Weight = 1D;
                header.Rows.Add(tbrow);

                XRTableRow tbrow1 = new XRTableRow();
                tbrow.Weight = 1D;
                header.Rows.Add(tbrow1);

                //Tao thong tin header
                //==
                XRTableCell _cell = new XRTableCell();
                _cell.RowSpan = 1;
                _cell.WidthF = _withH1;
                _cell.Text = "";
                _cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                _cell.Multiline = true;
                _cell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                header.WidthF = _cell.WidthF;
                _cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                XRTableCell _cell1 = new XRTableCell();
                _cell1.WidthF = _withH1;
                _cell1.Text = "STT";
                _cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                _cell1.Multiline = true;
                _cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                header.WidthF = _cell1.WidthF;

                ((XRTableRow)header.Rows[0]).Cells.Add(_cell);
                ((XRTableRow)header.Rows[1]).Cells.Add(_cell1);
                //==
                XRTableCell _cell2 = new XRTableCell();
                _cell2.RowSpan = 1;
                _cell2.WidthF = _withH2;
                _cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                _cell2.Text = "";
                _cell2.Multiline = true;
                _cell2.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right;
                header.WidthF = _cell2.WidthF;
                _cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                XRTableCell _cell_2 = new XRTableCell();
                _cell_2.WidthF = _withH2;
                _cell_2.Text = "Quyết định tốt nghiệp";
                _cell_2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                _cell_2.Multiline = true;
                _cell_2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                header.WidthF = _cell_2.WidthF;

                ((XRTableRow)header.Rows[0]).Cells.Add(_cell2);
                ((XRTableRow)header.Rows[1]).Cells.Add(_cell_2);
                //==
                XRTableCell _cell3 = new XRTableCell();
                _cell3.RowSpan = 1;
                _cell3.WidthF = _withH3;
                _cell3.Text = "";
                _cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                _cell3.Multiline = true;
                _cell3.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right;
                header.WidthF = _cell3.WidthF;
                _cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                XRTableCell _cell_3 = new XRTableCell();
                _cell_3.WidthF = _withH3;
                _cell_3.Text = "Tổng";
                _cell_3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                _cell_3.Multiline = true;
                _cell_3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                header.WidthF = _cell_3.WidthF;

                ((XRTableRow)header.Rows[0]).Cells.Add(_cell3);
                ((XRTableRow)header.Rows[1]).Cells.Add(_cell_3);
                //==
                XRTableCell _cell4 = new XRTableCell();
                _cell4.RowSpan = 1;
                _cell4.WidthF = _withH4;
                _cell4.Text = "Loại phôi bằng";
                _cell4.Multiline = true;
                _cell4.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                header.WidthF = _cell4.WidthF;
                _cell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                ((XRTableRow)header.Rows[0]).Cells.Add(_cell4);

                for (int i = 0; i < dtDType.Rows.Count; i++)
                {
                    XRTableCell _cell_4 = new XRTableCell();
                    _cell_4.WidthF = _withH4_1;
                    _cell_4.Text = dtDType.Rows[i]["DiplomasTypeName"].ToString();
                    _cell_4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    _cell_4.Multiline = true;
                    _cell_4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    header.WidthF = _cell_4.WidthF;
                    ((XRTableRow)header.Rows[1]).Cells.Add(_cell_4);
                }

                header.PerformLayout();
                header.AdjustSize();
                header.EndInit();

                header.HeightF = 40F;
                header.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                header.WidthF = 757L;

                //Detail
                XRTable detail = new XRTable();
                detail.BeginInit();
                Detail.Controls.Add(detail);
                detail.SuspendLayout();
                detail.Font = new System.Drawing.Font("Times New Roman", 12F);

                for (int j = 0; j < dtDecision.Rows.Count; j++)
                {
                    XRTableRow _drow = new XRTableRow();
                    _drow.Weight = 1D;
                    detail.Rows.Add(_drow);

                    XRTableCell d_cell1 = new XRTableCell();
                    d_cell1.RowSpan = 1;
                    d_cell1.WidthF = _withH1;
                    d_cell1.Text = dtDecision.Rows[j]["STT"].ToString();
                    d_cell1.Multiline = true;
                    d_cell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                    detail.WidthF = d_cell1.WidthF;
                    d_cell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell1);
                    // ==
                    XRTableCell d_cell2 = new XRTableCell();
                    d_cell2.RowSpan = 1;
                    d_cell2.WidthF = _withH2;
                    d_cell2.Text = dtDecision.Rows[j]["SoQuyetDinh"].ToString() + " ngày " + dtDecision.Rows[j]["NgayKyQuyetDinh"].ToString();
                    d_cell2.Multiline = true;
                    d_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell2.WidthF;
                    d_cell2.Font= new System.Drawing.Font("Times New Roman", 11F);
                    d_cell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 0, 0);
                    d_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell2);

                    //==
                    XRTableCell d_cell3 = new XRTableCell();
                    d_cell3.RowSpan = 1;
                    d_cell3.WidthF = _withH3;
                    d_cell3.Text = dtDecision.Rows[j]["TongSL"].ToString();
                    d_cell3.Multiline = true;
                    d_cell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    detail.WidthF = d_cell3.WidthF;
                    d_cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell3);
                    //==                    
                    int loop = dtDType.Rows.Count + count;
                    for (; count < loop; count++)
                    {
                        XRTableCell d_cell_4 = new XRTableCell();
                        d_cell_4.WidthF = _withH4_1;
                        if (dtQuantity.Rows.Count > count)
                        {                          
                            d_cell_4.Text = dtQuantity.Rows[count]["SoLuong"].ToString();
                        }
                        else
                        {
                            d_cell_4.Text = "0";
                        }
                        d_cell_4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        d_cell_4.Multiline = true;
                        d_cell_4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        detail.WidthF = d_cell_4.WidthF;
                        ((XRTableRow)detail.Rows[j]).Cells.Add(d_cell_4);
                    }           
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
                _total = 0;
                for (int k = 0; k < dtDecision.Rows.Count; k++)
                {
                    _total += int.Parse(dtDecision.Rows[k]["TongSL"].ToString());
                }
                f_cell2.Text = _total.ToString();
                f_cell2.Multiline = true;
                f_cell2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                GroupFooter1.WidthF = f_cell2.WidthF;
                f_cell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                ((XRTableRow)total.Rows[0]).Cells.Add(f_cell2);
                //==

                for (int i = 0; i < dtDType.Rows.Count; i++)
                {
                    XRTableCell f_cell_4 = new XRTableCell();
                    f_cell_4.WidthF = _withH4_1;
                    _total = 0;
                    for (int k = 0; k < dtQuantity.Rows.Count; k++)
                    {
                       if(dtQuantity.Rows[k]["DiplomasTypeID"].ToString()==dtDType.Rows[i]["DiplomasTypeID"].ToString())
                        {
                            _total += int.Parse(dtQuantity.Rows[k]["SoLuong"].ToString());
                        }
                    }
                    f_cell_4.Text = _total.ToString() ;
                    f_cell_4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    f_cell_4.Multiline = true;
                    f_cell_4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                    GroupFooter1.WidthF = f_cell_4.WidthF;
                    ((XRTableRow)total.Rows[0]).Cells.Add(f_cell_4);
                }

                total.PerformLayout();
                total.AdjustSize();
                total.EndInit();

                total.HeightF = 40F;
                total.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                total.WidthF = 757L;

                GroupFooter1.HeightF = 40F;
                Detail.HeightF = 40F;
                GroupHeader1.HeightF = 40F;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }

}

