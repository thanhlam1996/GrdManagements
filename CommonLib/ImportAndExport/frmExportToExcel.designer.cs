namespace CommonLib.ImportAndExport
{
    partial class frmExportToExcel
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
            CommonLib.UserControls.ColorColumn colorColumn1 = new CommonLib.UserControls.ColorColumn();
            CommonLib.UserControls.ColorColumn colorColumn2 = new CommonLib.UserControls.ColorColumn();
            this.grpListOfTable = new System.Windows.Forms.GroupBox();
            this.grdTable = new CommonLib.UserControls.XtraGridExtend();
            this.viewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.trvColumnView = new System.Windows.Forms.TreeView();
            this.chkIncludeHeader = new DevExpress.XtraEditors.CheckEdit();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.lblProcess = new DevExpress.XtraEditors.LabelControl();
            this.sDlgSave = new System.Windows.Forms.SaveFileDialog();
            this.lblPercent = new DevExpress.XtraEditors.LabelControl();
            this.chkShowProcess = new DevExpress.XtraEditors.CheckEdit();
            this.grpListOfTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowProcess.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpListOfTable
            // 
            this.grpListOfTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListOfTable.BackColor = System.Drawing.Color.Transparent;
            this.grpListOfTable.Controls.Add(this.grdTable);
            this.grpListOfTable.Controls.Add(this.trvColumnView);
            this.grpListOfTable.Location = new System.Drawing.Point(2, 0);
            this.grpListOfTable.Name = "grpListOfTable";
            this.grpListOfTable.Size = new System.Drawing.Size(455, 224);
            this.grpListOfTable.TabIndex = 0;
            this.grpListOfTable.TabStop = false;
            this.grpListOfTable.Text = "Danh sách các bảng";
            // 
            // grdTable
            // 
            this.grdTable.ActiveCell = null;
            this.grdTable.ActiveColumn = null;
            this.grdTable.ActiveRow = null;
            this.grdTable.ActiveRowInfo = null;
            this.grdTable.AllowNullValidateCell = true;
            this.grdTable.AllowUserToDeleteRows = false;
            this.grdTable.AutoCenterHeaderText = true;
            this.grdTable.CheckAllFieldName = "Check";
            colorColumn1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(236)))));
            colorColumn1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(212)))), ((int)(((byte)(242)))));
            this.grdTable.ColorHeaderChooseColumn = colorColumn1;
            colorColumn2.Color = System.Drawing.SystemColors.ButtonFace;
            colorColumn2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.grdTable.ColorUnEditableColumn = colorColumn2;
            this.grdTable.ColumnNumberNotNegative = null;
            this.grdTable.ColumnsChoose = null;
            this.grdTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTable.GradientModeChooseColumn = CommonLib.UserControls.LinearGradientMode.Vertical;
            this.grdTable.IsCheckChangedState = true;
            this.grdTable.IsCheckValidateCell = false;
            this.grdTable.IsCheckValidateRow = false;
            this.grdTable.Location = new System.Drawing.Point(3, 17);
            this.grdTable.MainView = this.viewData;
            this.grdTable.Name = "grdTable";
            this.grdTable.ShowMenuChooseVisibleColumn = false;
            this.grdTable.Size = new System.Drawing.Size(449, 204);
            this.grdTable.SummaryGroupType = DevExpress.Data.SummaryItemType.None;
            this.grdTable.TabIndex = 7;
            this.grdTable.UseCheckAll = true;
            this.grdTable.ValidateText = null;
            this.grdTable.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewData});
            // 
            // viewData
            // 
            this.viewData.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.viewData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.viewData.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.viewData.Appearance.SelectedRow.Options.UseBackColor = true;
            this.viewData.GridControl = this.grdTable;
            this.viewData.GroupFormat = "[#image]{1} {2}";
            this.viewData.Name = "viewData";
            this.viewData.OptionsMenu.EnableColumnMenu = false;
            this.viewData.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewData.OptionsView.ShowGroupPanel = false;
            // 
            // trvColumnView
            // 
            this.trvColumnView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvColumnView.CheckBoxes = true;
            this.trvColumnView.Location = new System.Drawing.Point(3, 20);
            this.trvColumnView.Name = "trvColumnView";
            this.trvColumnView.Size = new System.Drawing.Size(449, 200);
            this.trvColumnView.TabIndex = 6;
            this.trvColumnView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvColumnView_AfterCheck);
            // 
            // chkIncludeHeader
            // 
            this.chkIncludeHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIncludeHeader.EditValue = true;
            this.chkIncludeHeader.Location = new System.Drawing.Point(4, 230);
            this.chkIncludeHeader.Name = "chkIncludeHeader";
            this.chkIncludeHeader.Properties.Caption = "Xuất tiêu đề cột";
            this.chkIncludeHeader.Size = new System.Drawing.Size(101, 18);
            this.chkIncludeHeader.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.Location = new System.Drawing.Point(160, 250);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Trở lại";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(241, 250);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Tiếp";
            this.btnNext.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblProcess.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblProcess.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.Appearance.Options.UseBackColor = true;
            this.lblProcess.Appearance.Options.UseFont = true;
            this.lblProcess.Appearance.Options.UseTextOptions = true;
            this.lblProcess.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblProcess.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblProcess.Location = new System.Drawing.Point(137, 98);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(229, 32);
            this.lblProcess.TabIndex = 5;
            this.lblProcess.Text = "Quá trình xuất dữ liệu đang thực hiện...\r\nVui lòng đợi...";
            this.lblProcess.Visible = false;
            // 
            // sDlgSave
            // 
            this.sDlgSave.FileName = "Data";
            this.sDlgSave.Filter = "Excel File|*.xls|FoxPro file|*.dbf";
            this.sDlgSave.FilterIndex = 0;
            this.sDlgSave.OverwritePrompt = false;
            this.sDlgSave.RestoreDirectory = true;
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercent.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblPercent.Appearance.Options.UseBackColor = true;
            this.lblPercent.Appearance.Options.UseTextOptions = true;
            this.lblPercent.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblPercent.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblPercent.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPercent.Location = new System.Drawing.Point(6, 135);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(448, 33);
            this.lblPercent.TabIndex = 7;
            this.lblPercent.Visible = false;
            // 
            // chkShowProcess
            // 
            this.chkShowProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowProcess.EditValue = true;
            this.chkShowProcess.Location = new System.Drawing.Point(4, 254);
            this.chkShowProcess.Name = "chkShowProcess";
            this.chkShowProcess.Properties.Caption = "Hiển thị tiến trình";
            this.chkShowProcess.Size = new System.Drawing.Size(101, 18);
            this.chkShowProcess.TabIndex = 8;
            // 
            // frmExportToExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 279);
            this.Controls.Add(this.chkShowProcess);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.chkIncludeHeader);
            this.Controls.Add(this.grpListOfTable);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblProcess);
            this.LookAndFeel.SkinName = "Money Twins";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MinimumSize = new System.Drawing.Size(381, 208);
            this.Name = "frmExportToExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIS - Xuat Ra Excel";
            this.Load += new System.EventHandler(this.frmExportToExcel_Load);
            this.grpListOfTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowProcess.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpListOfTable;
        private DevExpress.XtraEditors.CheckEdit chkIncludeHeader;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.LabelControl lblProcess;
        private System.Windows.Forms.TreeView trvColumnView;
        private System.Windows.Forms.SaveFileDialog sDlgSave;
        private DevExpress.XtraEditors.LabelControl lblPercent;
        private CommonLib.UserControls.XtraGridExtend grdTable;
        private DevExpress.XtraGrid.Views.Grid.GridView viewData;
        private DevExpress.XtraEditors.CheckEdit chkShowProcess;
    }
}