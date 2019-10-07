using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel.Design;
using System.ComponentModel;
using Infragistics.Win.UltraWinGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using DevExpress.Data.Selection;
using Infragistics.Win.Misc;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace CommonLib
{
    public sealed class HotKeyController : Component, IMessageFilter, ISupportInitialize
    {
        public delegate void HotKeyEventHandler(object sender, HotKeyEventArgs e);
        public static event HotKeyEventHandler HotKeyPressed;
        internal Dictionary<object, List<HotKeyId>> dicHotKeys = new Dictionary<object, List<HotKeyId>>();
        public static Dictionary<ContainerControl, HotKeyController> RegistedController = new Dictionary<ContainerControl, HotKeyController>();
        static System.Globalization.DateTimeFormatInfo fDate = new System.Globalization.DateTimeFormatInfo();

        public HotKeyController()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }

        #region AddHotKey
        public bool AddHotKey(object control, Keys hotKey)
        {
            string k = hotKey.ToString().Replace(',', '+').Replace(" ", "");

            if (dicHotKeys.Any(r => r.Value.Any(r2 => r2.Key == hotKey)))
            {
                //throw new Exception("Key (" + k + ") đã tồn tại!");
            }
            else
            {

                if (!dicHotKeys.Any(r => r.Key == control))
                    dicHotKeys.Add(control, new List<HotKeyId>());

                dicHotKeys[control].Add(new HotKeyId { StringValue = k, Key = hotKey });
            }
            return true;
        }
        #endregion

        public void RemoveHotKey(object control)
        {
            if (dicHotKeys.Any(r => r.Key == control))
            {
                dicHotKeys.Remove(control);
            }
        }

        #region RemoveAllKeys
        public void RemoveAllKeys()
        {
            dicHotKeys.Clear();
        }
        #endregion

        #region ProcessHotKey
        static Type[] TypeOfGrid = new Type[] { typeof(DataGridView), typeof(UltraGrid), typeof(GridControl), typeof(GridView) };
        internal static bool ProcessClickInernal(object raisedControl)
        {
            try
            {
                if (raisedControl is Button)
                {
                    if (((Button)raisedControl).Enabled)
                    {
                        ((Button)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is BaseButton)
                {
                    if (((BaseButton)raisedControl).Enabled)
                    {
                        ((BaseButton)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is BarButtonItem)
                {
                    if (((BarButtonItem)raisedControl).Enabled)
                    {
                        ((BarButtonItem)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is MenuItem)
                {
                    if (((MenuItem)raisedControl).Enabled)
                    {
                        ((MenuItem)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is ToolStripMenuItem)
                {
                    if (((ToolStripMenuItem)raisedControl).Enabled)
                    {
                        ((ToolStripMenuItem)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is UltraButtonBase)
                {
                    if (((Control)raisedControl).Enabled)
                    {
                        ((UltraButtonBase)raisedControl).PerformClick();
                        return true;
                    }
                }
                else if (raisedControl is IButtonControl)
                {
                    var ctrl = raisedControl as Control;
                    if (ctrl != null)
                    {
                        if (ctrl.Enabled)
                        {
                            ((IButtonControl)raisedControl).PerformClick();
                            return true;
                        }
                    }
                    else
                    {
                        ((IButtonControl)raisedControl).PerformClick();
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
        
        bool ProcessClick(object raisedControl)
        {
            return HotKeyController.ProcessClickInernal(raisedControl);
        }

        bool ProcessHotKey(Control affectControl, Keys hotKey)
        {
            try
            {
                if (affectControl == null) return false;

                HotKeyEventArgs args = new HotKeyEventArgs();
                args.RaisedControl = affectControl;
                args.Key = hotKey;
                foreach (var r1 in dicHotKeys)
                {
                    foreach (var r2 in r1.Value.Where(rr => rr.Key == hotKey))
                    {
                        args.HotKey = r2.StringValue;
                        args.RaisedControl = r1.Key;
                    }

                }

                try
                {
                    if (HotKeyController.HotKeyPressed != null && args.HotKey != null && args.HotKey != "")
                        HotKeyController.HotKeyPressed(args.RaisedControl, args);
                }
                catch { }
                if (!args.Handle)
                {
                    if (args.Key == CtrlA)
                    {
                        if (args.RaisedControl is EventHandler)
                        {
                            ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            if (!TypeOfGrid.Any(r => r == args.RaisedControl.GetType()))
                            {
                                return false;
                            }
                            else
                            {
                                return HotKeyController.ProcessGridSelectAll(((Control)args.RaisedControl));
                            }
                        }
                    }
                    else if (args.Key == CtrlC)
                    {
                        if (args.RaisedControl is EventHandler)
                        {
                            ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            if (!TypeOfGrid.Any(r => r == args.RaisedControl.GetType()))
                            {
                                return false;
                            }
                            else
                            {
                                return HotKeyController.ProcessGridCopy(((Control)args.RaisedControl));
                            }
                        }
                    }
                    else if (args.Key == CtrlV)
                    {
                        if (args.RaisedControl is EventHandler)
                        {
                            ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            if (!TypeOfGrid.Any(r => r == args.RaisedControl.GetType()))
                            {
                                return false;
                            }
                            else
                            {
                                return HotKeyController.ProcessGridPaste(((Control)args.RaisedControl));
                            }
                        }
                    }
                    else if (args.Key == CtrlZ)
                    {
                        if (args.RaisedControl is EventHandler)
                        {
                            ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            if (!TypeOfGrid.Any(r => r == args.RaisedControl.GetType()))
                            {
                                return false;
                            }
                            else
                            {
                                return HotKeyController.ProcessGridUndo(((Control)args.RaisedControl));
                            }
                        }
                    }
                    else if (args.Key == Clear)
                    {
                        if (args.RaisedControl is EventHandler)
                        {
                            ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            if (!TypeOfGrid.Any(r => r == args.RaisedControl.GetType()))
                            {
                                return false;
                            }
                            else
                            {
                                return HotKeyController.ProcessGridDelete(((Control)args.RaisedControl));
                            }
                        }
                    }
                    else
                    {
                        if (args.HotKey != null && args.HotKey != "")
                        {
                            if (args.RaisedControl is EventHandler)
                            {
                                ((EventHandler)args.RaisedControl)(affectControl, new EventArgs());
                                return true;
                            }
                            else
                            {
                                return ProcessClick(args.RaisedControl);
                            }
                        }
                    }
                }
            }
            catch { }

            return false;
        }
        #endregion

        #region ProcessGridSelectAll
        internal static bool ProcessGridSelectAll(Control gridControl)
        {
            if (gridControl is DataGridView)
            {
                DataGridView grid = gridControl as DataGridView;
                grid.SelectAll();
                return true;
            }
            else if (gridControl is UltraGrid)
            {
                UltraGrid grid = gridControl as UltraGrid;
                grid.Selected.Rows.AddRange((UltraGridRow[])grid.Rows.All);
                return true;
            }
            else if (gridControl is GridControl)
            {
                ColumnView grid = ((GridControl)gridControl).FocusedView as ColumnView;
                grid.SelectAll();
                return true;
            }

            return false;
        }
        #endregion

        #region ProcessGridCopy
        internal static bool ProcessGridCopy(Control gridControl)
        {
            if (gridControl is DataGridView)
            {
                DataGridView grid = gridControl as DataGridView;
                var eCells = from r in grid.SelectedCells.Cast<DataGridViewCell>()
                             orderby r.RowIndex ascending, r.ColumnIndex ascending
                             select r;
                var eRows = from r in eCells
                            select new { Row = r.RowIndex };
                var eDatas = from r1 in eRows.Distinct()
                             join r2 in eCells on r1.Row equals r2.RowIndex into g_Cells
                             select new { Row = r1, Cells = g_Cells };
                StringBuilder ret = new StringBuilder();
                foreach (var row in eDatas)
                {
                    string retCell = "";
                    foreach (var cell in row.Cells)
                    {
                        retCell += (retCell == "" ? "" : "\t") + cell.Value;
                    }
                    ret.AppendLine(retCell);
                }
                string clip = ret.ToString();
                if (clip == "" && grid.CurrentCell != null)
                {                    
                    clip = grid.CurrentCell.Value.ToString();
                }
                if (clip != "")
                {
                    Clipboard.SetDataObject(clip);
                    return true;
                }
            }
            else if (gridControl is UltraGrid)
            {
                UltraGrid grid = gridControl as UltraGrid;
                var eCells = from r in grid.Selected.Cells.Cast<UltraGridCell>()
                             orderby r.Row.Index ascending, r.Column.Index ascending
                             select r;
                var eRows = from r in eCells
                            select new { Row = r.Row.Index };
                var eDatas = from r1 in eRows.Distinct()
                             join r2 in eCells on r1.Row equals r2.Row.Index into g_Cells
                             select new { Row = r1, Cells = g_Cells };
                StringBuilder ret = new StringBuilder();
                foreach (var row in eDatas)
                {
                    string retCell = "";
                    foreach (var cell in row.Cells)
                    {
                        retCell += (retCell == "" ? "" : "\t") + cell.Value;
                    }
                    ret.AppendLine(retCell);
                }
                string clip = ret.ToString();
                if (clip != "")
                {
                    Clipboard.SetDataObject(clip);
                    return true;
                }
            }
            else if (gridControl is GridControl)
            {
                BaseView grid = ((GridControl)gridControl).FocusedView;
                BaseGridController data = grid.DataController;
                SelectionController currSelect = data.Selection;
                int[] rows = currSelect.GetSelectedRows();
                StringBuilder ret = new StringBuilder();
                foreach (var r in rows)
                {
                    GridRowSelectionInfo selInfo = currSelect.GetSelectedObject(r) as GridRowSelectionInfo;
                    string retCell = "";
                    foreach (var col in selInfo.GetColumns())
                    {
                        var val = data.GetRowValue(r, col.ColumnHandle);
                        retCell += (retCell == "" ? "" : "\t") + val;
                    }
                    ret.AppendLine(retCell);
                }
                string clip = ret.ToString();
                if (clip != "")
                {
                    Clipboard.SetDataObject(clip);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region ProcessGridPaste
        static string[] boolTrue = new string[] { "1", "true", "TRUE", "True" };
        static string[] boolFalse = new string[] { "0", "false", "FALSE", "False" };
        static object PreParse(Type DesType, object value)
        {
            object val = DBNull.Value;
            if (DesType == typeof(string))
            {
                return value.ToString();
            }
            else if (DesType == typeof(bool))
            {
                string v1 = value.ToString();
                if (boolTrue.Any(r => r == v1))
                    val = true;
                else if (boolFalse.Any(r => r == v1))
                    val = false;
            }
            else if (DesType == typeof(byte))
            {
                val = byte.Parse(value.ToString());
            }
            else if (DesType == typeof(char))
            {
                val = char.Parse(value.ToString());
            }
            else if (DesType == typeof(decimal))
            {
                val = decimal.Parse(value.ToString());
            }
            else if (DesType == typeof(double))
            {
                val = double.Parse(value.ToString());
            }
            else if (DesType == typeof(int))
            {
                val = int.Parse(value.ToString());
            }
            else if (DesType == typeof(long))
            {
                val = long.Parse(value.ToString());
            }
            else if (DesType == typeof(short))
            {
                val = short.Parse(value.ToString());
            }
            else if (DesType == typeof(Single))
            {
                val = Single.Parse(value.ToString());
            }
            else if (DesType == typeof(DateTime))
            {
                try
                {
                    val = DateTime.Parse(value.ToString(), fDate);
                }
                catch
                {
                    val = DateTime.Parse(value.ToString());
                }
            }
            return val;
        }

        class CBData
        {
            public string[] Data { get; set; }
        }

        internal static bool ProcessGridPaste(Control gridControl)
        {
            bool ischange = false;

            var clipData = Clipboard.GetDataObject().GetData(typeof(string)).ToString().Replace('\r'.ToString(), "");
            clipData = clipData.Trim();
            List<CBData> lstData = new List<CBData>();
            foreach (var r in clipData.Split('\n'))
            {
                lstData.Add(new CBData { Data = r.Split('\t') });
            }

            if (gridControl is DataGridView)
            {
                DataGridView grid = gridControl as DataGridView;
                var eCells = from r in grid.SelectedCells.Cast<DataGridViewCell>()
                             orderby r.RowIndex ascending, r.ColumnIndex ascending
                             select r;
                var eRows = from r in eCells
                            select new { Row = r.RowIndex };
                var eDatas = from r1 in eRows.Distinct()
                             join r2 in eCells on r1.Row equals r2.RowIndex into g_Cells
                             select new { Row = r1, Cells = g_Cells };
                if (lstData.Count > 1 || lstData.Any(r => r.Data.Length > 1))
                {
                    int run = 0;
                    foreach (var row in eDatas)
                    {
                        if (run >= lstData.Count) break;
                        string[] valPaste = lstData[run].Data;
                        int i = 0;
                        foreach (var cell in row.Cells)
                        {
                            if (i >= valPaste.Length) break;
                            cell.Value = PreParse(cell.ValueType, valPaste[i]);
                            i++;
                            ischange = true;
                        }
                        run++;
                    }
                }
                else if (lstData.Count > 0)
                {
                    if (grid.SelectedCells.Count == 0 && grid.CurrentCell != null)
                    {
                        grid.CurrentCell.Value = PreParse(grid.CurrentCell.ValueType, grid.CurrentCell.Value.ToString() + lstData[0].Data[0]);
                        ischange = true;
                    }
                    else
                    {
                        foreach (var cell in eCells)
                        {
                            cell.Value = PreParse(cell.ValueType, lstData[0].Data[0]);
                            ischange = true;
                        }
                    }
                }
                int crow = -1;
                if (grid.CurrentCell != null)
                {
                    crow = grid.CurrentCell.RowIndex;
                }
                else if (grid.CurrentRow != null)
                {
                    crow = grid.CurrentRow.Index;
                }
                if (crow >= 0)
                {
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        grid.UpdateCellValue(col.Index, crow);
                    }
                }
            }
            else if (gridControl is UltraGrid)
            {
                UltraGrid grid = gridControl as UltraGrid;
                var eCells = from r in grid.Selected.Cells.Cast<UltraGridCell>()
                             orderby r.Row.Index ascending, r.Column.Index ascending
                             select r;
                var eRows = from r in eCells
                            select new { Row = r.Row.Index };
                var eDatas = from r1 in eRows.Distinct()
                             join r2 in eCells on r1.Row equals r2.Row.Index into g_Cells
                             select new { Row = r1, Cells = g_Cells };

                if (lstData.Count > 1 || lstData.Any(r => r.Data.Length > 1))
                {
                    int run = 0;
                    foreach (var row in eDatas)
                    {
                        if (run >= lstData.Count) break;
                        string[] valPaste = lstData[run].Data;
                        int i = 0;
                        foreach (var cell in row.Cells)
                        {
                            if (i >= valPaste.Length) break;
                            cell.Value = PreParse(cell.Column.DataType, valPaste[i]);
                            i++;
                            ischange = true;
                        }
                        run++;
                    }

                }
                else if (lstData.Count > 0)
                {
                    if (grid.Selected.Cells.Count == 0 && grid.ActiveCell != null)
                    {
                        grid.ActiveCell.Value = PreParse(grid.ActiveCell.Column.DataType, grid.ActiveCell.Value.ToString() + lstData[0].Data[0]);
                        ischange = true;
                    }
                    else
                    {
                        foreach (var cell in eCells)
                        {
                            cell.Value = PreParse(cell.Column.DataType, lstData[0].Data[0]);
                            ischange = true;
                        }
                    }
                }
                grid.UpdateData();
            }
            else if (gridControl is GridControl)
            {
                BaseView grid = ((GridControl)gridControl).FocusedView;
                BaseGridController data = grid.DataController;
                SelectionController currSelect = data.Selection;
                int[] rows = currSelect.GetSelectedRows();
                foreach (var r in rows)
                {
                    GridRowSelectionInfo selInfo = currSelect.GetSelectedObject(r) as GridRowSelectionInfo;
                    string retCell = "";
                    foreach (var col in selInfo.GetColumns())
                    {
                        var val = data.GetRowValue(r, col.ColumnHandle);
                        retCell += (retCell == "" ? "" : "\t") + val;
                    }
                }
                if (lstData.Count > 1 || lstData.Any(r => r.Data.Length > 1))
                {
                    int run = 0;
                    foreach (var r in rows)
                    {
                        if (run >= lstData.Count) break;
                        string[] valPaste = lstData[run].Data;
                        int i = 0;
                        GridRowSelectionInfo selInfo = currSelect.GetSelectedObject(r) as GridRowSelectionInfo;
                        foreach (var col in selInfo.GetColumns())
                        {
                            if (i >= valPaste.Length) break;
                            data.SetRowValue(r, col.ColumnHandle, PreParse(col.ColumnType, valPaste[i]));
                            i++;
                            ischange = true;
                        }
                        run++;
                    }
                }
                else if (lstData.Count > 0)
                {
                    foreach (var r in rows)
                    {
                        GridRowSelectionInfo selInfo = currSelect.GetSelectedObject(r) as GridRowSelectionInfo;
                        foreach (var col in selInfo.GetColumns())
                        {
                            data.SetRowValue(r, col.ColumnHandle, PreParse(col.ColumnType, lstData[0].Data[0]));
                            ischange = true;
                        }
                    }
                }
                grid.UpdateCurrentRow();
            }

            return ischange;
        }
        #endregion

        #region ProcessGridUndo
        internal static bool ProcessGridUndo(Control gridControl)
        {
            IEnumerable<DataRow> eDatas = null;

            if (gridControl is DataGridView)
            {
                DataGridView grid = gridControl as DataGridView;

            }
            else if (gridControl is UltraGrid)
            {
                UltraGrid grid = gridControl as UltraGrid;
            }
            else if (gridControl is GridControl)
            {
                BaseView grid = ((GridControl)gridControl).FocusedView;
            }

            if (eDatas != null)
            {

            }

            return false;
        }
        #endregion

        #region ProcessGridDelete
        internal static bool ProcessGridDelete(Control gridControl)
        {
            return false;
        }
        #endregion

        #region IMessageFilter Members
        const Keys CtrlA = Keys.Control | Keys.A;
        const Keys CtrlC = Keys.Control | Keys.C;
        const Keys CtrlV = Keys.Control | Keys.V;
        const Keys CtrlZ = Keys.Control | Keys.Z;
        const Keys Clear = Keys.Delete;
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        Message lastMessage;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x100 || m.Msg == 0x104)
            {
                lastMessage = m;
                Keys keyCode = ((Keys)(int)m.WParam) | Form.ModifierKeys;
                Control affectControl = Control.FromHandle(m.HWnd);
                if (affectControl is TextBoxBase && (keyCode == CtrlA || keyCode == CtrlC || keyCode == CtrlZ || keyCode == CtrlV))
                {
                    return false;
                }

                bool isHandle = false;
                Form activeForm = Form.ActiveForm;
                if (activeForm != null && activeForm.IsMdiContainer)
                {
                    if (activeForm.ActiveMdiChild != null)
                    {
                        if (HotKeyController.RegistedController.Any(r => r.Key == activeForm.ActiveMdiChild))
                        {
                            foreach (var objkey in HotKeyController.RegistedController[activeForm.ActiveMdiChild].dicHotKeys)
                            {
                                foreach (var hkey in objkey.Value.Where(r => r.Key == keyCode))
                                {
                                    isHandle = true;
                                    if (objkey.Key is EventHandler)
                                    {
                                        ((EventHandler)objkey.Key)(affectControl, new EventArgs());
                                        return true;
                                    }
                                    else
                                    {
                                        return ProcessClick(objkey.Key);
                                    }

                                }
                            }
                        }
                    }

                    if (!isHandle && HotKeyController.RegistedController.Any(r => r.Key == activeForm))
                    {
                        foreach (var objkey in HotKeyController.RegistedController[activeForm].dicHotKeys)
                        {
                            foreach (var hkey in objkey.Value.Where(r => r.Key == keyCode))
                            {
                                isHandle = true;
                                if (objkey.Key is EventHandler)
                                {
                                    ((EventHandler)objkey.Key)(affectControl, new EventArgs());
                                    return true;
                                }
                                else
                                {
                                    return ProcessClick(objkey.Key);
                                }
                            }
                        }
                    }

                    if (!isHandle)
                    {
                        return ProcessHotKey(affectControl, keyCode);
                    }
                }
                else if (activeForm != null)
                {
                    return ProcessHotKey(affectControl, keyCode);
                }
            }
            return false;
        }

        #endregion

        #region InitializeComponent
        private System.ComponentModel.IContainer components = null;
        private void InitializeComponent()
        {

        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                HotKeyController.RegistedController.Remove(this.ContainerControl);
            }
            catch { }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public ContainerControl ContainerControl
        {
            get { return _containerControl; }
            set { _containerControl = value; }
        }
        private ContainerControl _containerControl = null;

        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }

                IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    IComponent componentHost = host.RootComponent;
                    if (componentHost is ContainerControl)
                    {
                        ContainerControl = componentHost as ContainerControl;
                    }
                }
            }
        }
        #endregion

        #region ISupportInitialize Members

        public void BeginInit() { }

        public void EndInit()
        {
            setUpParentForm();
        }
        private void setUpParentForm()
        {
            if (this.ContainerControl != null) return;
            IDesignerHost host;
            if (Site != null)
            {
                host = Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    if (host.RootComponent is ContainerControl)
                    {
                        this.ContainerControl = (ContainerControl)host.RootComponent;
                    }
                }
            }
        }
        #endregion
    }

    public sealed class HotKeyEventArgs : EventArgs
    {
        bool _handle = false;
        public string HotKey { get; set; }
        public object RaisedControl { get; set; }
        public Keys Key { get; set; }
        public bool Handle
        {
            get { return _handle; }
            set { _handle = value; }
        }

        public HotKeyEventArgs()
        {
            this.HotKey = "";
            this.Key = Keys.None;
            this.RaisedControl = null;
        }
    }

    internal class HotKeyId
    {
        public Keys Key { get; set; }
        public string StringValue { get; set; }
        Dictionary<DataRow, int> StoreChangedEvent = null;

        public HotKeyId()
        {
            this.Key = Keys.None;
            this.StringValue = "";
        }
    }

    #region ShortKeyReg
    public sealed class ShortKeyReg
    {
        public static void RegisterHotKey(Form form, object button, Keys key)
        {
            try
            {
                if (key == Keys.Escape)
                {
                    IButtonControl btn = button as IButtonControl;
                    if (btn != null)
                        form.CancelButton = btn;
                }
                else if (key == Keys.Enter)
                {
                    IButtonControl btn = button as IButtonControl;
                    if (btn != null)
                        form.AcceptButton = btn;
                }
                else
                {
                    HotKeyController controller = null;

                    if (!HotKeyController.RegistedController.Any(r => r.Key == form))
                    {
                        controller = new HotKeyController();
                        controller.ContainerControl = form;
                        HotKeyController.RegistedController.Add(form, controller);
                    }
                    else
                    {
                        controller = HotKeyController.RegistedController[form];
                    }
                    form.FormClosed += (obj, args) =>
                        {
                            controller.RemoveAllKeys();
                        };
                    controller.AddHotKey(button, key);
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
                //throw new Exception("ShortKeyReg RegisterHotKey error! => " + Error);
            }
        }
       
        public static void RegisterHotKey(Form form, EventHandler handler, Keys key)
        {
            try
            {
                HotKeyController controller = null;

                if (!HotKeyController.RegistedController.Any(r => r.Key == form))
                {
                    controller = new HotKeyController();
                    controller.ContainerControl = form;
                    HotKeyController.RegistedController.Add(form, controller);
                }
                else
                {
                    controller = HotKeyController.RegistedController[form];
                }
                form.FormClosed += (obj, args) =>
                {
                    controller.RemoveAllKeys();
                };
                controller.AddHotKey(handler, key);
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Error += "\r\n" + ex.Message;
                }
                //throw new Exception("ShortKeyReg RegisterHotKey error! => " + Error);
            }
        }

        public static void RemoveHotKey(Form form, object button)
        {
            if (HotKeyController.RegistedController.Any(r => r.Key == form))
            {
                HotKeyController controller = HotKeyController.RegistedController[form];
                controller.RemoveHotKey(button);
            }
        }

        public static void RemoveHotKey(Form form, EventHandler handler)
        {
            if (HotKeyController.RegistedController.Any(r => r.Key == form))
            {
                HotKeyController controller = HotKeyController.RegistedController[form];
                controller.RemoveHotKey(handler);
            }
        }
        private const byte ModAlt = 1, ModControl = 2, ModShift = 4, ModWin = 8;

        static byte Win32Modifiers(bool Shift, bool Control, bool Alt)
        {
            byte toReturn = 0;
            if (Shift)
                toReturn += ModShift;
            if (Control)
                toReturn += ModControl;
            if (Alt)
                toReturn += ModAlt;
            return toReturn;
        }

        public static byte Win32ModifiersFromKeys(Keys k)
        {
            byte total = 0;

            if (((int)k & (int)Keys.Shift) == (int)Keys.Shift)
                total += ModShift;
            if (((int)k & (int)Keys.Control) == (int)Keys.Control)
                total += ModControl;
            if (((int)k & (int)Keys.Alt) == (int)Keys.Alt)
                total += ModAlt;
            if (((int)k & (int)Keys.LWin) == (int)Keys.LWin)
                total += ModWin;

            return total;
        }

        public static bool HotKeyForceAttach(Message m)
        {
            try
            {
                Form activeForm = Form.ActiveForm;
                if (activeForm == null) return false;
                Keys keyCode = ((Keys)(int)m.WParam) | Form.ModifierKeys;
                Control affectControl = Control.FromHandle(m.HWnd);
                if (HotKeyController.RegistedController.ContainsKey(activeForm))
                {
                    foreach (var objkey in HotKeyController.RegistedController[activeForm].dicHotKeys)
                    {
                        foreach (var hkey in objkey.Value.Where(r => r.Key == keyCode))
                        {
                            if (objkey.Key is EventHandler)
                            {
                                ((EventHandler)objkey.Key)(null, new EventArgs());
                                return true;
                            }
                            else
                            {
                                return HotKeyController.ProcessClickInernal(objkey.Key);
                            }
                        }

                    }
                }
                if (activeForm.IsMdiContainer)
                {
                    activeForm = activeForm.ActiveMdiChild;
                }
                else
                    return false;
                if (!HotKeyController.RegistedController.ContainsKey(activeForm)) return false;

                foreach (var objkey in HotKeyController.RegistedController[activeForm].dicHotKeys)
                {
                    foreach (var hkey in objkey.Value.Where(r => r.Key == keyCode))
                    {
                        if (objkey.Key is EventHandler)
                        {
                            ((EventHandler)objkey.Key)(affectControl, new EventArgs());
                            return true;
                        }
                        else
                        {
                            return HotKeyController.ProcessClickInernal(objkey.Key);
                        }
                    }
                }
            }
            catch { }

            return false;
        }

        public static bool HotKeyForceAttach(Keys key)
        {
            try
            {
                Form activeForm = Form.ActiveForm;
                if (activeForm == null) return false;


                if (HotKeyController.RegistedController.ContainsKey(activeForm))
                {
                    foreach (var objkey in HotKeyController.RegistedController[activeForm].dicHotKeys)
                    {
                        foreach (var hkey in objkey.Value.Where(r => r.Key == key))
                        {
                            if (objkey.Key is EventHandler)
                            {
                                ((EventHandler)objkey.Key)(null, new EventArgs());
                                return true;
                            }
                            else
                            {
                                return HotKeyController.ProcessClickInernal(objkey.Key);
                            }
                        }
                    }
                }

                if (activeForm.IsMdiContainer)
                {
                    activeForm = activeForm.ActiveMdiChild;
                }
                else
                    return false;
                if (!HotKeyController.RegistedController.ContainsKey(activeForm)) return false;

                foreach (var objkey in HotKeyController.RegistedController[activeForm].dicHotKeys)
                {
                    foreach (var hkey in objkey.Value.Where(r => r.Key == key))
                    {
                        if (objkey.Key is EventHandler)
                        {
                            ((EventHandler)objkey.Key)(null, new EventArgs());
                            return true;
                        }
                        else
                        {
                            return HotKeyController.ProcessClickInernal(objkey.Key);
                        }
                    }
                }
            }
            catch { }

            return false;
        }
    }

    #endregion
}
