using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using GrdCore.Entities;

namespace GrdUI.InBang
{
    public partial class frm_Grd_QuanLyQuyetDinh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public static int _filterBy = 0;
        public int _decisionTypeID = 19;
        public frm_Grd_QuanLyQuyetDinh me;
        DataTable _dtData = new DataTable();

        public string _decisionNumber = string.Empty;
        public string _decisionAlias = string.Empty;
        public string _signStaff = string.Empty;
        public string _signDate = string.Empty;
        public string _reason = string.Empty;
        public bool _isLinkFromStudentMarkDecisionInfo = false;
        public string _studyUnitID = string.Empty;
        public string _studentID = string.Empty;
        DataTable DataAnh = new DataTable();
        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;
        DataTable dtGrid = new DataTable();
        #endregion

        #region Inits
        public frm_Grd_QuanLyQuyetDinh()
        {
            InitializeComponent();
        }

        private void frm_Grd_QuanLyQuyetDinh_Load(object sender, EventArgs e)
        {
            #region Phân quyền          
            CommonFunctions.SetFormPermiss(this);

            #region Ðịnh nghĩa luới
            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'QLQD'").GetValue(0);
                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            this.StartPosition = FormStartPosition.CenterScreen;

            try
            {
                dateEdit_denNgay.EditValue = DateTime.Now;
                dateEdit_tuNgay.EditValue = DateTime.Now;
                LocQuyetDinhTheo();

                if (_decisionTypeID == 19)
                    radioGroupLoaiQuyetDinh.SelectedIndex = 2;
                else if (_decisionTypeID == 191)
                    radioGroupLoaiQuyetDinh.SelectedIndex = 0;
                else if (_decisionTypeID == 192)
                    radioGroupLoaiQuyetDinh.SelectedIndex = 1;
                else radioGroupLoaiQuyetDinh.SelectedIndex = 3;

                if (_isLinkFromStudentMarkDecisionInfo == true)
                {
                    radioGroupLoaiQuyetDinh.Properties.ReadOnly = true;
                    btnHuyQD.Enabled = false;
                    btnSuaQD.Enabled = false;
                    btnThemMoiQD.Enabled = false;
                }
            }
            catch { }
        }
        #endregion 

        #region Functions
        private void GetDecisionsByDate()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();

                _dtData.Rows.Clear();
                
                string fromDate = string.Empty, toDate = string.Empty;
                toDate = dateEdit_denNgay.Text;
                fromDate = dateEdit_tuNgay.Text;

