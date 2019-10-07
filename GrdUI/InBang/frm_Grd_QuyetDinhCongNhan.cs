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
using DevExpress.XtraSplashScreen;
using GrdUI.ChungChi;
using DevExpress.Common.Grid;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using GrdReports;
using ScrUI.Others;

namespace GrdUI.InBang
{
    public partial class frm_Grd_QuyetDinhCongNhan : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow _drGrids;

        string _maLoaiXet = string.Empty, _maDotXet = string.Empty, _decisionNumber = string.Empty, _decisionAlias = string.Empty, _ngayKy = string.Empty;
        int _decisionTypeID = 19;

        string  _decisionNumber_HUY = string.Empty, _decisionAlias_HUY = string.Empty, _ngayKy_HUY = string.Empty;
        int _decisionTypeID_HUY = 19;

        string[] _studyStatusID;
        #endregion

        #region Inits
        public frm_Grd_QuyetDinhCongNhan()
        {
            InitializeComponent();
        }

        private void frm_Grd_QuyetDinhCongNhan_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'QDCongNhan'").GetValue(0);

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

            GetYearStudy();
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            LoaiChungChi();

            DataTable dtTemp = User._dsDataDictionaries.Tables[0].Copy();
            DataRow drTemp = (DataRow)dtTemp.Select("SettingName = 'TinhTrangXetTotNghiep'").GetValue(0);
            _studyStatusID = drTemp["SettingStringData"].ToString().Split(';');

