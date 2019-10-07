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

namespace GrdUI.HeThong
{
    public partial class frm_Grd_ImportExcel : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public DataTable _dtResult = new DataTable(), _dtSheet = new DataTable();
        public string _sheetName = string.Empty, _fileName = string.Empty;
        #endregion

        #region Inits
        #region public frm_Grd_ImportExcel()
        public frm_Grd_ImportExcel()
        {
            InitializeComponent();
        }
        #endregion 
        #endregion

        #region Events
		#region private void btn_ok_Click(object sender, EventArgs e)
        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtSheet.Rows.Count == 0)
                    return;

                _dtResult = ExcelBL.GetSheetContent(ofdFiles.FileName, lookUpEdit_sheet.EditValue.ToString());
                _sheetName = lookUpEdit_sheet.EditValue.ToString();
                _fileName = ofdFiles.FileName;
            }
            catch { }
            this.Close();
        } 
        #endregion

        #region private void btn_thoat_Click(object sender, EventArgs e)
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region private void buttonEdit_chonFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        private void buttonEdit_chonFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                lookUpEdit_sheet.Properties.DataSource = null;
                ofdFiles.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
                if (ofdFiles.ShowDialog() == DialogResult.OK)
                {
                    buttonEdit_chonFile.Text = ofdFiles.FileName;
                    lookUpEdit_sheet.Properties.DataSource = null;
                    if (buttonEdit_chonFile.Text == string.Empty) return;

                    _dtSheet = ExcelBL.GetSchema(buttonEdit_chonFile.Text);
                    if (_dtSheet.Columns.Count == 0) return;
                    lookUpEdit_sheet.Properties.DataSource = _dtSheet;
                    lookUpEdit_sheet.Properties.DisplayMember = "SheetName";
                    lookUpEdit_sheet.Properties.ValueMember = "FullSheetName";

                    LookUpColumnInfoCollection coll = lookUpEdit_sheet.Properties.Columns;
                    if (coll.Count <= 0)
                    {
                        coll.Add(new LookUpColumnInfo("SheetName", 0, "Sheets"));
                    }

                    lookUpEdit_sheet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                    lookUpEdit_sheet.Properties.SearchMode = SearchMode.AutoComplete;
                    lookUpEdit_sheet.Properties.AutoSearchColumnIndex = 1;

                    lookUpEdit_sheet.EditValue = _dtSheet.Rows[0]["FullSheetName"].ToString();
                }
            }
            catch { }
        } 
        #endregion
	    #endregion    
    }
}