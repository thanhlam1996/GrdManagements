using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Container;
using System.Data;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.Utils;
using System.ComponentModel;
using DevExpress.XtraEditors.Repository;
using System.Drawing.Drawing2D;
using DevExpress.Data;
using System.Collections;
using DevExpress.Utils.Drawing;

namespace CommonLib.UserControls
{
    public class XtraGridExtend: GridControl
    {
        #region Init
        public XtraGridExtend()
        {
            InitializeComponent();
            _colorUnEditableColumn = new ColorColumn();
            _colorHeaderChooseColumn = new ColorColumn(Color.FromArgb(154, 192, 236), Color.FromArgb(184, 212, 242));
            this.MouseClick += new MouseEventHandler(XtraGridExtend_MouseClick);
        }

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuChooseVisibleColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChooseVisibleColumn});
            this.cmsMenu.Name = "cmsAutoResize";
            this.cmsMenu.Size = new System.Drawing.Size(231, 70);
            // 
            // mnuChooseVisibleColumn
            // 
            this.mnuChooseVisibleColumn.Image = global::CommonLib.Properties.Resources.tableprop;
            this.mnuChooseVisibleColumn.Name = "mnuChooseVisibleColumn";
            this.mnuChooseVisibleColumn.Size = new System.Drawing.Size(230, 22);
            this.mnuChooseVisibleColumn.Text = "Chọn cột hiển thị";
            this.mnuChooseVisibleColumn.Visible = false;
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


        private ContextMenuStrip cmsMenu;
        private IContainer components;
        private ToolStripMenuItem mnuChooseVisibleColumn;

        bool _isLoaded = false;

        protected override void OnLoaded()
        {
            try
            {
                base.OnLoaded();
                if (this.DefaultView != null && !_isLoaded)
                {
                    GridView view = this.DefaultView as GridView;
                    if (view != null)
                    {
                        view.GroupFormat = "[#image]{1} {2}";
                        view.ValidateRow += new ValidateRowEventHandler(view_ValidateRow);
                        view.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(view_ValidatingEditor);
                        view.InvalidRowException += new InvalidRowExceptionEventHandler(view_InvalidRowException);
                        //view.ShownEditor += new EventHandler(view_ShownEditor);
                        view.GroupLevelStyle += new GroupLevelStyleEventHandler(view_GroupLevelStyle);
                        view.MasterRowExpanded+=new CustomMasterRowEventHandler(view_MasterRowExpanded);
                        view.OptionsMenu.EnableGroupPanelMenu = false;
                        view.OptionsMenu.EnableColumnMenu = false;
                    }

                    SetCenterHeaderText();
                    CreateCheckAll();
                    CreateMenuChooseColumn();
                    InitSelectedRow_ShowEditor();
                    InitDeleteRow();
                }
            }
            catch { }
        }

        #endregion

        #region Variables

        #region Var grid
        private object[][] _validateText;
        private bool _isCheckValidateCell = false, _isCheckValidateRow = false, _autoCenterHeaderText = true;
        private bool _allowNullValidateCell = true, _allowUserToDeleteRows=false;

        DevExpress.Data.SummaryItemType _groupType= DevExpress.Data.SummaryItemType.None;
        ColorColumn _colorUnEditableColumn = null;
        LinearGradientMode _gradientModeChooseColumn = LinearGradientMode.Vertical;

        ColorColumn _colorHeaderChooseColumn = null;

        string[] _columnNumberNotNegative = null;
        bool _expandMaster = false;
        #endregion

        #region Var Check All 
        bool _useCheckAll = false;
        CustomPanel _panelCheck = null;
        CheckEdit _chkAll = null;
        string _checkAllFieldName = null;
        bool _checkAll = false;
        #endregion

        #region Var Choose Column
        bool _showMenuChooseVisibleColumn = false;
        string[] _columnsChoose = null;

        Panel pnlChooseColumn = null;
        #endregion

        #region Check State 
        bool _isCheckChangedState = true;
        #endregion      

        #endregion

        #region Local function

        void XtraGridExtend_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = this.GetViewAt(e.Location) as GridView;
                if (view != null)
                {
                    GridViewInfo vf = GetGridViewInfo(view);
                    if (vf != null)
                    {
                        GridColumn col = view.PressedColumn;                        
                        if (col != null)
                        {
                            //if (e.X >= vf.ColumnsInfo[col].Bounds.Left && e.X <= vf.ColumnsInfo[col].Bounds.Right && e.Y>= vf.ColumnsInfo[col].Bounds.Top && e.Y<= vf.ColumnsInfo[col].Bounds.Bottom)
                            {
                                OnColumnClick(sender, new GridColumnClickEventArgs(e.Button,e.Clicks,e.X,e.Y,e.Delta, col));                                
                            }
                        }
                    }
                }
            }
            catch { }
        }

        #region InitDeleteRow
        private void InitDeleteRow()
        {
            try
            {
                if (AllowUserToDeleteRows)
                {
                    foreach (GridView view in this.ViewCollection)
                    {
                        if (view.OptionsSelection.MultiSelectMode== GridMultiSelectMode.RowSelect)
                        {
                            view.KeyDown += new KeyEventHandler(view_KeyDown);
                        }
                    }
                }
            }
            catch { }
        }

        void view_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.KeyData == Keys.Delete && view != null)
                {
                    int[] sel = view.GetSelectedRows();
                    DataRow[] dr = new DataRow[sel.Length];
                    for (int i = 0; i < sel.Length; i++)
                    {
                        dr[i] = view.GetDataRow(sel[i]);
                    }

                    DataRowDeleteEventArgs arg = new DataRowDeleteEventArgs(dr);
                    OnDeleteRow(sender, arg);
                    if (!arg.Cancel)
                    {
                        view.DeleteSelectedRows();
                    }                                       
                }
            }
            catch { }
        }
        #endregion

        #region InitSelectedRow_ShowEditor
        private void InitSelectedRow_ShowEditor()
        {
            try
            {
                foreach (GridView view in this.ViewCollection)
                {
                    view.Appearance.SelectedRow.BackColor = Color.FromArgb(30, 0, 0, 240);
                    view.Appearance.FocusedRow.BackColor = Color.FromArgb(60, 0, 0, 240);
                    view.ShownEditor += new EventHandler(view_ShownEditor);
                }
            }
            catch { }
        }
        
        #endregion

        #region InitDateTimeColumn
        private void InitDateTimeColumn()
        {
            try
            {
                foreach (GridView view in this.ViewCollection)
                {
                    foreach (GridColumn col in view.Columns)
                    {
                        if (col.ColumnType == typeof(DateTime))
                        {                            
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            col.DisplayFormat.FormatString = "dd/MM/yyyy";
                            //col.ColumnEdit = new RepositoryItemDateEdit();
                            //col.ColumnEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            //col.ColumnEdit.EditFormat.FormatString = "dd/MM/yyyy";
                            //col.ColumnEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            //col.ColumnEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Create Menu choose column
        bool _createdChoose = false;
        private void CreateMenuChooseColumn()
        {
            try
            {
                if (ShowMenuChooseVisibleColumn && ColumnsChoose != null && !_createdChoose)
                {
                    if (ColumnsChoose.Length > 0)
                    {
                        GridView view = this.DefaultView as GridView;
                        if (view != null)
                        {
                            if (view.OptionsView.ShowColumnHeaders)
                            {
                                view.MouseDown += new MouseEventHandler(view_MouseDown);
                                
                                mnuChooseVisibleColumn.Click += new EventHandler(mnuChooseVisibleColumn_Click);
                                _createdChoose = true;
                            }
                        }
                    }                    
                    if (this.Parent != null && pnlChooseColumn==null)
                    {
                        pnlChooseColumn = new Panel();
                        this.Parent.Controls.Add(pnlChooseColumn);
                        pnlChooseColumn.Size = new Size(14, 14);
                        pnlChooseColumn.BackColor = _colorHeaderChooseColumn.Color;
                        pnlChooseColumn.Visible = true;
                        pnlChooseColumn.Left = this.Left+3;
                        pnlChooseColumn.Top = this.Top+4;
                        pnlChooseColumn.BringToFront();
                        
                        pnlChooseColumn.BackgroundImage = Properties.Resources.tableprop;
                        pnlChooseColumn.BackgroundImageLayout = ImageLayout.Stretch;
                        this.SizeChanged += new EventHandler(XtraGridExtend_SizeChanged);
                        pnlChooseColumn.Click += new EventHandler(pnlChooseColumn_Click);
                    }
                }
            }
            catch { }
        }

        void pnlChooseColumn_Click(object sender, EventArgs e)
        {
            mnuChooseVisibleColumn_Click(sender, e);
        }

        #region XtraGridExtend_SizeChanged
        void XtraGridExtend_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                pnlChooseColumn.Left = this.Left + 3;
                pnlChooseColumn.Top = this.Top + 4;
                pnlChooseColumn.BringToFront();
            }
            catch 
            {
                if (pnlChooseColumn != null)
                    pnlChooseColumn.Visible = false;
            }
        }
        #endregion

        #region view_MouseDown
        void view_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = this.DefaultView as GridView;
                if (view != null)
                {
                    if (view.OptionsView.ShowColumnHeaders)
                    {
                        if (e.Button == MouseButtons.Right  && e.Y< this.ColumnInfos[0].Bounds.Height)
                        {
                            if (ShowMenuChooseVisibleColumn && ColumnsChoose != null)
                                mnuChooseVisibleColumn.Visible = true;
                            cmsMenu.Show(MousePosition.X + 3, MousePosition.Y + 3);
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
                    GridColumn[] col = new GridColumn[] { };
                    if (_columnsChoose != null)
                    {
                        Array.Resize(ref col, _columnsChoose.Length);
                        for (int i = 0; i < _columnsChoose.Length; i++)
                        {
                            if (this.Columns.ColumnByFieldName(_columnsChoose[i]) != null)
                            {
                                col[i] = this.Columns.ColumnByFieldName(_columnsChoose[i]);
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
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        string[] scol = frm.ColumnChoosed();
                        foreach (GridColumn colChoose in this.Columns)
                            if (scol.Contains(colChoose.FieldName))
                                colChoose.Visible = true;
                            else if (_columnsChoose.Contains(colChoose.FieldName))
                                colChoose.Visible = false;
                    }
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region Create check all column

        #region CreateCheckAll
        private void CreateCheckAll()
        {
            try
            {
                if (UseCheckAll)
                {
                    if (this.Parent != null && _panelCheck == null && !this.DesignMode)
                    {
                        _panelCheck = new CustomPanel();
                        _chkAll = new CheckEdit();
                        _chkAll.Properties.Caption = "";
                        _chkAll.Size = new System.Drawing.Size(21, 18);
                        _panelCheck.Size = new System.Drawing.Size(21, 18);
                        _panelCheck.Controls.Add(_chkAll);
                        _chkAll.Visible = true;
                        _panelCheck.Visible = false;
                        _chkAll.Location = new Point(2, 0);
                        _chkAll.Anchor = AnchorStyles.None;
                        this.Parent.Controls.Add(_panelCheck);
                        this.MouseMove += new MouseEventHandler(XtraGridExtend_MouseMove);
                        this.Parent.MouseHover += new EventHandler(Parent_MouseHover);
                        _chkAll.MouseClick += new MouseEventHandler(_chkAll_MouseClick);
                        _chkAll.MouseDoubleClick += new MouseEventHandler(_chkAll_MouseDoubleClick);
                        _panelCheck.VisibleChanged += new EventHandler(_panelCheck_VisibleChanged);

                        GridView view = this.MainView as GridView;
                        if(view!=null)
                            view.RowCountChanged += new EventHandler(view_RowCountChanged);
                    }
                }
            }
            catch { }
        }

        void view_RowCountChanged(object sender, EventArgs e)
        {
            if (_panelCheck != null)
            {
                if (_panelCheck.Visible)
                    _panelCheck.Visible = false;
            }
        }

        void _panelCheck_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (_panelCheck.Visible)
                {                    
                    DataTable dt = this.GetThisData();
                    if (dt != null)
                    {
                        GridView view = this.DefaultView as GridView;
                        if (view != null)
                        {
                            if (view.EditingValue != null && view.FocusedValue !=view.EditingValue)
                            {                                
                                DataRow row = view.GetDataRow(view.FocusedRowHandle);
                                if (row != null)
                                {
                                    if(view.FocusedColumn.FieldName== CheckAllFieldName)
                                        row[CheckAllFieldName] = view.EditingValue;
                                }
                            }
                        }
                        DataRow[] drChecked = dt.Select(CheckAllFieldName + " = True");
                        if (drChecked.Length < this.VisibleRowCount || this.VisibleRowCount==0)
                        {
                            _chkAll.Checked = false;
                        }
                        else
                        {
                            _chkAll.Checked = true;
                        }
                    }
                }
            }
            catch { }
        }

        void Parent_MouseHover(object sender, EventArgs e)
        {
            if (_panelCheck.Visible)
            {
                _panelCheck.Visible = false;
            }
        }
        
        #endregion   
  
        #region _chkAll_MouseDoubleClick
        void _chkAll_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _chkAll_MouseClick(sender, e);
        }
        #endregion

        #region _chkAll_MouseClick
        void _chkAll_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.UseCheckAll && this.DataSource != null)
                {
                    if (this.Columns[CheckAllFieldName] != null)
                        if (this.Columns[CheckAllFieldName].ColumnType == typeof(bool))
                        {
                            if (e != null)
                            {
                                if (e.Button == MouseButtons.Right)
                                    _checkAll = _chkAll.Checked;
                                else
                                    _checkAll = !_chkAll.Checked;
                            }
                            else
                                _checkAll = !_chkAll.Checked;

                            foreach (GridRow dr in this.Rows)
                            {
                                if (dr.VisibleIndex >= 0)
                                {
                                    DataRow row = null;
                                    if (dr.RowKey is DataRow)
                                        row = dr.RowKey as DataRow;
                                    else if (dr.RowKey is DataRowView)
                                        row = ((DataRowView)dr.RowKey).Row;

                                    if (row[CheckAllFieldName].ToString() != _checkAll.ToString())
                                    {
                                        row[CheckAllFieldName] = _checkAll;
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                }
            }
            catch { }
        }
        #endregion

        #region XtraGridExtend_MouseMove
        void XtraGridExtend_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this[CheckAllFieldName].ColumnType != typeof(bool)) return;
                if (e.X >= this.ColumnInfos[this[CheckAllFieldName]].Bounds.Left && e.X <= this.ColumnInfos[this[CheckAllFieldName]].Bounds.Right)
                {                 
                    _panelCheck.BackColor = this.ColorHeaderChooseColumn.Color;
                    _panelCheck.BackColor2 = this.ColorHeaderChooseColumn.Color2;
                    _panelCheck.GradientMode = this.GradientModeChooseColumn;

                    _panelCheck.Width = this.Columns[CheckAllFieldName].VisibleWidth - 2;
                    _panelCheck.Height = this.ColumnInfos[this[CheckAllFieldName]].CaptionRect.Height + 2;
                    _panelCheck.Location = new Point(this.ColumnInfos[this[CheckAllFieldName]].Bounds.Left + this.Location.X, this.Top + 4);
                    _panelCheck.BringToFront();
                    _panelCheck.Visible = true;
                }
                else
                {
                    _panelCheck.Visible = false;
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region GenCenterHeaderText(GridBand band)
        private void GenCenterHeaderText(GridBand band)
        {
            if (band != null)
            {
                band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                if (band.Children != null)
                {
                    foreach (GridBand bandchild in band.Children)
                        GenCenterHeaderText(bandchild);
                }
            }
        }
        #endregion

        #region Set center header
        private void SetCenterHeaderText()
        {
            BandedGridView bands = this.MainView as BandedGridView;
            if (bands != null && AutoCenterHeaderText)
            {
                foreach (GridBand band in bands.Bands)
                {
                    GenCenterHeaderText(band);
                }
            }
            else
            {
                GridView view = this.DefaultView as GridView;
                if(view !=null)
                    foreach (GridColumn col in view.Columns)
                    {
                        col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    }
            }
        }
        #endregion

        #region DataColumns
        private DataColumnCollection DataColumns
        {
            get
            {
                if (this.DataSource == null) return null;
                if (this.DataSource is DataTable)
                    return ((DataTable)this.DataSource).Columns;
                else if (this.DataSource is DataView)
                    return ((DataView)this.DataSource).Table.Columns;
                else
                    return null;
            }
        }
        #endregion

        #region GetValidateText
        private ValiateValue GetValidateText(string columnName)
        {
            ValiateValue ret = null;
            try
            {
                if (_validateText == null) return null;
                foreach (object[] val in _validateText)
                {
                    if (val[0].ToString().ToLower() == columnName.ToLower())
                    {
                        ValidateValueType vtype = ValidateValueType.None;
                        if(val[2].ToString() ==ValidateValueType.Both.ToString())
                            vtype = ValidateValueType.Both;
                        else if(val[2].ToString() == ValidateValueType.InvalidType.ToString())
                            vtype = ValidateValueType.InvalidType;
                        else if(val[2].ToString() == ValidateValueType.NullValue.ToString())
                            vtype = ValidateValueType.NullValue;
                        ret = new ValiateValue(val[1].ToString(), vtype);
                        break;
                    }
                }
            }
            catch { }
            return ret;
        }
        #endregion

        #region GetGridViewInfo
        public static GridViewInfo GetGridViewInfo(GridView gridView)
        {
            if (gridView == null) return null;
            GridViewInfo res = gridView.GetViewInfo() as GridViewInfo;
            if (res == null) return null;
            if (!res.IsReady) gridView.LayoutChanged();
            return res;
        }
        #endregion

        #region SetColor UnEditable column
        private void SetColorUnEditableColumn()
        {
            try
            {
                if (this.DataSource != null)
                {
                    foreach (GridColumn col in this.Columns)
                    {
                        if (AutoCenterHeaderText)
                            col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        string name = "";
                        if (this.DataColumns.Contains(col.Name))
                            name = col.Name;
                        else if (this.DataColumns.Contains(col.FieldName))
                            name = col.FieldName;
                        if (this.DataColumns.Contains(name))
                        {
                            if (this.DataColumns[name].ReadOnly)
                            {
                                col.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
                                col.AppearanceCell.BackColor = ColorUnEditableColumn.Color;
                                col.AppearanceCell.BackColor2 = ColorUnEditableColumn.Color2;
                            }
                        }
                        //col.AppearanceHeader.BackColor = ColorHeader.Color;
                        //col.AppearanceHeader.BackColor2 = ColorHeader.Color2;
                    }
                }
            }
            catch { }
        }
        #endregion 
        
        #region Create ErrorText default
        private string ErrorTextDefault(Type typeColumn, string ExceptionString)
        {
            string s = "";
            #region Set text validate
            if (typeColumn == typeof(int) || typeColumn == typeof(Int64))
            {
                s = "Dữ liệu phải nhập kiểu số nguyên. (Nhấn ESC để bỏ qua)";
            }
            else if (typeColumn == typeof(decimal))
            {
                s = "Dữ liệu phải nhập kiểu số thập phân. (Nhấn ESC để bỏ qua)";
            }
            else if (typeColumn == typeof(double))
            {
                s = "Dữ liệu phải nhập kiểu số thực. (Nhấn ESC để bỏ qua)";
            }
            else if (typeColumn == typeof(bool))
            {
                s = "Dữ liệu phải nhập kiểu boolean (true/false). (Nhấn ESC để bỏ qua)";
            }
            else if (ExceptionString != null && ExceptionString != "")
            {
                s = ExceptionString + ". (Nhấn ESC để bỏ qua)";
            }
            else
            {
                s = "Invalid " + typeColumn.ToString();
            }
            #endregion
            return s;
        }
        #endregion

        #region GetThisData
        private DataTable GetThisData()
        {
            if (this.DataSource is DataTable)
                return (DataTable)this.DataSource;
            else if (this.DataSource is DataView)
                return ((DataView)this.DataSource).Table;
            else
                return null;
        }
        #endregion

        #endregion

        #region Properties

        #region MenuChooseVisibleColumn
        public bool ShowMenuChooseVisibleColumn
        {
            get { return _showMenuChooseVisibleColumn; }
            set { _showMenuChooseVisibleColumn = value; }
        }

        public string[] ColumnsChoose
        {
            get { return _columnsChoose; }
            set { _columnsChoose = value; }
        }

        #endregion

        #region Check all column

        #region UseCheckAll
        /// <summary>
        /// Set or get when need to use checkall column
        /// </summary>
        public bool UseCheckAll
        {
            get { return _useCheckAll; }
            set { _useCheckAll = value; }
        }
        #endregion

        #region CheckAllName
        /// <summary>
        /// FieldName of check all column
        /// </summary>
        public string CheckAllFieldName
        {
            get { return _checkAllFieldName; }
            set { _checkAllFieldName = value; }
        }
        #endregion

        #endregion

        #region GradientMode
        /// <summary>
        /// Gradient choose column
        /// </summary>
        public LinearGradientMode GradientModeChooseColumn
        {
            get { return _gradientModeChooseColumn; }
            set { _gradientModeChooseColumn = value; }
        }
        #endregion

        #region ColorHeaderChooseColumn
        /// <summary>
        /// Set or get color header of choose column
        /// </summary>
        public ColorColumn ColorHeaderChooseColumn
        {
            get { return _colorHeaderChooseColumn; }
            set { _colorHeaderChooseColumn = value; }
        }
        #endregion
        
        #region ColumnNumberNotNegetive
        /// <summary>
        /// Type of column is number, not allow negative
        /// </summary>
        public string[] ColumnNumberNotNegative
        {
            get { return _columnNumberNotNegative; }
            set { _columnNumberNotNegative = value; }
        }

        #endregion

        #region GroupType
        public DevExpress.Data.SummaryItemType SummaryGroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }
        #endregion

        #region AllowUserToDeleteRows
        /// <summary>
        /// Allow user delete row when press Delete and OptionsBehavior.Editable is false
        /// </summary>
        public bool AllowUserToDeleteRows
        {
            get { return _allowUserToDeleteRows; }
            set { _allowUserToDeleteRows = value; }
        }

        #endregion

        #region AllowNullValidateCell
        /// <summary>
        /// Allow null value when IsCheckValidateCell is true and ValiateText is not null
        /// </summary>
        public bool AllowNullValidateCell
        {
            get { return _allowNullValidateCell; }
            set { _allowNullValidateCell = value; }
        }
        #endregion

        #region AutoCenterHeaderText
        public bool AutoCenterHeaderText
        {
            get { return _autoCenterHeaderText; }
            set 
            { 
                _autoCenterHeaderText = value;
                SetCenterHeaderText();
            }
        }

        #endregion

        #region ColorUnEditableColumn
        /// <summary>
        /// Set color for uneditable columns
        /// </summary>
        public ColorColumn ColorUnEditableColumn
        {
            get { return _colorUnEditableColumn; }
            set
            {
                _colorUnEditableColumn = value;
                if (this.DataSource != null)
                    SetColorUnEditableColumn();
            }
        }
        #endregion

        #region CheckValidate
        public bool IsCheckValidateRow
        {
            get { return _isCheckValidateRow; }
            set { _isCheckValidateRow = value; }
        }

        public bool IsCheckValidateCell
        {
            get { return _isCheckValidateCell; }
            set { _isCheckValidateCell = value; }
        }
        #endregion

        #region ValidateText
        /// <summary>
        /// 3 value: ColumnName,ValidateText,ValidateValueType
        /// </summary>
        public object[][] ValidateText
        {
            get { return _validateText; }
            set { _validateText = value; }
        }
        #endregion

        #region Columns
        public GridColumnCollection Columns
        {
            get
            {
                GridView view = this.DefaultView as GridView;
                if (view == null)
                    return null;
                return view.Columns;
            }
        }
        #endregion

        #region Rows
        /// <summary>
        /// Get row that datasource is datatable or dataview(datatable)
        /// </summary>
        public GridRow[] Rows
        {
            get
            {
                GridView view = this.MainView as GridView;
                if (view == null) return null;
                GridRow[] row = new GridRow[view.DataRowCount];

                for (int i = 0; i < view.DataRowCount; i++)
                {
                    int indexDS = view.GetDataSourceRowIndex(i);
                    if (indexDS>=0)
                    {
                        int index = view.GetRowHandle(indexDS);
                        if (index >= 0)
                            row[i] = new GridRow(index, view.GetVisibleIndex(index), view.GetRowLevel(index), 0, view.GetRow(i), true);
                        else
                        {

                        }
                    }
                }
                return row;
            }
        }
        
        #endregion

        #region ColumnInfos
        public GridColumnsInfo ColumnInfos
        {
            get 
            {
                GridView view = this.DefaultView as GridView;
                if (view == null)
                    return null;

                GridViewInfo viewInfo = GetGridViewInfo(view);
                if (viewInfo == null) return null;
                return viewInfo.ColumnsInfo;
            }
        }
        #endregion

        #region RowInfos
        public GridRowInfo[] RowInfos
        {
            get
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                GridViewInfo viewInfo =GetGridViewInfo(view);
                if (viewInfo == null) return null;
                GridRowInfo[] row = new GridRowInfo[view.RowCount];

                for (int i = 0; i < row.Length; i++)
                {
                    int index = view.GetRowHandle(i);
                    row[i] = viewInfo.GetGridRowInfo(index);                    
                    //if (row[i] == null)
                    //{
                    //    row[i] = viewInfo.CreateRowInfo(this.Rows[index]);
                    //}                    
                }

                return row;
            }
        }
        #endregion

        #region Active Column
        public GridColumn ActiveColumn
        {
            get
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                return view.FocusedColumn;
            }

            set
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return;
                view.FocusedColumn = value;
            }
        }
        #endregion

        #region Active Rowinfo
        public GridRowInfo ActiveRowInfo
        {
            get
            {
                if (this.IsDesignMode) return null;
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                if (view.FocusedRowHandle < 0)
                {

                    if (view.ActiveEditor != null && view.FocusedRowModified)
                    {
                        return this.RowInfos[this.Rows.Length - 1];
                    }
                    else
                    {
                        //object r = view.GetFocusedRow();
                        //if (r != null)
                        //{
                        //    try
                        //    {
                        //        GridRow dr = new GridRow(view.FocusedRowHandle, view.GetVisibleIndex(view.FocusedRowHandle), view.GetRowLevel(view.FocusedRowHandle), 0, r);
                        //        return dr;
                        //    }
                        //    catch { }
                        //}
                        return null;
                    }
                }
                if (this.Rows == null) return null;

                

                if (this.Rows.Length >= view.FocusedRowHandle)
                    return this.RowInfos[view.FocusedRowHandle];
                else
                    return null;
            }

            set
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return;
                if (value == null) return;
                view.FocusedRowHandle = value.RowHandle;
            }
        }
        #endregion

        #region Active Row
        public GridRow ActiveRow
        {
            get
            {
                if (this.IsDesignMode) return null;
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                if (view.FocusedRowHandle < 0)
                {

                    if (view.ActiveEditor != null && view.FocusedRowModified)
                    {
                        return this.Rows[this.Rows.Length - 1];
                    }
                    else
                    {
                        object r = view.GetFocusedRow();
                        if (r != null)
                        {
                            try
                            {
                                GridRow dr = new GridRow(view.FocusedRowHandle, view.GetVisibleIndex(view.FocusedRowHandle), view.GetRowLevel(view.FocusedRowHandle), 0, r, true);
                                return dr;
                            }
                            catch { }
                        }
                        return null;
                    }
                }
                if (this.Rows == null) return null;
                if (this.VisibleRowCount >= view.FocusedRowHandle)
                    return this.Rows[view.FocusedRowHandle];
                else
                    return null;                
            }

            set
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return;
                if (value == null) return;
                view.FocusedRowHandle = value.RowHandle;
            }
        }
        #endregion

        #region Cell
        public GridCellInfo this[int columnIndex, int Row]
        {
            get
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                GridViewInfo viewInfo = GetGridViewInfo(view);
                if (viewInfo == null) return null;

                GridColumn column = this[columnIndex];
                GridCellInfo cell = viewInfo.GetGridCellInfo(Row, column);
                return cell;
                
            }
        }
        public GridCellInfo this[string columnName, int Row]
        {
            get
            {
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                if (viewInfo == null) return null;
                GridDataRowInfo row = viewInfo.RowsInfo[Row] as GridDataRowInfo;
                if (row == null) return null;
                if (this[columnName] == null) return null;
                return row.Cells[this[columnName].ColumnHandle];
            }
        }
        #endregion

        #region Active Cell

        public GridCellInfo ActiveCell
        {
            get
            {
                if (this.IsDesignMode) return null;
                if (this.ActiveColumn == null || this.ActiveRow == null) return null;
                GridView view = this.DefaultView as GridView;
                if (view == null) return null;
                
                //return this[this.ActiveColumn.ColumnHandle, this.ActiveRow.RowHandle];
                return this[view.FocusedColumn.ColumnHandle, view.FocusedRowHandle];
            }
            set
            {
                GridCellInfo cell = value;
                if (cell == null) return;

                GridView view = this.DefaultView as GridView;
                if (view == null) return;
                view.SelectCell(cell.RowHandle,cell.Column);
            }
        }
        #endregion

        #region Column
        public GridColumn this[int columnIndex]
        {
            get
            {
                if (this.Columns == null) return null;
                
                if (this.Columns.Count > columnIndex)
                    return this.Columns[columnIndex];
                else
                    return null;
            }
        }

        public GridColumn this[string columnName]
        {
            get
            {
                int index = -1;
                for (int i = 0; i < this.Columns.Count && index==-1; i++)
                    if (this.Columns[i].Name.ToLower() == columnName.ToLower() || this.Columns[i].FieldName.ToLower() == columnName.ToLower()) index = i;
                if(index>=0)
                    return this.Columns[index];
                else
                    return null;
            }
        }
        #endregion

        #region IsCheckChangedState
        /// <summary>
        /// Use this properties to check ChangedData
        /// </summary>
        public bool IsCheckChangedState
        {
            get { return _isCheckChangedState; }
            set { _isCheckChangedState = value; }
        }
        #endregion

        #region ChangedData
        /// <summary>
        /// Get changed data
        /// </summary>
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

                        
                        DataRow[] drchanged = dtSel.Select("","", DataViewRowState.Added | DataViewRowState.ModifiedOriginal | DataViewRowState.Deleted);

                        if (IsCheckChangedState)
                        {
                            DataTable changedForRoll = dtSel.Copy();
                            DataRow[] drchangedRoll = changedForRoll.Select("", "", DataViewRowState.Added | DataViewRowState.ModifiedOriginal | DataViewRowState.Deleted);

                            for (int i = 0; i < drchanged.Length; i++)
                            {
                                if (drchanged[i].RowState == DataRowState.Added)
                                {
                                    #region Add row
                                    DataRow drAdd = dt.NewRow();
                                    foreach (DataColumn col in dt.Columns)
                                    {
                                        drAdd[col.ColumnName] = drchanged[i][col.ColumnName];
                                    }
                                    dt.Rows.Add(drAdd);
                                    #endregion
                                }
                                else
                                {
                                    DataRow dr1 = drchanged[i];
                                    DataRow dr2 = drchangedRoll[i];
                                    dr2.RejectChanges();

                                    bool check = true;

                                    #region Check
                                    foreach (DataColumn col in dtSel.Columns)
                                    {
                                        string s1 = dr1[col.ColumnName].ToString();
                                        string s2 = dr2[col.ColumnName].ToString();
                                        if (s1 != s2)
                                        {
                                            check = false;
                                        }
                                        if (!check)
                                            if (col.DataType == typeof(int) || col.DataType == typeof(Int64) || col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(float))
                                            {
                                                decimal dec1 = -1, dec2 = -1;
                                                decimal.TryParse(s1, out dec1);
                                                decimal.TryParse(s2, out dec2);
                                                if (dec1 != dec2)
                                                {
                                                    check = false;
                                                    break;
                                                }
                                            }
                                            else
                                                break;
                                        check = true;
                                    }
                                    #endregion

                                    if (!check)
                                    {
                                        #region Add row
                                        DataRow drAdd = dt.NewRow();
                                        foreach (DataColumn col in dt.Columns)
                                        {
                                            drAdd[col.ColumnName] = drchanged[i][col.ColumnName];
                                        }
                                        dt.Rows.Add(drAdd);
                                        #endregion
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in drchanged)
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

        #region VisibleRowCount
        /// <summary>
        /// Count visible row
        /// </summary>
        public int VisibleRowCount
        {
            get 
            {
                return this.DefaultView == null ? 0 : this.DefaultView.RowCount;
            }
        }
        #endregion

        #endregion

        #region Build event
        public event DataRowChangeEventHandler RowChanged;
        public event DataRowDeleteEventHandler DeleteRow;
        public event GridColumnClickEventHanlder ColumnClick;

        protected void OnColumnClick(object sender, GridColumnClickEventArgs e)
        {
            if (ColumnClick != null)
                ColumnClick(sender, e);
            
        }

        protected void OnDeleteRow(object sender, DataRowDeleteEventArgs e)
        {
            if (DeleteRow != null)
                DeleteRow(sender, e);
        }

        protected void OnRowChanged(object sender,DataRowChangeEventArgs e)
        {
            if (RowChanged != null)
                RowChanged(sender, e);
        }

                
        #endregion

        #region Override

        #region Visible
        protected override void SetVisibleCore(bool value)
        {
            try
            {
                base.SetVisibleCore(value);
                if (_panelCheck != null && value== false)
                    _panelCheck.Visible = false;
            }
            catch { }
        }
        #endregion

        #region DataSource
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                _expandMaster = false;
                DataTable dt = null;
                if (value is DataTable)
                {
                    dt = value as DataTable;
                }
                else if (value is DataView)
                {
                    dt = (value as DataView).Table;
                }
                if (dt != null)
                {
                    dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);                    
                }
                SetColorUnEditableColumn();
                InitDateTimeColumn();
            }
        }           
        
        #endregion        

        #endregion

        #region Event

        #region dt_RowChanged
        private void dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            OnRowChanged(sender, e);
        }
        #endregion

        #region view_ShownEditor
        void view_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                GridView view = this.DefaultView as GridView;
                if (view != null)
                {
                    if (this.ActiveColumn != null)
                    {
                        if (this.ActiveColumn.ReadOnly && view.ActiveEditor is TextEdit && view.IsEditorFocused)
                        {
                            view.ActiveEditor.BackColor = ColorUnEditableColumn.Color2;
                            //view.ActiveEditor.Hide();
                        }                        
                        //else if (view.ActiveEditor is DateEdit)
                        //{
                        //    DateEdit d = view.ActiveEditor as DateEdit;
                        //    d.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        //    d.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
                        //    d.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        //    d.Properties.EditFormat.FormatString = "dd/MM/yyyy";
                        //}
                    }
                }
            }
            catch { }
        }
        
        #endregion

        #region Validate value

        #region view_ValidatingEditor
        void view_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if (e.ErrorText != "" && !e.Valid)
                    return;

                ValiateValue reply = null;
                GridView view = sender as GridView;
                if (view == null)
                    return;
                if (e.Value == null && view.IsEditing)
                {
                    return;
                }

                #region Check negative column
                if (ColumnNumberNotNegative != null)
                {
                    if (ColumnNumberNotNegative.Length > 0)
                    {
                        if (ColumnNumberNotNegative.Contains(view.FocusedColumn.FieldName) || ColumnNumberNotNegative.Contains(view.FocusedColumn.Name))
                        {
                            if (view.FocusedColumn.ColumnType == typeof(int) || view.FocusedColumn.ColumnType == typeof(float) ||
                                view.FocusedColumn.ColumnType == typeof(double) || view.FocusedColumn.ColumnType == typeof(decimal)
                                || view.FocusedColumn.ColumnType == typeof(Int64))
                            {
                                decimal tmp = 0;
                                if (decimal.TryParse(e.Value.ToString(), out tmp))
                                {
                                    if (tmp < 0)
                                    {
                                        e.ErrorText = "(Nhập dữ liệu sai) Không được nhập nhỏ hơn 0!";
                                        e.Valid = false;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region !IsCheckValidateCell
                if (!IsCheckValidateCell)
                {
                    if (e.Value.ToString() == "" && e.Value!=DBNull.Value)
                    {
                        //this.ActiveCell.State = GridRowCellState.GridFocused;
                        //view.SetRowCellValue(this.ActiveCell.RowHandle, this.ActiveCell.Column, DBNull.Value);
                        e.Value = DBNull.Value;
                    }
                    else if (e.Value != DBNull.Value)
                    {
                        try
                        {
                            Convert.ChangeType(e.Value, this.ActiveColumn.ColumnType);
                        }
                        catch (Exception ex)
                        {
                            if (e.Value.ToString() == "")
                            {
                                e.Value = DBNull.Value;
                                e.Valid = true;
                                return;
                            }

                            e.ErrorText = ErrorTextDefault(view.FocusedColumn.ColumnType, ex.Message);
                            e.Valid = false;
                            return;
                        }
                    }
                }
                #endregion

                reply = GetValidateText(view.FocusedColumn.Name);
                if(reply==null)
                    reply = GetValidateText(view.FocusedColumn.FieldName);
                if (reply != null && IsCheckValidateCell)
                {
                    #region Check
                    object obj = null;
                    try
                    {
                        
                        obj = Convert.ChangeType(e.Value, this.ActiveColumn.ColumnType);
                    }
                    catch { }
                    if (obj == null)
                    {
                        if (reply.ValiateType == ValidateValueType.NullValue || reply.ValiateType == ValidateValueType.Both)
                        {
                            e.ErrorText = reply.ValidateText;
                            e.Valid = false;
                        }
                        else if (e.Value.ToString() == "" && e.Value!=DBNull.Value)
                        {
                            e.Value = DBNull.Value;
                            e.ErrorText = "";
                            e.Valid = true;
                        }
                        else
                        {
                            e.ErrorText = reply.ValidateText;
                            e.Valid = false;
                        }
                    }
                    else
                    {
                        if ((reply.ValiateType == ValidateValueType.NullValue || reply.ValiateType == ValidateValueType.Both) && e.Value.ToString() == "")
                        {
                            e.ErrorText = reply.ValidateText;
                            e.Valid = false;
                        }
                        else
                        {
                            if (e.Value.ToString() == "" && e.Value != DBNull.Value)
                                //this.ActiveCell.CellValue = DBNull.Value;
                                e.Value = DBNull.Value;

                            if(view.HasColumnErrors)
                                view.SetColumnError(this.ActiveColumn, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);                            
                            if(!e.Valid)
                                e.Valid = true;
                        }
                    }
                    #endregion
                }
                else
                {
                    if (e.Value.ToString() == "" && !IsCheckValidateCell)
                    {
                        //this.ActiveCell.CellValue = DBNull.Value;
                        e.Value = DBNull.Value;
                        e.Valid = true;
                        return;
                    }
                    try
                    {
                        if(e.Value!=DBNull.Value)
                            Convert.ChangeType(e.Value, this.ActiveColumn.ColumnType);
                    }
                    catch (Exception ex)
                    {
                        #region Set text validate
                        Type type = view.FocusedColumn.ColumnType;

                        if (IsCheckValidateCell)
                            if (reply != null)
                                e.ErrorText = reply.ValidateText;
                            else
                            {
                                if (e.Value.ToString() == "" && AllowNullValidateCell)
                                {
                                    e.ErrorText = "";
                                    e.Value = DBNull.Value;
                                    e.Valid = true;
                                }
                                else
                                {
                                    e.ErrorText = ErrorTextDefault(type, ex.Message);
                                }
                            }
                        else
                            e.ErrorText = "";

                        #endregion
                        if (IsCheckValidateCell && e.ErrorText!="")
                            e.Valid = false;
                        else
                            e.Valid = true;
                    }
                }
            }
            catch { }
        }
        #endregion

        #region view_InvalidRowException
        void view_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {

            e.WindowCaption = "Có lỗi!";
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            if (e.ErrorText != "" && this.ValidateText==null)
            {
                XtraMessageBox.Show(e.ErrorText, e.WindowCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region view_ValidateRow
        void view_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                DataRowView row = e.Row as DataRowView;
                
                if (row != null && this.Visible)
                {
                    DataRow r = row.Row;
                    if (r.RowState == DataRowState.Detached  && !row.IsNew)
                        return;

                    DataTable dt = row.Row.Table;

                    if (IsCheckValidateCell)
                    {
                        bool invalid = false;
                        foreach (DataColumn col in dt.Columns)
                        {
                            ValiateValue reply = GetValidateText(col.ColumnName);
                            if (reply != null && row.Row[col.ColumnName].ToString() == "")
                            {
                                if (reply.ValiateType == ValidateValueType.NullValue || reply.ValiateType == ValidateValueType.Both)
                                {
                                    DevExpress.XtraGrid.Views.Base.ColumnView view = this.DefaultView as DevExpress.XtraGrid.Views.Base.ColumnView;
                                    view.SetColumnError(this.Columns[col.ColumnName], reply.ValidateText, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                                    invalid = true;
                                }
                            }
                            else if (reply == null && row.Row[col.ColumnName].ToString() == "" && !AllowNullValidateCell)
                            {
                                e.ErrorText = ErrorTextDefault(col.DataType,null);
                            }
                        }
                        if (invalid)
                        {
                            e.Valid = false;
                            return;
                        }
                    }

                    bool check = false;                    

                    for (int i = 0; i < dt.Columns.Count && !check; i++)
                    {                        
                        if (row[i].ToString() != "" && this[i].Visible)
                            check = true;
                    }
                    if (!check)
                    {
                        if (row.IsNew)
                        {
                            row.Delete();
                            e.Valid = true;
                        }
                        else
                        {
                            if (IsCheckValidateRow)
                            {
                                e.Valid = false;
                                e.ErrorText = "Dòng này không được bỏ trống.";
                            }
                            else
                            {
                                row.Delete();
                                row.Row.Delete();
                                e.Valid = true;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region view_GroupLevelStyle
        void view_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            GridView view = this.MainView as GridView;
            if (view == null) return;
            GridColumn column = view.GroupedColumns[e.Level];
            if (column == null) return;
            e.LevelAppearance.Combine(column.AppearanceHeader);
            e.LevelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            switch (e.Level)
            {
                case 0:
                    e.LevelAppearance.BackColor = Color.LightSkyBlue;
                    e.LevelAppearance.BackColor2 = Color.AliceBlue;
                    break;
                case 1:
                    e.LevelAppearance.BackColor = Color.DarkSeaGreen;
                    e.LevelAppearance.BackColor2 = Color.MintCream;
                    break;
                case 2:
                    e.LevelAppearance.BackColor = Color.PaleGoldenrod;
                    e.LevelAppearance.BackColor2 = Color.FloralWhite;
                    break;
                case 3:
                    e.LevelAppearance.BackColor = Color.DarkKhaki;
                    e.LevelAppearance.BackColor2 = Color.Ivory;
                    break;
            }
        }
        #endregion

        #region view_MasterRowExpanded
        void view_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            try
            {
                if (_expandMaster) return;
                GridView view = this.DefaultView as GridView;
                if (view != null)
                {
                    GridView viewDetail = view.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
                    if (viewDetail != null)
                    {
                        viewDetail.BestFitColumns();
                        foreach (GridColumn col in viewDetail.Columns)
                        {
                            if (AutoCenterHeaderText)
                                col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                            if (col.ReadOnly)
                            {
                                col.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
                                col.AppearanceCell.BackColor = ColorUnEditableColumn.Color;
                                col.AppearanceCell.BackColor2 = ColorUnEditableColumn.Color2;
                            }
                            col.AppearanceHeader.BackColor = ColorHeaderChooseColumn.Color;
                            col.AppearanceHeader.BackColor2 = ColorHeaderChooseColumn.Color2;
                        }
                    }
                }
                _expandMaster = true;
            }
            catch { }
        }
        #endregion

        #endregion

        #region Publlic function

        #region GettChildRows
        public void GetChildRows(GridView view, int groupRowHandle, ArrayList childRows, bool isCountGroupRow, bool allChild)
        {
            if (!view.IsGroupRow(groupRowHandle))
            {
                if (!isCountGroupRow)
                {
                    childRows.Add(view.GetRow(groupRowHandle));
                }
                return;
            }

            //Get the number of immediate children
            int childCount = view.GetChildRowCount(groupRowHandle);
            for (int i = 0; i < childCount; i++)
            {
                //Get the handle of a child row with the required index
                int childHandle = view.GetChildRowHandle(groupRowHandle, i);
                //If the child is a group row, then add its children to the list
                if (view.IsGroupRow(childHandle))
                {
                    if (isCountGroupRow)
                    {
                        object row = view.GetGroupRowValue(childHandle);
                        if (!childRows.Contains(row))
                            childRows.Add(row);
                    }
                    if (allChild)
                        GetChildRows(view, childHandle, childRows, isCountGroupRow, allChild);
                }
                else
                {
                    if (!isCountGroupRow)
                    {
                        // The child is a data row. 
                        // Add it to the childRows as long as the row wasn't added before
                        object row = view.GetRow(childHandle);
                        if (!childRows.Contains(row))
                            childRows.Add(row);
                    }
                    else

                        break;
                }
            }
        }
        #endregion

        #region GetSelectedRowToDataRow
        public DataRow[] GetSelectedRowToDataRow()
        {
            GridView view = this.MainView as GridView;
            if (view == null) return null;
            int[] rowsel = view.GetSelectedRows();
            DataRow[] dr = new DataRow[] { };
            

            DataTable dt = null;
            if (this.DataSource is DataTable)
                dt = this.DataSource as DataTable;
            else if (this.DataSource is DataView)
                dt = ((DataView)this.DataSource).Table;

            if (dt == null) return dr;
            GridRow[] row = this.Rows;
            if (row == null) return dr;

            for (int i = 0; i < rowsel.Length; i++)
            {
                Array.Resize(ref dr, dr.Length+1);
                if(row[rowsel[i]].RowKey is DataRow)
                    dr[dr.Length-1] = row[rowsel[i]].RowKey as DataRow;
                else if (row[rowsel[i]].RowKey is DataRowView)
                    dr[dr.Length-1] = ((DataRowView)row[rowsel[i]].RowKey).Row;                
            }
            return dr;
        }
        #endregion

        #region IndexRow
        public GridRow FindDataRow(DataRow row)
        {
            GridRow[] rows = this.Rows;
            if (rows == null) return null;

            if (rows.Length == 0) return null;
            GridRow rowf = null;
            foreach (GridRow r in rows)
            {
                if (r.RowKey is DataRow)
                {
                    if ((DataRow)r.RowKey == row)
                    {
                        rowf = r;
                        break;
                    }
                }
                else if (r.RowKey is DataRowView)
                {
                    if (((DataRowView)r.RowKey).Row == row)
                    {
                        rowf = r;
                        break;
                    }
                }
            }
            return rowf;
        }
        #endregion

        #region CheckIsOnHeader
        public bool CheckIsOnHeader(Point p)
        {
            bool _isOn = false;
            try
            {
                if (p.X >= this.Bounds.Left && p.X <= this.Bounds.Right && p.Y >= this.ColumnInfos[0].Bounds.Top && p.Y <= this.ColumnInfos[0].Bounds.Bottom)
                {
                    _isOn = true;
                }
            }
            catch { }
            return _isOn;
        }
        #endregion

        #region SelectedcColumn
        public void SelectedColumn(GridColumn column,SelectedColumnType type)
        {
            try
            {
                if (type != SelectedColumnType.Default)
                {
                    GridView view = column.View as GridView;
                    if (view != null)
                    {
                        if (type == SelectedColumnType.SelectedAll)
                        {

                        }
                        else
                        {
                            if (this.LevelTree.IsRootLevel)
                                SelectedColumn(column, SelectedColumnType.SelectedAll);
                            else
                            {                                
                                view.SelectCells(0, column,0, column);
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region Static  function
        public static DataRow GetDataRow(GridRow row)
        {            
            DataRow dr = null;
            if (row.RowKey is DataRow)
                dr = row.RowKey as DataRow;
            else if (row.RowKey is DataRowView)
                dr = ((DataRowView)row.RowKey).Row;            
            return dr;
        }
        #endregion
    }
    #region ColorUnEditableColumn
    public class ColorColumn
    {
        public ColorColumn()
        {

        }

        public ColorColumn(Color color,Color color2)
        {
            _color = color;
            _color2 = color2;
        }

        Color _color = System.Drawing.SystemColors.ButtonFace;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        Color _color2 = Color.FromArgb(255,252,250);
        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }

    }
    #endregion

    #region TypeGet
    public enum TypeGet
    {
        Column,ColumnInfo,Row,RowInfo
    }
    #endregion

    #region ColumnClickEventArgs
    public class GridColumnClickEventArgs: MouseEventArgs
    {
        public GridColumn Column = null;
        public GridColumnClickEventArgs(MouseButtons button,int clicks,int x,int y,int delta,GridColumn column) : base(button,clicks,x,y,delta)
        {
            Column = column;
        }
    }
    #endregion

    public delegate void GridColumnClickEventHanlder(object sender,GridColumnClickEventArgs e);
    public delegate void DataRowDeleteEventHandler(object sender, DataRowDeleteEventArgs e);

    #region Valiate class
    public enum ValidateValueType
    {
        None,NullValue,InvalidType,Both
    }

    public class ValiateValue
    {
        public ValiateValue(string text)
        {
            _validateText = text;
            _valiateType = ValidateValueType.NullValue;
        }
        public ValiateValue(string text, ValidateValueType type)
        {
            _validateText = text;
            _valiateType = type;
        }

        string _validateText = "";

        public string ValidateText
        {
            get { return _validateText; }
        }
        ValidateValueType _valiateType = ValidateValueType.NullValue;

        public ValidateValueType ValiateType
        {
            get { return _valiateType; }
        }


    }
    #endregion

    #region GridRowEx
    public class GridRowNew : GridRow
    {
        public object this[int columnIndex]
        {
            get
            {
                DataRow row = this.RowKey as DataRow;
                if (row == null) return null;
                else return row[columnIndex];
            }
            set
            {
                DataRow row = this.RowKey as DataRow;
                row[columnIndex] = value;
            }
        }

        public object this[string columnName]
        {
            get
            {
                DataRow row = this.RowKey as DataRow;
                if (row == null) return null;
                else return row[columnName];
            }
            set
            {
                DataRow row = this.RowKey as DataRow;
                row[columnName] = value;
            }
        }

        public bool IsNewRow
        {
            get
            {
                return this.RowHandle < 0 ? true : false;
            }
        }
    }

    #endregion

    #region SelectedColumnType
    public enum SelectedColumnType
    {
        Default,
        SelectedSingle,
        SelectedAll
    }
    #endregion

    #region DataRowDeleteEventArgs
    public class DataRowDeleteEventArgs: EventArgs
    {
        public DataRow[] row;
        public bool Cancel = false;
        public DataRowDeleteEventArgs(DataRow[] r)
        {
            row = r;
        }
    }
    #endregion
}
