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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using GrdCore.BLL;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_ThangXepLoaiChiTiet : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtThangXepLoai = new DataTable(), _dtGridColumns = new DataTable();
        DataTable _XepLoai = new DataTable();
        DataRow _drGrids;
        DataTable _dtThangXepLoaiChiTiet = new DataTable(), _dtSpecialScores = new DataTable();
        bool ScoreSystem = false;
        string _MaThangXepLoai = string.Empty;

        #endregion

        #region Inits
        public frm_Grd_ThangXepLoaiChiTiet()
        {
            InitializeComponent();
        }

        private void frmThangDiem_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'XepLoaiChiTiet'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            #endregion
            #endregion

            ThangXepLoai();
        }
        #endregion

        #region Functions

        #region private void ThangXepLoai()
        private void ThangXepLoai()
        {
            try
            {
                _dtThangXepLoai = BL_ChungChi.LayThangXepLoai("#");

                DataView myDataView = new DataView(_dtThangXepLoai);
                myDataView.Sort = "TenThangXepLoai";

                lkuThangXepLoai.Properties.DataSource = myDataView.ToTable();
                lkuThangXepLoai.Properties.DisplayMember = "TenThangXepLoai";
                lkuThangXepLoai.Properties.ValueMember = "MaThangXepLoai";

                LookUpColumnInfoCollection coll = lkuThangXepLoai.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TenThangXepLoai", 0, "Thang xếp loại"));

                lkuThangXepLoai.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuThangXepLoai.Properties.SearchMode = SearchMode.AutoComplete;
                lkuThangXepLoai.Properties.AutoSearchColumnIndex = 0;
                lkuThangXepLoai.ItemIndex = 0;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region public void SaveData()
        public void SaveData()
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow dr in _dtThangXepLoaiChiTiet.Rows)
                {

                    strXml += "<XepLoai DefaultRankID = \"" + CommonFunctions.RefreshXmlString(dr["DefaultRankID"].ToString())
                        + "\" LowerScore = \"" + CommonFunctions.RefreshXmlString(dr["LowerScore"].ToString())
                        + "\" UpperScore = \"" + CommonFunctions.RefreshXmlString(dr["UpperScore"].ToString())
                        + "\" HaBac = \"" + CommonFunctions.RefreshXmlString(dr["HaBac"].ToString()) + "\"/>";
                }
                strXml += "</Root>";

                int result = BL_ChungChi.LuuThangXepLoaiChiTiet(strXml, lkuThangXepLoai.EditValue.ToString(), ScoreSystem, User._UserID);
                if (result == 0)
                {
                    XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lkuThangXepLoai_EditValueChanged(null, null);
                }
                else
                    XtraMessageBox.Show("Cập nhật thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
        #endregion

        #endregion

        #region Events
        private void repositoryItemButtonEditXoaDiem_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                gridViewData.GetFocusedDataRow().Delete();
                _dtThangXepLoaiChiTiet.AcceptChanges();
            }
            catch { }
        }

        private void lkuThangXepLoai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _MaThangXepLoai = lkuThangXepLoai.EditValue.ToString();

                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();
                
                _dtThangXepLoaiChiTiet = BL_ChungChi.ThangXepLoaiChiTiet(_MaThangXepLoai, ScoreSystem);

                _dtThangXepLoaiChiTiet.Columns.Add("Delete", typeof(string));

                foreach (DataColumn dc in _dtThangXepLoaiChiTiet.Columns)
                    dc.ReadOnly = false;

                gridControlData.DataSource = _dtThangXepLoaiChiTiet;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

                gridViewData.Columns["LowerScore"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridViewData.Columns["LowerScore"].DisplayFormat.FormatString = "{0:0.00}";
                gridViewData.Columns["UpperScore"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridViewData.Columns["UpperScore"].DisplayFormat.FormatString = "{0:0.00}";

                AppGridView.RegisterControlField(gridViewData, "Delete", repositoryItemButtonEditXoaDiem);

                #region Xếp loại
                _XepLoai = BL_ChungChi.XepLoai();
                DataView myDataView = new DataView(_XepLoai);
                myDataView.Sort = "Rank asc, DefaultRankName asc";

                repositoryItemLookUpEdit_XepLoai.Properties.DataSource = myDataView.ToTable();
                repositoryItemLookUpEdit_XepLoai.Properties.DisplayMember = "DefaultRankName";
                repositoryItemLookUpEdit_XepLoai.Properties.ValueMember = "DefaultRankID";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_XepLoai.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("DefaultRankName", 0, "Xếp loại"));

                repositoryItemLookUpEdit_XepLoai.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEdit_XepLoai.Properties.SearchMode = SearchMode.AutoComplete;
                repositoryItemLookUpEdit_XepLoai.Properties.NullText = "";

                AppGridView.RegisterControlField(gridViewData, "DefaultRankID", repositoryItemLookUpEdit_XepLoai); 
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaThangXepLoai_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtThangXepLoai.Rows.Count == 0 || XtraMessageBox.Show("Xóa thang xếp loại '"
                    + lkuThangXepLoai.EditValue.ToString() + "---" + lkuThangXepLoai.Text + "' ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

                BL_ChungChi.CapNhatThangXepLoai(lkuThangXepLoai.EditValue.ToString(), "", "", "Del", User._UserID);

                ThangXepLoai();

                try
                {
                    lkuThangXepLoai.ItemIndex = 0;
                }
                catch { }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Cập nhật thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
            }
        }

        private void btnThemMoiThangXepLoai_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_CapNhatThangXepLoai f = new frm_Grd_CapNhatThangXepLoai();
                f.LoadData(string.Empty);
                f._isNew = true;
                f.ShowDialog();                

                if (f._isSubmit)
                {
                    ThangXepLoai();
                    lkuThangXepLoai.EditValue = f._MaThangXepLoai;
                    lkuThangXepLoai_EditValueChanged(null, null);
                }
            }
            catch { }
        }

        private void btnCapNhatThangXepLoai_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtThangXepLoai.Rows.Count == 0) return;
                frm_Grd_CapNhatThangXepLoai f = new frm_Grd_CapNhatThangXepLoai();
                f.LoadData(lkuThangXepLoai.EditValue.ToString());
                f._isNew = false;
                f.ShowDialog();

                if (f._isSubmit)
                {
                    ThangXepLoai();
                    lkuThangXepLoai.EditValue = f._MaThangXepLoai;
                    lkuThangXepLoai_EditValueChanged(null, null);
                }
            }
            catch { }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void gridView_Diem_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if ((gridViewData.FocusedColumn.FieldName == "LowerScore" || gridViewData.FocusedColumn.FieldName == "UpperScore") && radioGroup_ThangDiem.SelectedIndex == 1)
            {
                double dMark4 = -1;
                Double.TryParse(e.Value as String, out dMark4);

                if (dMark4 < 0 || dMark4 > 4)
                {
                    e.Valid = false;
                    e.ErrorText = "0 <= Điểm <= 4";
                }
            }

            if ((gridViewData.FocusedColumn.FieldName == "LowerScore" || gridViewData.FocusedColumn.FieldName == "UpperScore") && radioGroup_ThangDiem.SelectedIndex == 0)
            {
                double dMark10L = -1;
                Double.TryParse(e.Value as String, out dMark10L);

                if (dMark10L < 0 || dMark10L > 10)
                {
                    e.Valid = false;
                    e.ErrorText = "0 <= Điểm <= 10";
                }
            }
        }

        private void radioGroup_ThangDiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup_ThangDiem.SelectedIndex == 1)
                ScoreSystem = true;
            else
                ScoreSystem = false;

            lkuThangXepLoai_EditValueChanged(null,null);
        }

        private void gridView_Diem_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() == string.Empty)
                gridViewData.GetFocusedDataRow()[e.Column.FieldName] = DBNull.Value;
        }

        private void gridView_Diem_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            if (e.Value.ToString() == string.Empty)
                e.ExceptionMode = ExceptionMode.Ignore;
        } 
        #endregion
    }
}