namespace CommonLib.UserControls
{
    partial class frmVisibleColumns
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
            this.btnOK = new CommonLib.Buttons.btnOK();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdData = new CommonLib.UserControls.XtraGridExtend();
            this.viewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(159, 201);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdData);
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 195);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách các cột";
            // 
            // grdData
            // 
            this.grdData.ActiveCell = null;
            this.grdData.ActiveColumn = null;
            this.grdData.ActiveRow = null;
            this.grdData.ActiveRowInfo = null;
            this.grdData.AllowNullValidateCell = true;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AutoCenterHeaderText = true;
            this.grdData.CheckAllFieldName = "Check";
            colorColumn1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(236)))));
            colorColumn1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(212)))), ((int)(((byte)(242)))));
            this.grdData.ColorHeaderChooseColumn = colorColumn1;
            colorColumn2.Color = System.Drawing.SystemColors.ButtonFace;
            colorColumn2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.grdData.ColorUnEditableColumn = colorColumn2;
            this.grdData.ColumnNumberNotNegative = null;
            this.grdData.ColumnsChoose = null;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.GradientModeChooseColumn = CommonLib.UserControls.LinearGradientMode.Vertical;
            this.grdData.IsCheckChangedState = true;
            this.grdData.IsCheckValidateCell = false;
            this.grdData.IsCheckValidateRow = false;
            this.grdData.Location = new System.Drawing.Point(3, 17);
            this.grdData.MainView = this.viewData;
            this.grdData.Name = "grdData";
            this.grdData.ShowMenuChooseVisibleColumn = false;
            this.grdData.Size = new System.Drawing.Size(396, 175);
            this.grdData.SummaryGroupType = DevExpress.Data.SummaryItemType.None;
            this.grdData.TabIndex = 0;
            this.grdData.UseCheckAll = true;
            this.grdData.ValidateText = null;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewData});
            // 
            // viewData
            // 
            this.viewData.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.viewData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.viewData.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.viewData.Appearance.SelectedRow.Options.UseBackColor = true;
            this.viewData.GridControl = this.grdData;
            this.viewData.GroupFormat = "[#image]{1} {2}";
            this.viewData.Name = "viewData";
            this.viewData.OptionsMenu.EnableColumnMenu = false;
            this.viewData.OptionsMenu.EnableGroupPanelMenu = false;
            this.viewData.OptionsView.ShowGroupPanel = false;
            // 
            // frmVisibleColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 232);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.LookAndFeel.SkinName = "Money Twins";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmVisibleColumns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIS - Chon Cot Hien Thi";
            this.Load += new System.EventHandler(this.frmVisibleColumns_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonLib.Buttons.btnOK btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private XtraGridExtend grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView viewData;
    }
}