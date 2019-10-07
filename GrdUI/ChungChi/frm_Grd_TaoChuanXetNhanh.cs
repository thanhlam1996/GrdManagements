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
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using ScrUI.Others;
using GrdReports;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_TaoChuanXetNhanh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtDataCopy = new DataTable(), _dtTempTaoChuan = new DataTable();

        DataTable _dtGridColumns = new DataTable();
        DataRow _drGrids;

        string _maLoaiXet = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_TaoChuanXetNhanh()
        {
            InitializeComponent();
        }

        private void frm_Grd_TaoChuanXetNhanh_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'DanhSachChuongTrinhDaoTao'").GetValue(0);

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

            LoaiXet();
        }
        #endregion

        #region Functions
        private void LoaiXet()
        {
            try
            {
                DataTable dtLoaiXet = BL_ChungChi.LoaiXet();

                lkuLoaiXet.Properties.DataSource = dtLoaiXet;
                lkuLoaiXet.Properties.DisplayMember = "TenLoaiChungChi";
                lkuLoaiXet.Properties.ValueMember = "MaLoaiChungChi";
                lkuLoaiXet.Properties.NullText = "";

                LookUpColumnInfoCollection col2 = lkuLoaiXet.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("MaLoaiChungChi", 0, "Mã loại xét"));
                col2.Add(new LookUpColumnInfo("TenLoaiChungChi", 0, "Tên loại xét"));

                lkuLoaiXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuLoaiXet.Properties.SearchMode = SearchMode.AutoComplete;
                lkuLoaiXet.Properties.AutoSearchColumnIndex = 1;

                lkuLoaiXet.ItemIndex = 0;
            }
            catch (Exception ex) { }
        }

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
            catch (Exception ex) { }
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
            catch (Exception ex) { }
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

        private void DieuKienXet()
        {
            try
            {
                DataTable _dtDK = BL_ChungChi.DieuKienXet(lkuLoaiXet.EditValue.ToString());
                lookUpEdit_DKXet.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtDK.Copy());
                myDataView.Sort = "TenDieuKien";

                lookUpEdit_DKXet.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_DKXet.Properties.DisplayMember = "TenDieuKien";
                lookUpEdit_DKXet.Properties.ValueMember = "MaDieuKien";
                lookUpEdit_DKXet.Properties.NullText = "";

                LookUpColumnInfoCollection col2 = lookUpEdit_DKXet.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("MaDieuKien", 0, "Mã điều kiện"));
                col2.Add(new LookUpColumnInfo("TenDieuKien", 1, "Tên điều kiện"));

                lookUpEdit_DKXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_DKXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_DKXet.Properties.AutoSearchColumnIndex = 1;

                lookUpEdit_DKXet.ItemIndex = 0;                
            }
            catch (Exception ex) { }
        }

        private void GetData()
        {
            try
            {
                lkuLoaiXet.EditValue = _maLoaiXet;

                gridControlData.DataSource = null;      
                gridViewData.Columns.Clear();

                _dtData = BL_ChungChi.DanhSachChuongTrinhDaoTao( chkComboKhoaHoc.EditValue.ToString(),lookUpEdit_DKXet.EditValue.ToString());

                _dtData.Columns["SoChuanDat"].ReadOnly = false;


                gridControlData.DataSource = _dtData;            

                 AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

                //gridView_Data.Columns["Chon"].ReadOnly = false;
                StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                khongDuocChapNhan9.Appearance.BackColor = Color.Aqua;
                khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                khongDuocChapNhan9.Expression = "[SoChuanDaTao] > 0";
                gridViewData.FormatConditions.Add(khongDuocChapNhan9);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaoChuanNhanh()
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);
                string strXml = string.Empty;
                string Reval = string.Empty;
                DataRow dr;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    strXml = string.Empty;
                    strXml += "<Root><Datas StudyProgramID = \"" + gridViewData.GetDataRow(i)["StudyProgramID"].ToString() +
                            "\" OlogyID = \"" + gridViewData.GetDataRow(i)["OlogyID"].ToString() +
                             "\" MaDieuKien = \"" + lookUpEdit_DKXet.EditValue.ToString() +
                            "\"/></Root>";

                    int KQ = BL_ChungChi.BangChuanTemp(strXml, User._User.StaffID.ToString());

                    if (KQ == 1)
                    {
                        Reval = Reval + gridViewData.GetDataRow(i)["StudyProgramID"].ToString() + gridViewData.GetDataRow(i)["StudyProgramName"].ToString() + "\n";
                    }
                }
                SplashScreenManager.CloseForm(false);

                if (Reval == string.Empty)
                {
                    XtraMessageBox.Show("Quá trình cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                }
                else
                {
                    XtraMessageBox.Show("Lỗi cập nhật quả \n" + Reval, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch { }
        }
        #endregion

        #region Events
        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            _maLoaiXet = lkuLoaiXet.EditValue.ToString();

            GetData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkComboLoaiHinhDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
        }

        private void lkuLoaiXet_EditValueChanged(object sender, EventArgs e)
        {
            DieuKienXet();
        }

        private void chkComboBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            GetCourses();
        }

        private void btn_XoaChuan_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                string Reval = string.Empty;
                string result = string.Empty;
                DataRow dr;
                if (XtraMessageBox.Show("Xóa chuẩn đã chọn?\n(Chỉ xóa được chuẩn không khóa)", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;
       
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    int SoChuanDaTao = gridViewData.GetDataRow(i)["SoChuanDaTao"].ToString() == string.Empty ? 0 : Convert.ToInt16(gridViewData.GetDataRow(i)["SoChuanDaTao"].ToString());
                    if (SoChuanDaTao >= 1)
                    {
                        strXml = string.Empty;
                        strXml += "<Root><Data StudyProgramID = \"" + gridViewData.GetDataRow(i)["StudyProgramID"].ToString() +
                                 "\" MaDieuKien = \"" + lookUpEdit_DKXet.EditValue.ToString() +
                                "\"/></Root>";

                        result = BL_ChungChi.XoaChuanXet(string.Empty, strXml);

                        if (result == "1")
                        {
                            Reval = Reval + gridViewData.GetDataRow(i)["StudyProgramID"].ToString() + gridViewData.GetDataRow(i)["StudyProgramName"].ToString() + "\n";
                        }
                    }
                }

                if (Reval == string.Empty)
                {
                    XtraMessageBox.Show("Quá trình cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                }
                else
                {
                    if (result == "1")
                    {
                        XtraMessageBox.Show("Đã có sinh viên cấp QĐ thuộc chuẩn " + Reval, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GetData();
                    }
                }
            }
            catch(Exception ex) { }
        }

        private void simpleButton_InDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    if (gridViewData.GetDataRow(i)["SoChuanDaTao"].ToString() != "")
                        {
                            strXml += "<Data StudyProgramID = \"" + gridViewData.GetDataRow(i)["StudyProgramID"].ToString()
                                + "\" MaDieuKien = \"" + lookUpEdit_DKXet.EditValue.ToString()
                                + "\"/>";
                        }                   
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                string _nguoiLap = frmSoQD._NguoiLap;


                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.LayTieuChuanTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_TieuChuanTotNghiep_Yersin(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }

        private void btn_TaoChuan_Click(object sender, EventArgs e)
        {
            
            TaoChuanNhanh();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_TaoChuanXetNhanh_Load(null, null);
        }
        #endregion        
    }
}