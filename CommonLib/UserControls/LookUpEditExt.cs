using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ListControls;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Popup;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.Helpers;
using System.Globalization;
using System.Runtime.InteropServices;
using DevExpress.Utils.Drawing;

namespace CommonLib.UserControls
{
    [DefaultBindingProperty("Text"), ClassInterface(ClassInterfaceType.AutoDispatch), DefaultEvent("SelectedIndexChanged"), DefaultProperty("Items"), ComVisible(true)]
    public class LookUpEditExt : LookUpEdit, ISupportInitialize
    {
        static LookUpEditExt() { RepositoryItemLookUpEditExt.RegisterCustomEdit(); }

        public LookUpEditExt()
            : base()
        {

        }

        ~LookUpEditExt()
        {
            this.Items.Clear();
            if (this.dataSource != null)
                this.dataSource = null;
            Dispose(true);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.Properties.Buttons.Count == 0)
            {
                this.Properties.Buttons.Add(new EditorButton(ButtonPredefines.DropDown));
            }
        }

        bool pausePopup = false;
        private void CustomLookUpEdit_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            pausePopup = true;
            if (Properties.TextEditStyle == TextEditStyles.Standard)
            {
                FItem val = FindItemEx(e.DisplayValue.ToString());
                ClosePopup();
                this.DataAdapter.FilterPrefix = string.Empty;
                if (val.RowKey is DataRow && this.DisplayMember != "")
                {
                    EditValue = val;
                    pausePopup = true;
                    this.Text = ((DataRow)val.RowKey)[this.DisplayMember].ToString();
                    pausePopup = false;
                    this.SelectedIndex = val.Index;
                    AcceptPopupValue(this.EditValue);
                }
                else
                {
                    EditValue = val;
                }
            }
        }

        public override string EditorTypeName { get { return RepositoryItemLookUpEditExt.CustomEditName; } }

        protected override int FindItem(string text, bool partialSearch, int startIndex)
        {
            if (Properties.TextEditStyle != TextEditStyles.Standard)
                return base.FindItem(text, partialSearch, startIndex);
            else
                return -1;
        }

        internal class FItem
        {
            public object RowKey { get; set; }
            public int Index { get; set; }

            public static FItem Empty
            {
                get { return new FItem { Index = -1, RowKey = null }; }
            }
        }

        internal FItem FindItemEx(string text)
        {
            if (text == null || text.Length == 0) return FItem.Empty;
            if (!Properties.CaseSensitiveSearch) text = text.ToLower();
            for (int i = 0; i < Properties.CustomDataAdapter.ItemCount; i++)
            {
                for (int j = 0; j < Properties.Columns.VisibleCount; j++)
                {
                    LookUpColumnInfo col = Properties.Columns[j];
                    if (!col.Visible) continue;
                    string itemText = Properties.GetDisplayText(Properties.CustomDataAdapter.GetValueAtIndex(col.FieldName, i));
                    if (!Properties.CaseSensitiveSearch) itemText = itemText.ToLower();
                    if (text == itemText)
                    {
                        var item = new FItem { RowKey = Properties.CustomDataAdapter.GetRowKey(i) };
                        item.Index = i;
                        return item;
                    }
                }
            }
            return FItem.Empty;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemLookUpEditExt Properties
        {
            get { return base.Properties as RepositoryItemLookUpEditExt; }
        }

        public event EventHandler SelectedValueChange;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectionChangeCommitted;

        protected override void AcceptPopupValue(object val)
        {
            base.AcceptPopupValue(val);
            FireChangedValue();
        }

        protected override void UpdateEditValueOnClose(PopupCloseMode closeMode, bool acceptValue, object newValue, object oldValue)
        {
            if (closeMode == PopupCloseMode.Normal || closeMode == PopupCloseMode.ButtonClick || closeMode == PopupCloseMode.CloseUpKey)
            {
                base.UpdateEditValueOnClose(closeMode, acceptValue, newValue, oldValue);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ComboBoxSelectedIndexDescr")]
        public int SelectedIndex
        {
            get
            {
                return this.ItemIndex;
            }
            set
            {
                try
                {
                    if (this.PopupForm != null)
                    {
                        this.PopupForm.SelectedIndex = value;
                        this.PopupForm.ProcessKeyDown(new KeyEventArgs(Keys.Return));
                    }
                    else
                    {
                        SelectRowIndex(value);
                    }
                }
                catch { }
            }
        }

        private void SelectRowIndex(int value)
        {
            value = Math.Min(this.DataAdapter.ItemCount - 1, Math.Max(value, 0));
            this.EditValue = this.Properties.GetDataSourceValue(this.ValueMember, value);
        }

        bool _autoDisplayText = false;
        public bool AutoDisplayText
        {
            get
            {
                return _autoDisplayText;
            }
            set
            {
                this._autoDisplayText = value;
            }
        }
        private string _columnDisplay = "";
        public string ColumnDisplay
        {
            get { return _columnDisplay; }
            set
            {
                if (_columnDisplay != value)
                {
                    _columnDisplay = value;
                }
            }
        }

        bool _initDisplayColumn = false;

        internal bool InitDisplayColumn
        {
            get { return _initDisplayColumn; }
            set { _initDisplayColumn = value; }
        }

        object dataSource = null;

        [RefreshProperties(RefreshProperties.Repaint), AttributeProvider(typeof(IListSource)), Category("CatData"), Description("ListControlDataSourceDescr"), DefaultValue((string)null)]
        public object DataSource
        {
            set
            {
                dataSource = value;
                if (!this.DesignMode)
                {
                    this.Properties.DataSource = dataSource;
                    this.Properties.PopulateColumns();
                    if (AutoSelectFirstItem && (this.DisplayMember != "" || this.DataSource is object[]))
                    {
                        this.SelectedIndex = 0;
                        FireChangedValue();
                    }
                    _initDisplayColumn = false;
                }
            }
            get
            {
                return dataSource;
            }
        }

        bool _autoSelectFirstItem = true;
        [DefaultValue(true)]
        public bool AutoSelectFirstItem
        {
            get { return _autoSelectFirstItem; }
            set { _autoSelectFirstItem = value; }
        }

        string _displayMember = "";
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMemberDescr"), Category("CatData"), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }
            set
            {
                _displayMember = value;
                this.Properties.DisplayMember = _displayMember;
                if (value != "" && this.Properties.Columns.Count > 0)
                {
                    this.Properties.Columns[value].Visible = true;
                }
            }
        }

        internal string DisplayMember2
        {
            get
            {
                return _displayMember;
            }
            set
            {
                if (_displayMember != null)
                    _displayMember = value;
            }
        }

        string _valueMember = "";
        [Category("CatData"), DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("ListControlValueMemberDescr")]
        public string ValueMember
        {
            get
            {
                return _valueMember;
            }
            set
            {
                _valueMember = value;
                this.Properties.ValueMember = _valueMember;
                if (!IsDesignMode && AutoSelectFirstItem && (this.ValueMember != "" || this.DataSource is object[]))
                {
                    if (this.SelectedIndex < 0)
                    {
                        this.SelectedIndex = 0;
                        FireChangedValue();
                    }
                }
            }
        }

        internal string ValueMember2
        {
            get
            {
                return _valueMember;
            }
            set
            {
                if (_valueMember != null)
                    _valueMember = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ItemCount")]
        public int ItemCount
        {
            get
            {
                return this.DataAdapter.ListSourceRowCount;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("EditValue")]
        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }
            set
            {
                base.EditValue = value;
            }
        }

        void FireChangedValue()
        {
            OnSelectedValueChange(this, new SelectedValueArgs { SelectedValue = this.SelectedValue });
            OnSelectedIndexChange(this, new SelectedIndexArgs { SelectedIndex = this.SelectedIndex });
            OnSelectionChangeCommitted(this, new EventArgs());
        }

        bool _pauseSelectedChange = false;

        protected void OnSelectedValueChange(object sender, SelectedValueArgs e)
        {
            if (SelectedValueChange != null && !_pauseSelectedChange)
                SelectedValueChange(sender, e);
        }

        protected void OnSelectedIndexChange(object sender, SelectedIndexArgs e)
        {
            if (SelectedIndexChanged != null && !_pauseSelectedChange)
                SelectedIndexChanged(sender, e);
        }

        protected void OnSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SelectionChangeCommitted != null && !_pauseSelectedChange)
                SelectionChangeCommitted(sender, e);
        }

        [Browsable(false)]
        public bool FormattingEnabled
        {
            get
            {
                return FormatString == "";
            }
            set
            {

            }
        }

        [Browsable(false), DefaultValue((string)null), Bindable(true), Category("CatData"), Description("ListControlSelectedValueDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PauseSelectedChange
        {
            get
            {
                return _pauseSelectedChange;
            }
            set
            {
                _pauseSelectedChange = value;
            }
        }

        string _formatString = "";

        public string FormatString
        {
            get { return _formatString; }
            set { _formatString = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("DisplayText")]
        public string DisplayText
        {
            get { return Text; }
            set { Text = value; }
        }

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
                        if (_dropDownStyle == ComboBoxStyle.DropDownList)
                        {
                            this.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                        else if (_dropDownStyle == ComboBoxStyle.DropDown)
                        {
                            this.Properties.TextEditStyle = TextEditStyles.Standard;
                        }
                        else
                        {
                            this.Properties.TextEditStyle = TextEditStyles.HideTextEditor;
                        }
                    }
                }
                catch (Exception ex) { System.Console.WriteLine(ex.Message); }
            }
        }

        public int DropDownWidth
        {
            get
            {
                if (this.Properties != null)
                    return this.Properties.PopupWidth;
                else
                    return 0;
            }
            set
            {
                if (this.Properties != null)
                    this.Properties.PopupWidth = value;
            }
        }

        public int NumberItemDisplayed
        {
            get
            {
                if (this.Properties != null)
                    return this.Properties.DropDownRows;
                else
                    return 0;
            }
            set
            {
                if (this.Properties != null)
                    this.Properties.DropDownRows = value;
            }
        }
        string _oldText = "";

        [Browsable(false), DefaultValue((string)null), Bindable(true), Category("CatData"), Description("ListControlSelectedValueDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OldText
        {
            get
            {
                if (this.Properties != null)
                {
                    int index = this.Properties.GetDataSourceRowIndex(this.ValueMember, this.OldEditValue);
                    if (index < 0) return "";
                    object val = this.Properties.GetDataSourceValue(this.DisplayMember, index);
                    if (val != null)
                        return val.ToString();
                    else
                        return "";
                }
                return "";
            }
            set
            {
                _oldText = value;
            }
        }

        LookupEditItemCollections _items = null;

        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public LookupEditItemCollections Items
        {
            get
            {

                if (this._items == null)
                {
                    this._items = new LookupEditItemCollections(this);
                }
                return _items;
            }
        }

        internal PopupLookUpEditForm GetPopupEdit()
        {
            return this.PopupForm;
        }

        public bool ShowCaption
        {
            get
            {
                return this.Properties.ShowHeader;
            }
            set
            {
                this.Properties.ShowHeader = value;
            }
        }

        public bool UseSaveButton
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Value")]
        public object Value
        {
            get { return this.SelectedValue; }
        }

        [Browsable(false), DefaultValue((string)null), Bindable(true), Category("CatData"), Description("ListControlSelectedValueDescr"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                if (this.SelectedIndex < 0)
                    return null;
                return this.EditValue;
            }
            set
            {
                object oldVal = this.EditValue;
                this.EditValue = value;
                if (this.EditValue != oldVal)
                    FireChangedValue();
            }
        }

        [Browsable(false)]
        public Size SizeOfPopup
        {
            get
            {
                if (this.PopupForm != null)
                    return this.PopupForm.Size;
                else
                    return new Size();
            }
            set
            {
                if (this.PopupForm != null)
                    this.PopupForm.Size = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ComboBoxSelectedItemDescr"), Browsable(false), Bindable(true)]
        public object SelectedItem
        {
            get
            {
                if (this.SelectedIndex < 0)
                    return null;
                else
                    return this.Items[this.SelectedIndex];
            }
            set
            {
                if (this.DataAdapter != null)
                {
                    this.DataAdapter.FilterPrefix = string.Empty;
                    object obj0 = DataAdapter.GetRow(0);
                    if (obj0 is DataRowView && value is DataRow)
                    {
                        for (int i = 0; i < DataAdapter.VisibleListSourceRowCount; i++)
                        {
                            DataRowView obj = DataAdapter.GetRow(i) as DataRowView;
                            if (obj.Row == value)
                            {
                                this.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        int indexRow = this.DataAdapter.FindRowByRowValue(value);
                        if (indexRow >= 0)
                            this.SelectedIndex = indexRow;
                    }
                }
            }
        }

        internal LookUpListDataAdapterExt DataAdapter
        {
            get
            {
                return this.Properties.CustomDataAdapter;
            }
        }

        internal void RegisDisplayColumns()
        {
            if (!_initDisplayColumn)
            {
                List<string> cols = new List<string>();
                if (this.ColumnDisplay != "")
                {
                    cols.AddRange(this.ColumnDisplay.Split(';', ','));
                }
                else if (this.DisplayMember != "" && this.Properties.Columns.Count > 0)
                    cols.Add(this.DisplayMember);

                if (cols.Count == 0)
                {
                    if (this.Properties.Columns.Count > 0)
                        this.Properties.Columns[0].Visible = true;
                }
                else
                {
                    var eCols = from c1 in this.Properties.Columns.Cast<LookUpColumnInfo>()
                                join c2 in cols on c1.FieldName equals c2 into g_Cols
                                select new { Column = c1, Visible = g_Cols.Any() };
                    foreach (var col in eCols)
                    {
                        col.Column.Visible = col.Visible;
                    }
                }
            }
        }

        protected override void DoShowPopup()
        {
            if (!pausePopup)
            {
                RegisDisplayColumns();
                base.DoShowPopup();
                _isActive = true;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _isActive = false;
        }

        bool _isActive = false;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            _isActive = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!_isActive && this.Properties.TextEditStyle == TextEditStyles.Standard)
            {
                _isActive = true;
                this.SelectAll();
                this.ShowPopup();
            }
        }

        protected override void OnEditorKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.DataAdapter.VisibleCount > 0)
                {
                    if (this.PopupForm != null)
                    {
                        if (this.PopupForm.SelectedIndex == -1)
                            this.PopupForm.SelectedIndex = 0;
                        this.PopupForm.ProcessKeyDown(new KeyEventArgs(Keys.Return));
                    }
                    else
                    {
                        this.DataAdapter.Selection.Clear();
                        this.DataAdapter.Selection.SetSelected(0, true);
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                base.OnEditorKeyDown(e);
                FireChangedValue();
                return;
            }
            else if (e.KeyCode == Keys.Down)
            {
                base.OnEditorKeyDown(e);
                FireChangedValue();
                return;
            }

            base.OnEditorKeyDown(e);
        }

        public override void CancelPopup()
        {
            base.CancelPopup();
            if (this.PopupForm != null)
            {
                this.Text = this.ViewInfo.DisplayText;
                this.PopupForm.ClosePopup();
            }
        }

        public virtual void SelectValueByKey(object value, string Key)
        {
            if (this.DataAdapter == null) return;
            int index = this.DataAdapter.FindRowByValue(Key, value);
            if (index >= 0)
                this.SelectedIndex = index;
        }

        public object GetValueByKey(string Key)
        {
            if (this.SelectedIndex != -1)
                return this.Properties.GetDataSourceValue(Key, this.SelectedIndex);
            else
                return null;
        }

        #region ISupportInitialize Members

        public void BeginInit()
        {

        }

        public void EndInit()
        {

        }

        #endregion
    }

    [ListBindable(false)]
    public class LookupEditItemCollections : CollectionBase
    {
        LookUpEditExt _cbo = null;

        public LookupEditItemCollections(LookUpEditExt lookup)
        {
            _cbo = lookup;
        }

        protected override void OnClear()
        {
            base.OnClear();
            this.Clear();
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (this.Count == 0)
                this.Clear();
        }

        public virtual int Add(object item)
        {
            int index = this.InnerList.Add(item);

            if (!_cbo.IsDesignMode)
            {
                if (_cbo.DataSource == null || _cbo.DataSource != this.InnerList)
                {
                    this._cbo.DataSource = this.InnerList;
                }
            }
            return index;
        }

        public virtual void AddRange(object[] itemArray)
        {
            this.InnerList.AddRange(itemArray);
            if (!_cbo.IsDesignMode)
            {
                if (_cbo.DataSource == null || _cbo.DataSource != this.InnerList)
                {
                    this._cbo.DataSource = this.InnerList;
                }
            }
        }

        object GetItemInRange(int index)
        {
            return this[index];
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new int Count
        {
            get
            {
                return this._cbo.DataAdapter.ListSourceRowCount;
            }
        }

        public int IndexOf(object value)
        {
            if (this._cbo.DataAdapter.ListSource == null) return -1;
            return this._cbo.DataAdapter.ListSource.IndexOf(value);
        }

        public int IndexOfKey(object value)
        {
            if (this._cbo.DataAdapter.ListSource == null) return -1;
            return this._cbo.DataAdapter.ListSource.IndexOf(value);
        }

        public object[] ToArray()
        {
            object[] arr = new object[Count];
            for (int i = 0; i < Count; i++)
            {
                arr[i] = this[i];
            }
            return arr;
        }

        #region IList Members

        public new void Clear()
        {
            this._cbo.DataSource = null;
            this._cbo.Properties.DataSource = null;
            this._cbo.Properties.Columns.Clear();
            if (this._cbo.DataAdapter.ListSource == null) return;
            this._cbo.DataAdapter.ListSource.Clear();
            this._cbo.DataAdapter.RefreshData();
        }

        public bool Contains(object value)
        {
            if (this._cbo.DataAdapter.ListSource == null) return false;
            return this._cbo.DataAdapter.ListSource.Contains(value);
        }

        public void Insert(int index, object value)
        {
            if (this._cbo.DataAdapter.ListSource == null) return;
            this._cbo.DataAdapter.ListSource.Insert(index, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsFixedSize
        {
            get
            {
                if (this._cbo.DataAdapter.ListSource == null) return false;
                return this._cbo.DataAdapter.ListSource.IsFixedSize;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsReadOnly
        {
            get
            {
                if (this._cbo.DataAdapter.ListSource == null) return false;
                return this._cbo.DataAdapter.ListSource.IsReadOnly;
            }
        }

        public void Remove(object value)
        {
            if (this._cbo.DataAdapter.ListSource == null) return;
            this._cbo.DataAdapter.ListSource.Remove(value);
        }

        public new void RemoveAt(int index)
        {
            if (this._cbo.DataAdapter.ListSource == null) return;
            this._cbo.DataAdapter.ListSource.RemoveAt(index);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object this[int index]
        {
            get
            {
                if (this._cbo.DataAdapter.ListSource == null) return null;
                return this._cbo.DataAdapter.ListSource[index];
            }
            set
            {
                if (this._cbo.DataAdapter.ListSource == null) return;
                this._cbo.DataAdapter.ListSource[index] = value;
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            if (this._cbo.DataAdapter.ListSource == null) return;
            this._cbo.DataAdapter.ListSource.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get
            {
                if (this._cbo.DataAdapter.ListSource == null) return false;
                return this._cbo.DataAdapter.ListSource.IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                if (this._cbo.DataAdapter.ListSource == null) return null;
                return this._cbo.DataAdapter.ListSource.SyncRoot;
            }
        }

        #endregion
    }

    public class RepositoryItemLookUpEditExt : RepositoryItemLookUpEdit
    {
        public const string CustomEditName = "RepositoryItemLookUpEditExt";

        public override string EditorTypeName { get { return CustomEditName; } }

        static RepositoryItemLookUpEditExt() { RegisterCustomEdit(); }

        public RepositoryItemLookUpEditExt()
        {
            AllowNullInput = DefaultBoolean.True;
            NullText = "";
            ShowHeader = false;
            ShowFooter = false;
            AutoHeight = false;
            PopupSizeable = false;
        }

        public override string DisplayMember
        {
            get
            {
                return base.DisplayMember;
            }
            set
            {
                base.DisplayMember = value;
                LookUpEditExt editor = this.OwnerEdit as LookUpEditExt;
                if (editor != null)
                {
                    editor.DisplayMember2 = value;
                }
            }
        }

        public override string ValueMember
        {
            get
            {
                return base.ValueMember;
            }
            set
            {
                base.ValueMember = value;
                LookUpEditExt editor = this.OwnerEdit as LookUpEditExt;
                if (editor != null)
                {
                    editor.ValueMember2 = value;
                }
            }
        }

        public override void CreateDefaultButton()
        {
            base.CreateDefaultButton();
            if (this.Buttons.Count > 0)
                if (this.Buttons[0].Kind != ButtonPredefines.DropDown)
                    this.Buttons[0].Kind = ButtonPredefines.DropDown;
        }

        public override int MeasureColumn(Graphics g, LookUpColumnInfo column)
        {
            int num = 0;
            int itemCount = this.DataAdapter.ItemCount;
            if (this.BestFitRowCount > 0)
            {
                itemCount = Math.Min(itemCount, this.BestFitRowCount);
            }
            for (int i = 0; i < itemCount; i++)
            {
                string cellString = this.DataAdapter.GetCellString(column, i);
                num = Math.Max(num, this.Appearance.CalcTextSize(g, cellString, 0).ToSize().Width + 6);
            }
            num = Math.Max(num, this.Appearance.CalcTextSize(g, column.Caption, 0).ToSize().Width + 10);
            if (this.Columns.VisibleIndexOf(column) == this.AutoSearchColumnIndex)
            {
                num += 13;
            }
            return num;
        }

        public override int BestFit()
        {
            if (this.IsDisposed) return 0;
            if (this.OwnerEdit.IsDisposed) return 0;
            if (this.Columns.Count == 0)
            {
                return 0;
            }
            int num = 0;
            try
            {
                GraphicsInfo info = new GraphicsInfo();
                Graphics g = info.AddGraphics(null);
                try
                {

                    List<string> cols = new List<string>();
                    if (((LookUpEditExt)this.OwnerEdit).ColumnDisplay != "")
                    {
                        cols.AddRange(((LookUpEditExt)this.OwnerEdit).ColumnDisplay.Split(';', ','));
                    }
                    else if (this.DisplayMember != "" && this.Columns.Count > 0)
                        cols.Add(this.DisplayMember);
                    if (cols.Count > 0)
                    {
                        var eCols = from c1 in this.Columns.Cast<LookUpColumnInfo>()
                                    join c2 in cols on c1.FieldName equals c2 into g_Cols
                                    select new { Column = c1, Visible = g_Cols.Any() };
                        foreach (var col in eCols.Where(c => c.Visible))
                        {
                            int w = this.MeasureColumn(g, col.Column);
                            col.Column.Width = w;
                            if (this.PropertyStore.Contains(col.Column.FieldName))
                            {
                                LookUpColumnPopupSaveInfo info3 = (LookUpColumnPopupSaveInfo)this.PropertyStore[col.Column.FieldName];
                                info3.Width = col.Column.Width;
                            }
                            num += w;
                        }
                    }
                    else
                    {

                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            LookUpColumnInfo column = this.Columns[i];
                            if (column.Visible)
                            {
                                column.Width = this.MeasureColumn(g, column);
                                if (this.PropertyStore.Contains(column.FieldName))
                                {
                                    LookUpColumnPopupSaveInfo info3 = (LookUpColumnPopupSaveInfo)this.PropertyStore[column.FieldName];
                                    info3.Width = column.Width;
                                }
                                num += column.Width;
                            }
                        }
                    }
                }
                finally
                {
                    info.ReleaseGraphics();
                }
                return (num + SystemInformation.VerticalScrollBarWidth);
            }
            catch { }
            return -1;
        }

        public int BestFit(bool ResizePopup)
        {
            int w = this.BestFit();
            if (ResizePopup)
                this.PopupWidth = w;
            return w;
        }

        internal LookUpListDataAdapterExt CustomDataAdapter
        {
            get { return (LookUpListDataAdapterExt)DataAdapter; }
        }

        public static void RegisterCustomEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
              typeof(LookUpEditExt), typeof(RepositoryItemLookUpEditExt),
              typeof(LookUpEditViewInfo), new ButtonEditPainter(), true));
        }

        protected override LookUpListDataAdapter CreateDataAdapter()
        {
            return new LookUpListDataAdapterExt(this);
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemLookUpEditExt source = item as RepositoryItemLookUpEditExt;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        public override void PopulateColumns()
        {
            base.PopulateColumns();
            LookUpEditExt editor = this.OwnerEdit as LookUpEditExt;
            if (editor != null)
            {
                editor.InitDisplayColumn = false;
                editor.RegisDisplayColumns();
            }
        }

        protected override void ActivateDataSource(ActivationMode mode)
        {
            BindingContext context = (this.OwnerEdit == null) ? null : this.OwnerEdit.BindingContext;
            if (MasterDetailHelper.IsDataSourceReady(context, base.DataSource, string.Empty))
            {
                bool flag = (mode == ActivationMode.BindingContext) && this.IsDesignMode;
                if (flag)
                {
                    this.BeginUpdate();
                }
                try
                {
                    this.DataAdapter.SetDataSource(MasterDetailHelper.GetDataSource(context, base.DataSource, string.Empty), this.DisplayMember, this.ValueMember);
                }
                finally
                {
                    if (flag)
                    {
                        this.EndUpdate();
                    }
                }
            }
        }
    }

    public class SelectedIndexArgs : EventArgs
    {
        public int SelectedIndex { get; set; }
        public SelectedIndexArgs()
        {
            this.SelectedIndex = -1;
        }
    }

    public class SelectedValueArgs : EventArgs
    {
        public object SelectedValue { get; set; }
        public SelectedValueArgs()
        {
            this.SelectedValue = null;
        }
    }

    internal class LookUpListDataAdapterExt : LookUpListDataAdapter
    {
        public LookUpListDataAdapterExt(RepositoryItemLookUpEdit item)
            : base(item)
        {

        }

        protected override string CreateFilterExpression()
        {
            if (Item.TextEditStyle == TextEditStyles.Standard)
            {
                string likeClause = DevExpress.Data.Filtering.Helpers.LikeData.CreateStartsWithPattern(FilterPrefix);
                BinaryOperator[] bops = new BinaryOperator[Item.Columns.Count];
                for (int i = 0; i < bops.Length; i++)
                    bops[i] = new BinaryOperator(Item.Columns[i].FieldName, likeClause, BinaryOperatorType.Like);
                return new GroupOperator(GroupOperatorType.Or, bops).ToString();
            }
            else
                return base.CreateFilterExpression();
        }

    }
}
