using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;

namespace GrdUI.HeThong
{
    public partial class frm_Grd_CotHienThi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public DataTable _dtData = new DataTable();

        public string _chucNang = string.Empty;

        public bool _isAccepted = false;
        #endregion

        #region Inits
        #region public frm_Grd_CotHienThi()
        public frm_Grd_CotHienThi()
        {
            InitializeComponent();
        }
        #endregion

        private void frm_Grd_CotHienThi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);
            #endregion

            GetData();
        }
        #endregion

        #region Functions
        private void GetData()
        {
            try
            {
                gridControlData.DataSource = _dtData;

                AppGridView.InitGridView(gridViewData, true, true, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);

                if (User._foreignLanguage)
                {
                    AppGridView.ShowField(gridViewData,
                        new string[] { "VisibleIndex", "ForeignColumnName", "Visible" },
                        new string[] { "VisibleIndex", "ColumnName", "Visible" },
                        new int[] { 100, 100, 100 });
                    AppGridView.ReadOnlyColumn(gridViewData, new string[] { "ForeignColumnName" });
                }
                else
                {
                    AppGridView.ShowField(gridViewData,
                        new string[] { "VisibleIndex", "ColumnName", "Visible" },
                        new string[] { "STT", "Tên cột hiển thị", "Hiển thị" },
                        new int[] { 100, 100, 100 });
                    AppGridView.ReadOnlyColumn(gridViewData, new string[] { "ColumnName" });
                }

                AppGridView.AlignField(gridViewData, new string[] { "VisibleIndex" }, DevExpress.Utils.HorzAlignment.Center);                

                gridViewData.Columns["VisibleIndex"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS -Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void SaveData()
        {
            try
            {
                string strXml = string.Empty;
                foreach (DataRow dr in _dtData.Rows)
                {
                    strXml += "<GridColumns ID = \"" + dr["ID"].ToString()
                                + "\" ColumnName = \"" + dr["ColumnName"].ToString()
                                + "\" ForeignColumnName = \"" + dr["ForeignColumnName"].ToString()
                                + "\" Visible = \"" + dr["Visible"].ToString()
                                + "\" VisibleIndex = \"" + dr["VisibleIndex"].ToString()
                                + "\" ReadOnly = \"" + dr["ReadOnly"].ToString()
                                + "\" Width = \"" + dr["Width"].ToString()
                                + "\" SummaryType = \"" + dr["SummaryType"].ToString()
                                + "\" HeaderAlign = \"" + dr["HeaderAlign"].ToString()
                                + "\" DataAlign = \"" + dr["DataAlign"].ToString()
                                + "\" Fixed = \"" + dr["Fixed"].ToString()
                                + "\" Sorted = \"" + dr["Sorted"].ToString()
                                + "\" OldID = \"" + dr["OldID"].ToString() + "\"/>";
                }
                strXml = "<Root>" + strXml + "</Root>";

                string result = BL_DoiTuongPhanQuyen.LuuCotLuoiHienThi(_dtData.Rows[0]["GridID"].ToString(), strXml, User._UserID);

                if (result.Contains("..."))
                {
                    GetData();
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(result, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region Events
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_LuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
            _isAccepted = true;
            this.Close();
        } 

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            frm_Grd_CotHienThi_Load(null, null);
        }

        private void btn_dongY_Click(object sender, EventArgs e)
        {
            _isAccepted = true;
            this.Close();
        } 
        #endregion
    }
}