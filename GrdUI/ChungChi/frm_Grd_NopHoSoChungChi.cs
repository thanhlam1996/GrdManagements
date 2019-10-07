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

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_NopHoSoChungChi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtDataExcel = new DataTable();
        DataTable dt = new DataTable();
        int _loaiLocDuLieu = 0;
        string _maLoaiChungChi = string.Empty;
        string _strFilter = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_NopHoSoChungChi()
        {
            InitializeComponent();
        }

        #region private void frm_Grd_NopHoSoChungChi_Load
        private void frm_Grd_NopHoSoChungChi_Load(object sender, EventArgs e)
        {
            CommonFunctions.SetFormPermiss(this);

            LoaiChungChi();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("LoaiChon", typeof(string));
            dt.Rows.Add("KetQua", "Kết quả");
            dt.Rows.Add("DaNop", "Đã nộp");
            dt.Rows.Add("DaXacThuc", "Đã xác thực");
            dt.Rows.Add("CapTaiTruong", "Cấp tại trường");

            //List<string> values = new List<string>();
            //values.Add("Kết quả");
            //values.Add("Đã xác thực");
            //values.Add("Đã nộp");
            //values.Add("Cấp tại trường");
            //lkedit_LoaiCheck.Properties.DataSource = dt;
            //lkedit_LoaiCheck.Properties.DisplayMember = "LoaiChon";
            //lkedit_LoaiCheck.Properties.ValueMember = "ID";
            ////string[] data = (new string[] {"Kết quả", "Đã xác thực", "Đã nộp", "Cấp tại trường"});
            //lkedit_LoaiCheck.Properties.Columns.Add(new LookUpColumnInfo("ID", "ID"));
            //lkedit_LoaiCheck.Properties.Columns.Add(new LookUpColumnInfo("LoaiChon", "Loại chọn"));

            CCBEdit_LoaiChon.Properties.DataSource = dt;

            CCBEdit_LoaiChon.Properties.ValueMember = "ID";
            CCBEdit_LoaiChon.Properties.DisplayMember = "LoaiChon";

            CCBEdit_LoaiChon.Properties.SeparatorChar = ';';

        }
        #endregion

        #endregion

        #region Functions

        #region private void LoaiChungChi()
        private void LoaiChungChi()
        {
            DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi("CC");

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
                        _dtData = BL_ChungChi.NopHoSoChungChi(string.Empty, _strFilter, string.Empty, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, true, false);//, "Nhấn vào đây thêm dòng mới");
                        AppGridView.ShowField(gridViewData,
                            new string[] { "MaLoaiCCTT","TongDiem", "KetQua", "SoChungChi", "SoVaoSo", "NoiCap", "NgayCap", "NgayHetHan", "DaXacThuc", "DaNop"
                                , "CapTaiTruong", "GhiChu", "UpdateStaff", "UpdateDate" },
                            new string[] { "Loại chứng chỉ","Tổng điểm", "Kết quả", "Số chứng chỉ", "Số vào sổ", "Nơi cấp", "Ngày cấp", "Ngày hết hạn", "Đã xác thực", "Đã nộp"
                                , "Cấp tại trường", "Ghi chú", "Người cập nhật", "Ngày cập nhật" },
                            new int[] { 150,100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "TongDiem", "SoChungChi", "SoVaoSo", "NgayCap", "NgayHetHan"
                            , "UpdateStaff", "UpdateDate" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "TongDiem", "Số CC = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "UpdateStaff", "UpdateDate" });

                        gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

                        break;
                    case 1:
                        _dtData = BL_ChungChi.NopHoSoChungChi(string.Empty, string.Empty, _strFilter, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, true, false);
                        AppGridView.ShowField(gridViewData,
                            new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                ,"MaLoaiCCTT", "TongDiem", "KetQua", "SoChungChi", "SoVaoSo", "NoiCap", "NgayCap", "NgayHetHan", "DaXacThuc", "DaNop"
                                , "CapTaiTruong", "GhiChu", "UpdateStaff", "UpdateDate" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                                ,"Loại chứng chỉ", "Tổng điểm", "Kết quả", "Số chứng chỉ", "Số vào sổ", "Nơi cấp", "Ngày cấp", "Ngày hết hạn", "Đã xác thực", "Đã nộp"
                                , "Cấp tại trường", "Ghi chú", "Người cập nhật", "Ngày cập nhật" },
                            new int[] {150, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "BirthDay", "TongDiem", "SoChungChi", "SoVaoSo", "NgayCap", "NgayHetHan"
                            , "UpdateStaff", "UpdateDate" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID", "UpdateStaff", "UpdateDate" });

                        break;
                    case 2:
                        _dtData = BL_ChungChi.NopHoSoChungChi(string.Empty, string.Empty, string.Empty, _strFilter, _loaiLocDuLieu, _maLoaiChungChi);

                        foreach (DataColumn dc in _dtData.Columns)
                            dc.ReadOnly = false;

                        gridControlData.DataSource = _dtData;

                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, true, true);
                        AppGridView.ShowField(gridViewData,
                            new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                ,"MaLoaiCCTT", "TongDiem", "KetQua", "SoChungChi", "SoVaoSo", "NoiCap", "NgayCap", "NgayHetHan", "DaXacThuc", "DaNop"
                                , "CapTaiTruong", "GhiChu", "UpdateStaff", "UpdateDate" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                                ,"Loại chứng chỉ", "Tổng điểm", "Kết quả", "Số chứng chỉ", "Số vào sổ", "Nơi cấp", "Ngày cấp", "Ngày hết hạn", "Đã xác thực", "Đã nộp"
                                , "Cấp tại trường", "Ghi chú", "Người cập nhật", "Ngày cập nhật" },
                            new int[] { 100, 100, 100,100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "BirthDay", "TongDiem", "SoChungChi", "SoVaoSo", "NgayCap", "NgayHetHan"
                            , "UpdateStaff", "UpdateDate" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID", "UpdateStaff", "UpdateDate" });

                        break;
                    case 3:
                        _dtData = _dtDataExcel.Copy();
                        _strFilter = string.Empty;
                        foreach (DataRow dr in _dtData.Rows)
                        {

                            _strFilter += "<NopHoSoChungChi StudentID = \"" + dr["Mã SV"].ToString()
                                 //+ "\" MaLoaiCCTT = \"" + dr["Loại chứng chỉ"].ToString()
                                + "\" TongDiem = \"" + dr["Tổng điểm"].ToString()
                                + "\" KetQua = \"" + dr["Kết quả"].ToString()
                                + "\" SoChungChi = \"" + dr["Số chứng chỉ"].ToString()
                                + "\" SoVaoSo = \"" + dr["Số vào sổ"].ToString()
                                + "\" NoiCap = \"" + dr["Nơi cấp"].ToString()
                                + "\" NgayCap = \"" + dr["Ngày cấp"].ToString()
                                + "\" NgayHetHan = \"" + dr["Ngày hết hạn"].ToString()
                                + "\" DaXacThuc = \"" + dr["Đã xác thực"].ToString()
                                + "\" DaNop = \"" + dr["Đã nộp"].ToString()
                                + "\" CapTaiTruong = \"" + dr["Cấp tại trường"].ToString()
                                + "\" GhiChu = \"" + dr["Ghi chú"].ToString()
                                + "\"/>";
                        }
                            _strFilter = "<Root>" + _strFilter + "</Root>";
                            _dtData = BL_ChungChi.NopHoSoChungChi(_strFilter, string.Empty, string.Empty, string.Empty, _loaiLocDuLieu, _maLoaiChungChi);
                        
                            _dtData.Columns.Add("KetQuaImport", typeof(string));
                            _dtData.Columns["KetQuaImport"].ReadOnly = false;

                            foreach (DataColumn dc in _dtData.Columns)
                                dc.ReadOnly = false;

                            gridControlData.DataSource = _dtData;
                        
                        AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, true, false);
                        AppGridView.ShowField(gridViewData,
                            new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
                                ,"MaLoaiCCTT", "TongDiem", "KetQua", "SoChungChi", "SoVaoSo", "NoiCap", "NgayCap", "NgayHetHan", "DaXacThuc", "DaNop"
                                , "CapTaiTruong", "GhiChu", "KetQuaImport" },
                            new string[] { "Mã SV", "Họ và tên", "Ngày sinh", "Nơi sinh", "Lớp"
                                ,"Loại chứng chỉ" , "Tổng điểm", "Kết quả", "Số chứng chỉ", "Số vào sổ", "Nơi cấp", "Ngày cấp", "Ngày hết hạn", "Đã xác thực"
                                , "Đã nộp", "Cấp tại trường", "Ghi chú", "Kết quả đạt" },
                            new int[] { 100,100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 });
                        AppGridView.AlignField(gridViewData, new string[] { "BirthDay", "TongDiem", "SoChungChi", "SoVaoSo", "NgayCap", "NgayHetHan"
                            , "UpdateStaff", "UpdateDate" }, DevExpress.Utils.HorzAlignment.Center);
                        AppGridView.SummaryField(gridViewData, "StudentName", "Số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                        AppGridView.ReadOnlyColumn(gridViewData, new string[] { "StudentName", "BirthDay", "BirthPlace", "ClassStudentID", "UpdateStaff", "UpdateDate" });

                        break;
                }

                #region Xếp loại
                DataTable _dtChungChiThayThe = BL_ChungChi.LoaiChungChiThayThe(lkuLoaiChungChi.EditValue.ToString());
                DataView myDataView = new DataView(_dtChungChiThayThe);
                myDataView.Sort = "MaLoaiCCTT asc";

                repositoryItemLookUpEdit_LoaiCCTT.Properties.DataSource = myDataView.ToTable();
                repositoryItemLookUpEdit_LoaiCCTT.Properties.DisplayMember = "TenLoaiCCTT";
                repositoryItemLookUpEdit_LoaiCCTT.Properties.ValueMember = "MaLoaiCCTT";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_LoaiCCTT.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TenLoaiCCTT", 0, "Loại chứng chỉ"));

                repositoryItemLookUpEdit_LoaiCCTT.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEdit_LoaiCCTT.Properties.SearchMode = SearchMode.AutoComplete;
                repositoryItemLookUpEdit_LoaiCCTT.Properties.NullText = "";

                AppGridView.RegisterControlField(gridViewData, "MaLoaiCCTT", repositoryItemLookUpEdit_LoaiCCTT);
                #endregion

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

                switch (_loaiLocDuLieu)
                {
                    case 0:
                        foreach (DataRow dr in _dtData.Rows)
                            if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                                strXml += "<HoSoChungChi ID = \"" + dr["ID"].ToString() + "\""
                                + " TongDiem = \"" + dr["TongDiem"].ToString() + "\""
                                + " KetQua = \"" + dr["KetQua"].ToString() + "\""
                                + " SoChungChi = \"" + dr["SoChungChi"].ToString() + "\""
                                + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                + " NoiCap = \"" + dr["NoiCap"].ToString() + "\""
                                + " NgayCap = \"" + dr["NgayCap"].ToString() + "\""
                                + " NgayHetHan = \"" + dr["NgayHetHan"].ToString() + "\""
                                + " DaXacThuc = \"" + dr["DaXacThuc"].ToString() + "\""
                                + " DaNop = \"" + dr["DaNop"].ToString() + "\""
                                + " CapTaiTruong = \"" + dr["CapTaiTruong"].ToString() + "\""
                                + " MaLoaiCCTT = \"" + dr["MaLoaiCCTT"].ToString() + "\""
                                + " GhiChu = \"" + dr["GhiChu"].ToString() + "\"/>";

                        strXml = "<Root>" + strXml + "</Root>";

                        result = BL_ChungChi.LuuHoSoChungChi(strXml, _strFilter, _loaiLocDuLieu, _maLoaiChungChi , User._UserID);

                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 1:
                        foreach (DataRow dr in _dtData.Rows)
                            if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                            {
                                strXml = "<HoSoChungChi ID = \"" + dr["ID"].ToString() + "\""
                                + " TongDiem = \"" + dr["TongDiem"].ToString() + "\""
                                + " KetQua = \"" + dr["KetQua"].ToString() + "\""
                                + " SoChungChi = \"" + dr["SoChungChi"].ToString() + "\""
                                + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                + " NoiCap = \"" + dr["NoiCap"].ToString() + "\""
                                + " NgayCap = \"" + dr["NgayCap"].ToString() + "\""
                                + " NgayHetHan = \"" + dr["NgayHetHan"].ToString() + "\""
                                + " DaXacThuc = \"" + dr["DaXacThuc"].ToString() + "\""
                                + " DaNop = \"" + dr["DaNop"].ToString() + "\""
                                + " CapTaiTruong = \"" + dr["CapTaiTruong"].ToString() + "\""
                                + " MaLoaiCCTT = \"" + dr["MaLoaiCCTT"].ToString() + "\""
                                + " GhiChu = \"" + dr["GhiChu"].ToString() + "\"/>";

                                strXml = "<Root>" + strXml + "</Root>";

                                result = BL_ChungChi.LuuHoSoChungChi(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                            }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2:
                        foreach (DataRow dr in _dtData.Rows)
                            if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                            {
                                strXml = "<HoSoChungChi ID = \"" + dr["ID"].ToString() + "\""
                                + " TongDiem = \"" + dr["TongDiem"].ToString() + "\""
                                + " KetQua = \"" + dr["KetQua"].ToString() + "\""
                                + " SoChungChi = \"" + dr["SoChungChi"].ToString() + "\""
                                + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                + " NoiCap = \"" + dr["NoiCap"].ToString() + "\""
                                + " NgayCap = \"" + dr["NgayCap"].ToString() + "\""
                                + " NgayHetHan = \"" + dr["NgayHetHan"].ToString() + "\""
                                + " DaXacThuc = \"" + dr["DaXacThuc"].ToString() + "\""
                                + " DaNop = \"" + dr["DaNop"].ToString() + "\""
                                + " CapTaiTruong = \"" + dr["CapTaiTruong"].ToString() + "\""
                                + " MaLoaiCCTT = \"" + dr["MaLoaiCCTT"].ToString() + "\""
                                + " GhiChu = \"" + dr["GhiChu"].ToString() + "\"/>";

                                strXml = "<Root>" + strXml + "</Root>";

                                result = BL_ChungChi.LuuHoSoChungChi(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                            }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 3:
                        foreach (DataRow dr in _dtData.Rows)
                        {
                                strXml = "<HoSoChungChi ID = \"" + dr["ID"].ToString() + "\""
                                + " TongDiem = \"" + dr["TongDiem"].ToString() + "\""
                                + " KetQua = \"" + dr["KetQua"].ToString() + "\""
                                + " SoChungChi = \"" + dr["SoChungChi"].ToString() + "\""
                                + " SoVaoSo = \"" + dr["SoVaoSo"].ToString() + "\""
                                + " NoiCap = \"" + dr["NoiCap"].ToString() + "\""
                                + " NgayCap = \"" + dr["NgayCap"].ToString() + "\""
                                + " NgayHetHan = \"" + dr["NgayHetHan"].ToString() + "\""
                                + " DaXacThuc = \"" + dr["DaXacThuc"].ToString() + "\""
                                + " DaNop = \"" + dr["DaNop"].ToString() + "\""
                                + " CapTaiTruong = \"" + dr["CapTaiTruong"].ToString() + "\""
                                + " MaLoaiCCTT = \"" + dr["MaLoaiCCTT"].ToString() + "\""
                                + " GhiChu = \"" + dr["GhiChu"].ToString() + "\"/>";

                            strXml = "<Root>" + strXml + "</Root>";

                                result = BL_ChungChi.LuuHoSoChungChi(strXml, dr["StudentID"].ToString(), _loaiLocDuLieu, _maLoaiChungChi, User._UserID);
                        }
                        if (result == 0)
                        {
                            GetData();
                            XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            strXml += "<HoSoChungChi ID = \"" + gridViewData.GetDataRow(i)["ID"].ToString() + "\"/>";

                        if (strXml == string.Empty)
                        {
                            XtraMessageBox.Show("Chưa chọn sinh viên xóa", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            strXml = "<Root>" + strXml + "</Root>";
                            result = BL_ChungChi.XoaHoSoChungChi(strXml, _strFilter, _loaiLocDuLieu, _maLoaiChungChi, User._UserID);

                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case 1:
                    case 2:
                        foreach (int i in gridViewData.GetSelectedRows())
                        {
                            if(gridViewData.GetDataRow(i)["ID"].ToString()!=string.Empty)
                            strXml += "<HoSoChungChi ID = \"" + gridViewData.GetDataRow(i)["ID"].ToString()
                                    + "\" StudentID = \"" + gridViewData.GetDataRow(i)["StudentID"].ToString() + "\"/>";
                        }

                        if (strXml == string.Empty)
                        {
                            XtraMessageBox.Show("Chưa chọn sinh viên xóa", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            strXml = "<Root>" + strXml + "</Root>";
                            result = BL_ChungChi.XoaHoSoChungChi(strXml, string.Empty, _loaiLocDuLieu, _maLoaiChungChi, User._UserID);

                            if (result == 0)
                            {
                                GetData();
                                XtraMessageBox.Show("Xóa hồ sơ chứng chỉ thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case 3:
                        XtraMessageBox.Show("Chưa hỗ trợ phần xóa cho tính năng này.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void ExportExcel()
        private void ExportExcel()
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";

                sfdFiles.FileName = "UIS - Nộp hồ sơ chứng chỉ";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;
                    RepositoryItemCheckEdit columnEdit = (RepositoryItemCheckEdit)gridViewData.Columns["DaXacThuc"].RealColumnEdit;
                    columnEdit.DisplayValueChecked = "x";
                    columnEdit.DisplayValueUnchecked = "";

                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Nộp hồ sơ chứng chỉ";

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
                    layoutControlItemNhapLieu.Text = "Import Excel";
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
                    DataTable dt = BL_ChungChi.TimKiemThongTinSinhVien(buttonEditNhapLieu.Text, 0);

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

                    DataTable dt = BL_ChungChi.TimKiemThongTinSinhVien(buttonEditNhapLieu.Text, 0);

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
                    frm_Grd_ImportExcel f = new frm_Grd_ImportExcel();
                    f.ShowDialog();
                    _dtDataExcel = f._dtResult;
                    
                    buttonEditNhapLieu.Text = f._fileName;
                    
                    if (_dtDataExcel.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("Không có dữ liệu.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }
        }
        #endregion

        #region private void mnu_exportExcel_Click
        private void mnu_exportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }
        #endregion

        #region private void mnu_mauImport_Click
        private void mnu_mauImport_Click(object sender, EventArgs e)
        {
            CommonLib.ImportAndExport.ExportToExcell.Export(new string[][] { new string[] { "Mã SV"
                , "Tổng điểm", "Kết quả", "Số chứng chỉ", "Số vào sổ", "Nơi cấp", "Ngày cấp", "Ngày hết hạn", "Đã xác thực"
                , "Đã nộp", "Cấp tại trường", "Ghi chú" } }, new DataTable(), "UIS - Nộp hồ sơ chứng chỉ");

            //CommonLib.ImportAndExport.ExportToExcell.Export(new string[][]{ new string[] { "StudentID", "StudentName", "BirthDay", "BirthPlace", "ClassStudentID"
            //                    , "TongDiem", "KetQua", "SoChungChi", "SoVaoSo", "NoiCap", "NgayCap", "NgayHetHan", "DaXacThuc", "DaNop"
            //                    , "CapTaiTruong", "GhiChu" } }, new DataTable(), "UIS - Nộp hồ sơ chứng chỉ");
        }
        #endregion

        #region private void mnu_importDuLieu_Click
        private void mnu_importDuLieu_Click(object sender, EventArgs e)
        {

        }

        private void lkedit_LoaiCheck_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_ApDungCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtData.Columns.Count == 0)
                    return;
                foreach(CheckedListBoxItem item in CCBEdit_LoaiChon.Properties.GetItems())
                {
                    if(item.CheckState==CheckState.Checked)
                    {
                        for (int i = 0; i < gridViewData.DataRowCount; i++)
                            gridViewData.GetDataRow(i)[item.Value.ToString()] = checkEdit_All.CheckState;
                    }
                }
                //for (int i = 0; i < gridViewData.DataRowCount; i++)
                //{
                //    gridViewData.GetDataRow(i)["Chon"] = checkEdit_All.Checked;
                //}
            }

            catch(Exception ex) { }
        }
        #endregion

        #region private void btn_refresh_Click
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_NopHoSoChungChi_Load(null, null);
        }
        #endregion

        #endregion
    }
}