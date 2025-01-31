using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Infragistics.Win.UltraWinGrid;

namespace CommonLib.ImportAndExport
{
    public partial class frmImportAndExportData : DevExpress.XtraEditors.XtraForm
    {
        DataSet dsData = new DataSet();
        DataTable dtFile = new DataTable();
        SaveFileDialog sfDlg = new SaveFileDialog();

        public DataTable DtFile
        {
            get { return dtFile; }
            set { dtFile = value; }
        }

        public DataSet DsData
        {
            get { return dsData; }
            set { dsData = value; }
        }

        frmStatus fSta = new frmStatus();
        frmQConvertFontImport frmConvert = new frmQConvertFontImport();

        public frmImportAndExportData()
        {
            InitializeComponent();
            dsData.Tables.CollectionChanged += new CollectionChangeEventHandler(Tables_CollectionChanged);
        }

        private void frmImportAndExportData_Load(object sender, EventArgs e)
        {
            try
            {
                Tables_CollectionChanged(null, null);
                fSta.EnableTimer = false;
                dtFile.Columns.Add("TableName", typeof(string));
                dtFile.Columns.Add("Path", typeof(string));
                sfDlg.Filter = "FoxPro file (*.dbf)|*.dbf";
                sfDlg.FilterIndex = 0;                
            }
            catch { }
            CommonLib.ShortKeyReg.RegisterHotKey(this, CloseForm, Keys.Escape);
        }

        void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        void Tables_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            try
            {
                if (e == null) return;

                DataTable dtTbl = null;

                if (grdList.DataSource == null)
                {
                    dtTbl = new DataTable();
                    dtTbl.Columns.Add("TableName", typeof(string));
                    grdList.DataSource = dtTbl;
                    grvList.Columns["TableName"].Caption = "Tên file dữ liệu";
                    grvList.BestFitColumns();
                }
                else
                {
                    dtTbl = (DataTable)grdList.DataSource;
                }

                DataTable dt = e.Element as DataTable;
                if (dt == null) return;

                if (e.Action == CollectionChangeAction.Add)
                {
                    dtTbl.Rows.Add(dt.TableName);
                }
                else if (e.Action == CollectionChangeAction.Remove)
                {
                    DataRow[] drSel = dtTbl.Select("TableName='" + dt.TableName + "'");
                    if (drSel.Length > 0)
                        dtTbl.Rows.Remove(drSel[0]);

                    drSel = dtFile.Select("TableName='" + dt.TableName + "'");
                    if (drSel.Length > 0)
                        dtFile.Rows.Remove(drSel[0]);
                }

                dtTbl.AcceptChanges();
            }
            catch { }
        }

        private void mnuDeleteSelectedFile_Click(object sender, EventArgs e)
        {
            try
            {
                dsData.Tables.Remove(grvList.GetFocusedRowCellValue("TableName").ToString());
                if (dsData.Tables.Count == 0)
                {
                    grdData.DataSource = null;
                }
            }
            catch { }
        }

