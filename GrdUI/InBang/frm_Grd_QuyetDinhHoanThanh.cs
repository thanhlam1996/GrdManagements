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
using ScrUI.Others;
using GrdReports;

namespace GrdUI.InBang
{
    public partial class frm_Grd_QuyetDinhHoanThanh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow _drGrids;

        string _maLoaiXet = string.Empty, _maDotXet = string.Empty, _decisionNumber = string.Empty, _decisionAlias = string.Empty, _ngayKy = string.Empty;
        int _decisionTypeID = 192;

        string[] _studyStatusID;
        #endregion

        #region Inits
        public frm_Grd_QuyetDinhHoanThanh()
        {
            InitializeComponent();
        }

        private void frm_Grd_QuyetDinhHoanThanh_Load(object sender, EventArgs e)
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

            LoaiChungChi();

            GetYearStudy();
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            DataTable dtTemp = User._dsDataDictionaries.Tables[0].Copy();
            DataRow drTemp = (DataRow)dtTemp.Select("SettingName = 'TinhTrangXetTotNghiep'").GetValue(0);
            _studyStatusID = drTemp["SettingStringData"].ToString().Split(';');
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
            string maHinhThucCapChungChi = "CB";

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

                _dtData = BL_InBang.DanhSachCapQuyetDinhHoanThanh(_maLoaiXet, _maDotXet);

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
                if (XtraMessageBox.Show("Bạn có muốn cấp quyết định hoàn thành chương trình ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

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
                            message = BL_InBang.CapNhatVaHuyQuyetDinhHoanThanh(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                                , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), _decisionNumber, _decisionTypeID, User._UserID);

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
                                error = drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép cấp quyết định (" + drCapQD["StudyStatusName"].ToString() + ")";
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
                if (XtraMessageBox.Show("Bạn có muốn hủy quyết định hoàn thành chương trình ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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
                            message = BL_InBang.CapNhatVaHuyQuyetDinhHoanThanh(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
                                , drCapQD["StudyProgramID"].ToString(), drCapQD["StudentID"].ToString(), "", _decisionTypeID, User._UserID);

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
                                error = drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép hủy quyết định (" + drCapQD["StudyStatusName"].ToString() + ")";
                            else
                                error += "\n" + drCapQD["StudentID"].ToString() + " -- " + drCapQD["MaChuanXet"].ToString() + " -- " + "Tình trạng không cho phép hủy quyết định (" + drCapQD["StudyStatusName"].ToString() + ")";
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

                            message = BL_InBang.CapNhatVaHuySoThuTuQuyetDinhHoanThanh(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
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
                            message = BL_InBang.CapNhatVaHuySoThuTuQuyetDinhHoanThanh(_maLoaiXet, _maDotXet, drCapQD["MaChuanXet"].ToString()
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

        private void mnu_31_UEL_GiayChungNhanHoanThanhKH_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEditQuyetDinh_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                frm_Grd_QuanLyQuyetDinh f = new frm_Grd_QuanLyQuyetDinh();
                f._isLinkFromStudentMarkDecisionInfo = true;

                f._decisionTypeID = 192;

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

                    options.SheetName = "Danh sách";

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
            DotXet();
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
            catch (Exception ex) { }
        }
        #endregion
    }
}