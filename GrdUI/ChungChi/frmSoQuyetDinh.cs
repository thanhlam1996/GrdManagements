using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using System.Globalization;
using GrdUI;
using GrdUI.HeThong;
using GrdUI.PhoiBang;

namespace ScrUI.Others
{
    public partial class frmSoQuyetDinh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public string _ngayQD = string.Empty, _ngayQDDS = string.Empty, _soQD = string.Empty, _capBac = string.Empty
            , _capBacEng=string.Empty, _hoVaTen = string.Empty, _hoVaTenEng = string.Empty, _ngayQDEng = string.Empty;
        public bool _dongY = false;
        public bool _quyetDinh = true;
        public bool _ngayQuyetDinh = true;
        public string _ngay = string.Empty;
        public string _ngayKhongTinhThanh = string.Empty;
        public bool _nguoiDongDauKyTen = true;
        public string _NguoiLap = string.Empty;
        public bool _vietHoa = true;
        public String _TenPhong = string.Empty, _TenNgkyTheoPhong = string.Empty;
        public string _ngayThi = string.Empty;
        public bool _NgayThi = false;
        #endregion

        #region Inits
        #region public frmSoQuyetDinh()
        public frmSoQuyetDinh()
        {
            InitializeComponent();
        }
        #endregion
        frm_Grd_DanhMucPhoiBang f = new frm_Grd_DanhMucPhoiBang();
       
