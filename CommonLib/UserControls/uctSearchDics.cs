using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Runtime.InteropServices;

namespace CommonLib.UserControls
{
    [DesignTimeVisible(false), ToolboxItem(false)]
    public partial class uctSearchDics : Form
    {
        const int BorderWidth = 3;

        #region Constants

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTBORDER = 18;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTCAPTION = 2;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;

        #endregion

        txtSupportQuickSearch textBox { get; set; }

        #region Init
        public uctSearchDics(txtSupportQuickSearch TextBox)
        {
            InitializeComponent();
            this.textBox = TextBox;
            grdData.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            grdData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
        }
        #endregion

        public void LoadData(DataTable dtData)
        {
            try
            {
                DataTable dt = new DataTable();
                foreach (DataColumn col in dtData.Columns)
                {
                    dt.Columns.Add(col.ColumnName, typeof(string));
                }
                foreach (DataRow dr in dtData.Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
                grdData.DataSource = dt.DefaultView;
                grdData.DisplayLayout.Bands[0].ColHeadersVisible = false;
                grdData.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;
                int w = 0;
                foreach (UltraGridColumn col in grdData.DisplayLayout.Bands[0].Columns)
                {
                    col.MinWidth = 60;
                    col.PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
                    w += col.Width;
                }

                if (w > 50)
                {
                    this.Width = w;
                }
                grdData.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            }
            catch { }
        }

        #region Resize
        private int x = 0;
        private int y = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            x = e.X;
            y = e.Y;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.None)
            {
                if (e.Location.X < BorderWidth && e.Location.Y < BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.TopLeft;
                else if (e.Location.X < BorderWidth && e.Location.Y > Height - BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.BottomLeft;
                else if (e.Location.X > Width - BorderWidth && e.Location.Y < BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.TopRight;
                else if (e.Location.X >= Width - BorderWidth && e.Location.Y >= Height - BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.BottomRight;
                else if (e.Location.X > Width - BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Right;
                else if (e.Location.Y > Height - BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Bottom;
                else if (e.Location.X < BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Left;
                else if (e.Location.Y < BorderWidth)
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Top;
                else
                    ResizeDir = CommonLib.frmBorderLess.ResizeDirection.None;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (ResizeDir != CommonLib.frmBorderLess.ResizeDirection.None)
                {
                    ResizeForm(ResizeDir, e.Location);
                    x = e.X;
                    y = e.Y;
                }
                else if (ResizeDir == CommonLib.frmBorderLess.ResizeDirection.None)
                {
                    this.Left = (this.Left + e.X) - x;
                    this.Top = (this.Top + e.Y) - y;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Cursor = Cursors.Default;
        }

        private void ResizeForm(CommonLib.frmBorderLess.ResizeDirection direction, Point cPoint)
        {
            try
            {
                switch (direction)
                {
                    case CommonLib.frmBorderLess.ResizeDirection.Left:
                        {
                            Point p2 = this.Parent.PointToClient(MousePosition);
                            int l1 = this.Left;
                            this.Left = p2.X;
                            int l2 = this.Left;
                            int dx = l1 - l2;
                            this.Width += dx;
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopLeft:
                        {
                            Point p2 = this.Parent.PointToClient(MousePosition);
                            int l1 = this.Left;
                            this.Left = p2.X;
                            int l2 = this.Left;
                            int dx = l1 - l2;
                            this.Width += dx;

                            int t1 = this.Top;
                            this.Top = p2.Y;
                            int t2 = this.Top;
                            int dy = t1 - t2;
                            this.Height += dy;
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Top:
                        {
                            Point p2 = this.Parent.PointToClient(MousePosition);
                            int l1 = this.Top;
                            this.Top = p2.Y;
                            int l2 = this.Top;
                            int dy = l1 - l2;
                            this.Height += dy;
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopRight:
                        {
                            Point p2 = this.Parent.PointToClient(MousePosition);
                            int l1 = this.Top;
                            this.Top = p2.Y;
                            int l2 = this.Top;
                            int dy = l1 - l2;
                            this.Height += dy;
                            this.Width += (cPoint.X - x);
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Right:
                        {
                            this.Width += (cPoint.X - x);
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomRight:
                        {
                            this.Width += (cPoint.X - x);
                            this.Height += (cPoint.Y - y);
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Bottom:
                        {
                            this.Height += (cPoint.Y - y);
                        }
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomLeft:
                        {
                            Point p2 = this.Parent.PointToClient(MousePosition);
                            int l1 = this.Left;
                            this.Left = p2.X;
                            int l2 = this.Left;
                            int dx = l1 - l2;
                            this.Width += dx;
                            this.Height += (cPoint.Y - y);
                        }
                        break;
                }
            }
            catch { }
        }

        private CommonLib.frmBorderLess.ResizeDirection _resizeDir = CommonLib.frmBorderLess.ResizeDirection.None;

        public CommonLib.frmBorderLess.ResizeDirection ResizeDir
        {
            get { return _resizeDir; }
            set
            {
                _resizeDir = value;
                switch (value)
                {
                    case CommonLib.frmBorderLess.ResizeDirection.Left:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Right:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Top:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Bottom:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopRight:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomRight:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopLeft:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomLeft:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    default:
                            this.Cursor = Cursors.Default;
                        break;
                }
            }
        }
        #endregion

        #region Grid
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                UIElement element = grdData.DisplayLayout.UIElement.LastElementEntered;
                UltraGridRow row = null;
                if (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem is UltraGridRow)
                    row = grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem as UltraGridRow;
                else if (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem is UltraGridCell)
                    row = (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem as UltraGridCell).Row;
                if (row != null)
                {
                    if (HitSearch != null)
                    {
                        HitSearch(grdData, new txtSupportQuickSearch.HitQuickSearch { Row = row });
                    }
                }
            }
            catch { }
        }

        private void grdData_Click(object sender, EventArgs e)
        {
            grdData_DoubleClick(sender, e);
        }
        #endregion

        internal void ShowPopup()
        {
            if (grdData.DataSource != null)
            {
                this.Visible = true;
                var p = textBox.Parent.PointToScreen(textBox.Location);
                this.Location = new System.Drawing.Point(p.X, p.Y + textBox.Height + 1);
                textBox.Focus();
            }
        }

        internal event CommonLib.UserControls.txtSupportQuickSearch.HitQuickSearchHandler HitSearch;

        internal void RefreshView()
        {
            DataView dv = grdData.DataSource as DataView;
            if (dv != null)
                dv.RowFilter = string.Empty;
        }

        internal void Search()
        {
            try
            {
                DataView dvData = grdData.DataSource as DataView;
                string s = string.Empty;
                string ss = textBox.Text.Trim();
                if (ss != "")
                {
                    foreach (DataColumn col in dvData.Table.Columns)
                    {
                        s += (s == "" ? "" : " or ") + col + " like '%" + ss + "%'";
                    }
                }
                
                dvData.RowFilter = s;
            }
            catch { }
        }

        internal void SelectCurrentRow()
        {
            if (grdData.ActiveRow != null)
            {
                if (HitSearch != null)
                {
                    HitSearch(grdData, new txtSupportQuickSearch.HitQuickSearch { Row = grdData.ActiveRow });
                }
            }
        }

        private void grdData_MouseMove(object sender, MouseEventArgs e)
        {
            _resizeDir = CommonLib.frmBorderLess.ResizeDirection.None;
            this.Cursor = Cursors.Default;
        }
    }
}
