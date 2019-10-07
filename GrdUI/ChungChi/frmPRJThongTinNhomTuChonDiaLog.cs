using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GrdCore.BLL;
using GrdUI;

namespace ProjectUI.LuanVan
{
    public partial class frmPRJThongTinNhomTuChonDiaLog : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public string _maChuan = string.Empty;
        public bool _setDel = false;
        public bool _Sua = false;
        public string _MaNhom = string.Empty;
        public string _TenNhom  = string.Empty;
        public string _NhomCha = string.Empty;
        string _NhomChaMoi = string.Empty;
        public decimal _SoTC = 0;

        #endregion

        #region Inits

        #region frmPRJThongTinNhomTuChonDiaLog()
        public frmPRJThongTinNhomTuChonDiaLog()
        {
            InitializeComponent();
        }
        #endregion

        #region void frmPRJThongTinNhomTuChonDiaLog_Load(object sender, EventArgs e)
        private void frmPRJThongTinNhomTuChonDiaLog_Load(object sender, EventArgs e)
        {
            GetGroupSelections();
            if (_Sua == true)
            {
                txtGroupID.Enabled = false;
                txtGroupID.Text = _MaNhom;
                lookUpEditParentID.EditValue = _NhomCha;
                txtGroupName.Text = _TenNhom;
                txtCredits.Text = Convert.ToString(_SoTC);
                _NhomChaMoi = _NhomCha;
            }

        }
        #endregion     

        #endregion

        #region Functions
        #region GetGroupSelections
        private void GetGroupSelections()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = BL_ChungChi.GetGroupSelections(_maChuan);
                DataRow row;
                row = dtData.NewRow();
                DataView myDataView = new DataView(dtData);
                //myDataView.Sort = "CourseName DESC";
                lookUpEditParentID.Properties.DataSource = myDataView.ToTable();
                lookUpEditParentID.Properties.DisplayMember = "SelectionParentName";
                lookUpEditParentID.Properties.ValueMember = "SelectionParentID";
                LookUpColumnInfoCollection coll = lookUpEditParentID.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("SelectionParentID", 0, "Mã nhóm"));
                coll.Add(new LookUpColumnInfo("SelectionParentName", 0, "Tên nhóm"));
                lookUpEditParentID.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditParentID.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditParentID.Properties.AutoSearchColumnIndex = -1;
                lookUpEditParentID.Properties.NullText = string.Empty;

            }
            catch { }
        }
        #endregion

        #region void _Close()
        private void _Close()
        {
            this.Close();
        }
        #endregion

        #region SaveData()
        private void SaveData()
        {
            try
            {
                bool result = false;
                bool _recommend = false;
                string strXml = "<Root>";

                if (txtGroupID.Text != "" && txtGroupName.Text != "" && txtCredits.Text != "")
                {

                    strXml += "<Datas GroupID = \"" + Convert.ToString(txtGroupID.Text.ToString().Trim()) +
                        "\" GroupName = \"" + Convert.ToString(txtGroupName.Text.ToString().Trim()) +
                        "\" Credits = \"" + Convert.ToString(txtCredits.Text.ToString().Trim()) +
                        "\" GroupParentID = \"" + _NhomChaMoi +
                        "\" GroupParentID_Old = \"" + _NhomCha +
                        "\" ChuanID = \"" + _maChuan +
                        "\"/>";
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng nhập đủ thông tin", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                strXml += "</Root>";
                if (txtGroupID.Text != "" && txtGroupName.Text != "" && txtCredits.Text != "")
                {
                    BL_ChungChi.SaveGroupSelections(strXml, User._User.StaffID);
                    result = true;
                }
                if (result == true)
                {
                    XtraMessageBox.Show("Lưu dữ liệu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmPRJThongTinNhomTuChonDiaLog_Load(null, null);
                }
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex )
            {
                XtraMessageBox.Show("Lưu dữ liệu không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion

        #region Events
        #region void btnSave_Click(object sender, EventArgs e)
        private void btnSave_Click(object sender, EventArgs e)
        {
            double KT;

            try
            {
                KT = Double.Parse(txtCredits.Text.ToString());
            }
            catch
            {
                XtraMessageBox.Show("Số tín chỉ không đúng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveData();
            this.Close();
        }
        #endregion      

        #region void btnClose_Click(object sender, EventArgs e)
        private void btnClose_Click(object sender, EventArgs e)
        {
            _Close();
        }
        #endregion
        
        #endregion          



        #region Short Keys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                _Close();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveData();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void lookUpEditParentID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _NhomChaMoi = lookUpEditParentID.EditValue.ToString();
            }
            catch
            {
                _NhomChaMoi = "";
            }
        }
    }
}