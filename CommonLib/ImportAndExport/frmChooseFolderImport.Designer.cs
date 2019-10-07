namespace CommonLib.ImportAndExport
{
    partial class frmChooseFolderImport
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChooseFolder = new CommonLib.Buttons.pscButton();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnOK = new CommonLib.Buttons.pscButton();
            this.fldlgChoose = new System.Windows.Forms.FolderBrowserDialog();
            this.chkOptionImport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thư mục";
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(66, 23);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(362, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên file";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnChooseFolder.Location = new System.Drawing.Point(434, 23);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(91, 23);
            this.btnChooseFolder.TabIndex = 3;
            this.btnChooseFolder.Text = "Chọn thư mục";
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(66, 50);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(247, 21);
            this.txtFile.TabIndex = 4;
            this.txtFile.Text = "*.dbf";
            // 
            // btnOK
            // 
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(228, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(91, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkOptionImport
            // 
            this.chkOptionImport.AutoSize = true;
            this.chkOptionImport.Location = new System.Drawing.Point(319, 52);
            this.chkOptionImport.Name = "chkOptionImport";
            this.chkOptionImport.Size = new System.Drawing.Size(115, 17);
            this.chkOptionImport.TabIndex = 7;
            this.chkOptionImport.Text = "Import vào 1 bảng";
            this.chkOptionImport.UseVisualStyleBackColor = true;
            // 
            // frmChooseFolderImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 122);
            this.Controls.Add(this.chkOptionImport);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label1);
            this.Name = "frmChooseFolderImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chon Folder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CommonLib.Buttons.pscButton btnChooseFolder;
        private CommonLib.Buttons.pscButton btnOK;
        public System.Windows.Forms.TextBox txtFolder;
        public System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.FolderBrowserDialog fldlgChoose;
        public System.Windows.Forms.CheckBox chkOptionImport;
    }
}