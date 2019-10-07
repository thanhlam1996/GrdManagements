namespace GrdUI
{
    partial class frm_Grd_ConnectDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_ConnectDatabase));
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.txtDatabase2 = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtServerName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lkuAuthenticationType = new DevExpress.XtraEditors.LookUpEdit();
            this.lkuProvider = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDatabase1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemServerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUserName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDB1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnOK = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemDB2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemProvider = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAuthType = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuAuthenticationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuProvider.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuthType)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "check-circle-green-16.png");
            this.imageCollection.Images.SetKeyName(1, "Log Out.png");
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 357F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.layoutControlMain, 1, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(516, 336);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.txtDatabase2);
            this.layoutControlMain.Controls.Add(this.btnClose);
            this.layoutControlMain.Controls.Add(this.btnOK);
            this.layoutControlMain.Controls.Add(this.txtServerName);
            this.layoutControlMain.Controls.Add(this.txtUserName);
            this.layoutControlMain.Controls.Add(this.txtPassword);
            this.layoutControlMain.Controls.Add(this.lkuAuthenticationType);
            this.layoutControlMain.Controls.Add(this.lkuProvider);
            this.layoutControlMain.Controls.Add(this.txtDatabase1);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(82, 61);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(351, 214);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // txtDatabase2
            // 
            this.txtDatabase2.Location = new System.Drawing.Point(80, 156);
            this.txtDatabase2.Name = "txtDatabase2";
            this.txtDatabase2.Size = new System.Drawing.Size(259, 20);
            this.txtDatabase2.StyleController = this.layoutControlMain;
            this.txtDatabase2.TabIndex = 27;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 1;
            this.btnClose.ImageList = this.imageCollection;
            this.btnClose.Location = new System.Drawing.Point(274, 180);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 22);
            this.btnClose.StyleController = this.layoutControlMain;
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Thoát";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.ImageIndex = 0;
            this.btnOK.ImageList = this.imageCollection;
            this.btnOK.Location = new System.Drawing.Point(190, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 22);
            this.btnOK.StyleController = this.layoutControlMain;
            this.btnOK.TabIndex = 32;
            this.btnOK.Text = "Cấu hình";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(80, 36);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(259, 20);
            this.txtServerName.StyleController = this.layoutControlMain;
            this.txtServerName.TabIndex = 31;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(80, 84);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(259, 20);
            this.txtUserName.StyleController = this.layoutControlMain;
            this.txtUserName.TabIndex = 30;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(80, 108);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(259, 20);
            this.txtPassword.StyleController = this.layoutControlMain;
            this.txtPassword.TabIndex = 28;
            // 
            // lkuAuthenticationType
            // 
            this.lkuAuthenticationType.Location = new System.Drawing.Point(80, 60);
            this.lkuAuthenticationType.Name = "lkuAuthenticationType";
            this.lkuAuthenticationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkuAuthenticationType.Properties.NullText = "";
            this.lkuAuthenticationType.Size = new System.Drawing.Size(259, 20);
            this.lkuAuthenticationType.StyleController = this.layoutControlMain;
            this.lkuAuthenticationType.TabIndex = 25;
            this.lkuAuthenticationType.EditValueChanged += new System.EventHandler(this.lkuAuthenticationType_EditValueChanged);
            // 
            // lkuProvider
            // 
            this.lkuProvider.Location = new System.Drawing.Point(80, 12);
            this.lkuProvider.Name = "lkuProvider";
            this.lkuProvider.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkuProvider.Properties.NullText = "";
            this.lkuProvider.Properties.ReadOnly = true;
            this.lkuProvider.Size = new System.Drawing.Size(259, 20);
            this.lkuProvider.StyleController = this.layoutControlMain;
            this.lkuProvider.TabIndex = 24;
            this.lkuProvider.EditValueChanged += new System.EventHandler(this.lkuProvider_EditValueChanged);
            // 
            // txtDatabase1
            // 
            this.txtDatabase1.Location = new System.Drawing.Point(80, 132);
            this.txtDatabase1.Name = "txtDatabase1";
            this.txtDatabase1.Size = new System.Drawing.Size(259, 20);
            this.txtDatabase1.StyleController = this.layoutControlMain;
            this.txtDatabase1.TabIndex = 26;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemServerName,
            this.layoutControlItemUserName,
            this.layoutControlItemPassword,
            this.layoutControlItemDB1,
            this.layoutControlItemBtnOK,
            this.layoutControlItemBtnClose,
            this.emptySpaceItemBtn,
            this.layoutControlItemDB2,
            this.layoutControlItemProvider,
            this.layoutControlItemAuthType});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(351, 214);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemServerName
            // 
            this.layoutControlItemServerName.Control = this.txtServerName;
            this.layoutControlItemServerName.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemServerName.Name = "layoutControlItemServerName";
            this.layoutControlItemServerName.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemServerName.Text = "Server name:";
            this.layoutControlItemServerName.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemUserName
            // 
            this.layoutControlItemUserName.Control = this.txtUserName;
            this.layoutControlItemUserName.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemUserName.Name = "layoutControlItemUserName";
            this.layoutControlItemUserName.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemUserName.Text = "User:";
            this.layoutControlItemUserName.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemPassword.Text = "Pass:";
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemDB1
            // 
            this.layoutControlItemDB1.Control = this.txtDatabase1;
            this.layoutControlItemDB1.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemDB1.Name = "layoutControlItemDB1";
            this.layoutControlItemDB1.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemDB1.Text = "DB1:";
            this.layoutControlItemDB1.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemBtnOK
            // 
            this.layoutControlItemBtnOK.Control = this.btnOK;
            this.layoutControlItemBtnOK.Location = new System.Drawing.Point(178, 168);
            this.layoutControlItemBtnOK.MaxSize = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnOK.MinSize = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnOK.Name = "layoutControlItemBtnOK";
            this.layoutControlItemBtnOK.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnOK.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnOK.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnOK.TextVisible = false;
            // 
            // layoutControlItemBtnClose
            // 
            this.layoutControlItemBtnClose.Control = this.btnClose;
            this.layoutControlItemBtnClose.Location = new System.Drawing.Point(262, 168);
            this.layoutControlItemBtnClose.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnClose.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnClose.Name = "layoutControlItemBtnClose";
            this.layoutControlItemBtnClose.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnClose.TextVisible = false;
            // 
            // emptySpaceItemBtn
            // 
            this.emptySpaceItemBtn.AllowHotTrack = false;
            this.emptySpaceItemBtn.Location = new System.Drawing.Point(0, 168);
            this.emptySpaceItemBtn.Name = "emptySpaceItemBtn";
            this.emptySpaceItemBtn.Size = new System.Drawing.Size(178, 26);
            this.emptySpaceItemBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemDB2
            // 
            this.layoutControlItemDB2.Control = this.txtDatabase2;
            this.layoutControlItemDB2.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemDB2.Name = "layoutControlItemDB2";
            this.layoutControlItemDB2.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemDB2.Text = "DB2:";
            this.layoutControlItemDB2.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemProvider
            // 
            this.layoutControlItemProvider.Control = this.lkuProvider;
            this.layoutControlItemProvider.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemProvider.Name = "layoutControlItemProvider";
            this.layoutControlItemProvider.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemProvider.Text = "Provider:";
            this.layoutControlItemProvider.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItemAuthType
            // 
            this.layoutControlItemAuthType.Control = this.lkuAuthenticationType;
            this.layoutControlItemAuthType.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemAuthType.Name = "layoutControlItemAuthType";
            this.layoutControlItemAuthType.Size = new System.Drawing.Size(331, 24);
            this.layoutControlItemAuthType.Text = "Auth Type:";
            this.layoutControlItemAuthType.TextSize = new System.Drawing.Size(65, 13);
            // 
            // frm_Grd_ConnectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 336);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Grd_ConnectDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Grd_ConnectDatabase";
            this.Load += new System.EventHandler(this.frm_Grd_ConnectDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuAuthenticationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuProvider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuthType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraEditors.TextEdit txtDatabase2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtServerName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LookUpEdit lkuAuthenticationType;
        private DevExpress.XtraEditors.LookUpEdit lkuProvider;
        private DevExpress.XtraEditors.TextEdit txtDatabase1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemServerName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUserName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDB1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDB2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemProvider;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAuthType;
    }
}