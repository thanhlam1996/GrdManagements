using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Infragistics.Win.UltraWinGrid;
using System.Runtime.InteropServices;
using System.Drawing;

namespace CommonLib.UserControls
{
    public class txtSupportQuickSearch : TextBox, IMessageFilter
    {
        #region Init
        public txtSupportQuickSearch()
            : base()
        {
            Application.AddMessageFilter(this);
        }

        uctSearchDics quickSearch = null;
        DataTable _dtData = null;
        public string FieldDisplay { get; set; }

        public void LoadData(DataTable dtData, string fieldDisplay)
        {
            _dtData = dtData;
            FieldDisplay = fieldDisplay;
            InitSearch();
        }
        #endregion

        #region OnCreateControl
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!DesignMode)
            {
                quickSearch = new uctSearchDics(this);

                quickSearch.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + this.Height + 1);
                quickSearch.Visible = false;
                quickSearch.TopMost = true;
                
                quickSearch.HitSearch += (obj, args) =>
                {
                    this.Text = args.Row.Cells[this.FieldDisplay].Value.ToString();
                    quickSearch.Visible = false;
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_dtData != null)
            {
                _dtData.Dispose();
                _dtData = null;
            }

            if (quickSearch != null)
            {
                quickSearch.Dispose();
                quickSearch = null;
            }
        }

        void InitSearch()
        {
            quickSearch.LoadData(_dtData);
        }
        #endregion

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            
            if (!DesignMode)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    quickSearch.SelectCurrentRow();
                    quickSearch.Visible = false;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    quickSearch.Visible = false;
                }
                else if (e.KeyCode != Keys.Tab && e.KeyCode != (Keys.ShiftKey | Keys.F17))
                {
                    if (!quickSearch.Visible)
                    {
                        quickSearch.BringToFront();
                        quickSearch.ShowPopup();
                    }
                    quickSearch.Search();
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!DesignMode)
            {
                if (e.KeyCode == Keys.Up)
                {
                    quickSearch.grdData.PerformAction(UltraGridAction.AboveRow);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    quickSearch.grdData.PerformAction(UltraGridAction.BelowRow);
                    e.Handled = true;
                }
                
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (!DesignMode)
            {
                if (IsInPopup())
                    quickSearch.Visible = false;
            }
        }

        bool IsInPopup()
        {
            Form frm = Form.ActiveForm;
            if (frm != null)
            {
                if (frm.IsMdiContainer)
                {
                    if (frm.ActiveMdiChild == quickSearch)
                        return true;
                }
                else
                {
                    if (frm == quickSearch)
                        return true;
                }
            }
            return false;
        }

        #region Hit
        internal class HitQuickSearch : EventArgs
        {
            public UltraGridRow Row { get; set; }
        }

        internal delegate void HitQuickSearchHandler(object sender, HitQuickSearch args);
        #endregion

        #region IMessageFilter Members
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        public bool PreFilterMessage(ref Message m)
        {
            if (quickSearch != null)
            {
                if ((m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN) && quickSearch.Visible)
                {
                    int x = m.LParam.ToInt32() & 0xffff;
                    int y = m.LParam.ToInt32() >> 16;
                    if (!IsInPopup() && !this.Bounds.Contains(new Point(x, y)))
                    {
                        quickSearch.Visible = false;
                    }
                }
                else if (Form.ActiveForm == null && quickSearch.Visible)
                {
                    quickSearch.Visible = false;
                }
                
            }

            return false;
        }        
        #endregion
    }
}
