using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommonLib.ImportAndExport;
using CommonLib;
using DevExpress.XtraGrid.Views.Grid;
using Infragistics.Win.UltraWinGrid;
using DevExpress.XtraEditors;

namespace CommonLib.ImportAndExport
{
    public partial class frmImportFromExcel : dxfrmExtend
    {
        #region Variable
        private DataSet _dsRoot = null,_ds=null,_dsExcel = null;
        private DataTable _dtSchema = null;
        private int _step = 0;
        private bool checkPass = false,_continue=true;
        private DefineColumns _mapCol = null;
        //var of class ->properties
        private string _msgDlgResult = null, _titleDlgResult = null,_titleWarningMsg =null,_msgDlgErrorConstruction= null,_titleDlgErrorConstruction=null, _msgDlgError = null,_msgDlgNoneFile=null,_msgDlgWarning=null, _titleDlgError = null, _questionMsg = null, _titleQuestionMsg = null;
        private MessageBoxButtons _buttonDlgResult, _buttonDlgError;
        private MessageBoxIcon _iconDlgResult, _iconDlgError;
        private bool _haveMsgDlgResult = false;
        private bool _isCheckConstruct = false,_isExcel=true, _isOnlyReadMap=false;

        public string FromFile = "";
        public int ExcludeTopRow = 0;
        public string[] FromSheet = new string[] { };
        frmStatus fSta = new frmStatus();
        #endregion

        #region Properties
        public bool IsOnlyReadMap
        {
            get { return _isOnlyReadMap; }
            set { _isOnlyReadMap = value; }
        }

        public bool AllowEditConstruct
        {
            get { return btnEditConstruct.Visible; }
            set { btnEditConstruct.Visible = value; }
        }

        public bool HaveMsgDlgResult
        {
            get { return _haveMsgDlgResult; }
            set { _haveMsgDlgResult = value; }
        }
        /// <summary>
        /// Result after import from file
        /// </summary>
        public DataSet DataResult
        {
            get { return _ds; }
        }
        /// <summary>
        /// Message to display when success
        /// </summary>
        public string MsgDlgResult
        {
            get { return _msgDlgResult; }
            set { _msgDlgResult = value; }
        }
        /// <summary>
        /// Message to display when construction file excel is not right
        /// </summary>
        public string MsgDlgErrorConstruction
        {
            get { return _msgDlgErrorConstruction; }
            set { _msgDlgErrorConstruction = value; }
        }
        /// <summary>
        /// Title of dialog when construction of file is invalid
        /// </summary>
        public string TitleDlgErrorConstruction
        {
            get { return _titleDlgErrorConstruction; }
            set { _titleDlgErrorConstruction = value; }
        }        
        /// <summary>
        /// Title of dialog when success
        /// </summary>
        public string TitleDlgResult
        {
            get { return _titleDlgResult; }
            set { _titleDlgResult = value; }
        }
        /// <summary>
        /// Title of warning dialod
        /// </summary>
        public string TitleWarningMsg
        {
            get { return _titleWarningMsg; }
            set { _titleWarningMsg = value; }
        }
        /// <summary>
        /// Message to display when error
        /// </summary>
        /// 
        public string MsgDlgError
        {
            get { return _msgDlgError; }
            set { _msgDlgError = value; }
        }
        /// <summary>
        /// Message display when file not exist 
        /// </summary>
        public string MsgDlgNoneFile
        {
            get { return _msgDlgNoneFile; }
            set { _msgDlgNoneFile = value; }
        }
        /// <summary>
        /// Title of dialog when error
        /// </summary>
        public string TitleDlgError
        {
            get { return _titleDlgError; }
            set { _titleDlgError = value; }
        }
        /// <summary>
        /// Question of dialog in the event of question
        /// </summary>
        public string QuestionMsg
        {
            get { return _questionMsg; }
            set { _questionMsg = value; }
        }
        /// <summary>
        /// Title if question
        /// </summary>
        public string TitleQuestionMsg
        {
            get { return _titleQuestionMsg; }
            set { _titleQuestionMsg = value; }
        }
        /// <summary>
        /// Warning when error
        /// </summary>
        public string MsgDlgWarning
        {
            get { return _msgDlgWarning; }
            set { _msgDlgWarning = value; }
        }
        /// <summary>
        /// Button of dialog when success
        /// </summary>
        public MessageBoxButtons ButtonDlgResult
        {
            get { return _buttonDlgResult; }
            set { _buttonDlgResult = value; }
        }
        /// <summary>
        /// Button of dialog when error
        /// </summary>
        public MessageBoxButtons ButtonDlgError
        {
            get { return _buttonDlgError; }
            set { _buttonDlgError = value; }
        }
        /// <summary>
        /// Icon of dialog when success
        /// </summary>
        public MessageBoxIcon IconDlgResult
        {
            get { return _iconDlgResult; }
            set { _iconDlgResult = value; }
        }
        /// <summary>
        /// Icon of dialog when error
        /// </summary>
        public MessageBoxIcon IconDlgError
        {
            get { return _iconDlgError; }
            set { _iconDlgError = value; }
        }
        /// <summary>
        /// Set or get import will check construct data and sheet
        /// </summary>
        public bool IsCheckConstruct
        {
            get { return _isCheckConstruct; }
            set { _isCheckConstruct = value; }
        }

        #endregion

