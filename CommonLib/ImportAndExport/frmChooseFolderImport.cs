using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CommonLib.ImportAndExport
{
    public partial class frmChooseFolderImport : DevExpress.XtraEditors.XtraForm
    {
        public string FileConstruct = "";

        public frmChooseFolderImport()
        {
            InitializeComponent();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (fldlgChoose.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder.Text = fldlgChoose.SelectedPath;
                }
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }
    }
}