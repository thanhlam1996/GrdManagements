using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using DevExpress.XtraEditors;

namespace CommonLib.UserControls
{
    public class DataGridViewExtend: DataGridView
    {
        #region Init
        public DataGridViewExtend()
        {
            InitializeComponent();
            this.RowHeadersVisible = false;
            this.BackgroundColor = Color.White;
            this.mnuAutoResize.Click += new EventHandler(mnuAutoResize_Click);
            this.mnuCheckAll.Click += new EventHandler(mnuCheckAll_Click);
            this.mnuChooseVisibleColumn.Click += new EventHandler(mnuChooseVisibleColumn_Click);

        }             

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAutoResize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChooseVisibleColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoResize,
            this.mnuCheckAll,
            this.mnuChooseVisibleColumn});
            this.cmsMenu.Name = "cmsAutoResize";
            this.cmsMenu.Size = new System.Drawing.Size(231, 70);
            // 
            // mnuAutoResize
            // 
            this.mnuAutoResize.Name = "mnuAutoResize";
            this.mnuAutoResize.Size = new System.Drawing.Size(230, 22);
            this.mnuAutoResize.Text = "Tự động chỉnh độ rộng các cột";
            // 
            // mnuCheckAll
            // 
            this.mnuCheckAll.Name = "mnuCheckAll";
            this.mnuCheckAll.Size = new System.Drawing.Size(230, 22);
            this.mnuCheckAll.Text = "Chọn tất cả";
            // 
            // mnuChooseVisibleColumn
            // 
            this.mnuChooseVisibleColumn.Image = global::CommonLib.Properties.Resources.tableprop;
            this.mnuChooseVisibleColumn.Name = "mnuChooseVisibleColumn";
            this.mnuChooseVisibleColumn.Size = new System.Drawing.Size(230, 22);
            this.mnuChooseVisibleColumn.Text = "Chọn cột hiển thị";
            this.mnuChooseVisibleColumn.Visible = false;
            // 
            // DataGridViewExtend
            // 
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Variables

        private System.ComponentModel.IContainer components;
        private ContextMenuStrip cmsMenu;
        private ToolStripMenuItem mnuAutoResize;
        
        Color _colorUnEditableColumn = Color.OldLace;
        bool _showMenuAutoResize = false, _showMenuCheckAll = false, _showMenuChooseVisibleColumn = false, _checkAll = false, _useColorParent = true;

        private ToolStripMenuItem mnuCheckAll;
        private ToolStripMenuItem mnuChooseVisibleColumn;

        private bool _autoDateTimeControl = false;   
     
        string[] _columnsChoose=null;
        #endregion

        #region Properties

        #region UseColorParent
        public bool UseColorParent
        {
            get { return _useColorParent; }
            set { _useColorParent = value; }
        }
        #endregion

        #region IsAutoDateTimeControl
        /// <summary>
        /// Set auto create DateTimePicker when ValueType of column is DateTime
        /// </summary>
        public bool AutoDateTimeControl
        {
            get { return _autoDateTimeControl; }
            set { _autoDateTimeControl = value; }
        }
        #endregion

        #region ColorUnEditableColumn
        /// <summary>
        /// Set color for uneditable columns
        /// </summary>
        public Color ColorUnEditableColumn
        {
            get { return _colorUnEditableColumn; }
            set
            {
                _colorUnEditableColumn = value;
                SetColorUnEditableColumn(this, value);
            }
        }
        #endregion

        #region ShowMenuAutoResize
        /// <summary>
        /// Disable or enable menu auto resize column
        /// </summary>
        public bool ShowMenuAutoResize
        {
            get { return _showMenuAutoResize; }
            set { _showMenuAutoResize = value; }
        }
        #endregion

        #region ShowMenuCheckAll
        /// <summary>
        /// Disable or enable menu auto checkall for column 'Check'
        /// </summary>
        public bool ShowMenuCheckAll
        {
            get { return _showMenuCheckAll; }
            set { _showMenuCheckAll = value; }
        }
        #endregion

        #region ShowMenuChooseVisibleColumn
        /// <summary>
        /// Disable or enable menu auto checkall for column 'Check'
        /// </summary>
        public bool ShowMenuChooseVisibleColumn
        {
            get { return _showMenuChooseVisibleColumn; }
            set { _showMenuChooseVisibleColumn = value; }
        }
        #endregion

        #region ChangedData
        public DataTable ChangedData
        {
            get
            {
                if (this.DataSource != null)
                {
                    DataTable dt = null, dtSel = null;
                    try
                    {
                        dtSel = GetThisData();
                        dt = dtSel.Clone();
                        
                        foreach (DataColumn col in dt.Columns)
                            col.ReadOnly = false;

                        foreach (DataRow dr in dtSel.Rows)
                        {
                            if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                            {
                                DataRow drAdd = dt.NewRow();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    drAdd[col.ColumnName] = dr[col.ColumnName];
                                }
                                dt.Rows.Add(drAdd);
                            }
                        }
                    }
                    catch
                    {
                        dt = null;
                    }
                    return dt;
                }
                else
                    return null;
            }
        }
        #endregion

        #region RowHeaderVisible
        public bool RowHeaderVisible
        {
            get { return base.RowHeadersVisible; }
            set { this.RowHeadersVisible = value; }
        }
        #endregion

        #region ColumnsChoose
        /// <summary>
        /// Set column for visible by user
        /// </summary>
        public string[] ColumnsChoose
        {
            set { _columnsChoose = value; }
            get { return _columnsChoose; }
        }
        #endregion
        #endregion

        #region Local Function

        #region Get table
        private DataTable GetThisData()
        {
            DataTable dt = null;
            if (this.DataSource.GetType() == typeof(DataView))
            {
                dt = ((DataView)this.DataSource).Table;
            }
            else if (this.DataSource.GetType() == typeof(DataView))
            {
                dt = (DataTable)this.DataSource;
            }
            else
                throw new Exception("Please set DataSource is DataView or DataTable");

            return dt;
        }
        #endregion

        #region SetColor UnEditable column
        private void SetColorUnEditableColumn(DataGridView dtg, Color color)
        {
            if (dtg.DataSource != null)
            {
                foreach (DataGridViewColumn col in dtg.Columns)
                {
                    if (col.ReadOnly)
                        col.CellTemplate.Style.BackColor = color;
                }
            }
        }
        #endregion 

        #region mnuAutoResize_Click
        private void mnuAutoResize_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowMenuAutoResize)
                {
                    if (this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
                        this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    else
                        this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region mnuCheckAll_Click
        private void mnuCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowMenuCheckAll && this.DataSource != null)
                {
                    if (this.Columns.Contains("Check"))
                        if (this.Columns["Check"].ValueType == typeof(bool))
                        {
                            if (this.Rows.Count > 0)
                                _checkAll = !_checkAll;

                            foreach (DataGridViewRow dr in this.Rows)
                            {
                                if (dr.Cells["Check"].Value.ToString() != _checkAll.ToString() && dr.Visible)
                                {
                                    if(dr.Cells["Check"].Selected)
                                        dr.Cells["Check"].Selected = false;
                                    dr.Cells["Check"].Value = _checkAll;
                                    OnCellValueChanged(new DataGridViewCellEventArgs(dr.Cells["Check"].OwningColumn.Index, dr.Index));
                                }
                                else
                                {

                                }
                            }
                        }
                }
            }
            catch { }
        } 
        #endregion

        #region mnuChooseVisibleColumn_Click
        void mnuChooseVisibleColumn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowMenuChooseVisibleColumn)
                {
                    DataGridViewColumn[] col = new DataGridViewColumn[] { };
                    if (_columnsChoose != null)
                    {
                        Array.Resize(ref col, _columnsChoose.Length);
                        for (int i = 0; i < _columnsChoose.Length; i++)
                        {
                            if (this.Columns.Contains(_columnsChoose[i]))
                            {
                                col[i] = this.Columns[_columnsChoose[i]];
                            }
                        }
                    }
                    else
                    {
                        Array.Resize(ref col, this.Columns.Count);
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            col[i] = this.Columns[i];
                        }
                    }
                    frmVisibleColumns frm = new frmVisibleColumns(col);
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        string[] scol = frm.ColumnChoosed();
                        foreach (DataGridViewColumn colChoose in this.Columns)
                            if (scol.Contains(colChoose.Name))
                                colChoose.Visible = true;
                            else if(_columnsChoose.Contains(colChoose.Name))
                                colChoose.Visible = false;
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Show menu

        private void ShowMenu(object EventArgs)
        {
            bool isHeader = false;
            int type = -1;
            if (EventArgs == null)
                return;
            if (EventArgs.GetType() == typeof(MouseEventArgs))
            {
                if (ShowMenuAutoResize)
                {
                    type = 0;
                }
            }
            else if (EventArgs.GetType() == typeof(DataGridViewCellMouseEventArgs))
            {
                isHeader = true;
                DataGridViewColumn col = this.Columns["Check"];
                if (col != null)
                {
                    if (((DataGridViewCellMouseEventArgs)EventArgs).ColumnIndex == col.Index)
                    {
                        if (ShowMenuCheckAll)
                        {
                            type = 1;
                        }
                    }
                    else
                    {
                        type = 0;
                    }
                }
                else
                    type = 0;
            }

            try
            {
                bool IsRightClick = false;
                if (type == 0)
                {
                    if (((MouseEventArgs)EventArgs).Button == MouseButtons.Right)
                        IsRightClick = true;
                }
                else if (type == 1)
                {
                    if (((DataGridViewCellMouseEventArgs)EventArgs).Button == MouseButtons.Right)
                        IsRightClick = true;
                }

                if (IsRightClick)
                {
                    if (type == 0)
                    {
                        if ((ShowMenuChooseVisibleColumn || ShowMenuAutoResize) && isHeader)
                            cmsMenu.Show(MousePosition.X + 3, MousePosition.Y + 3);
                        if (ShowMenuAutoResize && isHeader)
                            mnuAutoResize.Visible = true;
                        else
                            mnuAutoResize.Visible = false;
                        mnuCheckAll.Visible = false;
                        //mnuChooseVisibleColumn.Visible = false;
                        if (ShowMenuChooseVisibleColumn && isHeader)
                            mnuChooseVisibleColumn.Visible = true;
                        else
                            mnuChooseVisibleColumn.Visible = false;
                        cmsMenu.AutoSize = true;
                        mnuAutoResize.Checked = this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill ? true : false;
                    }
                    else if (type == 1)
                    {
                        DataView dv = this.DataSource as DataView;
                        DataTable dt = dv.ToTable();
                        if (dt == null)
                            return;

                        if (this.Columns.Contains("Check"))
                        {
                            if (this.Columns["Check"].ValueType != typeof(bool))
                                return;
                        }
                        else
                            return;

                        //Update current cell if not change
                        DataGridViewCell cell = this.CurrentCell;
                        if (cell != null)
                            if (cell.OwningColumn.Name == "Check")
                            {
                                if (cell.IsInEditMode)
                                    this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            }
                        //

                        DataGridViewColumn col = this.Columns["Check"];
                        if (((DataGridViewCellMouseEventArgs)EventArgs).ColumnIndex == col.Index)
                        {
                            DataRow[] drChecked = dt.Select("Check = true");
                            if (drChecked.Length < this.Rows.Count)
                            {
                                mnuCheckAll.Text = "Chọn tất cả";
                                _checkAll = false;
                            }
                            else
                            {
                                mnuCheckAll.Text = "Bỏ chọn tất cả";
                                _checkAll = true;
                            }
                            cmsMenu.Show(MousePosition.X + 3, MousePosition.Y + 3);
                            mnuAutoResize.Visible = false;
                            mnuCheckAll.Visible = true;
                            mnuChooseVisibleColumn.Visible = false;
                            cmsMenu.AutoSize = false;
                            cmsMenu.Width = 110;
                            mnuCheckAll.Checked = _checkAll;
                        }
                    }
                }
            }
            catch { }
        }
        #endregion        

        #region Create DateTimePicker control
        private void CreateDateTimePicker()
        {
            if (this.DataSource != null)
            {
                foreach(DataGridViewColumn col in this.Columns)
                    if (col.ValueType == typeof(DateTime))
                    {
                        col.CellTemplate = new CalendarCell();
                    }
            }
        }
        #endregion

        #endregion

        #region Override

        #region OnColumnStateChanged
        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            try
            {
                base.OnColumnStateChanged(e);
                if (this.Parent.Visible)
                    if (e.Column.ReadOnly && e.Column.Visible)
                        e.Column.CellTemplate.Style.BackColor = this.ColorUnEditableColumn;
                    else if(e.Column.Visible)
                        e.Column.CellTemplate.Style.BackColor = this.BackgroundColor;
                DataGridViewColumn col = e.Column as DataGridViewColumn;
                if (col.ValueType == typeof(bool))
                {
                    //col.HeaderCell = new DatagridViewCheckBoxHeaderCell();
                }
            }
            catch { }
        }
        #endregion

        #region OnMouseClick
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //if (this.ShowMenuAutoResize && !cmsMenu.Visible)
            //{
            //    ShowMenu(e);
            //}
        }
        #endregion

        #region OnColumnHeaderMouseClick
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnColumnHeaderMouseClick(e);
            if ((this.ShowMenuChooseVisibleColumn || this.ShowMenuCheckAll || this.ShowMenuAutoResize) && !cmsMenu.Visible)
            {
                ShowMenu(e);
            }
        }
        #endregion

        #region OnDataSourceChanged
        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            _checkAll = false;
            if (AutoDateTimeControl)
            {
                CreateDateTimePicker();
            }

            foreach (DataGridViewColumn column in this.Columns)
            {
                if(UseColorParent)
                    column.HeaderCell.Style.BackColor = this.Parent.BackColor;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        #endregion

        #region OnCellValueChanged
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
        }
        #endregion 
        
        #region OnDataError
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                //base.OnDataError(displayErrorDialogIfNoHandler, e);
                string msg = GlobalLib.GetError(Functions.PscException(e.Exception.Message, 3));
                XtraMessageBox.Show(msg == "" ? e.Exception.Message : msg, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            catch { }
            
        }
        #endregion

        #region OnCellBeginEdit
        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);
            this[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.PowderBlue;
        }
        #endregion

        #region OnCellEndEdit
        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            this[e.ColumnIndex, e.RowIndex].Style.BackColor = this.BackgroundColor;
        }
        #endregion

        #region OnCellLeave
        protected override void OnCellLeave(DataGridViewCellEventArgs e)
        {
            base.OnCellLeave(e);
            CalendarEditingControl ctrl = this.EditingControl as CalendarEditingControl;
            if (ctrl != null && (this[e.ColumnIndex, e.RowIndex].Value == null || this[e.ColumnIndex, e.RowIndex].Value == DBNull.Value))
            {
                this[e.ColumnIndex, e.RowIndex].Value = ctrl.Value;
            }
        }
        #endregion

        #region OnKeyDown
        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            CalendarEditingControl ctrl = this.EditingControl as CalendarEditingControl;
            if (e.KeyData == Keys.Enter && ctrl != null)
            {
                if (this.CurrentCell != null)
                    this.CurrentCell.Value = ctrl.Value;
            }
            base.OnKeyDown(e);
        }
        #endregion

        #endregion

        #region public function

        #region GetSelectedRowToDataRow
        public DataRow[] GetSelectedRowToDataRow()
        {
            DataGridViewSelectedRowCollection row = this.SelectedRows;
            DataRow[] dr = new DataRow[] { };
            Array.Resize(ref dr, row.Count);

            DataTable dt = null;
            if (this.DataSource is DataTable)
                dt = this.DataSource as DataTable;
            else if (this.DataSource is DataView)
                dt = ((DataView)this.DataSource).Table;

            if (dt == null) return dr;

            for (int i = 0; i < row.Count; i++)
            {
                dr[i] = dt.NewRow();
                foreach (DataGridViewCell cell in row[i].Cells)
                {
                    dr[i][cell.ColumnIndex] = cell.Value;
                }
            }

            if (dr != null)
            {
                if (dr.Length == 0 && this.CurrentRow!=null)
                {
                    Array.Resize(ref dr, 1);
                    dr[0] = dt.NewRow();
                    foreach (DataGridViewCell cell in this.CurrentRow.Cells)
                    {
                        dr[0][cell.ColumnIndex] = cell.Value;
                    }
                }
            }
            return dr;
        }
        #endregion

        #endregion
    }
    #region Checkbox header
    public delegate void CheckBoxClickedHandler(bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }
    class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        public event CheckBoxClickedHandler OnCheckBoxClicked;

        public DatagridViewCheckBoxHeaderCell()
        {
        }

        protected override void Paint(System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) + (value.ToString().Length + s.Width + 5);
            p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <=
                checkBoxLocation.X + checkBoxSize.Width
            && p.Y >= checkBoxLocation.Y && p.Y <=
                checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(_checked);
                    this.DataGridView.InvalidateCell(this);
                }

            }
            base.OnMouseClick(e);
        }
    }
    #endregion
}
