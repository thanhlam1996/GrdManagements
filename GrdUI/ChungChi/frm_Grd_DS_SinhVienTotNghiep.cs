using DevExpress.Common.Grid;
using DevExpress.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using GrdCore.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GrdUI.ChungChi
{
    public partial class frm_Grd_DS_SinhVienTotNghiep : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtLoaiDieuKien = new DataTable()
            , _dtDKXet = new DataTable(), _dtCTDT=new DataTable(), _dtGridColumns = new DataTable();
        DataRow _drGrids;  
        #endregion

        #region Init
        public frm_Grd_DS_SinhVienTotNghiep()
        {
            InitializeComponent();
        }

        private void frm_Grd_DS_SinhVienTotNghiep_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'DSSinhVien_TN'").GetValue(0);

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
            GetStudyTypes();
            GetCourses(lookUpEdit_BacDaoTao.EditValue.ToString(), lookUpEdit_LHDT.EditValue.ToString());
            DotXet();
            //GetStudyPrograms(lookUpEdit_DotXet.EditValue.ToString(), chkCCB_KhoaHoc.EditValue.ToString());
        }
        #endregion

        #region Functions

        #region private void GetGraduateLevels()
        private void GetGraduateLevels()
        {
            try
            {
                //DataTable dtData = new DataTable();
                //dtData = User._dsDataDictionaries.Tables["GraduateLevels"];
                //DataView myDataView = new DataView(dtData);
                //lookUpEdit_BacDaoTao.Properties.DataSource = myDataView.ToTable();
                //lookUpEdit_BacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                //lookUpEdit_BacDaoTao.Properties.ValueMember = "GraduateLevelID";
                //if (dtData.Rows.Count > 0)
                //    lookUpEdit_BacDaoTao.EditValue = myDataView[0].Row[1].ToString();
                //lookUpEdit_BacDaoTao.Properties.Columns.Clear();
                //lookUpEdit_BacDaoTao.Properties.Columns.Add(new LookUpColumnInfo("GraduateLevelID", 100, "Mã bậc DT"));
                //lookUpEdit_BacDaoTao.Properties.Columns.Add(new LookUpColumnInfo("GraduateLevelName", 100, "Bậc đào tạo"));
                //lookUpEdit_BacDaoTao.Properties.AutoSearchColumnIndex = 0;

                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();
                lookUpEdit_BacDaoTao.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtGraduateLevels.Copy());
                myDataView.Sort = "GraduateLevelName";

                lookUpEdit_BacDaoTao.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_BacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                lookUpEdit_BacDaoTao.Properties.ValueMember = "GraduateLevelID";

                LookUpColumnInfoCollection coll = lookUpEdit_BacDaoTao.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GraduateLevelName", 0, "Bậc đào tạo"));

                lookUpEdit_BacDaoTao.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_BacDaoTao.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_BacDaoTao.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_BacDaoTao.ItemIndex = 0;
                lookUpEdit_BacDaoTao.Properties.NullText = "";
                lookUpEdit_BacDaoTao.EditValue = User._CurrentGraduateLevelID;
            }
            catch(Exception ex) { }
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
                if (dtData.Rows.Count > 0)
                    lookUpEdit_LHDT.EditValue = myDataView[0].Row[1].ToString();
                lookUpEdit_LHDT.Properties.Columns.Clear();
                lookUpEdit_LHDT.Properties.Columns.Add(new LookUpColumnInfo("StudyTypeID", 0, "Mã LHDT"));
                lookUpEdit_LHDT.Properties.Columns.Add(new LookUpColumnInfo("StudyTypeName", 0, "Loại hình đào tạo"));
                lookUpEdit_LHDT.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }
        #endregion

        #region private void GetCourses()
        private void GetCourses(string graduateLevelID, string studyTypeID)
        {
            //try
            //{
            //    DataTable dtData = new DataTable();
            //    dtData = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(DotXet);
            //    DataView myDataView = new DataView(dtData);
            //    myDataView.Sort = "CourseName DESC";
            //    lookUpEdit_KhoaHoc.Properties.DataSource = myDataView.ToTable();
            //    lookUpEdit_KhoaHoc.Properties.DisplayMember = "CourseName";
            //    lookUpEdit_KhoaHoc.Properties.ValueMember = "CourseID";
            //    if (dtData.Rows.Count > 0)
            //    {
            //        lookUpEdit_KhoaHoc.EditValue = myDataView[0].Row[0].ToString();
            //        LookUpColumnInfoCollection coll = lookUpEdit_KhoaHoc.Properties.Columns;
            //        coll.Clear();
            //        coll.Add(new LookUpColumnInfo("CourseID", 0, "Mã khóa học"));
            //        coll.Add(new LookUpColumnInfo("CourseName", 0, "Khóa học"));
            //        lookUpEdit_KhoaHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            //        lookUpEdit_KhoaHoc.Properties.SearchMode = SearchMode.AutoComplete;
            //        lookUpEdit_KhoaHoc.Properties.AutoSearchColumnIndex = 0;
            //    }
            //   else
            //    {
            //        lookUpEdit_KhoaHoc.EditValue = DBNull.Value;
            //    }
            //}
            //catch (Exception ex) { }

            try
            {
                if (lookUpEdit_BacDaoTao.EditValue.ToString() == string.Empty || lookUpEdit_LHDT.EditValue.ToString() == string.Empty)
                    return;

                DataTable dtCourses = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(graduateLevelID, studyTypeID);
                chkCCB_KhoaHoc.Properties.Items.Clear();
                foreach (DataRow dr in dtCourses.Rows)
                    chkCCB_KhoaHoc.Properties.Items.Add(dr["CourseID"].ToString(), dr["CourseID"].ToString() + " -- " + dr["CourseName"].ToString(), CheckState.Unchecked, true);

                chkCCB_KhoaHoc.Properties.SeparatorChar = ';';
                chkCCB_KhoaHoc.CheckAll();
            }
            catch { }
        }


        #endregion
          
        #region Đợt xét
        private void DotXet()
        {
            try
            {
                _dtDKXet = BL_ChungChi.DotXetTheoBacHe(lookUpEdit_BacDaoTao.EditValue.ToString()
                    , lookUpEdit_LHDT.EditValue.ToString(), "#", "#", "#");
                lookUpEdit_DotXet.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtDKXet.Copy());
                myDataView.Sort = "MaDot";

                lookUpEdit_DotXet.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_DotXet.Properties.DisplayMember = "TenDot";
                lookUpEdit_DotXet.Properties.ValueMember = "MaDot";

                LookUpColumnInfoCollection coll = lookUpEdit_DotXet.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaDot", 0, "Mã đợt xét"));
                coll.Add(new LookUpColumnInfo("TenDot", 0, "Đợt xét"));

                lookUpEdit_DotXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_DotXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_DotXet.Properties.AutoSearchColumnIndex = 1;
                lookUpEdit_DotXet.ItemIndex = 0;
                lookUpEdit_DotXet.Properties.NullText = "";

            }
            catch { }
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
                    if (dr.RowState == DataRowState.Modified)
                    {
                        strXml += "<SV_TN StudentID = \"" +dr["StudentID"].ToString()
                            + "\" HinhThuc = \"" + dr["HinhThuc"].ToString()
                            + "\"/>";
                    }
                }
                if (strXml != "")
                {
                    strXml = "<Root>" + strXml + "</Root>";

                    int result = BL_ChungChi.LuuSinhVienHinhThuc(lookUpEdit_DotXet.EditValue.ToString(), chkCCB_CTDT.EditValue.ToString(), strXml, User._UserID);

                    if (result == 0)
                    {
                        LoadData();
                        XtraMessageBox.Show("Lưu dữ liệu thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    XtraMessageBox.Show("Cập nhật dữ liệu trước khi lưu...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch { }
        }
        #endregion

        #region private void GetStudyPrograms(string CourseID)
        private void GetStudyPrograms(string MaDotXet, string CourseID)
        {
            try
            {
                _dtCTDT = new DataTable();
                //_dtCTDT = BL_ChungChi.GetStudyPrograms(CourseID);
                //if (_dtCTDT.Rows.Count > 0)
                //{
                //    DataView myDataView = new DataView(_dtCTDT);
                //    myDataView.Sort = "StudyProgramName ASC";
                //    lookUpEdit_StudyProgram.Properties.DataSource = myDataView.ToTable();
                //    lookUpEdit_StudyProgram.Properties.DisplayMember = "StudyProgramName";
                //    lookUpEdit_StudyProgram.Properties.ValueMember = "StudyProgramID";
                //    if (_dtCTDT.Rows.Count > 0)
                //        lookUpEdit_StudyProgram.EditValue = myDataView[0].Row[0].ToString();
                //    lookUpEdit_StudyProgram.Properties.Columns.Clear();
                //    lookUpEdit_StudyProgram.Properties.Columns.Add(new LookUpColumnInfo("StudyProgramID", 0, "Mã CTDT"));
                //    lookUpEdit_StudyProgram.Properties.Columns.Add(new LookUpColumnInfo("StudyProgramName", 0, "Chương trình đào tạo"));
                //    lookUpEdit_StudyProgram.Properties.AutoSearchColumnIndex = 0;
                //}
                //else
                //{
                //    lookUpEdit_StudyProgram.Properties.DataSource = null;
                //}

                if (chkCCB_KhoaHoc.EditValue.ToString() == string.Empty)
                    return;

                _dtCTDT = BL_ChungChi.GetStudyPrograms(MaDotXet, CourseID);
                chkCCB_CTDT.Properties.Items.Clear();
                foreach (DataRow dr in _dtCTDT.Rows)
                    chkCCB_CTDT.Properties.Items.Add(dr["StudyProgramID"].ToString(), dr["StudyProgramID"].ToString() + " -- " + dr["StudyProgramName"].ToString(), CheckState.Unchecked, true);

                chkCCB_CTDT.Properties.SeparatorChar = ';';
                chkCCB_CTDT.CheckAll();
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region LoadData()
        private void LoadData()
        {
            try
            {
                gridControl_DSSinhVien.DataSource = null;
                gridView_DSSinhVien.Columns.Clear();
                _dtData = BL_ChungChi.LayDanhSachSinhVienTotNghiep(chkCCB_CTDT.EditValue.ToString());

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                gridControl_DSSinhVien.DataSource = _dtData;
                AppGridView.InitGridView(gridView_DSSinhVien, _drGrids, _dtGridColumns, User._foreignLanguage);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        

        #endregion

        #region Events
        private void lookUpEdit_LHDT_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
            GetCourses(lookUpEdit_BacDaoTao.EditValue.ToString(), lookUpEdit_LHDT.EditValue.ToString());

        } 

        private void lookUpEdit_BacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
            GetCourses(lookUpEdit_BacDaoTao.EditValue.ToString(), lookUpEdit_LHDT.EditValue.ToString());

        }

        private void lookUpEdit_DotXet_EditValueChanged(object sender, EventArgs e)
        {
            GetStudyPrograms(lookUpEdit_DotXet.EditValue.ToString(), chkCCB_KhoaHoc.EditValue.ToString());
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();
                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS -Danh sách sinh viên tốt nghiệp-Đợt " + lookUpEdit_DotXet.EditValue.ToString();

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    GridControl gridControlExcel = new DevExpress.XtraGrid.GridControl();
                    GridView gridViewExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
                    gridControlExcel.ViewCollection.Add(gridViewExcel);
                    gridControlExcel.MainView = gridViewExcel;
                    DataTable _dtCopy = new DataTable();
                    _dtCopy = _dtData.Copy();
                    AppGridView.InitGridView(gridViewExcel, false, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                    AppGridView.ShowField(gridViewExcel,
                        new string[] { "StudentID", "LastName", "FirstName", "BirthDay", "Gender_VN", "BirthPlace", "ClassStudentID", "StudyProgramID"
                    , "StudyProgramName", "CourseID", "OlogyName", "HinhThuc"},
                        new string[] { "Mã sinh viên", "Họ", "Tên", "Ngày sinh", "Giới tính", "Nơi sinh", "Lớp", " Mã CTĐT", "Tên CTĐT", "Khóa", "Ngành", "Hình thức" },
                        new int[] { 70, 150, 50, 80, 20, 60, 100, 50, 150, 50, 100, 80 });
                    AppGridView.AlignHeader(gridViewExcel, new string[] { "StudentID", "LastName", "FirstName", "BirthDay", "Gender_VN", "BirthPlace", "ClassStudentID", "StudyProgramID"
                    , "StudyProgramName", "CourseID", "OlogyName", "HinhThuc" }, DevExpress.Utils.HorzAlignment.Center);
                    AppGridView.AlignField(gridViewExcel, new string[] { "BirthDay", "Gender_VN" }, DevExpress.Utils.HorzAlignment.Center);

                    gridViewExcel.OptionsView.ColumnAutoWidth = true;
                    gridViewExcel.BestFitColumns();

                    gridControlExcel.DataSource = _dtCopy;
                    Controls.Add(gridControlExcel);
                    gridControlExcel.ForceInitialize();
                    gridViewExcel.OptionsSelection.MultiSelect = false;
                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;
                    gridControlExcel.Visible = true;
                    var options = new XlsxExportOptions();

                    options.SheetName = lookUpEdit_DotXet.EditValue.ToString();

                    gridViewExcel.ExportToXlsx(sfdFiles.FileName, options);

                    gridViewExcel.OptionsSelection.MultiSelect = true;
                    Controls.Remove(gridControlExcel);
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                }
            catch (Exception ex)
            {
                gridView_DSSinhVien.OptionsSelection.MultiSelect = true;
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkCCB_KhoaHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetStudyPrograms(lookUpEdit_DotXet.EditValue.ToString(), chkCCB_KhoaHoc.EditValue.ToString());
        }

        private void simpleButton_Loc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion
    }
}