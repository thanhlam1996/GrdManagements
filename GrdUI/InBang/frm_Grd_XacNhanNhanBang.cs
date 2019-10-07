using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors.Repository;
using GrdUI.InBang;
using System.Globalization;
using GrdUI.ChungChi;

namespace GrdUI.InBang
{
    public partial class frm_Grd_XacNhanNhanBang : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtDataExcel = new DataTable();

        int _loaiLocDuLieu = 0;
        string _maLoaiChungChi = string.Empty;
        string _strFilter = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_XacNhanNhanBang()
        {
            InitializeComponent();
        }

        #region private void frm_Grd_XacNhanNhanBang_Load
        private void frm_Grd_XacNhanNhanBang_Load(object sender, EventArgs e)
        {
            CommonFunctions.SetFormPermiss(this);
            LoaiChungChi();
        }
        #endregion

        #endregion

        #region Functions

        #region private void LoaiChungChi()
        private void LoaiChungChi()
        {
            DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("#");

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
        #endregion

        #region private void GetData()
        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();
                _maLoaiChungChi = lkuLoaiChungChi.EditValue.ToString();
                switch (_loaiLocDuLieu)
                {
                    case 0:

                        _dtData = BL_ChungChi.XacNhanSinhVienNhanBang(string.Empty, _strFilter, string.Empty, string.Empty, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, false, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, false);
                        AppGridView.ShowField(gridViewData,
                            new string[] { "MaChuanXet", "MaDotXet", "MaDot", "StudyProgramName", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "DaNhan" },
                            new string[] { "Mã chuẩn xét", "Đợt xét", "Đợt cấp", "CTĐT", "Số hiệu bằng", "Số vào sổ", "Ngày nhận bằng", "Đã xác thực" },
                            new int[] { 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "MaChuanXet", "MaDotXet", "MaDotCap", "StudyProgramName", "SoHieuBang"
                            , "SoVaoSo", "NgayNhanBang", "DaNhan" }, DevExpress.Utils.HorzAlignment.Center);

                        gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                        break;
                    case 1:
                        _dtData = BL_ChungChi.XacNhanSinhVienNhanBang(string.Empty, string.Empty, _strFilter, string.Empty, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                        AppGridView.ShowField(gridViewData,
                            new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                , "MaChuanXet", "MaDotXet", "MaDot", "StudyProgramName", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "DaNhan" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                            , "Mã chuẩn xét", "Đợt xét", "Đợt cấp", "CTĐT", "Số hiệu bằng", "Số vào sổ", "Ngày nhận bằng", "Đã xác thực" },
                            new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "MaChuanXet", "MaDotXet", "MaDotCap", "StudyProgramName", "SoHieuBang"
                            , "SoVaoSo", "NgayNhanBang", "DaNhan" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID" });

                        break;
                    case 2:
                        _dtData = BL_ChungChi.XacNhanSinhVienNhanBang(string.Empty, string.Empty, string.Empty, _strFilter, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                        AppGridView.ShowField(gridViewData,
                             new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                , "MaChuanXet", "MaDotXet", "MaDot", "StudyProgramName", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "DaNhan" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                            , "Mã chuẩn xét", "Đợt xét", "Đợt cấp", "CTĐT", "Số hiệu bằng", "Số vào sổ", "Ngày nhận bằng", "Đã xác thực" },
                            new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "MaChuanXet", "MaDotXet", "MaDotCap", "StudyProgramName", "SoHieuBang"
                            , "SoVaoSo", "NgayNhanBang", "DaNhan" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID" });

                        break;
                    case 3:
                        _dtData = BL_ChungChi.XacNhanSinhVienNhanBang(string.Empty, string.Empty, string.Empty, string.Empty, _strFilter, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                        AppGridView.ShowField(gridViewData,
                             new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                , "MaChuanXet", "MaDotXet", "MaDot", "StudyProgramName", "SoHieuBang", "SoVaoSo", "NgayNhanBang", "DaNhan" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                            , "Mã chuẩn xét", "Đợt xét", "Đợt cấp", "CTĐT", "Số hiệu bằng", "Số vào sổ", "Ngày nhận bằng", "Đã xác thực" },
                            new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "MaChuanXet", "MaDotXet", "MaDotCap", "StudyProgramName", "SoHieuBang"
                            , "SoVaoSo", "NgayNhanBang", "DaNhan" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID" });

                        break;
                }

                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void SaveData()
        private void SaveData()
        {
            try
            {
                string strXml = string.Empty;
                int result = 0;
                string message = string.Empty;
                CultureInfo culture;
                culture = CultureInfo.CreateSpecificCulture("en-US");
                switch (_loaiLocDuLieu)
                {
                    case 0:
                        try
                        {
                            foreach (DataRow dr in _dtData.Rows)
                                if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                                    strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + dr["MaChuanXet"].ToString() + "\""
                                    + " MaDotXet = \"" + dr["MaDotXet"].ToString() + "\""
                                    + " MaDotCap = \"" + dr["MaDotCap"].ToString() + "\""
                                    + " MaChuongTrinh = \"" + dr["MaChuongTrinh"].ToString() + "\""
                                    + " NgayNhanBang = \"" + (dr["NgayNhanBang"].ToString()!=" "?String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["NgayNhanBang"].ToString()), culture):null) + "\""
                                    + " SoHieuBang = \"" + dr["SoHieuBang"].ToString() + "\""
                                    + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                    + " DaNhan = \"" + dr["DaNhan"].ToString() + "\"/>";

                            strXml = "<Root>" + strXml + "</Root>";

                            result = BL_ChungChi.LuuDSSinhVienNhanBang(strXml, _strFilter, _loaiLocDuLieu, _maLoaiChungChi, User._UserID);

                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 1:
                        try
                        {
                             
                            foreach (DataRow dr in _dtData.Rows)
                                if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                                {
                                    strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + dr["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + dr["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + dr["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + dr["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + (dr["NgayNhanBang"].ToString() != " " ? String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["NgayNhanBang"].ToString()), culture) : null) + "\""
                                   + " SoHieuBang = \"" + dr["SoHieuBang"].ToString() + "\""
                                   + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                   + " DaNhan = \"" + dr["DaNhan"].ToString() + "\"/>";

                                    strXml = "<Root>" + strXml + "</Root>";

                                    result = BL_ChungChi.LuuDSSinhVienNhanBang(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                                }
                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 2:
                        try
                        {
                            foreach (DataRow dr in _dtData.Rows)
                                if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                                {
                                    strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + dr["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + dr["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + dr["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + dr["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + (dr["NgayNhanBang"].ToString() != " " ? String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["NgayNhanBang"].ToString()), culture) : null) + "\""
                                   + " SoHieuBang = \"" + dr["SoHieuBang"].ToString() + "\""
                                   + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                   + " DaNhan = \"" + dr["DaNhan"].ToString() + "\"/>";

                                    strXml = "<Root>" + strXml + "</Root>";

                                    result = BL_ChungChi.LuuDSSinhVienNhanBang(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                                }
                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 3:
                        try
                        {
                            foreach (DataRow dr in _dtData.Rows)
                                if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                                {
                                    strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + dr["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + dr["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + dr["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + dr["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + (dr["NgayNhanBang"].ToString() != " " ? String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["NgayNhanBang"].ToString()), culture) : null) + "\""
                                   + " SoHieuBang = \"" + dr["SoHieuBang"].ToString() + "\""
                                   + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                   + " DaNhan = \"" + dr["DaNhan"].ToString() + "\"/>";

                                    strXml = "<Root>" + strXml + "</Root>";

                                    result = BL_ChungChi.LuuDSSinhVienNhanBang(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                                }
                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region  private void DeleteData()
        private void DeleteData()
        {
            try
            {
                string strXml = string.Empty;
                int result = 0;
                string message = string.Empty;
                switch (_loaiLocDuLieu)
                {
                    case 0:
                        foreach (int i in gridViewData.GetSelectedRows())
                            strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + gridViewData.GetDataRow(i)["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + gridViewData.GetDataRow(i)["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + gridViewData.GetDataRow(i)["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + gridViewData.GetDataRow(i)["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + gridViewData.GetDataRow(i)["NgayNhanBang"].ToString() + "\""
                                   + " DaNhan = \"" + gridViewData.GetDataRow(i)["DaNhan"].ToString() + "\"/>";

                        strXml = "<Root>" + strXml + "</Root>";

                        result = BL_ChungChi.XoaSinhVienNhanBangCC(strXml, _strFilter, _maLoaiChungChi, User._UserID);

                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 1:
                        foreach (int i in gridViewData.GetSelectedRows())
                        {
                            strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + gridViewData.GetDataRow(i)["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + gridViewData.GetDataRow(i)["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + gridViewData.GetDataRow(i)["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + gridViewData.GetDataRow(i)["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + gridViewData.GetDataRow(i)["NgayNhanBang"].ToString() + "\""
                                   + " DaNhan = \"" + gridViewData.GetDataRow(i)["DaNhan"].ToString() + "\"/>";

                            strXml = "<Root>" + strXml + "</Root>";

                            result = BL_ChungChi.XoaSinhVienNhanBangCC(strXml, gridViewData.GetDataRow(i)["StudentID"].ToString(), _maLoaiChungChi, User._UserID);
                        }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2:
                        foreach (int i in gridViewData.GetSelectedRows())
                        {
                            strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + gridViewData.GetDataRow(i)["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + gridViewData.GetDataRow(i)["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + gridViewData.GetDataRow(i)["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + gridViewData.GetDataRow(i)["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + gridViewData.GetDataRow(i)["NgayNhanBang"].ToString() + "\""
                                   + " DaNhan = \"" + gridViewData.GetDataRow(i)["DaNhan"].ToString() + "\"/>";

                            strXml = "<Root>" + strXml + "</Root>";

                            result = BL_ChungChi.XoaSinhVienNhanBangCC(strXml, gridViewData.GetDataRow(i)["StudentID"].ToString(), _maLoaiChungChi, User._UserID);
                        }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 3:
                        foreach (int i in gridViewData.GetSelectedRows())
                        {
                            strXml += "<DanhDachSVXacNhan MaChuanXet = \"" + gridViewData.GetDataRow(i)["MaChuanXet"].ToString() + "\""
                                   + " MaDotXet = \"" + gridViewData.GetDataRow(i)["MaDotXet"].ToString() + "\""
                                   + " MaDotCap = \"" + gridViewData.GetDataRow(i)["MaDotCap"].ToString() + "\""
                                   + " MaChuongTrinh = \"" + gridViewData.GetDataRow(i)["MaChuongTrinh"].ToString() + "\""
                                   + " NgayNhanBang = \"" + gridViewData.GetDataRow(i)["NgayNhanBang"].ToString() + "\""
                                   + " DaNhan = \"" + gridViewData.GetDataRow(i)["DaNhan"].ToString() + "\"/>";

                            strXml = "<Root>" + strXml + "</Root>";

                            result = BL_ChungChi.XoaSinhVienNhanBangCC(strXml, gridViewData.GetDataRow(i)["StudentID"].ToString(), _maLoaiChungChi, User._UserID);
                        }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        

        #endregion

        #region Events

        #region private void btn_thoat_Click
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region private void btn_luuDuLieu_Click
        private void btn_luuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region private void btn_xoaDuLieu_Click
        private void btn_xoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        #endregion

        #region private void btn_excel_Click
        private void btn_excel_Click(object sender, EventArgs e)
        {
            cms_ImportExcel.Show(Cursor.Position.X, Cursor.Position.Y);
        }
        #endregion

        #region private void btn_locDuLieu_Click
        private void btn_locDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }
        #endregion

        #region private void rdgCachNhap_SelectedIndexChanged
        private void rdgCachNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdgCachNhap.SelectedIndex)
            {
                case 0:
                    layoutControlItemNhapLieu.Text = "Sinh viên";
                    buttonEditNhapLieu.ReadOnly = false;
                    break;
                case 1:
                    layoutControlItemNhapLieu.Text = "Lớp";
                    buttonEditNhapLieu.ReadOnly = true;
                    break;
                case 2:
                    layoutControlItemNhapLieu.Text = "CTĐT";
                    buttonEditNhapLieu.ReadOnly = true;
                    break;
                case 3:
                    layoutControlItemNhapLieu.Text = "Đợt cấp";
                    buttonEditNhapLieu.ReadOnly = true;
                    break;                 
            }

            gridControlData.DataSource = null;
            gridViewData.Columns.Clear();
            _loaiLocDuLieu = rdgCachNhap.SelectedIndex;
            buttonEditNhapLieu.Text = string.Empty;
            _strFilter = string.Empty;
        }
        #endregion

        #region private void buttonEditNhapLieu_KeyDown
        private void buttonEditNhapLieu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_loaiLocDuLieu == 0 && e.KeyData == Keys.Enter)
                {
                    DataTable dt = BL_ChungChi.TimKiemThongTinSinhVienNhanBang(buttonEditNhapLieu.Text);

                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("Mã sinh viên không đúng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dt.Rows.Count == 1)
                    {
                        _strFilter = dt.Rows[0]["StudentID"].ToString();
                        buttonEditNhapLieu.Text = dt.Rows[0]["StudentID"].ToString()
                            + " - " + dt.Rows[0]["StudentName"].ToString() + " - " + dt.Rows[0]["ClassStudentName"].ToString();
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        DataView view = new DataView(dt);
                        DataTable distinctValues = view.ToTable(true, "StudentID");

                        if (distinctValues.Rows.Count == 1)
                        {
                            _strFilter = dt.Rows[0]["StudentID"].ToString();
                            buttonEditNhapLieu.Text = dt.Rows[0]["StudentID"].ToString()
                                + " - " + dt.Rows[0]["StudentName"].ToString() + " - " + dt.Rows[0]["ClassStudentName"].ToString();
                        }
                        else
                        {
                            frm_Grd_ThongTinSinhVien frm = new frm_Grd_ThongTinSinhVien();
                            frm.dtData = dt;
                            frm.WindowState = FormWindowState.Maximized;
                            frm.ShowDialog();

                            if (frm.isSubmit == true)
                            {
                                _strFilter = frm._studentID;
                                buttonEditNhapLieu.Text = frm._studentID + " - " + frm._studentName + " - " + frm._classStudentName;
                            }
                            else
                            {
                                _strFilter = string.Empty;
                                buttonEditNhapLieu.Text = string.Empty;
                                buttonEditNhapLieu.Focus();
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region private void buttonEditNhapLieu_ButtonClick
        private void buttonEditNhapLieu_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            buttonEditNhapLieu.Text = string.Empty;
            switch (_loaiLocDuLieu)                
            {
                case 0:
                    SplashScreenManager splashScreen = new SplashScreenManager();
                    SplashScreenManager.ShowForm(this, typeof(frm_Grd_ChoThucThi), true, true, false);

                    DataTable dt = BL_ChungChi.TimKiemThongTinSinhVienNhanBang(buttonEditNhapLieu.Text);

                    SplashScreenManager.CloseForm(false);

                    frm_Grd_ThongTinSinhVien frm = new frm_Grd_ThongTinSinhVien();
                    frm.dtData = dt;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();

                    if (frm.isSubmit == true)
                    {
                        _strFilter = frm._studentID;
                        buttonEditNhapLieu.Text = frm._studentID + " - " + frm._studentName + " - " + frm._classStudentName;
                    }
                    else
                    {
                        _strFilter = string.Empty;
                        buttonEditNhapLieu.Text = string.Empty;
                        buttonEditNhapLieu.Focus();
                    }
                    break;
                case 1:
                    frm_Grd_Lop frmLop = new frm_Grd_Lop();
                    frmLop.WindowState = FormWindowState.Maximized;
                    frmLop.ShowDialog();

                    if (frmLop._isSubmit)
                    {
                        _strFilter = frmLop._maLop;
                        buttonEditNhapLieu.Text = frmLop._maLop;
                    }
                    break;
                case 2:
                    frm_Grd_ChuongTrinhDaoTao frmCTDT = new frm_Grd_ChuongTrinhDaoTao();
                    frmCTDT.WindowState = FormWindowState.Maximized;
                    frmCTDT.ShowDialog();

                    if (frmCTDT._isSubmit)
                    {
                        _strFilter = frmCTDT._maCTDT;
                        buttonEditNhapLieu.Text = frmCTDT._maCTDT;
                    }
                    break;
                case 3:
                    frm_Grd_DotCap_2 frmDotCap = new frm_Grd_DotCap_2();
                    frmDotCap.WindowState = FormWindowState.Maximized;
                    frmDotCap.ShowDialog();

                    if (frmDotCap._isSubmit)
                    {
                        _strFilter = frmDotCap._maDotCap;
                        buttonEditNhapLieu.Text = frmDotCap._maDotCap;
                    }
                    break;
            }
        }
        #endregion


        #region private void btn_refresh_Click
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_XacNhanNhanBang_Load(null, null);
        }
        #endregion

        #endregion
    }
}