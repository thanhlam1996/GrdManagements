using DevExpress.XtraEditors;
using GrdCore.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace GrdUI.PhoiBang
{
    public partial class frm_Grd_DanhMucLoaiPhoiBang : Form
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow _drGrids;
        #endregion

        #region Inits
        public frm_Grd_DanhMucLoaiPhoiBang()
        {
            InitializeComponent();
        }

        private void frm_Grd_DanhMucLoaiPhoiBang_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
               // dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'Phoibang_DanhMucLoaiPhoiBang'").GetValue(0);
                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion
            GetData();
        }
        #endregion
        #region Functions
        private void AdjustSizeCol()
        {
            int size = gridControlData.Size.Width;
            int coutCol = _dtGridColumns.Rows.Count;
            for (int i = 0; i < coutCol; i++)
            {
                gridViewData.Columns[i].Width = size / coutCol;
            }
        }
        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();

                _dtData = new DataTable();
                _dtData = BL_PhoiBang.DanhMucLoaiPhoiBang();

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;   
                gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                gridControlData.DataSource = _dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
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
                        strXml += "<DiplomasType DiplomasTypeID = \"" + CommonFunctions.RefreshXmlString(dr["DiplomasTypeID"].ToString())
                            + "\" DiplomasTypeName = \"" + CommonFunctions.RefreshXmlString(dr["DiplomasTypeName"].ToString())                      
                            + "\" OldDiplomasTypeID = \"" + CommonFunctions.RefreshXmlString(dr["OldDiplomasTypeID"].ToString())
                            + "\" AutoID = \"" + CommonFunctions.RefreshXmlString(dr["AutoID"].ToString()) + "\"/>";
                    }
                }
                if (strXml != "")
                {
                    strXml = "<Root>" + strXml + "</Root>";

                    DataTable check = BL_PhoiBang.Check_DanhMucLoaiPhoi(strXml);
                    if (check.Rows.Count > 0)
                    {
                        string mes = string.Empty;
                        for (int i = 0; i < check.Rows.Count; i++)
                        {
                            mes += "Loại phôi " + check.Rows[i]["DiplomasTypeID"].ToString() + "- Không thể thay đổi thông tin!\n";
                        }
                        XtraMessageBox.Show(mes, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }
                    else
                    {
                        string result = BL_PhoiBang.Insert_DanhMucLoaiPhoi(strXml);
                        XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }
                  
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật dữ liệu trước khi lưu...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    if (XtraMessageBox.Show("Xóa dữ liệu đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    string strXml = string.Empty;
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        if (!(gridViewData.GetDataRow(i)["DiplomasTypeID"] == DBNull.Value || gridViewData.GetDataRow(i)["DiplomasTypeID"].ToString() == string.Empty))
                            strXml += "<DiplomasType AutoID = \"" + gridViewData.GetDataRow(i)["AutoID"].ToString() + "\"/>";
                    }
                    strXml = "<Root>" + strXml + "</Root>";

                    DataTable check = BL_PhoiBang.Check_DanhMucLoaiPhoi(strXml);
                    if (check.Rows.Count > 0)
                    {
                        string mes = string.Empty;
                        for (int i = 0; i < check.Rows.Count; i++)
                        {
                            mes += "Loại phôi " + check.Rows[i]["DiplomasTypeID"].ToString() + "- Không được xóa!\n";
                        }
                        XtraMessageBox.Show(mes, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }
                    else
                    {
                        string result = BL_PhoiBang.Delete_DanhMucLoaiPhoi(strXml);
                        XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }

                }

                else
                    XtraMessageBox.Show("Chưa chọn dữ liệu để xóa...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            GetData();
        }    

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Loại xét";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;

                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Loại xét";

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

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            frm_Grd_DanhMucLoaiPhoiBang_Load(null, null);
            AdjustSizeCol();
        }
        #endregion
       
    }
}
