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

namespace GrdUI.HeThong
{
    public partial class frm_Grd_CotLuoiHienThi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable();
        string _luoiHienThi = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_CotLuoiHienThi()
        {
            InitializeComponent();
        }

        private void frm_Grd_CotLuoiHienThi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            btnSave.Enabled = (User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM");
            btnDelete.Enabled = (User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM");
            #endregion

            LuoiHienThi();
            lookUpEdit_luoiHienThi.ItemIndex = 0;
        }
        #endregion

        #region Functions
        private void LuoiHienThi()
        {
            try
            {
                DataTable _dtLuoiHienThi = BL_DoiTuongPhanQuyen.LuoiHienThi();

                DataView myDataView = _dtLuoiHienThi.DefaultView;
                myDataView.Sort = "GridName ASC";

                lookUpEdit_luoiHienThi.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_luoiHienThi.Properties.DisplayMember = "GridName";
                lookUpEdit_luoiHienThi.Properties.ValueMember = "ID";

                LookUpColumnInfoCollection coll = lookUpEdit_luoiHienThi.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GridID", "Mã lưới"));
                coll.Add(new LookUpColumnInfo("GridName", "Tên lưới"));

                lookUpEdit_luoiHienThi.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_luoiHienThi.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_luoiHienThi.Properties.AutoSearchColumnIndex = 0;
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
                gridViewData.Columns.Clear();

                _luoiHienThi = lookUpEdit_luoiHienThi.EditValue.ToString();

                _dtData = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_luoiHienThi);

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                _dtData.Columns["OldID"].AllowDBNull = true;
                _dtData.Columns["GridID"].AllowDBNull = true;

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = !(User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM");

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect, false, false, "Nhấn vào đây để thêm mới");
                AppGridView.ShowField(gridViewData,
                    new string[] { "ID", "ColumnName", "Visible", "VisibleIndex", "ReadOnly"
                        , "Width", "SummaryType", "HeaderAlign", "DataAlign", "Fixed", "Sorted" },
                    new string[] { "Mã cột", "Tên cột", "Hiển thị", "Thứ tự", "Không chỉnh sửa"
                        , "Độ rộng", "Thống kê", "Canh lề tiêu đề", "Canh lề dữ liệu", "Ghim cột", "Cho sắp xếp" });
                AppGridView.AlignField(gridViewData, new string[] { "VisibleIndex", "Width" }, DevExpress.Utils.HorzAlignment.Center);

                #region SummaryType
                DataTable dtSummaryType = new DataTable();
                dtSummaryType.Columns.Add("ID", typeof(string));
                dtSummaryType.Columns.Add("Description", typeof(string));

                dtSummaryType.Rows.Add("COUNT", "Đếm số lượng");
                dtSummaryType.Rows.Add("SUM", "Tính tổng");
                dtSummaryType.Rows.Add("AVERAGE", "Trung bình");
                dtSummaryType.Rows.Add("MIN", "Nhỏ nhất");
                dtSummaryType.Rows.Add("MAX", "Lớn nhất");
                dtSummaryType.Rows.Add("NONE", "Mặc định");

                repositoryItemLookUpEditSummaryType.DataSource = dtSummaryType;
                repositoryItemLookUpEditSummaryType.DisplayMember = "Description";
                repositoryItemLookUpEditSummaryType.ValueMember = "ID";
                repositoryItemLookUpEditSummaryType.NullText = string.Empty;

                LookUpColumnInfoCollection coll = repositoryItemLookUpEditSummaryType.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditSummaryType.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditSummaryType.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "SummaryType", repositoryItemLookUpEditSummaryType);
                #endregion

                #region Align
                DataTable dtAlign = new DataTable();
                dtAlign.Columns.Add("ID", typeof(string));
                dtAlign.Columns.Add("Description", typeof(string));

                dtAlign.Rows.Add("CENTER", "Canh giữa");
                dtAlign.Rows.Add("FAR", "Canh phải");
                dtAlign.Rows.Add("NEAR", "Canh trái");
                dtAlign.Rows.Add("DEFAULT", "Mặc định");

                repositoryItemLookUpEditAlignHeader.DataSource = dtAlign;
                repositoryItemLookUpEditAlignHeader.DisplayMember = "Description";
                repositoryItemLookUpEditAlignHeader.ValueMember = "ID";
                repositoryItemLookUpEditAlignHeader.NullText = string.Empty;

                LookUpColumnInfoCollection collH = repositoryItemLookUpEditAlignHeader.Columns;
                collH.Clear();
                collH.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditAlignHeader.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditAlignHeader.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "HeaderAlign", repositoryItemLookUpEditAlignHeader);


