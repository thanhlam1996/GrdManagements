using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Common.Grid;
using GrdCore.BLL;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_ThongTinSinhVien : Form
    {
        #region Variables
        public DataTable dtData = new DataTable();
        public bool isSubmit = false;
        public string _studentID = string.Empty;
        public string _studentName = string.Empty;
        public string _studyProgramID = string.Empty;
        public string _studyProgramName = string.Empty;
        public string _classStudentName = string.Empty;
        public string _studyStatusName = string.Empty;
        public string _BirthDay = string.Empty;
        public string _BirthPlace = string.Empty;

        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;
        #endregion

        #region Inits
        public frm_Grd_ThongTinSinhVien()
        {
            InitializeComponent();
        }

        private void frm_Grd_ThongTinSinhVien_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            try
            {
                DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                drGrids = (DataRow)dtGrid.Select("GridID = 'ThongTinSV'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());               
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm(this, typeof(frm_Grd_ChoThucThi), true, true, false);

                gridControlData.DataSource = dtData;
                AppGridView.ReadOnlyColumn(gridViewData);
                AppGridView.InitGridView(gridViewData, drGrids, _dtGridColumns, User._foreignLanguage);

                SplashScreenManager.CloseForm(false);
            }
            catch { SplashScreenManager.CloseForm(false); }
        }
        #endregion 

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        #endregion

        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridViewData.GetFocusedDataRow();

                _studentID = dr["StudentID"].ToString();
                _studentName = dr["StudentName"].ToString();
                _studyProgramID = dr["StudyProgramID"].ToString();
                _studyProgramName = dr["StudyProgramName"].ToString();
                _classStudentName = dr["ClassStudentName"].ToString();
                _studyStatusName = dr["StudyStatusName"].ToString();
                _BirthDay = dr["BirthDay"].ToString();
                _BirthPlace = dr["BirthPlace"].ToString();

                isSubmit = true;
                this.Close();
            }
            catch { }
        }
    }   
}
