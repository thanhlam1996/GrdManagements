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
using DevExpress.XtraGrid.Views.Grid;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_DotCap_ChuongTrinh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow drGrids;
        public int NhanDang = 1;

        DataTable _dtCTDT = new DataTable();
        DataTable _dtNgoaiCTDT = new DataTable();
        DataTable _dtSinhVien = new DataTable();

        public string _maLoaiChungChi = string.Empty, _namHoc = string.Empty, _hocKy = string.Empty, _bacDT = string.Empty, _loaiHinhDaoTao = string.Empty, _MaDot = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_DotCap_ChuongTrinh()
        {
            InitializeComponent();
        }

        private void frm_Grd_DotCap_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'DotCap'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
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
            if (NhanDang == 2)
            {
                lkuNamHoc.EditValue = User._CurrentYearStudy;

                lkuHocKy.EditValue = User._CurrentTerm;

                lkuLoaiChungChi.Enabled = false;
                lkuBacDaoTao.Enabled = false;
                lkuLHDT.Enabled = false;
                lkuNamHoc.Enabled = false;
                lkuHocKy.Enabled = false;
                lookUpEdit_DotXet.Enabled = false;
            }

            GetGraduateLevels();
            GetStudyTypes();

        }
        #endregion

        #region Functions
        private void MaLoaiChungChi()
        {
            DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("CC; CB; DK");

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

            if (NhanDang == 2)
            {
                lkuLoaiChungChi.EditValue = _maLoaiChungChi;
            }
            else
            {
                lkuLoaiChungChi.EditValue = "TN";
            }
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

                if (NhanDang == 2)
                {
                    lkuNamHoc.EditValue = _namHoc;
                }
                else
                {
                    lkuNamHoc.EditValue = User._CurrentYearStudy;
                }
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

                LookUpColumnInfoCollection col1 = lkuHocKy.Properties.Columns;
                col1.Clear();
                col1.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lkuHocKy.Properties.DataSource = dv.ToTable();
                lkuHocKy.Properties.DisplayMember = "TermName";
                lkuHocKy.Properties.ValueMember = "TermID";

                lkuHocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuHocKy.Properties.SearchMode = SearchMode.AutoComplete;
                //lkuHocKy.Properties.AutoSearchColumnIndex = 0;
                lkuHocKy.Properties.NullText = string.Empty;

                if (NhanDang == 2)
                {
                    lkuHocKy.EditValue = _hocKy;
                }
                else
                {
                    lkuHocKy.EditValue = User._CurrentTerm;
                }
            }
            catch { }
        }

        private void LayDotXet(string BacDaoTao, string HeDaoTao, string LoaiCC, string NamHoc, string HocKy)
        {
            try
            {
                DataTable _dt = BL_ChungChi.DotXetTheoBacHe(BacDaoTao, HeDaoTao
                    , NamHoc, HocKy, LoaiCC);

                DataView dv = new DataView(_dt);
                dv.Sort = "MaDot";

                lookUpEdit_DotXet.Properties.DataSource = dv.ToTable();
                lookUpEdit_DotXet.Properties.DisplayMember = "TenDot";
                lookUpEdit_DotXet.Properties.ValueMember = "MaDot";

                LookUpColumnInfoCollection coll = lookUpEdit_DotXet.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TenDot", 0, "Đợt xét"));

                lookUpEdit_DotXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_DotXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_DotXet.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_DotXet.Properties.NullText = string.Empty;

                if (NhanDang == 2)
                {
                    lookUpEdit_DotXet.EditValue = _MaDot;
                    GetData();
                }
                else
                {
                    lookUpEdit_DotXet.ItemIndex = 0;
                }
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
                DataSet _ds = BL_ChungChi.DanhSachChuongTrinhDaoTao_DotXet(lookUpEdit_DotXet.EditValue.ToString());

                 _dtCTDT = _ds.Tables["CTDTTrongDot"].Copy();
                _dtSinhVien = _ds.Tables["SinhVien"].Copy();
                _dtNgoaiCTDT = _ds.Tables["CTDTNgoaiDot"].Copy();

                //Chương trình đào tạo trong đợt xét
                gridControl_CTTrong.DataSource = _dtCTDT;
                AppGridView.InitGridView(gridView_CTTrong, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, false);
                AppGridView.ShowField(gridView_CTTrong,
                            new string[] {  "StudyProgramID", "StudyProgramName", "OlogyName", "CourseName", "CourseTime" },
                            new string[] {  "Mã chương trình", "Tên chương trình", "Ngành", "Khóa", "Niên khóa" }
                            , new int[] {  70, 150, 150, 150, 70, 70});
                AppGridView.AlignField(gridView_CTTrong, new string[] { "StudyProgramID", "CourseTime" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.ReadOnlyColumn(gridView_CTTrong, new string[] { "StudyProgramID", "StudyProgramName", "OlogyName", "CourseName", "CourseTime" });

                //gridView_CTTrong.OptionsView.ColumnAutoWidth = true;
                gridView_CTTrong.BestFitColumns();

                //Chương trình đào tạo ngoài đợt xét
                gridControl_CTNgoai.DataSource = _dtNgoaiCTDT;
                AppGridView.InitGridView(gridView_CTNgoai, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, false);
                AppGridView.ShowField(gridView_CTNgoai,
                            new string[] {  "StudyProgramID", "StudyProgramName", "OlogyName", "CourseName", "CourseTime" },
                            new string[] {  "Mã chương trình", "Tên chương trình", "Ngành", "Khóa", "Niên khóa" }
                            , new int[] {  70, 150, 150, 150, 70, 70 });
                AppGridView.AlignField(gridView_CTNgoai, new string[] { "StudyProgramID", "CourseTime" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.ReadOnlyColumn(gridView_CTNgoai, new string[] { "StudyProgramID", "StudyProgramName", "OlogyName", "CourseName", "CourseTime" });

                //gridView_CTNgoai.OptionsView.ColumnAutoWidth = true;
                gridView_CTNgoai.BestFitColumns();

                //Danh sách sinh viên đăng ký online
                gridControl_SinhVien.DataSource = _dtSinhVien;
                AppGridView.InitGridView(gridView_SinhVien, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridView_SinhVien,
                            new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "StudyProgramName", "CourseTime", "OlogyName" },
                            new string[] { "Mã SV", "Họ tên", "Ngày sinh", "Nơi sinh", "CTĐT", "Niên khóa", "Ngành" }
                            , new int[] { 40, 150, 70, 70, 150, 70, 150 });
                AppGridView.AlignField(gridView_SinhVien, new string[] { "StudentID", "BirthDay", "CourseTime" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.ReadOnlyColumn(gridView_SinhVien, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "StudyProgramName", "CourseTime", "OlogyName" });

                //gridView_SinhVien.OptionsView.ColumnAutoWidth = true;
                gridView_SinhVien.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                if (NhanDang == 2)
                {

                    lkuBacDaoTao.EditValue = _bacDT;
                }
                else
                {
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

                if (NhanDang == 2)
                {

                     lkuLHDT.EditValue = _loaiHinhDaoTao;
                }
                else
                {
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
            }
            catch { }
        }
        #endregion

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkuHocKy_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LayDotXet(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString());
            }
            catch { }
        }

        private void simpleButton_Them_Click(object sender, EventArgs e)
        {
            try
            {
                string _strXml = string.Empty;
                foreach (int i in gridView_CTNgoai.GetSelectedRows())
                {
                        _strXml += "<Data StudyProgramID = \"" + gridView_CTNgoai.GetDataRow(i)["StudyProgramID"].ToString() +
                       "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString() +
                       "\" UpdateStaff = \"" + User._User.StaffID.ToString() +
                       "\"/>";
                }

                _strXml = "<Root>" + _strXml + "</Root>";

                int KQ = BL_ChungChi.LuuChuongTrinhVaoDotXet(_strXml);

                GetData();
            }
            catch
            {
                XtraMessageBox.Show("Cập nhật dữ liệu thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                string _strXml = string.Empty;
                foreach (int i in gridView_CTTrong.GetSelectedRows())
                {
                    _strXml += "<Data StudyProgramID = \"" + gridView_CTTrong.GetDataRow(i)["StudyProgramID"].ToString() +
                    "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString() +
                    "\" UpdateStaff = \"" + User._User.StaffID.ToString() +
                    "\"/>";
                }

                _strXml = "<Root>" + _strXml + "</Root>";

                int KQ = BL_ChungChi.XoaChuongTrinhVaoDotXet(_strXml);

                if (KQ == 0)
                {
                    XtraMessageBox.Show("Cập nhật dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Chương trình đào tạo đã có trong kết quả xét", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                GetData();
            }
            catch
            {
                XtraMessageBox.Show("Cập nhật dữ liệu thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkuLoaiChungChi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LayDotXet(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString());
            }
            catch { }
        }

        private void lkuBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LayDotXet(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString());
            }
            catch { }
        }

        private void lkuLHDT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LayDotXet(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString());
            }
            catch { }
        }

        private void lookUpEdit_DotXet_EditValueChanged(object sender, EventArgs e)
        {
            gridControl_CTNgoai.DataSource = null;
            gridControl_CTTrong.DataSource = null;
            gridControl_SinhVien.DataSource = null;
        }

        private void simpleButton_XemDanhSachSV_Click(object sender, EventArgs e)
        {
            try
            {
                ChungChi.frm_Grd_DanhSachSinhVienDotXet frm = new frm_Grd_DanhSachSinhVienDotXet();
                frm._MaDot = lookUpEdit_DotXet.EditValue.ToString();
                frm.ShowDialog();
            }
            catch { }
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void rdGrpLoaiMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaLoaiChungChi();
        }

        private void lkuNamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lkuNamHoc.EditValue.ToString());
            LayDotXet(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString(), lkuLoaiChungChi.EditValue.ToString(), lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DotCap_Load(null, null);
        } 
        #endregion
    }
}