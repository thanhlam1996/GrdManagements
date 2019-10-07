namespace CommonLib.ImportAndExport
{
    partial class frmEditConstructImportEdit
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
            this.grcNameField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcTypeData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repDataType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new CommonLib.Buttons.pscButton();
            this.btnCloseNoImg1 = new CommonLib.Buttons.btnCloseNoImg();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDataType)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 236);
            this.panel1.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repDataType});
            this.grdData.Size = new System.Drawing.Size(473, 236);
            this.grdData.TabIndex = 0;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcNameField,
            this.grcTypeData});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // grcNameField
            // 
            this.grcNameField.Caption = "Tên Field";
            this.grcNameField.FieldName = "ColumnName";
            this.grcNameField.Name = "grcNameField";
            this.grcNameField.Visible = true;
            this.grcNameField.VisibleIndex = 0;
            this.grcNameField.Width = 311;
            // 
            // grcTypeData
            // 
            this.grcTypeData.Caption = "Kiểu dữ liệu";
            this.grcTypeData.ColumnEdit = this.repDataType;
            this.grcTypeData.FieldName = "TypeData";
            this.grcTypeData.Name = "grcTypeData";
            this.grcTypeData.Visible = true;
            this.grcTypeData.VisibleIndex = 1;
            this.grcTypeData.Width = 141;
            // 
            // repDataType
            // 
            this.repDataType.AutoHeight = false;
            this.repDataType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repDataType.Name = "repDataType";
            this.repDataType.NullText = "...Chọn kiểu...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Controls.Add(this.btnCloseNoImg1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnApply.Location = new System.Drawing.Point(8, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(108, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Cập nhật cấu trúc";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCloseNoImg1
            // 
            this.btnCloseNoImg1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseNoImg1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCloseNoImg1.Location = new System.Drawing.Point(380, 6);
            this.btnCloseNoImg1.Name = "btnCloseNoImg1";
            this.btnCloseNoImg1.Size = new System.Drawing.Size(81, 23);
            this.btnCloseNoImg1.TabIndex = 1;
            this.btnCloseNoImg1.Text = "Đóng";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cboTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(473, 44);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chọn bảng";
            // 
            // cboTable
            // 
            this.cboTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(77, 14);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(211, 21);
            this.cboTable.TabIndex = 2;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // frmEditConstructImportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 319);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "frmEditConstructImportEdit";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chỉnh sửa cấu trúc dữ liệu";
            this.Load += new System.EventHandler(this.frmEditConstructImportEdit_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDataType)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private CommonLib.Buttons.btnCloseNoImg btnCloseNoImg1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTable;
        private DevExpress.XtraGrid.Columns.GridColumn grcNameField;
        private DevExpress.XtraGrid.Columns.GridColumn grcTypeData;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repDataType;
        private CommonLib.Buttons.pscButton btnApply;
    }
}