using DevExpress.XtraEditors;
using GrdCore.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Common.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using GrdReports;
using ScrUI.Others;

namespace GrdUI.PhoiBang
{
    public partial class frm_Grd_DanhMucPhoiBang : Form
    {
        #region Variables
        DataSet _dtprintSet = new DataSet();
        DataTable _dtData = new DataTable(), _dtGridColumns = new DataTable(), _dtDataTypeDiplomas = new DataTable();
        DataRow _drGrids;
        public bool isBienban = false;
        int _periodOfGrantID = 0;
        string UpdateStaff = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_DanhMucPhoiBang()
        {
            InitializeComponent();
        }
        private void frm_Grd_DanhMucPhoiBang_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
            try
            {
                //dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'Phoibang_DanhMucPhoiBang'").GetValue(0);
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
           // GetData();
        }

        #endregion
        #region Functions
        private void GetData()
        {
            try
            {         
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();
                _dtData = new DataTable();
                _periodOfGrantID = int.Parse(lookUp_DotCapPhoiBang.EditValue.ToString());
                _dtData = BL_PhoiBang.Danhmucphoibang(_periodOfGrantID);

                foreach (DataColumn dc in _dtData.Columns)
                dc.ReadOnly = false;
                gridControlData.DataSource = _dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
                #region Reponsitory Date            
                AppGridView.RegisterControlField(gridViewData, "ReceivedDate", repositoryItemLookUpEdit_ReciviedDate);
                #endregion
                           
                #region Danh mục loại phôi
                _dtDataTypeDiplomas = BL_PhoiBang.DanhMucLoaiPhoiBang();
                repositoryItemLookUpEdit_LoaiPhoi.DataSource = _dtDataTypeDiplomas;
                repositoryItemLookUpEdit_LoaiPhoi.DisplayMember = "DiplomasTypeName";
                repositoryItemLookUpEdit_LoaiPhoi.ValueMember = "DiplomasTypeID";

                LookUpColumnInfoCollection coll = repositoryItemLookUpEdit_LoaiPhoi.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("DiplomasTypeID", 50, "Mã loại"));
                coll.Add(new LookUpColumnInfo("DiplomasTypeName", 100, "Tên Loại phôi"));

                repositoryItemLookUpEdit_LoaiPhoi.NullText = string.Empty;
                repositoryItemLookUpEdit_LoaiPhoi.SearchMode = SearchMode.AutoComplete;

                AppGridView.RegisterControlField(gridViewData, "DiplomasTypeID", repositoryItemLookUpEdit_LoaiPhoi);
                #endregion
                AdjustSizeCol();
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

                lookUp_DotCapPhoiBang.Properties.DataSource = myDataView.ToTable();
                lookUp_DotCapPhoiBang.Properties.DisplayMember = "PeriodOfGrantName";
                lookUp_DotCapPhoiBang.Properties.ValueMember = "AutoID";

                LookUpColumnInfoCollection coll = lookUp_DotCapPhoiBang.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("PeriodOfGrantName", "Tên đợt cấp"));
                coll.Add(new LookUpColumnInfo("DispatchNumber", "Công văn số"));
                coll.Add(new LookUpColumnInfo("DispatchDate", "Ngày"));

