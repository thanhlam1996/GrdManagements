using DevExpress.Common.Grid;
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
    public partial class frm_Grd_ChonChuanXeCopy : Form
    {
        #region Variables
        public string _BacDT = string.Empty, _HeDT = string.Empty, _NamHoc = string.Empty, _HocKy = string.Empty, _DieuKien = string.Empty;
        public string _Chuan = string.Empty;
        DataTable _dtChuanXet = new DataTable();
        #endregion

        #region frm_Grd_ChonChuanXet_Load(object sender, EventArgs e)
        private void frm_Grd_ChonChuanXet_Load(object sender, EventArgs e)
        {
            LayKhoaHoc(_BacDT,_HeDT);
            LayNganhHoc();

            Getdata();
        }
        #endregion

        private void Getdata()
        {
            try
            {
                _dtChuanXet = BL_ChungChi.ChuanXetTheoKhoaNganh(checkedComboBoxEdit_KhoaHoc.EditValue.ToString(), checkedComboBoxEdit_NganhHoc.EditValue.ToString(), _DieuKien);

                _dtChuanXet.Columns["Chon"].ReadOnly = false;

                gridControlData.DataSource = _dtChuanXet;
                AppGridView.InitGridView(gridViewData, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                AppGridView.ShowField(gridViewData,
                    new string[] {"MaChuanXet", "TenChuanXet", "OlogyName", "CourseName", "STCTLBB", "STCTLTC" },
                    new string[] {"Mã chuẩn xét", "Tên chuẩn xét", "Ngành học", "Khóa học", "Số TC bắt buộc", "Số TC tự chọn" });
                AppGridView.AlignField(gridViewData, new string[] { "Chon", "MaChuanXet", "STCTLBB", "STCTLTC" },
                    DevExpress.Utils.HorzAlignment.Center);
                AppGridView.SummaryField(gridViewData, "MaChuanXet", "Tổng số chuẩn = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.ReadOnlyColumn(gridViewData, new string[] { "MaChuanXet", "TenChuanXet", "OlogyName", "CourseName", "STCTLBB", "STCTLTC" });
                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();

            }
            catch (Exception ex) { }

        }

        private void simpleButton_Thoat_Click(object sender, EventArgs e)
        {
            _Chuan = string.Empty;
            this.Close();
        }

        #region private void gridViewData_DoubleClick(object sender, EventArgs e)
        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _Chuan += gridViewData.GetFocusedDataRow()["MaChuanXet"].ToString();
                this.Close();
            }
            catch { }
        }
        #endregion

        private void LayKhoaHoc(string BacDT, string LHDT)
        {
            try
            {
                DataTable _dtKhoaHoc = BL_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(BacDT, LHDT);

                checkedComboBoxEdit_KhoaHoc.Properties.DataSource = _dtKhoaHoc;
                checkedComboBoxEdit_KhoaHoc.Properties.ValueMember = "CourseID";
                checkedComboBoxEdit_KhoaHoc.Properties.DisplayMember = "CourseName";

                checkedComboBoxEdit_KhoaHoc.Properties.SeparatorChar = ';';
                checkedComboBoxEdit_KhoaHoc.CheckAll();
            }
            catch { }
        }

        private void LayNganhHoc()
        {
            try
            {
                DataTable _dtNganhHoc = User._dsDataDictionaries.Tables["Ologies"].Copy();

                checkedComboBoxEdit_NganhHoc.Properties.DataSource = _dtNganhHoc;
                checkedComboBoxEdit_NganhHoc.Properties.ValueMember = "OlogyID";
                checkedComboBoxEdit_NganhHoc.Properties.DisplayMember = "OlogyName";

                checkedComboBoxEdit_NganhHoc.Properties.SeparatorChar = ';';
                checkedComboBoxEdit_NganhHoc.CheckAll();
            }
            catch { }
        }

        public frm_Grd_ChonChuanXeCopy()
        {
            InitializeComponent();
        }

        private void simpleButton_Loc_Click(object sender, EventArgs e)
        {
            Getdata();
        }
    }
}
