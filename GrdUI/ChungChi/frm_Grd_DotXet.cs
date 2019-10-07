using DevExpress.Common.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GrdCore.BLL;
using GrdReports;
using ScrUI.Others;
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
    public partial class frm_Grd_DotXet : Form
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow drGrids;
        DataSet _dtprintSet = new DataSet();
        #endregion

        #region Inits
        public frm_Grd_DotXet()
        {
            InitializeComponent();
        }

        private void frm_Grd_DotXet_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'DotXet'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            GetYearStudy();
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            GetGraduateLevels();
            GetStudyTypes();
            GetLoaiXet();
        }
        #endregion

        #region Functions
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
                lkuNamHoc.Properties.NullText = string.Empty;

                LookUpColumnInfoCollection coll = lkuNamHoc.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("YearStudy", 0, "Năm học"));

                lkuNamHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuNamHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lkuNamHoc.Properties.AutoSearchColumnIndex = 0;
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
                lkuHocKy.Properties.NullText = string.Empty;

                LookUpColumnInfoCollection coll = lkuHocKy.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lkuHocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuHocKy.Properties.SearchMode = SearchMode.AutoComplete;
                lkuHocKy.Properties.AutoSearchColumnIndex = 0;                

                lkuHocKy.ItemIndex = 0;
            }
            catch { }
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
                lkuBacDaoTao.Properties.NullText = string.Empty;

                lkuBacDaoTao.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuBacDaoTao.Properties.SearchMode = SearchMode.AutoComplete;
                lkuBacDaoTao.Properties.AutoSearchColumnIndex = 0;                

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
                lkuLHDT.Properties.NullText = string.Empty;

                lkuLHDT.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuLHDT.Properties.SearchMode = SearchMode.AutoComplete;
                lkuLHDT.Properties.AutoSearchColumnIndex = 0;                

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

        private void GetLoaiXet()
        {
            try
            {
                DataTable dtLoaiXet = BL_ChungChi.LoaiXet();

                lookUpEdit_CC.Properties.DataSource = dtLoaiXet;
                lookUpEdit_CC.Properties.DisplayMember = "TenLoaiChungChi";
                lookUpEdit_CC.Properties.ValueMember = "MaLoaiChungChi";

                LookUpColumnInfoCollection col2 = lookUpEdit_CC.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("MaLoaiChungChi", 0, "Mã loại chứng chỉ"));
                col2.Add(new LookUpColumnInfo("TenLoaiChungChi", 0, "Tên loại chứng chỉ"));
                lookUpEdit_CC.Properties.NullText = "";

                lookUpEdit_CC.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_CC.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_CC.Properties.AutoSearchColumnIndex = 0;

                lookUpEdit_CC.EditValue = "TN";
            }
            catch { }
        }

        private void GetData()
        {
            try
            {               
                _dtData = BL_ChungChi.DotXetTheoBacHe(lkuBacDaoTao.EditValue.ToString(), lkuLHDT.EditValue.ToString()
                    , lkuNamHoc.EditValue.ToString(), lkuHocKy.EditValue.ToString(), lookUpEdit_CC.EditValue.ToString());

                _dtData.Columns["OldMaDot"].AllowDBNull = true;

                _dtData.Columns.Add("ChiTiet", typeof(string));

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                AppGridView.RegisterControlField(gridViewData, "ChiTiet", repositoryItemButtonEdit_XemChiTietCTDT);
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
                    if (!(gridViewData.GetDataRow(i)["OldMaDot"] == DBNull.Value || gridViewData.GetDataRow(i)["OldMaDot"].ToString() == string.Empty))
                        strXml += "<DotXet MaDot = \"" + gridViewData.GetDataRow(i)["OldMaDot"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                int result = BL_ChungChi.XoaDotXet(strXml, User._UserID);

                if (result == 0)
                {
                    GetData();

                    XtraMessageBox.Show("Xóa thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (result == 1)
                    {
                        XtraMessageBox.Show("Đợt xét đã được lưu trong kết quả xét.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        XtraMessageBox.Show("Xóa không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                string _ngayBatDau = string.Empty, _ngayKetThuc = string.Empty;
                string strXml = string.Empty;
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                        if ((dr["BeginDate"] != DBNull.Value && dr["EndDate"] == DBNull.Value)
                            || (dr["BeginDate"] == DBNull.Value && dr["EndDate"] != DBNull.Value))
                        {
                            XtraMessageBox.Show("Dữ liệu ngày bắt đầu và ngày kết thúc không hợp lệ.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (dr["BeginDate"] != DBNull.Value || dr["EndDate"] != DBNull.Value)
                        {
                            if (Convert.ToDateTime(dr["BeginDate"]).Date > Convert.ToDateTime(dr["EndDate"]).Date)
                            {
                                XtraMessageBox.Show("Dữ liệu ngày bắt đầu phải nhỏ hơn ngày kết thúc.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (dr["BeginDate"] != DBNull.Value)
                                _ngayBatDau = Convert.ToDateTime(dr["BeginDate"]).ToString("dd/MM/yyyy");

                            if (dr["EndDate"] != DBNull.Value)
                                _ngayKetThuc = Convert.ToDateTime(dr["EndDate"]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            _ngayBatDau = string.Empty;
                            _ngayKetThuc = string.Empty;
                        }                            

                        strXml += "<DotXet MaDot = \"" + dr["MaDot"].ToString()
                                + "\" TenDot = \"" + dr["TenDot"].ToString()
                                + "\" GraduateLevelID = \"" + lkuBacDaoTao.EditValue.ToString()
                                + "\" StudyTypeID = \"" + lkuLHDT.EditValue.ToString()
                                + "\" YearStudy = \"" + lkuNamHoc.EditValue.ToString()
                                + "\" TermID = \"" + lkuHocKy.EditValue.ToString()
                                + "\" MaLoaiChungChi = \"" + lookUpEdit_CC.EditValue.ToString()
                                + "\" BeginDate = \"" + _ngayBatDau
                                + "\" EndDate = \"" + _ngayKetThuc
                                + "\" OldMaDot = \"" + dr["OldMaDot"].ToString()
                                + "\"/>";
                    }
                }
                strXml = "<Root>" + strXml + "</Root>";

                int KQ = BL_ChungChi.LuuDotXet(strXml, User._UserID);
                if (KQ == 0)
                {
                    GetData();

                    XtraMessageBox.Show("Lưu thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu không thành công.\nĐã tồn tại mã đợt xét này.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void lookUpEdit_NamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lkuNamHoc.EditValue.ToString());
        }

        private void btnLocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DotXet_Load(null, null);
        }

        private void repositoryItemButtonEdit_XemChiTietCTDT_Click(object sender, EventArgs e)
        {
            try
            {
                ChungChi.frm_Grd_DotCap_ChuongTrinh frm = new frm_Grd_DotCap_ChuongTrinh();
                frm.NhanDang = 2;
                frm._maLoaiChungChi = lookUpEdit_CC.EditValue.ToString();
                frm._bacDT = lkuBacDaoTao.EditValue.ToString();
                frm._loaiHinhDaoTao = lkuLHDT.EditValue.ToString();
                frm._namHoc = lkuNamHoc.EditValue.ToString();
                frm._hocKy = lkuHocKy.EditValue.ToString();
                frm._MaDot = gridViewData.GetFocusedDataRow()["MaDot"].ToString();
                frm.ShowDialog();
            }
            catch { }
        }

        private void btn_ThongKeTheoDot_Click(object sender, EventArgs e)
        {
            cms_Report_UEL.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void cms_ThongKeSLTN_TheoDot_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        strXml += "<PeriodOfGrantName MaDot =\"" + gridViewData.GetDataRow(i)["MaDot"].ToString()
                               + "\" StudyTypeID = \"" + gridViewData.GetDataRow(i)["StudyTypeID"].ToString() + "\"/>";
                    }
                    strXml = "<Root>" + strXml + "</Root>";
                  
                }
                else
                {
                    XtraMessageBox.Show("Hãy chọn Đợt cấp để in dữ liệu!");
                }

                _dtprintSet = BL_Reports.ThongKeTotNghieptheoDot(strXml);
                if(_dtprintSet.Tables["Decision"].Rows.Count>0)
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

                    frmGrdReports frm = new frmGrdReports();
                    DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                    frm._load_XtraReport_ThonkeTotNghiep_TheoDot(_dtprintSet, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Đợt xét này chưa áp dụng Quyết định nào nên không thể thống kê!", "Uis-Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void cms_ThongKeXepLoaiTNTheoNganh_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        strXml += "<PeriodOfGrantName MaDot =\"" + gridViewData.GetDataRow(i)["MaDot"].ToString()
                               + "\" StudyTypeID = \"" + gridViewData.GetDataRow(i)["StudyTypeID"].ToString() + "\"/>";
                    }
                    strXml = "<Root>" + strXml + "</Root>";

                }
                else
                {
                    XtraMessageBox.Show("Hãy chọn Đợt cấp để in dữ liệu!");
                }

                _dtprintSet = BL_Reports.ThongKeXepLoaiTotNghiepTheoNganh(strXml);
                if (_dtprintSet.Tables["Decision"].Rows.Count > 0)
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

                    frmGrdReports frm = new frmGrdReports();
                    DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                    frm._load_XtraReport_ThonkeTotNghiep_TheoNganh(_dtprintSet, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Đợt xét này chưa áp dụng Quyết định nào nên không thể thống kê!", "Uis-Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}
