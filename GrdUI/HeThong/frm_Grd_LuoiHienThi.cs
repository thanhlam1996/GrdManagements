using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace GrdUI.HeThong
{
    public partial class frm_Grd_LuoiHienThi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable();
        #endregion

        #region Inits
        public frm_Grd_LuoiHienThi()
        {
            InitializeComponent();
        }

        private void frm_Grd_LuoiHienThi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            btnSave.Enabled = (User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM");
            btnDelete.Enabled = (User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM");
            #endregion

            GetData();
        }
        #endregion

        #region Functions
        private void GetData()
        {
            try
            {
                gridViewData.Columns.Clear();

                _dtData = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _dtData.Columns["ModuleID"].AllowDBNull = true;

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, false, "Nhấn vào đây để thêm mới");
                AppGridView.ShowField(gridViewData,
                    new string[] { "GridID", "GridName", "ShowAutoFilterRow", "MultiSelect", "MultiSelectMode"
                        , "ShowGroupPanel", "NewItemRowPosition", "ColumnAutoWidth", "ShowViewCaption", "BestFitColumns" },
                    new string[] { "Mã lưới", "Tên lưới", "Lọc dữ liệu", "Chọn nhiều dòng", "Kiểu chọn nhiều"
                        , "Nhóm dữ liệu", "Vị trí dòng thêm mới", "Tự động canh độ rộng", "Thể hiện tên lưới", "Độ rộng cột theo dữ liệu" });

                if (!(User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM"))
                    AppGridView.ReadOnlyColumn(gridViewData);

                #region MultiSelectMode
                DataTable dtMultiSelectMode = new DataTable();
                dtMultiSelectMode.Columns.Add("ID", typeof(string));
                dtMultiSelectMode.Columns.Add("Description", typeof(string));

                dtMultiSelectMode.Rows.Add("CELL", "Chọn nhiều ô dữ liệu");
                dtMultiSelectMode.Rows.Add("ROW", "Chọn nhiều dòng dữ liệu");
                dtMultiSelectMode.Rows.Add("CHECKBOX", "Chọn nhiều bằng check");

                repositoryItemLookUpEditMultiMode.DataSource = dtMultiSelectMode;
                repositoryItemLookUpEditMultiMode.DisplayMember = "Description";
                repositoryItemLookUpEditMultiMode.ValueMember = "ID";
                repositoryItemLookUpEditMultiMode.NullText = string.Empty;

                LookUpColumnInfoCollection coll = repositoryItemLookUpEditMultiMode.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditMultiMode.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditMultiMode.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "MultiSelectMode", repositoryItemLookUpEditMultiMode);
                #endregion

                #region NewItemRowPosition
                DataTable dtNewRow = new DataTable();
                dtNewRow.Columns.Add("ID", typeof(string));
                dtNewRow.Columns.Add("Description", typeof(string));

                dtNewRow.Rows.Add("TOP", "Phía trên");
                dtNewRow.Rows.Add("BOTTOM", "Phía dưới");
                dtNewRow.Rows.Add("NONE", "Không hiển thị");

                repositoryItemLookUpEditNewRow.DataSource = dtNewRow;
                repositoryItemLookUpEditNewRow.DisplayMember = "Description";
                repositoryItemLookUpEditNewRow.ValueMember = "ID";
                repositoryItemLookUpEditNewRow.NullText = string.Empty;

                LookUpColumnInfoCollection collN = repositoryItemLookUpEditNewRow.Columns;
                collN.Clear();
                collN.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditNewRow.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditNewRow.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "NewItemRowPosition", repositoryItemLookUpEditNewRow);
                #endregion

                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
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
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                        strXml += "<Grids ID = \"" + dr["ID"].ToString() //== string.Empty ? "0": dr["ID"].ToString()
                            + "\" GridID = \"" + dr["GridID"].ToString()
                            + "\" GridName = \"" + dr["GridName"].ToString()
                            + "\" ForeignGridName = \"" + dr["ForeignGridName"].ToString()
                            + "\" ShowAutoFilterRow = \"" + dr["ShowAutoFilterRow"].ToString()
                            + "\" MultiSelect = \"" + dr["MultiSelect"].ToString()
                            + "\" MultiSelectMode = \"" + dr["MultiSelectMode"].ToString()
                            + "\" ShowGroupPanel = \"" + dr["ShowGroupPanel"].ToString()
                            + "\" NewItemRowPosition = \"" + dr["NewItemRowPosition"].ToString()
                            + "\" ColumnAutoWidth = \"" + dr["ColumnAutoWidth"].ToString()
                            + "\" ShowViewCaption = \"" + dr["ShowViewCaption"].ToString()
                            + "\" BestFitColumns = \"" + dr["BestFitColumns"].ToString() + "\"/>";
                    }
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.LuuLuoiHienThi(strXml, User._UserID);

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
                    strXml += "<Grids ID = \"" + gridViewData.GetDataRow(i)["ID"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.XoaLuoiHienThi(strXml, User._UserID);

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
        #endregion

        #region Events
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_LuoiHienThi_Load(null, null);
        }

        private void btn_LuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btn_excel_Click(object sender, EventArgs e)
        {
            cms_excel.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void mnu_exportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Lưới hiển thị";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;

                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Lưới hiển thị";

                    gridControlData.ExportToXlsx(sfdFiles.FileName, options);

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

        private void mnu_importExcel_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Grd_ImportExcel f = new frm_Grd_ImportExcel();
                f.ShowDialog();
                DataTable dtExcelLoad = f._dtResult;

                if (dtExcelLoad.Columns.Count == 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataRow dr in dtExcelLoad.Rows)
                {
                    if (_dtData.Select("GridID = '" + dr["Mã lưới"].ToString() + "'").Length == 0)
                    {
                        DataRow drNew = _dtData.NewRow();

                        drNew["ModuleID"] = "GRD";
                        drNew["GridID"] = dr["Mã lưới"].ToString();
                        drNew["GridName"] = dr["Tên lưới"].ToString();

                        if (dr["Lọc dữ liệu"].ToString() == "Checked")
                            drNew["ShowAutoFilterRow"] = true;
                        else
                            drNew["ShowAutoFilterRow"] = false;

                        if (dr["Chọn nhiều dòng"].ToString() == "Checked")
                            drNew["MultiSelect"] = true;
                        else
                            drNew["MultiSelect"] = false;

                        if (dr["Kiểu chọn nhiều"].ToString() == "Chọn nhiều ô dữ liệu")
                            drNew["MultiSelectMode"] = "CELL";
                        else if (dr["Kiểu chọn nhiều"].ToString() == "Chọn nhiều dòng dữ liệu")
                            drNew["MultiSelectMode"] = "ROW";
                        else
                            drNew["MultiSelectMode"] = "CHECKBOX";

                        if (dr["Nhóm dữ liệu"].ToString() == "Checked")
                            drNew["ShowGroupPanel"] = true;
                        else
                            drNew["ShowGroupPanel"] = false;

                        if (dr["Vị trí dòng thêm mới"].ToString() == "Phía trên")
                            drNew["NewItemRowPosition"] = "TOP";
                        else if (dr["Vị trí dòng thêm mới"].ToString() == "Phía dưới")
                            drNew["NewItemRowPosition"] = "BOTTOM";
                        else
                            drNew["NewItemRowPosition"] = "NONE";

                        if (dr["Tự động canh độ rộng"].ToString() == "Checked")
                            drNew["ColumnAutoWidth"] = true;
                        else
                            drNew["ColumnAutoWidth"] = false;

                        if (dr["Thể hiện tên lưới"].ToString() == "Checked")
                            drNew["ShowViewCaption"] = true;
                        else
                            drNew["ShowViewCaption"] = false;

                        if (dr["Độ rộng cột theo dữ liệu"].ToString() == "Checked")
                            drNew["BestFitColumns"] = true;
                        else
                            drNew["BestFitColumns"] = false;

                        _dtData.Rows.Add(drNew);
                    }
                    else
                    {
                        DataRow drNew = (DataRow)_dtData.Select("GridID = '" + dr["Mã lưới"].ToString() + "'").GetValue(0);

                        drNew["GridName"] = dr["Tên lưới"].ToString();

                        if (dr["Lọc dữ liệu"].ToString() == "Checked")
                            drNew["ShowAutoFilterRow"] = true;
                        else
                            drNew["ShowAutoFilterRow"] = false;

                        if (dr["Chọn nhiều dòng"].ToString() == "Checked")
                            drNew["MultiSelect"] = true;
                        else
                            drNew["MultiSelect"] = false;

                        if (dr["Kiểu chọn nhiều"].ToString() == "Chọn nhiều ô dữ liệu")
                            drNew["MultiSelectMode"] = "CELL";
                        else if (dr["Kiểu chọn nhiều"].ToString() == "Chọn nhiều dòng dữ liệu")
                            drNew["MultiSelectMode"] = "ROW";
                        else
                            drNew["MultiSelectMode"] = "CHECKBOX";

                        if (dr["Nhóm dữ liệu"].ToString() == "Checked")
                            drNew["ShowGroupPanel"] = true;
                        else
                            drNew["ShowGroupPanel"] = false;

                        if (dr["Vị trí dòng thêm mới"].ToString() == "Phía trên")
                            drNew["NewItemRowPosition"] = "TOP";
                        else if (dr["Vị trí dòng thêm mới"].ToString() == "Phía dưới")
                            drNew["NewItemRowPosition"] = "BOTTOM";
                        else
                            drNew["NewItemRowPosition"] = "NONE";

                        if (dr["Tự động canh độ rộng"].ToString() == "Checked")
                            drNew["ColumnAutoWidth"] = true;
                        else
                            drNew["ColumnAutoWidth"] = false;

                        if (dr["Thể hiện tên lưới"].ToString() == "Checked")
                            drNew["ShowViewCaption"] = true;
                        else
                            drNew["ShowViewCaption"] = false;

                        if (dr["Độ rộng cột theo dữ liệu"].ToString() == "Checked")
                            drNew["BestFitColumns"] = true;
                        else
                            drNew["BestFitColumns"] = false;
                    }
                }

                XtraMessageBox.Show("Import thành công. Hãy kiểm tra và lưu dữ liệu.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("File excel không đúng cấu trúc cột: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}