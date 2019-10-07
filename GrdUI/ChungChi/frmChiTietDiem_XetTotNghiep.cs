using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using GrdCore.BLL;

namespace GrdUI.ChungChi
{
    public partial class frmChiTietDiem_XetTotNghiep : DevExpress.XtraEditors.XtraForm
    {
        public frmChiTietDiem_XetTotNghiep()
        {
            InitializeComponent();
        }

        public string _StudentID = string.Empty, _MaChuanXet = string.Empty, _GhiChu = string.Empty;
        public string _StudentName = string.Empty, _TenChuanXet = string.Empty, _DTB = string.Empty, _STCBB = string.Empty, _STCTC = string.Empty, _TenKetQua = string.Empty;

        private void simpleButton_Excel_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xls";

                sfdFiles.FileName = "Chi tiết xét SV " + _StudentID; ;

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridControlData.ExportToXls(sfdFiles.FileName);
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataTable dtPrint = new DataTable();

        private void frmChiTietDiem_XetTotNghiep_Load(object sender, EventArgs e)
        {
            //Lấy thông tin sinh viên, điểm, các nhóm tự chọn
            GetStudentInfo();
            GetData();
        }


        private void GetStudentInfo()
        {
            try
            {
                DataSet ds = new DataSet();
                memoEdit_ThongTn.Text = "Mã sinh viên \t:   " + _StudentID+ Environment.NewLine
                    + "Tên sinh viên \t:   " + _StudentName + Environment.NewLine
                    + "Tên chuẩn xét \t:   " + _TenChuanXet + Environment.NewLine
                    + "Điểm trung bình \t:   " + _DTB + Environment.NewLine
                    + "Số TC bắt buộc \t:   " + _STCBB + Environment.NewLine
                    + "Số TC tự chọn \t:   " + _STCTC + Environment.NewLine
                    + "Kết quả \t\t:   " + _TenKetQua + Environment.NewLine
                    + "Ghi chú \t\t:   " + _GhiChu
                    ;
            }
            catch { }
        }

        private void GetData()
        {
            try
            {
                DataSet ds = BL_ChungChi.ChiTietXet(_StudentID, _MaChuanXet);
                DataTable _dtBangDiem = ds.Tables["BangDiem"].Copy();
                DataTable _dtChungChi = ds.Tables["ChungChi"].Copy();
                DataTable _dtHoSo = ds.Tables["HoSo"].Copy();
                gridControlData.DataSource = null;
                gridControlData.DataSource = _dtBangDiem;

                AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                AppGridView.ShowField(gridViewData,
                   new string[] {  "CurriculumID", "CurriculumName", "Credits", "DiemTK_10", "DiemTK_4", "DiemTK_Chu", "IsPass","TinhDiem","YearStudy","TermID" },
                   new string[] {  "Mã Môn", "Tên Môn", "Tín Chỉ", "Điểm 10", "Điểm 4", "Điểm Chữ", "Kết Quả", "Tính điểm", "Năm học", "Học kỳ" },
                   new int[] { 100, 150, 300, 80, 80, 80, 80, 80,80,80,80 });
                gridViewData.OptionsView.ColumnAutoWidth = false;
                gridViewData.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                      new GridColumnSortInfo(gridViewData.Columns["SelectionParentName"], DevExpress.Data.ColumnSortOrder.Ascending),
                      new GridColumnSortInfo(gridViewData.Columns["SelectionName"], DevExpress.Data.ColumnSortOrder.Ascending),
                }, 3);

                gridViewData.ExpandAllGroups();


                //Chi tiet chung chi
                gridControl_ChungChi.DataSource = null;
                gridControl_ChungChi.DataSource = _dtChungChi;

                gridView_ChungChi.OptionsView.ColumnAutoWidth = true;
                gridView_ChungChi.BestFitColumns();

                //Chi tiết hồ sơ
                gridControl_HoSo.DataSource = null;
                gridControl_HoSo.DataSource = _dtHoSo;

                gridView_HoSo.OptionsView.ColumnAutoWidth = true;
                gridView_HoSo.BestFitColumns();

            }
            catch { }
        }

        private void gridViewData2_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    string _CurriculumType = gridViewData2.GetRowCellDisplayText(e.RowHandle, gridViewData2.Columns["SelectionParentID"]);
            //    if (_CurriculumType != "00")
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }

        private void gridViewData2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "DiemTK_Chu" || e.Column.FieldName == "DiemTK_10" || e.Column.FieldName == "DiemTK_4" || e.Column.FieldName == "IsPass")
            {
                string _MarkLetter = gridViewData.GetRowCellDisplayText(e.RowHandle, gridViewData.Columns["DiemTK_Chu"]);
                if (_MarkLetter == "F")
                {
                    e.Appearance.BackColor = Color.Silver;
                    e.Appearance.BackColor2 = Color.White;
                }
                string _Result = gridViewData.GetRowCellDisplayText(e.RowHandle, gridViewData.Columns["IsPass"]);
                if (_Result == "")
                {
                    e.Appearance.BackColor = Color.Silver;
                    e.Appearance.BackColor2 = Color.White;
                }
            }
        }
    }
}