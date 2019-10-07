using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CommonLib.UserControls
{
    public partial class uctPopupControl : UserControl
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        const int BorderWidth = 3;
        private CommonLib.frmBorderLess.ResizeDirection _resizeDir = CommonLib.frmBorderLess.ResizeDirection.None;
        private bool _createBorder = true;
        protected Infragistics.Win.PopupInfo popInfo = null;
        protected Infragistics.Win.Misc.UltraPopupControlContainer upopCon;

        const int WM_MOUSEMOVE = 0x0200,MK_CONTROL = 0x0008, MK_LBUTTON = 0x0001, MK_MBUTTON = 0x0010, MK_RBUTTON = 0x0002, MK_SHIFT = 0x0004, MK_XBUTTON1 = 0x0020, MK_XBUTTON2 = 0x0040;
        const int WM_MOUSELEAVE = 0x02A3, WM_MOUSEWHEEL = 0x020A, WM_MOUSEHOVER = 0x02A1,WM_LBUTTONDOWN = 0x0201;
        const int HTCAPTION = 0x2;

        public uctPopupControl()
        {
            InitializeComponent();
            //this.Resize += new System.EventHandler(this.Invalidate);
            //this.MouseDown += new MouseEventHandler(this_MouseDown);
            //this.MouseMove += new MouseEventHandler(MoveWindow);
            popInfo = new Infragistics.Win.PopupInfo();
            upopCon = new Infragistics.Win.Misc.UltraPopupControlContainer(this.components);
            upopCon.PopupControl = this;
            upopCon.Closed += new EventHandler(upopCon_Closed);
        }        

        protected virtual void OnClose(object sender, EventArgs e)
        {
            if (Close != null)
                Close(sender, e);
        }

        void upopCon_Closed(object sender, EventArgs e)
        {
            OnClose(sender, e);
        }

        public event EventHandler Close;

        #region Constants

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTBORDER = 18;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;

        #endregion

        #region CreateBorder
        public bool CreateBorder
        {
            set { _createBorder = value; }
            get { return _createBorder; }
        }

        #endregion

        public virtual void ShowPopup(Control ctrl)
        {
            if (ctrl == null) { upopCon.Show(); return; }
            try
            {
                Rectangle rec = ctrl.RectangleToScreen(ctrl.DisplayRectangle);
                popInfo.ExclusionRect = rec;
                popInfo.PreferredLocation = new Point(rec.Right, rec.Bottom);
                popInfo.Position = Infragistics.Win.DropDownPosition.BelowExclusionRect | Infragistics.Win.DropDownPosition.LeftOfExclusionRect;
                popInfo.Owner = ctrl;
                upopCon.Show(popInfo);
            }
            catch
            {
                upopCon.Show(ctrl);

            }
        }

        public virtual void ShowPopup(Point p)
        {
            popInfo.Position = Infragistics.Win.DropDownPosition.BelowExclusionRect | Infragistics.Win.DropDownPosition.LeftOfExclusionRect;
            upopCon.Show(p);
        }

        private bool _draw = false;
        public void DrawBorder()
        {
            if (!_draw)
            {
                this.Padding = new Padding(this.Padding.Left + 3, this.Padding.Top + 3, this.Padding.Right + 3, this.Padding.Bottom + 3);
                _draw = true;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ResizeDir = CommonLib.frmBorderLess.ResizeDirection.None;
        }

        #region MoveWindow with event MouseDown
        public void MoveWindow(object sender, MouseEventArgs e)
        {
            this_MouseMove(sender, e);

            if (e.Button == MouseButtons.Left)
            {
                if (Cursor == Cursors.Default)
                {
                    //ReleaseCapture();
                    //SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
                else
                {
                    ReleaseCapture();
                    ResizeForm(ResizeDir);
                }
            }
        }
        #endregion

        #region this_MouseMove
        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X < BorderWidth && e.Location.Y < BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.TopLeft;
            else if (e.Location.X < BorderWidth && e.Location.Y > Height - BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.BottomLeft;
            else if (e.Location.X > Width - BorderWidth && e.Location.Y > Height - BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.BottomRight;
            else if (e.Location.X > Width - BorderWidth && e.Location.Y < BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.TopRight;
            else if (e.Location.X < BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Left;
            else if (e.Location.X > Width - BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Right;
            else if (e.Location.Y < BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Top;
            else if (e.Location.Y > Height - BorderWidth)
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.Bottom;
            else
                ResizeDir = CommonLib.frmBorderLess.ResizeDirection.None;
        }
        #endregion

        #region this_MouseDown
        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Cursor != Cursors.Default)
                ResizeForm(ResizeDir);
            else if (Cursor == Cursors.Default)
                MoveWindow(sender, e);
        }
        #endregion

        #region ResizeForm
        private void ResizeForm(CommonLib.frmBorderLess.ResizeDirection direction)
        {
            try
            {
                int dir = -1;
                switch (direction)
                {
                    case CommonLib.frmBorderLess.ResizeDirection.Left:
                        dir = HTLEFT;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopLeft:
                        dir = HTTOPLEFT;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Top:
                        dir = HTTOP;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.TopRight:
                        dir = HTTOPRIGHT;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Right:
                        dir = HTRIGHT;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomRight:
                        dir = HTBOTTOMRIGHT;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.Bottom:
                        dir = HTBOTTOM;
                        break;
                    case CommonLib.frmBorderLess.ResizeDirection.BottomLeft:
                        dir = HTBOTTOMLEFT;
                        break;
                }
                if (dir != -1)
                {
                    ReleaseCapture();

                    SendMessage(this.ParentForm.Handle, WM_NCLBUTTONDOWN, dir, 0);
                }
            }
            catch { }
        }
        #endregion

        #region Invalidate
        private void Invalidate(object sender, EventArgs e)
        {
            Invalidate();
        }
        #endregion

        #region ResizeDir
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

        #region PopupControl_Paint
        private void PopupControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.DarkGray), new Rectangle(2, 2, this.Width - 5, this.Height - 5));
            GC.Collect();
        }
        #endregion

        public void ClosePopup()
        {
            if (this.upopCon != null)
                upopCon.Close();
        }
    }
}