                repositoryItemLookUpEditAlignData.DataSource = dtAlign;
                repositoryItemLookUpEditAlignData.DisplayMember = "Description";
                repositoryItemLookUpEditAlignData.ValueMember = "ID";
                repositoryItemLookUpEditAlignData.NullText = string.Empty;

                LookUpColumnInfoCollection collD = repositoryItemLookUpEditAlignData.Columns;
                collD.Clear();
                collD.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditAlignData.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditAlignData.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "DataAlign", repositoryItemLookUpEditAlignData);
                #endregion

                #region Fixed
                DataTable dtFixed = new DataTable();
                dtFixed.Columns.Add("ID", typeof(string));
                dtFixed.Columns.Add("Description", typeof(string));

                dtFixed.Rows.Add("LEFT", "Bên trái");
                dtFixed.Rows.Add("RIGHT", "Bên phải");
                dtFixed.Rows.Add("NONE", "Mặc định");

                repositoryItemLookUpEditFixed.DataSource = dtFixed;
                repositoryItemLookUpEditFixed.DisplayMember = "Description";
                repositoryItemLookUpEditFixed.ValueMember = "ID";
                repositoryItemLookUpEditFixed.NullText = string.Empty;

                LookUpColumnInfoCollection collF = repositoryItemLookUpEditFixed.Columns;
                collF.Clear();
                collF.Add(new LookUpColumnInfo("Description", "Diễn giải"));

                repositoryItemLookUpEditFixed.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditFixed.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "Fixed", repositoryItemLookUpEditFixed);
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
                    gridViewData.DeleteSelectedRows();

                    if (dr.RowState != DataRowState.Deleted)
                    {
                        strXml += "<GridColumns ID = \"" + dr["ID"].ToString()
                                + "\" ColumnName = \"" + dr["ColumnName"].ToString()
                                + "\" ForeignColumnName = \"" + dr["ForeignColumnName"].ToString()
                                + "\" Visible = \"" + dr["Visible"].ToString()
                                + "\" VisibleIndex = \"" + dr["VisibleIndex"].ToString()
                                + "\" ReadOnly = \"" + dr["ReadOnly"].ToString()
                                + "\" Width = \"" + dr["Width"].ToString()
                                + "\" SummaryType = \"" + dr["SummaryType"].ToString()
                                + "\" HeaderAlign = \"" + dr["HeaderAlign"].ToString()
                                + "\" DataAlign = \"" + dr["DataAlign"].ToString()
                                + "\" Fixed = \"" + dr["Fixed"].ToString()
                                + "\" Sorted = \"" + dr["Sorted"].ToString()
                                + "\" OldID = \"" + dr["OldID"].ToString() + "\"/>";
                    }
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.LuuCotLuoiHienThi(_luoiHienThi, strXml, User._UserID);

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
                    if (!(gridViewData.GetDataRow(i)["OldID"] == DBNull.Value || gridViewData.GetDataRow(i)["OldID"].ToString() == string.Empty))
                        strXml += "<GridColumns ID = \"" + gridViewData.GetDataRow(i)["OldID"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.XoaCotLuoiHienThi(_luoiHienThi, strXml, User._UserID);

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

        private void btn_LuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void bbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_CotLuoiHienThi_Load(null, null);
        }

        private void btn_LocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
        }
        #endregion
    }
}