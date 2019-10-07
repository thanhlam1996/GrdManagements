using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_SoChuanXetDat : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtDataCopy = new DataTable(), _dtSoChuan = new DataTable(), _dtTempTaoChuan = new DataTable();

        DataTable _dtGridColumns = new DataTable();
        DataRow _drGrids;

        string _maLoaiXet = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_SoChuanXetDat()
        {
            InitializeComponent();
        }

        private void frm_Grd_SoChuanXetDat_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'SoChuanDat'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            GetGraduateLevels();
            if (User._CurrentGraduateLevelID != string.Empty)
            {
                chkComboBacDaoTao.EditValue = User._CurrentGraduateLevelID;
                chkComboBacDaoTao.RefreshEditValue();
            }
            else
                chkComboBacDaoTao.CheckAll();

            GetStudyTypes();
            if (User._CurrentStudyTypeID != string.Empty)
            {
                chkComboLoaiHinhDaoTao.EditValue = User._CurrentStudyTypeID;
                chkComboLoaiHinhDaoTao.RefreshEditValue();
            }
            else
                chkComboLoaiHinhDaoTao.CheckAll();
        }
        #endregion

        #region Functions
        private void GetGraduateLevels()
        {
            try
            {
                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();
                chkComboBacDaoTao.Properties.Items.Clear();
                foreach (DataRow dr in _dtGraduateLevels.Rows)
                    chkComboBacDaoTao.Properties.Items.Add(dr["GraduateLevelID"].ToString(), dr["GraduateLevelName"].ToString(), CheckState.Unchecked, true);

                chkComboBacDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetStudyTypes()
        {
            try
            {
                DataTable _dtStudyTypes = User._dsDataDictionaries.Tables["StudyTypes"].Copy();
                chkComboLoaiHinhDaoTao.Properties.Items.Clear();
                foreach (DataRow dr in _dtStudyTypes.Rows)
                    chkComboLoaiHinhDaoTao.Properties.Items.Add(dr["StudyTypeID"].ToString(), dr["StudyTypeName"].ToString(), CheckState.Unchecked, true);

                chkComboLoaiHinhDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetCourses()
        {
            try
            {
                if (chkComboBacDaoTao.EditValue.ToString() == string.Empty || chkComboLoaiHinhDaoTao.EditValue.ToString() == string.Empty)
                    return;

                DataTable dtCourses = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(chkComboBacDaoTao.EditValue.ToString(), chkComboLoaiHinhDaoTao.EditValue.ToString());
                chkComboKhoaHoc.Properties.Items.Clear();
                foreach (DataRow dr in dtCourses.Rows)
                    chkComboKhoaHoc.Properties.Items.Add(dr["CourseID"].ToString(), dr["CourseID"].ToString() + " -- " + dr["CourseName"].ToString(), CheckState.Unchecked, true);

                chkComboKhoaHoc.Properties.SeparatorChar = ';';
                chkComboKhoaHoc.CheckAll();
            }
            catch { }
        }

        private void GetData()
        {
            try
            {
                _dtData = BL_ChungChi.SoChuanDat(chkComboKhoaHoc.EditValue.ToString());

                _dtData.Columns["SoChuanDat"].ReadOnly = false;

                gridControlData.DataSource = _dtData;            

                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void SaveData()
        private void SaveData()
        {
            try
            {
                string strXml = string.Empty;
                foreach (DataRow dr in _dtData.Rows)
                {
                    if(dr.RowState == DataRowState.Modified)
                        strXml += "<SoChuanXetDat MaCTDT = \"" + dr["StudyProgramID"].ToString()
                                + "\" SoChuanDat = \"" + dr["SoChuanDat"].ToString()
                                + "\"/>";                       
                }
                strXml = "<Root>" + strXml + "</Root>";

                int result = BL_ChungChi.LuuSoChuanDat(strXml, User._UserID);

                if (result == 0)
                {
                    GetData();
                    XtraMessageBox.Show("Lưu thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void chkComboLoaiHinhDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
        }

        private void chkComboBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
        }

        private void btn_ApDung_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    gridViewData.GetDataRow(i)["SoChuanDat"] = spinEdit_SoChuanDat.EditValue;
                }

                gridViewData.RefreshData();

            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_SoChuanXetDat_Load(null, null);
        }
        #endregion        
    }
}