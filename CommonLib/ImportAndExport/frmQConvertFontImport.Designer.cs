namespace CommonLib.ImportAndExport
{
    partial class frmQConvertFontImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboChangefont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK1 = new CommonLib.Buttons.btnOK();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboChangefont
            // 
            this.cboChangefont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChangefont.FormattingEnabled = true;
            this.cboChangefont.Items.AddRange(new object[] {
            "Chuyển TCVN sang Unicode",
            "Chuyển VNI sang Unicode",
            "Chuyển Unicode sang TCVN",
            "Chuyển VNI sang TCVN",
            "Không chuyển mã"});
            this.cboChangefont.Location = new System.Drawing.Point(85, 25);
            this.cboChangefont.Name = "cboChangefont";
            this.cboChangefont.Size = new System.Drawing.Size(235, 21);
            this.cboChangefont.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Chuyển font";
            // 
            // btnOK1
            // 
            this.btnOK1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOK1.Location = new System.Drawing.Point(245, 63);
            this.btnOK1.Name = "btnOK1";
            this.btnOK1.Size = new System.Drawing.Size(75, 23);
            this.btnOK1.TabIndex = 13;
            this.btnOK1.Text = "&Đồng ý";
            this.btnOK1.Click += new System.EventHandler(this.btnOK1_Click);
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(15, 63);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(170, 17);
            this.chkDefault.TabIndex = 14;
            this.chkDefault.Text = "Không hiện hộp thoại này nữa";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // frmQConvertFontImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 98);
            this.Controls.Add(this.chkDefault);
            this.Controls.Add(this.btnOK1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboChangefont);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmQConvertFontImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển Font";
            this.Load += new System.EventHandler(this.frmQConvertFontImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CommonLib.Buttons.btnOK btnOK1;
        public System.Windows.Forms.ComboBox cboChangefont;
        public System.Windows.Forms.CheckBox chkDefault;
    }
}