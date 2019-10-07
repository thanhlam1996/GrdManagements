using DevExpress.XtraEditors;
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
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_DanhMucLoaiChungChi : Form
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow _drGrids;
        #endregion

        #region Inits
        public frm_Grd_DanhMucLoaiChungChi()
        {
            InitializeComponent();
        }

        private void frm_Grd_DanhMucLoaiChungChi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'GRD_DMLoaiChungChi'").GetValue(0);

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
        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();

                _dtData = new DataTable();
                _dtData = BL_ChungChi.LoaiXet();

                _dtData.Columns["MaLoaiChungChi_Old"].AllowDBNull = true;

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                gridControlData.DataSource = _dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

                #region Hình thức cấp chứng chỉ, cấp bằng
                DataTable _tbNhomDiemDB = BL_ChungChi.LayHinhThucCap();

                repositoryItemLookUpEdit_HinhThucXet.DataSource = _tbNhomDiemDB;
                repositoryItemLookUpEdit_HinhThucXet.DisplayMember = "TenHinhThuc";
                repositoryItemLookUpEdit_HinhThucXet.ValueMember = "MaHinhThuc";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_HinhThucXet.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("MaHinhThuc", 50, "Mã hình thức"));
                coll.Add(new LookUpColumnInfo("TenHinhThuc", 100, "Tên hình thức"));

                repositoryItemLookUpEdit_HinhThucXet.NullText = string.Empty;
                repositoryItemLookUpEdit_HinhThucXet.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "MaHinhThuc", repositoryItemLookUpEdit_HinhThucXet);
                #endregion

                #region Lấy nhóm môn
                DataTable dtNhomMon = BL_ChungChi.LayNhomMon();

                repositoryItemCheckedComboBoxEdit_NhomMon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
                repositoryItemCheckedComboBoxEdit_NhomMon.DataSource = dtNhomMon;

                repositoryItemCheckedComboBoxEdit_NhomMon.ValueMember = "CurriculumGroupID";
                repositoryItemCheckedComboBoxEdit_NhomMon.DisplayMember = "CurriculumGroupName";

                repositoryItemCheckedComboBoxEdit_NhomMon.SeparatorChar = ';';

                AppGridView.RegisterControlField(gridViewData, "CurriculumGroupID", repositoryItemCheckedComboBoxEdit_NhomMon);
                #endregion
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
                        strXml += "<ChungChi MaLoaiChungChi = \"" + CommonFunctions.RefreshXmlString(dr["MaLoaiChungChi"].ToString())
                            + "\" TenLoaiChungChi = \"" + CommonFunctions.RefreshXmlString(dr["TenLoaiChungChi"].ToString())
                            + "\" MaHinhThuc = \"" + CommonFunctions.RefreshXmlString(dr["MaHinhThuc"].ToString())
                            + "\" MaLoaiChungChi_Old = \"" + CommonFunctions.RefreshXmlString(dr["MaLoaiChungChi_Old"].ToString())
                            + "\" GhiChu = \"" + CommonFunctions.RefreshXmlString(dr["Note"].ToString())
                            + "\" CurriculumGroupID  = \"" + CommonFunctions.RefreshXmlString(dr["CurriculumGroupID"].ToString()) + "\"/>";
                    }
                }
                if (strXml != "")
                {
                    strXml = "<Root>" + strXml + "</Root>";

                    int result = BL_ChungChi.LuuLoaiXet(strXml, User._UserID);

                    if (result == 0)
                    {
                        frm_Grd_DanhMucLoaiChungChi_Load(null, null);
                        XtraMessageBox.Show("Lưu dữ liệu thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (!(gridViewData.GetDataRow(i)["MaLoaiChungChi_Old"] == DBNull.Value || gridViewData.GetDataRow(i)["MaLoaiChungChi_Old"].ToString() == string.Empty))
                            strXml += "<ChungChi MaLoaiChungChi = \"" + gridViewData.GetDataRow(i)["MaLoaiChungChi_Old"].ToString() + "\"/>";
                    }
                    strXml = "<Root>" + strXml + "</Root>";

                    int result = BL_ChungChi.XoaLoaiXet(strXml, User._UserID);
                    if (result == 0)
                    {
                        GetData();
                        XtraMessageBox.Show("Xóa thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Xóa không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            frm_Grd_DanhMucLoaiChungChi_Load(null, null);
        }
        #endregion
       
    }
}
