using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;
using System.Linq;
using GrdCore.BLL;
using GrdUI;
using DevExpress.XtraGrid;
using System.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Drawing;
using System.Collections.Generic;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_ChuanXet : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtNotPhanNhom = new DataTable(), _dtPhanNhom = new DataTable(),
            _dtChuanMonHoc = new DataTable(),_dtMonChuaDuaVaoChuan = new DataTable(), _dtCurriculums = new DataTable(), _dtPhanNhomDel = new DataTable(),
            _dtHocVienShow = new DataTable(), _dtHocVienSave = new DataTable() ,dtData = new DataTable(), _dtTinChi = new DataTable();
        string _ChuanID = string.Empty, _ologyName = string.Empty, _StudyProgramID = string.Empty,
            _CurriculumDel = string.Empty, _State = string.Empty, _strXmlHocVien= string.Empty;
        string _GroupID = string.Empty;
        string _ParentGroupID = string.Empty;
        DataSet _dsMonHoc = new DataSet();
        DataTable _dtKKT = new DataTable();
        #endregion

        #region Inits
        #region frm_Grd_ChuanXet()
        public frm_Grd_ChuanXet()
        {
            InitializeComponent();
        }
        #endregion

        #region frmPRJChuanXet_Load(object sender, EventArgs e)
        private void frmPRJChuanXet_Load(object sender, EventArgs e)
        {
            try
            {

                GetGraduateLevels();
                GetStudyTypes();
                GetOlogies();
                GetRegulations();
                GetLoaiXet();
                ClearText();
                if(_ChuanID!="")
                    GetDataTreeNhomTC(_ChuanID);

                _dtPhanNhomDel.Columns.AddRange(new DataColumn[]{ new DataColumn("Chon",typeof(string)),
                    new DataColumn("CurriculumID",typeof(string)), new DataColumn("CurriculumName",typeof(string)), new DataColumn("Credits",typeof(string)), new DataColumn("CurriculumType",typeof(string))
                });

                simpleButton_HoanTat_Click(null,null);
            }
            catch { }
        }
        #endregion
        #endregion

        #region Functions
        #region Lấy diều kiện xét 
        private void LayDieuKienXet(string MaLoaiXet, string QC)
        {      
            DataTable _dtDK = BL_ChungChi.DieuKienXet(MaLoaiXet);
            lookUpEdit_DKXet.Properties.DataSource = null;
            DataView myDataView = new DataView(_dtDK.Copy()); 
                
            myDataView.Sort = "TenDieuKien";

            lookUpEdit_DKXet.Properties.DataSource = myDataView.ToTable();
            lookUpEdit_DKXet.Properties.DisplayMember = "TenDieuKien";
            lookUpEdit_DKXet.Properties.ValueMember = "MaDieuKien";

            LookUpColumnInfoCollection col2 = lookUpEdit_DKXet.Properties.Columns;
            col2.Clear();
            col2.Add(new LookUpColumnInfo("TenDieuKien", 0, "Điều kiện"));

            lookUpEdit_DKXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookUpEdit_DKXet.Properties.SearchMode = SearchMode.AutoComplete;
            lookUpEdit_DKXet.Properties.AutoSearchColumnIndex = 0;
            if (_dtDK.Select("RegulationID = '" + QC + "'").Length > 0)
            {
                lookUpEdit_DKXet.EditValue = ((DataRow)_dtDK.Select("RegulationID = '" + QC + "'").GetValue(0))["MaDieuKien"].ToString();
            }
            else
            {
                lookUpEdit_DKXet.ItemIndex = -1;
            }
            lookUpEdit_DKXet.Properties.NullText = "<Chưa có dữ liệu>";
        }
        #endregion

        #region GetDataTreeList()
        void GetDataTreeList(string CourseID)
        {
            try
            {
                string lx = lookUpEdit_LoaiXet.EditValue.ToString();
                _dtData = BL_ChungChi.LayChuanXet(CourseID, lx);
                DataTable _dtChild = new DataTable();
                string temp = string.Empty;
                int count = 0;
                _dtChild.Columns.Add("Cha", typeof(string));
                _dtChild.Columns.Add("Con", typeof(string));
                _dtChild.Columns.Add("Chuẩn xét", typeof(string));
                DataTable _dtFather = new DataTable();
                _dtFather.Columns.Add("Cha", typeof(string));
                _dtFather.Columns.Add("Con", typeof(string));
                _dtFather.Columns.Add("Chuẩn xét", typeof(string));             
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr["OlogyID"].ToString().Trim() != temp || count == 0)
                    {
                        if (_dtFather.Select("Cha = '" + dr["OlogyID"].ToString() + "'").Length == 0)
                            _dtFather.Rows.Add(dr["OlogyID"].ToString(), dr["OlogyID"].ToString(), dr["OlogyID"].ToString()+"-"+dr["OlogyName"].ToString());

                        _dtChild.Rows.Add(dr["OlogyID"].ToString(), dr["ChuanID"].ToString(), dr["StudyProgramName"].ToString());
                        count++;
                    }
                    else
                    {
                        _dtChild.Rows.Add(dr["OlogyID"].ToString(), dr["ChuanID"].ToString(), dr["StudyProgramName"].ToString());
                    }
                }
                DataView view = new DataView(_dtFather);
                DataTable _distinctFather = view.ToTable(true, "Cha", "Con", "Chuẩn xét");
                _dtChild.Merge(_distinctFather);
                treeList_CTDT.DataSource = _dtChild;
                treeList_CTDT.ParentFieldName = "Cha";
                treeList_CTDT.KeyFieldName = "Con";
                treeList_CTDT.CollapseAll();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region void GetDataTreeNhomTC(string _ChuanID)
        private void GetDataTreeNhomTC(string _ChuanID)
        {
            try
            {
                DataTable _dtDataTree = new DataTable();
                _dtDataTree = BL_ChungChi.LayNhomTuChonTrongChuan(_ChuanID);

                DataTable _dtFather = new DataTable();
                _dtFather.Columns.AddRange(new DataColumn[]{new DataColumn("ID",typeof(string)),new DataColumn("SelectionParentID",typeof(string)),
                      new DataColumn("SelectionID", typeof(string)), new DataColumn("Tên nhóm", typeof(string)), new DataColumn("Số TC", typeof(string))});
                DataTable _dtChild = new DataTable();
                _dtChild.Columns.AddRange( new DataColumn[]{new DataColumn("ID", typeof(string)),new DataColumn("SelectionParentID",typeof(string)),
                      new DataColumn("SelectionID", typeof(string)), new DataColumn("Tên nhóm", typeof(string)), new DataColumn("Số TC", typeof(string))});

                foreach (DataRow row in _dtDataTree.Rows)
                {
                    if (row["SelectionParentID"].ToString() != row["SelectionID"].ToString())
                        _dtChild.Rows.Add(row["ID"].ToString(), row["SelectionParentID"].ToString(), row["SelectionID"].ToString(), row["SelectionName"].ToString(), row["GatherCredits"].ToString());
                    else
                        _dtFather.Rows.Add(row["ID"].ToString(), row["SelectionParentID"].ToString(), row["SelectionID"].ToString(), row["SelectionName"].ToString(), row["GatherCredits"].ToString());
                }
                _dtChild.Merge(_dtFather);
                treeListNhomTC.Columns.Clear();
                treeListNhomTC.DataSource = _dtChild;
                treeListNhomTC.ParentFieldName = "SelectionParentID";
                treeListNhomTC.KeyFieldName = "ID";
                treeListNhomTC.VisibleColumns["SelectionID"].Visible = false;
                treeListNhomTC.BestFitColumns();
                treeListNhomTC.CollapseAll();
            }
            catch { }
        }
        #endregion
        
        #region private void GetYearStudy()
        private void GetCourses(string bacDT, string LHDT)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(bacDT, LHDT);
                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "CourseName DESC";
                lookUpEdit_KhoaHoc.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_KhoaHoc.Properties.DisplayMember = "CourseName";
                lookUpEdit_KhoaHoc.Properties.ValueMember = "CourseID";
                lookUpEdit_KhoaHoc.EditValue = myDataView[0].Row[0].ToString();
                LookUpColumnInfoCollection coll = lookUpEdit_KhoaHoc.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("CourseID", 0, "Mã khóa học"));
                coll.Add(new LookUpColumnInfo("CourseName", 0, "Khóa học"));
                lookUpEdit_KhoaHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_KhoaHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_KhoaHoc.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }
        #endregion

        #region private void GetStudyTypes()
        private void GetStudyTypes()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = User._dsDataDictionaries.Tables["StudyTypes"];
                DataView myDataView = new DataView(dtData);
                lookUpEdit_LHDT.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_LHDT.Properties.DisplayMember = "StudyTypeName";
                lookUpEdit_LHDT.Properties.ValueMember = "StudyTypeID";
                if(dtData.Rows.Count > 0)
                    lookUpEdit_LHDT.EditValue = myDataView[0].Row[1].ToString();
                lookUpEdit_LHDT.Properties.Columns.Clear();
                lookUpEdit_LHDT.Properties.Columns.Add(new LookUpColumnInfo("StudyTypeID", 0, "Mã LHDT"));
                lookUpEdit_LHDT.Properties.Columns.Add(new LookUpColumnInfo("StudyTypeName", 0, "Loại hình đào tạo"));
                lookUpEdit_LHDT.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_LHDT.Properties.NullText = "";
                lookUpEdit_LHDT.EditValue = User._CurrentStudyTypeID;
            }
            catch { }
        }
        #endregion

        #region private void GetGraduateLevels()
        private void GetGraduateLevels()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = User._dsDataDictionaries.Tables["GraduateLevels"];
                DataView myDataView = new DataView(dtData);
                lookUpEdit_BacDaoTao.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_BacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                lookUpEdit_BacDaoTao.Properties.ValueMember = "GraduateLevelID";
                if (dtData.Rows.Count > 0)
                    lookUpEdit_BacDaoTao.EditValue = myDataView[0].Row[1].ToString();
                lookUpEdit_BacDaoTao.Properties.Columns.Clear();
                lookUpEdit_BacDaoTao.Properties.Columns.Add(new LookUpColumnInfo("GraduateLevelID", 100, "Mã bậc DT"));
                lookUpEdit_BacDaoTao.Properties.Columns.Add(new LookUpColumnInfo("GraduateLevelName", 100, "Bậc đào tạo"));
                lookUpEdit_BacDaoTao.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_BacDaoTao.Properties.NullText = "";
                lookUpEdit_BacDaoTao.EditValue = User._CurrentGraduateLevelID;
            }
            catch { }
        }
        #endregion

        #region private void GetOlogies()
        private void GetOlogies()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = User._dsDataDictionaries.Tables["Ologies"];
                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "DisplayName ASC";
                lookUpEdit_NganhDaoTao.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_NganhDaoTao.Properties.DisplayMember = "DisplayName";
                lookUpEdit_NganhDaoTao.Properties.ValueMember = "OlogyID";
                if(dtData.Rows.Count>0)
                    lookUpEdit_NganhDaoTao.EditValue = myDataView[0].Row[0].ToString();
                lookUpEdit_NganhDaoTao.Properties.Columns.Clear();
                lookUpEdit_NganhDaoTao.Properties.Columns.Add(new LookUpColumnInfo("OlogyID", 100, "Mã ngành"));
                lookUpEdit_NganhDaoTao.Properties.Columns.Add(new LookUpColumnInfo("DisplayName", 100, "Ngành đào tạo"));
                lookUpEdit_NganhDaoTao.ItemIndex = -1;
                lookUpEdit_NganhDaoTao.Properties.NullText = "Chưa có dữ liệu";

            }
            catch { }
        }
        #endregion

        #region private void GetRegulations()
        private void GetRegulations()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = BL_ChungChi.GetRegulations();
                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "RegulationName ASC";
                lookUpEdit_QuyCheDaoTao.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_QuyCheDaoTao.Properties.DisplayMember = "RegulationName";
                lookUpEdit_QuyCheDaoTao.Properties.ValueMember = "RegulationID";
                if (dtData.Rows.Count > 0)
                    lookUpEdit_QuyCheDaoTao.EditValue = myDataView[0].Row[0].ToString();
                lookUpEdit_QuyCheDaoTao.Properties.Columns.Clear();
                lookUpEdit_QuyCheDaoTao.Properties.Columns.Add(new LookUpColumnInfo("RegulationID", 0, "Mã quy chế"));
                lookUpEdit_QuyCheDaoTao.Properties.Columns.Add(new LookUpColumnInfo("RegulationName", 0, "Quy chế đào tạo"));
                lookUpEdit_QuyCheDaoTao.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }
        #endregion

        #region private void GetStudyPrograms(string @StudyProgramSearch)
        private void GetStudyPrograms(string @StudyProgramSearch)
        {
            try
            {
                dtData = new DataTable();
                dtData = BL_ChungChi.GetStudyPrograms(@StudyProgramSearch, "#");
                if (dtData.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu.", "UIS - Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dtData.Rows.Count == 1)
                {
                    buttonEdit_StudyProgram.EditValue = dtData.Rows[0]["StudyProgramID"].ToString();
                    buttonEdit_StudyProgram.Text = dtData.Rows[0]["StudyProgramID"].ToString() + " - " + dtData.Rows[0]["StudyProgramName"].ToString();
                    lookUpEdit_NganhDaoTao.EditValue = dtData.Rows[0]["OlogyID"].ToString() ;
                    textBox_MaChuanXet.Text = dtData.Rows[0]["StudyProgramID"].ToString() + "_" + lookUpEdit_LoaiXet.EditValue.ToString();
                    txtTenChuongTrinh.Text = dtData.Rows[0]["StudyProgramName"].ToString();
                    lookUpEdit_QuyCheDaoTao.EditValue = dtData.Rows[0]["RegulationID"].ToString();


                }
                else
                {

                }
            }
            catch { }
        }
        #endregion

        #region Lấy môn học theo chuẩn và điều kiện xét
        private void GetCurriculums(string _ChuanID, string DKXet)
        {
            try
            {
                #region gridview_CTDT MonHocTrongChuan
                //_dsMonHoc = BL_ChungChi.LayMonHocTrongChuan(_ChuanID, lookUpEdit_DKXet.EditValue.ToString());
                _dsMonHoc = BL_ChungChi.LayMonHocTrongChuan(_ChuanID, DKXet);
                _dtChuanMonHoc = _dsMonHoc.Tables["MonHocChuanXet"].Copy();
                _dtMonChuaDuaVaoChuan = _dsMonHoc.Tables["MonHocDinhNghiaXet"].Copy();

                _dtCurriculums = BL_ChungChi.GetCurriculums(_StudyProgramID);
                #endregion
                _dtChuanMonHoc.Columns["CurriculumName"].AllowDBNull = true;
                _dtChuanMonHoc.Columns["Credits"].AllowDBNull = true;
                _dtChuanMonHoc.Columns["CurriculumType"].DefaultValue = true;
                _dtChuanMonHoc.Columns["CurriculumType"].ReadOnly = false;

                _dtChuanMonHoc.Columns["Chon"].ReadOnly = false;
                _dtChuanMonHoc.Columns["BatBuocXet"].ReadOnly = false;
                //_dtChuanMonHoc.Columns["StudyPartID"].AllowDBNull = true;
                _dtChuanMonHoc.Columns["StudyPartName"].AllowDBNull = true;
                gridControl_CTDT.DataSource = _dtChuanMonHoc;
                AppGridView.InitGridView(gridView_CTDT, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridView_CTDT,
                    new string[] { "Chon", "CurriculumID", "CurriculumName", "Credits", "CurriculumType", "CurriculumGroupName", "StudyPartID", "DiemDat", "BatBuocXet", "Del" },
                    new string[] { "Chọn xóa", "Mã môn học", "Tên môn học", "STC", "Bắt buộc", "Nhóm môn học", "Khối kiến thức", "Điểm đạt", "Bắt buộc xét", "Xóa" },
                    new int[] { 50, 70, 200, 50, 70, 100, 100, 100, 100, 70 });
                AppGridView.AlignField(gridView_CTDT, new string[] { "Chon", "CurriculumID", "CurriculumType", "Credits", "DiemDat", "StudyPartID", "BatBuocXet" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.SummaryField(gridView_CTDT, "CurriculumName", "Tổng số môn học = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.ReadOnlyColumn(gridView_CTDT, new string[] { "Credits", "CurriculumName" });
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion

        #region void ClearText()
        private void ClearText()
        {
            textBox_MaChuanXet.ResetText();
            txtTenChuongTrinh.ResetText();
            treeList_CTDT.Enabled = false;
            _ChuanID = "";
            _StudyProgramID = "";
            txt_STC_TichLuy.Text = "0";
            txt_STC_TuChon.Text = "0";
            txtState.ResetText();
            txtTitle.ResetText();
            txtTitle.ResetText();
            buttonEdit_StudyProgram.Text = "";
            lookUpEdit_NganhDaoTao.ItemIndex = -1;
            xtraTabControl.Enabled = true;
            btnClearNew.Enabled = false;
            btnSave.Enabled = false;
            btnDel.Enabled = false;

            gridControl_CTDT.DataSource = null;
            gridControl_NotPhanNhom.DataSource = null;
            gridControl_PhanNhom.DataSource = null;
            treeListNhomTC.DataSource = null;
            gridControl_SaveHV.DataSource = null;

            ButtonStatus(true);
            label_TrangThai.Text = "THÊM MỚI CHUẨN XÉT";
        }
        #endregion

        #region void ESC()
        private void ESC()
        {
            this.Close();
        }
        #endregion

        #region void SaveData()
        private void SaveData()
        {
            try
            {
                int result = 1;
                string strThangDiem = string.Empty;
                string strXml = "<Root>";

                if (Convert.ToString(textBox_MaChuanXet.Text.ToString()) == lookUpEdit_NganhDaoTao.EditValue.ToString())
                {
                    XtraMessageBox.Show("Mã chuẩn không được trùng với mã ngành", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (buttonEdit_StudyProgram.EditValue.ToString() != "" && textBox_MaChuanXet.Text != "" && txtTenChuongTrinh.Text!="")
                {

                    strXml += "<Datas MaChuanXet = \"" + Convert.ToString(textBox_MaChuanXet.Text.ToString()) +
                        "\" StudyProgramID = \"" + _StudyProgramID +
                        "\" TenChuanXet = \"" + txtTenChuongTrinh.Text.ToString() +
                        "\" OlogyID = \"" + lookUpEdit_NganhDaoTao.EditValue.ToString() +
                        "\" SelectionCredits = \"" + txt_STC_TuChon.Text.ToString() +
                        "\" MandatoryGatherCredits = \"" + txt_STC_TichLuy.Text.ToString() +
                        "\" RegulationID = \"" + lookUpEdit_QuyCheDaoTao.EditValue.ToString() +
                        "\" MaDieuKien = \"" + lookUpEdit_DKXet.EditValue.ToString() +
                        "\" STCTCTD = \"" + textEdit_TinChiTCTD.Text.ToString() +
                        "\" MaChuanXet_Old = \"" + _ChuanID +
                        "\"/>";
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng nhập đủ thông tin", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                strXml += "</Root>";
                if (buttonEdit_StudyProgram.EditValue.ToString() != "" && textBox_MaChuanXet.Text != "" && txtTenChuongTrinh.Text!="")
                {
                       result=BL_ChungChi.LuuChuanXet(strXml);
                }

                if (result ==0)
                {
                    XtraMessageBox.Show("Lưu dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lookUpEdit_KhoaHoc_EditValueChanged(null,null);
                }
                else if(result==1)
                {
                    XtraMessageBox.Show("Không thể chỉnh sửa\nChuẩn này đã được xét", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookUpEdit_KhoaHoc_EditValueChanged(null, null);
                }
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region void SaveDataCTDT()
        private void SaveDataCTDT()
        {
            try
            {
                bool result = false;
                string strXml = "<Root>";
                DataTable _dtTemp = (DataTable)gridControl_CTDT.DataSource;
                foreach (DataRow _dr in _dtTemp.Rows)
                {
                    strXml += "<Datas ChuanID = \"" + _ChuanID +
                        "\" CurriculumID = \"" + _dr["CurriculumID"].ToString() +
                        "\" CurriculumType = \"" + _dr["CurriculumType"].ToString() +
                        "\" DiemDat = \"" + _dr["DiemDat"].ToString() +
                        "\" BatBuocXet = \"" + _dr["BatBuocXet"].ToString() +
                        "\"/>";
                }
                strXml += "</Root>";
                if (buttonEdit_StudyProgram.EditValue != DBNull.Value && textBox_MaChuanXet.Text != "")
                {
                    BL_ChungChi.LuuMonHocChuan(strXml);
                    result = true;
                }

                if (result == true)
                {
                    XtraMessageBox.Show("Lưu dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region void SaveDataNhomTC()
        private void SaveDataNhomTC()
        {
            try
            {
                bool result = false;
                string strXml = "<Root>";
                DataTable _dtTemp = (DataTable)gridControl_PhanNhom.DataSource as DataTable;

                if (_GroupID != string.Empty)
                {
                    foreach (DataRow dr in _dtTemp.Rows)
                    {
                            strXml += "<Datas GroupID = \"" + _GroupID.ToString() +
                            "\" GroupParentID = \"" + _ParentGroupID.ToString() +
                             "\" CurriculumID = \"" + dr["CurriculumID"].ToString() +
                            "\"/>";
                    }
                }
                else
                     XtraMessageBox.Show("Chưa chọn nhóm tự chọn", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                strXml += "</Root>";
                if (Convert.ToString(_GroupID) != "")
                {
                    BL_ChungChi.LuuPhanNhom(strXml);
                    result = true;
                }

                if (result == true)
                {
                    XtraMessageBox.Show("Lưu dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch{ }
        }
        #endregion

        #region void SaveHocVien()
        private void SaveHocVien()
        {
            try
            {
                bool result = false;
                string strXml = "<Root>";
                DataTable _dtTemp = (DataTable)gridControl_SaveHV.DataSource;
                foreach (DataRow _dr in _dtTemp.Rows)
                {
                    strXml += "<Datas StudentID = \"" + _dr["StudentID"].ToString() +
                       "\" MaChuanXet = \"" + _ChuanID +
                       "\"/>";
                }
                strXml += "</Root>";

                BL_ChungChi.LuuHocVienChuan(strXml, _ChuanID);
                result = true;

                if (result == true)
                {
                    XtraMessageBox.Show("Lưu dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region void DelPhanNhomTC()
        private void DelPhanNhomTC()
        {
            try
            {
                bool result = false;
                string strXml = "<Root>";
                if (_dtPhanNhomDel.Rows.Count>0)
                {
                    foreach (DataRow dr in _dtPhanNhomDel.Rows)
                    {
                        if (dr.RowState == DataRowState.Added | dr.RowState == DataRowState.Modified)
                            strXml += "<Datas GroupID = \"" + _GroupID.ToString() +
                             "\" CurriculumID = \"" + dr["CurriculumID"].ToString() +
                            "\"/>";
                    }
                }
                else
                    XtraMessageBox.Show("Dữ liệu cần xóa chưa tồn tại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                strXml += "</Root>";
                if (_dtPhanNhomDel.Rows.Count>0)
                {
                    BL_ChungChi.XoaPhanNhom(strXml);
                    result = true;
                }

                if (result == true)
                    XtraMessageBox.Show("Xóa dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    XtraMessageBox.Show("Xóa dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region void DelHocVien()
        private void DelHocVien()
        {
            try
            {
                bool result = false;
                if (_strXmlHocVien != "<Root></Root>")
                {
                    BL_ChungChi.XoaHocVienChuan(_strXmlHocVien);
                    result = true;
                }
                if (result == true)
                    XtraMessageBox.Show("Xóa dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    XtraMessageBox.Show("Xóa dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region void DelCTDT()
        private void DelCTDT()
        {
            try
            {
                string[] _split = _CurriculumDel.Split(';');
                string strXml = "<Root>";
                bool result = false;
                if (textBox_MaChuanXet.Text != "")
                {
                    foreach (string s in _split)
                    {
                        strXml += "<Datas ChuanID = \"" + _ChuanID +
                           "\" CurriculumID = \"" + s +
                           "\"/>";
                    }
                    strXml += "</Root>";
                    if (!strXml.Equals("<Root></Root>"))
                    {
                        BL_ChungChi.XoaMonHocChuan(strXml);
                        result = true;
                    }
                    if (result == true)
                    {
                        frmPRJChuanXet_Load(null, null);
                        XtraMessageBox.Show("Xóa dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Xóa dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    XtraMessageBox.Show("Xóa dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region DelData()
        private void DelData()
        {
            try
            {
                string result = string.Empty;
                if (XtraMessageBox.Show("Xóa chuẩn đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;
                if (textBox_MaChuanXet.Text != "")
                {
                    result = BL_ChungChi.XoaChuanXet(_ChuanID, string.Empty);
                }
                if (result != "")
                {
                    if (result == "0")
                    {
                        lookUpEdit_KhoaHoc_EditValueChanged(null, null);
                        XtraMessageBox.Show("Xóa dữ liệu thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        XtraMessageBox.Show("Chuẩn đang có sinh viên được công nhận tốt nghiệp.\n           Xóa dữ liệu không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    XtraMessageBox.Show("Xóa dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region private void GetLoaiXet()
        private void GetLoaiXet()
        {
            try
            {
                DataTable _dtLoaiChungChi = BL_ChungChi.LayDanhSachLoaiChungChi();
                lookUpEdit_LoaiXet.Properties.DataSource = null;

                DataView myDataViewCC = new DataView(_dtLoaiChungChi.Copy());
                myDataViewCC.Sort = "TenLoaiChungChi";

                lookUpEdit_LoaiXet.Properties.DataSource = myDataViewCC.ToTable();
                lookUpEdit_LoaiXet.Properties.DisplayMember = "TenLoaiChungChi";
                lookUpEdit_LoaiXet.Properties.ValueMember = "MaLoaiChungChi";

                LookUpColumnInfoCollection col2 = lookUpEdit_LoaiXet.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("TenLoaiChungChi", 0, "Loại chứng chỉ"));

                lookUpEdit_LoaiXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_LoaiXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_LoaiXet.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_LoaiXet.ItemIndex = 0;
                lookUpEdit_LoaiXet.Properties.NullText = "";
                lookUpEdit_LoaiXet.EditValue = "TN";

            }
            catch { }
        }
        #endregion

        #region Tình trạng button
        private void ButtonStatus(bool _Status)
        {
            buttonEdit_StudyProgram.Enabled = _Status;
            buttonEdit_StudyProgram.ForeColor = Color.Black;
            lookUpEdit_NganhDaoTao.Enabled = _Status;
            lookUpEdit_NganhDaoTao.ForeColor = Color.Black;
            //textBox_MaChuanXet.Enabled = _Status;
            //textBox_MaChuanXet.ForeColor = Color.Black;
            txtTenChuongTrinh.Enabled = _Status;
            txtTenChuongTrinh.ForeColor = Color.Black;
            lookUpEdit_QuyCheDaoTao.Enabled = _Status;
            lookUpEdit_QuyCheDaoTao.ForeColor = Color.Black;
            lookUpEdit_DKXet.Enabled = _Status;
            lookUpEdit_DKXet.ForeColor = Color.Black;
            txt_STC_TichLuy.Enabled = _Status;
            txt_STC_TichLuy.ForeColor = Color.Black;
            txt_STC_TuChon.Enabled = _Status;
            txt_STC_TuChon.ForeColor = Color.Black;
            textEdit_TinChiTCTD.Enabled = _Status;
            textEdit_TinChiTCTD.ForeColor = Color.Black;

            gridView_CTDT.OptionsBehavior.Editable = _Status;
            gridView_NotPhanNhom.OptionsBehavior.Editable = _Status;
            gridView_PhanNhom.OptionsBehavior.Editable = _Status;
            gridView_SaveHV.OptionsBehavior.Editable = _Status;

            simpleButton_CopyChuan.Enabled = _Status;
            btnCopyCurriculum.Enabled = _Status;
            simpleButton_XoaMH.Enabled = _Status;
            simpleButton_Luu.Enabled = _Status;
            btnInsertTree.Enabled = _Status;
            btnUpdate.Enabled = _Status;
            btnDelTC.Enabled = _Status;
            btnDownAll.Enabled = _Status;
            btnDown.Enabled = _Status;
            btnUp.Enabled = _Status;
            btnUpAll.Enabled = _Status;
            btnAllNext.Enabled = _Status;
            btnNext.Enabled = _Status;
            btnPrevious.Enabled = _Status;
            btnAllPrevious.Enabled = _Status;
        }
        #endregion
        private void DoubleTreeList_Click(object keyValue)
        {
            foreach (DataRow dr in _dtData.Rows)
            {
                if (keyValue.ToString().Trim().Equals(dr["MaChuanXet"].ToString().Trim()))
                {
                    #region set Text tab "Thông tin chung"
                    label_TrangThai.Text = "CHỈNH SỬA THÔNG TIN CHUẨN XÉT";
                    txt_STC_TichLuy.Text = Convert.ToString(dr["STCTLBB"]);
                    txt_STC_TuChon.Text = Convert.ToString(dr["STCTLTC"]);
                    txtTenChuongTrinh.Text = Convert.ToString(dr["TenChuanXet"]);
                    textBox_MaChuanXet.Text = Convert.ToString(dr["MaChuanXet"]);
                    _ChuanID = Convert.ToString(dr["MaChuanXet"]);
                    _StudyProgramID = Convert.ToString(dr["StudyProgramID"]);
                    lookUpEdit_NganhDaoTao.EditValue = dr["OlogyID"].ToString();
                    txtTitle.Text = "Mã chuẩn xét: " + Convert.ToString(keyValue) + " - Tên ngành: " + lookUpEdit_NganhDaoTao.Text.ToString();
                    lookUpEdit_QuyCheDaoTao.EditValue = Convert.ToString(dr["RegulationID"]);
                    buttonEdit_StudyProgram.EditValue = Convert.ToString(dr["StudyProgramID"]);
                    _State = dr["State"].ToString();
                    lookUpEdit_DKXet.EditValue = dr["MaDieuKien"].ToString();
                    textEdit_TinChiTCTD.Text = dr["STCTCTD"].ToString();
                    #endregion

                    #region Tab "Chương trình đào tạo"
                    GetCurriculums(_ChuanID, lookUpEdit_DKXet.EditValue.ToString());

                    #region Button del in gridview_CTDT
                    AppGridView.RegisterControlField(gridView_CTDT, "Del", repositoryItemButtonEditDelete);
                    ///
                    _dtKKT = BL_ChungChi.LayDanhSachKhoiKienThuc();
                    repositoryItemLookUpEdit_StudyPart.DataSource = _dtKKT;
                    repositoryItemLookUpEdit_StudyPart.DisplayMember = "StudyPartName";
                    repositoryItemLookUpEdit_StudyPart.ValueMember = "StudyPartID";
                    repositoryItemLookUpEdit_StudyPart.NullText = string.Empty;

                    LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_StudyPart.Columns;
                    coll.Clear();
                    coll.Add(new LookUpColumnInfo("StudyPartID", 0, "Mã KKT"));
                    coll.Add(new LookUpColumnInfo("StudyPartName", 1, "Khối kiến thức"));

                    repositoryItemLookUpEdit_StudyPart.BestFitMode = BestFitMode.BestFitResizePopup;
                    repositoryItemLookUpEdit_StudyPart.SearchMode = SearchMode.AutoComplete;
                    repositoryItemLookUpEdit_StudyPart.AutoSearchColumnIndex = 0;
                    AppGridView.RegisterControlField(gridView_CTDT, "StudyPartID", repositoryItemLookUpEdit_StudyPart);
                    ///

                    //repositoryItemLookUpEdit_Curiculumns.DataSource = _dtChuanMonHoc;
                    repositoryItemLookUpEdit_Curiculumns.DataSource = _dtMonChuaDuaVaoChuan;
                    repositoryItemLookUpEdit_Curiculumns.DisplayMember = "CurriculumID";
                    repositoryItemLookUpEdit_Curiculumns.ValueMember = "CurriculumID";
                    repositoryItemLookUpEdit_Curiculumns.NullText = string.Empty;
                    #endregion

                    #region lookupedit in gridview_CTDT
                    LookUpColumnInfoCollection col = repositoryItemLookUpEdit_Curiculumns.Columns;
                    col.Clear();
                    col.Add(new LookUpColumnInfo("CurriculumID", 0, "Mã môn học"));
                    col.Add(new LookUpColumnInfo("CurriculumName", 1, "Tên môn học"));

                    repositoryItemLookUpEdit_Curiculumns.BestFitMode = BestFitMode.BestFitResizePopup;
                    repositoryItemLookUpEdit_Curiculumns.SearchMode = SearchMode.AutoComplete;
                    repositoryItemLookUpEdit_Curiculumns.AutoSearchColumnIndex = 0;
                    AppGridView.RegisterControlField(gridView_CTDT, "CurriculumID", repositoryItemLookUpEdit_Curiculumns);
                    //repositoryItemLookUpEdit_Curiculumns.DataSource = _dtCurriculums;
                    gridView_CTDT.OptionsView.ColumnAutoWidth = true;
                    gridView_CTDT.BestFitColumns();
                    #endregion

                    #endregion

                    #region Lock Tình trạng
                    if (_State.ToString().ToLower().Trim() == "true")
                    {
                        btnDel.Enabled = false;
                        btnSave.Enabled = false;
                        //btnCopyCurriculum.Enabled = true;
                        txtState.Text = "Tình trạng: Đã khóa";
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnDel.Enabled = true;
                        //btnCopyCurriculum.Enabled = false;
                        txtState.Text = "Tình trạng: Đang mở";
                        btnSave.Enabled = true;
                    }
                    #endregion

                    #region D/s môn học chưa phân nhóm
                    GetDataTreeNhomTC(_ChuanID);
                    try
                    {
                        DataTable _dtTree = (DataTable)treeListNhomTC.DataSource;
                        if (_dtTree.Rows.Count > 0)
                        {
                            DataSet dsPhanNhom = new DataSet();
                            if (Convert.ToString(_GroupID) != "")
                                dsPhanNhom = BL_ChungChi.NhomMonHocTuChon(_ChuanID, "", _GroupID.ToString(), lookUpEdit_DKXet.EditValue.ToString());
                            else
                                dsPhanNhom = BL_ChungChi.NhomMonHocTuChon(_ChuanID, "", _dtTree.Rows[0]["SelectionID"].ToString(), lookUpEdit_DKXet.EditValue.ToString());

                            _dtNotPhanNhom = dsPhanNhom.Tables["ChuaPhanNhom"].Copy();
                            _dtPhanNhom = dsPhanNhom.Tables["PhanNhom"].Copy();

                            _dtNotPhanNhom.Columns["Chon"].ReadOnly = false;
                            _dtPhanNhom.Columns["Chon"].ReadOnly = false;

                            //gridView_NotPhanNhom.Columns.Clear();
                            //gridView_PhanNhom.Columns.Clear();

                            gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;

                            AppGridView.InitGridView(gridView_NotPhanNhom, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                            AppGridView.ShowField(gridView_NotPhanNhom,
                                new string[] { "Chon", "CurriculumID", "CurriculumName", "Credits" },
                                new string[] { "Chon", "Mã môn học", "Tên môn học", "STC" },
                                new int[] { 80, 150, 250, 100 });
                            AppGridView.AlignField(gridView_NotPhanNhom, new string[] { "Chon", "CurriculumID", "Credits" },
                                DevExpress.Utils.HorzAlignment.Center);
                            AppGridView.SummaryField(gridView_NotPhanNhom, "CurriculumName", "Tổng số môn học = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                            AppGridView.ReadOnlyColumn(gridView_NotPhanNhom, new string[] { "Credits", "CurriculumID", "CurriculumName" });
                            gridView_NotPhanNhom.OptionsView.ColumnAutoWidth = true;
                            gridView_NotPhanNhom.BestFitColumns();

                            gridControl_PhanNhom.DataSource = _dtPhanNhom;

                            AppGridView.InitGridView(gridView_PhanNhom, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                            AppGridView.ShowField(gridView_PhanNhom,
                                new string[] { "Chon", "CurriculumID", "CurriculumName", "Credits" },
                                new string[] { "Chon", "Mã môn học", "Tên môn học", "STC" },
                                new int[] { 80, 150, 250, 100 });
                            AppGridView.AlignField(gridView_PhanNhom, new string[] { "Chon", "CurriculumID", "Credits" },
                                DevExpress.Utils.HorzAlignment.Center);
                            AppGridView.SummaryField(gridView_PhanNhom, "CurriculumName", "Tổng số môn học = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                            AppGridView.ReadOnlyColumn(gridView_PhanNhom, new string[] { "Credits", "CurriculumID", "CurriculumName" });
                            gridView_PhanNhom.OptionsView.ColumnAutoWidth = true;
                            gridView_PhanNhom.BestFitColumns();

                        }
                    }

                    catch (Exception ex) { }
                    #endregion

                    #region Học viên
                    DataSet _dsHocVien = new DataSet();

                    _dsHocVien = BL_ChungChi.HocVienChuanXet(_ChuanID);
                    _dtHocVienShow = _dsHocVien.Tables["Show"].Copy();
                    _dtHocVienSave = _dsHocVien.Tables["Save"].Copy();
                    _dtHocVienSave.Columns["Chon"].ReadOnly = false;
                    _dtHocVienShow.Columns["Chon"].ReadOnly = false;

                    gridControl_showHV.DataSource = _dtHocVienShow;
                    AppGridView.InitGridView(gridView_showHV, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                    AppGridView.ShowField(gridView_showHV,
                        new string[] { "Chon", "StudentID", "FullName", "BirthDay", "BirthPlace" },
                        new string[] { "Chọn", "Mã học viên", "Tên học viên", "Ngày sinh", "Nơi sinh" },
                        new int[] { 50, 70, 150, 80, 100, 80 });
                    AppGridView.AlignField(gridView_showHV, new string[] { "Chon", "StudentID", "BirthDay", "BirthPlace" },
                        DevExpress.Utils.HorzAlignment.Center);
                    AppGridView.SummaryField(gridView_showHV, "FullName", "Tổng số học viên = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                    AppGridView.ReadOnlyColumn(gridView_showHV, new string[] { "StudentID", "FullName", "BirthDay", "BirthPlace" });
                    gridView_showHV.OptionsView.ColumnAutoWidth = true;
                    gridView_showHV.BestFitColumns();

                    gridControl_SaveHV.DataSource = _dtHocVienSave;
                    AppGridView.InitGridView(gridView_SaveHV, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                    AppGridView.ShowField(gridView_SaveHV,
                        new string[] { "Chon", "StudentID", "FullName", "BirthDay", "BirthPlace" },
                        new string[] { "Chon", "Mã học viên", "Tên học viên", "Ngày sinh", "Nơi sinh" },
                        new int[] { 50, 70, 150, 80, 100, 80 });
                    AppGridView.AlignField(gridView_SaveHV, new string[] { "Chon", "StudentID", "BirthDay", "BirthPlace" },
                        DevExpress.Utils.HorzAlignment.Center);
                    AppGridView.SummaryField(gridView_SaveHV, "FullName", "Tổng số học viên = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                    AppGridView.ReadOnlyColumn(gridView_SaveHV, new string[] { "StudentID", "FullName", "BirthDay", "BirthPlace" });

                    gridView_SaveHV.OptionsView.ColumnAutoWidth = true;
                    gridView_SaveHV.BestFitColumns();
                    #endregion
                }
            }
        }
        #endregion

        #region Events
        #region private void textBox_MaChuanXet_ButtonClick(object sender, ButtonPressedEventArgs e)
        private void textBox_MaChuanXet_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                
                textBox_MaChuanXet.Text = buttonEdit_StudyProgram.EditValue.ToString();
                txtTenChuongTrinh.Text = buttonEdit_StudyProgram.Text;
                _dtTinChi = BL_ChungChi.LayTongTinChi(buttonEdit_StudyProgram.EditValue.ToString());
                txt_STC_TichLuy.Text = _dtTinChi.Rows[0]["TongTCBB"].ToString();
                txt_STC_TuChon.Text = _dtTinChi.Rows[0]["TongTCTC"].ToString();
                textEdit_TinChiTCTD.Text = ((DataRow)dtData.Select("StudyProgramID = '" + buttonEdit_StudyProgram.EditValue.ToString() + "'").GetValue(0))["FreeSelectionCredits"].ToString(); ;
            }
            catch { }
        }
        #endregion

        #region void btnSave_Click(object sender, EventArgs e)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                treeList_CTDT.Enabled = true;
                xtraTabControl.Enabled = true;
                btnClearNew.Enabled = false;
                btnSave.Enabled = false;
                btnDel.Enabled = false;
                buttonEdit_StudyProgram.ReadOnly = true;
                //textBox_MaChuanXet.ReadOnly = true;
                //txt_STC_TichLuy.ReadOnly = true;
                //txt_STC_TuChon.ReadOnly = true;
                //textEdit_TinChiTCTD.ReadOnly = true;

                ButtonStatus(true);

                label_TrangThai.Text = "CHỈNH SỬA CHUẨN XÉT";
            }
            catch { }
        }
        #endregion

        #region private void lookUpEdit_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            try 
            {
                GetDataTreeList(lookUpEdit_KhoaHoc.EditValue.ToString());
            }
            catch { }
        }
        #endregion

        #region void btnClearNew_Click(object sender, EventArgs e)
        private void btnClearNew_Click(object sender, EventArgs e)
        {
            ClearText();
        }
        #endregion

        #region private void treeList_CTDT_DoubleClick(object sender, EventArgs e)
        private void treeList_CTDT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                object keyValue = treeList_CTDT.FocusedNode[treeList_CTDT.KeyFieldName];
                DoubleTreeList_Click(keyValue);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region  void lookUpEdit_BacDaoTao_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_BacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetCourses(lookUpEdit_BacDaoTao.EditValue.ToString(), lookUpEdit_LHDT.EditValue.ToString());
            }
            catch { }
        }
        #endregion

        #region void lookUpEdit_LHDT_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_LHDT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetCourses(lookUpEdit_BacDaoTao.EditValue.ToString(), lookUpEdit_LHDT.EditValue.ToString());
            }
            catch { }
        }
        #endregion

        #region void btnDel_Click(object sender, EventArgs e)
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabControl.SelectedTabPageIndex == 0)
                    DelData();
                //if (xtraTabControl.SelectedTabPageIndex == 1)
                //    DelCTDT();
                //if (xtraTabControl.SelectedTabPageIndex == 2)
                //    DelPhanNhomTC();
                //if (xtraTabControl.SelectedTabPageIndex == 3)
                //    DelHocVien();
            }
            catch { }
        }
        #endregion

        #region void btnClose_Click(object sender, EventArgs e)
        private void btnClose_Click(object sender, EventArgs e)
        {
            ESC();
        }
        #endregion

        #region void btnRest_Click(object sender, EventArgs e)
        private void btnRest_Click(object sender, EventArgs e)
        {
            try
            {
                frmPRJChuanXet_Load(null, null);
            }
            catch { }
        }
        #endregion

        #region void gridView_CTDT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        private void gridView_CTDT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "CurriculumID")
                {
                    string cellValue = gridView_CTDT.GetRowCellValue(e.RowHandle, gridView_CTDT.Columns["CurriculumID"]).ToString();

                    if (_dtChuanMonHoc.Select("CurriculumID='" + cellValue + "'").Length > 0)
                    {
                        XtraMessageBox.Show("Đã tồn tại mã môn học", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridView_CTDT.DeleteRow(e.RowHandle);
                    }
                    else
                    {
                        DataRow drCurriculum = (DataRow)_dtMonChuaDuaVaoChuan.Select("CurriculumID='" + cellValue + "'").GetValue(0);
                        gridView_CTDT.GetFocusedDataRow()["Chon"] = drCurriculum["Chon"];
                        gridView_CTDT.GetFocusedDataRow()["CurriculumID"] = drCurriculum["CurriculumID"];
                        gridView_CTDT.GetFocusedDataRow()["CurriculumName"] = drCurriculum["CurriculumName"];
                        gridView_CTDT.GetFocusedDataRow()["Credits"] = drCurriculum["Credits"];
                        gridView_CTDT.GetFocusedDataRow()["CurriculumType"] = drCurriculum["CurriculumType"];
                        gridView_CTDT.GetFocusedDataRow()["CurriculumGroupName"] = drCurriculum["CurriculumGroupName"];
                        gridView_CTDT.GetFocusedDataRow()["StudyPartID"] = drCurriculum["StudyPartID"];
                        gridView_CTDT.GetFocusedDataRow()["DiemDat"] = drCurriculum["DiemDat"];
                        gridView_CTDT.GetFocusedDataRow()["BatBuocXet"] = drCurriculum["BatBuocXet"];
                    }
                }
            }
            catch (Exception ex){ }
        }   
        #endregion

        #region void repositoryItemButtonEditDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
        private void repositoryItemButtonEditDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                _CurriculumDel += gridView_CTDT.GetFocusedDataRow()["CurriculumID"].ToString() + ";";
                _dtChuanMonHoc.Rows.Remove(gridView_CTDT.GetFocusedDataRow());
            }
            catch { }
        }
        #endregion

        #region void txt_Tong_STC_KeyPress(object sender, KeyPressEventArgs e)
        private void txt_Tong_STC_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)) == false)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }
        #endregion

        #region txt_STC_TuChon_KeyPress(object sender, KeyPressEventArgs e)
        private void txt_STC_TuChon_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)) == false)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }
        #endregion

        #region void txt_TGDT_KeyPress(object sender, KeyPressEventArgs e)
        private void txt_TGDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)) == false)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }
        #endregion

        #region void txt_STC_TichLuy_KeyPress(object sender, KeyPressEventArgs e)
        private void txt_STC_TichLuy_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)) == false)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }

        #endregion

        #region void txtDiemTL_KeyPress(object sender, KeyPressEventArgs e)
        private void txtDiemTL_KeyPress(object sender, KeyPressEventArgs e)
       {
            try
            {
                if (((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)) == false)
                {
                    e.Handled = true;
                }
            }
            catch { }
        }
        #endregion
        
        #region void btnUpdate_Click(object sender, EventArgs e)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_MaChuanXet.Text != "")
                {
                    ProjectUI.LuanVan.frmPRJThongTinNhomTuChonDiaLog frm = new ProjectUI.LuanVan.frmPRJThongTinNhomTuChonDiaLog();
                    frm._maChuan = textBox_MaChuanXet.Text.Trim();
                    frm._MaNhom = _GroupID;
                    frm._NhomCha = _ParentGroupID;
                    frm._TenNhom = treeListNhomTC.FocusedNode["Tên nhóm"].ToString();
                    try
                    {
                        frm._SoTC = decimal.Parse(treeListNhomTC.FocusedNode["Số TC"].ToString());
                    }
                    catch
                    {
                    }
                    frm._Sua = true;
                    frm.ShowDialog();
                    GetDataTreeNhomTC(_ChuanID);
                }
                else
                    XtraMessageBox.Show("Chưa chọn mã chuẩn xét để sửa nhóm", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion
       
        #region void btnInsertTree_Click(object sender, EventArgs e)
        private void btnInsertTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_MaChuanXet.Text != "")
                {
                    ProjectUI.LuanVan.frmPRJThongTinNhomTuChonDiaLog frm = new ProjectUI.LuanVan.frmPRJThongTinNhomTuChonDiaLog();
                    frm._maChuan = textBox_MaChuanXet.Text.Trim();
                    frm.ShowDialog();

                    GetDataTreeNhomTC(_ChuanID);
                }
                else
                    XtraMessageBox.Show("Chưa chọn mã chuẩn xét để thêm nhóm", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
        #endregion

        #region void btnDelTC_Click(object sender, EventArgs e)
        private void btnDelTC_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("Tất cả các môn trong nhóm tự chọn sẽ bị xóa \n Bạn có muốn tiếp tục xóa", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    BL_ChungChi.XoaNhomTuChon( _ParentGroupID, _GroupID);

                    GetDataTreeNhomTC(_ChuanID);
                }
                catch(Exception ex) { }
            }
        }

        private void treeListNhomTC_Click(object sender, EventArgs e)
        {
            try
            {
                _GroupID = treeListNhomTC.FocusedNode["SelectionID"].ToString();
                _ParentGroupID = treeListNhomTC.FocusedNode["SelectionParentID"].ToString();
            }
            catch (Exception ex) { }
        }

        private void gridView_NotPhanNhom_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            StyleFormatCondition DaCoTuChon = new DevExpress.XtraGrid.StyleFormatCondition();
            DaCoTuChon.Appearance.BackColor = Color.PaleTurquoise;
            DaCoTuChon.Appearance.Options.UseBackColor = true;
            DaCoTuChon.Condition = FormatConditionEnum.Expression;

            DaCoTuChon.Expression = "[DuaVaoTuChon] = 1";
            gridView_NotPhanNhom.FormatConditions.Add(DaCoTuChon);

        }

        private void lookUpEdit_DKXet_EditValueChanged(object sender, EventArgs e)
        {
            GetCurriculums(textBox_MaChuanXet.Text, lookUpEdit_DKXet.EditValue.ToString());
        }

        #region Lưu thông tin chung chương trình đào tạo
        private void simpleButton_Luu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #region Hoàn tất thao tac
        private void simpleButton_HoanTat_Click(object sender, EventArgs e)
        {
            textBox_MaChuanXet.ResetText();
            txtTenChuongTrinh.ResetText();
            treeList_CTDT.Enabled = true;
            _ChuanID = "";
            _StudyProgramID = "";
            txt_STC_TichLuy.Text = "0";
            txt_STC_TuChon.Text = "0";
            txtState.ResetText();
            txtTitle.ResetText();
            txtTitle.ResetText();
            buttonEdit_StudyProgram.Text = "";
            lookUpEdit_NganhDaoTao.ItemIndex = -1;
            xtraTabControl.Enabled = true;

            ButtonStatus(false);

            btnClearNew.Enabled = true;
            //btnSave.Enabled = true;
            //btnDel.Enabled = true;

            label_TrangThai.Text = "THÊM MỚI CHUẨN XÉT";
        }
        #endregion

        #region Lấy điều kiện xét theo loại xét
        private void lookUpEdit_LoaiXet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetDataTreeList(lookUpEdit_KhoaHoc.EditValue.ToString());
                LayDieuKienXet(lookUpEdit_LoaiXet.EditValue.ToString(), "#");
            }
            catch { }
        }
        #endregion

        #region Lấy chương trình đào tạo
        private void buttonEdit_StudyProgram_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                DataTable _tblSearch = BL_ChungChi.GetStudyPrograms_Search(lookUpEdit_KhoaHoc.EditValue.ToString(), "", lookUpEdit_LoaiXet.EditValue.ToString());
                if (_tblSearch.Rows.Count > 0)
                {
                    frm_Grd_ChuongTrinhDaoTao_TheoKhoa frm = new frm_Grd_ChuongTrinhDaoTao_TheoKhoa();
                    frm._dtData = _tblSearch.Copy();
                    frm.ShowDialog();
                    if (frm._isSubmit == true)
                    {
                        buttonEdit_StudyProgram.Text = frm._maCTDT + "-" + frm._maCTDT;
                        _StudyProgramID = frm._maCTDT;
                        lookUpEdit_NganhDaoTao.EditValue = frm._Nganh;
                        textBox_MaChuanXet.Text = frm._maCTDT + "_" + lookUpEdit_LoaiXet.EditValue.ToString();
                        _ChuanID = frm._maCTDT + "_" + lookUpEdit_LoaiXet.EditValue.ToString();
                        txtTenChuongTrinh.Text = frm._maCTDT;
                        lookUpEdit_QuyCheDaoTao.EditValue = frm._QCDaoTao;

                        txt_STC_TichLuy.Text = frm._STCBB.ToString();
                        txt_STC_TuChon.Text = frm._STCTC.ToString();

                        LayDieuKienXet(lookUpEdit_LoaiXet.EditValue.ToString(), frm._QCDaoTao);
                    }
                }
            }
            catch { }
        }

        private void btnKhoaChuan_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 1;
                //object keyValue = treeList_CTDT.FocusedNode[treeList_CTDT.KeyFieldName];
                object keyValue = textBox_MaChuanXet.Text;
                if (buttonEdit_StudyProgram.EditValue.ToString() != "" && textBox_MaChuanXet.Text != "" && txtTenChuongTrinh.Text != "")
                {
                    if (_State.ToString().ToLower().Trim() != "true")
                    {
                        result = BL_ChungChi.KhoaChuanXet(textBox_MaChuanXet.Text, true);
                        lookUpEdit_KhoaHoc_EditValueChanged(null, null);
                        DoubleTreeList_Click(keyValue);

                    }
                    else
                        XtraMessageBox.Show("Chuẩn đang ở tình trạng khóa", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    XtraMessageBox.Show("Vui lòng chọn chuẩn trước", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (result==0)
                    XtraMessageBox.Show("Khóa chuẩn thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Khóa chuẩn không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_MoChuan_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 1;
                //object keyValue = treeList_CTDT.FocusedNode[treeList_CTDT.KeyFieldName];
                object keyValue = textBox_MaChuanXet.Text;
                if (buttonEdit_StudyProgram.EditValue.ToString() != "" && textBox_MaChuanXet.Text != "" && txtTenChuongTrinh.Text != "")
                {
                    if (_State.ToString().ToLower().Trim() == "true")
                    {
                        result = BL_ChungChi.KhoaChuanXet(textBox_MaChuanXet.Text, false);
                        lookUpEdit_KhoaHoc_EditValueChanged(null, null);
                        DoubleTreeList_Click(keyValue);

                    }
                    else
                        XtraMessageBox.Show("Chuẩn đang mở", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    XtraMessageBox.Show("Vui lòng chọn chuẩn trước", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (result == 0)
                    XtraMessageBox.Show("Mở khóa chuẩn thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    XtraMessageBox.Show("Chuẩn đang ở tình trạng mở", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex) { }
        }

        private void buttonEdit_StudyProgram_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    DataTable _tblSearch = BL_ChungChi.GetStudyPrograms_Search(lookUpEdit_KhoaHoc.EditValue.ToString(), buttonEdit_StudyProgram.EditValue.ToString(), lookUpEdit_LoaiXet.EditValue.ToString());
                    if (_tblSearch.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_tblSearch.Rows.Count == 1)
                    {
                        buttonEdit_StudyProgram.Text = _tblSearch.Rows[0]["StudyProgramID"].ToString() + "-" + _tblSearch.Rows[0]["StudyProgramID"].ToString();
                        _StudyProgramID = _tblSearch.Rows[0]["StudyProgramID"].ToString();
                        lookUpEdit_NganhDaoTao.EditValue = _tblSearch.Rows[0]["OlogyID"].ToString();
                        textBox_MaChuanXet.Text = _tblSearch.Rows[0]["StudyProgramID"].ToString() + "_" + lookUpEdit_LoaiXet.EditValue.ToString();
                        txtTenChuongTrinh.Text = _tblSearch.Rows[0]["StudyProgramName"].ToString();
                        lookUpEdit_QuyCheDaoTao.EditValue = _tblSearch.Rows[0]["RegulationID"].ToString();
                        lookUpEdit_DKXet.EditValue = _tblSearch.Rows[0]["RegulationID"].ToString();

                        txt_STC_TichLuy.Text = _tblSearch.Rows[0]["STCBB"].ToString()==string.Empty?"0": _tblSearch.Rows[0]["STCBB"].ToString();
                        txt_STC_TuChon.Text = _tblSearch.Rows[0]["STCTC"].ToString()==string.Empty?"0": _tblSearch.Rows[0]["STCTC"].ToString();

                        LayDieuKienXet(lookUpEdit_LoaiXet.EditValue.ToString(), _tblSearch.Rows[0]["RegulationID"].ToString());
                    }
                    else
                    {
                        frm_Grd_ChuongTrinhDaoTao_TheoKhoa frm = new frm_Grd_ChuongTrinhDaoTao_TheoKhoa();
                        frm._dtData = _tblSearch.Copy();
                        frm.ShowDialog();
                        if (frm._isSubmit == true)
                        {
                            buttonEdit_StudyProgram.Text = frm._maCTDT + "-" + frm._maCTDT;
                            _StudyProgramID = frm._maCTDT;
                            lookUpEdit_NganhDaoTao.EditValue = frm._Nganh;
                            textBox_MaChuanXet.Text = frm._maCTDT + "_" + lookUpEdit_LoaiXet.EditValue.ToString();
                            txtTenChuongTrinh.Text = frm._maCTDT;
                            lookUpEdit_QuyCheDaoTao.EditValue = frm._QCDaoTao;

                            txt_STC_TichLuy.Text = frm._STCBB.ToString();
                            txt_STC_TuChon.Text = frm._STCTC.ToString();

                            LayDieuKienXet(lookUpEdit_LoaiXet.EditValue.ToString(), _tblSearch.Rows[0]["RegulationID"].ToString());
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region  private void simpleButton_CopyChuan_Click(object sender, EventArgs e)
        private void simpleButton_CopyChuan_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_ChonChuanXeCopy frm = new frm_Grd_ChonChuanXeCopy();
                frm._DieuKien = lookUpEdit_DKXet.EditValue.ToString();
                frm._BacDT = lookUpEdit_BacDaoTao.EditValue.ToString();
                frm._HeDT = lookUpEdit_LHDT.EditValue.ToString();
                frm.ShowDialog();

                string _ChuanCopy = frm._Chuan;
                if (_ChuanCopy == string.Empty)
                {
                    return;
                }

                if (_ChuanCopy != string.Empty && _ChuanID != string.Empty)
                {
                    BL_ChungChi.SaveCopyMon_Chuan(_ChuanID, _ChuanCopy, User._User.StaffID);

                    treeList_CTDT_DoubleClick(sender, e);
                    if (gridView_CTDT.RowCount > 0)
                        XtraMessageBox.Show("Sao chép môn học thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        XtraMessageBox.Show("Không có dữ liệu sao chép", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Sao chép môn học không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region Check chọn môn học
        private void checkBox_TatCa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_dtChuanMonHoc.Columns.Count == 0)
                    return;

                string _studentID = string.Empty, _studyProgramID = string.Empty;

                for (int i = 0; i < gridView_CTDT.DataRowCount; i++)
                {
                    gridView_CTDT.GetDataRow(i)["Chon"] = checkBox_TatCa.Checked;
                }
            }

            catch { }
        }
        #endregion

        #region Xóa môn học
        private void simpleButton_XoaMH_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dtChuanMonHoc_Copy = _dtChuanMonHoc.Copy();
                for (int i = 0; i < _dtChuanMonHoc_Copy.Rows.Count; i++)
                {
                    if (_dtChuanMonHoc_Copy.Rows[i]["Chon"].ToString().ToUpper() == "TRUE")
                    {
                        DataRow row = (DataRow)_dtChuanMonHoc.Select("CurriculumID = '" + _dtChuanMonHoc_Copy.Rows[i]["CurriculumID"].ToString() + "'").GetValue(0);
                        _dtChuanMonHoc.Rows.Remove(row);
                    }
                }

                SaveDataCTDT();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region void btnDownAll_Click(object sender, EventArgs e)
        private void btnDownAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in _dtNotPhanNhom.Rows)
                {
                    _dtPhanNhom.Rows.Add(dr["Chon"].ToString(), dr["CurriculumID"].ToString(), dr["CurriculumName"].ToString(), dr["Credits"].ToString());
                    dr.Delete();
                }
                _dtPhanNhom.AcceptChanges();
                _dtNotPhanNhom.AcceptChanges();
                gridControl_PhanNhom.DataSource = _dtPhanNhom;
                gridControl_PhanNhom.RefreshDataSource();
                gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;
                gridControl_NotPhanNhom.RefreshDataSource();

                SaveDataNhomTC();
            }
            catch { }
        }
        #endregion

        #region void btnDown_Click(object sender, EventArgs e)
        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView_NotPhanNhom.DataRowCount > 0)
                {
                    foreach (DataRow dr in _dtNotPhanNhom.Rows)
                    {
                        if (dr["Chon"].ToString().ToLower() == "true")
                        {
                            dr["Chon"] = false;
                            _dtPhanNhom.Rows.Add(dr["Chon"].ToString(), dr["CurriculumID"].ToString(), dr["CurriculumName"].ToString(), dr["Credits"].ToString());
                            dr.Delete();
                        }
                    }
                    _dtNotPhanNhom.AcceptChanges();
                    _dtPhanNhom.AcceptChanges();
                    gridControl_PhanNhom.DataSource = _dtPhanNhom;
                    gridView_PhanNhom.RefreshData();
                    gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;
                    gridView_NotPhanNhom.RefreshData();
                }

                SaveDataNhomTC();
            }
            catch { }
        }
        #endregion

        #region void btnUp_Click(object sender, EventArgs e)
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtPhanNhom.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtPhanNhom.Rows)
                    {
                        if (dr["Chon"].ToString().ToLower() == "true")
                        {
                            dr["Chon"] = false;
                            _dtNotPhanNhom.Rows.Add(dr["Chon"].ToString(), dr["CurriculumID"].ToString(), dr["CurriculumName"].ToString(), dr["Credits"].ToString(),"0");
                            dr.Delete();
                        }
                    }
                    _dtPhanNhom.AcceptChanges();
                    _dtNotPhanNhom.AcceptChanges();
                    gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;
                    gridView_NotPhanNhom.RefreshData();

                    gridControl_PhanNhom.DataSource = _dtPhanNhom;
                    gridView_PhanNhom.RefreshData();
                }

                SaveDataNhomTC();
            }
            catch { }
        }
        #endregion

        #region void btnUpAll_Click(object sender, EventArgs e)
        private void btnUpAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in _dtPhanNhom.Rows)
                {
                    _dtNotPhanNhom.Rows.Add(dr["Chon"].ToString(), dr["CurriculumID"].ToString(), dr["CurriculumName"].ToString(), dr["Credits"].ToString(), "0");
                    dr.Delete();
                }
                _dtPhanNhom.AcceptChanges();
                _dtNotPhanNhom.AcceptChanges();
                gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;
                gridControl_NotPhanNhom.RefreshDataSource();
                gridControl_PhanNhom.DataSource = _dtPhanNhom;
                gridControl_PhanNhom.RefreshDataSource();

                SaveDataNhomTC();
            }
            catch { }
        }
        #endregion

        #region void treeListNhomTC_DoubleClick(object sender, EventArgs e)
        private void treeListNhomTC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _dtPhanNhom.Clear();
                _dtNotPhanNhom.Clear();
                _GroupID = treeListNhomTC.FocusedNode["SelectionID"].ToString();
                _ParentGroupID = treeListNhomTC.FocusedNode["SelectionParentID"].ToString();
                object valueParent = treeListNhomTC.FocusedNode[treeListNhomTC.ParentFieldName];
                int _count = 0;
                DataTable _dtTree = (DataTable)treeListNhomTC.DataSource;
                _count = _dtTree.Select("SelectionParentID ='" + valueParent + "'").Count();

                if (textBox_MaChuanXet.Text != "")
                {
                    if (_count == 1)
                    {
                        _GroupID = treeListNhomTC.FocusedNode["SelectionID"].ToString();
                        _ParentGroupID = treeListNhomTC.FocusedNode[treeListNhomTC.ParentFieldName].ToString();
                            txtTitle.Text = "Mã chuẩn xét: " + _ChuanID + " - Tên ngành: " + lookUpEdit_NganhDaoTao.Text.ToString() + " - Mã nhóm: " + _GroupID;
                    }

                    if (treeListNhomTC.FocusedNode["SelectionID"] != treeListNhomTC.FocusedNode[treeListNhomTC.ParentFieldName])
                    {
                        _GroupID = treeListNhomTC.FocusedNode["SelectionID"].ToString();
                        _ParentGroupID = treeListNhomTC.FocusedNode[treeListNhomTC.ParentFieldName].ToString();
                         txtTitle.Text = "Mã chuẩn xét: " + _ChuanID + " - Tên ngành: " + lookUpEdit_NganhDaoTao.Text.ToString() + " - Mã nhóm: " + _GroupID;
                    }
                    DataSet dsPhanNhom = new DataSet();

                    if(_GroupID != _ParentGroupID)
                    dsPhanNhom = BL_ChungChi.NhomMonHocTuChon(_ChuanID,_ParentGroupID, _GroupID, lookUpEdit_DKXet.EditValue.ToString());

                    _dtNotPhanNhom = dsPhanNhom.Tables["ChuaPhanNhom"].Copy();
                    _dtPhanNhom = dsPhanNhom.Tables["PhanNhom"].Copy();
                    _dtPhanNhom.Columns["Chon"].ReadOnly = false;
                    _dtNotPhanNhom.Columns["Chon"].ReadOnly = false;

                    gridControl_NotPhanNhom.DataSource = _dtNotPhanNhom;
                    gridControl_PhanNhom.DataSource = _dtPhanNhom;
                    gridView_NotPhanNhom.RefreshData();
                    gridView_PhanNhom.RefreshData();
            }
            }catch{}
        }
        #endregion

        #region void btnCopyCurriculum_Click(object sender, EventArgs e)
        private void btnCopyCurriculum_Click(object sender, EventArgs e)
        {
            try
            {
               // _StudyProgramID = textBox_MaChuanXet.Text;
                if (_StudyProgramID != string.Empty && _ChuanID != string.Empty)
                {
                    BL_ChungChi.SaveCopyMon(_ChuanID, _StudyProgramID, User._User.StaffID);

                    //treeList_CTDT_DoubleClick(sender, e);
                    GetCurriculums(_ChuanID, lookUpEdit_DKXet.EditValue.ToString());
                    if (gridView_CTDT.RowCount>0)
                        XtraMessageBox.Show("Sao chép môn học thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        XtraMessageBox.Show("Không có dữ liệu sao chép", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Sao chép môn học không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex) { }
        }
        #endregion

        #region void btnAllNext_Click(object sender, EventArgs e)
        private void btnAllNext_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in _dtHocVienShow.Rows)
            {
                _dtHocVienSave.Rows.Add(dr["Chon"].ToString(), dr["StudentID"].ToString(), dr["FullName"].ToString(), dr["BirthDay"].ToString(), dr["BirthPlace"].ToString());
                dr.Delete();
            }
            _dtHocVienSave.AcceptChanges();
            _dtHocVienShow.AcceptChanges();
            gridControl_SaveHV.DataSource = _dtHocVienSave;
            gridControl_SaveHV.RefreshDataSource();
            gridControl_showHV.DataSource = _dtHocVienShow;
            gridControl_showHV.RefreshDataSource();

            SaveHocVien();
        }
        #endregion

        #region void btnAllPrevious_Click(object sender, EventArgs e)
        private void btnAllPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                _strXmlHocVien = "<Root>";
                foreach (DataRow dr in _dtHocVienSave.Rows)
                {
                    _dtHocVienShow.Rows.Add(dr["Chon"].ToString(), dr["StudentID"].ToString(), dr["FullName"].ToString(), dr["BirthDay"].ToString(), dr["BirthPlace"].ToString());
                    _strXmlHocVien += "<Datas StudentID = \"" + dr["StudentID"].ToString() +
                        "\" StudyProgramID = \"" + _StudyProgramID +
                        "\"/>";
                    dr.Delete();
                }
                _strXmlHocVien += "</Root>";
                _dtHocVienSave.AcceptChanges();
                _dtHocVienShow.AcceptChanges();
                gridControl_showHV.DataSource = _dtHocVienShow;
                gridControl_showHV.RefreshDataSource();
                gridControl_SaveHV.DataSource = _dtHocVienSave;
                gridControl_SaveHV.RefreshDataSource();

                SaveHocVien();
            }
            catch { }
        }
        #endregion

        #region void btnNext_Click(object sender, EventArgs e)
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView_showHV.DataRowCount > 0)
                {
                    foreach (DataRow dr in _dtHocVienShow.Rows)
                    {
                        if (dr["Chon"].ToString().ToLower() == "true")
                        {
                            dr["Chon"] = false;
                            _dtHocVienSave.Rows.Add(dr["Chon"].ToString(), dr["StudentID"].ToString(), dr["FullName"].ToString(), dr["BirthDay"].ToString(), dr["BirthPlace"].ToString());
                            dr.Delete();
                        }
                    }
                    _dtHocVienShow.AcceptChanges();
                    _dtHocVienSave.AcceptChanges();
                    gridControl_SaveHV.DataSource = _dtHocVienSave;
                    gridControl_showHV.RefreshDataSource();
                    gridControl_showHV.DataSource = _dtHocVienShow;
                    gridControl_showHV.RefreshDataSource();
                }

                SaveHocVien();
            }
            catch { }
        }
        #endregion

        #region void btnPrevious_Click(object sender, EventArgs e)
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView_SaveHV.DataRowCount >0)
                {
                    _strXmlHocVien = "<Root>";
                    if (_dtHocVienSave.Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dtHocVienSave.Rows)
                        {
                            if (dr["Chon"].ToString().ToLower() == "true")
                            {
                                dr["Chon"] = false;
                                _dtHocVienShow.Rows.Add(dr["Chon"].ToString(), dr["StudentID"].ToString(), dr["FullName"].ToString(), dr["BirthDay"].ToString(), dr["BirthPlace"].ToString());
                                _strXmlHocVien += "<Datas StudentID = \"" + dr["StudentID"].ToString() +
                               "\" StudyProgramID = \"" + _StudyProgramID +
                               "\"/>";
                                dr.Delete();
                            }
                        }
                    }
                    else
                    {
                        _dtHocVienSave = _dtHocVienShow.Copy();
                        _dtHocVienShow.Clear();
                    }
                    _strXmlHocVien += "</Root>";
                    _dtHocVienSave.AcceptChanges();
                    _dtHocVienShow.AcceptChanges();
                    gridControl_showHV.DataSource = _dtHocVienShow;
                    gridControl_showHV.RefreshDataSource();

                    gridControl_SaveHV.DataSource = _dtHocVienSave;
                    gridControl_SaveHV.RefreshDataSource();
                }

                SaveHocVien();
            }
            catch { }
        }
        #endregion
          
        #endregion

        #region ShortKey
        #region protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                ESC();
                return true;
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                ClearText();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                if (xtraTabControl.SelectedTabPageIndex == 0)
                {
                    SaveData();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 1)
                {
                    SaveDataCTDT();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 2)
                {
                    SaveDataNhomTC();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 3)
                {
                    SaveHocVien();
                    return true;
                }
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                if (xtraTabControl.SelectedTabPageIndex == 0)
                {
                    DelData();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 1)
                {
                    DelCTDT();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 2)
                {
                    DelPhanNhomTC();
                    return true;
                }
                if (xtraTabControl.SelectedTabPageIndex == 3)
                {
                    DelHocVien();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        #endregion
        #endregion
    }
}