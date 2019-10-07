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
using ScrUI.Others;
using GrdReports;

namespace GrdUI.InBang
{
    public partial class frm_Grd_DotCap : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataSet _dtprintSet = new DataSet();
        DataRow drGrids;

        string maLoaiChungChi = string.Empty, namHoc = string.Empty, hocKy = string.Empty, bacDT = string.Empty, loaiHinhDaoTao = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_DotCap()
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
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            GetGraduateLevels();
            GetStudyTypes();
        }
        #endregion

        #region Functions
        private void MaLoaiChungChi()
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

                //lkuNamHoc.ItemIndex = 0;
                lkuNamHoc.EditValue = User._CurrentYearStudy;
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

                //lkuHocKy.ItemIndex = 0;
                lkuHocKy.EditValue = User._CurrentTerm;
            }
            catch { }
        }

        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();

                maLoaiChungChi = lkuLoaiChungChi.EditValue.ToString();
                namHoc = lkuNamHoc.EditValue.ToString();
                hocKy = lkuHocKy.EditValue.ToString();
                bacDT = lkuBacDaoTao.EditValue.ToString();
                loaiHinhDaoTao = lkuLHDT.EditValue.ToString();
                _dtData = BL_InBang.DotCap(maLoaiChungChi, namHoc, hocKy, bacDT, loaiHinhDaoTao, 2);

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);
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
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    XtraMessageBox.Show("Đang có dữ liệu được chọn để xóa." + "\n" + "Hãy xử lý xóa hoặc bỏ chọn trước khi lưu."
                        , "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string strXml = string.Empty;
                string ngayKy = string.Empty;
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                    {
                        strXml += "<DotCap MaDot = \"" + dr["MaDot"].ToString()
                                + "\" TenDot = \"" + dr["TenDot"].ToString()
                                + "\" ID = \"" + dr["ID"].ToString()
                                + "\" GhiChu = \"" + dr["GhiChu"].ToString()
                                + "\" NgayKyBang = \"" + Convert.ToDateTime(dr["NgayKyBang"]).ToString("dd/MM/yyyy")
                                + "\" Excel = \"" + dr["Excel"].ToString()
                                + "\"/>";
                    }
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_InBang.LuuDotCap(strXml, maLoaiChungChi, User._UserID, namHoc, hocKy, bacDT, loaiHinhDaoTao);

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

        private void DeleteData()
        {
            try
            {
                if (XtraMessageBox.Show("Xóa dữ liệu đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    strXml += "<DotCap ID = \"" + gridViewData.GetDataRow(i)["ID"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_InBang.XoaDotCap(strXml, maLoaiChungChi, User._UserID, namHoc, hocKy, bacDT, loaiHinhDaoTao);

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
        #endregion

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btn_inan_thongke_Click(object sender, EventArgs e)
        {
            //if (User._CollegeID == 31)
            //    cms_thongke_UEL.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            cms_Report_UEL.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void cms_ThongKeSLTN_TheoDot_Click(object sender, EventArgs e)
        {
           
        }

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
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
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DotCap_Load(null, null);
        } 
        #endregion
    }
}