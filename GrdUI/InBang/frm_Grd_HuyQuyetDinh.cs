using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.Entities;
using System.IO;
using GrdCore.BLL;

namespace GrdUI.InBang
{
    public partial class frm_Grd_HuyQuyetDinh : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public Decisions _decisionInfos = new Decisions();
        public bool _isLinkFromStudentMarkDecisions = false, _submit = false;
        public int _linkFromStudentMarkDecisionsTypes = 0; // 0 : chỉ xem, 1 : Sửa, 2 : Thêm mới
        public int _decisionTypeID = 0;
        #endregion

        #region Inits
        public frm_Grd_HuyQuyetDinh()
        {
            InitializeComponent();
        }

        private void frm_Grd_HuyQuyetDinh_Load(object sender, EventArgs e)
        {            
            this.StartPosition = FormStartPosition.CenterScreen;

            #region Phân quyền
            CommonFunctions.SetFormPermiss(this); 
            #endregion
        }
        #endregion

        #region Functions
        //public void LoadData()
        //{
        //    if (_isLinkFromStudentMarkDecisions)
        //    {
        //        if (_decisionTypeID == 19)
        //            textEdit_LoaiQD.Text = "Quyết định tốt nghiệp";
        //        else if (_decisionTypeID == 191)
        //            textEdit_LoaiQD.Text = "Quyết định chứng chỉ";
        //        else if (_decisionTypeID == 192)
        //            textEdit_LoaiQD.Text = "Quyết định hoàn thành CTĐT";

        //        switch (_linkFromStudentMarkDecisionsTypes)
        //        {
        //            case 0: //chỉ xem
        //                {
        //                    textEdit_maQD.Text = _decisionInfos.DecisionNumber;
        //                    textEdit_maQD.Properties.ReadOnly = true;
        //                    txt_DecisionNumber.Text = _decisionInfos.DecisionAlias; 
        //                    txt_DecisionNumber.Properties.ReadOnly = true;
        //                    memoEdit_reason.Text = _decisionInfos.Reason; 
        //                    memoEdit_reason.Properties.ReadOnly = true;
        //                    txt_SignStaff.Text = _decisionInfos.SignStaff; 
        //                    txt_SignStaff.Properties.ReadOnly = true;

        //                    if (_decisionInfos.SignDate != string.Empty)
        //                        dateEdit_ngayKy.DateTime = CommonFunctions.GetDate(_decisionInfos.SignDate);
        //                    else
        //                        dateEdit_ngayKy.Text = string.Empty;

        //                    dateEdit_ngayKy.Enabled = false;
        //                    btn_luuDuLieu.Enabled = false;                           
        //                } break;

        //            case 1: //Sửa
        //                {
        //                    txt_DecisionNumber.Text = _decisionInfos.DecisionAlias;
        //                    textEdit_maQD.Text = _decisionInfos.DecisionNumber;
        //                    memoEdit_reason.Text = _decisionInfos.Reason;
        //                    txt_SignStaff.Text = _decisionInfos.SignStaff;

        //                    if (_decisionInfos.SignDate != string.Empty)
        //                        dateEdit_ngayKy.DateTime = CommonFunctions.GetDate(_decisionInfos.SignDate);
        //                    else
        //                        dateEdit_ngayKy.Text = string.Empty;

        //                    btn_luuDuLieu.Enabled = true;
        //                } break;

        //            case 2: //Thêm mới
        //                {
        //                    txt_DecisionNumber.Text = string.Empty;
        //                    memoEdit_reason.Text = string.Empty;
        //                    txt_SignStaff.Text = string.Empty;
        //                    dateEdit_ngayKy.Text = string.Empty;
        //                    btn_luuDuLieu.Enabled = true;
        //                } break;
        //        }
        //    }
        }
        #endregion

        #region Events
        //private void btn_luuDuLieu_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (_isLinkFromStudentMarkDecisions)
        //        {
        //            Decisions decisionInfo = new Decisions();
        //            decisionInfo.DecisionNumber = textEdit_maQD.Text.Trim();
        //            decisionInfo.DecisionAlias = txt_DecisionNumber.Text.Trim();
        //            decisionInfo.DecisionTypeID = _decisionTypeID;
        //            decisionInfo.IsInUsed = true;
        //            decisionInfo.Reason = memoEdit_reason.Text.Trim();
        //            decisionInfo.SignStaff = txt_SignStaff.Text.Trim();
        //            decisionInfo.UpdateStaff = User._UserID;

        //            if (dateEdit_ngayKy.Text == string.Empty)
        //                decisionInfo.SignDate = string.Empty;
        //            else
        //                decisionInfo.SignDate = dateEdit_ngayKy.DateTime.ToString("dd/MM/yyyy");

        //            int result = 0;
        //            if (_decisionInfos.DecisionNumber != string.Empty)
        //                result = decisionInfo.UpdateDecisions(_decisionInfos.DecisionNumber);
        //            else
        //                result = decisionInfo.InsertDecisions();

        //            _submit = true;

        //            this.Close();
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btn_thoat_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        #endregion
    //}
}