using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace CommonLib
{
    public partial class frmQuickType : frmBorderLess
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool GetCaretPos(out Point lpPoint);

        #region Variables
        DataView _data = null;
        Control _controlOnKey = null;
        string[][] _header = null;
        string[] _keyFilter = null;
        string[] _keyForGrid = null;
        string[][] _keyReturnGrid = null;
        CommonLib.Buttons.pscButton btnQuickType = null;

        bool _loadedData = false, _showQuickType = true;

        string _key = "", _filterEx = string.Empty, _keyReturnToTag = "";

        #endregion

        #region Properties
        public string KeyReturnToTag
        {
            get { return _keyReturnToTag; }
            set { _keyReturnToTag = value; }
        }

        public string FilterEx
        {
            get { return _filterEx; }
            set { _filterEx = value; }
        }

        public bool AllowEdit
        {
            get { return grvData.OptionsBehavior.Editable; }
            set { grvData.OptionsBehavior.Editable = value; }
        }

        public bool ShowQuickType
        {
            get { return _showQuickType; }
            set { _showQuickType = value; }
        }
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public bool UseSaveButton
        {
            get { return btnSave.Visible; }
            set { btnSave.Visible = value; }
        }
        public DataView Data
        {
            get { return _data; }
            set { _data = value; InitDataSource(); }
        }
        public Control ControlOnKey
        {
            get { return _controlOnKey; }
            set { _controlOnKey = value; }
        }
        public string[][] Header
        {
            get { return _header; }
            set { _header = value; }
        }
        public string[] KeyForGrid
        {
            get { return _keyForGrid; }
            set { _keyForGrid = value; }
        }
        public string[] KeyFilter
        {
            get { return _keyFilter; }
            set { _keyFilter = value; }
        }
        /// <summary>
        /// Use only when ControlOnKey is Grid and KeyReturnGrid is not null
        /// </summary>
        public string[][] KeyReturnGrid
        {
            get { return _keyReturnGrid; }
            set { _keyReturnGrid = value; }
        }

        public object SelectedValue
        {
            get
            {
                object val = null;
                try
                {
                    if (_controlOnKey is GridControl || _controlOnKey is DataGridView)
                    {
                        DataRowView drv = grdData.ActiveRow.RowKey as DataRowView;
                        if (KeyReturnGrid == null)
                        {
                            val = drv.Row[Key];
                        }
                        else
                        {
                            if (KeyReturnGrid.Length > 0)
                            {
                                string s = "";
                                if (_controlOnKey is GridControl && KeyForGrid.Length > 0)
                                {
                                    s = ((GridView)((GridControl)_controlOnKey).FocusedView).FocusedColumn.FieldName;
                                }
                                else if (_controlOnKey is DataGridView && KeyForGrid.Length > 0)
                                {
                                    s = ((DataGridView)_controlOnKey).Columns[((DataGridView)_controlOnKey).CurrentCell.ColumnIndex].Name;
                                }
                                string[] arr = null;
                                string stmp = null;
                                foreach (string[] arr1 in KeyReturnGrid)
                                {
                                    stmp = Array.Find(arr1, delegate(string sFind) { if (s == sFind) return true; else return false; });
                                    if (stmp != null)
                                    {
                                        arr = arr1;
                                        break;
                                    }
                                }

                                string f = null;
                                foreach (string f1 in arr)
                                    if (f1 != stmp) f = f1;
                                if (f != null)
                                {
                                    val = drv.Row[f];
                                }
                            }
                            else
                                val = drv.Row[Key];
                        }
                    }
                    else
                    {
                        DataRowView drv = grdData.ActiveRow.RowKey as DataRowView;
                        val = drv.Row[Key];
                    }
                }
                catch { }
                return val;
            }
        }
        #endregion

        #region Init
        public frmQuickType()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadControl();
            base.OnLoad(e);
        }
        #endregion

        #region Event

        private void grdData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (grdData.CheckIsOnHeader(e.Location)) return;
                SetValueToControl();
                this.Visible = false;
            }
            catch { }
        }

        bool IsHideOnGridColumn()
        {
            try
            {
                if (_controlOnKey is GridControl || _controlOnKey is DataGridView)
                {
                    if (KeyForGrid == null) return true;
                    if (_controlOnKey is GridControl && KeyForGrid.Length > 0)
                    {
                        string s = ((GridView)((GridControl)_controlOnKey).FocusedView).FocusedColumn.FieldName;
                        string f = Array.Find(KeyForGrid, delegate(string sFind) { if (s == sFind) return true; else return false; });
                        if (f == null) return true;
                    }
                    else if (_controlOnKey is DataGridView && KeyForGrid.Length > 0)
                    {
                        string s = ((DataGridView)_controlOnKey).Columns[((DataGridView)_controlOnKey).CurrentCell.ColumnIndex].Name;
                        string f = Array.Find(KeyForGrid, delegate(string sFind) { if (s == sFind) return true; else return false; });
                        if (f == null) return true;
                    }
                }
            }
            catch { }
            return false;
        }

        void _controlOnKey_KeyUp(object sender, KeyEventArgs e)
        {
            Application.UseWaitCursor = true;

            try
            {
                if (!_showQuickType) { Application.UseWaitCursor = false; return; }
                if (e.KeyCode == Keys.None) { Application.UseWaitCursor = false; return; }
                if (IsHideOnGridColumn()) { Application.UseWaitCursor = false; return; }
                if (e.KeyData == Keys.Enter)
                {
                    SetValueToControl();
                    this.Visible = false;
                }
                else if (e.KeyData == Keys.Up)
                {
                    if (!this.Visible)
                        this.Visible = true;
                    _isShowMessage = false;
                    HitLocation();
                    this.TopMost = this.Visible;
                    grvData.MovePrev();
                    if (sender is TextEdit && _controlOnKey is GridControl)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyData == Keys.Down)
                {
                    if (!this.Visible)
                        this.Visible = true;
                    HitLocation();
                    this.TopMost = this.Visible;
                    grvData.MoveNext();
                    if (sender is TextEdit && _controlOnKey is GridControl)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyData != Keys.Escape)
                {
                    if (!this.Visible)
                    {
                        this.Visible = true;
                        HitLocation();
                        this.TopMost = this.Visible;
                    }
                    string filter = string.Empty;
                    string text = GetTextOnControl(sender);
                    text = text.Trim();
                    foreach (string s in _keyFilter)
                    {
                        if (Data.Table.Columns[s].DataType == typeof(string) || Data.Table.Columns[s].DataType == typeof(DateTime))
                            filter += (filter == string.Empty ? "" : " or ") + s + " like '%" + text + "%'";
                        else
                            filter += (filter == string.Empty ? "" : " or ") + s + "=" + text;
                    }
                    filter = _filterEx == string.Empty ? (filter + "") : ("(" + filter + ") and (" + _filterEx + ")");
                    filter = filter == "" ? string.Empty : filter;
                    Data.RowFilter = filter;
                    Console.WriteLine(filter);
                }
                if (sender is TextEdit)
                    ((TextEdit)sender).Focus();
                else if (sender is GridView)
                    ((GridView)sender).Focus();
                else
                    ((Control)sender).Focus();
            }
            catch { }
            Application.UseWaitCursor = false;
        }
        #endregion

        #region Private function

        private string GetTextOnControl(object sender)
        {
            if (_controlOnKey == null) return string.Empty;
            if (_controlOnKey is TextBox)
                return ((TextBox)_controlOnKey).Text;
            else if (_controlOnKey is TextEdit)
                return ((TextEdit)_controlOnKey).Text;
            else if (_controlOnKey is DataGridView)
                return ((DataGridView)_controlOnKey).CurrentCell.Value.ToString();
            else if (_controlOnKey is GridControl)
            {
                if (((GridControl)_controlOnKey).FocusedView.ActiveEditor != null)
                    return ((GridControl)_controlOnKey).FocusedView.ActiveEditor.Text;
                else
                {
                    GridView view = ((GridControl)_controlOnKey).FocusedView as GridView;
                    if (view == null) return "";
                    return view.GetFocusedValue().ToString();
                }
            }
            else if (_controlOnKey is BaseEdit)
                return ((BaseEdit)_controlOnKey).Text;
            else
                return ((Control)_controlOnKey).Text;
        }

        private void InitKeyUpControl()
        {
            if (_controlOnKey != null)
            {
                if (_controlOnKey is GridControl)
                {
                    foreach (GridView v in ((GridControl)_controlOnKey).ViewCollection)
                    {
                        v.ShownEditor += new EventHandler(v_ShownEditor);
                        v.HiddenEditor += new EventHandler(v_HiddenEditor);
                        v.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(v_FocusedRowChanged);
                    }
                }
                else
                {
                    ((Control)_controlOnKey).KeyUp += new KeyEventHandler(_controlOnKey_KeyUp);
                    ((Control)_controlOnKey).Leave += new EventHandler(frmQuickType_Leave);
                    ((Control)_controlOnKey).GotFocus += new EventHandler(control_GotFocus);
                }
            }
        }

        void control_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if (_controlOnKey == null) return;
                if (IsHideOnGridColumn()) { if (btnQuickType != null) btnQuickType.Visible = false; return; }
                if (btnQuickType.Parent == null)
                {
                    Control f = _controlOnKey.Parent;
                    f.Controls.Add(btnQuickType);
                    Form frm = _controlOnKey.FindForm();
                    ShortKeyReg.RegisterHotKey(frm, btnQuickType, Keys.Control | Keys.Shift | Keys.H);
                }
                Rectangle pctrl = new Rectangle();
                if (_controlOnKey is TextBox)
                {
                    pctrl = ((Control)_controlOnKey).Bounds;
                }
                else if (_controlOnKey is TextEdit)
                {
                    pctrl = ((Control)_controlOnKey).Bounds;
                }
                else if (_controlOnKey is DataGridView)
                {
                    pctrl = ((DataGridView)_controlOnKey).CurrentCell.ContentBounds;
                }
                else if (_controlOnKey is GridControl)
                {
                    pctrl = ((GridControl)_controlOnKey).FocusedView.ActiveEditor.Bounds;
                }
                else if (_controlOnKey is BaseEdit)
                {
                    pctrl = ((Control)_controlOnKey).Bounds;
                }
                else
                {
                    pctrl = ((Control)_controlOnKey).Bounds;
                }

                Point pt = new Point(pctrl.Right, pctrl.Top + 2);

                btnQuickType.Visible = true;
                btnQuickType.BringToFront();
                btnQuickType.Location = pt;
                this.timer1.Enabled = btnQuickType.Visible;
            }
            catch { }
        }

        BaseEdit objEdit = null;
        void v_HiddenEditor(object sender, EventArgs e)
        {
            if (objEdit != null)
            {
                objEdit.CausesValidation = false;
                objEdit.KeyDown -= _controlOnKey_KeyUp;
            }
            if (btnQuickType != null) btnQuickType.Visible = false;
        }

        void v_ShownEditor(object sender, EventArgs e)
        {
            if (sender is GridView)
            {
                objEdit = ((GridView)sender).ActiveEditor;
                objEdit.CausesValidation = true;
                objEdit.KeyDown += new KeyEventHandler(_controlOnKey_KeyUp);
                if (btnQuickType != null)
                {
                    if (IsHideOnGridColumn()) { btnQuickType.Visible = false; return; }

                    Rectangle pctrl = objEdit.Bounds;

                    if (btnQuickType.Parent == null)
                    {
                        Control f = _controlOnKey.Parent;
                        f.Controls.Add(btnQuickType);
                        Form frm = _controlOnKey.FindForm();
                        ShortKeyReg.RegisterHotKey(frm, btnQuickType, Keys.Control | Keys.Shift | Keys.H);
                    }

                    Point pt = new Point(pctrl.Right + 2, pctrl.Top - 5 + ((CommonLib.UserControls.XtraGridExtend)((GridView)sender).GridControl).ColumnInfos[0].Bounds.Height);
                    btnQuickType.Visible = true;
                    btnQuickType.BringToFront();
                    btnQuickType.Location = pt;
                    this.timer1.Enabled = btnQuickType.Visible;
                }
            }
        }

        void v_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ContainsFocus) return;
            this.Visible = false;
        }

        void frmQuickType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.ContainsFocus || btnQuickType.Capture) return;

                this.Visible = false;
                if (btnQuickType != null) btnQuickType.Visible = false;
            }
            catch { }
        }

        private void HitLocation()
        {
            try
            {
                if (_controlOnKey == null) return;
                Rectangle p = new Rectangle();
                Rectangle pctrl = new Rectangle();
                if (_controlOnKey is TextBox)
                {
                    pctrl = ((Control)_controlOnKey).DisplayRectangle;
                    p = ((Control)_controlOnKey).RectangleToScreen(pctrl);
                }
                else if (_controlOnKey is TextEdit)
                {
                    pctrl = ((Control)_controlOnKey).DisplayRectangle;
                    p = ((Control)_controlOnKey).RectangleToScreen(pctrl);
                }
                else if (_controlOnKey is DataGridView)
                {
                    pctrl = ((DataGridView)_controlOnKey).CurrentCell.ContentBounds;
                    p = ((DataGridView)_controlOnKey).CurrentCell.ContentBounds;
                }
                else if (_controlOnKey is GridControl)
                {
                    pctrl = ((GridControl)_controlOnKey).FocusedView.ActiveEditor.DisplayRectangle;
                    p = ((GridControl)_controlOnKey).FocusedView.ActiveEditor.RectangleToScreen(pctrl);
                }
                else if (_controlOnKey is BaseEdit)
                {
                    pctrl = ((Control)_controlOnKey).DisplayRectangle;
                    p = ((Control)_controlOnKey).RectangleToScreen(pctrl);
                }
                else
                {
                    pctrl = ((Control)_controlOnKey).DisplayRectangle;
                    p = ((Control)_controlOnKey).RectangleToScreen(pctrl);
                }
                Point pt = new Point(p.Left, p.Bottom + 3);
                Rectangle ret = Screen.PrimaryScreen.WorkingArea;
                if (pt.X + this.Width > ret.Right)
                    pt.X = pt.X - ((pt.X + this.Width) - ret.Right);
                if (pt.Y + this.Height > ret.Bottom)
                    pt.Y = pt.Y - this.Height - pctrl.Height - 6;
                this.Location = pt;
            }
            catch { }
        }

        private void SetValueToControl()
        {
            try
            {
                if (_controlOnKey == null) return;
                if (_controlOnKey is TextBox)
                {
                    ((TextBox)_controlOnKey).Text = SelectedValue.ToString();
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((TextBox)_controlOnKey).Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
                else if (_controlOnKey is TextEdit)
                {
                    ((TextEdit)_controlOnKey).Text = SelectedValue.ToString();
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((TextEdit)_controlOnKey).Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
                else if (_controlOnKey is DataGridView)
                {
                    ((DataGridView)_controlOnKey).CurrentCell.Value = SelectedValue;
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((DataGridView)_controlOnKey).CurrentCell.Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
                else if (_controlOnKey is GridControl)
                {
                    ((GridControl)_controlOnKey).FocusedView.ActiveEditor.Text = SelectedValue.ToString();
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((GridControl)_controlOnKey).FocusedView.ActiveEditor.Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
                else if (_controlOnKey is BaseEdit)
                {
                    ((BaseEdit)_controlOnKey).Text = SelectedValue.ToString();
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((BaseEdit)_controlOnKey).Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
                else
                {
                    ((Control)_controlOnKey).Text = SelectedValue.ToString();
                    if (KeyReturnToTag != null && KeyReturnToTag != "")
                        ((Control)_controlOnKey).Tag = GetSelectedValueByKey(KeyReturnToTag);
                }
            }
            catch { }
        }
        #endregion

        #region Public function
        public object GetSelectedValueByKey(string ColumnKey)
        {
            object val = null;
            try
            {
                DataRowView drv = grdData.ActiveRow.RowKey as DataRowView;
                val = drv.Row[ColumnKey];
            }
            catch { }
            return val;
        }

        public CommonLib.UserControls.XtraGridExtend GetGird()
        {
            return grdData;
        }

        private void InitDataSource()
        {
            try
            {
                grdData.DataSource = Data;

                foreach (GridColumn col in grdData.Columns)
                {
                    if (_header != null)
                    {
                        string[] h = Array.Find(_header, delegate(string[] arr)
                        {
                            if (arr == null) return false;
                            else if (arr.Length != 2) return false;
                            else if (arr[0] == col.FieldName) return true;
                            else return false;
                        });
                        if (h != null)
                        {
                            if (h.Length != 2) col.Visible = false;
                            else if (h[0] == col.FieldName) { col.Caption = h[1]; col.Visible = true; }
                            else col.Visible = false;
                        }
                        else
                            col.Visible = false;
                    }
                }
            }
            catch { }
        }

        public void LoadControl()
        {
            if (_controlOnKey != null && !_loadedData && _data != null)
            {
                InitDataSource();
                InitKeyUpControl();
                #region Draw form check box
                btnQuickType = new CommonLib.Buttons.pscButton();
                btnQuickType.MaximumSize = new Size(24, 16);
                btnQuickType.ClientSize = new System.Drawing.Size(24, 16);
                btnQuickType.Name = "btnQuickType";
                btnQuickType.Image = Properties.Resources.keypress;
                btnQuickType.ToolTip = "Tắt tìm nhanh (Ctrl+Shift+H)";
                btnQuickType.Click += new EventHandler(btnQuickType_Click);
                btnQuickType.Visible = false;
                btnQuickType.Text = "";
                btnQuickType.ImageLocation = ImageLocation.MiddleCenter;
                #endregion
                _loadedData = true;
            }
        }

        void btnQuickType_Click(object sender, EventArgs e)
        {
            try
            {
                _showQuickType = !_showQuickType;
                btnQuickType.ToolTip = _showQuickType ? "Tắt tìm nhanh (Ctrl+Shift+H)" : "Bật tìm nhanh (Ctrl+Shift+H)";
                btnQuickType.Image = _showQuickType ? Properties.Resources.keypress : Properties.Resources.keynotpress;
                _controlOnKey.Focus();
                this.Visible = _showQuickType;
            }
            catch { }
        }

        /// <summary> 
        /// Init popup 
        /// </summary>
        /// <param name="control">apply QuickType</param>
        /// <param name="data">DataView to search</param>
        /// <param name="header">Caption column and Visible column</param>
        /// <param name="keyFilter">Keyword for searching</param>
        /// <param name="KeyForEnter">Column return value</param>
        /// <param name="KeyForGrid">KeyForGrid not null and length=0 then use all column</param>
        /// <returns>Return inited Form</returns>        
        public static frmQuickType InitControlPopup(Control control, DataView data, string[][] header, string[] keyFilter, string KeyForEnter, string[] KeyForGrid)
        {
            if (control != null)
            {
                frmQuickType frm = new frmQuickType();
                frm.ControlOnKey = control;
                frm.Data = data;
                frm.Header = header;
                frm.KeyFilter = keyFilter;
                frm.LoadControl();
                frm.Key = KeyForEnter;
                frm.KeyForGrid = KeyForGrid;
                return frm;
            }
            else
                return null;
        }

        /// <summary> 
        /// Init popup 
        /// </summary>
        /// <param name="control">apply QuickType</param>
        /// <param name="data">DataView to search</param>
        /// <param name="header">Caption column and Visible column</param>
        /// <param name="keyFilter">Keyword for searching</param>
        /// <param name="KeyForEnter">Column return value</param>
        /// <param name="KeyForGrid">KeyForGrid not null and length=0 then use all column</param>
        /// <param name="KeyReturnToTag">KeyReturnToTag not null or empty then not set tag</param>
        /// <returns>Return inited Form</returns>        
        public static frmQuickType InitControlPopup(Control control, DataView data, string[][] header, string[] keyFilter, string KeyForEnter, string[] KeyForGrid, string KeyReturnToTag)
        {
            if (control != null)
            {
                frmQuickType frm = new frmQuickType();
                frm.ControlOnKey = control;
                frm.Data = data;
                frm.Header = header;
                frm.KeyFilter = keyFilter;
                frm.KeyReturnToTag = KeyReturnToTag;
                frm.LoadControl();
                frm.Key = KeyForEnter;
                frm.KeyForGrid = KeyForGrid;
                return frm;
            }
            else
                return null;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_controlOnKey == null) return;
                if (_isShowMessage) return;
                if (this.ContainsFocus || this.Capture) return;
                if (_controlOnKey is GridControl)
                {
                    if (Form.ActiveForm == null || ((GridControl)_controlOnKey).FocusedView.ActiveEditor == null) { this.Visible = false; if (btnQuickType != null) btnQuickType.Visible = false; }
                }
                else
                    if (Form.ActiveForm == null || !_controlOnKey.Focused) { this.Visible = false; if (btnQuickType != null) btnQuickType.Visible = false; }
            }
            catch { }
        }

        private void frmQuickType_VisibleChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.Visible;
        }

        private void frmQuickType_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void frmQuickType_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.9;
        }

        public event EventHandler SaveClick;
        private bool _isShowMessage = false;

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (SaveClick != null)
            {
                _isShowMessage = true;
                SaveClick(sender, e);
                _isShowMessage = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSaveClick(sender, e);
        }

        private void frmQuickType_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                grdData_MouseDoubleClick(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }

    #region Key hook
    public class UserActivityHook
    {
        #region Windows structure definitions


        [StructLayout(LayoutKind.Sequential)]
        private class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestcode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }


        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardHookStruct
        {
            public int vkcode;
            public int scancode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #endregion

        #region Windows function imports
        [DllImport("user32.dll", CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr hMod,
            int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
              CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(
            int idHook,
            int ncode,
            int wParam,
            IntPtr lParam);

        private delegate int HookProc(int ncode, int wParam, IntPtr lParam);

        [DllImport("user32")]
        private static extern int ToAscii(
            int uVirtKey,
            int uScancode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState);

        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        #endregion

        #region Windows constants

        private const int WH_MOUSE_LL = 14;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE = 7;
        private const int WH_KEYBOARD = 2;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        private const byte VK_SHIFT = 0x10;
        private const byte VK_CAPITAL = 0x14;
        private const byte VK_NUMLOCK = 0x90;

        #endregion

        public UserActivityHook()
        {
            Start();
        }

        public UserActivityHook(bool InstallMouseHook, bool InstallKeyboardHook)
        {
            Start(InstallMouseHook, InstallKeyboardHook);
        }

        ~UserActivityHook()
        {
            Stop(true, true, false);
        }
        public event MouseEventHandler OnMouseActivity;
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;
        private int hMouseHook = 0;
        private int hKeyboardHook = 0;
        private static HookProc MouseHookProcedure;
        private static HookProc KeyboardHookProcedure;

        public void Start()
        {
            this.Start(true, true);
        }

        public void Start(bool InstallMouseHook, bool InstallKeyboardHook)
        {
            if (hMouseHook == 0 && InstallMouseHook)
            {
                MouseHookProcedure = new HookProc(MouseHookProc);

                hMouseHook = SetWindowsHookEx(
                    WH_MOUSE_LL,
                    MouseHookProcedure,
                    Marshal.GetHINSTANCE(
                        Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);

                if (hMouseHook == 0)
                {

                    int errorcode = Marshal.GetLastWin32Error();

                    Stop(true, false, false);

                    throw new Win32Exception(errorcode);
                }
            }


            if (hKeyboardHook == 0 && InstallKeyboardHook)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);

                hKeyboardHook = SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    KeyboardHookProcedure,
                    Marshal.GetHINSTANCE(
                    Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);

                if (hKeyboardHook == 0)
                {
                    int errorcode = Marshal.GetLastWin32Error();
                    Stop(false, true, false);
                    throw new Win32Exception(errorcode);
                }
            }
        }

        public void Stop()
        {
            this.Stop(true, true, true);
        }

        public void Stop(bool UninstallMouseHook, bool UninstallKeyboardHook, bool ThrowExceptions)
        {
            if (hMouseHook != 0 && UninstallMouseHook)
            {
                int retMouse = UnhookWindowsHookEx(hMouseHook);
                hMouseHook = 0;
                if (retMouse == 0 && ThrowExceptions)
                {
                    int errorcode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorcode);
                }
            }

            if (hKeyboardHook != 0 && UninstallKeyboardHook)
            {
                int retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                if (retKeyboard == 0 && ThrowExceptions)
                {
                    int errorcode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorcode);
                }
            }
        }

        private int MouseHookProc(int ncode, int wParam, IntPtr lParam)
        {
            if ((ncode >= 0) && (OnMouseActivity != null))
            {
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        break;

                    case WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        break;

                    case WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
                        break;
                }

                int clickCount = 0;
                if (button != MouseButtons.None)
                    if (wParam == WM_LBUTTONDBLCLK || wParam == WM_RBUTTONDBLCLK) clickCount = 2;
                    else clickCount = 1;

                MouseEventArgs e = new MouseEventArgs(
                                                    button,
                                                    clickCount,
                                                    mouseHookStruct.pt.x,
                                                    mouseHookStruct.pt.y,
                                                    mouseDelta);

                OnMouseActivity(this, e);
            }
            return CallNextHookEx(hMouseHook, ncode, wParam, lParam);
        }

        private int KeyboardHookProc(int ncode, Int32 wParam, IntPtr lParam)
        {
            bool handled = false;
            if ((ncode >= 0) && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                if (KeyDown != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkcode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDown(this, e);
                    handled = handled || e.Handled;
                }

                if (KeyPress != null && wParam == WM_KEYDOWN)
                {
                    bool isDownShift = ((GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
                    bool isDownCapslock = (GetKeyState(VK_CAPITAL) != 0 ? true : false);

                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkcode,
                              MyKeyboardHookStruct.scancode,
                              keyState,
                              inBuffer,
                              MyKeyboardHookStruct.flags) == 1)
                    {
                        char key = (char)inBuffer[0];
                        if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                        KeyPressEventArgs e = new KeyPressEventArgs(key);
                        KeyPress(this, e);
                        handled = handled || e.Handled;
                    }
                }


                if (KeyUp != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkcode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUp(this, e);
                    handled = handled || e.Handled;
                }

            }

            if (handled)
                return 1;
            else
                return CallNextHookEx(hKeyboardHook, ncode, wParam, lParam);
        }
    }

    #endregion    

}