        private void mnuDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                while (dsData.Tables.Count > 0)
                {
                    dsData.Tables.Remove(dsData.Tables[0]);
                }
                if (dsData.Tables.Count == 0)
                {
                    grdData.DataSource = null;
                }
            }
            catch { }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                dlg.Title = "Chon File Import";
                dlg.Filter = "Suport File(Excel,FoxPro)|*.xls;*.dbf|Excel file|*.xls|FoxPro file|*.dbf";
                dlg.FilterIndex = 1;
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FontIndex conv = FontIndex.iNotKnown;

                    #region Chon Font
                    if (!frmConvert.chkDefault.Checked)
                    {
                        frmConvert.IsImport = true;
                        if (frmConvert.ShowDialog(this) == DialogResult.OK)
                        {
                            if (frmConvert.cboChangefont.SelectedIndex == 0)
                                conv = FontIndex.iTCV;
                            else if (frmConvert.cboChangefont.SelectedIndex == 1)
                                conv = FontIndex.iVNI;
                        }
                    }
                    else
                    {
                        if (frmConvert.cboChangefont.SelectedIndex == 0)
                            conv = FontIndex.iTCV;
                        else if (frmConvert.cboChangefont.SelectedIndex == 1)
                            conv = FontIndex.iVNI;
                    }
                    #endregion

                    foreach (string sFile in dlg.FileNames)
                    {
                        string[] s = sFile.Split('\\');
                        s = s[s.Length - 1].Split('.');
                        if (sFile.ToLower().EndsWith("dbf"))
                        {
                            try
                            {
                                fSta.UpdateStatus("Đang đọc File: " + sFile);
                                DataTable dtDBF = CommonLib.ReadDBF.ReadFileDBF(sFile, s[0]);
                                #region Chuyen Font
                                if (conv != FontIndex.iNotKnown)
                                {
                                    ConvertFont fCon = new ConvertFont();
                                    int i = 0, count = dtDBF.Rows.Count;
                                    DataColumn[] colString = FindColumnString(dtDBF);

                                    if (colString != null)
                                    {
                                        foreach (DataRow dr in dtDBF.Rows)
                                        {
                                            i++;
                                            fSta.UpdateStatus("Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                            Application.DoEvents();
                                            foreach (DataColumn col1 in colString)
                                            {
                                                string ss = dr[col1.ColumnName].ToString();
                                                fCon.Convert(ref ss, conv, FontIndex.iUNI);
                                                dr[col1.ColumnName] = ss;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                dtDBF.TableName = GenTableName(s[0], sFile);
                                dsData.Tables.Add(dtDBF);
                                dtFile.Rows.Add(dtDBF.TableName, sFile);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {

                            DataTable dtSheet = CommonLib.ExcelBL.GetSchema(sFile);
                            foreach (DataRow drSheet in dtSheet.Rows)
                            {
                                try
                                {
                                    fSta.UpdateStatus("Đang đọc File: " + sFile);
                                    DataTable dtExcel = CommonLib.ExcelBL.GetSheetContent(sFile, drSheet["FullSheetName"].ToString());
                                    #region Chuyen Font
                                    if (conv != FontIndex.iNotKnown)
                                    {
                                        ConvertFont fCon = new ConvertFont();
                                        int i = 0, count = dtExcel.Rows.Count;
                                        DataColumn[] colString = FindColumnString(dtExcel);

                                        if (colString != null)
                                        {
                                            foreach (DataRow dr in dtExcel.Rows)
                                            {
                                                i++;
                                                fSta.UpdateStatus("Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                Application.DoEvents();
                                                foreach (DataColumn col1 in colString)
                                                {
                                                    string ss = dr[col1.ColumnName].ToString();
                                                    fCon.Convert(ref ss, conv, FontIndex.iUNI);
                                                    dr[col1.ColumnName] = ss;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    dtExcel.TableName = GenTableName(s[0], sFile);
                                    dsData.Tables.Add(dtExcel);
                                    dtFile.Rows.Add(dtExcel.TableName, sFile);
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            fSta.Hide();
        }

        string GenTableName(string file, string path)
        {
            file = file.ToLower();
            if (!dsData.Tables.Contains(file))
                return file;
            for (int i = 0; i < 10000; i++)
            {
                if (!dsData.Tables.Contains(file + i))
                    return file + i;
            }            

            return file + (new Random().Next(200000));
        }

        private void grvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{
            //    if (dsData.Tables.Count == 0)
            //    {
            //        grdData.DataSource = null;
            //    }
            //    else
            //    {
            //        DataTable dt = dsData.Tables[grvList.GetFocusedRowCellValue("TableName").ToString()].Copy();
            //        grdData.DataSource = null;
            //        grdData.DataSource = dt;
            //    }
            //}
            //catch { }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            try
            {
                frmChooseFolderImport frm = new frmChooseFolderImport();
                FontIndex conv = FontIndex.iNotKnown;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string path in Directory.GetFiles(frm.txtFolder.Text, frm.txtFile.Text, SearchOption.AllDirectories))
                    {

                        #region Chon Font
                        if (conv == FontIndex.iNotKnown)
                        {
                            if (!frmConvert.chkDefault.Checked)
                            {
                                frmConvert.IsImport = true;
                                if (frmConvert.ShowDialog(this) == DialogResult.OK)
                                {
                                    if (frmConvert.cboChangefont.SelectedIndex == 0)
                                        conv = FontIndex.iTCV;
                                    else if (frmConvert.cboChangefont.SelectedIndex == 1)
                                        conv = FontIndex.iVNI;
                                }
                            }
                            else
                            {
                                if (frmConvert.cboChangefont.SelectedIndex == 0)
                                    conv = FontIndex.iTCV;
                                else if (frmConvert.cboChangefont.SelectedIndex == 1)
                                    conv = FontIndex.iVNI;
                            }
                        }
                        #endregion

                        string[] s = path.Split('\\');
                        s = s[s.Length - 1].Split('.');

                        if (path.ToLower().EndsWith("dbf"))
                        {
                            fSta.UpdateStatus("Import File: " + path);
                            try
                            {
                                DataTable dtDBF = CommonLib.ReadDBF.ReadFileDBF(path, s[0]);
                                #region Chuyen Font
                                if (conv != FontIndex.iNotKnown)
                                {
                                    ConvertFont fCon = new ConvertFont();
                                    int i = 0, count = dtDBF.Rows.Count;
                                    DataColumn[] colString = FindColumnString(dtDBF);

                                    if (colString != null)
                                    {
                                        foreach (DataRow dr in dtDBF.Rows)
                                        {
                                            i++;
                                            fSta.UpdateStatus("Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                            Application.DoEvents();
                                            foreach (DataColumn col1 in colString)
                                            {
                                                string ss = dr[col1.ColumnName].ToString();
                                                fCon.Convert(ref ss, conv, FontIndex.iUNI);
                                                dr[col1.ColumnName] = ss;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                if (frm.chkOptionImport.Checked && dsData.Tables.Contains(s[0]))
                                {
                                    DataTable dtOld = dsData.Tables[s[0]];
                                    foreach (DataRow dr in dtDBF.Rows)
                                    {
                                        dtOld.ImportRow(dr);
                                    }
                                    dtOld.AcceptChanges();
                                }
                                else
                                {
                                    dtDBF.TableName = GenTableName(s[0], path);
                                    dsData.Tables.Add(dtDBF);
                                    dtFile.Rows.Add(dtDBF.TableName, path);
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (path.ToLower().EndsWith("xls"))
                        {
                            fSta.UpdateStatus("Import File: " + path);
                            DataTable dtSheet = CommonLib.ExcelBL.GetSchema(path);
                            foreach (DataRow drSheet in dtSheet.Rows)
                            {
                                try
                                {
                                    DataTable dtExcel = CommonLib.ExcelBL.GetSheetContent(path, drSheet["FullSheetName"].ToString());
                                    #region Chuyen Font
                                    if (conv != FontIndex.iNotKnown)
                                    {
                                        ConvertFont fCon = new ConvertFont();
                                        int i = 0, count = dtExcel.Rows.Count;
                                        DataColumn[] colString = FindColumnString(dtExcel);

                                        if (colString != null)
                                        {
                                            foreach (DataRow dr in dtExcel.Rows)
                                            {
                                                i++;
                                                fSta.UpdateStatus("Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                Application.DoEvents();
                                                foreach (DataColumn col1 in colString)
                                                {
                                                    string ss = dr[col1.ColumnName].ToString();
                                                    fCon.Convert(ref ss, conv, FontIndex.iUNI);
                                                    dr[col1.ColumnName] = ss;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    if (frm.chkOptionImport.Checked && dsData.Tables.Contains(s[0]))
                                    {
                                        DataTable dtOld = dsData.Tables[s[0]];
                                        foreach (DataRow dr in dtExcel.Rows)
                                        {
                                            dtOld.ImportRow(dr);
                                        }
                                        dtOld.AcceptChanges();
                                    }
                                    else
                                    {
                                        dtExcel.TableName = GenTableName(s[0], path);
                                        dsData.Tables.Add(dtExcel);
                                        dtFile.Rows.Add(dtExcel.TableName, path);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            fSta.Hide();
        }

        public DataColumn[] FindColumnString(DataTable dtData)
        {
            DataColumn[] colData = new DataColumn[dtData.Columns.Count], colString = null;
            dtData.Columns.CopyTo(colData, 0);
            colString = Array.FindAll(colData, delegate(DataColumn col) { if (col.DataType == typeof(string)) return true; else return false; });
            return colString;
        }

        private void grvData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
            catch { }
        }

        private void grdData_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ExtendFirstColumn;
                e.Layout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex;
            }
            catch { }
        }

        private void mnuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                frmExportToExcel frm = new frmExportToExcel(dsData, null);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    XtraMessageBox.Show("Xuất Excel thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                cmsExport.Show(MousePosition.X + 3, MousePosition.Y + 3);
            }
            catch { }
        }

        private void mnuFoxPro_Click(object sender, EventArgs e)
        {
            frmConvert.chkDefault.Checked = false;
            bool error = false;
            try
            {
                string sameFile = "";
                int timeSameFile = 0;
                foreach (DataTable dtData in dsData.Tables)
                {
                    string[] s = null;
                    if (timeSameFile < 3)
                    {
                        OpenFileDialog sfdFiles = new OpenFileDialog();
                        sfdFiles.Title = "Chon File de xuat du lieu: " + dtData.TableName;
                        sfdFiles.Filter = "FoxPro file|*.dbf";
                        if (!(sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)) return;
                        sameFile = sfdFiles.FileName;
                        timeSameFile++;
                    }
                    s = sameFile.Split('\\');
                    s = s[s.Length - 1].Split('.');

                    DataTable dtDBF = CommonLib.ReadDBF.ReadFileSchemaDBF(sameFile, s[0]);
                    DataTable dtInput = new DataTable();
                    foreach (DataRow dr in dtDBF.Rows)
                    {
                        dtInput.Columns.Add(dr["ColumnName"].ToString(), Type.GetType(dr["DataType"].ToString()));
                    }

                    OleDbConnect conDBF = new OleDbConnect(String.Format("Provider=VFPOLEDB.1;Data Source={0};", sameFile));
                    try
                    {
                        frmMapColumn frmM = new frmMapColumn();
                        frmM.LoadData(dtInput, dtData, null);
                        if (frmM.ShowDialog(this) == DialogResult.OK)
                        {

                            int i = 0, count = dtData.Rows.Count;
                            ConvertFont fCon = new ConvertFont();
                            FontIndex conv = FontIndex.iNotKnown;
                            frmConvert.IsImport = false;
                            if (!frmConvert.chkDefault.Checked)
                            {
                                if (frmConvert.ShowDialog() == DialogResult.OK)
                                {
                                    if (frmConvert.cboChangefont.SelectedIndex == 0)
                                        conv = FontIndex.iTCV;
                                    else if (frmConvert.cboChangefont.SelectedIndex == 1)
                                        conv = FontIndex.iVNI;
                                }
                            }

                            int timeCount = 0;
                            string sqlError = "";

                            foreach (DataRow dr in dtData.Rows)
                            {
                                string insCol = "";
                                string insVal = "";

                                foreach (DataColumn colInput in dtInput.Columns)
                                {

                                    DataRow[] drMapCol = frmM.dtMap.Select("InputColumn='" + colInput.ColumnName + "'");
                                    if (drMapCol.Length > 0)
                                    {

                                        string mapCol = drMapCol[0]["MapColumn"].ToString().Trim();

                                        if (mapCol != "" && dr[mapCol] != DBNull.Value)
                                        {
                                            insCol += (insCol == "" ? "" : ",") + colInput.ColumnName;

                                            if (colInput.DataType == typeof(string))
                                            {
                                                string ss = dr[mapCol].ToString().Trim();
                                                if (conv != FontIndex.iNotKnown)
                                                {
                                                    fCon.Convert(ref ss, FontIndex.iUNI, conv);
                                                }
                                                insVal += (insVal == "" ? "" : ",") + "'" + ss + "'";
                                            }
                                            else if (colInput.DataType == typeof(DateTime))
                                            {
                                                insVal += (insVal == "" ? "" : ",") + "{^" + CommonLib.Functions.ConvertDateToStringSQL((DateTime)dr[mapCol], "yyyy-MM-dd HH:mm:ss") + "}";
                                            }
                                            else if (colInput.DataType == typeof(bool))
                                            {
                                                string val = "1";
                                                if (dr[mapCol].ToString() == false.ToString())
                                                    val = "0";
                                                insVal += (insVal == "" ? "" : ",") + (dr[colInput.ColumnName] == DBNull.Value ? "null" : val);
                                            }
                                            else
                                            {
                                                insVal += (insVal == "" ? "" : ",") + dr[mapCol];
                                            }
                                        }
                                    }
                                }

                                fSta.UpdateStatus("Đang lưu dữ liệu (" + ((int)(i * 100 / count)) + "%)");
                                try
                                {
                                    string sql = "insert into " + s[0] + "(" + insCol + ") values(" + insVal + ")";
                                    int result = conDBF.ExcuteSQLNotCloseConnect(sql);
                                    if (result < 0)
                                    {
                                        if (timeCount < 3)
                                        {
                                            if (XtraMessageBox.Show("Có lỗi ở mẫu tin " + (i + 1), "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            {
                                                error = true;
                                                break;
                                            }
                                            else
                                                timeCount++;
                                        }
                                        sqlError += "\n" + sql;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (timeCount < 3)
                                    {
                                        if (XtraMessageBox.Show("Có lỗi ở mẫu tin " + (i + 1) + ": " + ex.Message, "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                        {
                                            error = true;
                                            break;
                                        }
                                        else
                                            timeCount++;
                                    }
                                    sqlError += "\n" + sqlError;
                                }
                                i++;
                            }
                            if (timeCount > 0)
                            {
                                frmMessageString frmMess = new frmMessageString();
                                frmMess.txtValue.Text = sqlError;
                                frmMess.Text = "Error!";
                                frmMess.ShowDialog(this);
                            }
                        }
                    }
                    catch { }
                    
                    conDBF.CloseConnection();
                }
            }
            catch 
            {
                error = true;
            }
            fSta.Hide();
            if(!error)
                XtraMessageBox.Show("Xuất FoxPro thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void grvList_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsData.Tables.Count == 0)
                {
                    grdData.DataSource = null;
                }
                else
                {
                    DataTable dt = dsData.Tables[grvList.GetFocusedRowCellValue("TableName").ToString()].Copy();
                    grdData.DataSource = null;
                    grdData.DataSource = dt;
                }
            }
            catch { }
        }

        //public event SaveFileHandler SaveFile;

        //protected void OnSaveFile(object sender, SaveFileArgs e)
        //{
        //    if (SaveFile != null)
        //    {                
        //        SaveFile(sender, e);
        //    }
        //}
    }

    //public delegate void SaveFileHandler(object sender, SaveFileArgs e);

    //public class SaveFileArgs : EventArgs
    //{
    //    string _fileName = "";

    //    public SaveFileArgs(string fileName)
    //    {
    //        _fileName = fileName;
    //    }

    //    public string FileName
    //    {
    //        get { return _fileName; }
    //        set { _fileName = value; }
    //    }
    //}
}