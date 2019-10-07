using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GrdCore.BLL;
using DevExpress.Common.Grid;
using DevExpress.XtraSplashScreen;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_ChuongTrinhDaoTao : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable();
        public string _maCTDT = string.Empty;
        public bool _isSubmit = false;
        public string _graduateLevelID = string.Empty, _studyTypeID = string.Empty;

        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;
        #endregion

        #region Inits
        public frm_Grd_ChuongTrinhDaoTao()
        {
            InitializeComponent();
        }

        private void frm_Grd_ChuongTrinhDaoTao_Load(object sender, EventArgs e)
        {
            try
            {
                #region Phân quyền
                CommonFunctions.SetFormPermiss(this);

                #region Định nghĩa lưới
                try
                {
                    DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                    drGrids = (DataRow)dtGrid.Select("GridID = 'CTDT'").GetValue(0);

                    _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
                }
                catch
                {
                    XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                #endregion

                if (_graduateLevelID != string.Empty)
                    checkedComboBoxEdit_BacDaoTao.Enabled = false;

                if (_studyTypeID != string.Empty)
                    checkedComboBoxEdit_LHDT.Enabled = false;

                if (_graduateLevelID == string.Empty)
                    _graduateLevelID = User._CurrentGraduateLevelID;

                if (_studyTypeID == string.Empty)
                    _studyTypeID = User._CurrentStudyTypeID;

                #region Bậc đào tạo
                GetGraduateLevels();
                if (_graduateLevelID == string.Empty)
                    checkedComboBoxEdit_BacDaoTao.CheckAll();
                else
                {
                    bool macDinh = false;
                    foreach (string str in _graduateLevelID.Split(';'))
                        if (((DataTable)checkedComboBoxEdit_BacDaoTao.Properties.DataSource).Select("GraduateLevelID = '" + str + "'").Length > 0)
                        {
                            macDinh = true;
                            break;
                        }

                    if (macDinh == false)
                        checkedComboBoxEdit_BacDaoTao.CheckAll();
                    else
                        checkedComboBoxEdit_BacDaoTao.EditValue = _graduateLevelID;
                }
                checkedComboBoxEdit_BacDaoTao.RefreshEditValue();
                #endregion

                #region Loại hình đào tạo
                GetStudyTypes();
                if (_studyTypeID == string.Empty)
                    checkedComboBoxEdit_LHDT.CheckAll();
                else
                {
                    bool macDinh = false;
                    foreach (string str in _studyTypeID.Split(';'))
                        if (((DataTable)checkedComboBoxEdit_LHDT.Properties.DataSource).Select("StudyTypeID = '" + str + "'").Length > 0)
                        {
                            macDinh = true;
                            break;
                        }

                    if (macDinh == false)
                        checkedComboBoxEdit_LHDT.CheckAll();
                    else
                        checkedComboBoxEdit_LHDT.EditValue = _studyTypeID;
                }
                checkedComboBoxEdit_LHDT.RefreshEditValue();
                #endregion

                btn_Loc_Click(null, null);
            }
            catch { }
        }
        #endregion 

        #region Functions
        private void GetGraduateLevels()
        {
            try
            {
                checkedComboBoxEdit_BacDaoTao.Properties.DataSource = null;

                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();

                checkedComboBoxEdit_BacDaoTao.Properties.DataSource = _dtGraduateLevels;
                checkedComboBoxEdit_BacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                checkedComboBoxEdit_BacDaoTao.Properties.ValueMember = "GraduateLevelID";

                checkedComboBoxEdit_BacDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetStudyTypes()
        {
            try
            {
                checkedComboBoxEdit_LHDT.Properties.DataSource = null;

                DataTable _dtStudyTypes = User._dsDataDictionaries.Tables["StudyTypes"].Copy();

                checkedComboBoxEdit_LHDT.Properties.DataSource = _dtStudyTypes;
                checkedComboBoxEdit_LHDT.Properties.DisplayMember = "StudyTypeName";
                checkedComboBoxEdit_LHDT.Properties.ValueMember = "StudyTypeID";

                checkedComboBoxEdit_LHDT.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetCourses()
        {
            try
            {
                checkedComboBoxEdit_KhoaHoc.Properties.DataSource = null;

                DataTable _dtCourses = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(checkedComboBoxEdit_BacDaoTao.EditValue.ToString()
                    , checkedComboBoxEdit_LHDT.EditValue.ToString());

                checkedComboBoxEdit_KhoaHoc.Properties.DataSource = _dtCourses;
                checkedComboBoxEdit_KhoaHoc.Properties.DisplayMember = "CourseDisplay";
                checkedComboBoxEdit_KhoaHoc.Properties.ValueMember = "CourseID";

                checkedComboBoxEdit_KhoaHoc.Properties.SeparatorChar = ';';
                checkedComboBoxEdit_KhoaHoc.CheckAll();
            }
            catch { }
        }

        private void GetOlogies()
        {
            try
            {
                checkedComboBoxEdit_nganhHoc.Properties.DataSource = null;

                DataTable _dtOlogies = BL_ChungChi.LayNganhHoc_BacDaoTao_LoaiHinhDaoTao_KhoaQuanLy(checkedComboBoxEdit_BacDaoTao.EditValue.ToString()
                    , checkedComboBoxEdit_LHDT.EditValue.ToString(), "#");

                checkedComboBoxEdit_nganhHoc.Properties.DataSource = _dtOlogies;
                checkedComboBoxEdit_nganhHoc.Properties.DisplayMember = "OlogyName";
                checkedComboBoxEdit_nganhHoc.Properties.ValueMember = "OlogyID";

                checkedComboBoxEdit_nganhHoc.Properties.SeparatorChar = ';';
                checkedComboBoxEdit_nganhHoc.CheckAll();
            }
            catch { }
        }

        private void GetData()
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm(this, typeof(frm_Grd_ChoThucThi), true, true, false);

                _dtData = BL_ChungChi.ChuongTrinhDaoTao(checkedComboBoxEdit_KhoaHoc.EditValue.ToString(), checkedComboBoxEdit_nganhHoc.EditValue.ToString());

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                SplashScreenManager.CloseForm(false);
            }
            catch { SplashScreenManager.CloseForm(false); }
        }
        #endregion

        #region Events
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedComboBoxEdit_LHDT_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
            GetOlogies();
        }

        private void checkedComboBoxEdit_BacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
            GetOlogies();
        }

        private void btn_LamTuoi_Click(object sender, EventArgs e)
        {
            frm_Grd_ChuongTrinhDaoTao_Load(null, null);
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btn_DongY_Click(object sender, EventArgs e)
        {
            try
            {
                _maCTDT = string.Empty;

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    if (_maCTDT == string.Empty)
                        _maCTDT = gridViewData.GetDataRow(i)["StudyProgramID"].ToString();
                    else
                       _maCTDT += "; " + gridViewData.GetDataRow(i)["StudyProgramID"].ToString();
                }

                _isSubmit = true;
                this.Close();
            }
            catch { }
        }
        #endregion
    }
}