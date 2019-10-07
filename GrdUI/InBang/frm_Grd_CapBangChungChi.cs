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
using DevExpress.Export;
using DevExpress.XtraPrinting;
using GrdReports;
using GrdUI.ChungChi;
using ScrUI.Others;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace GrdUI.InBang
{
    public partial class frm_Grd_CapBangChungChi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable();

        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;
        DataTable dtLoaiChungChi = new DataTable();
        DataTable _dtDataExcel = new DataTable();
        bool lamMoi = false;

        DataTable dtPrints = new DataTable();
        int _loaiLocDuLieu = 0;
        string _strFilter = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_CapBangChungChi()
        {
            InitializeComponent();
        }

        private void frm_Grd_CapBangChungChi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            //DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            //try
            //{
            //    drGrids = (DataRow)dtGrid.Select("GridID = 'CapBangCC'").GetValue(0);

            //    _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
            //}
            //catch
            //{
            //    XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            #endregion
            #endregion

            MaLoaiChungChi();

            GetYearStudy();
            lkuNamHoc.EditValue = User._CurrentYearStudy;

            lkuHocKy.EditValue = User._CurrentTerm;

            GetGraduateLevels();

            GetStudyTypes();

            PhuongThucSapXepDuLieu();
        }
        #endregion

        #region Functions
        #region GetGraduateLevels
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
        #endregion

        #region GetStudyTypes
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

        #region MaLoaiChungChi
        private void MaLoaiChungChi()
        {
            try
            {
                dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("CC; CB");

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
        #endregion

        #region GetYearStudy
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
        #endregion

        #region GetTerms
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
        #endregion

        #region DotCap
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
            catch { }
        }
        #endregion

        #region GetData
        private void GetData(bool lamMoi)
        {
            try
            {
                _dtData = BL_InBang.DanhSachDuocCapBangChungChi(lkuDotCap.EditValue.ToString(), lamMoi, lookUpEdit_DanhHieu.EditValue.ToString());
                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                int dem = 0;
                for (int i = 0; i < _dtData.Rows.Count; i++)
                {
                    if(_dtData.Rows[i]["MaKhoaQuanLy"].ToString()=="06")
                    {
                        dem++;
                    }
                }
                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, true);
                AppGridView.ShowField(gridViewData,
                    new string[] { "MaSinhVien", "HoVaTen", "TA_HoVaTen", "NgaySinh", "NoiSinh", "TA_NoiSinh", "GioiTinh", "TA_GioiTinh"
                        , "Lop", "TenKhoaQuanLy", "TenBacDaoTao", "TA_TenBacDaoTao", "TenLHDT", "TA_TenLHDT", "MaNganh", "TenNganh","ChuyenNganh", "TA_TenNganh", "ThoiGianDaoTao"
                        , "TenXepLoai", "TA_TenXepLoai", "SoQuyetDinh", "NgayKyQuyetDinh", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "IsPrinted" },
                    new string[] { "Mã sinh viên", "Họ và tên", "Họ và tên (TA)", "Ngày sinh", "Nơi sinh", "Nơi sinh (TA)", "Giới tính", "Giới tính (TA)"
                        , "Lớp", "Khoa quản lý", "Bậc ĐT", "Bậc ĐT (TA)", "LHĐT", "LHĐT (TA)", "Mã ngành", "Tên ngành","Chuyên ngành", "Tên ngành (TA)", "TG đào tạo"
                        , "Xếp loại", "Xếp loại (TA)", "Số quyết định", "Ngày ký QĐ", "Số hiệu", "Số vào sổ", "Ngày nhận", "Đã in" },
                    new int[] { 80, 80, 80, 80, 80, 80, 80, 80
                        , 80, 80, 80, 80, 80, 80, 80, 80, 80, 80
                        , 80, 80, 80, 80, 80, 80, 80,80, 80 });

                AppGridView.AlignField(gridViewData, new string[] { "NgaySinh", "NoiSinh", "TA_NoiSinh", "GioiTinh", "TA_GioiTinh"
                        , "Lop", "TenBacDaoTao", "TA_TenBacDaoTao", "TenLHDT", "TA_TenLHDT", "MaNganh", "ThoiGianDaoTao"
                        , "TenXepLoai", "TA_TenXepLoai", "SoQuyetDinh", "NgayKyQuyetDinh", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "IsPrinted" }, DevExpress.Utils.HorzAlignment.Center);
                AppGridView.SummaryField(gridViewData, "MaSinhVien", "SL: {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.ReadOnlyColumn(gridViewData, new string[] { "MaSinhVien", "HoVaTen", "TA_HoVaTen", "NgaySinh", "NoiSinh", "TA_NoiSinh", "GioiTinh", "TA_GioiTinh"
                        , "Lop", "TenKhoaQuanLy", "TenBacDaoTao", "TA_TenBacDaoTao", "TenLHDT", "TA_TenLHDT", "MaNganh", "TenNganh","ChuyenNganh", "TA_TenNganh", "ThoiGianDaoTao"
                        , "TenXepLoai", "TA_TenXepLoai", "SoQuyetDinh", "NgayKyQuyetDinh", "IsPrinted" });

                gridViewData.Columns["MaSinhVien"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridViewData.Columns["HoVaTen"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                AppGridView.FixedField(gridViewData, new string[] { "IsPrinted", "SoHieuBang", "SoVaoSo", "NgayNhanBang" }, DevExpress.XtraGrid.Columns.FixedStyle.Right);

                gridViewData.OptionsView.ColumnAutoWidth = false;
                gridViewData.BestFitColumns();

                if (_dtData.Rows.Count > 0)
                {
                    string MaxString = _dtData.Rows[0]["MaxVaoSo"].ToString();
                    string _MaxsoVaoSo = string.Empty;
                    for (int j = 0; j < MaxString.Length - (int.Parse(MaxString).ToString()).Length; j++)
                    {
                        _MaxsoVaoSo += "0";
                    }

                    _MaxsoVaoSo += (int.Parse(MaxString) + 1).ToString();

                    textEdit_maSo_SoVaoSo.Text = _MaxsoVaoSo;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region SaveData
        private void SaveData()
        {
            try
            {
                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);

                    strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                            + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                            + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                            + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                            + "\" SoHieuBang = \"" + dr["SoHieuBang"].ToString()
                            + "\" SoVaoSo = \"" + dr["SoVaoSo"].ToString()
                            + "\" NgayNhanBang = \"" + dr["NgayNhanBang"].ToString()
                            + "\" STTVaoSo = \"" + dr["STTVaoSo"].ToString()
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                BL_InBang.LuuThongTinBangChungChi(strXml, User._UserID);

                XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //btn_locDuLieu_Click(null,null);
                GetData(lamMoi);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region MauPhoiBangChungChi
        private void MauPhoiBangChungChi()
        {
            try
            {
                DataTable dtMauIn = BL_ChungChi.MauBangMauChungChi_DanhHieu(lkuLoaiChungChi.EditValue.ToString(), lookUpEdit_DanhHieu.EditValue.ToString());

                lookUpEdit_mauBang.Properties.DataSource = dtMauIn;
                lookUpEdit_mauBang.Properties.DisplayMember = "TenMauIn";
                lookUpEdit_mauBang.Properties.ValueMember = "MaMauIn";
                lookUpEdit_mauBang.Properties.NullText = string.Empty;

                LookUpColumnInfoCollection coll = lookUpEdit_mauBang.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaMauIn", 0, "Mã mẫu in"));
                coll.Add(new LookUpColumnInfo("TenMauIn", 1, "Tên mẫu in"));

                lookUpEdit_mauBang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_mauBang.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_mauBang.Properties.AutoSearchColumnIndex = 1;

                lookUpEdit_mauBang.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Mẫu phôi bằng chứng chỉ in từng SV
        private void MauPhoiBangChungChi_InTungSV()
        {
            try
            {
                DataTable dtMauIn = BL_ChungChi.MauBangMauChungChi_DanhHieu(lkuLoaiChungChi.EditValue.ToString(), lookUpEdit_DanhHieu.EditValue.ToString());

                lookUpEdit_MauIn.Properties.DataSource = dtMauIn;
                lookUpEdit_MauIn.Properties.DisplayMember = "TenMauIn";
                lookUpEdit_MauIn.Properties.ValueMember = "MaMauIn";
                lookUpEdit_MauIn.Properties.NullText = string.Empty;

                LookUpColumnInfoCollection coll = lookUpEdit_MauIn.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaMauIn", 0, "Mã mẫu in"));
                coll.Add(new LookUpColumnInfo("TenMauIn", 1, "Tên mẫu in"));

                lookUpEdit_MauIn.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_MauIn.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_MauIn.Properties.AutoSearchColumnIndex = 1;

                lookUpEdit_MauIn.EditValue = dtMauIn.Rows[0]["MaMauIn"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void PhuongThucSapXepDuLieu()
        private void PhuongThucSapXepDuLieu()
        {
            try
            {
                DataTable _dtSort = BL_ChungChi.PhuongThucSapXepDuLieu("DSCB");

                gridControl_Sort.DataSource = _dtSort;

                AppGridView.InitGridView(gridView_Sort, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false, "Nhấn vào đây để thêm mới");
                AppGridView.ShowField(gridView_Sort,
                    new string[] { "MaCotHienThi", "SapXepTangDan" },
                    new string[] { "Cột sắp xếp", "Tăng dần" },
                    new int[] { 100, 100 });

                #region Cột sắp xếp
                DataTable _dtCachSapXep = BL_ChungChi.CotHienThiTrenLuoi("DSCB");

                repositoryItemGridLookUpEdit_cachSapXep.DataSource = _dtCachSapXep;

                AppGridView.RegisterControlField(gridView_Sort, "MaCotHienThi", repositoryItemGridLookUpEdit_cachSapXep);
                AppRepositoryItemGridLookUpEdit.InitRepositoryItemGridLookUp(repositoryItemGridLookUpEdit_cachSapXep, true, TextEditStyles.Standard);
                AppRepositoryItemGridLookUpEdit.ShowField(repositoryItemGridLookUpEdit_cachSapXep, new string[] { "TenCotHienThi" }, new string[] { "Cột sắp xếp" });
                repositoryItemGridLookUpEdit_cachSapXep.DisplayMember = "TenCotHienThi";
                repositoryItemGridLookUpEdit_cachSapXep.ValueMember = "MaCotHienThi";
                repositoryItemGridLookUpEdit_cachSapXep.NullText = string.Empty;
                #endregion

                gridView_Sort.OptionsView.ColumnAutoWidth = true;
                gridView_Sort.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void SortData()
        private void SortData()
        {
            try
            {
                string _strSort = string.Empty;
                bool _bTangDan = true;
                DataRow dr;

                gridViewData.ClearSorting();

                if (gridView_Sort.RowCount == 0)
                    return;

                for (int i = 0; i < gridView_Sort.RowCount; i++)
                {
                    dr = gridView_Sort.GetDataRow(i);

                    _strSort = dr["MaCotHienThi"].ToString();
                    if (_strSort == "HoLot")
                        _strSort = "LastNameS";
                    else if (_strSort == "Ten")
                        _strSort = "FirstNameS";
                    if (_strSort == "MaNganh")
                        _strSort = "STT_Nganh";

                    if (dr["SapXepTangDan"].ToString().ToUpper() == "FALSE")
                        _bTangDan = false;
                    else
                        _bTangDan = true;

                    if (_bTangDan == true)
                        gridViewData.Columns[_strSort].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    else
                        gridViewData.Columns[_strSort].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi :" + ex.ToString(), "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                gridViewData.RefreshData();
            }
        }
        #endregion

        #region private void LuuPhuongThucSapXep()
        private void LuuPhuongThucSapXep()
        {
            try
            {
                if (XtraMessageBox.Show("Cập nhật phương thức sắp xếp ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                string strXml = string.Empty;
                DataRow drS;
                bool _sapXepTangDan = true;
                for (int i = 0; i < gridView_Sort.RowCount; i++)
                {
                    drS = gridView_Sort.GetDataRow(i);

                    if (drS["SapXepTangDan"].ToString().ToUpper() == "FALSE")
                        _sapXepTangDan = false;
                    else
                        _sapXepTangDan = true;

                    strXml += "<SapXep MaCotHienThi = \"" + drS["MaCotHienThi"].ToString()
                            + "\" SapXep = \"" + (i + 1).ToString()
                            + "\" SapXepTangDan = \"" + _sapXepTangDan.ToString() + "\"/>";
                }

                strXml = "<Root>" + strXml + "</Root>";

                BL_ChungChi.LuuPhuongThucCapNhat("DSCB", strXml, User._UserID);

                PhuongThucSapXepDuLieu();
                XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region DanhHieu
        private void DanhHieu()
        {
            try
            {
                DataTable dtData = BL_ChungChi.DanhHieu();

                lookUpEdit_DanhHieu.Properties.DataSource = dtData;
                lookUpEdit_DanhHieu.Properties.DisplayMember = "GraduationDegreeName";
                lookUpEdit_DanhHieu.Properties.ValueMember = "GraduationDegreeID";

                LookUpColumnInfoCollection coll = lookUpEdit_DanhHieu.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GraduationDegreeName", 100, "Danh hiệu"));

                lookUpEdit_DanhHieu.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_DanhHieu.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_DanhHieu.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_DanhHieu.Properties.NullText = string.Empty;

                lookUpEdit_DanhHieu.ItemIndex = 0;
            }
            catch { }
        }
        #endregion

        #region Bật tắt chức năng
        private void isEnable(bool _Enable)
        {
            if (_Enable == true)
            {
                simpleButton_In.Enabled = true;
                simpleButton_Xem.Enabled = true;
                //simpleButton_Huy.Enabled = true;
            }
            else
            {
                simpleButton_In.Enabled = false;
                simpleButton_Xem.Enabled = false;
                //simpleButton_Huy.Enabled = false;
            }
        }
        #endregion

        #region Bật tắt chức năng
        private string CongPhanSo()
        {
            string _ChuoiSo;
            string _SoHienTai;
            int _so = 0;
            if (textEdit_So.Text == "" || textEdit_So.Text == string.Empty)
            {
                _SoHienTai = "0001";
                _so = 1;
            }
            else
            {
                _SoHienTai = textEdit_So.Text;
                _so = int.Parse(textEdit_So.Text);
            }
            _ChuoiSo = string.Empty;
            //Dem so 0 cua chuoi so
            for (int j = 0; j < _SoHienTai.Length - (_so + 1).ToString().Length; j++)
            {
                _ChuoiSo += "0";
            }

            _ChuoiSo = _ChuoiSo + (_so + 1).ToString();

            return _ChuoiSo;
        }
        #endregion

        #region Luu số hiệu bằng
        private void LuuSoHieuBang()
        {
            try
            {
                string strXml = string.Empty;
                for (int i = 0; i < dtPrints.Rows.Count; i++)
                {
                    DataRow dr = gridViewData.GetDataRow(i);

                    strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                            + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                            + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                            + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                            + "\" SoHieuBang = \"" + textEdit_Chu.Text + textEdit_So.Text
                            + "\" SoVaoSo = \"" + dr["SoVaoSo"].ToString()
                            + "\" IsPrinted = \"" + "1"
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                BL_InBang.LuuThongTinBangChungChi(strXml, User._UserID);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Không lưu được số hiệu bằng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion

        #region Events
        #region lkuLoaiChungChi_EditValueChanged
        private void lkuLoaiChungChi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string LoaiCC = ((DataRow)dtLoaiChungChi.Select("MaLoaiChungChi = '" + lkuLoaiChungChi.EditValue.ToString() + "'").GetValue(0))["MaHinhThuc"].ToString();
                if (LoaiCC == "CB")
                {
                    DanhHieu();
                    layoutControlItem_DanhHieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    layoutControlItem_DanhHieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                DotCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đữ liệu", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region lkuNamHoc_EditValueChanged
        private void lkuNamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lkuNamHoc.EditValue.ToString());
            DotCap();
        }
        #endregion

        #region lkuHocKy_EditValueChanged
        private void lkuHocKy_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }
        #endregion

        #region lkuBacDaoTao_EditValueChanged
        private void lkuBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }
        #endregion

        #region lkuLHDT_EditValueChanged
        private void lkuLHDT_EditValueChanged(object sender, EventArgs e)
        {
            DotCap();
        }
        #endregion

        #region btn_locDuLieu_Click
        private void btn_locDuLieu_Click(object sender, EventArgs e)
        {
            GetData(false);
            MauPhoiBangChungChi();
            MauPhoiBangChungChi_InTungSV();
        }
        #endregion

        #region btn_thoat_Click
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btn_luuDuLieu_Click
        private void btn_luuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region btn_xoaDuLieu_Click
        private void btn_xoaDuLieu_Click(object sender, EventArgs e)
        {
            GetData(true);
        }
        #endregion

        #region btn_refresh_Click
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_CapBangChungChi_Load(null, null);
        }
        #endregion

        #region btnExcel_Click
        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();
                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Cấp bằng, chứng chỉ ('" + lkuDotCap.Text + "')";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    GridControl gridControlExcel = new DevExpress.XtraGrid.GridControl();
                    GridView gridViewExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
                    gridControlExcel.ViewCollection.Add(gridViewExcel);
                    gridControlExcel.MainView = gridViewExcel;

                    DataTable dtExcelLoad = new DataTable();
                    dtExcelLoad.Columns.Add("Mã sinh viên");
                    dtExcelLoad.Columns.Add("Họ và tên");
                    dtExcelLoad.Columns.Add("Họ và tên (TA)");
                    dtExcelLoad.Columns.Add("Ngày sinh");
                    dtExcelLoad.Columns.Add("Nơi sinh");
                    dtExcelLoad.Columns.Add("Nơi sinh (TA)");
                    dtExcelLoad.Columns.Add("Giới tính");
                    dtExcelLoad.Columns.Add("Giới tính (TA)");
                    dtExcelLoad.Columns.Add("Lớp");
                    dtExcelLoad.Columns.Add("Khoa quản lý");
                    dtExcelLoad.Columns.Add("Bậc ĐT");
                    dtExcelLoad.Columns.Add("Bậc ĐT (TA)");
                    dtExcelLoad.Columns.Add("LHĐT");
                    dtExcelLoad.Columns.Add("LHĐT (TA)");
                    dtExcelLoad.Columns.Add("Mã ngành");
                    dtExcelLoad.Columns.Add("Tên ngành");
                    dtExcelLoad.Columns.Add("Tên ngành (TA)");
                    dtExcelLoad.Columns.Add("TG đào tạo");
                    dtExcelLoad.Columns.Add("Xếp loại");
                    dtExcelLoad.Columns.Add("Xếp loại (TA)");
                    dtExcelLoad.Columns.Add("Số quyết định");
                    dtExcelLoad.Columns.Add("Ngày ký QĐ");
                    dtExcelLoad.Columns.Add("Số hiệu");
                    dtExcelLoad.Columns.Add("Số vào sổ");
                    dtExcelLoad.Columns.Add("Ngày nhận bằng");
                    dtExcelLoad.Columns.Add("Đã in");
                    //dtExcelLoad.Columns.Add("MaxVaoSo");
                    DataRow dr;
                    for (int i = 0; i < gridViewData.RowCount; i++)
                    {
                        dr = dtExcelLoad.NewRow();
                        dr["Mã sinh viên"] = gridViewData.GetDataRow(i)["MaSinhVien"].ToString();
                        dr["Họ và tên"] = gridViewData.GetDataRow(i)["HoVaTen"].ToString();
                        dr["Họ và tên (TA)"] = gridViewData.GetDataRow(i)["TA_HoVaTen"].ToString();
                        dr["Ngày sinh"] = gridViewData.GetDataRow(i)["NgaySinh"].ToString();
                        dr["Nơi sinh"] = gridViewData.GetDataRow(i)["NoiSinh"].ToString();
                        dr["Nơi sinh (TA)"] = gridViewData.GetDataRow(i)["TA_NoiSinh"].ToString();
                        dr["Giới tính"] = gridViewData.GetDataRow(i)["GioiTinh"].ToString();
                        dr["Giới tính (TA)"] = gridViewData.GetDataRow(i)["TA_GioiTinh"].ToString();
                        dr["Lớp"] = gridViewData.GetDataRow(i)["Lop"].ToString();
                        dr["Khoa quản lý"] = gridViewData.GetDataRow(i)["TenKhoaQuanLy"].ToString();
                        dr["Bậc ĐT"] = gridViewData.GetDataRow(i)["TenBacDaoTao"].ToString();
                        dr["Bậc ĐT (TA)"] = gridViewData.GetDataRow(i)["TA_TenBacDaoTao"].ToString();
                        dr["LHĐT"] = gridViewData.GetDataRow(i)["TenLHDT"].ToString();
                        dr["LHĐT (TA)"] = gridViewData.GetDataRow(i)["TA_TenLHDT"].ToString();
                        dr["Mã ngành"] = gridViewData.GetDataRow(i)["MaNganh"].ToString();
                        dr["Tên ngành"] = gridViewData.GetDataRow(i)["TenNganh"].ToString();
                        dr["Tên ngành (TA)"] = gridViewData.GetDataRow(i)["TA_TenNganh"].ToString();
                        dr["TG đào tạo"] = gridViewData.GetDataRow(i)["ThoiGianDaoTao"].ToString();
                        dr["Xếp loại"] = gridViewData.GetDataRow(i)["TenXepLoai"].ToString();
                        dr["Xếp loại (TA)"] = gridViewData.GetDataRow(i)["TA_TenXepLoai"].ToString();
                        dr["Số quyết định"] = gridViewData.GetDataRow(i)["SoQuyetDinh"].ToString();
                        dr["Ngày ký QĐ"] = Convert.ToDateTime(gridViewData.GetDataRow(i)["NgayKyQuyetDinh"]).ToString("dd/MM/yyyy");
                        dr["Số hiệu"] = gridViewData.GetDataRow(i)["SoHieuBang"].ToString();
                        dr["Số vào sổ"] = gridViewData.GetDataRow(i)["SoVaoSo"].ToString();
                        dr["Ngày nhận bằng"] = gridViewData.GetDataRow(i)["NgayNhanBang"].ToString() == "" ? string.Empty : Convert.ToDateTime(gridViewData.GetDataRow(i)["NgayNhanBang"]).ToString("dd/MM/yyyy");
                        dr["Đã in"] = gridViewData.GetDataRow(i)["IsPrinted"].ToString();
                        dtExcelLoad.Rows.Add(dr);
                    }
                    foreach (DataColumn tCol in dtExcelLoad.Columns)
                    {
                        GridColumn gCol = new GridColumn();
                        gCol.Name = "col" + tCol.ColumnName;
                        gCol.FieldName = tCol.ColumnName;
                        gCol.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
                        gridViewExcel.Columns.Add(gCol);
                        gCol.Visible = true;
                    }
                    gridControlExcel.DataSource = dtExcelLoad;
                    Controls.Add(gridControlExcel);
                    gridControlExcel.ForceInitialize();



                    gridViewExcel.OptionsSelection.MultiSelect = false;
                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;
                    gridControlExcel.Visible = true;
                    var options = new XlsxExportOptions();

                    options.SheetName = "Danh sách cấp bằng, chứng chỉ_Đợt "+lkuDotCap.EditValue.ToString();

                    gridViewExcel.ExportToXlsx(sfdFiles.FileName, options);

                    gridViewExcel.OptionsSelection.MultiSelect = true;
                    Controls.Remove(gridControlExcel);
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gridViewData.OptionsSelection.MultiSelect = true;
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btn_capSoVaoSo_Click
        private void btn_capSoVaoSo_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Cấp số vào sổ cho các dòng đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int index_VS;
                try
                {
                    index_VS = int.Parse(textEdit_maSo_SoVaoSo.Text.Trim());
                }
                catch
                {
                    XtraMessageBox.Show("Mã số không đúng định dạng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string _soVaoSo = string.Empty;
                int _lenSoVaoSo = textEdit_maSo_SoVaoSo.Text.Trim().Length;
                string _maSV = string.Empty;
                int _DemSLSV = 0;
                if (textBox_SoLuongcapSVS.Text == string.Empty || int.Parse(textBox_SoLuongcapSVS.Text) == 0)
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        DataRow dr = gridViewData.GetDataRow(i);
                        if (checkEdit_ChuaCapSHS.Checked != true || dr["SoVaoSo"].ToString() == string.Empty)
                        {
                            if (index_VS <= 0 && checkEdit_soVaoSoGiamDan.Checked)
                            {
                                XtraMessageBox.Show("Không đủ số vào sổ để cấp.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            _soVaoSo = string.Empty;

                            //Dem so 0 cua chuoi so
                            for (int j = 0; j < _lenSoVaoSo - index_VS.ToString().Length; j++)
                            {
                                _soVaoSo += "0";
                            }

                            _soVaoSo += index_VS.ToString();

                            if (checkEdit_maSV_maHV.Checked)
                                _maSV = gridViewData.GetDataRow(i)["MaSinhVien"].ToString();

                            if (radioGroup_soVaoSo.SelectedIndex == 0)
                                dr["SoVaoSo"] = textEdit_SHB_SoVaoSo.Text + _maSV + textEdit_SHS_SoVaoSo.Text + _soVaoSo;
                            else if (radioGroup_soVaoSo.SelectedIndex == 1)
                                dr["SoVaoSo"] = _soVaoSo + textEdit_SHB_SoVaoSo.Text + _maSV + textEdit_SHS_SoVaoSo.Text;
                            else
                                dr["SoVaoSo"] = textEdit_SHB_SoVaoSo + _soVaoSo + textEdit_SHS_SoVaoSo.Text + _maSV;

                            dr["STTVaoSo"] = _soVaoSo;

                            if (checkEdit_soVaoSoGiamDan.Checked)
                                index_VS--;
                            else
                                index_VS++;
                        }

                    }
                }
                else
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        if (_DemSLSV >= Int16.Parse(textBox_SoLuongcapSVS.Text))
                            break;

                        DataRow dr = gridViewData.GetDataRow(i);
                        if (checkEdit_ChuaCapSHS.Checked != true || dr["SoVaoSo"].ToString() == string.Empty)
                        {
                            if (index_VS <= 0 && checkEdit_soVaoSoGiamDan.Checked)
                            {
                                XtraMessageBox.Show("Không đủ số vào sổ để cấp.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            _soVaoSo = string.Empty;

                            //Dem so 0 cua chuoi so
                            for (int j = 0; j < _lenSoVaoSo - index_VS.ToString().Length; j++)
                            {
                                _soVaoSo += "0";
                            }

                            _soVaoSo += index_VS.ToString();

                            if (checkEdit_maSV_maHV.Checked)
                                _maSV = gridViewData.GetDataRow(i)["MaSinhVien"].ToString();

                            if (radioGroup_soVaoSo.SelectedIndex == 0)
                                dr["SoVaoSo"] = textEdit_SHB_SoVaoSo.Text + _maSV + textEdit_SHS_SoVaoSo.Text + _soVaoSo;
                            else if (radioGroup_soVaoSo.SelectedIndex == 1)
                                dr["SoVaoSo"] = _soVaoSo + textEdit_SHB_SoVaoSo.Text + _maSV + textEdit_SHS_SoVaoSo.Text;
                            else
                                dr["SoVaoSo"] = textEdit_SHB_SoVaoSo + _soVaoSo + textEdit_SHS_SoVaoSo.Text + _maSV;

                            if (checkEdit_soVaoSoGiamDan.Checked)
                                index_VS--;
                            else
                                index_VS++;

                            _DemSLSV++;
                        }
                    }
                }

                XtraMessageBox.Show("Cấp số vào sổ thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
        #endregion

        #region btn_xoaSoVaoSo_Click
        private void btn_xoaSoVaoSo_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Xóa số vào sổ các dòng đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);
                    dr["SoVaoSo"] = string.Empty;
                    dr["STTVaoSo"] = string.Empty;
                }

                XtraMessageBox.Show("Xóa số vào sổ thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
        #endregion

        #region btn_capSoHieuBang_Click
        private void btn_capSoHieuBang_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Cấp số hiệu bằng cho các dòng đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                int index;
                try
                {
                    index = int.Parse(textEdit_maSo_SoHieuBang.Text.Trim());
                }
                catch
                {
                    XtraMessageBox.Show("Mã số không đúng định dạng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string _soHieuBang = string.Empty;
                int _lenSoHieuBang = textEdit_maSo_SoHieuBang.Text.Trim().Length;

                int _DemSLSV = 0;
                if (textEdit_SoLuongCapSHB.Text == string.Empty || int.Parse(textEdit_SoLuongCapSHB.Text) == 0)
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        if (index <= 0 && checkEdit_soHieuBangGiamDan.Checked)
                        {
                            XtraMessageBox.Show("Không đủ số hiệu bằng để cấp.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        if (checkEdit_ChuaCoSHB.Checked != true || gridViewData.GetDataRow(i)["SoHieuBang"].ToString() == string.Empty)
                        {

                            _soHieuBang = string.Empty;

                            //Dem so 0 cua chuoi so
                            for (int j = 0; j < _lenSoHieuBang - index.ToString().Length; j++)
                            {
                                _soHieuBang += "0";
                            }

                            _soHieuBang += index.ToString();

                            if (radioGroup_soHieuBang.SelectedIndex == 1)
                                gridViewData.GetDataRow(i)["SoHieuBang"] = _soHieuBang + textEdit_SHB_SoHieuBang.Text.Trim();
                            else
                                gridViewData.GetDataRow(i)["SoHieuBang"] = textEdit_SHB_SoHieuBang.Text.Trim() + _soHieuBang;

                            if (checkEdit_soHieuBangGiamDan.Checked)
                                index--;
                            else
                                index++;
                        }
                    }
                }
                else
                {
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        if (_DemSLSV >= Int16.Parse(textEdit_SoLuongCapSHB.Text))
                            break;

                        if (index <= 0 && checkEdit_soHieuBangGiamDan.Checked)
                        {
                            XtraMessageBox.Show("Không đủ số hiệu bằng để cấp.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        if (checkEdit_ChuaCoSHB.Checked != true || gridViewData.GetDataRow(i)["SoHieuBang"].ToString() == string.Empty)
                        {

                            _soHieuBang = string.Empty;

                            //Dem so 0 cua chuoi so
                            for (int j = 0; j < _lenSoHieuBang - index.ToString().Length; j++)
                            {
                                _soHieuBang += "0";
                            }

                            _soHieuBang += index.ToString();

                            if (radioGroup_soHieuBang.SelectedIndex == 1)
                                gridViewData.GetDataRow(i)["SoHieuBang"] = _soHieuBang + textEdit_SHB_SoHieuBang.Text.Trim();
                            else
                                gridViewData.GetDataRow(i)["SoHieuBang"] = textEdit_SHB_SoHieuBang.Text.Trim() + _soHieuBang;

                            if (checkEdit_soHieuBangGiamDan.Checked)
                                index--;
                            else
                                index++;

                            _DemSLSV++;
                        }
                    }

                }

                XtraMessageBox.Show("Cấp số hiệu bằng thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region btn_xoaSoHieuBang_Click
        private void btn_xoaSoHieuBang_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Xóa số hiệu bằng các dòng đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);
                    dr["SoHieuBang"] = string.Empty;
                }

                XtraMessageBox.Show("Xóa số hiệu bằng thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
        #endregion

        #region btn_huyDaInBang_Click
        private void btn_huyDaInBang_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);

                    strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                            + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                            + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                            + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                BL_InBang.HuyTinhTrangDaInBang(strXml, User._UserID);

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);
                    dr["IsPrinted"] = false;
                    dr["NgayNhanBang"] = string.Empty;
                }
                XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region gridViewData_CustomDrawRowIndicator
        private void gridViewData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (_dtData.Rows.Count == 0)
                    return;

                if (e.Info.IsRowIndicator && e.RowHandle != -999997)
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region btn_inBang_Click
        private void btn_inBang_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);
                    if(dr["SoHieuBang"].ToString()==string.Empty || dr["SoVaoSo"].ToString()==string.Empty)
                    {
                        XtraMessageBox.Show("Sinh viên " + dr["MaSinhVien"].ToString() + " chưa được cấp SHB/SVS", "UIS - Thông báo");
                        continue;
                    }
                    strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                            + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                            + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                            + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                            + "\" SoHieuBang = \"" + dr["SoHieuBang"].ToString()
                            + "\"/>";
                }
                if (strXml != string.Empty)
                {
                    strXml = "<Root>" + strXml + "</Root>";

                    if (lookUpEdit_mauBang.EditValue.ToString() == null)
                    {
                        XtraMessageBox.Show("Chưa chọn mẫu bằng để in", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataTable dtPrints = BL_InBang.ThongTinCapBangChungChi(strXml, lookUpEdit_mauBang.EditValue.ToString());
                        if (dtPrints.Rows.Count < 0)
                            return;
                        frmGRDInBangChungChi frm = new frmGRDInBangChungChi();
                        frm._dtDataSource = dtPrints.Copy();
                        frm._reportName = lookUpEdit_mauBang.EditValue.ToString();
                        frm.WindowState = FormWindowState.Maximized;
                        frm.ShowDialog();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region btnInSoGoc_Click
        private void btnInSoGoc_Click(object sender, EventArgs e)
        {
            if (User._CollegeID == 31)
            {
                try
                {
                    string strXml = "<Root>";
                    DataRow row;
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        row = gridViewData.GetDataRow(i);
                        strXml += "<BangDiem StudentID = \"" + row["MaSinhVien"].ToString()
                                + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                                + "\" MaDot = \"" + lkuDotCap.EditValue.ToString()
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
                    _tbPrint = BL_InBang.SoGocCapBangTN_UEL(strXml);
                    if (_tbPrint.Rows.Count == 0)
                        return;

                    frmGrdReports frm = new frmGrdReports();
                    frm._load_XtraReport_SoGocCapBangTN_UEL(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                    frm.ShowDialog();
                }
                catch (Exception ex) { }
            }
            else
            {
                try
                {

                    DataTable _dtPrints = new DataTable();
                    _dtPrints.Columns.Add("HeDaoTao", typeof(string));
                    _dtPrints.Columns.Add("DotThang", typeof(string));
                    _dtPrints.Columns.Add("NgayRaQuyetDinh", typeof(string));
                    _dtPrints.Columns.Add("NgayKyBang", typeof(string));
                    _dtPrints.Columns.Add("MaSV", typeof(string));
                    _dtPrints.Columns.Add("HoLot", typeof(string));
                    _dtPrints.Columns.Add("Ten", typeof(string));
                    _dtPrints.Columns.Add("NgaySinh", typeof(string));
                    _dtPrints.Columns.Add("Phai", typeof(string));
                    _dtPrints.Columns.Add("DanToc", typeof(string));
                    _dtPrints.Columns.Add("QuocTich", typeof(string));
                    _dtPrints.Columns.Add("XepLoai", typeof(string));
                    _dtPrints.Columns.Add("SoHieuBang", typeof(string));
                    _dtPrints.Columns.Add("SoVaoSo", typeof(string));
                    _dtPrints.Columns.Add("KhoaQuanLy", typeof(string));
                    _dtPrints.Columns.Add("NganhHoc", typeof(string));
                    _dtPrints.Columns.Add("Khoi", typeof(string));
                    _dtPrints.Columns.Add("KhoaHoc", typeof(string));
                    _dtPrints.Columns.Add("SoQuyetDinh", typeof(string));
                    _dtPrints.Columns.Add("NgayQuyetDinh", typeof(string));
                    _dtPrints.Columns.Add("NoiSinh", typeof(string));
                    _dtPrints.Columns.Add("MaKhoaQuanLy", typeof(string));
                    _dtPrints.Columns.Add("MaNganh", typeof(string));
                    _dtPrints.Columns.Add("CTDT", typeof(string));
                    _dtPrints.Columns.Add("TenS", typeof(string));
                    _dtPrints.Columns.Add("HoLotS", typeof(string));
                    _dtPrints.Columns.Add("STT_Nganh", typeof(string));

                    DataRow dr;
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        dr = _dtPrints.NewRow();

                        dr["HeDaoTao"] = gridViewData.GetDataRow(i)["TenLHDT"].ToString().ToUpper();
                        dr["DotThang"] = gridViewData.GetDataRow(i)["DotCapThang"];

                        if (gridViewData.GetDataRow(i)["NgayKyQuyetDinh"] != DBNull.Value)
                        {
                            dr["NgayRaQuyetDinh"] = Convert.ToDateTime(gridViewData.GetDataRow(i)["NgayKyQuyetDinh"]).ToString("dd/MM/yyyy");
                            dr["NgayQuyetDinh"] = Convert.ToDateTime(gridViewData.GetDataRow(i)["NgayKyQuyetDinh"]).ToString("dd/MM/yyyy");
                        }

                        if (gridViewData.GetDataRow(i)["NgayKyBang"] != DBNull.Value)
                            dr["NgayKyBang"] = Convert.ToDateTime(gridViewData.GetDataRow(i)["NgayKyBang"]).ToString("dd/MM/yyyy");

                        dr["MaSV"] = gridViewData.GetDataRow(i)["MaSinhVien"];
                        dr["HoLot"] = gridViewData.GetDataRow(i)["HoLot"];
                        dr["Ten"] = gridViewData.GetDataRow(i)["Ten"];
                        dr["NgaySinh"] = gridViewData.GetDataRow(i)["NgaySinh"];
                        dr["Phai"] = gridViewData.GetDataRow(i)["GioiTinh"];
                        dr["XepLoai"] = gridViewData.GetDataRow(i)["TenXepLoai"];
                        dr["SoHieuBang"] = gridViewData.GetDataRow(i)["SoHieuBang"];
                        dr["SoVaoSo"] = gridViewData.GetDataRow(i)["SoVaoSo"];
                        dr["KhoaQuanLy"] = "KHOA " + gridViewData.GetDataRow(i)["TenKhoaQuanLy"].ToString().ToUpper();
                        dr["NganhHoc"] = "NGÀNH " + gridViewData.GetDataRow(i)["TenNganh"].ToString().ToUpper();
                        dr["Khoi"] = gridViewData.GetDataRow(i)["Lop"];
                        dr["KhoaHoc"] = gridViewData.GetDataRow(i)["TenKhoaHoc"];
                        dr["SoQuyetDinh"] = gridViewData.GetDataRow(i)["SoQuyetDinh"].ToString().ToUpper();
                        dr["NoiSinh"] = gridViewData.GetDataRow(i)["NoiSinh"];
                        dr["MaKhoaQuanLy"] = gridViewData.GetDataRow(i)["MaKhoaQuanLy"];
                        dr["MaNganh"] = gridViewData.GetDataRow(i)["MaNganh"];
                        dr["CTDT"] = gridViewData.GetDataRow(i)["MaChuongTrinh"];
                        dr["TenS"] = gridViewData.GetDataRow(i)["FirstNameS"];
                        dr["HoLotS"] = gridViewData.GetDataRow(i)["LastNameS"];
                        dr["STT_Nganh"] = gridViewData.GetDataRow(i)["MaKhoaQuanLy"];

                        _dtPrints.Rows.Add(dr);
                    }

                    frmGrdReports frm = new frmGrdReports();
                    frm._load_XtraReport_UTE_SoBang(_dtPrints);
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region lookUpEdit_mauBang_EditValueChanged
        private void lookUpEdit_mauBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int a = Int32.Parse(lookUpEdit_mauBang.EditValue.ToString());
                DataTable dtMauIn = BL_InBang.MauBangMauChungChi(null, Int32.Parse(lookUpEdit_mauBang.EditValue.ToString()));
                textEdit_SHS_SoVaoSo.Text = dtMauIn.Rows[0]["KiHieuSVS"].ToString();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion

        #region Áp dụng sắp xếp
        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            SortData();
        }
        #endregion

        #region Lưu sắp xếp 
        private void simpleButton_CapNhat_Click(object sender, EventArgs e)
        {
            LuuPhuongThucSapXep();
        }
        #endregion

        #region Kiểm tra ký tự số (số vào sổ)
        private void textBox_SoLuongcapSVS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            //if (e.KeyChar == (char)13) btnSearch_Click(sender, e);
        }
        #endregion

        #region Kiểm tra ký tự số (Số hiệu bằng)
        private void textEdit_SoLuongCapSHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }
        #endregion

        #region btn_importCBCC_Click
        private void btn_importCBCC_Click(object sender, EventArgs e)
        {
            frm_Grd_ImportExcel f = new frm_Grd_ImportExcel();
            f.ShowDialog();
            DataTable _dtExcel = f._dtResult;
            if (_dtExcel.Rows.Count == 0 || _dtData.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có dữ liệu.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                DataRow dr; string strXml = string.Empty;
                for (int row = 0; row < _dtData.Rows.Count; row++)
                {
                    if (_dtExcel.Select("[Mã sinh viên] = '" + _dtData.Rows[row]["MaSinhVien"].ToString() + "' and [Mã Ngành] = '" + _dtData.Rows[row]["MaNganh"].ToString() + "'").Length > 0)
                    {
                        DataRow drow = (DataRow)_dtExcel.Select("[Mã sinh viên] = '" + _dtData.Rows[row]["MaSinhVien"].ToString() + "' and [Mã Ngành] = '" + _dtData.Rows[row]["MaNganh"].ToString() + "'").GetValue(0);
                        if (checkEdit_GhiDeDuLieu.Checked == true)
                        {
                            _dtData.Rows[row]["SoVaoSo"] = drow["Số vào sổ"].ToString();
                            _dtData.Rows[row]["SoHieuBang"] = drow["Số hiệu"].ToString();
                        }
                        else
                        {
                            if (drow["Số vào sổ"].ToString() != string.Empty && _dtData.Rows[row]["SoVaoSo"].ToString() == string.Empty)
                            {
                                _dtData.Rows[row]["SoVaoSo"] = drow["Số vào sổ"].ToString();
                            }

                            if (drow["Số hiệu"].ToString() != string.Empty && _dtData.Rows[row]["SoHieuBang"].ToString() == string.Empty)
                            {
                                _dtData.Rows[row]["SoHieuBang"] = drow["Số hiệu"].ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (lkuDotCap.EditValue.ToString() == null || lookUpEdit_DanhHieu.EditValue.ToString() == null)
                    XtraMessageBox.Show("UIS - Thông báo", "Bổ sung thông tin đợt cấp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btn_ApDung_Click
        private void btn_ApDung_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKiTuApDung.Text != string.Empty && int.Parse(txtKiTuApDung.Text) != 0)
                {
                    int a = int.Parse(txtKiTuApDung.Text);
                    StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                    khongDuocChapNhan9.Appearance.BackColor = Color.CadetBlue;
                    khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                    khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                    khongDuocChapNhan9.Expression = "[DoDaiTen] > " + a;
                    gridViewData.FormatConditions.Add(khongDuocChapNhan9);
                    gridViewData.OptionsSelection.EnableAppearanceFocusedRow = false;
                }
                else
                {
                    int a = 1000;
                    StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                    khongDuocChapNhan9.Appearance.BackColor = Color.CadetBlue;
                    khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                    khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                    khongDuocChapNhan9.Expression = "[DoDaiTen] > " + a;
                    gridViewData.FormatConditions.Add(khongDuocChapNhan9);
                    gridViewData.OptionsSelection.EnableAppearanceFocusedRow = false;
                }
            }
            catch { }
        }
        #endregion

        #region Kiểm tra ký tự nhập vào là số
        private void txtKiTuApDung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;           //Allow Backspace
        }

        #endregion

        #region Sau khi nhập ký tự
        private void txtKiTuApDung_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtKiTuApDung.Text != string.Empty && int.Parse(txtKiTuApDung.Text) != 0)
            {
                int a = int.Parse(txtKiTuApDung.Text);
                StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                khongDuocChapNhan9.Appearance.BackColor = Color.CadetBlue;
                khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                khongDuocChapNhan9.Expression = "[DoDaiTen] > " + a;
                gridViewData.FormatConditions.Add(khongDuocChapNhan9);
                gridViewData.OptionsSelection.EnableAppearanceFocusedRow = false;

               StyleFormatCondition khongDuocChapNhan9_White = new DevExpress.XtraGrid.StyleFormatCondition();
                khongDuocChapNhan9_White.Appearance.BackColor = Color.White;
                khongDuocChapNhan9_White.Appearance.Options.UseBackColor = true;
                khongDuocChapNhan9_White.Condition = FormatConditionEnum.Expression;
                khongDuocChapNhan9_White.Expression = "[DoDaiTen] <= " + a;
                gridViewData.FormatConditions.Add(khongDuocChapNhan9_White);
                gridViewData.OptionsSelection.EnableAppearanceFocusedRow = false;
            }
            else
            {
                int a = 0;
                StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                khongDuocChapNhan9.Appearance.BackColor = Color.White;
                khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                khongDuocChapNhan9.Expression = "[DoDaiTen] > " + a;
                gridViewData.FormatConditions.Add(khongDuocChapNhan9);
                gridViewData.OptionsSelection.EnableAppearanceFocusedRow = false;
            }
        }
        #endregion

        #region MenuItems
        #region mnu_46_DNU_SoGocCapBangTN_Click
        private void mnu_46_DNU_SoGocCapBangTN_Click(object sender, EventArgs e)
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
                _tbPrint = BL_InBang.SoGocCapbangChungChi_DNU(lkuDotCap.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_SoGocCapBang_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

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
                    strXml += "<GiayCNTNTemp StudentID = \"" + row["MaSinhVien"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
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
                string _nguoiLap = frmSoQD._NguoiLap;
                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.GiayChungNhanTotNghiepTamThoi_DNU(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_ChungNhanHocThanhKhoaHoc_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, _nguoiLap, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_46_DNU_SoGocCapChungChi_Click
        private void mnu_46_DNU_SoGocCapChungChi_Click(object sender, EventArgs e)
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
                _tbPrint = BL_InBang.SoGocCapbangChungChi_DNU(lkuDotCap.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_SoGocCapChungChi_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region simpleButton_InDuLieu_Click
        private void simpleButton_InDuLieu_Click(object sender, EventArgs e)
        {
            mnu_items.Show(Cursor.Position.X, Cursor.Position.Y);
        }
        #endregion

        #endregion

        #endregion

        private void mnu_31_UEL_BangDiemTNCQA3_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<BangDiem StudentID = \"" + row["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
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

                string _ngayKyTenTA = frmSoQD._ngayQDEng;
                string _nguoiKyTenTA = frmSoQD._hoVaTenEng;
                string _CapBacTA = frmSoQD._capBacEng;

                DataTable _tbTiengViet = new DataTable();
                DataTable _tbTiengAnh = new DataTable();
                _tbTiengViet = BL_ChungChi.BangDiemTotNghiepCQ_A3(strXml);
                _tbTiengAnh = BL_ChungChi.BangDiemTotNghiepCQ_A3_TiengAnh(strXml);

                if (_tbTiengViet.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                string NameControl = ((ToolStripMenuItem)sender).Name;
                string TextControl = ((ToolStripMenuItem)sender).Text;
                //frm._load_XtraReport_ChungNhanHocThanhKhoaHoc_UEL(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._User.StaffName
                //    , DBNull.Value.ToString(), this.Name, NameControl, TextControl, User._CollegeLogo, User._CollegeName, User._AdministrativeUnit);

                frm._load_XtraReport_BangDiemTNCQ_A3_UEL(_tbTiengViet, _tbTiengAnh, _ngayKyTen, _CapBac, _nguoiKyTen, _ngayKyTenTA
                    , _nguoiKyTenTA, _CapBacTA, User._User.StaffName, DBNull.Value.ToString(), this.Name, NameControl, TextControl, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }

        private void mnu_31_UEL_BangDiemTN_KCQA3_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                DataRow row;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    row = gridViewData.GetDataRow(i);
                    strXml += "<BangDiem StudentID = \"" + row["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
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

                string _ngayKyTenTA = frmSoQD._ngayQDEng;
                string _nguoiKyTenTA = frmSoQD._hoVaTenEng;
                string _CapBacTA = frmSoQD._capBacEng;

                DataTable _tbTiengViet = new DataTable();
                DataTable _tbTiengAnh = new DataTable();
                _tbTiengViet = BL_ChungChi.BangDiemTotNghiepCQ_A3(strXml);
                _tbTiengAnh = BL_ChungChi.BangDiemTotNghiepCQ_A3_TiengAnh(strXml);

                if (_tbTiengViet.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                string NameControl = ((ToolStripMenuItem)sender).Name;
                string TextControl = ((ToolStripMenuItem)sender).Text;

                frm._load_XtraReport_BangDiemTNKCQ_A3_UEL(_tbTiengViet, _tbTiengAnh, _ngayKyTen, _CapBac, _nguoiKyTen, _ngayKyTenTA
                    , _nguoiKyTenTA, _CapBacTA, User._User.StaffName, DBNull.Value.ToString(), this.Name, NameControl, TextControl, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }

        private void checkEdit_InTungSV_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_InTungSV.Checked == true)
            {
                MessageBox.Show("Chức năng này chỉ được sử dụng khi số vào sổ đã được cấp","UIS - Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                isEnable(checkEdit_InTungSV.Checked);
            }
            else
            {
                isEnable(checkEdit_InTungSV.Checked);
            }
        }
     

        #region Lấy dữ liệu từ dòng đang chọn
        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (checkEdit_InTungSV.Checked == true)
                {
                    string strXml = string.Empty;
                    DataRow dr = gridViewData.GetFocusedDataRow();
                    if (dr["SoVaoSo"].ToString() == string.Empty)
                    {
                        XtraMessageBox.Show("Sinh viên " + dr["MaSinhVien"].ToString() + " chưa được cấp SVS", "UIS - Thông báo");
                    }
                    else
                    {
                        strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                                + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                                + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                                + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                                + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                                + "\"/>";
                    }

                    if (strXml != string.Empty)
                    {
                        strXml = "<Root>" + strXml + "</Root>";
                        if (lookUpEdit_MauIn.EditValue.ToString() == null)
                        {
                            XtraMessageBox.Show("Chưa chọn mẫu bằng để in", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dtPrints = BL_InBang.ThongTinCapBangChungChi(strXml, lookUpEdit_MauIn.EditValue.ToString());
                            if (dtPrints.Rows.Count < 0)
                            {
                                XtraMessageBox.Show("Không tìm thấy dữ liệu", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            textEdit_HoTenTV.Text = dtPrints.Rows[0]["HoVaTen"].ToString();
                            label_NgaySinh.Text = dtPrints.Rows[0]["TV_NgaySinh"].ToString();
                            label_XepLoai.Text = dtPrints.Rows[0]["TenXepLoai"].ToString();
                            label_LHDT.Text = dtPrints.Rows[0]["TenLHDT"].ToString();
                            label_Nganh.Text = dtPrints.Rows[0]["TenNganh"].ToString();
                            label_NgayIn.Text = dtPrints.Rows[0]["NgayKyBang"].ToString();
                            label_DanhXung.Text = dtPrints.Rows[0]["DanhXung"].ToString();

                            textEdit_HoTenTA.Text = dtPrints.Rows[0]["TA_HoVaTen"].ToString();
                            label_NgaySinh_TA.Text = dtPrints.Rows[0]["TA_NgaySinh"].ToString();
                            label_XepLoai_TA.Text = dtPrints.Rows[0]["TA_TenXepLoai"].ToString();
                            label_LHDT_TA.Text = dtPrints.Rows[0]["TA_TenLHDT"].ToString();
                            label_Nganh_TA.Text = dtPrints.Rows[0]["TA_TenNganh"].ToString();
                            label_NgayIn_TA.Text = dtPrints.Rows[0]["TA_NgayKyBang"].ToString();
                            label_DX_TA.Text = dtPrints.Rows[0]["TA_DanhXung"].ToString();

                            if (dtPrints.Rows[0]["IsPrinted"].ToString().ToUpper() == "TRUE")
                            {
                                label_TinhTrangIn.Text = "Đã in";
                                isEnable(false);
                            }
                            else
                            {
                                label_TinhTrangIn.Text = "Chưa in";
                                isEnable(true);
                            }

                            label_SVS.Text = dtPrints.Rows[0]["SoVaoSo"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        private void simpleButton_In_Click(object sender, EventArgs e)
        {
            try
            {
                dtPrints.Columns["HoVaTen"].ReadOnly = false;
                dtPrints.Columns["TA_HoVaTen"].ReadOnly = false;
                dtPrints.Rows[0]["SoHieuBang"] = textEdit_Chu.Text + textEdit_So.Text;
                dtPrints.Rows[0]["HoVaTen"] = textEdit_HoTenTV.Text;
                dtPrints.Rows[0]["TA_HoVaTen"] = textEdit_HoTenTA.Text;
                frmGRDInBangChungChi frm = new frmGRDInBangChungChi();
                frm._dtDataSource = dtPrints.Copy();
                frm._reportName = lookUpEdit_MauIn.EditValue.ToString();
                frm.WindowState = FormWindowState.Maximized;
                if (checkEdit_KT.Checked == true)
                {
                    frm._Print = false;
                }
                else
                {
                    frm._Print = true;
                }
                frm.ShowDialog();

                LuuSoHieuBang();

                textEdit_So.Text =  CongPhanSo();

                ((DataRow)_dtData.Select("MaSinhVien = '" + dtPrints.Rows[0]["MaSinhVien"].ToString() + "'").GetValue(0))["IsPrinted"] = true;
                gridViewData.RefreshData();
            }
            catch (Exception ex)
            { }
        }

        private void textEdit_So_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void simpleButton_Xem_Click(object sender, EventArgs e)
        {
            try
            {
                dtPrints.Columns["HoVaTen"].ReadOnly = false;
                dtPrints.Columns["TA_HoVaTen"].ReadOnly = false;
                dtPrints.Rows[0]["SoHieuBang"] = textEdit_Chu.Text + textEdit_So.Text;
                dtPrints.Rows[0]["HoVaTen"] = textEdit_HoTenTV.Text;
                dtPrints.Rows[0]["TA_HoVaTen"] = textEdit_HoTenTA.Text;
                frmGRDInBangChungChi frm = new frmGRDInBangChungChi();
                frm._dtDataSource = dtPrints.Copy();
                frm._reportName = lookUpEdit_MauIn.EditValue.ToString();
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
            catch (Exception ex)
            { }
        }

        private void simpleButton_Huy_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);

                    strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                            + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                            + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                            + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                            + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                BL_InBang.HuyTinhTrangDaInBang(strXml, User._UserID);

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    DataRow dr = gridViewData.GetDataRow(i);
                    dr["IsPrinted"] = false;
                }
                XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewData_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (checkEdit_InTungSV.Checked == true)
                {
                    dtPrints = new DataTable();
                    string strXml = string.Empty;
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        DataRow dr = gridViewData.GetDataRow(i);
                        if (dr["SoVaoSo"].ToString() == string.Empty)
                        {
                            XtraMessageBox.Show("Sinh viên " + dr["MaSinhVien"].ToString() + " chưa được cấp SVS", "UIS - Thông báo");
                            continue;
                        }
                        strXml += "<DSCB MaSinhVien = \"" + dr["MaSinhVien"].ToString()
                                + "\" MaChuanXet = \"" + dr["MaChuanXet"].ToString()
                                + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                                + "\" MaDotXet = \"" + dr["MaDotXet"].ToString()
                                + "\" MaDotCap = \"" + dr["MaDotCap"].ToString()
                                + "\"/>";

                        break;
                    }

                    if (strXml != string.Empty)
                    {
                        strXml = "<Root>" + strXml + "</Root>";
                        if (lookUpEdit_MauIn.EditValue.ToString() == null)
                        {
                            XtraMessageBox.Show("Chưa chọn mẫu bằng để in", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dtPrints = BL_InBang.ThongTinCapBangChungChi(strXml, lookUpEdit_MauIn.EditValue.ToString());
                            if (dtPrints.Rows.Count < 0)
                            {
                                XtraMessageBox.Show("Không tìm thấy dữ liệu", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }
                    }

                    textEdit_HoTenTV.Text = dtPrints.Rows[0]["HoVaTen"].ToString();
                    label_NgaySinh.Text = dtPrints.Rows[0]["TV_NgaySinh"].ToString();
                    label_XepLoai.Text = dtPrints.Rows[0]["TenXepLoai"].ToString();
                    label_LHDT.Text = dtPrints.Rows[0]["TenLHDT"].ToString();
                    label_Nganh.Text = dtPrints.Rows[0]["TenNganh"].ToString();
                    label_NgayIn.Text = dtPrints.Rows[0]["NgayKyBang"].ToString();
                    label_DanhXung.Text = dtPrints.Rows[0]["DanhXung"].ToString();

                    textEdit_HoTenTA.Text = dtPrints.Rows[0]["TA_HoVaTen"].ToString();
                    label_NgaySinh_TA.Text = dtPrints.Rows[0]["TA_NgaySinh"].ToString();
                    label_XepLoai_TA.Text = dtPrints.Rows[0]["TA_TenXepLoai"].ToString();
                    label_LHDT_TA.Text = dtPrints.Rows[0]["TA_TenLHDT"].ToString();
                    label_Nganh_TA.Text = dtPrints.Rows[0]["TA_TenNganh"].ToString();
                    label_NgayIn_TA.Text = dtPrints.Rows[0]["TA_NgayKyBang"].ToString();
                    label_DX_TA.Text = dtPrints.Rows[0]["TA_DanhXung"].ToString();

                    if (dtPrints.Rows[0]["IsPrinted"].ToString().ToUpper() == "TRUE")
                    {
                        label_TinhTrangIn.Text = "Đã in";
                        isEnable(false);
                    }
                    else
                    {
                        label_TinhTrangIn.Text = "Chưa in";
                        isEnable(true);
                    }

                    label_SVS.Text = dtPrints.Rows[0]["SoVaoSo"].ToString();
                }
            }
            catch { }
        }

   
    }
}