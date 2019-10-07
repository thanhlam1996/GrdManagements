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
    public partial class frm_Grd_DongDauKyTen : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtData = new DataTable();
        public bool _loc = true, _kyThay = false, _dongY = false;
        public string _chuoiKyThay = string.Empty, _capBacNguoiKyTen = string.Empty, _hoVaTenNguoiKyTen = string.Empty;
        public string _chuoiKyThayEng = string.Empty, _capBacNguoiKyTenEng = string.Empty, _hoVaTenNguoiKyTenEng = string.Empty;
        string ID = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_DongDauKyTen()
        {
            InitializeComponent();            
        }

        private void frm_Grd_DongDauKyTen_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this); 
            #endregion

            ThongTinDongDauKyTen();
        }
        #endregion 

        #region Functions
        private void ThongTinDongDauKyTen()
        {
            try
            {
                _dtData = BL_DoiTuongPhanQuyen.ThongTinDongDauKyTen(User._UserID);
                _dtData.Columns.Add("IsCheck", typeof(bool));

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                foreach (DataRow dr in _dtData.Rows)
                    dr["IsCheck"] = false;

                gridViewData.Columns.Clear();
                gridControlData.DataSource = _dtData;

                #region Định nghĩa lưới
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                DataTable _dtGridColumns = new DataTable();
                DataRow _drGrids;
                try
                {
                    dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                    _drGrids = (DataRow)dtGrid.Select("GridID = 'SignStaffs'").GetValue(0);
                    _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
                }
                catch
                {
                    XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);

                if (_loc == false)
                {
                    layoutControlItem_luuDuLieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                    AppGridView.ReadOnlyColumn(gridViewData);
                }
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

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {          
            try
            {
                if (gridViewData.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Chưa chọn dữ liệu cần xóa. Vui lòng kiểm tra lại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strXml = string.Empty;
                foreach (int i in gridViewData.GetSelectedRows())
                {
                    strXml += "<ThongTin ID = \"" + gridViewData.GetDataRow(i)["ID"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.XoaThongTinDongDauKyTen(strXml, User._UserID);

                if (result.Contains("..."))
                {
                    frm_Grd_DongDauKyTen_Load(null, null);
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

        private void btn_LuuDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewData.SelectedRowsCount > 0)
                {
                    XtraMessageBox.Show("Đang xử lý chọn. Vui lòng kiểm tra lại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strXml = string.Empty;
                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Added)
                        strXml += "<ThongTin KyThay = \"" + dr["KyThay"].ToString()
                                    + "\" ChuoiKyThay = \"" + dr["ChuoiKyThay"].ToString()
                                    + "\" CapBacNguoiKyTen = \"" + dr["CapBacNguoiKyTen"].ToString()
                                    + "\" HoVaTenNguoiKyTen = \"" + dr["HoVaTenNguoiKyTen"].ToString()
                                    + "\" MacDinh = \"" + dr["MacDinh"].ToString()
                                    + "\" ID = \"" + dr["ID"].ToString()
                                    + "\" ChuoiKyThay_TA = \"" + dr["ChuoiKyThay_TA"].ToString()
                                    + "\" CapBacNguoiKyTen_TA = \"" + dr["CapBacNguoiKyTen_TA"].ToString()
                                    + "\" HoVaTenNguoiKy_TA = \"" + dr["HoVaTenNguoiKy_TA"].ToString()
                                    + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.LuuThongTinDongDauKyTen(strXml, User._UserID);

                if (result.Contains("..."))
                {
                    frm_Grd_DongDauKyTen_Load(null, null);
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

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DongDauKyTen_Load(null, null);
        }

        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            if (_loc == false)
            {
                try
                {
                    _kyThay = Convert.ToBoolean(gridViewData.GetFocusedDataRow()["KyThay"]);
                    _chuoiKyThay = gridViewData.GetFocusedDataRow()["ChuoiKyThay"].ToString();
                    _capBacNguoiKyTen = gridViewData.GetFocusedDataRow()["CapBacNguoiKyTen"].ToString();
                    _hoVaTenNguoiKyTen = gridViewData.GetFocusedDataRow()["HoVaTenNguoiKyTen"].ToString();

                    _chuoiKyThayEng = gridViewData.GetFocusedDataRow()["ChuoiKyThay_TA"].ToString();
                    _capBacNguoiKyTenEng = gridViewData.GetFocusedDataRow()["CapBacNguoiKyTen_TA"].ToString();
                    _hoVaTenNguoiKyTenEng = gridViewData.GetFocusedDataRow()["HoVaTenNguoiKy_TA"].ToString();
                    _dongY = true;
                    this.Close();
                }
                catch { }
            }
        } 

        private void gridViewData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MacDinh")
            {
                if (e.RowHandle >= 0)
                {
                    ID = gridViewData.GetFocusedDataRow()["ID"].ToString();

                    for (int i = 0; i < _dtData.Rows.Count; i++)
                    {
                        if (_dtData.Rows[i]["ID"].ToString() != ID)
                            _dtData.Rows[i]["MacDinh"] = false;
                        else
                            _dtData.Rows[i]["MacDinh"] = true;
                    }
                }
                else
                {
                    ID = gridViewData.GetFocusedDataRow()["ID"].ToString();

                    for (int i = 0; i < _dtData.Rows.Count; i++)
                        _dtData.Rows[i]["MacDinh"] = false;
                }
            }
        } 
        #endregion
    }
}