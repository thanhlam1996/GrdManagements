namespace CommonLib.FormInputValue
{
    partial class frmViewDataSource
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
            this.components = new System.ComponentModel.Container();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.dtgData = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuReplaceCell = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReplaceColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReplaceAll = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTable
            // 
            this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(12, 12);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(197, 21);
            this.cboTable.TabIndex = 0;
            // 
            // dtgData
            // 
            this.dtgData.AllowUserToAddRows = false;
            this.dtgData.AllowUserToDeleteRows = false;
            this.dtgData.AllowUserToResizeRows = false;
            this.dtgData.BackgroundColor = System.Drawing.Color.White;
            this.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgData.ContextMenuStrip = this.cmsMenu;
            this.dtgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgData.Location = new System.Drawing.Point(0, 40);
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(824, 378);
            this.dtgData.TabIndex = 1;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReplaceCell,
            this.mnuReplaceColumn,
            this.mnuReplaceAll});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(315, 70);
            // 
            // mnuReplaceCell
            // 
            this.mnuReplaceCell.Name = "mnuReplaceCell";
            this.mnuReplaceCell.Size = new System.Drawing.Size(314, 22);
            this.mnuReplaceCell.Text = "Thay thế gia trị ô đang chọn ";
            this.mnuReplaceCell.Click += new System.EventHandler(this.mnuReplaceCell_Click);
            // 
            // mnuReplaceColumn
            // 
            this.mnuReplaceColumn.Name = "mnuReplaceColumn";
            this.mnuReplaceColumn.Size = new System.Drawing.Size(314, 22);
            this.mnuReplaceColumn.Text = "Thay thế giá trị cột đang chọn";
            this.mnuReplaceColumn.Click += new System.EventHandler(this.mnuReplaceColumn_Click);
            // 
            // mnuReplaceAll
            // 
            this.mnuReplaceAll.Name = "mnuReplaceAll";
            this.mnuReplaceAll.Size = new System.Drawing.Size(314, 22);
            this.mnuReplaceAll.Text = "Thay thế giá trị tất cả các ô có giá trị tương tự";
            this.mnuReplaceAll.Click += new System.EventHandler(this.mnuReplaceAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 40);
            this.panel1.TabIndex = 2;
            // 
            // frmViewDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 418);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.panel1);
            this.Name = "frmViewDataSource";
            this.Text = "View DataSource";
            this.Load += new System.EventHandler(this.frmViewDataSource_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuReplaceCell;
        private System.Windows.Forms.ToolStripMenuItem mnuReplaceColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuReplaceAll;
        public System.Windows.Forms.DataGridView dtgData;
    }
}