        #region private void frmSoQuyetDinh_Load(object sender, EventArgs e)
        private void frmSoQuyetDinh_Load(object sender, EventArgs e)
        {
            CommonFunctions.SetFormPermiss(this);

            DataRow[] drSelect = User._dsDataDictionaries.Tables[0].Select("SettingName = 'TinhThanh'");
            if (drSelect.Length != 0)
            {
                textEdit_ngayQD.Text = drSelect[0]["SettingStringData"].ToString();
            }

            if (_soQD == string.Empty)
            {
                DataRow[] drSelect1 = User._dsDataDictionaries.Tables[0].Select("SettingName = 'SoQuyetDinh'");
                if (drSelect1.Length != 0)
                {
                    textEdit_soQuyetDinh.Text = drSelect1[0]["SettingStringData"].ToString();
                }
            }
            else
                textEdit_soQuyetDinh.Text = _soQD;

            if (_quyetDinh == false)
            {
                layoutControlItem_soQuyetDinh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (_ngayQuyetDinh == false)
                {
                    layoutControlItem_ngayQuyetDinh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem_diaDanh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem_vietHoa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    dateEdit_ngayQuyetDinh.DateTime = DateTime.Now;
                    checkEdit_vietHoaNgay.Checked = _vietHoa;
                    dateEdit_ngayThi.DateTime = DateTime.Now;
                }
            }
            else
            {
                dateEdit_ngayQuyetDinh.DateTime = DateTime.Now;
                checkEdit_vietHoaNgay.Checked = _vietHoa;
                dateEdit_ngayThi.DateTime = DateTime.Now;
            }

            if (_nguoiDongDauKyTen == false)
            {
                layoutControlItem_capBac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_capBacEng.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_check_kyThay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_kyThay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_hoTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            textEdit_capBac.Text = _capBac;
            textEdit_capBacEng.Text = _capBacEng;

            try
            {
                DataTable _dtData_KT = new DataTable();
                _dtData_KT = BL_DoiTuongPhanQuyen.ThongTinDongDauKyTen(User._UserID);
                DataRow drow = (DataRow)_dtData_KT.Select("MacDinh = 1").GetValue(0);

                checkEdit_kyThay.Checked = Convert.ToBoolean(drow["KyThay"]);
                textEdit_kyThay.Text = drow["ChuoiKyThay"].ToString();
                textEdit_capBac.Text = drow["CapBacNguoiKyTen"].ToString();
                textEdit_capBacEng.Text = drow["CapBacNguoiKyTen_TA"].ToString();
                textEdit_hoVaTen.Text = drow["HoVaTenNguoiKyTen"].ToString();
                textEdit_hoVaTenEng.Text = drow["HoVaTenNguoiKyTen_TA"].ToString();
            }
            catch
            {

            }
            if(_NgayThi==false)
                layoutControlItem_NgayThi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            else
                layoutControlItem_NgayThi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

        }
        #endregion 
        #endregion

        #region Events
        #region private void btn_thoat_Click(object sender, EventArgs e)
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region private void btn_dongY_Click(object sender, EventArgs e)
        private void btn_dongY_Click(object sender, EventArgs e)
        {
            _dongY = true;

            if (textEdit_ngayQD.Text.Trim() != string.Empty)
            {
                if (dateEdit_ngayQuyetDinh.Text != string.Empty)
                {
                    _ngayQD = textEdit_ngayQD.Text.Trim() + ", ngày " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("dd")
                        + " tháng " + (dateEdit_ngayQuyetDinh.DateTime.Month > 3 ? dateEdit_ngayQuyetDinh.DateTime.Month.ToString() : dateEdit_ngayQuyetDinh.DateTime.Date.ToString("MM"))
                        + " năm " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("yyyy");
                    _ngayQDDS = "ngày " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("dd")
                        + " tháng " + (dateEdit_ngayQuyetDinh.DateTime.Month > 3 ? dateEdit_ngayQuyetDinh.DateTime.Month.ToString() : dateEdit_ngayQuyetDinh.DateTime.Date.ToString("MM"))
                        + " năm " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("yyyy");
                }
                else
                {
                    _ngayQD = textEdit_ngayQD.Text.Trim() + ", ngày    tháng     năm      .";
                }

            }
            else
            {
                if (checkEdit_vietHoaNgay.Checked)
                    _ngayQD = "Ngày ";
                else
                    _ngayQD = "ngày ";

                if (dateEdit_ngayQuyetDinh.Text != string.Empty)
                {
                    _ngayQD += dateEdit_ngayQuyetDinh.DateTime.Date.ToString("dd")
                        + " tháng " + (dateEdit_ngayQuyetDinh.DateTime.Month > 3 ? dateEdit_ngayQuyetDinh.DateTime.Month.ToString() : dateEdit_ngayQuyetDinh.DateTime.Date.ToString("MM"))
                        + " năm " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("yyyy");
                }
                else
                {
                    _ngayQD += "   tháng     năm      .";
                }
            }

            if (checkEdit_vietHoaNgay.Checked)
                _ngayKhongTinhThanh = "Ngày ";
            else
                _ngayKhongTinhThanh = "ngày ";

            if (dateEdit_ngayQuyetDinh.Text != string.Empty)
            {
                _ngayKhongTinhThanh += dateEdit_ngayQuyetDinh.DateTime.Date.ToString("dd")
                    + " tháng " + (dateEdit_ngayQuyetDinh.DateTime.Month > 3 ? dateEdit_ngayQuyetDinh.DateTime.Month.ToString() : dateEdit_ngayQuyetDinh.DateTime.Date.ToString("MM"))
                    + " năm " + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("yyyy");
            }
            else
                _ngayKhongTinhThanh += "   tháng     năm      .";


            #region Ngày tiếng anh
            _ngayQDEng = "Ho Chi Minh City, " + dateEdit_ngayQuyetDinh.DateTime.ToString("MMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            #endregion


            if (dateEdit_ngayQuyetDinh.Text != string.Empty)
            {
                _ngay = dateEdit_ngayQuyetDinh.DateTime.ToString("dd/MM/yyyy");
            }
            else
                _ngay = string.Empty;

            _soQD = textEdit_soQuyetDinh.Text;

            if (checkEdit_kyThay.Checked)
            {
                _capBac = textEdit_kyThay.Text.Replace("#", "\r\n") + "\n" + textEdit_capBac.Text;
                _capBacEng = textEdit_kyThayEng.Text.Replace("#", "\r\n") + "\n" + textEdit_capBacEng.Text;
            }
            else
            {
                _capBac = textEdit_capBac.Text;
                _capBacEng = textEdit_capBacEng.Text;
            }
            _hoVaTen = textEdit_hoVaTen.Text;
            _hoVaTenEng = textEdit_hoVaTenEng.Text;
            _NguoiLap = txt_NguoiLap.Text;

            if (dateEdit_ngayThi.Text != string.Empty)
            {
                _ngayThi = dateEdit_ngayQuyetDinh.DateTime.Date.ToString("dd")
                + "/" + (dateEdit_ngayQuyetDinh.DateTime.Month > 3 ? dateEdit_ngayQuyetDinh.DateTime.Month.ToString() : dateEdit_ngayQuyetDinh.DateTime.Date.ToString("MM"))
                + "/" + dateEdit_ngayQuyetDinh.DateTime.Date.ToString("yyyy");

            }
            else
                _ngayThi = string.Empty;
                
            this.Close();
        }
        #endregion

        #region private void btn_nguoiKyTen_Click(object sender, EventArgs e)
        private void btn_nguoiKyTen_Click(object sender, EventArgs e)
        {
            frm_Grd_DongDauKyTen frm = new frm_Grd_DongDauKyTen();
            frm._loc = false;
            frm.ShowDialog();

            if (frm._dongY == true)
            {
                checkEdit_kyThay.Checked = frm._kyThay;
                textEdit_kyThay.Text = frm._chuoiKyThay;
                textEdit_capBac.Text = frm._capBacNguoiKyTen;                
                textEdit_hoVaTen.Text = frm._hoVaTenNguoiKyTen;
                textEdit_kyThayEng.Text = frm._chuoiKyThayEng;
                textEdit_capBacEng.Text = frm._capBacNguoiKyTenEng;
                textEdit_hoVaTenEng.Text = frm._hoVaTenNguoiKyTenEng;
            }
        }
        #endregion

      
        #endregion
    }
}