        #region Init
        /// <summary>
        /// Init this class with a DataSet (no data) and a DefineColumns to map it between English and Vietnamese
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="MapToColumn"></param>
        public frmImportFromExcel(DataSet Dataconstruction,DefineColumns MapToColumn)
        {
            InitializeComponent();
            try
            {
                _dsRoot = Dataconstruction;
                //Remove data in dataset
                foreach (DataTable dt in _dsRoot.Tables)
                {
                    dt.Clear();
                }
                _mapCol = MapToColumn;
                if (_mapCol == null) _mapCol = new DefineColumns();

                #region Init properties
                this.MsgDlgError = "Quá trình nhập dữ liệu có lỗi";
                this.MsgDlgResult = "Nhập dữ liệu thành công";
                this.MsgDlgNoneFile = "Không tìm thấy tập tin Excel. Bạn hãy xem lại!";
                this.QuestionMsg = "Tập tin Excel có số Sheet khác với dữ liệu cần nhập. Bạn có muốn tiếp tục không?";
                this.MsgDlgWarning = "Tập tin Excel không phù hơp. Vui lòng chọn tập tin khác!";
                this.MsgDlgErrorConstruction = "Cấu trúc tập tin Excel không hợp lệ. Vui lòng chọn một tập tin khác!";
                this.TitleDlgError = "UIS - Thông báo";
                this.TitleDlgResult = "UIS - Thông báo";
                this.TitleQuestionMsg = "UIS - Thông báo";
                this.TitleWarningMsg = "UIS - Thông báo";
                this.IconDlgError = MessageBoxIcon.Error;
                this.IconDlgResult = MessageBoxIcon.Information;
                this.ButtonDlgError = MessageBoxButtons.OK;
                this.ButtonDlgResult = MessageBoxButtons.OK;
                #endregion
                fSta.Owner = this;
                cboChangeCode.SelectedIndex = 2;
            }
            catch { }
        }

        #region frmImportFromExcel_Load
        private void frmImportFromExcel_Load(object sender, EventArgs e)
        {
            fSta.EnableTimer = false;
            ChooseStep();
            if (_mapCol == null)
                _mapCol = new DefineColumns();
            _dsExcel = new DataSet();
            if (FromFile != "")
            {
                txtFile.Text = FromFile;
                btnNext_Click(sender, e);
                if (txtFile.Text.ToLower().EndsWith("dbf"))
                {
                    cboChangeCode.SelectedIndex = 0;
                }
            }
            if (FromSheet != null)
            {
                if (FromSheet.Length > 0)
                {
                    for (int i = 0; i < cklSelectSheet.Items.Count; i++)
                    {
                        if (FromSheet.Contains(cklSelectSheet.Items[i].ToString()))
                            cklSelectSheet.SetItemChecked(i, true);
                        else
                            cklSelectSheet.SetItemChecked(i, false);
                    }
                }
            }
            CommonLib.GridSearch.ApplySearch(grdSheetAndColumn, txtSearchMapSheetToTargetData, false, new string[] { "ColumnSheet", "ColumnDataSet" });

            CommonLib.ShortKeyReg.RegisterHotKey(this, CloseForm, Keys.Escape);
        }

        void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #endregion

