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
    public partial class frm_Grd_ChungChiNopThayThe : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtThangXepLoai = new DataTable(), _dtGridColumns = new DataTable();
        DataTable _XepLoai = new DataTable();
        DataRow _drGrids;
        DataTable _dtChungChiThayThe = new DataTable(), _dtSpecialScores = new DataTable();
        bool ScoreSystem = false;
        string _LoaiChungChi = string.Empty;

        #endregion

        #region Inits
        public frm_Grd_ChungChiNopThayThe()
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
                _drGrids = (DataRow)dtGrid.Select("GridID = 'ChungChiThayThe'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            LoaiChungChi();
        }
        #endregion

        #region Functions

        #region public void SaveData()
        public void SaveData()
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow dr in _dtChungChiThayThe.Rows)
                {

                    strXml += "<Data MaLoaiCCTT = \"" + CommonFunctions.RefreshXmlString(dr["MaLoaiCCTT"].ToString())
                        + "\" MaLoaiCCTT_Old = \"" + CommonFunctions.RefreshXmlString(dr["MaLoaiCCTT_Old"].ToString())
                        + "\" TenLoaiCCTT = \"" + CommonFunctions.RefreshXmlString(dr["TenLoaiCCTT"].ToString())
                        + "\" GhiChu = \"" + CommonFunctions.RefreshXmlString(dr["GhiChu"].ToString())
                        + "\" MaLoaiChungChi = \"" + lku_LoaiChungChi.EditValue.ToString() + "\"/>";
                }
                strXml += "</Root>";

                int result = BL_ChungChi.LuuChungChiThayThe(strXml, lku_LoaiChungChi.EditValue.ToString());
                if (result == 0)
                {
                    XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region private void LoaiChungChi()
        private void LoaiChungChi()
        {
            DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("#");

            lku_LoaiChungChi.Properties.DataSource = dtLoaiChungChi;
            lku_LoaiChungChi.Properties.DisplayMember = "TenLoaiChungChi";
            lku_LoaiChungChi.Properties.ValueMember = "MaLoaiChungChi";

            LookUpColumnInfoCollection coll = lku_LoaiChungChi.Properties.Columns;
            coll.Clear();
            coll.Add(new LookUpColumnInfo("TenLoaiChungChi", "Tên chứng chỉ"));

            lku_LoaiChungChi.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lku_LoaiChungChi.Properties.SearchMode = SearchMode.AutoComplete;
            lku_LoaiChungChi.Properties.AutoSearchColumnIndex = 0;
            lku_LoaiChungChi.Properties.NullText = string.Empty;

            lku_LoaiChungChi.ItemIndex = 0;
        }
        #endregion

        #endregion

        #region Events
        private void repositoryItemButtonEditXoaDiem_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                gridViewData.GetFocusedDataRow().Delete();
                _dtChungChiThayThe.AcceptChanges();
            }
            catch { }
        }

        #region lấy các chứng chỉ thay thế
        private void lku_LoaiChungChi_EditValueChanged(object sender, EventArgs e)
        {
            _LoaiChungChi = lku_LoaiChungChi.EditValue.ToString();

            gridControlData.DataSource = null;
            gridViewData.Columns.Clear();

            _dtChungChiThayThe = BL_ChungChi.LoaiChungChiThayThe(_LoaiChungChi);

            _dtChungChiThayThe.Columns.Add("Delete", typeof(string));

            foreach (DataColumn dc in _dtChungChiThayThe.Columns)
                dc.ReadOnly = false;

            gridControlData.DataSource = _dtChungChiThayThe;
            AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

            AppGridView.RegisterControlField(gridViewData, "Delete", repositoryItemButtonEditXoaDiem);
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion
    }
}