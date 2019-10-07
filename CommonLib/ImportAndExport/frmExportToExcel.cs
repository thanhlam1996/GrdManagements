using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using DevExpress.XtraEditors;

namespace CommonLib.ImportAndExport
{
    public partial class frmExportToExcel : dxfrmExtend
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);

        #region Variable
        private DataSet _data = null;
        private int _step = 0;
        private bool _continue = true, _isCheckAlsoChild = true;
        private DefineColumns _mapCol = null;
        //var of class ->properties
        private string _msgDlgResult = null, _titleDlgResult = null, _msgDlgError = null, _titleDlgError = null, _questionMsg = null, _titleQuestionMsg = null;
        private MessageBoxButtons _buttonDlgResult, _buttonDlgError;
        private MessageBoxIcon _iconDlgResult, _iconDlgError;

        private string[] _visibleColumn = null;
        private string[][][] _header = new string[][][] { };

        #endregion

        #region Properties
        public bool OutPutDefineColumn
        {
            get { return chkIncludeHeader.Checked; }
            set { chkIncludeHeader.Checked = value; }
        }
        public string[][][] Header
        {
            get { return _header; }
            set { _header = value; }
        }

        #region VisibleColumn
        public string[] VisibleColumn
        {
            get { return _visibleColumn; }
            set { _visibleColumn = value; }
        }
        #endregion

        /// <summary>
        /// Message to display when success
        /// </summary>
        public string MsgDlgResult
        {
            get { return _msgDlgResult; }
            set { _msgDlgResult = value; }
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
        /// Message to display when error
        /// </summary>
        public string MsgDlgError
        {
            get { return _msgDlgError; }
            set { _msgDlgError = value; }
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
        #endregion

        #region Init
        /// <summary>
        /// Init this class with a DataSet (have data) and a DefineColumns to map it between English and Vietnamese
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="MapToColumn"></param>
        public frmExportToExcel(DataSet Data, DefineColumns MapToColumn)
        {
            InitializeComponent();
            _data = Data;
            _mapCol = MapToColumn;
            if (_mapCol == null) _mapCol = new DefineColumns();

            #region Init properties
            this.MsgDlgError = "Quá trình xuất dữ liệu có lỗi";
            this.MsgDlgResult = "Xuất dữ liệu thành công";
            this.QuestionMsg = "Đã có một tập tin giống tên. Bạn có muốn lưu đè lên tập tin cũ không?";
            this.TitleDlgError = "UIS - Thông báo";
            this.TitleDlgResult = "UIS - Thông báo";
            this.TitleQuestionMsg = "UIS - Thông báo";
            this.IconDlgError = MessageBoxIcon.Error;
            this.IconDlgResult = MessageBoxIcon.Information;
            this.ButtonDlgError = MessageBoxButtons.OK;
            this.ButtonDlgResult = MessageBoxButtons.OK;
            #endregion
        }

        public frmExportToExcel(DataTable Data, DefineColumns MapToColumn)
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            if (Data.DataSet == null)
                ds.Tables.Add(Data);
            else
                ds.Tables.Add(Data.Copy());
            _data = ds;
            _mapCol = MapToColumn;
            if (_mapCol == null) _mapCol = new DefineColumns();

            #region Init properties
            this.MsgDlgError = "Quá trình xuất dữ liệu có lỗi";
            this.MsgDlgResult = "Xuất dữ liệu thành công";
            this.QuestionMsg = "Đã có một tập tin giống tên. Bạn có muốn lưu đè lên tập tin cũ không?";
            this.TitleDlgError = "UIS - Thông báo";
            this.TitleDlgResult = "UIS - Thông báo";
            this.TitleQuestionMsg = "UIS - Thông báo";
            this.IconDlgError = MessageBoxIcon.Error;
            this.IconDlgResult = MessageBoxIcon.Information;
            this.ButtonDlgError = MessageBoxButtons.OK;
            this.ButtonDlgResult = MessageBoxButtons.OK;
            #endregion
        }
        #endregion

        #region load data with Table will be add to grid
        private void frmExportToExcel_Load(object sender, EventArgs e)
        {
            try
            {
                #region Load into Grid
                if (_data == null)
                    _data = new DataSet();
                else
                {
                    for (int i = 0; i < _data.Tables.Count; )
                    {
                        if (_data.Tables[i] == null)
                            _data.Tables.Remove(_data.Tables[i]);
                        else if (_data.Tables[i].Columns.Count == 0)
                            _data.Tables.Remove(_data.Tables[i]);
                        else
                            i++;
                    }
                }
                DataTable dtLoad = new DataTable();
                dtLoad.Columns.Add("TableName", typeof(string));
                dtLoad.Columns.Add("TitleSheet", typeof(string));
                dtLoad.Columns.Add("Check", typeof(bool));

                for (int i = 0; i < _data.Tables.Count; i++)
                {
                    DataRow dr = dtLoad.NewRow();
                    dr["TableName"] = _data.Tables[i].TableName;
                    string s = _data.Tables[i].TableName;
                    s = s.Contains("Table") ? _data.Tables[i].Columns[0].ColumnName : s;
                    dr["TitleSheet"] = _mapCol.GetCaption(s.Substring(0, s.Contains("ID") ? s.Length - 2 : s.Length), true);
                    dr["Check"] = true;
                    dtLoad.Rows.Add(dr);
                }

                grdTable.DataSource = dtLoad.DefaultView;
                grdTable.Columns["TableName"].Visible = false;
                grdTable.Columns["TitleSheet"].Caption = "Tên bảng để hiển thị";
                grdTable.Columns["Check"].Caption = "Chọn xuất";
                grdTable.Columns["Check"].Width = 110;
                grdTable.Columns["Check"].OptionsColumn.FixedWidth = true;
                //grdTable.Columns["TitleSheet"].Width = 130;
                //grdTable.Columns["TitleSheet"].OptionsColumn.FixedWidth = true;
                #endregion

                #region Load Step
                ChooseStep();
                #endregion
            }
            catch { }
            CommonLib.ShortKeyReg.RegisterHotKey(this, CloseForm, Keys.Escape);
        }
        void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Export
        private void btnExcel_Click(object sender, EventArgs e)
        {
            string Folder = "";
            try
            {
                #region Choose step
                _step++;
                ChooseStep();
                #endregion

                if (_step == 1)
                    LoadTree();

                #region Export
                if (!_continue)
                    return;
                this.sDlgSave.FileName = "UIS-" + ((DataView)grdTable.DataSource).ToTable().Rows[0]["TitleSheet"].ToString();
                if (this.sDlgSave.ShowDialog() != DialogResult.OK)
                {
                    _step = 0;
                    ChooseStep();
                    return;
                }

                if (File.Exists(sDlgSave.FileName))
                    if (XtraMessageBox.Show(this.QuestionMsg, this.TitleQuestionMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        _step = 0;
                        ChooseStep();
                        return;
                    }
                Folder = sDlgSave.FileName;

                SpreadsheetGear.IWorkbook exBook = SpreadsheetGear.Factory.GetWorkbook(new System.Globalization.CultureInfo("en-US"));

                int sheet = 0;
                DataSet dsTmp = new DataSet();
                                                
                foreach (DataRow dr in ((DataView)grdTable.DataSource).ToTable().Select("Check=1"))
                {
                    dsTmp.Tables.Add(_data.Tables[dr["TableName"].ToString()].Copy());
                }
                if (Folder.ToLower().EndsWith(".dbf"))
                {
                    string Lpath = Folder.Substring(0, Folder.Length - 4);
                    int Lindex = 0;
                    foreach (DataTable dt in dsTmp.Tables)
                    {
                        Application.DoEvents();
                        if (!_continue) break;
                        #region Remove column
                        string s = dt.TableName;
                        TreeNode node = null;
                        for (int i = 0; i < trvColumnView.Nodes.Count && node == null; i++)
                            if (trvColumnView.Nodes[i].Name == s)
                            {
                                node = trvColumnView.Nodes[i];
                                break;
                            }
                        if (node != null)
                        {
                            foreach (TreeNode child in node.Nodes)
                            {
                                if (!child.Checked)
                                    dt.Columns.Remove(child.Name);
                            }
                        }
                        #endregion
                        lblPercent.Text = "Đang xuất bảng " + dt.TableName + " ...";
                        Application.DoEvents();
                        #region Export
                        DBFExport dbf = new DBFExport(dt);
                        dbf.Export(Lpath + (Lindex == 0 ? "" : Lindex.ToString()) + ".dbf");
                        #endregion
                        Lindex++;
                    }
                    try
                    {
                        if (File.Exists(Folder))
                            System.Diagnostics.Process.Start("explorer.exe", @"/select, " + Folder);
                    }
                    catch { }
                    return;
                }
                else
                {

                    if (chkShowProcess.Checked) //hien thi tien trinh xuat
                    {
                        int iheader = 0;
                        foreach (DataTable dt in dsTmp.Tables)
                        {
                            Application.DoEvents();
                            if (!_continue) break;
                            #region Remove column
                            string s = dt.TableName;
                            TreeNode node = null;
                            for (int i = 0; i < trvColumnView.Nodes.Count && node == null; i++)
                                if (trvColumnView.Nodes[i].Name == s)
                                {
                                    node = trvColumnView.Nodes[i];
                                    break;
                                }
                            if (node != null)
                            {
                                foreach (TreeNode child in node.Nodes)
                                {
                                    if (!child.Checked)
                                        dt.Columns.Remove(child.Name);
                                }
                            }
                            #endregion
                            lblPercent.Text = "Đang xuất bảng " + dt.TableName + " ...";
                            Application.DoEvents();
                            #region Export

                            #region Create worksheet
                            SpreadsheetGear.IWorksheet exSheet = null;
                            if (exBook.Worksheets.Count < sheet + 1)
                            {
                                exSheet = exBook.Worksheets.Add();
                            }
                            else
                            {
                                exSheet = exBook.Worksheets[sheet];
                            }

                            try
                            {
                                exSheet.Name = _mapCol.GetCaption(dt.TableName, true);
                            }
                            catch { }
                            #endregion

                            #region Export header
                            int ih = 0;

                            if (Header.Length > iheader)
                                if (Header[iheader].Length > 0)
                                {
                                    for (ih = 0; ih < Header[iheader].Length; ih++)
                                    {
                                        if (Header[iheader][ih].Length > 0)
                                        {
                                            SpreadsheetGear.IRange r = exSheet.Cells[ih, 0, ih, Header[iheader][ih].Length - 1];
                                            r.NumberFormat = "@";
                                            if (Header[iheader][ih] != null)
                                            {
                                                r.Value = Header[iheader][ih];
                                                try
                                                {
                                                    r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                                                    r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                                    r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                                                    r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                                                    r.Interior.ColorIndex = 18;
                                                    r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                                                    r.Interior.PatternColorIndex = -4105;
                                                    r.Font.Bold = true;
                                                    r.RowHeight = 16;
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                            iheader++;

                            if (chkIncludeHeader.Checked)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    SpreadsheetGear.IRange r = exSheet.Cells[ih, i];
                                    r.NumberFormat = "@";
                                    r.Value = _mapCol.GetCaption(dt.Columns[i], true);
                                    try
                                    {
                                        r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                                        r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                        r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                                        r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                                        r.Interior.ColorIndex = 18;
                                        r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                                        r.Interior.PatternColorIndex = -4105;
                                        r.Font.Bold = true;
                                        r.RowHeight = 16;
                                    }
                                    catch { }
                                }
                            }
                            #endregion

                            #region Export value
                            int count = dt.Rows.Count;
                            if (chkIncludeHeader.Checked)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (dt.Columns[i].DataType == typeof(string))
                                    {
                                        exSheet.Cells[ih + 1, i].EntireColumn.NumberFormat = "@";
                                    }
                                    else if (dt.Columns[i].DataType == typeof(DateTime))
                                    {
                                        exSheet.Cells[ih + 1, i].EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                }

                                exSheet.Cells[ih + 1, 0, ih + 1 + 2, dt.Columns.Count - 1].CopyFromDataTable(dt, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                            }
                            else
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (dt.Columns[i].DataType == typeof(string))
                                    {
                                        exSheet.Cells[ih, i].EntireColumn.NumberFormat = "@";
                                    }
                                    else if (dt.Columns[i].DataType == typeof(DateTime))
                                    {
                                        exSheet.Cells[ih, i].EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                }

                                exSheet.Cells[ih, 0, ih + 2, dt.Columns.Count - 1].CopyFromDataTable(dt, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                            }
                            #endregion

                            #endregion

                            sheet++;
                        }
                        #region Custom
                        WorkSheet w = new WorkSheet(exBook);
                        OnCustomModifySheet(this, new CustomModifySheetArgs(w));
                        #endregion
                    }
                    else
                    {
                        int iheader = 0;
                        foreach (DataTable dt in dsTmp.Tables)
                        {
                            #region Remove column
                            string s = dt.TableName;
                            TreeNode node = null;
                            for (int i = 0; i < trvColumnView.Nodes.Count && node == null; i++)
                                if (trvColumnView.Nodes[i].Name == s)
                                {
                                    node = trvColumnView.Nodes[i];
                                    break;
                                }
                            if (node != null)
                            {
                                foreach (TreeNode child in node.Nodes)
                                {
                                    if (!child.Checked)
                                        dt.Columns.Remove(child.Name);
                                }
                            }
                            #endregion

                            lblPercent.Text = "Đang xuất bảng " + dt.TableName + " ...";
                            Application.DoEvents();
                            #region Export

                            #region Create worksheet
                            SpreadsheetGear.IWorksheet exSheet = null;
                            if (exBook.Worksheets.Count < sheet + 1)
                            {
                                exSheet = exBook.Worksheets.Add();
                            }
                            else
                            {
                                exSheet = exBook.Worksheets[sheet];
                            }

                            try
                            {
                                exSheet.Name = _mapCol.GetCaption(dt.TableName, true);
                            }
                            catch { }
                            #endregion

                            #region Export header
                            int ih = 0;
                            if (Header.Length > iheader)
                                if (Header[iheader].Length > 0)
                                {
                                    for (ih = 0; ih < Header[iheader].Length; ih++)
                                    {
                                        if (Header[iheader][ih].Length > 0)
                                        {
                                            SpreadsheetGear.IRange r = exSheet.Cells[ih, 0, ih, Header[iheader][ih].Length - 1];
                                            r.NumberFormat = "@";
                                            if (Header[iheader][ih] != null)
                                            {
                                                r.Value = Header[iheader][ih];
                                                try
                                                {
                                                    r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                                                    r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                                    r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                                                    r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                                                    r.Interior.ColorIndex = 18;
                                                    r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                                                    r.Interior.PatternColorIndex = -4105;
                                                    r.Font.Bold = true;
                                                    r.RowHeight = 16;
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                            iheader++;

                            if (chkIncludeHeader.Checked)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    SpreadsheetGear.IRange r = exSheet.Cells[ih, i];
                                    r.NumberFormat = "@";
                                    r.Value = _mapCol.GetCaption(dt.Columns[i], true);
                                    try
                                    {
                                        r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                                        r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                        r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                                        r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                                        r.Interior.ColorIndex = 18;
                                        r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                                        r.Interior.PatternColorIndex = -4105;
                                        r.Font.Bold = true;
                                        r.RowHeight = 16;
                                    }
                                    catch { }
                                }
                            }
                            #endregion

                            #region Export value
                            int count = dt.Rows.Count;
                            if (chkIncludeHeader.Checked)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (dt.Columns[i].DataType == typeof(string))
                                    {
                                        exSheet.Cells[ih + 1, i].EntireColumn.NumberFormat = "@";
                                    }
                                    else if (dt.Columns[i].DataType == typeof(DateTime))
                                    {
                                        exSheet.Cells[ih + 1, i].EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                }

                                exSheet.Cells[ih + 1, 0, ih + 1, dt.Columns.Count - 1].CopyFromDataTable(dt, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                            }
                            else
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (dt.Columns[i].DataType == typeof(string))
                                    {
                                        exSheet.Cells[ih, i].EntireColumn.NumberFormat = "@";
                                    }
                                    else if (dt.Columns[i].DataType == typeof(DateTime))
                                    {
                                        exSheet.Cells[ih, i].EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                                    }
                                }
                                exSheet.Cells[ih, 0, ih, dt.Columns.Count - 1].CopyFromDataTable(dt, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                            }
                            #endregion

                            #endregion

                            sheet++;
                        }
                        #region Custom
                        WorkSheet w = new WorkSheet(exBook);
                        OnCustomModifySheet(this, new CustomModifySheetArgs(w));
                        #endregion
                    }

                    if (_continue)
                    {
                        exBook.SaveAs(Folder, SpreadsheetGear.FileFormat.XLS97);
                        exBook.Close();
                        lblPercent.Text = "Hoàn thành...";
                        lblPercent.Update();
                        XtraMessageBox.Show(this.MsgDlgResult, this.TitleDlgResult, this.ButtonDlgResult, this.IconDlgResult);
                        this.Close();
                        if (Folder != "")
                        {
                            try
                            {
                                if (File.Exists(Folder))
                                    System.Diagnostics.Process.Start("explorer.exe", @"/select, " + Folder);
                            }
                            catch { }
                        }
                    }
                }

                _step = 0;
                ChooseStep();

                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.TitleDlgError, this.ButtonDlgError, this.IconDlgError);
                _step = 0;
                ChooseStep();
            }
        }

        #endregion

        #region ChooseStep()
        private void ChooseStep()
        {
            if (_step == 0)
            {
                grpListOfTable.Visible = true;
                grpListOfTable.Text = "Danh sách các bảng";
                grdTable.Visible = true;
                trvColumnView.Visible = false;
                lblProcess.Visible = false;
                lblPercent.Visible = false;
                btnBack.Enabled = false;
                chkIncludeHeader.Visible = false;
                chkShowProcess.Visible = false;
                btnNext.Enabled = _data == null ? false : true;
                btnNext.Text = "Tiếp";
                _continue = false;
            }
            else if (_step == 1)
            {
                if (((DataView)grdTable.DataSource).ToTable().Select("Check=1").Length == 0)
                {
                    XtraMessageBox.Show("Phải chọn ít nhất một bảng để xuất", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _step = 0;
                    return;
                }
                grpListOfTable.Visible = true;
                grpListOfTable.Text = "Danh sách các cột trong bảng";
                grdTable.Visible = false;
                trvColumnView.Visible = true;
                lblProcess.Visible = false;
                lblPercent.Visible = false;
                btnBack.Enabled = true;
                chkIncludeHeader.Visible = true;
                chkShowProcess.Visible = true;
                btnNext.Enabled = _data == null ? false : true;
                btnNext.Text = "Hoàn tất";
                _continue = false;
            }
            else if (_step == 2)
            {
                grpListOfTable.Visible = false;
                grdTable.Visible = false;
                trvColumnView.Visible = false;
                lblProcess.Visible = true;
                lblPercent.Visible = true;
                btnBack.Enabled = false;
                chkIncludeHeader.Visible = false;
                chkShowProcess.Visible = false;
                btnNext.Text = "Hủy bỏ";
                _continue = true;
            }
            else
            {
                _step = 0;
                ChooseStep();
            }
            this.Update();
        }
        #endregion

        #region btnBack_Click
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (_step > 0)
                    _step--;
                ChooseStep();
            }
            catch { }
        }
        #endregion

        #region trvColumnView_AfterCheck
        private void trvColumnView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null &&  _isCheckAlsoChild)
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
        }
        #endregion

        #region LoadTree()
        private void LoadTree()
        {
            #region Remove All Node
            while (trvColumnView.Nodes.Count > 0)
                trvColumnView.Nodes.RemoveAt(0);
            #endregion

            #region Add node
            DataTable dtTree = new DataTable();
            dtTree.Columns.Add("Column", typeof(string));
            foreach (DataRow dr in ((DataView)grdTable.DataSource).ToTable().Select("Check=1"))
            {
                DataTable dt = _data.Tables[dr["TableName"].ToString()];
                TreeNode node = trvColumnView.Nodes.Add(dr["TableName"].ToString(),dr["TitleSheet"].ToString());
                int countCheck = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    if (VisibleColumn != null)
                    {
                        if (VisibleColumn.Contains(col.ColumnName))
                        {
                            TreeNode nodeChild = node.Nodes.Add(col.ColumnName, _mapCol.GetCaption(col, true));
                            nodeChild.Checked = true;
                            countCheck++;
                        }
                        else
                            node.Nodes.Add(col.ColumnName,_mapCol.GetCaption(col, true));
                    }
                    else
                    {
                        TreeNode nodeChild = node.Nodes.Add(col.ColumnName,_mapCol.GetCaption(col, true));
                        nodeChild.Checked = true;
                        countCheck++;
                    }
                }
                _isCheckAlsoChild = false;
                if (countCheck == dt.Columns.Count)
                    node.Checked = true;
                else
                    node.Checked = false;
                _isCheckAlsoChild = true;
            }
            #endregion
        }
        #endregion

        public event CustomModifySheetHandler CustomModifySheet;

        protected void OnCustomModifySheet(object sender,CustomModifySheetArgs e)
        {
            if (CustomModifySheet != null)
                CustomModifySheet(sender, e);
        }
    }

    public delegate void CustomModifySheetHandler(object sender,CustomModifySheetArgs e);

    public class Cells
    {
        SpreadsheetGear.IWorkbook _workBook = null;

        internal Cells(SpreadsheetGear.IWorkbook workBook)
        {
            this._workBook = workBook;
        }

        public SpreadsheetGear.IRange this[int row, int col]
        {
            get
            {
                int r = 0, c = 0;
                if (row > 0) r = row - 1;
                if (col > 0) c = col - 1;
                return this._workBook.Worksheets[0].Cells[r, c];
            }
        }
    }

    public class WorkSheet
    {
        SpreadsheetGear.IWorkbook _workBook = null;
        Cells _cells = null;

        internal WorkSheet(SpreadsheetGear.IWorkbook workBook)
        {
            this._workBook = workBook;
            this._cells = new Cells(workBook);
        }

        public SpreadsheetGear.IWorksheet ISheet
        {
            get
            {
                return this._workBook.Worksheets[0];

            }
        }

        public Cells Cells
        {
            get { return this._cells; }
        }

        public SpreadsheetGear.IRange get_Range(SpreadsheetGear.IRange cell1, SpreadsheetGear.IRange cell2)
        {
            return this.ISheet.Cells[cell1.Row, cell1.Column, cell2.Row, cell2.Column];
        }

        public SpreadsheetGear.IWorksheets Worksheets
        {
            get
            {
                return this._workBook.Worksheets;
            }
        }
    }

    public class CustomModifySheetArgs : EventArgs
    {
        WorkSheet _sheet = null;

        public WorkSheet Sheet
        {
            get { return _sheet; }
        }        

        internal CustomModifySheetArgs(WorkSheet sheet)
        {
            _sheet= sheet;
        }
    }

    public class ExportToExcell
    {
        #region Export use SpreadsheetGear

        #region Export(HeaderExcelSheets Header, DataSet dsInput)
        public static bool Export(string[][] Header, DataTable dtInput)
        {
            return Export(Header, dtInput, null);
        }
        #endregion

        #region Export(string[][] Header, DataTable dtInput, string FileName)
        public static bool Export(string[][] Header, DataTable dtInput, string FileName)
        {
            return Export(Header, dtInput, FileName, null);
        }

        public static bool Export(string[][] Header, DataTable dtInput, string FileName, Predicate<CustomModifySheetArgs> match)
        {
            if (dtInput == null) return false;
            HeaderExcelSheets headers = new HeaderExcelSheets();
            if (Header != null)
            {
                foreach (var h in Header)
                {
                    HeaderExcelSheet hh = new HeaderExcelSheet();
                    hh.DataTableName = dtInput.TableName;
                    hh.Captions.Add(new HeaderExcels(h));
                    headers.Add(hh);
                }                
            }
            else
            {
                HeaderExcelSheet hh = new HeaderExcelSheet();
                hh.DataTableName = dtInput.TableName;
                List<string> cols = new List<string>();
                foreach (DataColumn col in dtInput.Columns)
                {
                    cols.Add(col.Caption == "" ? col.ColumnName : col.Caption);
                }
                hh.Captions.Add(new HeaderExcels(cols.ToArray()));
                headers.Add(hh);
            }
            return Export(headers, dtInput, FileName, match);
        }
        #endregion

        #region Export(HeaderExcelSheets Header, DataTable dtInput)
        private static bool Export(HeaderExcelSheets Header, DataTable dtInput)
        {
            if (dtInput == null) return false;
            return Export(Header, dtInput, null);
        }
        #endregion

        #region Export(HeaderExcelSheets Header, DataTable dtInput, string FileName)
        private static bool Export(HeaderExcelSheets Header, DataTable dtInput, string FileName)
        {
            return Export(Header, dtInput, FileName, null);
        }

        private static bool Export(HeaderExcelSheets Header, DataTable dtInput, string FileName, Predicate<CustomModifySheetArgs> match)
        {            
            if (dtInput == null) return false;
            DataSet ds = new DataSet();
            if (dtInput.DataSet != null)
                ds.Tables.Add(dtInput.Copy());
            else
                ds.Tables.Add(dtInput);
            foreach (var h in Header)
            {
                h.DataTableName = dtInput.TableName;
            }
            return Export(Header, ds, FileName, match);
        }
        #endregion

        #region Export(HeaderExcelSheets Header, DataSet dsInput)
        public static bool Export(HeaderExcelSheets Header, DataSet dsInput)
        {
            return Export(Header, dsInput, null);
        }
        #endregion

        #region Export(HeaderExcelSheets Header, DataSet dsInput, string FileName)
        public static bool Export(HeaderExcelSheets Header, DataSet dsInput, string FileName)
        {
            return Export(Header, dsInput, FileName, null);
        }

        public static bool Export(HeaderExcelSheets Header, DataSet dsInput, string FileName, Predicate<CustomModifySheetArgs> match)
        {
            bool success = false;
            string Folder = "";
            SaveFileDialog sDlgSave = new SaveFileDialog();
            if (FileName != null && FileName != "")
                sDlgSave.FileName = FileName;
            else
                sDlgSave.FileName = "UIS";
            sDlgSave.Filter = "Excel File|*.xls|FoxPro file|*.dbf";
            sDlgSave.OverwritePrompt = false;
            sDlgSave.RestoreDirectory = true;
            sDlgSave.FilterIndex = 0;
            if (sDlgSave.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            else
            {
                try
                {
                    if (File.Exists(sDlgSave.FileName))
                        if (XtraMessageBox.Show("Có một tập tin cùng tên, bạn có muốn ghi đè không?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return false;
                        }
                    Folder = sDlgSave.FileName;

                    CommonLib.ImportAndExport.StatusForm.Self.UpdateStatus("Đang xuất dữ liệu...");
                    if (Folder.ToLower().EndsWith(".dbf"))
                    {
                        string Lpath = Folder.Substring(0, Folder.Length - 4);
                        int Lindex = 0;
                        foreach (DataTable dt in dsInput.Tables)
                        {
                            Application.DoEvents();                            
                            #region Export
                            DBFExport dbf = new DBFExport(dt);
                            dbf.Export(Lpath + (Lindex == 0 ? "" : Lindex.ToString()) + ".dbf");
                            #endregion
                            Lindex++;
                        }
                        success = true;
                    }
                    else
                    {
                        if (Header == null)
                            Header = new HeaderExcelSheets();

                        SpreadsheetGear.IWorkbook wbook = SpreadsheetGear.Factory.GetWorkbook(new System.Globalization.CultureInfo("en-US"));
                        for (int i = 0; i < dsInput.Tables.Count; i++)
                        {
                            List<HeaderExcels> captions = new List<HeaderExcels>();
                            var header = Header[dsInput.Tables[i].TableName];
                            if (header != null)
                            {
                                captions = header.Captions;
                            }
                            else
                            {
                                HeaderExcels caps = new HeaderExcels();
                                foreach (DataColumn col in dsInput.Tables[i].Columns)
                                {
                                    caps.Add(col.Caption == "" ? col.ColumnName : col.Caption);
                                }
                                captions = new List<HeaderExcels>();
                                captions.Add(caps);
                            }

                            SpreadsheetGear.IWorksheet wSheet = null;
                            if (i < wbook.Worksheets.Count)
                            {
                                wSheet = wbook.Worksheets[i];
                            }
                            else
                            {
                                wSheet = wbook.Worksheets.Add();
                            }
                            wSheet.Name = dsInput.Tables[i].TableName;

                            int row = 0;
                            foreach (DataColumn col in dsInput.Tables[i].Columns)
                            {
                                SpreadsheetGear.IRange r = wSheet.Cells[row, col.Ordinal];
                                if (col.DataType == typeof(string))
                                    r.EntireColumn.NumberFormat = "@";
                                else if (col.DataType == typeof(DateTime))
                                    r.EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                            }
                            foreach (var caps in captions)
                            {
                                for (int i1 = 0; i1 < caps.Count; i1++)
                                {
                                    SpreadsheetGear.IRange r = wSheet.Cells[row, i1];
                                    r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                                    r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                                    r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                                    r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                                    r.Interior.ColorIndex = 18;
                                    r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                                    r.Interior.PatternColorIndex = -4105;
                                    r.Font.Bold = true;
                                    r.Value = caps[i1].Caption;
                                }
                                row++;
                            }

                            wSheet.Cells[row, 0, row + 2, dsInput.Tables[i].Columns.Count].CopyFromDataTable(dsInput.Tables[i], SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                            if (match != null)
                            {
                                WorkSheet w = new WorkSheet(wbook);
                                match(new CustomModifySheetArgs(w));
                            }
                        }

                        wbook.SaveAs(Folder, SpreadsheetGear.FileFormat.XLS97);
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    string Error = ex.Message;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        Error += "\r\n" + ex.Message;
                    }
                    XtraMessageBox.Show("Xuất dữ liệu có lỗi: " + Error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CommonLib.ImportAndExport.StatusForm.Self.Hide();
                    return false;
                }
            }

            CommonLib.ImportAndExport.StatusForm.Self.Hide();
            if (success && Folder != "")
            {
                try
                {
                    if (File.Exists(Folder))
                        System.Diagnostics.Process.Start("explorer.exe", @"/select, " + Folder);
                }
                catch { }
            }
            return success;
        }
        #endregion

        #endregion

        #region Excel Editor
        public static DataTable EditByExcel(DataTable dtData)
        {
            try
            {
                var eCols = from c in dtData.Columns.Cast<DataColumn>()
                            select new { ColumnName = c.ColumnName, Caption = c.ColumnName };

                SpreadsheetGear.IWorkbook wbook = SpreadsheetGear.Factory.GetWorkbook(new System.Globalization.CultureInfo("en-US"));
                SpreadsheetGear.IWorksheet wSheet = null;
                int i = 0;
                if (i < wbook.Worksheets.Count)
                {
                    wSheet = wbook.Worksheets[i];
                }
                else
                {
                    wSheet = wbook.Worksheets.Add();
                }
                wSheet.Name = dtData.TableName;

                int row = 0;
                foreach (DataColumn col in dtData.Columns)
                {
                    SpreadsheetGear.IRange r = wSheet.Cells[row, col.Ordinal];
                    if (col.DataType == typeof(string))
                        r.EntireColumn.NumberFormat = "@";
                    else if (col.DataType == typeof(DateTime))
                        r.EntireColumn.NumberFormat = "yyyy-MM-dd HH:mm:ss";
                }

                var captions = eCols.ToList();

                for (int i1 = 0; i1 < captions.Count; i1++)
                {
                    SpreadsheetGear.IRange r = wSheet.Cells[row, i1];
                    r.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                    r.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                    r.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;
                    r.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                    r.Interior.ColorIndex = 18;
                    r.Interior.Pattern = SpreadsheetGear.Pattern.Solid;
                    r.Interior.PatternColorIndex = -4105;
                    r.Font.Bold = true;
                    r.Value = captions[i1].Caption;
                }
                row++;

                wSheet.Cells[row, 0, row + 2, dtData.Columns.Count].CopyFromDataTable(dtData, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);

                string fileTemp = GetTempFilePathWithExtension(".xls");

                wbook.SaveAs(fileTemp, SpreadsheetGear.FileFormat.XLS97);
                FileInfo f1Date = new FileInfo(fileTemp);
                DateTime currentEditDate = f1Date.LastWriteTime;
                System.Diagnostics.Process designer = System.Diagnostics.Process.Start(fileTemp);
                designer.WaitForInputIdle();
                designer.WaitForExit();
                FileInfo f2Date = new FileInfo(fileTemp);
                DateTime d2 = f2Date.LastWriteTime;
                if (d2 > currentEditDate)
                {
                    DataTable dt = CommonLib.ExcelBL.GetSheetContent(fileTemp, dtData.TableName + "$");
                    File.Delete(fileTemp);
                    return dt;
                }
                File.Delete(fileTemp);
            }
            catch { }
            return null;
        }

        static string GetTempFilePathWithExtension(string extension)
        {
            var path = Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString() + extension;
            return Path.Combine(path, fileName);
        }
        #endregion
    }

    #region Header
    public class HeaderExcel
    {
        public string Caption { get; set; }
        public HeaderExcel(string caption)
        {
            this.Caption = caption;
        }
    }

    public class HeaderExcels : List<HeaderExcel>
    {        
        public HeaderExcels(params string[] captions)
        {
            foreach (var s in captions)
            {
                this.Add(new HeaderExcel(s));
            }
        }
        public void Add(string caption)
        {
            this.Add(new HeaderExcel(caption));
        }
    }

    public class HeaderExcelSheet
    {
        public HeaderExcelSheet()
        {
            this.Captions = new List<HeaderExcels>();
        }
        public string DataTableName { get; set; }
        public List<HeaderExcels> Captions { get; set; }
    }

    public class HeaderExcelSheets : List<HeaderExcelSheet>
    {
        public HeaderExcelSheet this[string dataTableName]
        {
            get
            {
                HeaderExcelSheet hs = new HeaderExcelSheet();
                foreach (var sh in this.Where(h => String.Compare(h.DataTableName, dataTableName, true) == 0))
                {
                    foreach (var sh1 in sh.Captions)
                    {
                        hs.Captions.Add(sh1);
                    }
                }
                return hs;

            }
        }
    }
    #endregion
}
