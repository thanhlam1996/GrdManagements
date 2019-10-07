namespace CommonLib
{
    partial class frmQuickType
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
            CommonLib.UserControls.ColorColumn colorColumn1 = new CommonLib.UserControls.ColorColumn();
            CommonLib.UserControls.ColorColumn colorColumn2 = new CommonLib.UserControls.ColorColumn();
            this.btnSave = new CommonLib.Buttons.pscButton();
            this.btnClose = new CommonLib.Buttons.pscButton();
            this.grdData = new CommonLib.UserControls.XtraGridExtend();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(215, 235);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseTextOptions = true;
            this.btnClose.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnClose.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Show;
            this.btnClose.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(476, 245);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grdData
            // 
            this.grdData.ActiveCell = null;
            this.grdData.ActiveColumn = null;
            this.grdData.ActiveRow = null;
            this.grdData.ActiveRowInfo = null;
            this.grdData.AllowNullValidateCell = true;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.AutoCenterHeaderText = true;
            this.grdData.CheckAllFieldName = null;
            colorColumn1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(192)))), ((int)(((byte)(236)))));
            colorColumn1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(212)))), ((int)(((byte)(242)))));
            this.grdData.ColorHeaderChooseColumn = colorColumn1;
            colorColumn2.Color = System.Drawing.SystemColors.ButtonFace;
            colorColumn2.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.grdData.ColorUnEditableColumn = colorColumn2;
            this.grdData.ColumnNumberNotNegative = null;
            this.grdData.ColumnsChoose = null;
            this.grdData.GradientModeChooseColumn = CommonLib.UserControls.LinearGradientMode.Vertical;
            this.grdData.IsCheckChangedState = true;
            this.grdData.IsCheckValidateCell = false;
            this.grdData.IsCheckValidateRow = false;
            this.grdData.Location = new System.Drawing.Point(6, 6);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.ShowMenuChooseVisibleColumn = false;
            this.grdData.Size = new System.Drawing.Size(483, 223);
            this.grdData.SummaryGroupType = DevExpress.Data.SummaryItemType.None;
            this.grdData.TabIndex = 2;
            this.grdData.UseCheckAll = false;
            this.grdData.ValidateText = null;
            this.grdData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdData_MouseDoubleClick);
            // 
            // grvData
            // 
            this.grvData.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.grvData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvData.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.grvData.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupFormat = "[#image]{1} {2}";
            this.grvData.Name = "grvData";
            this.grvData.OptionsMenu.EnableColumnMenu = false;
            this.grvData.OptionsMenu.EnableGroupPanelMenu = false;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmQuickType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 264);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.KeyPreview = true;
            this.Name = "frmQuickType";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "frmQuickType";
            this.Deactivate += new System.EventHandler(this.frmQuickType_Deactivate);
            this.Activated += new System.EventHandler(this.frmQuickType_Activated);
            this.VisibleChanged += new System.EventHandler(this.frmQuickType_VisibleChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmQuickType_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CommonLib.Buttons.pscButton btnSave;
        private CommonLib.Buttons.pscButton btnClose;
        private CommonLib.UserControls.XtraGridExtend grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;

    }
}