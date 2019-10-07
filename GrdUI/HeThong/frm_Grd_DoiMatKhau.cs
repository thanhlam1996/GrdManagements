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

namespace GrdUI.HeThong
{
    public partial class frm_Grd_DoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        #region Variables

        #endregion

        #region Inits
        public frm_Grd_DoiMatKhau()
        {
            InitializeComponent();
        }

        private void frm_Grd_DoiMatKhau_Load(object sender, EventArgs e)
        {
            try
            {
                txtNguoiDung.Text = User._User.StaffName;
                txtMatKhauCu.Focus();
            }
            catch { }
        }
        #endregion

        #region Functions
        public void DoiMatKhau()
        {
            try
            {
                string matKhauCu = txtMatKhauCu.Text.Trim();
                string matKhauMoi = txtMatKhauMoi.Text.Trim();
                string xacNhanLaiMatKhau = txtXacNhanMatKhau.Text.Trim();

                if (matKhauMoi != xacNhanLaiMatKhau)
                {
                    XtraMessageBox.Show("Mật khẩu mới nhập vào không trùng với mật khẩu xác nhận. Xin nhập lại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string result = BL_DecentralizationManagements.ChangePassword(User._UserID
                        , CommonFunctions.EncodeMD5(User._UserID, matKhauMoi)
                        , CommonFunctions.EncodeMD5(User._UserID, matKhauCu));

                    if (result.Contains("..."))
                    {
                        XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Events
        private void btnDongY_Click(object sender, EventArgs e)
        {
            DoiMatKhau();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}