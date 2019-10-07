using System;
using DevExpress.XtraEditors;
using GrdCore.BLL;

namespace GrdUI.PhoiBang
{
    public partial class Dialog_reason : DevExpress.XtraEditors.XtraForm
    {
        frm_Grd_Chitietphoi f = new frm_Grd_Chitietphoi();
        string _reason = string.Empty, _SerialNumberID=string.Empty;
        int _AutoID = 0;
        bool isUpdate = true;
        public Dialog_reason(string txt,int dis,string SerialNumberID)
        {
            InitializeComponent();
            Getdata(txt);
            _AutoID = dis;
            _SerialNumberID = SerialNumberID;
        }

        #region Fucntion
        private void Getdata(string txt)
        {
            _reason = txt;
            rich_Reason.Text = txt;
            if(txt==string.Empty)
            {
                btn_Reuse.Enabled = false;
            }
        }
        #endregion
        private void btn_exits_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Reuse_Click(object sender, EventArgs e)
        {
            try
            {
                string _result = BL_PhoiBang.Update_SuDungLaiPhoiDaHuy(_AutoID, _SerialNumberID, User._UserID);
                XtraMessageBox.Show(_result, "Thông báo");
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Reuse.Enabled == false)//new
            {
                if (rich_Reason.Text == string.Empty) 
                {
                    XtraMessageBox.Show("Hãy nhập lý do hủy phôi bằng!", "UIS-Thông báo");
                }
                else
                {
                    isUpdate = false;
                }
            }
            else //Update
            {
                if (rich_Reason.Text == string.Empty)
                {
                    XtraMessageBox.Show("Lý do hủy phôi bằng không được bỏ trống!", "UIS-Thông báo");
                }
                else
                {
                    isUpdate = true;
                }
            }
            //UpdateStaff
            try
            {
                string _result = BL_PhoiBang.Update_ChiTietPhoi(rich_Reason.Text, isUpdate, _AutoID, _SerialNumberID, User._UserID);
                XtraMessageBox.Show(_result, "Thông báo");
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}