namespace CommonLib.ImportAndExport
{
    partial class frmMapColumn
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFolder = new CommonLib.Buttons.pscButton();
            this.chkNotShowAgain = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 336);
            this.panel1.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(506, 336);
            this.grdData.TabIndex = 1;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkNotShowAgain);
            this.panel2.Controls.Add(this.btnFolder);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 336);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(506, 34);
            this.panel2.TabIndex = 1;
            // 
            // btnFolder
            // 
            this.btnFolder.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnFolder.Location = new System.Drawing.Point(231, 6);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(73, 23);
            this.btnFolder.TabIndex = 2;
            this.btnFolder.Text = "Đồng ý";
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // chkNotShowAgain
            // 
            this.chkNotShowAgain.AutoSize = true;
            this.chkNotShowAgain.Location = new System.Drawing.Point(12, 10);
            this.chkNotShowAgain.Name = "chkNotShowAgain";
            this.chkNotShowAgain.Size = new System.Drawing.Size(187, 17);
            this.chkNotShowAgain.TabIndex = 3;
            this.chkNotShowAgain.Text = "Không hiện hộp thoại này lần nữa";
            this.chkNotShowAgain.UseVisualStyleBackColor = true;
            this.chkNotShowAgain.Visible = false;
            // 
            // frmMapColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 370);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmMapColumn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map column";
            this.Load += new System.EventHandler(this.frmMapColumn_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private CommonLib.Buttons.pscButton btnFolder;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        public System.Windows.Forms.CheckBox chkNotShowAgain;
    }
}