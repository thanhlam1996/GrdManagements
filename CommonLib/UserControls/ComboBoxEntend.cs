using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Infragistics.Win.UltraWinGrid;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Drawing.Design;

namespace CommonLib.UserControls
{
    [DefaultProperty("Items"), LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
    public class ComboBoxEntend : DevExpress.XtraEditors.ButtonEdit, System.ComponentModel.ISupportInitialize 
    {
        public static bool UseLookAndFeel = true;
        private PopupControl _popup = null;
        private bool _useSaveButton = false, _autoDisplayText = false, _showCaption = false, _isKeyPress = false, _formattingEnabled = true, _autoSelectFirstItem=true,_autoNullValueOnNotMatch= true;
        private int _numberItemDisplayed = 8;
        private Size _sizeOfPopup = Size.Empty;
        private string _columnDisplay = "", _formatString="";
        private string _displayMember = "", _valueMember = "", _displayText = "", _oldText = "";
        private CDevItemCollection _items = null;
        private string _rootRowFilter = string.Empty;
        private string[] curDisplayColumn = new string[] { };
        private bool isOnSearch = false;

        #region Init

        public ComboBoxEntend()
            : base()
        {
            base.Properties.Buttons.Clear();
            base.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
            _popup = new PopupControl();
            _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = _showCaption;
            _popup.pscButton1.Click += new EventHandler(pscButton1_Click);
            
            this.LookAndFeel.UseDefaultLookAndFeel = UseLookAndFeel;
            this.LookAndFeel.UseWindowsXPTheme = !UseLookAndFeel;
            _items = new CDevItemCollection(this);
            try
            {
                Infragistics.Win.Appearance apr = new Infragistics.Win.Appearance();
                apr.BackColor = System.Drawing.Color.Lavender;
                apr.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(186)))), ((int)(((byte)(239)))));
                apr.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                Grid.DisplayLayout.Override.HeaderAppearance = apr;
            }
            catch { }
            _popup.ClosePopup += new EventHandler(upopCon_Closed);
            this.MouseWheel += new MouseEventHandler(ComboBoxEntend_MouseWheel);
        }

        void ComboBoxEntend_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                    _popup.ultgData.ActiveRowScrollRegion.Scroll(RowScrollAction.LineUp);
                else
                    _popup.ultgData.ActiveRowScrollRegion.Scroll(RowScrollAction.LineDown);
            }
            catch { }
        }

        void upopCon_Closed(object sender, EventArgs e)
        {
            try
            {
                if (IsSelectDataAble)
                {
                    DataView dv = null;
                    if (DataSource is DataView)
                        dv = (DataView)DataSource;
                    else if (DataSource is DataTable)
                        dv = ((DataTable)DataSource).DefaultView;
                    if (dv.RowFilter != _rootRowFilter)
                    {
                        isOnSearch = true;
                        dv.RowFilter = _rootRowFilter;
                        isOnSearch = false;                        
                    }
                }
                foreach (UltraGridRow row in Grid.Selected.Rows)
                {
                    if (row != Grid.ActiveRow)
                        row.Selected = false;
                }                
            }
            catch { }
        }                  
        
        #endregion

        #region Properties
        System.Windows.Forms.ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDown;
        [DefaultValue(ComboBoxStyle.DropDown)]
        public System.Windows.Forms.ComboBoxStyle DropDownStyle
        {
            get { return _dropDownStyle; }
            set 
            {
                try
                {
                    _dropDownStyle = value;
                    if (this.Properties != null)
                    {
                        if (this.Properties.Buttons.Count > 0)
                        {
                            if (_dropDownStyle == ComboBoxStyle.DropDownList)
                            {
                                this.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown;
                                this.Properties.ReadOnly = true;
                            }
                            else if (_dropDownStyle == ComboBoxStyle.DropDown)
                            {
                                this.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;
                                this.Properties.ReadOnly = false;
                            }
                            else
                            {
                                this.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo;
                                this.Properties.ReadOnly = false;
                            }
                        }
                    }
                }
                catch (Exception ex) { System.Console.WriteLine(ex.Message); }
            }
        }

        void InitCurDisplayColumn()
        {
            if (_initDisplayColumn) return;
            curDisplayColumn = new string[] { };
            if (_valueMember == null) _valueMember = "";
            if (_displayMember == null) _displayMember = "";
            if (_columnDisplay == null) _columnDisplay = "";
            if (!IsSelectDataAble && _valueMember == "" && _displayMember=="")
            {
                Array.Resize(ref curDisplayColumn, curDisplayColumn.Length + 1);
                curDisplayColumn[0] = "Value";
                return;
            }
            if (_columnDisplay != "")
                curDisplayColumn = _columnDisplay.Split(',');
            else
            {
                if (_autoDisplayText)
                {
                    if(_valueMember!="")
                        curDisplayColumn = new string[] { _valueMember };
                    if (_displayMember != "")
                    {
                        Array.Resize(ref curDisplayColumn, curDisplayColumn.Length + 1);
                        curDisplayColumn[curDisplayColumn.Length-1] = _displayMember;
                    }
                }
                else
                {
                    curDisplayColumn = new string[] { _displayMember };
                }
            }
        }

        [Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ValueMember
        {
            get { return _valueMember; }
            set
            {
                if (value != _valueMember)
                {
                    _initDisplayColumn = false;
                }
                else
                    return;
                _valueMember = value;
                ReInitDropDown();
                InitCurDisplayColumn();
                if (AutoSelectFirstItem)
                    SelectedIndex = 0;
                else
                    SetValueToText(null);
                OnChaneValueMember(this, new EventArgs());
            }
        }

        [Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden) ,Description("Value display to textbox of selected item")]
        public string DisplayMember
        {
            get 
            { 

                return _displayMember; 
            }
            set
            {
                if (value != _displayMember)
                {
                    _initDisplayColumn = false;
                }
                else
                    return;
                _displayMember = value;
                ReInitDropDown();
                InitCurDisplayColumn();
                if (AutoSelectFirstItem)
                    SelectedIndex = 0;
                else
                    SetValueToText(null);
            }
        }

        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        //[Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CDevItemCollection Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new CDevItemCollection(this);
                }
                return _items;
            }
        }

        public object SelectedItem
        {
            get 
            {
                try
                {
                    return Grid.ActiveRow.ListObject;
                }
                catch { }
                return null;
            }
            set 
            {
                try
                {
                    if (value == null)
                    {
                        _displayText = "";
                        _value = null;
                        SetValueToText(null);
                    }
                    else
                    {
                        int index = Items.IndexOf(value);
                        if (this.Grid.ActiveRow != null)
                            this.Grid.ActiveRow.Activated = false;
                        if (index >= 0)
                        {
                            this.Grid.Rows[index].Activate();
                        }
                        SetValueToText(this.Grid.ActiveRow);
                        OnSelectedValueChange(this, new EventArgs());
                        OnSelectedIndexChange(this, new EventArgs());
                        OnSelectionChangeCommitted(this, new EventArgs());
                    }
                }
                catch { }
            }
        }

        public int DropDownWidth
        {
            get { return _sizeOfPopup.Width; }
            set { _sizeOfPopup.Width = value; }
        }

        [DefaultValue(true)]
        public bool FormattingEnabled
        {
            get { return _formattingEnabled; }
            set { _formattingEnabled = value; }
        }

        [DefaultValue(true)]
        public bool AutoNullValueOnNotMatch
        {
            get { return _autoNullValueOnNotMatch; }
            set { _autoNullValueOnNotMatch = value; }
        }
        [DefaultValue(true)]
        public bool AutoSelectFirstItem
        {
            get { return _autoSelectFirstItem; }
            set { _autoSelectFirstItem = value; }
        }

        public bool ShowCaption
        {
            get
            {
                if (_popup != null)
                    return _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible;
                else
                    return false;
            }
            set
            {
                _showCaption = value;
                if (_popup != null)
                {
                    _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = value;
                }
            }
        }

        public string OldText
        {
            get { return _oldText; }
            set { _oldText = value; }
        }

        private object _value=null;

        public string DisplayText
        {
            get { return _displayText; }
            set { _displayText = value; }
        }

        public string FormatString
        {
            get { return _formatString; }
            set { _formatString = value; }
        }        

        public string ColumnDisplay
        {
            get { return _columnDisplay; }
            set 
            {
                if (value != _columnDisplay)
                {
                    _initDisplayColumn = false;
                    _isSetSize = false;
                }
                _columnDisplay = value;
                //InitDisplayColumn();
                ReInitDropDown();
                InitCurDisplayColumn();
            }
        }

        [DefaultValue(false)]
        public bool AutoDisplayText
        {
            get { return _autoDisplayText; }
            set { _autoDisplayText = value; }
        }

        public int NumberItemDisplayed
        {
            get { return _numberItemDisplayed; }
            set { _numberItemDisplayed = value; }
        }

        public Size SizeOfPopup
        {
            get { return _sizeOfPopup;}
            set 
            {
                if (value.IsEmpty) return;
                if (_popup != null) _popup.Size = value; 
            }
        }

        public UltraGrid Grid
        {
            get { if (_popup == null) return null; else return _popup.ultgData; }
        }

        public bool UseSaveButton
        {
            get { return _useSaveButton; }
            set
            {
                _useSaveButton = value;
                if (_popup == null)
                {
                    _popup = new PopupControl();
                    _popup.pscButton1.Click += new EventHandler(pscButton1_Click);
                }
                _popup.UseSave = _useSaveButton;
                if (_useSaveButton)
                {
                    _popup.ultgData.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
                    _popup.ultgData.DisplayLayout.Override.AllowAddNew = AllowAddNew.FixedAddRowOnTop;
                    _popup.ultgData.DisplayLayout.Override.TemplateAddRowPrompt = "Thêm mới...";
                }
                else
                    _popup.ultgData.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            }
        }

        public object Value
        {
            get { return _value; }
        }
       
        [Browsable(false)]
        public object SelectedValue
        {
            get 
            {
                return _value; 
            }
            set 
            {
                try
                {
                    if (_value is string && value !=null)
                    {
                        if (_value.ToString().Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            return;
                        }
                    }
                    else if (_value == value)
                    {
                        return;
                    }
                    if (value == null)
                    {
                        _displayText = "";
                        _value = null;
                        SetValueToText(null);
                    }
                    else
                    {
                        int index = -1;
                        if (this._valueMember == "")
                            index = Items.IndexOf(value);
                        else
                            index = Items.IndexOfKey(value);
                        if (this.Grid.ActiveRow != null)
                            this.Grid.ActiveRow.Activated = false;
                        if (index >= 0)
                        {
                            this.Grid.Rows[index].Activate();
                        }
                        SetValueToText(this.Grid.ActiveRow);
                    }
                    OnSelectedValueChange(this, new EventArgs());
                    OnSelectedIndexChange(this, new EventArgs());
                    OnSelectionChangeCommitted(this, new EventArgs());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #region Find
        internal object GetDataByKey(object value)
        {
            object val = null;
            try
            {
                UltraGridRow row = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r) 
                { 
                    if (value is string)
                    {
                        int com = StringComparer.CurrentCultureIgnoreCase.Compare(r.Cells[this.ValueMember].Value, value);
                        if (com == 0) return true; else return false;
                    }
                    else
                        if (r.Cells[this.ValueMember].Value == value) return true; else return false; 
                });
                return row.ListObject;
            }
            catch { }
            return val;
        }

        internal int GetIndexByKey(object value)
        {
            int index = -1;
            try
            {
                UltraGridRow row = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r) 
                {
                    if (value is string)
                    {
                        int com = StringComparer.CurrentCultureIgnoreCase.Compare(r.Cells[this.ValueMember].Value, value);
                        if (com == 0) return true; else return false;
                    }
                    else
                        if (r.Cells[this.ValueMember].Value == value) return true; else return false; 
                    
                });
                index= row.Index;
            }
            catch { }
            return index;
        }

        internal int GetIndex(object value)
        {
            int index = -1;
            try
            {                
                UltraGridRow row = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r) 
                {
                    if (value is string)
                    {
                        int com = StringComparer.CurrentCultureIgnoreCase.Compare(r.ListObject, value);
                        if (com==0) return true; else return false;
                    }
                    else
                        if (r.ListObject == value) return true; else return false;
                });
                index = row.Index;                
            }
            catch { }
            return index;
        }

        internal object GetDataByList(object value)
        {
            object val = null;
            try
            {
                int index = this.Items.IndexOf(value);
                val = this.Items[index];
            }
            catch { }
            return val;
        }

        public void SelectValueByKey(object value, string Key)
        {
            try
            {
                if (value == null)
                {
                    if (Grid.ActiveRow != null)
                        Grid.ActiveRow.Activated = false;
                    _displayText = "";
                    SetValueToText(null);
                    OnSelectedValueChange(this, new EventArgs());
                    OnSelectedIndexChange(this, new EventArgs());
                    OnSelectionChangeCommitted(this, new EventArgs());
                    return;
                }
                UltraGridRow row = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r) { if (r.Cells[Key].Value == value) return true; else return false; });
                if (row == null)
                {
                    if (Grid.ActiveRow != null)
                        Grid.ActiveRow.Activated = false;
                    _displayText = "";
                    SetValueToText(null);
                    OnSelectedValueChange(this, new EventArgs());
                    OnSelectedIndexChange(this, new EventArgs());
                    OnSelectionChangeCommitted(this, new EventArgs());
                    return;
                }
                if (this.Grid.ActiveRow != null)
                    this.Grid.ActiveRow.Activated = false;
                if (row.Index >= 0)
                {
                    this.Grid.Rows[row.Index].Activate();
                }
                SetValueToText(this.Grid.ActiveRow);
                OnSelectedValueChange(this, new EventArgs());
                OnSelectedIndexChange(this, new EventArgs());
                OnSelectionChangeCommitted(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        public int SelectedIndex
        {
            get
            {
                if (this.DesignMode) return -1;
                int index = -1;
                try
                {
                    if (this.Grid == null)
                        return -1;
                    else if (this.Grid.ActiveRow == null)
                        return -1;
                    else
                        return this.Grid.ActiveRow.Index;
                }
                catch { }
                return index;
            }
            set
            {
                try
                {
                    if (this.Grid.ActiveRow != null)
                    {
                        this.Grid.ActiveRow.Activated = false;
                        this.Grid.Selected.Rows.Clear();
                    }
                    if (value >= 0)
                    {
                        this.Grid.Rows[value].Activate();
                    }
                    SetValueToText(this.Grid.ActiveRow);
                    OnSelectedValueChange(this, new EventArgs());
                    OnSelectedIndexChange(this, new EventArgs());
                    OnSelectionChangeCommitted(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        [DefaultValue((string)null), RefreshProperties(RefreshProperties.Repaint), AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get 
            {
                if (_popup != null)
                {
                    return _popup.ultgData.DataSource;
                }
                else
                    return null;
            }
            set
            {                    
                if (_popup == null)
                {
                    _popup = new PopupControl();
                    _popup.pscButton1.Click += new EventHandler(pscButton1_Click);
                }
                if (_popup.ultgData.DataSource == null && value == null)
                    return;
                else if (value == null)
                {
                    SetValueToText(null);
                    return;
                }

                if (value is object[])
                {
                    object[] val = (object[])value;
                    List<object> list = new List<object>();
                    for (int i = 0; i < val.Length; i++)
                    {
                        list.Add(val[i]);
                    }
                    _popup.ultgData.DataSource = list;
                }
                else if (value is List<object>)
                {
                    _popup.ultgData.DataSource = value;
                }
                else
                    _popup.ultgData.DataSource = value;
                if (value is DataView)
                {
                    _rootRowFilter = ((DataView)value).RowFilter;
                    ((DataView)value).ListChanged += new ListChangedEventHandler(ComboBoxEntend_ListChanged);
                }                
                else
                    _rootRowFilter = string.Empty;                
                _value = null;
                Reset();
                _isSetSize = false;
                InitDropDown();
                if (AutoSelectFirstItem)
                    SelectedIndex = 0;
                else
                    SetValueToText(null);
            }
        }

        void ComboBoxEntend_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!isOnSearch)
                {
                    _rootRowFilter = ((DataView)DataSource).RowFilter;
                    _items.InitListSource();
                }
            }
            catch { }
        }

        #endregion        

        #region Override
        protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)
        {
            base.OnClickButton(buttonInfo);
            if (_popup == null) return;
            if (_popup.ultgData.DataSource == null) return;
            try
            {
                if (_popup.IsDisplayed)
                {
                    _popup.Close();
                    return;
                }
                ShowPopup();
                _popup.ultgData.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            try
            {
                if (!_popup.IsDisplayed && this.SelectionLength==0)
                    this.SelectAll();
            }
            catch { }
        }

        bool _beginClick = true;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this._popup.IsDisplayed)
                _beginClick = true;
            if (_beginClick && this.Items.Count>0)
            {
                if (this.ViewInfo.PressedInfo.HitTest != DevExpress.XtraEditors.ViewInfo.EditHitTest.Button && this.ViewInfo.PressedInfo.HitTest != DevExpress.XtraEditors.ViewInfo.EditHitTest.Button2 && !_popup.IsDisplayed && this.SelectionLength==0)
                {
                    this.SelectAll();                    
                    if (this.Properties.ReadOnly)
                    {
                        ShowPopup();
                    }
                }
            }
            _beginClick = false;
        }        

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Text == "")
                this.SelectedIndex = -1;
            _beginClick = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                int index = -1;
                switch (e.KeyData)
                {
                    case Keys.Right:
                        this.SelectionStart = this.SelectionStart + this.SelectionLength - 1;
                        e.Handled = true;
                        break;
                    case Keys.Up:
                        this.SelectionStart = 0;
                        _oldText = this.Text;
                        if (_popup.ultgData.ActiveRow != null)
                        {
                            index = _popup.ultgData.ActiveRow.Index - 1;
                            _popup.ultgData.ActiveRow.Selected = false;
                        }
                        else
                            index = -1;
                        _isKeyPress = true;
                        if (index >= _popup.ultgData.Rows.Count)
                        {
                            index = _popup.ultgData.Rows.Count - 1;
                        }
                        if (index < 0) index = 0;
                        if (_popup.ultgData.ActiveRow != null)
                            _popup.ultgData.ActiveRow.Selected = false;
                        _popup.ultgData.ActiveRow = _popup.ultgData.Rows[index];
                        _popup.ultgData.ActiveRow.Selected = true;
                        SetValueToText(_popup.ultgData.ActiveRow);
                        _isKeyPress = false;
                        break;
                    case Keys.Down:
                        this.SelectionStart = 0;
                        _oldText = this.Text;
                        if (_popup.ultgData.ActiveRow != null)
                        {
                            index = _popup.ultgData.ActiveRow.Index + 1;
                            _popup.ultgData.ActiveRow.Selected = false;
                        }
                        else
                            index = -1;
                        _isKeyPress = true;
                        if (index >= _popup.ultgData.Rows.Count)
                        {
                            index = _popup.ultgData.Rows.Count - 1;
                        }
                        if (index < 0) index = 0;
                        if (_popup.ultgData.ActiveRow != null)
                            _popup.ultgData.ActiveRow.Selected = false;
                        _popup.ultgData.ActiveRow = _popup.ultgData.Rows[index];
                        _popup.ultgData.ActiveRow.Selected = true;
                        SetValueToText(_popup.ultgData.ActiveRow);
                        _isKeyPress = false;
                        break;
                }
                base.OnKeyDown(e);
            }
            catch { }
        }

        protected override void OnEditorKeyUp(KeyEventArgs e)
        {
            base.OnEditorKeyUp(e);
            try
            {
                string value = this.Text.Trim().ToLower();
                UltraGridRow row = _popup.ultgData.ActiveRow;

                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        _popup.Close();
                        if (_value != null)
                        {
                            int ind = -1;
                            if (ValueMember == "")
                                ind = Items.IndexOf(_value);
                            else
                                ind = Items.IndexOfKey(_value);
                            this.Grid.ActiveRow.Selected = false;
                            this.Grid.Selected.Rows.Clear();
                            ////if (IsSelectDataAble)
                            ////{
                            ////    _popup.Table.DefaultView.RowFilter = _rootRowFilter;
                            ////}
                            this.Grid.Rows[ind].Activate();
                            SetValueToText(this.Grid.ActiveRow);
                        }
                        break;
                    case Keys.F1:
                        ShowPopup();
                        this.SelectAll();
                        break;
                    case Keys.F4:
                        ShowPopup();
                        this.SelectAll();
                        break;
                    case Keys.Enter:
                        _oldText = this.Text;
                        if (this.Text.Trim() == "" && !_popup.IsDisplayed)
                        {
                            SetValueToText(null);
                            return;
                        }
                        if (row != null)
                        {
                            if (this.ValueMember == "")
                            {
                                if (row.ListObject.ToString().ToLower().Contains(value))
                                {
                                    SetValueToText(row);
                                    _popup.Close();
                                    return;
                                }
                                else
                                {
                                    row.Selected = false;
                                }
                            }
                            else
                            {
                                if (_value == row.Cells[_valueMember].Value)
                                {
                                    SetValueToText(row);
                                    _popup.Close();
                                    return;
                                }

                                if (row.Cells[_displayMember].Value.ToString().Trim().ToLower().Contains(value))
                                {
                                    SetValueToText(row);
                                    _popup.Close();
                                    return;
                                }
                                else if (row.Cells[_valueMember].Value.ToString().Trim().ToLower().Contains(value))
                                {
                                    SetValueToText(row);
                                    _popup.Close();
                                    return;
                                }
                                else
                                {
                                    row.Selected = false;
                                }
                            }
                        }
                        else if (AutoNullValueOnNotMatch)
                            SetValueToText(null);
                        _popup.Close();
                        break;
                    case Keys.Up:
                        break;
                    case Keys.Down:
                        break;
                    default:
                        if (this.Properties.ReadOnly) return;
                        if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3 || e.KeyCode == Keys.F5 || e.KeyCode == Keys.F6 || e.KeyCode == Keys.F7 || e.KeyCode == Keys.F8 || e.KeyCode == Keys.F9 || e.KeyCode == Keys.F10 || e.KeyCode == Keys.F11 || e.KeyCode == Keys.F12 || e.Alt || e.Control)
                        {
                        }
                        else if (char.IsLetterOrDigit((char)e.KeyCode) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                        {
                            if (e.Control || e.Alt)
                            {
                                return;
                            }
                            try
                            {
                                this.SelectionLength = this.Text.Length - this.SelectionStart;
                            }
                            catch { }
                            _oldText = this.Text;
                            if (!_popup.IsDisplayed && (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete))
                                ShowPopup();
                            if (this.SelectionLength == this.Text.Length && this.Text.Trim().Length > 0) return;

                            if (!IsSelectDataAble)
                            {
                                UltraGridRow rFind = FindRow(value);
                                if (rFind != null)
                                {
                                    rFind.Selected = true;
                                    this.Grid.Rows[rFind.Index].Activate();
                                }
                            }
                            else
                            {
                                decimal res = 0;
                                DataView dv = null;
                                if (_popup.ultgData.DataSource is DataView)
                                    dv = (DataView)_popup.ultgData.DataSource;
                                else
                                    dv = _popup.Table.DefaultView;
                                string filter = "";
                                if (_popup.Table.Columns[_valueMember].DataType != typeof(DateTime) && _popup.Table.Columns[_valueMember].DataType != typeof(string))
                                {
                                    if (decimal.TryParse(value, out res))
                                    {
                                        filter = _valueMember + "= " + res;
                                    }
                                }
                                else
                                {
                                    filter = _valueMember + " like '%" + value + "%'";
                                }
                                if (_popup.Table.Columns[_displayMember].DataType != typeof(DateTime) && _popup.Table.Columns[_displayMember].DataType != typeof(string))
                                {
                                    if (decimal.TryParse(value, out res))
                                    {
                                        filter = (filter == "" ? "" : filter + " or ") + _displayMember + "= " + res;
                                    }
                                }
                                else
                                {
                                    filter = (filter == "" ? "" : filter + " or ") + _displayMember + " like '%" + value + "%'";
                                }
                                if (row != null) row.Selected = false;
                                if (filter == "") filter = string.Empty;
                                if (_rootRowFilter != "")
                                {
                                    if (filter == "") filter = "1=1";
                                    filter = "( " +_rootRowFilter + " ) and (" + filter +")";
                                }
                                isOnSearch = true;
                                dv.RowFilter = filter;
                                isOnSearch = false;
                                if (_popup.ultgData.Rows.Count > 0)
                                {
                                    UltraGridRow rFind = FindRow(value);
                                    if (rFind != null)
                                    {
                                        rFind.Selected = true;
                                        this.Grid.Rows[rFind.Index].Activate();
                                    }
                                    if (_popup.ultgData.ActiveRow == null)
                                    {
                                        _popup.ultgData.Rows[0].Selected = true;
                                        _popup.ultgData.Rows[0].Activate();
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region IsSelectDataAble
        internal bool IsSelectDataAble
        {
            get
            {
                if (Grid.DataSource is DataTable || Grid.DataSource is DataView)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region FindRow()
        UltraGridRow FindRow(string value)
        {
            UltraGridRow rFind = null;
            if (this._valueMember == "" || this._displayMember=="")
            {
                if (this._valueMember == "" && this._displayMember == "")
                {
                    rFind = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r)
                    {
                        if (r.ListObject.ToString().StartsWith(value, true, null))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                }
                else if (this._valueMember == "" && this._displayMember!="")
                {
                    rFind = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r)
                    {
                        if (r.Cells[_displayMember].Value.ToString().StartsWith(value, true, null))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                }
                else
                {
                    rFind = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r)
                    {
                        if (r.Cells[_valueMember].Value.ToString().StartsWith(value, true, null))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                }                
            }
            else
            {
                rFind = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r)
                {
                    if (r.Cells[_valueMember].Value.ToString().StartsWith(value, true, null))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                if (rFind == null)
                {
                    rFind = Array.Find((UltraGridRow[])this.Grid.Rows.All, delegate(UltraGridRow r)
                    {
                        if (r.Cells[_displayMember].Value.ToString().StartsWith(value, true, null))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                }
            }
            return rFind;
        }
        #endregion

        #region InitDisplayColumn
        internal bool _initDisplayColumn = false;

        void InitDisplayColumn()
        {
            try
            {
                if (_initDisplayColumn) return;
                if (curDisplayColumn.Length > 0)
                {
                    foreach (UltraGridColumn col in _popup.ultgData.DisplayLayout.Bands[0].Columns)
                    {
                        if (curDisplayColumn.Contains(col.Key))
                        {
                            if (col.Key == _displayMember)
                            {
                                col.AutoSizeMode = ColumnAutoSizeMode.Default;
                            }
                            else
                            {
                                col.AutoSizeMode = ColumnAutoSizeMode.None;                                
                                col.PerformAutoResize(PerformAutoSizeType.AllRowsInBand,_showCaption);
                                col.Width += 23;
                                col.LockedWidth = true;
                            }
                            col.Hidden = false;
                        }
                        else
                            col.Hidden = true;
                    }
                }
                if (_popup.ultgData.DisplayLayout.Bands[0].Columns.Count > 0)
                    _initDisplayColumn = true;
            }
            catch { }
        }
        #endregion

        #region pscButton1_Click
        void pscButton1_Click(object sender, EventArgs e)
        {
            OnSaveClick(sender, e);
        }
        #endregion

        #region InitDropDown
        bool _intEventGrid = false;
        void InitDropDown()
        {
            if (_popup == null) return;
            if (!_intEventGrid)
            {
                _popup.ultgData.Click += new EventHandler(ultgData_Click);
                _popup.ultgData.DoubleClickCell += new DoubleClickCellEventHandler(ultgData_DoubleClickCell);
                _popup.ultgData.KeyUp += new KeyEventHandler(ultgData_KeyUp);
                _popup.ultgData.DoubleClickRow += new DoubleClickRowEventHandler(ultgData_DoubleClickRow);
                _intEventGrid = true;
            }
        }

        void ReInitDropDown()
        {
            if (_popup == null) return;
            if (!_intEventGrid)
            {
                _popup.ultgData.Click += new EventHandler(ultgData_Click);
                _popup.ultgData.DoubleClickCell += new DoubleClickCellEventHandler(ultgData_DoubleClickCell);
                _popup.ultgData.KeyUp += new KeyEventHandler(ultgData_KeyUp);
                _popup.ultgData.DoubleClickRow += new DoubleClickRowEventHandler(ultgData_DoubleClickRow);
                _popup.ultgData.MouseMove += new MouseEventHandler(ultgData_MouseMove);
                _intEventGrid = true;
            }
            _isSetSize = false;
        }

        void ultgData_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Infragistics.Win.UIElement element = _popup.ultgData.DisplayLayout.UIElement.ElementFromPoint(e.Location);
                    UltraGridRow row = element.SelectableItem as UltraGridRow;
                    if (row!=null)
                    {
                        if (!row.Activated)
                            row.Activated = true;
                    }
                }
                else if (e.Button == MouseButtons.None)
                {
                    _popup.ultgData.Focus();
                }
            }
            catch { }
        }
        
        #endregion

        #region Gird event
        void ultgData_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            UltraGridRow row = _popup.ultgData.ActiveRow;
            SetValueToText(row);
            _popup.Close();
        }

        void ultgData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow row = _popup.ultgData.ActiveRow;
            SetValueToText(row);
            _popup.Close();
        }

        void ultgData_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                UltraGridRow row = _popup.ultgData.ActiveRow;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!_popup.ultgData.ActiveCell.IsInEditMode)
                        {
                            SetValueToText(row);
                            _popup.Close();
                        }
                        else
                        {
                            _popup.ultgData.PerformAction(UltraGridAction.ExitEditMode);
                        }
                        break;
                    case Keys.Escape:
                        _popup.Close();
                        break;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void ultgData_AfterRowActivate(object sender, EventArgs e)
        {
            //ultgData_Click(sender, e);
        }

        void ultgData_Click(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow row = _popup.ultgData.ActiveRow;
                if (_popup.ultgData.Selected.Rows.Count > 1)
                {
                    _popup.ultgData.Selected.Rows.Clear();
                    row.Selected = true;
                    row.Activate();
                }
                Infragistics.Win.UIElement element = _popup.ultgData.DisplayLayout.UIElement.ElementFromPoint(_popup.ultgData.DisplayLayout.UIElement.CurrentMousePosition);
                if (element != null)
                {
                    if (element.SelectableItem == null) return;
                    if (!element.SelectableItem.IsSelectable)
                    {
                        return;
                    }
                    if (element.Parent is Infragistics.Win.UltraWinGrid.HeaderUIElement) return;
                }
                else return;
                SetValueToText(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region SetValueToText
        void SetValueToText(UltraGridRow row)
        {
            try
            {
                if (row != null)
                {                    
                    if (_autoDisplayText)
                    {
                        if (this._displayMember == "")
                        {
                            _value = row.ListObject;
                            this._displayText = row.ListObject.ToString();
                            this.Text = this._displayText;
                        }
                        else
                        {
                            _value = row.Cells[this._valueMember].Value;
                            string txt = row.Cells[this._displayMember].Value.ToString();
                            if (FormatString != "" && FormattingEnabled)
                            {
                                try
                                {
                                    txt = String.Format(FormatString, row.Cells[this._displayMember].Value);
                                }
                                catch { }
                            }
                            _displayText = txt;
                            this.Text = row.Cells[this._valueMember].Value.ToString() + "    " + txt;
                        }
                    }
                    else
                    {
                        if (this._displayMember == "")
                        {
                            _value = row.ListObject;
                            this._displayText = row.ListObject.ToString();
                            this.Text = this._displayText;
                        }
                        else
                        {
                            _value = row.Cells[this._valueMember].Value;
                            string txt = row.Cells[this._displayMember].Value.ToString();
                            if (FormatString != "" && FormattingEnabled)
                            {
                                try
                                {
                                    txt = String.Format(FormatString, row.Cells[this._displayMember].Value);
                                }
                                catch { }
                            }
                            _displayText = txt;
                            this.Text = txt;
                        }
                    }
                    if (!_isKeyPress)
                    {
                        if (!_useSaveButton)
                            _popup.Close();
                    }
                    _isKeyPress = false;
                }
                else
                {
                    _value = null;
                    _displayText = "";
                    this.Text = "";
                }
                OnSelectedValueChange(this, new EventArgs());
                OnSelectedIndexChange(this, new EventArgs());
                OnSelectionChangeCommitted(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region GetValueByKey
        public object GetValueByKey(string Key)
        {
            object val = null;
            try
            {
                if (_value.ToString() == _popup.ultgData.ActiveRow.Cells[_valueMember].Value.ToString())
                    val = _popup.ultgData.ActiveRow.Cells[Key].Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return val;
        }
        #endregion

        bool _isSetSize = false;

        #region ShowPopup
        private void ShowPopup()
        {
            try
            {
                if (_popup.IsDisplayed) return;
                InitDisplayColumn();
                if (!IsSelectDataAble)
                {
                    #region Set size
                    if (!_isSetSize && _popup != null)
                    {
                        Items.InitListSource();
                        int ws = 0;
                        if (_popup.ultgData.ActiveRowScrollRegion != null)
                            if (!_popup.ultgData.ActiveRowScrollRegion.Hidden)
                            {
                                ws = 13;
                            }
                        _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = _showCaption;
                        int width = Math.Min(this.DropDownWidth, this.Width);// _popup.ultgData.DisplayLayout.Bands[0].Columns[0].CalculateAutoResizeWidth(PerformAutoSizeType.AllRowsInBand, _showCaption);
                        bool chkItem = _numberItemDisplayed > Items.Count;
                        this._popup.Height = (chkItem ? Items.Count : _numberItemDisplayed) * 18 + (chkItem ? 5 : 0) + (_showCaption ? 24 : 0) + (_useSaveButton ? 36 : -12) + ws + 3;
                        this._popup.Width = width;
                        if (this._popup.Width < this.Width)
                            this._popup.Width = this.Width;
                        _popup.ResetSize();
                        _popup.DrawBorder();

                        _isSetSize = true;
                    }
                    UltraGridRow r = _popup.ultgData.ActiveRow;
                    if (r != null)
                        _popup.ultgData.ActiveRowScrollRegion.ScrollRowIntoView(r);
                    if (_popup.Size.Width < this.Width)
                        _popup.Size = new Size(this.Width, _popup.Height);                    
                    _popup.ShowPopup(this);
                    #endregion
                }
                else
                {
                    #region Set size
                    if (!_isSetSize && _popup != null)
                    {
                        if (_sizeOfPopup.Width > 5 && _sizeOfPopup.Height > 20)
                        {
                            _popup.Size = _sizeOfPopup;
                        }
                        else
                        {                                                        
                            int width = Math.Min(this.DropDownWidth, this.Width);
                            //foreach (string s in curDisplayColumn)
                            //{
                            //    int w = 0;
                            //    UltraGridColumn col = _popup.ultgData.DisplayLayout.Bands[0].Columns[s];

                            //    if (col.AutoSizeMode == ColumnAutoSizeMode.None)
                            //        w = col.Width;
                            //    else if (col.MaxWidth != 0)
                            //        w = col.MaxWidth;
                            //    else
                            //        w = col.CalculateAutoResizeWidth(PerformAutoSizeType.AllRowsInBand, _showCaption);
                                
                            //    width += w;
                            //}
                            this._popup.Width = width;
                            if (_popup.ultgData.ActiveRowScrollRegion != null)
                            {
                                int ws= 0;
                                if (!_popup.ultgData.ActiveRowScrollRegion.Hidden)
                                {
                                    this._popup.Width += 23;
                                    ws=13;
                                }
                                bool chkItem = _numberItemDisplayed > Items.Count;
                                this._popup.Height = (chkItem ? Items.Count : _numberItemDisplayed) * 18 + (chkItem ? 5 : 0) + (_showCaption ? 24 : 0) + (_useSaveButton ? 36 : -12) + ws + 3;
                            }
                            if (this._popup.Width < this.Width)
                                this._popup.Width = this.Width;
                            this._popup.ResetSize();

                            
                            if (_popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible != _showCaption)
                                _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = _showCaption;
                            
                            _popup.DrawBorder();
                        }
                        _isSetSize = true;
                    }
                    _popup.ShowPopup(this);
                    
                    #endregion
                }
                this.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        public event EventHandler SelectedValueChange;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectionChangeCommitted;
        public event EventHandler SaveClick;

        private bool _pauseSelectedChange = false;
        public bool PauseSelectedChange
        {
            get { return _pauseSelectedChange; }
            set { _pauseSelectedChange = value; }
        }

        public void ResizePopup()
        {
            try
            {
                if (!IsSelectDataAble)
                {
                    if (!_isSetSize && _popup != null)
                    {
                        int ws = 0;
                        if (_popup.ultgData.ActiveRowScrollRegion != null)
                            if (!_popup.ultgData.ActiveRowScrollRegion.Hidden)
                            {
                                ws = 13;
                            }
                        _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = _showCaption;
                        _popup.Size = new Size(this.Width, (_numberItemDisplayed > Items.Count ? Items.Count : _numberItemDisplayed) * 18 + (_showCaption ? 42 : 18) + (_useSaveButton ? 43 : 0) + ws);
                        _popup.ResetSize();
                        _popup.DrawBorder();

                        UltraGridRow r = _popup.ultgData.ActiveRow;
                        if (r != null)
                            _popup.ultgData.ActiveRowScrollRegion.ScrollRowIntoView(r);
                        
                        _isSetSize = true;
                    }                    
                }
                else
                {
                    int width = 0;
                    foreach (string s in curDisplayColumn)
                    {
                        int w = 0;
                        UltraGridColumn col = _popup.ultgData.DisplayLayout.Bands[0].Columns[s];

                        if (col.AutoSizeMode == ColumnAutoSizeMode.None)
                            w = col.Width;
                        else if (col.MaxWidth != 0)
                            w = col.MaxWidth;
                        else
                            w = col.CalculateAutoResizeWidth(PerformAutoSizeType.AllRowsInBand, _showCaption);

                        width += w;
                    }
                    this._popup.Width = width;
                    if (_popup.ultgData.ActiveRowScrollRegion != null)
                    {
                        int ws = 0;
                        if (!_popup.ultgData.ActiveRowScrollRegion.Hidden)
                        {
                            this._popup.Width += 23;
                            ws = 13;
                        }
                        bool chkItem = _numberItemDisplayed > Items.Count;
                        this._popup.Height = (chkItem ? Items.Count : _numberItemDisplayed) * 18 + (chkItem ? 5 : 0) + (_showCaption ? 24 : 0) + (_useSaveButton ? 36 : -12) + ws + 3;
                    }
                    if (this._popup.Width < this.Width)
                        this._popup.Width = this.Width;
                    this._popup.ResetSize();

                    if (_popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible != _showCaption)
                        _popup.ultgData.DisplayLayout.Bands[0].ColHeadersVisible = _showCaption;

                    _popup.DrawBorder();                   
                    _isSetSize = true;
                }
            }
            catch { }
        }

        protected void OnSelectedValueChange(object sender, EventArgs e)
        {
            if (SelectedValueChange != null && !_pauseSelectedChange)
                SelectedValueChange(sender, e);
        }

        protected void OnSelectedIndexChange(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null && !_pauseSelectedChange)
                SelectedIndexChanged(sender, e);
        }

        protected void OnSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SelectionChangeCommitted != null && !_pauseSelectedChange)
                SelectionChangeCommitted(sender, e);
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (SaveClick != null)
            {
                _popup.Close();
                SaveClick(sender, e);
                ShowPopup();
            }
        }

        internal void FireItemAdd(object item)
        {
            if (this.Grid.DataSource is List<object>)
            {
                List<object> items = this.Grid.DataSource as List<object>;
                items.Add(item);
                DataSource = items;
            }
        }

        internal void FireItemAdd(object[] items)
        {
            if (!DesignMode)
                DataSource = items;
        }

        internal event EventHandler ChangeValueMember;
        protected void OnChaneValueMember(object sender, EventArgs e)
        {
            if (ChangeValueMember != null)
                ChangeValueMember(sender, e);
        }

        #region ISupportInitialize Members

        public void BeginInit()
        {
            //CDevItemCollection i = new CDevItemCollection(this);            
        }

        public void EndInit()
        {
            
        }

        #endregion

        #region ISupportInitialize Members

        void System.ComponentModel.ISupportInitialize.BeginInit()
        {
            
        }

        void System.ComponentModel.ISupportInitialize.EndInit()
        {
            
        }

        #endregion
    }

    [ListBindable(true)]
    public class CDevItemCollection : System.Collections.CollectionBase
    {
        ComboBoxEntend _cbo = null;
        System.Collections.ArrayList valueList = new System.Collections.ArrayList();

        public CDevItemCollection(ComboBoxEntend cbo)
        {
            _cbo = cbo;
            _cbo.Grid.PropertyChanged += new Infragistics.Win.PropertyChangedEventHandler(Grid_PropertyChanged);
            _cbo.ChangeValueMember += new EventHandler(_cbo_ChangeValueMember);            
        }

        void _cbo_ChangeValueMember(object sender, EventArgs e)
        {
            try
            {
                this.valueList.Clear();
                if (_cbo.ValueMember == "")
                {
                    foreach (UltraGridRow row in _cbo.Grid.Rows)
                    {
                        this.valueList.Add(row.ListObject);
                    }
                }
                else
                {
                    foreach (UltraGridRow row in _cbo.Grid.Rows)
                    {
                        this.valueList.Add(row.Cells[_cbo.ValueMember].Value);
                    }
                }
            }
            catch { }
        }

        void Grid_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.PropertyIds proid = (Infragistics.Win.UltraWinGrid.PropertyIds)e.ChangeInfo.PropId;
                switch (proid)
                {
                    case PropertyIds.DataSource:
                        _cbo._initDisplayColumn = false;
                        InitListSource();                        
                        break;
                    case PropertyIds.SortComparisonType:
                        InitListSource();
                        break;
                    case PropertyIds.SortedColumns:
                        InitListSource();
                        break;
                    case PropertyIds.SortIndicator:
                        InitListSource();
                        break;
                }
            }
            catch { }
        }

        internal void InitListSource()
        {
            try
            {                
                this.InnerList.Clear();
                this.valueList.Clear();
                if (_cbo.ValueMember == "")
                {
                    foreach (UltraGridRow row in _cbo.Grid.Rows)
                    {
                        this.InnerList.Add(row.ListObject);
                        this.valueList.Add(row.ListObject);
                    }
                }
                else
                {
                    foreach (UltraGridRow row in _cbo.Grid.Rows)
                    {
                        this.InnerList.Add(row.ListObject);
                        this.valueList.Add(row.Cells[_cbo.ValueMember].Value);
                    }
                }
            }
            catch { }
        }

        public int Count
        {
            get
            {
                return this.InnerList.Count;
            }
        }

        public object this[int index]
        {
            get
            {
                if (this.Count != _cbo.Grid.Rows.Count)
                {
                    this.InitListSource();
                }
                return this.InnerList[index];

            }
        }

        public int IndexOf(object value)
        {
            if (this.Count != _cbo.Grid.Rows.Count)
            {
                this.InitListSource();
            }
            return this.InnerList.IndexOf(value);           
        }

        public int IndexOfKey(object value)
        {
            if (this.Count != _cbo.Grid.Rows.Count)
            {
                this.InitListSource();
            }
            return this.valueList.IndexOf(value);
        }                   
    }
}
