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
using DevExpress.XtraEditors.Controls;

namespace GrdUI.InBang
{
    public partial class frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public int _stadyStatusID = 1;
        public bool _isAccepted = false;
        #endregion

        #region Inits
        public frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep()
        {
            InitializeComponent();
        }

        private void frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);
            #endregion

            GetStudyStatus();
        }
        #endregion

        #region Functions
        private void GetStudyStatus()
        {
            try
            {
                DataTable dtData = BL_InBang.GetStudyStatus();

                lookUpEditTinhTrang.Properties.DataSource = dtData;
                lookUpEditTinhTrang.Properties.DisplayMember = "StudyStatusName";
                lookUpEditTinhTrang.Properties.ValueMember = "StudyStatusID";

                LookUpColumnInfoCollection coll = lookUpEditTinhTrang.Properties.Columns;
                coll.Add(new LookUpColumnInfo("StudyStatusName", 0, "Tinh trạng"));

                lookUpEditTinhTrang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEditTinhTrang.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEditTinhTrang.Properties.AutoSearchColumnIndex = 0;

                lookUpEditTinhTrang.ItemIndex = 0;
            }
            catch { }
        }
        #endregion

        #region Events
        private void btnHuyQuyetDinh_Click(object sender, EventArgs e)
        {
            _isAccepted = true;
            _stadyStatusID = Convert.ToInt32(lookUpEditTinhTrang.EditValue.ToString());
            this.Close();
        }
        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            _isAccepted = false;
            this.Close();
        }
        #endregion
    }
}