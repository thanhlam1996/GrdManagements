using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace CommonLib.ImportAndExport
{
    public partial class frmStatus : frmBorderLess
    {
        [DllImportAttribute("user32.dll")]
        internal static extern long ShowWindow(IntPtr hWnd, long nCmdShow);

        PostionStatus _position = PostionStatus.Default;

        public PostionStatus Position
        {
            get { return _position; }
            set 
            {
                _position = value;
            }
        }

        public frmStatus()
        {
            InitializeComponent();
        }

        public event StatusShowHandler StatusShow;

        protected void OnStatusShow(object sender, StatusShowArgs e)
        {
            if (StatusShow != null)
                StatusShow(sender, e);
        }

        delegate void SetVisibleCoreCallback(bool value);

        protected override void SetVisibleCore(bool value)
        {
            StatusShowArgs e = new StatusShowArgs();
            OnStatusShow(this, e);
            if (e.Cancel && value)
            {
                StatusForm.IsBusy = false;
                if (this.Visible)
                    base.SetVisibleCore(false);
                Application.DoEvents();
                return; 
            }
            this.EnableTimer = true;
            if (value) ShowWindow(this.Handle,4);
            if (this.InvokeRequired)
            {
                SetVisibleCoreCallback d = new SetVisibleCoreCallback(SetVisibleCore);
                this.Invoke(d, value);
            }
            else
                base.SetVisibleCore(value);
            if (value)
            {
                switch (Position)
                {
                    case PostionStatus.Default:
                    case PostionStatus.Center:
                        this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
                        break;
                    case PostionStatus.LeftTop:
                        this.Location = new Point(10, 54);
                        break;
                    case PostionStatus.LeftBottom:
                        this.Location = new Point(10, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 20);
                        break;
                    case PostionStatus.RightTop:
                        this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10, 54);
                        break;
                    case PostionStatus.RightBottom:
                        this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 20);
                        break;
                }
            }
            Application.DoEvents();
        }       

        public int TimeWaitOut = 5000;
        int time = 0;

        public bool EnableTimer
        {
            get { return timer1.Enabled; }
            set 
            { 
                time = 0; timer1.Enabled = value;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((time >= TimeWaitOut || !StatusForm.IsBusy))
            {
                this.HideStatus();
                EnableTimer = false;
                time = 0;
                StatusForm.IsBusy = false;
                Application.DoEvents();
            }
            else
            {
                time += timer1.Interval;
                Application.DoEvents();
            }
        }

        delegate void StringParameterDelegate(string value);

        public void UpdateStatus(string value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterDelegate(UpdateStatus), new object[] { value });
                return;
            }            
            if(!this.Visible)
                base.SetVisibleCore(true);
            lblStatus.Text = value;
            if (Form.ActiveForm != null)
                this.Activate();
            Application.DoEvents();
        }

        public void ShowStatus()
        {
            Form active = Form.ActiveForm;
            if (active != null)
                this.Show(active);
            else if(StatusForm.mainForm!=null)
                this.Show(StatusForm.mainForm);
            else
                this.Show();

        }

        internal void HideStatus()
        {
            base.SetVisibleCore(false);
        }
    }

    public delegate void StatusShowHandler(object sender,StatusShowArgs e);

    public class StatusShowArgs : EventArgs
    {
        bool _cancel = false;

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
    }

    public enum PostionStatus
    {
        Default,Center,LeftTop,LeftBottom,RightTop,RightBottom
    }

    public class StatusForm
    {
        public static bool IsBusy = false;
        static frmStatus frm = null;
        static PostionStatus pos = PostionStatus.Default;
        public static Form mainForm = null;
        public static void SetPostionStatus(PostionStatus position)
        {
            pos = position;
        }

        public static frmStatus Self
        {
            get
            {
                if (frm == null)
                {
                    frm = new frmStatus();
                    frm.Text = "";
                    frm.ShowInTaskbar = false;
                    frm.TimeWaitOut = 2000;
                    frm.EnableTimer = true;
                }
                return frm;
            }
        }

        public static void SetBusy()
        {
            if (frm == null)
            {
                frm = new frmStatus();
                frm.Text = "";
                frm.ShowInTaskbar = false;
                frm.TimeWaitOut = 2000;
                frm.EnableTimer = true;
            }
            if (frm.IsDisposed)
            {
                frm = null;
                frm = new frmStatus();
                frm.Text = "";
                frm.ShowInTaskbar = false;
                frm.TimeWaitOut = 2000;
                frm.EnableTimer = true;
            }
            if (mainForm != null)
                frm.Owner = mainForm;
            else if (Form.ActiveForm != null)
                frm.Owner = Form.ActiveForm;
            frm.Position = pos;
            IsBusy = true;
            frm.Visible = true;
        }

        public static void StopBusy()
        {
            if (frm != null)
            {
                if (!frm.IsDisposed)
                {
                    frm.HideStatus();
                }
            }
            IsBusy = false;
        }
    }
}