using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_CapNhatThangXepLoai : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtThangXepLoai = new DataTable();
        public bool _isSubmit = false;
        public string _MaThangXepLoai = string.Empty;
        public bool _isNew = false;
        #endregion

        #region Inits
        public frm_Grd_CapNhatThangXepLoai()
        {
            InitializeComponent();
        } 

        private void frmCapNhatThangDiem_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }
	    #endregion

        #region Functions
        public void LoadData(string MaThangXepLoai)
        {
            try
            {
                _MaThangXepLoai = MaThangXepLoai;

                if (MaThangXepLoai != string.Empty)
                {
                    _dtThangXepLoai = BL_ChungChi.LayThangXepLoai(_MaThangXepLoai);

                    if (_dtThangXepLoai.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("Không lấy được dữ liệu.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }

                    DataRow dr = _dtThangXepLoai.Rows[0];
                    textEdit_MaThangXepLoai.Text = dr["MaThangXepLoai"].ToString();
                    textEdit_TenThangXepLoai.Text = dr["TenThangXepLoai"].ToString();
                }
            }
            catch { }
        }
        #endregion

        #region Events
        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEdit_MaThangXepLoai.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("Chưa nhập mã thang điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string MaThangXepLoai = textEdit_MaThangXepLoai.Text.Trim();
                string TenThangXepLoai = textEdit_TenThangXepLoai.Text.Trim();

                if (_isNew == true)
                    BL_ChungChi.CapNhatThangXepLoai(MaThangXepLoai, TenThangXepLoai, _MaThangXepLoai, "Ins", User._UserID);
                else
                    BL_ChungChi.CapNhatThangXepLoai(MaThangXepLoai, TenThangXepLoai, _MaThangXepLoai, "Upd", User._UserID);

                _MaThangXepLoai = MaThangXepLoai;
                XtraMessageBox.Show("Cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _isSubmit = true;
                this.Close();
            }
            catch (Exception ex)
            { 
                XtraMessageBox.Show("Cập nhật thất bại", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            _isSubmit = false;
            this.Close();
        }
        #endregion 
    }
}