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
    public partial class frmQConvertFontImport : DevExpress.XtraEditors.XtraForm
    {
        private bool _isImport = true;

        public bool IsImport
        {
            get { return _isImport; }
            set 
            { 
                _isImport = value;
                cboChangefont.Items.Clear();
                if (_isImport)
                {
                    this.cboChangefont.Items.AddRange(new object[] {
            "Chuyển TCVN sang Unicode",
            "Chuyển VNI sang Unicode",
            "Không chuyển mã"});
                }
                else
                {
                    this.cboChangefont.Items.AddRange(new object[] {
            "Chuyển Unicode sang TCVN",
            "Chuyển Unicode sang VNI",
            "Không chuyển mã"});
                }

                this.cboChangefont.SelectedIndex = 2;
            }
        }
        public frmQConvertFontImport()
        {
            InitializeComponent();
        }

        private void frmQConvertFontImport_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch { }
        }

        private void btnOK1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}