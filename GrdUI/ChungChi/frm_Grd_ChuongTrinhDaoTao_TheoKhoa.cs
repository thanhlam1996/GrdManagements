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
using DevExpress.Common.Grid;
using DevExpress.XtraSplashScreen;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_ChuongTrinhDaoTao_TheoKhoa : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public DataTable _dtData = new DataTable();
        public string _maCTDT = string.Empty;
        public bool _isSubmit = false;
        public string _tenCTDT = string.Empty, _Nganh = string.Empty, _QCDaoTao = string.Empty ;
        public decimal _STCBB = 0, _STCTC = 0;

        DataTable _dtGridColumns = new DataTable();
        DataRow drGrids;
        #endregion

        #region Inits
        public frm_Grd_ChuongTrinhDaoTao_TheoKhoa()
        {
            InitializeComponent();
        }

        private void frm_Grd_ChuongTrinhDaoTao_Load(object sender, EventArgs e)
        {
            try
            {
                #region Phân quyền
                CommonFunctions.SetFormPermiss(this);

                #region Định nghĩa lưới
                try
                {
                    DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                    drGrids = (DataRow)dtGrid.Select("GridID = 'CTDT'").GetValue(0);

                    _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(drGrids["ID"].ToString());
                }
                catch
                {
                    XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion
                #endregion

                GetData();
            }
            catch { }
        }
        #endregion 

        #region Functions
        private void GetData()
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm(this, typeof(frm_Grd_ChoThucThi), true, true, false);

                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridViewData,
                    new string[] { "StudyProgramID", "StudyProgramName", "OlogyID", "OlogyName", "StudyYears", "BeginDate", "RegulationID", "RegulationName", "STCBB", "STCTC" },
                    new string[] { "Mã CTĐT", "Tên CTĐT", "Mã ngành", "Tên ngành", "Số năm đào tạo", "Ngày bắt đầu", "Mã QC", "Tên QC", "STC bắc buộc", "STC tự chọn" }
                    , new int[] { 70,150,50,200,100,200,50,100,50,50 });
                AppGridView.AlignField(gridViewData, new string[] { "StudyProgramID", "OlogyID", "StudyYears", "BeginDate", "RegulationID", "STCBB", "STCTC" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.ReadOnlyColumn(gridViewData, new string[] {  "StudyProgramName", "OlogyID", "OlogyName", "StudyYears", "BeginDate", "RegulationID", "RegulationName", "STCBB", "STCTC" });

            }
            catch { SplashScreenManager.CloseForm(false); }
        }
        #endregion

        #region Events
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            _isSubmit = false;
            this.Close();
        }

        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _isSubmit = true;
                _maCTDT = gridViewData.GetFocusedDataRow()["StudyProgramID"].ToString();
                _tenCTDT = gridViewData.GetFocusedDataRow()["StudyProgramName"].ToString();
                _Nganh = gridViewData.GetFocusedDataRow()["OlogyID"].ToString();
                _QCDaoTao = gridViewData.GetFocusedDataRow()["RegulationID"].ToString();
                _STCBB = gridViewData.GetFocusedDataRow()["STCBB"].ToString() == string.Empty?0:(decimal.Parse(gridViewData.GetFocusedDataRow()["STCBB"].ToString()));
                _STCTC = gridViewData.GetFocusedDataRow()["STCTC"].ToString() == string.Empty ? 0 : (decimal.Parse(gridViewData.GetFocusedDataRow()["STCTC"].ToString()));

                this.Close();
            }
            catch (Exception ex) { }

        }

        private void btn_LamTuoi_Click(object sender, EventArgs e)
        {
            frm_Grd_ChuongTrinhDaoTao_Load(null, null);
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            GetData();
        }
        #endregion
    }
}