                lookUp_DotCapPhoiBang.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUp_DotCapPhoiBang.Properties.SearchMode = SearchMode.AutoComplete;
                lookUp_DotCapPhoiBang.Properties.AutoSearchColumnIndex = 0;
                lookUp_DotCapPhoiBang.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AdjustSizeCol()
        {
            int size = gridControlData.Size.Width;
            int coutCol = _dtGridColumns.Rows.Count + 1;
            for (int i = 0; i < coutCol; i++)
            {
                gridViewData.Columns[i].Width = size / coutCol;
            }
        }
        private void SaveData()
        {
            try
            {
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    XtraMessageBox.Show("Đang có dữ liệu được chọn để xóa." + "\n" + "Hãy xử lý xóa hoặc bỏ chọn trước khi lưu."
                            , "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string strXml = string.Empty;

                foreach (DataRow dr in _dtData.Rows)
                {
                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                        strXml += "<Diplomas ShipmentsID = \"" + CommonFunctions.RefreshXmlString(dr["ShipmentsID"].ToString())
                            + "\" DiplomasTypeID = \"" + CommonFunctions.RefreshXmlString(dr["DiplomasTypeID"].ToString())
                            + "\" Quantity = \"" + CommonFunctions.RefreshXmlString(dr["Quantity"].ToString())
                            + "\" ReceivedDate = \"" + CommonFunctions.RefreshXmlString(DateTime.Parse(dr["ReceivedDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                            + "\" SerialNumber = \"" + CommonFunctions.RefreshXmlString(dr["SerialNumber"].ToString()).ToUpper().Trim()
                            + "\" Code = \"" + CommonFunctions.RefreshXmlString(dr["Code"].ToString())
                            + "\" PeriodOfGrantID = \"" + lookUp_DotCapPhoiBang.EditValue.ToString()
                            + "\" AutoID = \"" + CommonFunctions.RefreshXmlString(dr["AutoID"].ToString()) 
                            + "\" OldShipmentsID = \"" + CommonFunctions.RefreshXmlString(dr["OldShipmentsID"].ToString())
                            + "\" CodeLength = \"" + CommonFunctions.RefreshXmlString(dr["CodeLength"].ToString()) + "\"/>";
                    }
                }
                if (strXml != "")
                {
                    strXml = "<Root>" + strXml + "</Root>";
                    DataTable check = BL_PhoiBang.Check_DanhMucPhoi(strXml);
                    if (check.Rows.Count > 0)
                    {
                        string mes = string.Empty;
                        for (int i = 0; i < check.Rows.Count; i++)
                        {
                            mes += "Lô phôi " + check.Rows[i]["ShipmentsID"].ToString() + "- Không thể thay đổi thông tin \n";
                        }
                        XtraMessageBox.Show(mes, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }
                    else
                    {
                        UpdateStaff = User._UserID;
                        string result = BL_PhoiBang.Insert_DanhMucPhoi(strXml, UpdateStaff);
                        GetData();
                        AdjustSizeCol();//Điều chỉnh kích thước canh đều các cột khi load lại form
                        XtraMessageBox.Show(result.ToString(), "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }           
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

        private void DeleteData()
        {
            try
            {
                if (gridViewData.GetSelectedRows().Length > 0)
                {
                    if (XtraMessageBox.Show("Xóa dữ liệu đã chọn ?", "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    string strXml = string.Empty;
                    foreach (int i in gridViewData.GetSelectedRows())
                    {
                        if (!(gridViewData.GetDataRow(i)["ShipmentsID"] == DBNull.Value || gridViewData.GetDataRow(i)["ShipmentsID"].ToString() == string.Empty))
                            strXml += "<Diplomas ShipmentsID = \"" + gridViewData.GetDataRow(i)["ShipmentsID"].ToString()+ "\"/>";
                    }
                    strXml = "<Root>" + strXml + "</Root>";

                    DataTable check = BL_PhoiBang.Check_DanhMucPhoi(strXml);
                   if (check.Rows.Count>0)
                    {
                        string mes = string.Empty;
                        for(int i=0; i<check.Rows.Count; i++)
                        {
                            mes +="Lô phôi "+ check.Rows[i]["ShipmentsID"].ToString()+ "- Không được xóa \n";
                        }
                        XtraMessageBox.Show(mes, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   else
                    {
                        string result = BL_PhoiBang.Delete_DanhMucPhoi(strXml);
                        XtraMessageBox.Show(result.ToString(), "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                        AdjustSizeCol();
                    }
                   
                }

                else
                    XtraMessageBox.Show("Chưa chọn dữ liệu để xóa...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnXoaDuLieu_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
    

        private void btn_Locdulieu_Click(object sender, EventArgs e)
        {
            GetData();
            AdjustSizeCol();
        }

        private void btn_ThongKeTheoDot_Click(object sender, EventArgs e)
        {
            cms_Report_UEL.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void cms_BienBanKiemKeVanBang_Click(object sender, EventArgs e)
        {
            try
            {
                int _PeriodOfGrant = int.Parse(lookUp_DotCapPhoiBang.EditValue.ToString());

                _dtprintSet = BL_Reports.BienBanKiemVanBang(_PeriodOfGrant);

                frmSoQuyetDinh_ReportPhoiBang frmSoQD = new frmSoQuyetDinh_ReportPhoiBang(_dtprintSet.Tables["Total"].Copy());
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();
                //frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                //frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                //frmSoQD._quyetDinh = false;
                //frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;
                string _Nguoilapbang = User._UserName.ToString();
                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                DataTable _dtSluong = frmSoQD.dt_SLDeNghi2;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BienBanKiemKeVanBang(_dtprintSet, _dtSluong, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName, _Nguoilapbang);
                frm.ShowDialog();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
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
