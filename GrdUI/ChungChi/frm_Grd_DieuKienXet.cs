using DevExpress.Common.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
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

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_DieuKienXet : Form
    {
        #region Variables
        int _flagID = 0; // 1: Thêm mới; 2: Cập nhật

        DataTable _dtDieuKienXet = new DataTable();
        DataTable _dtLoaiChungChi = new DataTable();
        DataTable _dtXepLoai = new DataTable();
        DataTable _dtLoaiChungChiXet = new DataTable();
        DataTable _dtTinhTrang = new DataTable();
        string _MaDieuKien = string.Empty;

        DataSet dsDieuKienXet = new DataSet();
        DataTable _dtGridColumns = new DataTable();
        DataRow _drGrids;
        DataTable dtStudyStatus = new DataTable();
        #endregion

        #region Init
        public frm_Grd_DieuKienXet()
        {
            InitializeComponent();
        }

        private void frm_Grd_DieuKienXet_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới xếp loại
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'XepLoaiChiTiet'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            LoaiXet();
            GetRegulations();
            lookUpEdit_LoaiHinhXet_EditValueChanged(null, null);

        }
        #endregion

        #region Functions
        private void LoaiXet()
        {
            try
            {
                _dtLoaiChungChi = BL_ChungChi.LoaiXet();

                lookUpEdit_LoaiHinhXet.Properties.DataSource = _dtLoaiChungChi;

                lookUpEdit_LoaiHinhXet.Properties.DisplayMember = "TenLoaiChungChi";
                lookUpEdit_LoaiHinhXet.Properties.ValueMember = "MaLoaiChungChi";
                lookUpEdit_LoaiHinhXet.Properties.NullText = "";

                LookUpColumnInfoCollection col2 = lookUpEdit_LoaiHinhXet.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("TenLoaiChungChi", 0, "Loại chứng chỉ"));

                lookUpEdit_LoaiHinhXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_LoaiHinhXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_LoaiHinhXet.Properties.AutoSearchColumnIndex = 0;

                lookUpEdit_LoaiHinhXet.EditValue = "TN";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DieuKienXet()
        {
            try
            {
                _dtDieuKienXet = BL_ChungChi.DieuKienXet(lookUpEdit_LoaiHinhXet.EditValue==null?"null":lookUpEdit_LoaiHinhXet.EditValue.ToString());

                gridControlDK.DataSource = _dtDieuKienXet;

                AppGridView.InitGridView(gridViewDK, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridViewDK,
                    new string[] { "MaDieuKien", "TenDieuKien" },
                    new string[] { "Mã điều kiện", "Tên điều kiện" },
                    new int[] { 100, 100 });
                AppGridView.ReadOnlyColumn(gridViewDK);

                gridViewDK.OptionsView.ColumnAutoWidth = true;
                gridViewDK.BestFitColumns();
                DieuKienXetChiTiet();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region private void GetRegulations()

        private void GetRegulations()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = BL_ChungChi.GetRegulations();
                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "RegulationName ASC";
                lookUpEdit_QuyChe.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_QuyChe.Properties.DisplayMember = "RegulationName";
                lookUpEdit_QuyChe.Properties.ValueMember = "RegulationID";
                if (dtData.Rows.Count > 0)
                    lookUpEdit_QuyChe.EditValue = myDataView[0].Row[0].ToString();
                lookUpEdit_QuyChe.Properties.Columns.Clear();
                lookUpEdit_QuyChe.Properties.Columns.Add(new LookUpColumnInfo("RegulationID", 0, "Mã quy chế"));
                lookUpEdit_QuyChe.Properties.Columns.Add(new LookUpColumnInfo("RegulationName", 0, "Quy chế đào tạo"));
                lookUpEdit_QuyChe.Properties.AutoSearchColumnIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        string xml = string.Empty;
        private void DieuKienXetChiTiet()
        {
            try
            {                
                dsDieuKienXet = BL_ChungChi.DieuKienXetChiTiet(_MaDieuKien, lookUpEdit_LoaiHinhXet.EditValue == null ? "null" : lookUpEdit_LoaiHinhXet.EditValue.ToString());

                #region Điều kiện chi tiết
                DataTable dtDieuKienChiTiet = dsDieuKienXet.Tables["DieuKienXet"].Copy();

                if (dtDieuKienChiTiet.Rows.Count == 0)
                {
                    textBox_MaDK.Text = _MaDieuKien;
                    textBox_TenDK.Text = string.Empty;

                    radioGroup_ThangDiem.SelectedIndex = 0;
                    textEdit_DTB.Text = "5";
                    textEdit_MonNo.Text = "0";
                    textEdit_SoTCNo.Text = "0";
                    textEdit_PTSoTCNo.Text = "0";
                    textBox_HaBacCT.Text = "0";
                    textBox_HaBacHL.Text = "0";
                    checkEdit_KiemTraHocPhi.Checked = false;
                    checkEdit_KiemTraHoSo.Checked = false;
                    checkEdit_KiemTraThuVien.Checked = false;
                    textEdit_DiemRL.Text = "0";
                }
                else
                {
                    textBox_MaDK.Text = _MaDieuKien;
                    textBox_TenDK.Text = dtDieuKienChiTiet.Rows[0]["TenDieuKien"].ToString();

                    radioGroup_ThangDiem.SelectedIndex = Convert.ToInt16(dtDieuKienChiTiet.Rows[0]["ScoreSystem"].ToString());
                    textEdit_DTB.Text = dtDieuKienChiTiet.Rows[0]["DTB"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["DTB"].ToString()).ToString("0.##");
                    textEdit_MonNo.Text = dtDieuKienChiTiet.Rows[0]["SoMonNo"].ToString() == string.Empty ? "0.00" : Convert.ToInt32(dtDieuKienChiTiet.Rows[0]["SoMonNo"].ToString()).ToString("#");
                    textEdit_SoTCNo.Text = dtDieuKienChiTiet.Rows[0]["SoTCNo"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["SoTCNo"].ToString()).ToString("0.##");
                    textEdit_PTSoTCNo.Text = dtDieuKienChiTiet.Rows[0]["PhanTramSoTCNo"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["PhanTramSoTCNo"].ToString()).ToString("0.##");
                    textBox_HaBacCT.Text = dtDieuKienChiTiet.Rows[0]["HaBacCT"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["HaBacCT"].ToString()).ToString("0.##");
                    textBox_HaBacHL.Text = dtDieuKienChiTiet.Rows[0]["HaBacHL"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["HaBacHL"].ToString()).ToString("0.##");
                    checkEdit_KiemTraHocPhi.Checked = dtDieuKienChiTiet.Rows[0]["KiemTraHocPhi"].ToString() == string.Empty ? false : Convert.ToBoolean(dtDieuKienChiTiet.Rows[0]["KiemTraHocPhi"].ToString());
                    checkEdit_KiemTraHoSo.Checked = dtDieuKienChiTiet.Rows[0]["KiemTraHoSo"].ToString() == string.Empty ? false : Convert.ToBoolean(dtDieuKienChiTiet.Rows[0]["KiemTraHoSo"].ToString());
                    checkEdit_KiemTraThuVien.Checked = dtDieuKienChiTiet.Rows[0]["KiemTraNoSach"].ToString() == string.Empty ? false : Convert.ToBoolean(dtDieuKienChiTiet.Rows[0]["KiemTraNoSach"].ToString());
                    textEdit_DiemRL.Text = dtDieuKienChiTiet.Rows[0]["DiemRL"].ToString() == string.Empty ? "0.00" : Convert.ToDouble(dtDieuKienChiTiet.Rows[0]["DiemRL"].ToString()).ToString("0.##");

                    if (dtDieuKienChiTiet.Rows[0]["RegulationID"].ToString() == string.Empty)
                    {
                        checkEdit_ApDungQC.Checked = false;
                        lookUpEdit_QuyChe.ItemIndex = -1;
                    }
                    else
                    {
                        checkEdit_ApDungQC.Checked = true;
                        lookUpEdit_QuyChe.EditValue = dtDieuKienChiTiet.Rows[0]["RegulationID"].ToString();
                    }
                }
                #endregion

                #region Loại chứng chỉ, loại xét khác
                _dtLoaiChungChiXet = dsDieuKienXet.Tables["LoaiChungChi"].Copy();
                _dtLoaiChungChiXet.Columns["Xet"].ReadOnly = false;

                gridControlData.DataSource = _dtLoaiChungChiXet;

                AppGridView.InitGridView(gridViewData, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridViewData,
                    new string[] { "MaLoaiChungChi", "TenLoaiChungChi", "Xet" },
                    new string[] { "Mã loại chứng chỉ", "Tên loại chứng chỉ", "Xét" },
                    new int[] { 100, 100, 100 });
                AppGridView.ReadOnlyColumn(gridViewData, new string[] { "MaLoaiChungChi", "TenLoaiChungChi" });

                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
                #endregion

                #region Xếp loại chi tiết
                _dtXepLoai = dsDieuKienXet.Tables["ThangXepLoaiChiTiet"].Copy();
                _dtXepLoai.Columns["Delete"].ReadOnly = false;

                gridControlXepLoai.DataSource = _dtXepLoai;
                AppGridView.InitGridView(gridViewXepLoai, _drGrids, _dtGridColumns, User._foreignLanguage);

                gridViewXepLoai.Columns["LowerScore"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridViewXepLoai.Columns["LowerScore"].DisplayFormat.FormatString = "{0:0.00}";
                gridViewXepLoai.Columns["UpperScore"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                gridViewXepLoai.Columns["UpperScore"].DisplayFormat.FormatString = "{0:0.00}";

                AppGridView.RegisterControlField(gridViewXepLoai, "Delete", repositoryItemButtonEditXoaDiem);

                #region Xếp loại
                DataTable dtXepLoai = BL_ChungChi.XepLoai();
                DataView myDataView = new DataView(dtXepLoai);
                myDataView.Sort = "Rank asc, DefaultRankName asc";

                repositoryItemLookUpEdit_XepLoai.Properties.DataSource = myDataView.ToTable();
                repositoryItemLookUpEdit_XepLoai.Properties.DisplayMember = "DefaultRankName";
                repositoryItemLookUpEdit_XepLoai.Properties.ValueMember = "DefaultRankID";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_XepLoai.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("DefaultRankName", 0, "Xếp loại"));

                repositoryItemLookUpEdit_XepLoai.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                repositoryItemLookUpEdit_XepLoai.Properties.SearchMode = SearchMode.AutoComplete;
                repositoryItemLookUpEdit_XepLoai.Properties.NullText = "";

                AppGridView.RegisterControlField(gridViewXepLoai, "DefaultRankID", repositoryItemLookUpEdit_XepLoai);
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lock_Unlock_Control(bool _lockControl)
        {
            try
            {
                if (_flagID == 2)
                    textBox_MaDK.ReadOnly = true;
                else
                    textBox_MaDK.ReadOnly = _lockControl;

                textBox_TenDK.ReadOnly = _lockControl;
                radioGroup_ThangDiem.Properties.ReadOnly = _lockControl;
                textEdit_DTB.Properties.ReadOnly = _lockControl;
                textEdit_MonNo.Properties.ReadOnly = _lockControl;
                textEdit_SoTCNo.Properties.ReadOnly = _lockControl;
                textEdit_PTSoTCNo.Properties.ReadOnly = _lockControl;
                textBox_HaBacCT.ReadOnly = _lockControl;
                textBox_HaBacHL.ReadOnly = _lockControl;
                checkEdit_KiemTraHocPhi.Properties.ReadOnly = _lockControl;
                checkEdit_KiemTraHoSo.Properties.ReadOnly = _lockControl;
                checkEdit_KiemTraThuVien.Properties.ReadOnly = _lockControl;
                textEdit_DiemRL.Properties.ReadOnly = _lockControl;
                checkEdit_ApDungQC.Properties.ReadOnly = _lockControl;
                lookUpEdit_LoaiHinhXet.Enabled = _lockControl;

                if (_lockControl == true)
                {
                    AppGridView.ReadOnlyColumn(gridViewData);
                    AppGridView.ReadOnlyColumn(gridViewXepLoai);
                }
                else
                {
                    gridViewData.Columns["Xet"].OptionsColumn.AllowEdit = true;

                    foreach (GridColumn gc in gridViewXepLoai.Columns)
                        gc.OptionsColumn.AllowEdit = true;
                }

                simpleButton_CapNhat.Enabled = _lockControl;
                simpleButton_Them.Enabled = _lockControl;
                simpleButton_Delete.Enabled = _lockControl;
                btnHuy.Enabled = !_lockControl;
                btnLuuDuLieu.Enabled = !_lockControl;
            }
            catch (Exception ex) { }
        }

        private void DeleteData()
        {
            try
            {
                int KQ = BL_ChungChi.XoaDieuKien(_MaDieuKien);

                if (KQ == 0)
                {
                    XtraMessageBox.Show("Xóa thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lookUpEdit_LoaiHinhXet_EditValueChanged(null, null);
                    gridViewDK_FocusedRowChanged(null, null);
                }
                else if (KQ == 2)
                {
                    XtraMessageBox.Show("Không thể xóa điều kiện xét." + "\n" + "Điều kiện đã có trong chuẩn", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    XtraMessageBox.Show("Xóa không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                double haBacCT = 0, haBacHL = 0, diemTrungBinh = 0, soTCNo = 0, phanTramSoTCNo = 0, diemRenLuyen = 0;
                int soMonNo = 0;

                double.TryParse(textBox_HaBacCT.Text, out haBacCT);
                double.TryParse(textBox_HaBacHL.Text, out haBacHL);
                double.TryParse(textEdit_DTB.Text, out diemTrungBinh);
                double.TryParse(textEdit_SoTCNo.Text, out soTCNo);
                double.TryParse(textEdit_PTSoTCNo.Text, out phanTramSoTCNo);
                double.TryParse(textEdit_DiemRL.Text, out diemRenLuyen);

                string xmlDK = "<Root><Datas MaDieuKien = \"" + textBox_MaDK.Text
                    + "\" TenDieuKien = \"" + textBox_TenDK.Text
                    + "\" MaLoaiChungChi = \"" + lookUpEdit_LoaiHinhXet.EditValue.ToString()
                    + "\" ScoreSystem = \"" + radioGroup_ThangDiem.SelectedIndex.ToString()
                    + "\" HaBacCT = \"" + haBacCT.ToString()
                    + "\" HaBacHL = \"" + haBacHL.ToString()
                    + "\" DTB = \"" + diemTrungBinh.ToString()
                    + "\" SoMonNo = \"" + soMonNo.ToString()
                    + "\" SoTCNo = \"" + soTCNo.ToString()
                    + "\" PhanTramSoTCNo = \"" + phanTramSoTCNo.ToString()
                    + "\" DiemRL = \"" + diemRenLuyen.ToString()
                    + "\" KiemTraNoSach = \"" + checkEdit_KiemTraThuVien.Checked.ToString()
                    + "\" KiemTraHoSo = \"" + checkEdit_KiemTraHoSo.Checked.ToString()
                    + "\" KiemTraHocPhi = \"" + checkEdit_KiemTraHocPhi.Checked.ToString()
                    + "\" RegulationID = \"" + (checkEdit_ApDungQC.Checked == true ? lookUpEdit_QuyChe.EditValue.ToString() : "")
                    + "\"/></Root>";

                string xmlCC = string.Empty;
                foreach (DataRow drow in _dtLoaiChungChiXet.Rows)
                {
                    if (drow["Xet"].ToString().ToUpper() == "TRUE")
                        xmlCC += "<ChungChi MaLoaiChungChi = \"" + drow["MaLoaiChungChi"].ToString()
                            + "\" MaDieuKien = \"" + textBox_MaDK.Text
                            + "\"/>";
                }
                xmlCC = "<Root>" + xmlCC + "</Root>";

                string xmlXL = "<Root>";
                foreach (DataRow dr in _dtXepLoai.Rows)
                {
                    xmlXL += "<XepLoai DefaultRankID = \"" + dr["DefaultRankID"].ToString()
                        + "\" MaThangXepLoai = \"" + textBox_MaDK.Text
                        + "\" ScoreSystem = \"" + radioGroup_ThangDiem.SelectedIndex.ToString()
                        + "\" LowerScore = \"" + dr["LowerScore"].ToString()
                        + "\" UpperScore = \"" + dr["UpperScore"].ToString()
                        + "\" HaBac = \"" + dr["HaBac"].ToString() + "\"/>";
                }
                xmlXL += "</Root>";

                int KQ = BL_ChungChi.LuuDieuKien(xmlDK, xmlCC, xmlXL, User._UserID, _flagID);

                if (_flagID == 1)
                {
                    if (KQ == 0)
                    {
                        XtraMessageBox.Show("Thêm mới thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lookUpEdit_LoaiHinhXet_EditValueChanged(null, null);
                        int rowHandle = gridViewDK.LocateByValue("MaDieuKien", textBox_MaDK.Text);
                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            gridViewDK.FocusedRowHandle = rowHandle;
                    }
                    else
                    {
                        XtraMessageBox.Show("Thêm mới thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (KQ == 0)
                    {
                        XtraMessageBox.Show("Cập nhật thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        gridViewDK_FocusedRowChanged(null, null);
                    }
                    else if(KQ==1)
                    {
                        XtraMessageBox.Show("Không thể cập nhật\nĐã có sinh viên xét tốt nghiệp", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        gridViewDK_FocusedRowChanged(null, null);
                    }
                    else
                    {
                        XtraMessageBox.Show("cập nhật thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void simpleButton_Them_Click(object sender, EventArgs e)
        {
            _flagID = 1;

            gridControlDK.Enabled = false;
            _MaDieuKien = string.Empty;
            DieuKienXetChiTiet();
            Lock_Unlock_Control(false);            
        }

        private void simpleButton_CapNhat_Click(object sender, EventArgs e)
        {
            if (_MaDieuKien == string.Empty)
            {
                XtraMessageBox.Show("Chưa có điều kiện để cập nhật.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _flagID = 2;

            gridControlDK.Enabled = false;
            Lock_Unlock_Control(false);            
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();

            gridControlDK.Enabled = true;
            Lock_Unlock_Control(true);
            _flagID = 0;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            gridControlDK.Enabled = true;
            gridViewDK_FocusedRowChanged(null, null);
            Lock_Unlock_Control(true);
            _flagID = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frm_Grd_DieuKienXet_Load(null, null);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton_Delete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void lookUpEdit_LoaiHinhXet_EditValueChanged(object sender, EventArgs e)
        {
            DieuKienXet();
            gridViewDK_FocusedRowChanged(null, null);
        }

        private void gridViewDK_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewDK.DataRowCount == 0)
                    _MaDieuKien = string.Empty;
                else
                    _MaDieuKien = gridViewDK.GetFocusedDataRow()["MaDieuKien"].ToString();
                DieuKienXetChiTiet();
            }
            catch (Exception ex) { }
        }
        #endregion

        private void checkEdit_ApDungQC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_ApDungQC.Checked)
            {
                lookUpEdit_QuyChe.Enabled = true;
            }
            else
            {
                lookUpEdit_QuyChe.Enabled = false;
            }
        }
    }
}
