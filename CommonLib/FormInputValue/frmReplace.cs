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
    public partial class frmReplace : CommonLib.dxfrmExtend
    {
        public frmReplace()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmReplace_Load(object sender, EventArgs e)
        {
            try
            {
                CommonLib.ShortKeyReg.RegisterHotKey(this, btnOK, Keys.Enter);
            }
            catch { }
        }
    }
}