        #region btnBrowse_Click()
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Suport File(Excel,FoxPro)|*.xls;*.xlsx;*.dbf|Excel file|*.xls;*.xlsx|FoxPro file|*.dbf";
                dlg.FilterIndex = 1;
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = dlg.FileName;
                }
            }
            catch { }
        }
        #endregion

        #region ChooseStep()
        private void ChooseStep()
        {
            try
            {
                switch (_step)
                {
                    case 0:
                        //Reset data
                        if (_dsRoot != null)
                        {
                            _ds = _dsRoot.Copy();
                        }
                        checkPass = false;
                        _dtSchema = null;
                        _continue = true;
                        //
                        btnBack.Enabled = false;
                        btnNext.Text = "Tiếp tục";
                        pnlStep1.Visible = true;
                        pnlStep2.Visible = false;
                        pnlStep3.Visible = false;
                        pnlStep4.Visible = false;
                        pnlStep5.Visible = false;
                        break;
                    case 1:
                        btnBack.Enabled = true;
                        btnNext.Text = "Tiếp tục";
                        pnlStep1.Visible = false;
                        pnlStep2.Visible = true;
                        pnlStep3.Visible = false;
                        pnlStep4.Visible = false;
                        pnlStep5.Visible = false;
                        break;
                    case 2:
                        btnBack.Enabled = true;
                        btnNext.Text = "Tiếp tục";
                        pnlStep1.Visible = false;
                        pnlStep2.Visible = false;
                        pnlStep3.Visible = true;
                        pnlStep4.Visible = false;
                        pnlStep5.Visible = false;
                        break;
                    case 3:
                        btnBack.Enabled = true;
                        btnNext.Text = "Hoàn tất";
                        pnlStep1.Visible = false;
                        pnlStep2.Visible = false;
                        pnlStep3.Visible = false;
                        pnlStep4.Visible = true;
                        pnlStep5.Visible = false;
                        break;                    
                    case 4:
                        btnBack.Enabled = true;
                        btnNext.Text = "Hủy bỏ";
                        pnlStep1.Visible = false;
                        pnlStep2.Visible = false;
                        pnlStep3.Visible = false;
                        pnlStep4.Visible = false;
                        pnlStep5.Visible = true;
                        break;
                    default:
                        _step = 0;
                        ChooseStep();
                        break;
                }
                this.Update();
            }
            catch { }
        }
        #endregion

        #region btnBack_Click()
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dsRoot != null)
                {
                    _ds = _dsRoot.Copy();
                }
                if (_step > 0)
                    if (_isExcel)
                        _step--;
                    else
                        _step = 0;
                ChooseStep(); 
            }
            catch { }
        }
        #endregion

        #region btnNext_Click()
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNext.Text == "Hủy bỏ")
                {
                    _continue = false;
                    _step = 4;
                }
                if (File.Exists(txtFile.Text))
                {
                    #region Check construction file and get sheet
                    if (!checkPass)
                    {
                        if (_ds != null)
                        {
                            #region Check Sheet
                            if (txtFile.Text.ToLower().EndsWith("dbf"))
                            {
                                _isExcel = false;
                                cboChangeCode.Visible = true;
                            }
                            else
                            {
                                _isExcel = true;
                                cboChangeCode.Visible = false;
                                _dtSchema = ExcelBL.GetSchema(txtFile.Text);
                                //if (_dtSchema.Rows.Count != _ds.Tables.Count)
                                //    if (PscMessage.PscMessage.Show(this.QuestionMsg, this.TitleQuestionMsg, PscMessage.ListButton.YesNo, PscMessage.ICon.QuestionMark) != PscMessage.Result.Ok)
                                //    {
                                //        checkPass = false;
                                //        return;
                                //    }
                                foreach (DataRow dr in _dtSchema.Rows)
                                {

                                    string s = dr["SheetName"].ToString();
                                    if (s.Length > 2)
                                    {
                                        if (s.StartsWith("'"))
                                            s = s.Substring(1, s.Length - 1);
                                        if (s.EndsWith("'"))
                                            s = s.Substring(0, s.Length - 1);
                                    }
                                    dr["SheetName"] = s;
                                }
                            }
                            checkPass = true;
                            #endregion                            
                        }
                        else
                        {                            
                            XtraMessageBox.Show(this.MsgDlgWarning,this.TitleWarningMsg, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            checkPass = false;
                            return;
                        }
                    }
                    #endregion
                    if (checkPass)
                    {

                        #region Choose type import
                        if (_isExcel)
                        {
                            grpMapSheetToTargetData.Text = "Chọn cột tương ứng trong Sheet của Excel với đối tượng dữ liệu nhập vào";
                            #region Choose step
                            switch (_step)
                            {
                                case 0:
                                    cboChangeCode.Visible = false;
                                    btnNext.Text = "Hủy bỏ";
                                    btnNext.Update();
                                    #region Remove and add sheet
                                    while (cklSelectSheet.Items.Count > 0 && _continue)
                                    {
                                        Application.DoEvents();
                                        cklSelectSheet.Items.RemoveAt(0);
                                    }
                                    if (_dtSchema == null)
                                        return;
                                    if (_dtSchema.Rows.Count == 0)
                                        return;
                                    foreach (DataRow dr in _dtSchema.Rows)
                                    {
                                        Application.DoEvents();
                                        if (!_continue)
                                            break;
                                        cklSelectSheet.Items.Add(dr["SheetName"].ToString(), true);
                                    }
                                    #endregion
                                    break;
                                case 1:
                                    cboChangeCode.Visible = false;
                                    if (cklSelectSheet.CheckedItems.Count == 0)
                                    {
                                        XtraMessageBox.Show("Bạn phải chọn ít nhất một Sheet", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    btnNext.Text = "Hủy bỏ";
                                    btnNext.Update();

                                    fSta.Visible = true;
                                    fSta.UpdateStatus( "Đang đọc cấu trúc file...");
                                    Application.DoEvents();
                                    #region Remove table in dataset created from file
                                    while (_dsExcel.Tables.Count > 0)
                                        _dsExcel.Tables.RemoveAt(0);
                                    #endregion

                                    #region Define table to restore sheets and column of input dataset
                                    DataTable dtGird = new DataTable();
                                    dtGird.Columns.Add("Check", typeof(bool));
                                    dtGird.Columns.Add("Column", typeof(string));
                                    dtGird.Columns.Add("Sheet", typeof(string));
                                    dtGird.Columns.Add("ColumnMap", typeof(string));
                                    #endregion

                                    #region Add data into restored table
                                    for (int i = 0; i < cklSelectSheet.CheckedItems.Count && _continue; i++)
                                    {
                                        Application.DoEvents();
                                        if (!_continue)
                                        {
                                            _step = 4;
                                            break;
                                        }
                                        DataTable dt = new DataTable();

                                        try
                                        {
                                            dt = ExcelBL.GetConstructColumn(ExcludeTopRow, txtFile.Text, cklSelectSheet.CheckedItems[i].ToString() + "$");
                                        }
                                        catch { }
                                        dt.TableName = cklSelectSheet.CheckedItems[i].ToString();
                                        _dsExcel.Tables.Add(dt.Copy());
                                    }
                                    if (_ds.Tables.Count == 1 && _dsExcel.Tables.Count==1)
                                    {
                                        DataRow dr = dtGird.NewRow();
                                        dr["ColumnMap"] = _ds.Tables[0].TableName;
                                        dr["Column"] = _mapCol.GetCaption(_ds.Tables[0].TableName, true);
                                        dr["Check"] = true;
                                        dr["Sheet"] = _dsExcel.Tables[0].TableName;
                                        dtGird.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        foreach (DataTable dtInDataSet in _ds.Tables)
                                        {
                                            DataRow dr = dtGird.NewRow();
                                            int i = _dsExcel.Tables.IndexOf(_mapCol.GetCaption(dtInDataSet.TableName, true));
                                            dr["ColumnMap"] = dtInDataSet.TableName;
                                            dr["Column"] = _mapCol.GetCaption(dtInDataSet.TableName, true);
                                            if (i < 0)
                                                dr["Check"] = false;
                                            else
                                            {
                                                dr["Check"] = true;
                                                dr["Sheet"] = _dsExcel.Tables[i].TableName;
                                            }
                                            dtGird.Rows.Add(dr);

                                        }
                                    }
                                    #endregion
                                    if (_continue)
                                    {
                                        dtGird.Columns["Column"].ReadOnly = true;

                                        #region Set data for Grid
                                        grdEditTableSheet.DataSource = null;
                                        grdEditTableSheet.DataSource = new DataView(dtGird);

                                        DataTable dt_ds = new DataTable();
                                        dt_ds.Columns.Add("Sheet", typeof(string));
                                        dt_ds.Columns.Add("SheetName", typeof(string));

                                        foreach (DataTable dt_ds2 in _dsExcel.Tables)
                                        {
                                            DataRow dr_ds = dt_ds.NewRow();
                                            dr_ds["Sheet"] = dt_ds2.TableName;
                                            dr_ds["SheetName"] = _mapCol.GetCaption(dt_ds2.TableName, true);
                                            dt_ds.Rows.Add(dr_ds);
                                        }

                                        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                        rep.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sheet"));
                                        rep.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SheetName"));
                                        rep.DataSource = dt_ds.DefaultView;
                                        rep.DisplayMember = "SheetName";
                                        rep.ValueMember = "Sheet";
                                        rep.Columns["Sheet"].Visible = false;
                                        rep.ShowHeader = false;
                                        rep.NullText = "";
                                        grdEditTableSheet.Columns["Sheet"].ColumnEdit = rep;
                                        grdEditTableSheet.Columns["Check"].Caption = "Chọn";
                                        grdEditTableSheet.Columns["Column"].Caption = "Bảng để nhập vào";
                                        grdEditTableSheet.Columns["Sheet"].Caption = "Sheet trong Excel";
                                        grdEditTableSheet.Columns["ColumnMap"].Visible = false;
                                        grdEditTableSheet.CheckAllFieldName = "Check";
                                        grdEditTableSheet.UseCheckAll = true;
                                        #endregion
                                    }
                                    break;
                                case 2:
                                    DataTable dtGrid = ((DataView)grdEditTableSheet.DataSource).ToTable();
                                    if (dtGrid.Select("Check=1").Length == 0)
                                    {
                                        XtraMessageBox.Show("Bạn phải chọn ít nhất một bảng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    btnNext.Text = "Hủy bỏ";
                                    btnNext.Update();
                                    #region add data into combobox
                                    DataTable dtCombo = dtGrid.Clone();
                                    foreach (DataRow dr in dtGrid.Select("Check=1"))
                                    {
                                        Application.DoEvents();
                                        if (!_continue)
                                        {
                                            _step = 4;
                                            break;
                                        }
                                        DataRow drAdd = dtCombo.NewRow();
                                        foreach (DataColumn col in dtCombo.Columns)
                                        {
                                            drAdd[col.ColumnName] = dr[col.ColumnName];
                                        }
                                        dtCombo.Rows.Add(drAdd);
                                    }
                                    #endregion
                                    if (_continue)
                                    {
                                        cboTableToSheetPnl4.DataSource = dtCombo.DefaultView;
                                        cboTableToSheetPnl4.DisplayMember = "Column";
                                        cboTableToSheetPnl4.ValueMember = "ColumnMap";

                                        #region add data into grid
                                        DataTable dtGrid2 = new DataTable();
                                        #region Define a table for grid
                                        dtGrid2.Columns.Add("ColumnDataSet", typeof(string));
                                        dtGrid2.Columns.Add("ColumnDataSetMap", typeof(string));
                                        dtGrid2.Columns.Add("TableSheet", typeof(string));
                                        dtGrid2.Columns.Add("TableDataSet", typeof(string));
                                        dtGrid2.Columns.Add("ColumnSheet", typeof(string));
                                        #endregion
                                        DataTable dtGridCombo = dtGrid2.Clone();

                                        #region add data
                                        foreach (DataRow dr in dtGrid.Select("Check = 1"))
                                        {
                                            Application.DoEvents();
                                            if (!_continue)
                                            {
                                                _step = 4;
                                                break;
                                            }
                                            DataTable dtSheet = _dsExcel.Tables[dr["Sheet"].ToString()];
                                            DataTable dtDataSet = _ds.Tables[dr["ColumnMap"].ToString()];
                                            if (dtSheet.Columns.Count != dtDataSet.Columns.Count && IsCheckConstruct)
                                            {
                                                XtraMessageBox.Show(this.MsgDlgErrorConstruction, this.TitleDlgErrorConstruction, this.ButtonDlgError, this.IconDlgError);
                                                _step = 4;
                                                break;
                                            }
                                            #region Add data
                                            dtGridCombo.Rows.Add(DBNull.Value, DBNull.Value, dtSheet.TableName, dtDataSet.TableName, DBNull.Value);
                                            for (int i = 0; i < dtDataSet.Columns.Count; i++)
                                            {
                                                Application.DoEvents();
                                                if (!_continue)
                                                {
                                                    _step = 4;
                                                    break;
                                                }
                                                DataRow drGrid2 = dtGrid2.NewRow();
                                                drGrid2["TableSheet"] = dtSheet.TableName;
                                                drGrid2["TableDataSet"] = dtDataSet.TableName;

                                                DataRow drGridCombo = dtGridCombo.NewRow();
                                                drGridCombo["TableSheet"] = dtSheet.TableName;
                                                drGridCombo["TableDataSet"] = dtDataSet.TableName;

                                                drGrid2["ColumnDataSet"] = _mapCol.GetCaption(dtDataSet.Columns[i], true);
                                                drGrid2["ColumnDataSetMap"] = dtDataSet.Columns[i].ColumnName;

                                                drGridCombo["ColumnDataSet"] = _mapCol.GetCaption(dtDataSet.Columns[i], true);
                                                drGridCombo["ColumnDataSetMap"] = dtDataSet.Columns[i].ColumnName;

                                                int index = dtSheet.Columns.IndexOf(dtDataSet.Columns[i].ColumnName);
                                                if (index < 0)
                                                    index = dtSheet.Columns.IndexOf(drGrid2["ColumnDataSet"].ToString());
                                                if (index >= 0)
                                                    drGrid2["ColumnSheet"] = dtSheet.Columns[index].ColumnName;
                                                if (dtSheet.Columns.Count > i)
                                                    drGridCombo["ColumnSheet"] = dtSheet.Columns[i].ColumnName;

                                                dtGrid2.Rows.Add(drGrid2);

                                                dtGridCombo.Rows.Add(drGridCombo);
                                            }
                                            #endregion
                                        }
                                        #region Add combo
                                        if (cboTableToSheetPnl4.SelectedValue == null)
                                            return;
                                        string col = cboTableToSheetPnl4.SelectedValue.ToString();
                                        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                        if (col != null)
                                        {
                                            rep.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ColumnSheet"));
                                            //rep.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SheetName"));
                                            DataTable dtCbo = dtGridCombo.Clone();
                                            DataRow dr = dtCbo.NewRow();
                                            dr["TableDataSet"] = "$";
                                            dtCbo.Rows.Add(dr);
                                            foreach (DataRow drSel in ((DataView)cboTableToSheetPnl4.DataSource).Table.Rows)
                                            {
                                                foreach (DataColumn dc in _dsExcel.Tables[drSel["Sheet"].ToString()].Columns)
                                                {
                                                    DataRow drAdd = dtCbo.NewRow();
                                                    drAdd["ColumnSheet"] = dc.ColumnName;
                                                    drAdd["TableSheet"] = drSel["Sheet"].ToString();
                                                    drAdd["TableDataSet"] = drSel["ColumnMap"].ToString();
                                                    dtCbo.Rows.Add(drAdd);
                                                }
                                            }
                                            
                                            
                                            rep.DataSource = dtCbo.DefaultView;
                                            rep.DisplayMember = "ColumnSheet";
                                            rep.ValueMember = "ColumnSheet";

                                            ((DataView)rep.DataSource).RowFilter = "TableDataSet='" + col + "' or TableDataSet ='$'";
                                            rep.ShowHeader = false;
                                            rep.NullText = "";

                                        }
                                        #endregion

                                        if (_continue)
                                        {
                                            dtGrid2.Columns["ColumnDataSet"].ReadOnly = true;
                                            grdSheetAndColumn.DataSource = dtGrid2.DefaultView;
                                            grdSheetAndColumn.Columns["ColumnSheet"].Caption = "Cột trong Excel";
                                            grdSheetAndColumn.Columns["ColumnDataSet"].Caption = "Cột cần nhập vào";
                                            grdSheetAndColumn.Columns["TableSheet"].Visible = false;
                                            grdSheetAndColumn.Columns["TableDataSet"].Visible = false;
                                            grdSheetAndColumn.Columns["ColumnDataSetMap"].Visible = false;
                                            grdSheetAndColumn.Columns["ColumnSheet"].ColumnEdit = rep;
                                            cboSheetPnl4_SelectedIndexChanged(null, null);
                                        }

                                        cboChangeCode.Visible = true;
                                        #endregion
                                        #endregion
                                    }
                                    break;
                                case 3:
                                    //import
                                    btnNext.Text = "Hủy bỏ";
                                    btnNext.Update();
                                    if (!_continue)
                                    {
                                        _continue = false;
                                        _step = 4;
                                    }
                                    else
                                    {
                                        if (IsOnlyReadMap)
                                        {
                                            btnResult_Click(null, null);
                                            return;
                                        }
                                        #region Import
                                        
                                        fSta.Visible=true;
                                        DataView dv = (DataView)grdSheetAndColumn.DataSource;
                                        foreach (DataTable dtSheet in _dsExcel.Tables)
                                        {
                                            DataRow[] drTable = dv.Table.Select("TableSheet='" + dtSheet.TableName + "' and isnull(ColumnSheet,'')<>''");
                                            if (drTable.Length > 0)
                                            {
                                                fSta.UpdateStatus( "Đang nhập bảng " + _mapCol.GetCaption(drTable[0]["TableDataset"].ToString(), true));
                                                Application.DoEvents();
                                                DataTable dtMap = ExcelBL.CreateMapConstruct();
                                                foreach (DataRow drMap in drTable)
                                                {
                                                    dtMap.Rows.Add(drMap["ColumnDataSetMap"], drMap["ColumnSheet"]);
                                                }
                                                DataTable dtData = _ds.Tables[drTable[0]["TableDataSet"].ToString()];

                                                bool read = ExcelBL.GetSheetContentFromConstruct(ExcludeTopRow, txtFile.Text, dtSheet.TableName + "$", dtMap, ref dtData);
                                                if (!read)
                                                {
                                                    this.DialogResult = DialogResult.Cancel;
                                                    this.Close();
                                                    return;
                                                }

                                                DataColumn[] colString = FindColumnString(dtData);
                                                if (colString != null)
                                                {
                                                    foreach (DataColumn col in colString)
                                                    {
                                                        col.ReadOnly = false;
                                                    }
                                                    decimal i = 0, count = dtData.Rows.Count;
                                                    if (cboChangeCode.SelectedIndex == 0 && colString.Length > 0)
                                                    {
                                                        ConvertFont fCon = new ConvertFont();
                                                        foreach (DataRow dr in dtData.Rows)
                                                        {
                                                            i++;
                                                            fSta.UpdateStatus( "Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                            Application.DoEvents();
                                                            foreach (DataColumn col in colString)
                                                            {
                                                                string ss = dr[col.ColumnName].ToString();
                                                                fCon.Convert(ref ss, FontIndex.iTCV, FontIndex.iUNI);
                                                                dr[col.ColumnName] = ss;
                                                            }
                                                        }
                                                    }
                                                    else if (cboChangeCode.SelectedIndex == 1 && colString.Length > 0)
                                                    {
                                                        ConvertFont fCon = new ConvertFont();
                                                        foreach (DataRow dr in dtData.Rows)
                                                        {
                                                            i++;
                                                            fSta.UpdateStatus( "Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                            Application.DoEvents();
                                                            foreach (DataColumn col in colString)
                                                            {
                                                                string ss = dr[col.ColumnName].ToString();
                                                                fCon.Convert(ref ss, FontIndex.iVNI, FontIndex.iUNI);
                                                                dr[col.ColumnName] = ss;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (this.HaveMsgDlgResult)
                                            XtraMessageBox.Show(this.MsgDlgResult, this.TitleDlgResult, this.ButtonDlgResult, this.IconDlgResult);
                                        btnResult_Click(null, null);
                                        #endregion
                                    }
                                    break;
                                case 4:
                                    _continue = false;
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Choose step
                            grpMapSheetToTargetData.Text = "Chọn cột tương ứng trong tập tin tương với đối tượng dữ liệu nhập vào";
                            string[] s = txtFile.Text.Split('\\');
                            s = s[s.Length - 1].Split('.');
                            DataTable dtSheet, dtDataSet;
                            switch (_step)
                            {
                                case 0:

                                    #region step 0
                                    btnNext.Text = "Hủy bỏ";
                                    btnNext.Update();

                                    _dtSchema = ReadDBF.ReadFileSchemaDBF(txtFile.Text, s[0]);
                                    #region Add table to combo
                                    DataTable dtGird = new DataTable();
                                    dtGird.Columns.Add("Column", typeof(string));
                                    dtGird.Columns.Add("Sheet", typeof(string));
                                    dtGird.Columns.Add("ColumnMap", typeof(string));
                                    dtGird.Rows.Add(_mapCol.GetCaption(_dsRoot.Tables[0].TableName, true), s[0], _dsRoot.Tables[0].TableName);
                                    cboTableToSheetPnl4.DataSource = dtGird.DefaultView;
                                    cboTableToSheetPnl4.DisplayMember = "Column";
                                    cboTableToSheetPnl4.ValueMember = "ColumnMap";
                                    #endregion

                                    DataTable dtGrid2 = new DataTable();
                                    #region Define a table for grid
                                    dtGrid2.Columns.Add("ColumnDataSet", typeof(string));
                                    dtGrid2.Columns.Add("ColumnDataSetMap", typeof(string));
                                    dtGrid2.Columns.Add("TableSheet", typeof(string));
                                    dtGrid2.Columns.Add("TableDataSet", typeof(string));
                                    dtGrid2.Columns.Add("ColumnSheet", typeof(string));
                                    #endregion
                                    DataTable dtGridCombo = dtGrid2.Clone();

                                    Application.DoEvents();
                                    if (!_continue)
                                    {
                                        _step = 4;
                                        break;
                                    }
                                    dtSheet = new DataTable();
                                    dtSheet.TableName = _dtSchema.TableName;
                                    foreach (DataRow dr in _dtSchema.Rows)
                                    {
                                        dtSheet.Columns.Add(dr["ColumnName"].ToString(), dr["DataType"].GetType());
                                    }
                                    dtDataSet = _ds.Tables[0];
                                    if (dtSheet.Columns.Count != dtDataSet.Columns.Count && IsCheckConstruct)
                                    {
                                        XtraMessageBox.Show(this.MsgDlgErrorConstruction, this.TitleDlgErrorConstruction, this.ButtonDlgError, this.IconDlgError);
                                        _step = 4;
                                        break;
                                    }
                                    #region Add column to map
                                    dtGridCombo.Rows.Add(DBNull.Value, DBNull.Value, dtSheet.TableName, dtDataSet.TableName, DBNull.Value);
                                    for (int i = 0; i < dtDataSet.Columns.Count; i++)
                                    {
                                        Application.DoEvents();
                                        if (!_continue)
                                        {
                                            _step = 4;
                                            break;
                                        }
                                        DataRow drGrid2 = dtGrid2.NewRow();
                                        drGrid2["TableSheet"] = dtSheet.TableName;
                                        drGrid2["TableDataSet"] = dtDataSet.TableName;

                                        DataRow drGridCombo = dtGridCombo.NewRow();
                                        drGridCombo["TableSheet"] = dtSheet.TableName;
                                        drGridCombo["TableDataSet"] = dtDataSet.TableName;

                                        drGrid2["ColumnDataSet"] = _mapCol.GetCaption(dtDataSet.Columns[i], true);
                                        drGrid2["ColumnDataSetMap"] = dtDataSet.Columns[i].ColumnName;

                                        drGridCombo["ColumnDataSet"] = _mapCol.GetCaption(dtDataSet.Columns[i], true);
                                        drGridCombo["ColumnDataSetMap"] = dtDataSet.Columns[i].ColumnName;

                                        int index = dtSheet.Columns.IndexOf(dtDataSet.Columns[i].ColumnName);
                                        if (index < 0)
                                            index = dtSheet.Columns.IndexOf(drGrid2["ColumnDataSet"].ToString());
                                        if (index >= 0)
                                            drGrid2["ColumnSheet"] = dtSheet.Columns[index].ColumnName;
                                        if (dtSheet.Columns.Count > i)
                                            drGridCombo["ColumnSheet"] = dtSheet.Columns[i].ColumnName;

                                        dtGrid2.Rows.Add(drGrid2);

                                        dtGridCombo.Rows.Add(drGridCombo);
                                    }
                                    #endregion

                                    #region Add combo
                                    if (cboTableToSheetPnl4.SelectedValue == null)
                                        return;
                                    string col = cboTableToSheetPnl4.SelectedValue.ToString();
                                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                    if (col != null)
                                    {
                                        rep.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ColumnSheet"));

                                        DataTable dtCbo = dtGridCombo.Clone();
                                        DataRow dr = dtCbo.NewRow();
                                        dr["TableDataSet"] = "$";
                                        dtCbo.Rows.Add(dr);
                                        DataRowView drv = cboTableToSheetPnl4.SelectedItem as DataRowView;
                                        foreach (DataRow drSel in _dtSchema.Rows)
                                        {
                                            DataRow drAdd = dtCbo.NewRow();
                                            drAdd["ColumnSheet"] = drSel["ColumnName"].ToString();
                                            drAdd["TableSheet"] = drv["Sheet"].ToString();
                                            drAdd["TableDataSet"] = drv["ColumnMap"].ToString();
                                            dtCbo.Rows.Add(drAdd);
                                        }                                        
                                        rep.DataSource = dtCbo.DefaultView;
                                        rep.DisplayMember = "ColumnSheet";
                                        rep.ValueMember = "ColumnSheet";

                                        ((DataView)rep.DataSource).RowFilter = "TableDataSet='" + col + "' or TableDataSet ='$'";
                                        rep.ShowHeader = false;
                                        rep.NullText = "";

                                    }
                                    #endregion

                                    if (_continue)
                                    {
                                        dtGrid2.Columns["ColumnDataSet"].ReadOnly = true;
                                        grdSheetAndColumn.DataSource = dtGrid2.DefaultView;
                                        grdSheetAndColumn.Columns["ColumnSheet"].Caption = "Cột trong file";
                                        grdSheetAndColumn.Columns["ColumnDataSet"].Caption = "Cột cần nhập vào";
                                        grdSheetAndColumn.Columns["TableSheet"].Visible = false;
                                        grdSheetAndColumn.Columns["TableDataSet"].Visible = false;
                                        grdSheetAndColumn.Columns["ColumnDataSetMap"].Visible = false;
                                        grdSheetAndColumn.Columns["ColumnSheet"].ColumnEdit = rep;
                                        cboSheetPnl4_SelectedIndexChanged(null, null);
                                    }
                                    #endregion
                                    break;
                                case 3:
                                    btnNext.Text = "Hủy bỏ";

                                    btnNext.Update();
                                    if (!_continue)
                                    {
                                        _continue = false;
                                        _step = 4;
                                    }
                                    else
                                    {
                                        if (IsOnlyReadMap)
                                        {
                                            btnResult_Click(null, null);
                                            return;
                                        }

                                        #region Import 
                                        fSta.Visible=true;
                                        DataRowView drv = cboTableToSheetPnl4.SelectedItem as DataRowView;
                                        DataView dv = (DataView)grdSheetAndColumn.DataSource;
                                        DataRow[] drTable = dv.Table.Select("TableSheet='" + _dtSchema.TableName + "' and isnull(ColumnSheet,'')<>''");
                                        if (drTable.Length > 0)
                                        {
                                            fSta.UpdateStatus("Đang nhập bảng " + drv["Column"]);
                                            Application.DoEvents();
                                            DataTable dtMap = ReadDBF.CreateMapConstruct();
                                            foreach (DataRow drMap in drTable)
                                            {
                                                dtMap.Rows.Add(drMap["ColumnDataSetMap"], drMap["ColumnSheet"]);
                                            }
                                            DataTable dtData = _ds.Tables[drv["ColumnMap"].ToString()];

                                            ReadDBF.ReadFileDBFFromConstruct(txtFile.Text, drv["Sheet"].ToString(), dtMap, ref dtData);

                                            DataColumn[] colString = FindColumnString(dtData);
                                            if (colString != null)
                                            {
                                                foreach (DataColumn col1 in colString)
                                                {
                                                    col1.ReadOnly = false;
                                                }
                                                decimal i = 0, count = dtData.Rows.Count;
                                                if (cboChangeCode.SelectedIndex == 0 && colString.Length > 0)
                                                {
                                                    ConvertFont fCon = new ConvertFont();
                                                    foreach (DataRow dr in dtData.Rows)
                                                    {
                                                        i++;
                                                        fSta.UpdateStatus( "Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                        Application.DoEvents();
                                                        foreach (DataColumn col1 in colString)
                                                        {
                                                            string ss = dr[col1.ColumnName].ToString();
                                                            fCon.Convert(ref ss, FontIndex.iTCV, FontIndex.iUNI);
                                                            dr[col1.ColumnName] = ss;
                                                        }
                                                    }
                                                }
                                                else if (cboChangeCode.SelectedIndex == 1 && colString.Length > 0)
                                                {
                                                    ConvertFont fCon = new ConvertFont();
                                                    foreach (DataRow dr in dtData.Rows)
                                                    {
                                                        i++;
                                                        fSta.UpdateStatus("Đang chuyển Font (" + ((int)(i * 100 / count)) + "%)");
                                                        Application.DoEvents();
                                                        foreach (DataColumn col1 in colString)
                                                        {
                                                            string ss = dr[col1.ColumnName].ToString();
                                                            fCon.Convert(ref ss, FontIndex.iVNI, FontIndex.iUNI);
                                                            dr[col1.ColumnName] = ss;
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        if (this.HaveMsgDlgResult)
                                            XtraMessageBox.Show(this.MsgDlgResult, this.TitleDlgResult, this.ButtonDlgResult, this.IconDlgResult);
                                        btnResult_Click(null, null);
                                        #endregion
                                    }
                                    break;
                                case 4:
                                    _continue = false;
                                    break;
                            }

                            #endregion
                        }
                        #endregion
                    }
                    if (!_continue)
                        _step = 0;
                    else
                    {
                        if (_isExcel)
                            _step++;
                        else
                        {
                            if (_step == 0)
                                _step = 3;
                            else if (_step == 3)
                                _step = 4;
                        }
                    }
                    ChooseStep();
                }
                else
                {
                    XtraMessageBox.Show(this.MsgDlgNoneFile,this.TitleDlgError, this.ButtonDlgError, this.IconDlgError);
                }
                if (fSta != null)
                    fSta.Hide();
                Application.DoEvents();
            }
            catch(Exception ex)
            { 
                XtraMessageBox.Show(ex.Message, this.TitleDlgError, this.ButtonDlgError, this.IconDlgError);
                if (fSta != null)
                    fSta.Hide();
                Application.DoEvents();
            }
        }
        #endregion          
        
        #region FindColumnString
        public DataColumn[] FindColumnString(DataTable dtData)
        {            
            DataColumn[] colData = new DataColumn[dtData.Columns.Count], colString = null;
            dtData.Columns.CopyTo(colData, 0);
            colString = Array.FindAll(colData, delegate(DataColumn col) { if (col.DataType == typeof(string)) return true; else return false; });
            return colString;
        }
        #endregion

        #region GetMap
        public DataTable GetMap()
        {
            DataTable dt = null;
            try
            {
                DataView dv = (DataView)grdSheetAndColumn.DataSource;
                dt = dv.Table;
            }
            catch { }
            return dt;
        }
        #endregion

        #region cboSheetPnl4_SelectedIndexChanged()
        private void cboSheetPnl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdSheetAndColumn.DataSource != null)
                {
                    string col = cboTableToSheetPnl4.SelectedValue.ToString();
                    DataView dv = (DataView)grdSheetAndColumn.DataSource;
                    dv.RowFilter = "TableDataSet='"+ col +"'";

                    if (grdSheetAndColumn.Columns["ColumnSheet"].ColumnEdit != null)
                    {
                        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep = grdSheetAndColumn.Columns["ColumnSheet"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit; 
                        DataView dvColumn = rep.DataSource as DataView;
                        if (dvColumn != null)
                            dvColumn.RowFilter = "TableDataSet='" + col + "' or TableDataSet ='$'";
                    }
                }
            }
            catch { }
        }
        #endregion

        #region btnResult_Click()
        private void btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();                
            }
            catch { }
        }
        #endregion

        #region btnEditConstruct_Click
        private void btnEditConstruct_Click(object sender, EventArgs e)
        {
            frmEditConstructImportEdit frm = new frmEditConstructImportEdit();
            frm.dsConstruct = this._ds;
            frm._mapCol = this._mapCol;
            frm.ShowDialog(this);
            DataView dvSheetAndColumn = (DataView)grdSheetAndColumn.DataSource;
            foreach (DataTable dtDataSet in this._ds.Tables)
            {
                DataRow[] dr1 = dvSheetAndColumn.Table.Select("TableDataSet='" + dtDataSet.TableName + "'");
                string TableSheet = dr1[0]["TableSheet"].ToString();
                foreach (DataColumn col in dtDataSet.Columns)
                {
                    if (dvSheetAndColumn.Table.Select("ColumnDataSetMap='"+col.ColumnName+"' and TableSheet='"+TableSheet+"' and TableDataSet='"+ dtDataSet.TableName +"'").Length == 0)
                    {
                        DataRowView drAdd = dvSheetAndColumn.AddNew();
                        drAdd["ColumnDataSet"] = _mapCol.GetCaption(col, true);
                        drAdd["ColumnDataSetMap"] = col.ColumnName;
                        drAdd["TableSheet"] = TableSheet;
                        drAdd["TableDataSet"] = dtDataSet.TableName;
                        drAdd["ColumnSheet"] = DBNull.Value;
                    }
                }
            }
        }
        #endregion

        #region chkAllPnlStep2_CheckedChanged
        private void chkAllPnlStep2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < cklSelectSheet.Items.Count; i++)
                {
                    cklSelectSheet.SetItemChecked(i, chkAllPnlStep2.Checked);
                }
            }
            catch { }
        }
        #endregion

        #region ultgSheetAndColumn_AfterCellUpdate
        //private void ultgSheetAndColumn_AfterCellUpdate(object sender, CellEventArgs e)
        //{
        //    try
        //    {
        //        UltraGridRow row = ultgSheetAndColumn.ActiveRow;
        //        if (row != null && e.Cell.Column.Key != "ColumnDataSetMap")
        //        {
        //            row.Cells["ColumnDataSetMap"].Value = e.Cell.Value;
        //        }
        //    }
        //    catch { }
        //}
        #endregion
    }
}
