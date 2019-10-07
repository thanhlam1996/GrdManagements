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

namespace GrdUI.InBang
{
    public partial class frm_Grd_TruongDuLieu : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataRow drGrids;
        #endregion

        #region Inits
        public frm_Grd_TruongDuLieu()
        {
            InitializeComponent();
        }

        private void frm_Grd_TruongDuLieu_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'TrgDuLieu'").GetValue(0);

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

                _dtData = BL_InBang.TruongDuLieuIn();

                _dtData.Columns["TenTruongDuLieuCu"].AllowDBNull = true;

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

                bool inBang = false, inChungChi = false;
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr["InBang"] == DBNull.Value)
                        inBang = false;
                    else
                        inBang = bool.Parse(dr["InBang"].ToString());

                    if (dr["InChungChi"] == DBNull.Value)
                        inChungChi = false;
                    else
                        inChungChi = bool.Parse(dr["InChungChi"].ToString());

                    strXml += "<TruongIn TenTruongDuLieu = \"" + dr["TenTruongDuLieu"].ToString()
                            + "\" DienGiai = \"" + dr["DienGiai"].ToString()
                            + "\" InBang = \"" + inBang.ToString()
                            + "\" InChungChi = \"" + inChungChi.ToString()
                            + "\" TenTruongDuLieuCu = \"" + dr["TenTruongDuLieuCu"].ToString()
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                int result = BL_InBang.LuuTruongDuLieuIn(strXml, User._UserID);
                if (result == 0)
                {
                    GetData();
                    XtraMessageBox.Show("Lưu thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Lưu không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (gridViewData.GetSelectedRows().Length == 0)
                {
                    XtraMessageBox.Show("Chưa có dữ liệu chọn để xóa.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string strXml = string.Empty;

                foreach (int i in gridViewData.GetSelectedRows())
                {
                    if (!(gridViewData.GetDataRow(i)["TenTruongDuLieuCu"] == DBNull.Value || gridViewData.GetDataRow(i)["TenTruongDuLieuCu"].ToString() == string.Empty))
                        strXml += "<TruongIn TenTruongDuLieu = \"" + gridViewData.GetDataRow(i)["TenTruongDuLieuCu"].ToString()
                            + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                int result = BL_InBang.XoaTruongDuLieuIn(strXml, User._UserID);
                if (result == 0)
                {
                    GetData();
                    XtraMessageBox.Show("Xóa thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Xóa không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_TruongDuLieu_Load(null, null);
        } 
        #endregion
    }
}