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
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;

namespace GrdUI.InBang
{
    public partial class frm_Grd_QuanLyMauIn : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow drGrids;

        bool _mauBang = false;
        #endregion

        #region Inits
        public frm_Grd_QuanLyMauIn()
        {
            InitializeComponent();
        }

        private void frm_Grd_QuanLyMauIn_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'QLMauIn'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
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
                gridViewData.Columns.Clear();

                string maHinhThucCapChungChi = "CC";
                if (rdGrpLoaiMauIn.SelectedIndex == 0)
                {
                    _mauBang = false;
                    maHinhThucCapChungChi = "CC";
                }
                else
                {
                    _mauBang = true;
                    maHinhThucCapChungChi = "CB";
                }

                _dtData = BL_InBang.MauBangMauChungChi(_mauBang);
                _dtData.Columns.Add("DinhNghiaMau", typeof(string));

                foreach (DataColumn dc in _dtData.Columns)
                {
                    dc.ReadOnly = false;
                }

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                AppGridView.RegisterControlField(gridViewData, "DinhNghiaMau", repositoryItemButtonEditMauIn);

                #region Loại chứng chỉ
                DataTable dtLoaiChungChi = BL_InBang.LoaiChungChi_MaHinhThucCapChungChi(maHinhThucCapChungChi);

                repositoryItemLookUpEditLoaiChungChi.DataSource = dtLoaiChungChi;
                repositoryItemLookUpEditLoaiChungChi.DisplayMember = "TenLoaiChungChi";
                repositoryItemLookUpEditLoaiChungChi.ValueMember = "MaLoaiChungChi";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEditLoaiChungChi.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TenLoaiChungChi", "Tên chứng chỉ"));

                repositoryItemLookUpEditLoaiChungChi.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEditLoaiChungChi.Properties.SearchMode = SearchMode.AutoComplete;
                repositoryItemLookUpEditLoaiChungChi.Properties.AutoSearchColumnIndex = 0;
                repositoryItemLookUpEditLoaiChungChi.NullText = string.Empty;

                AppGridView.RegisterControlField(gridViewData, "MaLoaiChungChi", repositoryItemLookUpEditLoaiChungChi);
                #endregion

                #region Loại chứng chỉ
                if (rdGrpLoaiMauIn.SelectedIndex == 1)
                {
                    gridViewData.Columns["GraduationDegreeID"].Visible = true;
                    DataTable _dtDanhHieu = BL_ChungChi.DanhHieu();

                    repositoryItemLookUpEdit_DanhHieu.DataSource = _dtDanhHieu;
                    repositoryItemLookUpEdit_DanhHieu.DisplayMember = "GraduationDegreeName";
                    repositoryItemLookUpEdit_DanhHieu.ValueMember = "GraduationDegreeID";

                    LookUpColumnInfoCollection col2 = repositoryItemLookUpEdit_DanhHieu.Properties.Columns;
                    col2.Clear();
                    col2.Add(new LookUpColumnInfo("GraduationDegreeName", "Danh hiệu"));

                    repositoryItemLookUpEdit_DanhHieu.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                    repositoryItemLookUpEdit_DanhHieu.Properties.SearchMode = SearchMode.AutoComplete;
                    repositoryItemLookUpEdit_DanhHieu.Properties.AutoSearchColumnIndex = 0;
                    repositoryItemLookUpEdit_DanhHieu.NullText = string.Empty;

                    AppGridView.RegisterControlField(gridViewData, "GraduationDegreeID", repositoryItemLookUpEdit_DanhHieu);
                }
                else
                {
                    gridViewData.Columns["GraduationDegreeID"].Visible = false;
                }
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
                    if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                    {
                        strXml += "<MauChungChiMauBang MaMauIn = \"" + dr["MaMauIn"].ToString()
                                + "\" TenMauIn = \"" + dr["TenMauIn"].ToString()
                                + "\" MaLoaiChungChi = \"" + dr["MaLoaiChungChi"].ToString()
                                + "\" DienGiai = \"" + dr["DienGiai"].ToString()
                                + "\" KiHieuSVS = \"" + dr["KiHieuSVS"].ToString()
                                + "\" GraduationDegreeID = \"" + (rdGrpLoaiMauIn.SelectedIndex == 1 ? dr["GraduationDegreeID"].ToString() : string.Empty)
                                + "\" SuDung = \"" + dr["SuDung"].ToString()
                                + "\"/>";
                    }
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_InBang.LuuMauBangMauChungChi(strXml, User._UserID, _mauBang);

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
                        strXml += "<MauChungChiMauBang MaMauIn = \"" + gridViewData.GetDataRow(i)["MaMauIn"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_InBang.XoaMauBangMauChungChi(strXml, User._UserID, _mauBang);

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
        private void repositoryItemButtonEditMauIn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DataRow dr = gridViewData.GetFocusedDataRow();
                if (dr.RowState == DataRowState.Unchanged)
                {
                    frm_Grd_MauChungChi frm = new frm_Grd_MauChungChi();
                    frm._maMauIn = Convert.ToInt32(dr["MaMauIn"]);
                    frm._mauBang = _mauBang;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void rdGrpLoaiMauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_QuanLyMauIn_Load(null, null);
        }

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }


        private void simpleButton_CopyMau_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}