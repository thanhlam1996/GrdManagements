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
using DevExpress.XtraEditors.Controls;
using GrdCore.BLL;
using DevExpress.Common.Grid;
using DevExpress.XtraSplashScreen;
using GrdUI.ChungChi;

namespace GrdUI.InBang
{
    public partial class frm_Grd_QuyetDinhCongNhanVaDotCap : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;

        DataTable _dtNotIn = new DataTable();
        DataRow drNotIn;
        #endregion

        #region Inits
        public frm_Grd_QuyetDinhCongNhanVaDotCap()
        {
            InitializeComponent();
        }

        private void frm_Grd_DanhSachCap_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            try
            {                
                drGrids = (DataRow)dtGrid.Select("GridID = 'QDDotCap'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                drNotIn = (DataRow)dtGrid.Select("GridID = 'QDNgoaiDotCap'").GetValue(0);

                _dtNotIn = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drNotIn["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            MaLoaiChungChi();

            GetYearStudy();
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            GetGraduateLevels();

            GetStudyTypes();

            #region Quyết định chưa đưa vào đợt cấp
            GetGraduateLevels_QD();
            lookUpEditBacDaoTaoQD.EditValue = lkuBacDaoTao.EditValue.ToString();

            GetStudyTypes_QD();
            lookUpEditLHDTQD.EditValue = lkuLHDT.EditValue.ToString();

            GetYearStudy_QD();
            lookUpEditNamHoc.EditValue = lkuNamHoc.EditValue.ToString();

            GetTerms_QD(lkuNamHoc.EditValue.ToString());
            lookUpEditHocKy.EditValue = lookUpEditHocKy.EditValue.ToString();
            #endregion
        }
        #endregion

        #region Functions
        private void GetGraduateLevels()
        {
            try
            {
                DataTable dtData = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();

                lkuBacDaoTao.Properties.DataSource = dtData;
                lkuBacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                lkuBacDaoTao.Properties.ValueMember = "GraduateLevelID";

                LookUpColumnInfoCollection coll = lkuBacDaoTao.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GraduateLevelID", 50, "Mã"));
                coll.Add(new LookUpColumnInfo("GraduateLevelName", 100, "Tên"));

                lkuBacDaoTao.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuBacDaoTao.Properties.SearchMode = SearchMode.AutoComplete;
                lkuBacDaoTao.Properties.AutoSearchColumnIndex = 0;
                lkuBacDaoTao.Properties.NullText = string.Empty;

                if (User._CurrentGraduateLevelID.Trim() == string.Empty)
                    lkuBacDaoTao.ItemIndex = 0;
                else
                {
                    if (!User._CurrentGraduateLevelID.Trim().Contains(";"))
                        lkuBacDaoTao.EditValue = User._CurrentGraduateLevelID;
                    else
                        lkuBacDaoTao.EditValue = User._CurrentGraduateLevelID.Split(';')[0].ToString();
                }
            }
            catch { }
        }

        private void GetStudyTypes()
        {
            try
            {
                DataTable dtData = User._dsDataDictionaries.Tables["StudyTypes"].Copy();

                lkuLHDT.Properties.DataSource = dtData;
                lkuLHDT.Properties.DisplayMember = "StudyTypeName";
                lkuLHDT.Properties.ValueMember = "StudyTypeID";

                LookUpColumnInfoCollection coll = lkuLHDT.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("StudyTypeID", 50, "Mã"));
                coll.Add(new LookUpColumnInfo("StudyTypeName", 100, "Tên"));

                lkuLHDT.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuLHDT.Properties.SearchMode = SearchMode.AutoComplete;
                lkuLHDT.Properties.AutoSearchColumnIndex = 0;
                lkuLHDT.Properties.NullText = string.Empty;

                if (User._CurrentStudyTypeID.Trim() == string.Empty)
                    lkuLHDT.ItemIndex = 0;
                else
                {
                    if (!User._CurrentStudyTypeID.Trim().Contains(";"))
                        lkuLHDT.EditValue = User._CurrentStudyTypeID;
                    else
                        lkuLHDT.EditValue = User._CurrentStudyTypeID.Split(';')[0].ToString();
                }
            }
            catch { }
        }

        private void MaLoaiChungChi()
        {
            try
            {
                DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("CC; CB");

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

                lkuLoaiChungChi.EditValue = "TN";
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

        private void DotCap()
        {
            try
            {
                DataTable dtDotCap = BL_InBang.DotCap(lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString()
                    , lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), 0);

                lkuDotCap.Properties.DataSource = dtDotCap;
                lkuDotCap.Properties.DisplayMember = "TenDot";
                lkuDotCap.Properties.ValueMember = "ID";

                LookUpColumnInfoCollection coll = lkuDotCap.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaDot", "Mã"));
                coll.Add(new LookUpColumnInfo("TenDot", "Tên"));
                coll.Add(new LookUpColumnInfo("NgayKyBang", "Ngày ký bằng"));
                coll.Add(new LookUpColumnInfo("GhiChu", "Ghi chú"));

                lkuDotCap.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuDotCap.Properties.SearchMode = SearchMode.AutoComplete;
                lkuDotCap.Properties.AutoSearchColumnIndex = 0;
                lkuDotCap.Properties.NullText = string.Empty;

                lkuDotCap.ItemIndex = 0;
            }
            catch(Exception ex) { }
        }

        private void GetData()
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm(this, typeof(frm_Grd_ChoThucThi), true, true, false);

                DataSet dsData = BL_InBang.QuyetDinhCongNhanTotNghiepDotCapBang(lookUpEditBacDaoTaoQD.EditValue.ToString()
                        , lookUpEditLHDTQD.EditValue.ToString(), lookUpEditNamHoc.EditValue.ToString(), lookUpEditHocKy.EditValue.ToString()
                        , lkuLoaiChungChi.EditValue.ToString(), Convert.ToInt32(lkuDotCap.EditValue.ToString()));

                gridControlNotIn.DataSource = dsData.Tables[0].Copy();
                AppGridView.InitGridView(gridViewNotIn, drNotIn, _dtNotIn, User._foreignLanguage);

                gridControlData.DataSource = dsData.Tables[1].Copy();
                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteData()
        {
            try
            {
                string xmlData = string.Empty;
                foreach (int i in gridViewNotIn.GetSelectedRows())
                {
                    xmlData += "<QuyetDinhCongNhan SoQuyetDinh = \"" + gridViewNotIn.GetDataRow(i)["DecisionNumber"].ToString()
                        + "\"/>";
                }
                xmlData = "<Root>" + xmlData + "</Root>";

                string result = BL_InBang.HuyQuyetDinhVaDotCapBang(xmlData, Convert.ToInt32(lkuDotCap.EditValue.ToString()), User._UserID);

                if (result.Contains("..."))
                {
                    GetData();
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {
                string xmlData = string.Empty;
                foreach (int i in gridViewNotIn.GetSelectedRows())
                {
                    xmlData += "<QuyetDinhCongNhan SoQuyetDinh = \"" + gridViewNotIn.GetDataRow(i)["DecisionNumber"].ToString()
                        + "\"/>";
                }
                xmlData = "<Root>" + xmlData + "</Root>";

                string result = BL_InBang.LuuQuyetDinhVaDotCapBang(xmlData, Convert.ToInt32(lkuDotCap.EditValue.ToString()), User._UserID);

                if (result.Contains("..."))
                {
                    GetData();
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetGraduateLevels_QD()
        {
            try
            {
                DataTable dtData = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();

                lookUpEditBacDaoTaoQD.Properties.DataSource = dtData;
                lookUpEditBacDaoTaoQD.Properties.DisplayMember = "GraduateLevelName";
                lookUpEditBacDaoTaoQD.Properties.ValueMember = "GraduateLevelID";

                LookUpColumnInfoCollection coll = lookUpEditBacDaoTaoQD.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GraduateLevelID", 50, "Mã"));
                coll.Add(new LookUpColumnInfo("GraduateLevelName", 100, "Tên"));

                lookUpEditBacDaoTaoQD.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditBacDaoTaoQD.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditBacDaoTaoQD.Properties.AutoSearchColumnIndex = 0;
                lookUpEditBacDaoTaoQD.Properties.NullText = string.Empty;

                if (User._CurrentGraduateLevelID.Trim() == string.Empty)
                    lookUpEditBacDaoTaoQD.ItemIndex = 0;
                else
                {
                    if (!User._CurrentGraduateLevelID.Trim().Contains(";"))
                        lookUpEditBacDaoTaoQD.EditValue = User._CurrentGraduateLevelID;
                    else
                        lookUpEditBacDaoTaoQD.EditValue = User._CurrentGraduateLevelID.Split(';')[0].ToString();
                }
            }
            catch { }
        }

        private void GetStudyTypes_QD()
        {
            try
            {
                DataTable dtData = User._dsDataDictionaries.Tables["StudyTypes"].Copy();

                lookUpEditLHDTQD.Properties.DataSource = dtData;
                lookUpEditLHDTQD.Properties.DisplayMember = "StudyTypeName";
                lookUpEditLHDTQD.Properties.ValueMember = "StudyTypeID";

                LookUpColumnInfoCollection coll = lookUpEditLHDTQD.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("StudyTypeID", 50, "Mã"));
                coll.Add(new LookUpColumnInfo("StudyTypeName", 100, "Tên"));

                lookUpEditLHDTQD.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditLHDTQD.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditLHDTQD.Properties.AutoSearchColumnIndex = 0;
                lookUpEditLHDTQD.Properties.NullText = string.Empty;

                if (User._CurrentStudyTypeID.Trim() == string.Empty)
                    lookUpEditLHDTQD.ItemIndex = 0;
                else
                {
                    if (!User._CurrentStudyTypeID.Trim().Contains(";"))
                        lookUpEditLHDTQD.EditValue = User._CurrentStudyTypeID;
                    else
                        lookUpEditLHDTQD.EditValue = User._CurrentStudyTypeID.Split(';')[0].ToString();
                }
            }
            catch { }
        }

        private void GetYearStudy_QD()
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

                lookUpEditNamHoc.Properties.DataSource = myDataView.ToTable();
                lookUpEditNamHoc.Properties.DisplayMember = "YearStudy";
                lookUpEditNamHoc.Properties.ValueMember = "YearStudyID";

                LookUpColumnInfoCollection coll = lookUpEditNamHoc.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("YearStudy", 0, "Năm học"));

                lookUpEditNamHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditNamHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditNamHoc.Properties.AutoSearchColumnIndex = 0;
                lookUpEditNamHoc.Properties.NullText = string.Empty;

                lookUpEditNamHoc.ItemIndex = 0;
            }
            catch { }
        }

        private void GetTerms_QD(string yearStudy)
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

                lookUpEditHocKy.Properties.DataSource = dv.ToTable();
                lookUpEditHocKy.Properties.DisplayMember = "TermName";
                lookUpEditHocKy.Properties.ValueMember = "TermID";

                LookUpColumnInfoCollection coll = lookUpEditHocKy.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lookUpEditHocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditHocKy.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditHocKy.Properties.AutoSearchColumnIndex = 0;
                lookUpEditHocKy.Properties.NullText = string.Empty;

                lookUpEditHocKy.ItemIndex = 0;
            }
            catch { }
        }
        #endregion

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DanhSachCap_Load(null, null);
        }

        private void lkuLoaiChungChi_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }

        private void lkuNamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lkuNamHoc.EditValue.ToString());
            DotCap();
        }

        private void lkuHocKy_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }

        private void lkuBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }

        private void lkuLHDT_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }

        private void lookUpEditNamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms_QD(lookUpEditNamHoc.EditValue.ToString());
            //GetData();
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void lookUpEditBacDaoTaoQD_EditValueChanged(object sender, EventArgs e)
        {
            //GetData();
        }

        private void lookUpEditLHDTQD_EditValueChanged(object sender, EventArgs e)
        {
            //GetData();
        }

        private void lookUpEditHocKy_EditValueChanged(object sender, EventArgs e)
        {
            //GetData();
        }
        #endregion
    }
}