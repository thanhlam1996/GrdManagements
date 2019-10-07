using DevExpress.XtraEditors;
using GrdCore.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace GrdUI.PhoiBang
{
    public partial class frm_Grd_CauHinhLoaiPhoi_Nganh : Form
    {
        #region Variables
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable();
        DataTable _dtDataTypeDiplomas = new DataTable();
        DataRow _drGrids;
        string UpdateStaff = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_CauHinhLoaiPhoi_Nganh()
        {
            InitializeComponent();
        }
        private void frm_Grd_CauHinhLoaiPhoi_Nganh_Load(object sender, EventArgs e)
        {
            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
                //dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'Phoibang_Cauhinhloaiphoi_nganh'").GetValue(0);
                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            GetData();
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
        private void GetData()
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();

                _dtData = new DataTable();
                _dtData = BL_PhoiBang.Get_CauHinhLoaiPhoi_Nganh();        
                foreach (DataColumn dc in _dtData.Columns)
                    dc.ReadOnly = false;

                gridControlData.DataSource = _dtData;              
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);


                //Phoibang_Cauhinhloaiphoi_nganh
                #region Danh mục loại phôi
                _dtDataTypeDiplomas = BL_PhoiBang.DanhMucLoaiPhoiBang();
                _dtDataTypeDiplomas.Rows.Add("-1","-1", "Không chọn");
                repositoryItemLookUpEdit_DanhMucLoaiPhoi.DataSource = _dtDataTypeDiplomas;
                repositoryItemLookUpEdit_DanhMucLoaiPhoi.DisplayMember = "DiplomasTypeName";
                repositoryItemLookUpEdit_DanhMucLoaiPhoi.ValueMember = "DiplomasTypeID";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_DanhMucLoaiPhoi.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("DiplomasTypeID", 50, "Mã loại"));
                coll.Add(new LookUpColumnInfo("DiplomasTypeName", 100, "Tên Loại phôi"));

                repositoryItemLookUpEdit_DanhMucLoaiPhoi.NullText = string.Empty;
                repositoryItemLookUpEdit_DanhMucLoaiPhoi.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "DiplomasTypeID", repositoryItemLookUpEdit_DanhMucLoaiPhoi);
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {             
                string strXml = string.Empty;

                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                       if(dr["DiplomasTypeID"].ToString()=="-1")
                        {
                            strXml += "<DiplomasType OlogyID = \"" + CommonFunctions.RefreshXmlString(dr["OlogyID"].ToString())
                          + "\" DiplomasTypeID = \"" + "NULL" + "\"/>";
                        }
                       else
                        {
                            strXml += "<DiplomasType OlogyID = \"" + CommonFunctions.RefreshXmlString(dr["OlogyID"].ToString())
                           + "\" DiplomasTypeID = \"" + CommonFunctions.RefreshXmlString(dr["DiplomasTypeID"].ToString()) + "\"/>";
                        }
                    }
                }
                if (strXml != "")
                {
                    strXml = "<Root>" + strXml + "</Root>";
                    UpdateStaff = User._UserID;
                    string resultUpd = BL_PhoiBang.Update_CauHinhLoaiPhoi_Nganh(strXml, UpdateStaff);
                    MessageBox.Show(resultUpd);
                    GetData();
                    AdjustSizeCol();

                }
                else
                {
                    XtraMessageBox.Show("Cập nhật dữ liệu trước khi lưu...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        #endregion

        #region Events
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuDuLieu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

       

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            GetData();
        }    

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Loại xét";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;

                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Loại xét";

                    gridControlData.ExportToXlsx(sfdFiles.FileName, options);

                    gridViewData.OptionsSelection.MultiSelect = true;
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gridViewData.OptionsSelection.MultiSelect = true;
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            GetData();
            AdjustSizeCol();
        }
        #endregion
       
    }
}