                if (radioGroupLoaiQuyetDinh.SelectedIndex == 0)
                    _dtData = BL_Decision.GetDecisionsByDate(fromDate, toDate, (Convert.ToInt16(lookUpEdit_locTheo.EditValue) == 0), 191);
                else if (radioGroupLoaiQuyetDinh.SelectedIndex == 1)
                    _dtData = BL_Decision.GetDecisionsByDate(fromDate, toDate, (Convert.ToInt16(lookUpEdit_locTheo.EditValue) == 0), 192);
                else if (radioGroupLoaiQuyetDinh.SelectedIndex == 2)
                    _dtData = BL_Decision.GetDecisionsByDate(fromDate, toDate, (Convert.ToInt16(lookUpEdit_locTheo.EditValue) == 0), 19);
                else
                    _dtData = BL_Decision.GetDecisionsByDate(fromDate, toDate, (Convert.ToInt16(lookUpEdit_locTheo.EditValue) == 0), 17);
                
                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                if (_isLinkFromStudentMarkDecisionInfo == true)
                    AppGridView.ReadOnlyColumn(gridViewData);
            }
            catch { }
        }

        private void LocQuyetDinhTheo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(int));
                dt.Columns.Add("Display", typeof(string));

                dt.Rows.Add(0, "Ngày ký");
                dt.Rows.Add(1, "Ngày cập nhật");

                DataView myDataView = dt.DefaultView;
                myDataView.Sort = "Value";

                lookUpEdit_locTheo.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_locTheo.Properties.DisplayMember = "Display";
                lookUpEdit_locTheo.Properties.ValueMember = "Value";

                LookUpColumnInfoCollection coll = lookUpEdit_locTheo.Properties.Columns;
                if (coll.Count <= 0)
                    coll.Add(new LookUpColumnInfo("Display", 0, "Điều kiện lọc"));

                lookUpEdit_locTheo.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_locTheo.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_locTheo.Properties.AutoSearchColumnIndex = 0;

                lookUpEdit_locTheo.EditValue = 0;
            }
            catch { }
        }
        #endregion

        #region Events
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void lookUpEdit_locTheo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridControlData.DataSource = null;
                GetDecisionsByDate();
            }
            catch { }
        }

        private void radioGroupLoaiQuyetDinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioGroupLoaiQuyetDinh.SelectedIndex == 0)
                    _decisionTypeID = 191;
                else if (radioGroupLoaiQuyetDinh.SelectedIndex == 1)
                    _decisionTypeID = 192;
                else if (radioGroupLoaiQuyetDinh.SelectedIndex == 2)
                    _decisionTypeID = 19;
                else _decisionTypeID = 17;


                lookUpEdit_locTheo_EditValueChanged(null, null);
            }
            catch { }
        }

        private void btnThongTinQD_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_ChiTietQuyetDinh f = new frm_Grd_ChiTietQuyetDinh();
                f._decisionTypeID = _decisionTypeID;
                f._isLinkFromStudentMarkDecisions = true;
                f._linkFromStudentMarkDecisionsTypes = 0;
                f._decisionInfos = new Decisions();
                f._decisionInfos.DecisionNumber = gridViewData.GetFocusedDataRow()["DecisionNumber"].ToString();
                f._decisionInfos.DecisionAlias = gridViewData.GetFocusedDataRow()["DecisionAlias"].ToString();
                f._decisionInfos.DecisionTypeID = _decisionTypeID;
                f._decisionInfos.Reason = gridViewData.GetFocusedDataRow()["Reason"].ToString();
                f._decisionInfos.SignDate = gridViewData.GetFocusedDataRow()["SignDate"].ToString();
                f._decisionInfos.SignStaff = gridViewData.GetFocusedDataRow()["SignStaff"].ToString();

                f.LoadData();
                f.ShowDialog();
            }
            catch { }
        }

        private void btnHuyQD_Click(object sender, EventArgs e)
        {
            try
            {
                if (_decisionTypeID != 19)
                {
                    if (gridViewData.GetFocusedDataRow()["NumberOfDecisions"].ToString() != "0")
                    {
                        XtraMessageBox.Show("Không thể hủy quyết định này do đã sử dụng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string decisionNumber = gridViewData.GetFocusedDataRow()["DecisionNumber"].ToString();
                string decisionNumberHuy = string.Empty;
                if (XtraMessageBox.Show("Bạn muốn hủy quyết định "+decisionNumber.ToString(), "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {                       
                        int result = BL_Decision.DeleteDecision(decisionNumber, _decisionTypeID, User._User.StaffID);
                        if(result==0)
                            XtraMessageBox.Show("Hủy quyết định thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if(result==1)
                            XtraMessageBox.Show("Hủy quyết định không thành công\nĐã có sinh viên thuộc quyết định này", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                            XtraMessageBox.Show("Hủy quyết định thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lookUpEdit_locTheo_EditValueChanged(null, null);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                    return;
            }
            catch { }
        }

        private void btnSuaQD_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_ChiTietQuyetDinh f = new frm_Grd_ChiTietQuyetDinh();
                f._decisionTypeID = _decisionTypeID;
                f._isLinkFromStudentMarkDecisions = true;
                f._linkFromStudentMarkDecisionsTypes = 1;
                f._decisionInfos = new GrdCore.Entities.Decisions();
                f._decisionInfos.DecisionNumber = gridViewData.GetFocusedDataRow()["DecisionNumber"].ToString();
                f._decisionInfos.DecisionTypeID = _decisionTypeID;
                f._decisionInfos.Reason = gridViewData.GetFocusedDataRow()["Reason"].ToString();
                f._decisionInfos.SignDate = gridViewData.GetFocusedDataRow()["SignDate"].ToString();
                f._decisionInfos.SignStaff = gridViewData.GetFocusedDataRow()["SignStaff"].ToString();
                f._decisionInfos.UpdateStaff = User._UserID;
                f._decisionInfos.DecisionAlias = gridViewData.GetFocusedDataRow()["DecisionAlias"].ToString();

                f.LoadData();
                f.ShowDialog();

                if (f._submit)
                    lookUpEdit_locTheo_EditValueChanged(null, null);
            }
            catch { }
        }

        private void btnThemMoiQD_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_ChiTietQuyetDinh f = new frm_Grd_ChiTietQuyetDinh();
                f._isLinkFromStudentMarkDecisions = true;
                f._linkFromStudentMarkDecisionsTypes = 2;
                f._decisionTypeID = _decisionTypeID;
                f._decisionInfos = new GrdCore.Entities.Decisions();
                f.LoadData();
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();

                if (f._submit)
                    lookUpEdit_locTheo_EditValueChanged(null, null);
            }
            catch { }
        }

        private void dateEdit_tuNgay_Validated(object sender, EventArgs e)
        {
            lookUpEdit_locTheo_EditValueChanged(null, null);
        } 

        private void dateEdit_denNgay_Validated(object sender, EventArgs e)
        {
            lookUpEdit_locTheo_EditValueChanged(null, null);
        }

        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_isLinkFromStudentMarkDecisionInfo == true)
                {
                    _decisionNumber = gridViewData.GetFocusedDataRow()["DecisionNumber"].ToString();
                    _decisionAlias = gridViewData.GetFocusedDataRow()["DecisionAlias"].ToString();
                    _signDate = gridViewData.GetFocusedDataRow()["SignDate"].ToString();
                    this.Close();
                }
            }
            catch { }
        }
        #endregion
    }
}