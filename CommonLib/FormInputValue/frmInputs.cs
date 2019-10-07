using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.FormInputValue
{
    public partial class frmInputs : CommonLib.dxfrmExtend
    {
        #region Init
        public frmInputs()
        {
            InitializeComponent();
        }

        public frmInputs(string titleValue,string titleButton)
        {            
            InitializeComponent();
            TitleValue = titleValue;
            TitleButton = titleButton;
        }

        public frmInputs(string titleValue)
        {
            InitializeComponent();
            TitleValue = titleValue;
        }
        #endregion

        #region Properties
        public string Value
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public string TitleValue
        {
            get { return lblValue.Text; }
            set { lblValue.Text = value; }
        }

        public string TitleButton
        {
            get { return btnInput.Text; }
            set { btnInput.Text = value; }
        }

        public bool UseSearchButton
        {
            get { return btnSearch.Visible; }
            set { btnSearch.Visible = value; }
        }
        #endregion

        #region Event
        private void btnInput_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnInput_Click(null, null);
            }
        }

        private void lblValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int left = lblValue.Location.X;
                int width = lblValue.Width;
                int right = left + width+20;

                int pos = right - txtValue.Location.X;
                if (pos > 0)
                {
                    txtValue.Location = new Point(right + 20, txtValue.Location.Y);
                    txtValue.Width -= (pos+20);
                }
            }
            catch { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnSearchClick(this, e);
        }

        #endregion

        #region Build event
        public event EventHandler SearchClick;

        protected void OnSearchClick(object sender,EventArgs e)
        {
            if (SearchClick != null)
                SearchClick(sender, e);
        }
        #endregion

        #region frmInputs_Load
        private void frmInputs_Load(object sender, EventArgs e)
        {            
            CommonLib.ShortKeyReg.RegisterHotKey(this, CloseForm, Keys.Escape);
        }
        void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
