using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;

namespace CommonLib
{
    public partial class frmBorderLess : DevExpress.XtraEditors.XtraForm
    {
        public frmBorderLess()
        {
            InitializeComponent();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BorderPaint);
            this.Resize += new System.EventHandler(this.Invalidate);
            this.MouseDown += new MouseEventHandler(this_MouseDown);
            this.MouseMove += new MouseEventHandler(MoveWindow);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Padding = new Padding(this.Padding.Left + 3, this.Padding.Top + 3, this.Padding.Right + 3, this.Padding.Bottom + 3);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ResizeDir = ResizeDirection.None;
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        const int BorderWidth = 6;
        private ResizeDirection _resizeDir = ResizeDirection.None;
        private bool _createBorder = true;

        public enum ResizeDirection
        {
            None = 0,
            Left = 1,
            TopLeft = 2,
            Top = 3,
            TopRight = 4,
            Right = 5,
            BottomRight = 6,
            Bottom = 7,
            BottomLeft = 8
        }

        #region CreateBorder
        public bool CreateBorder
        {
            set { _createBorder = value; }
            get { return _createBorder; }
        }

        #endregion

        #region ResizeDir
        public ResizeDirection ResizeDir
        {
            get { return _resizeDir; }
            set
            {
                _resizeDir = value;
                switch (value)
                {
                    case ResizeDirection.Left:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case ResizeDirection.Right:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case ResizeDirection.Top:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case ResizeDirection.Bottom:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case ResizeDirection.TopRight:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    case ResizeDirection.BottomRight:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    case ResizeDirection.TopLeft:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    case ResizeDirection.BottomLeft:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    default:
                        this.Cursor = Cursors.Default;
                        break;
                }
            }
        }
        #endregion

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

        #region BorderPaint
        private void BorderPaint(object sender, PaintEventArgs e)
        {
            if (sender is Control && CreateBorder)
            {
                Control s = (Control)sender;
                if (s.Name == this.Name)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.DarkGray), new Rectangle(2, 2, this.Width - 5, this.Height - 5));
                }
                GC.Collect();
            }
        }

        private void Invalidate(object sender, EventArgs e)
        {
            Invalidate();
        }
        #endregion

        #region MoveWindow with event MouseDown
        public void MoveWindow(object sender, MouseEventArgs e)
        {
            this_MouseMove(sender, e);

            if (e.Button == MouseButtons.Left)
            {
                if (Cursor == Cursors.Default)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
                else
                {
                    ReleaseCapture();
                    ResizeForm(ResizeDir);
                }
            }
        }
        #endregion

        #region ResizeForm
        private void ResizeForm(ResizeDirection direction)
        {
            int dir = -1;
            switch (direction)
            {
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.TopLeft:
                    dir = HTTOPLEFT;
                    break;
                case ResizeDirection.Top:
                    dir = HTTOP;
                    break;
                case ResizeDirection.TopRight:
                    dir = HTTOPRIGHT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
            }
            if (dir != -1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }
        #endregion

        #region this_MouseDown
        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && WindowState != FormWindowState.Minimized && Cursor != Cursors.Default)
                ResizeForm(ResizeDir);
            else if (Cursor == Cursors.Default)
                MoveWindow(sender, e);
        }
        #endregion

        #region this_MouseMove
        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X < BorderWidth && e.Location.Y < BorderWidth)
                ResizeDir = ResizeDirection.TopLeft;
            else if (e.Location.X < BorderWidth && e.Location.Y > Height - BorderWidth)
                ResizeDir = ResizeDirection.BottomLeft;
            else if (e.Location.X > Width - BorderWidth && e.Location.Y > Height - BorderWidth)
                ResizeDir = ResizeDirection.BottomRight;
            else if (e.Location.X > Width - BorderWidth && e.Location.Y < BorderWidth)
                ResizeDir = ResizeDirection.TopRight;
            else if (e.Location.X < BorderWidth)
                ResizeDir = ResizeDirection.Left;
            else if (e.Location.X > Width - BorderWidth)
                ResizeDir = ResizeDirection.Right;
            else if (e.Location.Y < BorderWidth)
                ResizeDir = ResizeDirection.Top;
            else if (e.Location.Y > Height - BorderWidth)
                ResizeDir = ResizeDirection.Bottom;
            else
                ResizeDir = ResizeDirection.None;
        }
        #endregion
    }
}