            if (User._CollegeID == 31)
            {
                mnu_DSSVDuocCongNhanTNKhongXetDaCapQD.Visible = true;
            }
        }
        #endregion

        #region Functions
        private void GetGraduateLevels()
        {
            try
            {
                chkComboBacDaoTao.Properties.DataSource = null;

                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();

                chkComboBacDaoTao.Properties.DataSource = _dtGraduateLevels;
                chkComboBacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                chkComboBacDaoTao.Properties.ValueMember = "GraduateLevelID";

                chkComboBacDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetStudyTypes()
        {
            try
            {
                chkComboLoaiHinhDaoTao.Properties.DataSource = null;

                DataTable _dtStudyTypes = User._dsDataDictionaries.Tables["StudyTypes"].Copy();

                chkComboLoaiHinhDaoTao.Properties.DataSource = _dtStudyTypes;
                chkComboLoaiHinhDaoTao.Properties.DisplayMember = "StudyTypeName";
                chkComboLoaiHinhDaoTao.Properties.ValueMember = "StudyTypeID";

                chkComboLoaiHinhDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetYearStudy()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("YearStudy", typeof(string));
                dtData.Columns.Add("YearStudyID", typeof(string));

                foreach (DataRow dr in User._dsDataDictionaries.Tables["Terms"].Rows)
                    if (dtData.Select("YearStudy = '" + dr["YearStudy"].ToString() + "'").Length == 0)
                        dtData.Rows.Add(new object[] { dr["YearStudy"].ToString(), dr["YearStudy"].ToString() });

                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "YearStudy DESC";

                lkuNamHoc.Properties.DataSource = myDataView.ToTable();
                lkuNamHoc.Properties.DisplayMember = "YearStudy";
                lkuNamHoc.Properties.ValueMember = "YearStudyID";

                LookUpColumnInfoCollection coll = lkuNamHoc.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("YearStudy", 0, "Năm học"));

                lkuNamHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuNamHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lkuNamHoc.Properties.AutoSearchColumnIndex = 0;
                lkuNamHoc.Properties.NullText = string.Empty;

                lkuNamHoc.ItemIndex = 0;
            }
            catch { }
        }

        private void GetTerms(string yearStudy)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("TermID", typeof(string));
                dtData.Columns.Add("TermName", typeof(string));

                DataRow[] drSelect = User._dsDataDictionaries.Tables["Terms"].Select("YearStudy = '" + yearStudy + "'");
                foreach (DataRow dr in drSelect)
                    dtData.Rows.Add(new object[] { dr["TermID"].ToString(), dr["TermName"].ToString() });

                DataView dv = new DataView(dtData);
                dv.Sort = "TermName";

                lkuHocKy.Properties.DataSource = dv.ToTable();
                lkuHocKy.Properties.DisplayMember = "TermName";
                lkuHocKy.Properties.ValueMember = "TermID";

                LookUpColumnInfoCollection coll = lkuHocKy.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lkuHocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuHocKy.Properties.SearchMode = SearchMode.AutoComplete;
                lkuHocKy.Properties.AutoSearchColumnIndex = 0;
                lkuHocKy.Properties.NullText = string.Empty;

                lkuHocKy.ItemIndex = 0;
            }
            catch { }
        }

        private void LoaiChungChi()
        {
            string maHinhThucCapChungChi = "CC";
            if (rdGrpLoaiMauIn.SelectedIndex == 1)
                maHinhThucCapChungChi = "CC";
            else
                maHinhThucCapChungChi = "CB";

            DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi(maHinhThucCapChungChi);

            lkuLoaiChungChi.Properties.DataSource = dtLoaiChungChi;
            lkuLoaiChungChi.Properties.DisplayMember = "TenLoaiChungChi";
            lkuLoaiChungChi.Properties.ValueMember = "MaLoaiChungChi";

            LookUpColumnInfoCollection coll = lkuLoaiChungChi.Properties.Columns;
            coll.Clear();
            coll.Add(new LookUpColumnInfo("TenLoaiChungChi", "Tên chứng chỉ"));

            lkuLoaiChungChi.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lkuLoaiChungChi.Properties.SearchMode = SearchMode.AutoComplete;
            lkuLoaiChungChi.Properties.AutoSearchColumnIndex = 0;
            lkuLoaiChungChi.Properties.NullText = string.Empty;

            lkuLoaiChungChi.ItemIndex = 0;
        }

        private void DotXet()
        {
            try
            {
                if (chkComboBacDaoTao.EditValue.ToString() == string.Empty
                    || chkComboLoaiHinhDaoTao.EditValue.ToString() == string.Empty
                    || lkuNamHoc.EditValue == null
                    || lkuHocKy.EditValue == null
                    || lkuLoaiChungChi.EditValue == null)
                    return;

                DataTable dtDotXet = BL_ChungChi.DotXetTheoBacHe(chkComboBacDaoTao.EditValue.ToString(), chkComboLoaiHinhDaoTao.EditValue.ToString()
                    , lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString());

                lkuDotXet.Properties.DataSource = dtDotXet;
                lkuDotXet.Properties.DisplayMember = "TenDot";
                lkuDotXet.Properties.ValueMember = "MaDot";

                LookUpColumnInfoCollection coll = lkuDotXet.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaDot", 0, "Mã"));
                coll.Add(new LookUpColumnInfo("TenDot", 0, "Tên"));

                lkuDotXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuDotXet.Properties.SearchMode = SearchMode.AutoComplete;
                lkuDotXet.Properties.AutoSearchColumnIndex = 0;
                lkuDotXet.Properties.NullText = string.Empty;

                lkuDotXet.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetData()
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);                

                _dtData = BL_InBang.DanhSachCapQuyetDinhCongNhan(_maLoaiXet, _maDotXet);

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

                SplashScreenManager.CloseForm(false);
            }
            catch(Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapQuyetDinh()
        {
            try
            {
                if (rdGrpLoaiMauIn.SelectedIndex == 0)
                {
                    if (XtraMessageBox.Show("Khi cấp quyết định tình trạng sinh viên có thể sẽ bị thay đổi." + "\n" + "Bạn có muốn cấp quyết định công nhận ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                }
                else
                {
                    if (XtraMessageBox.Show("Bạn có muốn cấp quyết định công nhận ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                }

                if (_decisionNumber == string.Empty || buttonEditQuyetDinh.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("Chưa có chọn quyết định", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }

                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                string error = string.Empty, message = string.Empty;
                DataRow drCapQD;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    drCapQD = gridViewData.GetDataRow(i);

                    try
                    {
                        if (_studyStatusID.Contains(drCapQD["StudyStatusID"].ToString()))
                        {
                            message = BL_InBang.CapNhatVaHuyQuyetDinhCongNhan(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                            , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), _decisionNumber, _decisionTypeID, User._UserID, 6);

                            if (!message.Contains("..."))
                            {
                                if (error == string.Empty)
                                    error = message;
                                else
                                    error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + message;
                            }
                        }
                        else
                        {
                            if (error == string.Empty)
                            {
                                error = drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép cấp quyết định (" + drCapQD["StudyStatusName"].ToString() + ")";
                            }
                            else
                                error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép cấp quyết định (" + drCapQD["StudyStatusName"].ToString() + ")";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (error == string.Empty)
                            error = ex.Message;
                        else
                            error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + ex.Message;
                    }
                }

                SplashScreenManager.CloseForm(false);

                GetData();

                if (error != string.Empty)
                    XtraMessageBox.Show(error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XtraMessageBox.Show("Cập nhật hoàn thành...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuyQuyetDinh()
        {
            try
            {
                try
                {
                    frm_Grd_QuanLyQuyetDinh f = new frm_Grd_QuanLyQuyetDinh();
                    f._isLinkFromStudentMarkDecisionInfo = true;

                        f._decisionTypeID = 17;


                    f.ShowDialog();

                    if (f._decisionNumber != string.Empty)
                    {
                        _decisionTypeID = 19;
                        _decisionNumber_HUY = f._decisionNumber;
                        _decisionAlias_HUY = f._decisionAlias;
                        _ngayKy_HUY = f._signDate;

                        if (_ngayKy != string.Empty)
                            buttonEditQuyetDinh.Text = _decisionAlias + " - Ngày ký: " + Convert.ToDateTime(_ngayKy).ToString("dd/MM/yyyy");
                        else
                            buttonEditQuyetDinh.Text = _decisionAlias + " - Ngày ký: ";
                    }
                }
                catch { }

                if (_decisionNumber_HUY != string.Empty)
                {
                    frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep frm = new frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep();
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (rdGrpLoaiMauIn.SelectedIndex == 0)
                    {
                        if (XtraMessageBox.Show("Khi hủy quyết định tình trạng sinh viên sẽ bị thay đổi." + "\n" + "Bạn có muốn hủy quyết định công nhận ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            return;

                        frm.ShowDialog();
                    }
                    else
                    {
                        if (XtraMessageBox.Show("Bạn có muốn hủy quyết định công nhận ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            return;
                    }

                    if (frm._isAccepted == true)
                    {
                        SplashScreenManager splashScreen = new SplashScreenManager();
                        SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                        string error = string.Empty, message = string.Empty;
                        DataRow drCapQD;
                        foreach (int i in gridViewData.GetSelectedRows())
                        {
                            drCapQD = gridViewData.GetDataRow(i);

                            try
                            {
                                message = BL_InBang.CapNhatVaHuyQuyetDinhCongNhan(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                                    , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), _decisionNumber_HUY, _decisionTypeID, User._UserID, frm._stadyStatusID);

                                if (!message.Contains("..."))
                                {
                                    if (error == string.Empty)
                                        error = message;
                                    else
                                        error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + message;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (error == string.Empty)
                                    error = ex.Message;
                                else
                                    error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + ex.Message;
                            }
                        }

                        SplashScreenManager.CloseForm(false);

                        GetData();
                        buttonEditQuyetDinh.Text = string.Empty;
                        if (error != string.Empty)
                            XtraMessageBox.Show(error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            XtraMessageBox.Show("Cập nhật hoàn thành...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapSoThuTu()
        {
            try
            {
                if (XtraMessageBox.Show("Bạn có muốn cấp số thứ tự ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                int doDaiSo = 0, soBatDau = 0;
                try
                {
                    soBatDau = Convert.ToInt32(textEditSo.Text);
                }
                catch
                {
                    XtraMessageBox.Show("Số thứ tự không đúng định dạng chữ số.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                doDaiSo = textEditSo.Text.Trim().Length;

                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                string error = string.Empty, message = string.Empty, soThuTu = string.Empty;
                DataRow drCapQD;
                int soSoKhong = 0;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    soThuTu = string.Empty;
                    drCapQD = gridViewData.GetDataRow(i);

                    try
                    {
                        if (_studyStatusID.Contains(drCapQD["StudyStatusID"].ToString()))
                        {
                            #region Cấp phát số thứ tự
                            for (soSoKhong = 1; soSoKhong <= doDaiSo - soBatDau.ToString().Length; soSoKhong++)
                                soThuTu += "0";

                            soThuTu = soThuTu + soBatDau.ToString();

                            if (radioGroupCachDanhSTT.SelectedIndex == 0)
                                soThuTu = textEditChu.EditValue.ToString() + soThuTu;
                            else
                                soThuTu = soThuTu + textEditChu.EditValue.ToString();
                            #endregion

                            message = BL_InBang.CapNhatVaHuySoThuTuQuyetDinhCongNhan(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                                , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), soThuTu, User._UserID);

                            if (!message.Contains("..."))
                            {
                                if (error == string.Empty)
                                    error = message;
                                else
                                    error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + message;
                            }
                        }
                        else
                        {
                            if (error == string.Empty)
                                error = drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép cấp số thứ tự (" + drCapQD["StudyStatusName"].ToString() + ")";
                            else
                                error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép cấp số thứ tự (" + drCapQD["StudyStatusName"].ToString() + ")";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (error == string.Empty)
                            error = ex.Message;
                        else
                            error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + ex.Message;
                    }

                    soBatDau++;
                }

                SplashScreenManager.CloseForm(false);

                GetData();

                if (error != string.Empty)
                    XtraMessageBox.Show(error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XtraMessageBox.Show("Cập nhật hoàn thành...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuySoThuTu()
        {
            try
            {
                if (XtraMessageBox.Show("Bạn có muốn cấp số thứ tự ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                string error = string.Empty, message = string.Empty;
                DataRow drCapQD;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    drCapQD = gridViewData.GetDataRow(i);

                    try
                    {
                        if (_studyStatusID.Contains(drCapQD["StudyStatusID"].ToString()))
                        {
                            message = BL_InBang.CapNhatVaHuySoThuTuQuyetDinhCongNhan(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                            , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), "", User._UserID);

                            if (!message.Contains("..."))
                            {
                                if (error == string.Empty)
                                    error = message;
                                else
                                    error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + message;
                            }
                        }
                        else
                        {
                            if (error == string.Empty)
                                error = drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép hủy số thứ tự (" + drCapQD["StudyStatusName"].ToString() + ")";
                            else
                                error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép hủy số thứ tự (" + drCapQD["StudyStatusName"].ToString() + ")";
                        }
                }
                    catch (Exception ex)
                    {
                        if (error == string.Empty)
                            error = ex.Message;
                        else
                            error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + ex.Message;
                    }
                }

                SplashScreenManager.CloseForm(false);

                GetData();

                if (error != string.Empty)
                    XtraMessageBox.Show(error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XtraMessageBox.Show("Hủy hoàn thành...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void lkuNamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lkuNamHoc.EditValue.ToString());
            DotXet();
        }

        private void btnCapQuyetDinh_Click(object sender, EventArgs e)
        {
            CapQuyetDinh();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkComboBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }

        private void lkuLoaiHinhDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }

        private void lkuHocKy_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }

        private void lkuLoaiChungChi_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }

        private void btnCapSTT_Click(object sender, EventArgs e)
        {
            CapSoThuTu();
        }

        private void btnHuySTT_Click(object sender, EventArgs e)
        {
            HuySoThuTu();
        }

        private void btn_huyQuyetDinh_Click(object sender, EventArgs e)
        {
            HuyQuyetDinh();
        }

        private void simpleButton_InDuLieu_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        #region Bảng điểm tín chỉ
        private void mnu_BangDiemTotNghiep_TC_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach ( int i in gridViewData.GetSelectedRows())
                { 
                    row = gridViewData.GetDataRow(i);
                    strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                        + "\" MaDot = \"" + lkuDotXet.EditValue.ToString()
                            + "\"/>";
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

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemTotNghiepDayDu_Yersin(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._User.StaffName, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region Bảng điểm niên chế
        private void mnu_BangDiemTotNghiep_NC_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                        + "\" MaDot = \"" + lkuDotXet.EditValue.ToString()
                            + "\"/>";
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

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._User.StaffName, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region private void mnu_KetQuaXepLoaiTN_NienChe_Click(object sender, EventArgs e)
        private void mnu_KetQuaXepLoaiTN_NienChe_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.KetQuaXepLoaiBangTN(lkuDotXet.EditValue.ToString());

                //if (_tbPrint.Rows.Count == 0)
                //    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_KetQuaXepLoaiBangTN_NienChe(_tbPrint, _ngayKyTen, User._User.StaffName.ToUpper(), true, true, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void mnu_GiayChungNhanTN_CDYT_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<GiayCNTN StudentID = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\"/>";
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKy = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                
                DataTable _tbPrint = new DataTable();
                //_tbPrint = BL_InBang.GiayChungNhanTotNghiep_CDYT(lkuDotXet.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), strXml);
                _tbPrint = BL_InBang.GiayChungNhanTotNghiep_CDYT(lkuDotXet.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(),strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                string NameControl = ((ToolStripMenuItem)sender).Name;
                string TextControl = ((ToolStripMenuItem)sender).Text;
                frm._load_XtraReport_GiayChungNhanTotNghiep_UEL(_tbPrint, _ngayKy, _CapBac, _nguoiKyTen, User._User.StaffName, User._CollegeLogo
                    , User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnu_DSSVCongNhanTN_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<CNTN MaSinhVien = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                        + "\" MaDotXet = \"" + lkuDotXet.EditValue.ToString()
                            + "\"/>";
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachCongNhanTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_DanhSachCongNhanTotNghiep_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnu_KetQuaXepLoaiBangTN_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.KetQuaXepLoaiBangTN(lkuDotXet.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_KetQuaXepLoaiBangTN(_tbPrint, _ngayKyTen, User._User.StaffName.ToUpper(), true, true, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEditQuyetDinh_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                frm_Grd_QuanLyQuyetDinh f = new frm_Grd_QuanLyQuyetDinh();
                f._isLinkFromStudentMarkDecisionInfo = true;

                if (rdGrpLoaiMauIn.SelectedIndex == 0)
                    f._decisionTypeID = 19;
                else
                    f._decisionTypeID = 191;

                f.ShowDialog();

                if (f._decisionNumber != string.Empty)
                {
                    _decisionTypeID = f._decisionTypeID;
                    _decisionNumber = f._decisionNumber;
                    _decisionAlias = f._decisionAlias;
                    _ngayKy = f._signDate;

                    if (_ngayKy != string.Empty)
                        buttonEditQuyetDinh.Text = _decisionAlias + " - Ngày ký: " + Convert.ToDateTime(_ngayKy).ToString("dd/MM/yyyy");
                    else
                        buttonEditQuyetDinh.Text = _decisionAlias + " - Ngày ký: ";
                }
            }
            catch { }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Danh sách cấp quyết định công nhận";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;

                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Danh sách cấp QĐCN_Đợt xét " + lkuDotXet.EditValue.ToString();

                    gridViewData.ExportToXlsx(sfdFiles.FileName, options);

                    gridViewData.OptionsSelection.MultiSelect = true;
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gridViewData.OptionsSelection.MultiSelect = true;
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            HeThong.frm_Grd_CotHienThi frm = new HeThong.frm_Grd_CotHienThi();
            frm._chucNang = "QDCongNhan";
            frm._dtData = _dtGridColumns;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm._isAccepted)
            {
                _dtGridColumns = frm._dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
            }
        }

        private void rdGrpLoaiMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoaiChungChi();
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                _decisionNumber = string.Empty; _decisionAlias = string.Empty; _ngayKy = string.Empty; _decisionTypeID = 19;
                _maLoaiXet = lkuLoaiChungChi.EditValue.ToString();
                _maDotXet = lkuDotXet.EditValue.ToString();
                buttonEditQuyetDinh.Text = string.Empty;

                GetData();
            }
            catch { }
        }

        #region mnu_46_DNU_ChungNhanHoanThanhKhoaHoc_Click
        private void mnu_46_DNU_ChungNhanHoanThanhKhoaHoc_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<GiayCNTNTemp StudentID = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                        + "\" MaDot = \"" + lkuDotXet.EditValue.ToString()
                            + "\"/>";
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                _tbPrint = BL_InBang.GiayChungNhanHoanThanh_UEL(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                string NameControl = ((ToolStripMenuItem)sender).Name;
                string TextControl = ((ToolStripMenuItem)sender).Text;
                frm._load_XtraReport_ChungNhanHocThanhKhoaHoc_UEL(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen
                    , User._CollegeLogo, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region UEL mnu_DSSVDuocCongNhanTN_KhongXetDaCapQD_Click
        private void mnu_DSSVDuocCongNhanTNKhongXetDaCapQD_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachCongNhanTotNghiep_KhongXetQD_UEL(lkuDotXet.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_DanhSachCongNhanTN_UEL_KhongXetDaCapQD(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion
    }
}