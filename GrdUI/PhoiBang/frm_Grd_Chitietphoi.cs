using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using DevExpress.XtraEditors.Controls;
using DevExpress.Common.Grid;

namespace GrdUI.PhoiBang
{
    public partial class frm_Grd_Chitietphoi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable(), _dtDataTypeDiplomas = new DataTable();
        DataRow _drGrids;
        string _ShipmentsID = string.Empty;
        string _Reason = string.Empty,_SerialNumberID=string.Empty;
        int _AutoID = 0, _PeriodOfGrantID=0;
        #endregion

        #region Inits
        public frm_Grd_Chitietphoi()
        {
            InitializeComponent();
        }
        private void frm_Grd_Chitietphoi_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);
            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
                _drGrids = (DataRow)dtGrid.Select("GridID = 'PhoiBang_ChitietPhoi'").GetValue(0);
                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion
           GetPeriodOfGrant();           
        }

        #endregion

        #region Functions
        private void AdjustSizeCol()
        {
            int size = gridControlData.Size.Width;
            int coutCol = _dtGridColumns.Rows.Count;
            for (int i = 0; i < coutCol; i++)
            {
                gridViewData.Columns[i].Width = size / coutCol;
            }
        }
        private void GetShipments()
        {
            try
            {
                DataTable _shipments = BL_PhoiBang.DanhSachPhoiJoinLoaiPhoi(_PeriodOfGrantID);            

                DataView myDataView = _shipments.DefaultView;
                myDataView.Sort = "ReceivedDate Desc";

                checkedComboBoxEdit_Danhmuclo.Properties.Items.Clear();     

                foreach (DataRow dr in _shipments.Rows)
                    checkedComboBoxEdit_Danhmuclo.Properties.Items.Add(dr["ShipmentsID"].ToString(), dr["ShipmentsID"].ToString()+" - ("+ dr["DiplomasTypeName"].ToString() + ")", CheckState.Checked, true);

                checkedComboBoxEdit_Danhmuclo.Properties.SeparatorChar = ';';
                checkedComboBoxEdit_Danhmuclo.CheckAll();
                checkedComboBoxEdit_Danhmuclo.Properties.DropDownRows = 7;

                //lookUpEdit_Mucphoibang.Properties.DataSource = myDataView.ToTable();
                //lookUpEdit_Mucphoibang.Properties.DisplayMember = "ShipmentsID";
                //lookUpEdit_Mucphoibang.Properties.ValueMember = "ShipmentsID";

                //LookUpColumnInfoCollection coll = lookUpEdit_Mucphoibang.Properties.Columns;
                //coll.Clear();
                //coll.Add(new LookUpColumnInfo("ShipmentsID", "Mã lô phôi"));
                //coll.Add(new LookUpColumnInfo("DiplomasTypeName", "Loại phôi"));
                //coll.Add(new LookUpColumnInfo("ReceivedDate", "Ngày nhận"));
                //coll.Add(new LookUpColumnInfo("Quantity", "Số lượng"));

                //lookUpEdit_Mucphoibang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                //lookUpEdit_Mucphoibang.Properties.SearchMode = SearchMode.AutoComplete;
                //lookUpEdit_Mucphoibang.Properties.AutoSearchColumnIndex = 0;
                //lookUpEdit_Mucphoibang.ItemIndex = 0;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetPeriodOfGrant()
        {
            try
            {
                DataTable _shipments = BL_PhoiBang.DanhMucDotCapPhoi();

                DataView myDataView = _shipments.DefaultView;
                myDataView.Sort = "DispatchDate Desc";

                lookUp_DotCapPhoi.Properties.DataSource = myDataView.ToTable();
                lookUp_DotCapPhoi.Properties.DisplayMember = "PeriodOfGrantName";
                lookUp_DotCapPhoi.Properties.ValueMember = "AutoID";

                LookUpColumnInfoCollection coll = lookUp_DotCapPhoi.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("PeriodOfGrantName", "Tên đợt cấp"));
                coll.Add(new LookUpColumnInfo("DispatchNumber", "Công văn số"));
                coll.Add(new LookUpColumnInfo("DispatchDate", "Ngày"));

                lookUp_DotCapPhoi.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUp_DotCapPhoi.Properties.SearchMode = SearchMode.AutoComplete;
                lookUp_DotCapPhoi.Properties.AutoSearchColumnIndex = 0;               
                lookUp_DotCapPhoi.ItemIndex = 0;
                _PeriodOfGrantID = int.Parse(lookUp_DotCapPhoi.EditValue.ToString());
                GetShipments();
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();
                _ShipmentsID = checkedComboBoxEdit_Danhmuclo.EditValue.ToString();// lookUpEdit_Mucphoibang.EditValue.ToString();
                _PeriodOfGrantID = int.Parse(lookUp_DotCapPhoi.EditValue.ToString());
                _dtData = BL_PhoiBang.DanhSachPhoiTheoLo(_PeriodOfGrantID,_ShipmentsID);

                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;               
                gridControlData.DataSource = _dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
                rdg_DisplayDiplomas.SelectedIndex = 0;

                gridViewData.Columns["ButtonCancel"].AppearanceCell.ForeColor = Color.Blue;
                gridViewData.Columns["ButtonCancel"].AppearanceCell.Font = new Font(gridViewData.Columns["ButtonCancel"].AppearanceCell.Font, FontStyle.Italic);
                gridViewData.Columns["ButtonCancel"].AppearanceCell.Font = new Font(gridViewData.Columns["ButtonCancel"].AppearanceCell.Font, FontStyle.Underline);


                DevExpress.XtraGrid.StyleFormatCondition cn_Null = new DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, gridViewData.Columns["Status"],null,-1);
                cn_Null.Appearance.ForeColor = Color.Black;
                cn_Null.Appearance.BackColor = Color.YellowGreen;
                cn_Null.ApplyToRow = true;
                gridViewData.FormatConditions.Add(cn_Null);
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

        private void lookUp_DotCapPhoi_EditValueChanged(object sender, EventArgs e)
        {
       
            checkedComboBoxEdit_Danhmuclo.Properties.Items.Clear();
            _PeriodOfGrantID = int.Parse(lookUp_DotCapPhoi.EditValue.ToString());
            GetShipments();                
        }

        private void rdg_DisplayDiplomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _choose = Int16.Parse(rdg_DisplayDiplomas.SelectedIndex.ToString());
            switch (_choose)
            {
                case 0: gridViewData.ActiveFilterString = "[Status] = '0' ADN [Status] = '1' AND [Status] = '-1'";//Tat ca
                    break;
                case 1: gridViewData.ActiveFilterString = "[Status] = '1'";//da cap
                    break;
                case 2: gridViewData.ActiveFilterString = "[Status] = '0'";//Chua cap
                    break;
                case 3: gridViewData.ActiveFilterString = "[Status] = '-1'";//da huy
                    break;
                default:
                    break;
            }                     
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetData();
            AdjustSizeCol();
        }

        private void gridViewData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(e.CellValue.ToString()=="Hủy phôi" || e.CellValue.ToString() == "Chi tiết")
            {
                _AutoID = int.Parse(_dtData.Rows[int.Parse(e.RowHandle.ToString())]["AutoID"].ToString());
                _Reason = _dtData.Rows[int.Parse(e.RowHandle.ToString())]["Reason"].ToString();
                _SerialNumberID = _dtData.Rows[int.Parse(e.RowHandle.ToString())]["SerialNumberID"].ToString();
                Dialog_reason f = new Dialog_reason(_Reason,_AutoID, _SerialNumberID);
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                GetData();
                AdjustSizeCol();
            }
            
        }
       

        private void btn_LocDuLieu_Click(object sender, EventArgs e)
        {
            GetData();
            AdjustSizeCol();
        }
        #endregion

       
    }
}