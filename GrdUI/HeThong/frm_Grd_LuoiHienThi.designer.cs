namespace GrdUI.HeThong
{
    partial class frm_Grd_LuoiHienThi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_LuoiHienThi));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlData = new DevExpress.XtraGrid.GridControl();
            this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEditNewRow = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEditMultiMode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemExit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCenterButton = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemDelete = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_excel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.cms_excel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_exportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_importExcel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditNewRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditMultiMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            this.cms_excel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.btn_excel);
            this.layoutControl.Controls.Add(this.btnDelete);
            this.layoutControl.Controls.Add(this.btnRefresh);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnExit);
            this.layoutControl.Controls.Add(this.gridControlData);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(601, 325);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // btnDelete
            // 
            this.btnDelete.ImageIndex = 5;
            this.btnDelete.ImageList = this.imageCollectionSmall;
            this.btnDelete.Location = new System.Drawing.Point(350, 291);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 22);
            this.btnDelete.StyleController = this.layoutControl;
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 3;
            this.btnRefresh.ImageList = this.imageCollectionSmall;
            this.btnRefresh.Location = new System.Drawing.Point(12, 291);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(26, 22);
            this.btnRefresh.StyleController = this.layoutControl;
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 4;
            this.btnSave.ImageList = this.imageCollectionSmall;
            this.btnSave.Location = new System.Drawing.Point(429, 291);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 22);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu dữ liệu";
            this.btnSave.Click += new System.EventHandler(this.btn_LuuDuLieu_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 2;
            this.btnExit.ImageList = this.imageCollectionSmall;
            this.btnExit.Location = new System.Drawing.Point(520, 291);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 22);
            this.btnExit.StyleController = this.layoutControl;
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // gridControlData
            // 
            this.gridControlData.Location = new System.Drawing.Point(12, 12);
            this.gridControlData.MainView = this.gridViewData;
            this.gridControlData.Name = "gridControlData";
            this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditNewRow,
            this.repositoryItemLookUpEditMultiMode});
            this.gridControlData.Size = new System.Drawing.Size(577, 275);
            this.gridControlData.TabIndex = 4;
            this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewData});
            // 
            // gridViewData
            // 
            this.gridViewData.GridControl = this.gridControlData;
            this.gridViewData.Name = "gridViewData";
            this.gridViewData.NewItemRowText = "Nhấn vào đây để thêm mới";
            this.gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridViewData.OptionsView.ShowAutoFilterRow = true;
            this.gridViewData.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEditNewRow
            // 
            this.repositoryItemLookUpEditNewRow.AutoHeight = false;
            this.repositoryItemLookUpEditNewRow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditNewRow.Name = "repositoryItemLookUpEditNewRow";
            // 
            // repositoryItemLookUpEditMultiMode
            // 
            this.repositoryItemLookUpEditMultiMode.AutoHeight = false;
            this.repositoryItemLookUpEditMultiMode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditMultiMode.Name = "repositoryItemLookUpEditMultiMode";
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid,
            this.layoutControlItemExit,
            this.layoutControlItemSave,
            this.layoutControlItemRefresh,
            this.emptySpaceItemCenterButton,
            this.layoutControlItemDelete,
            this.layoutControlItem1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(601, 325);
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlData;
            this.layoutControlItemGrid.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(581, 279);
            this.layoutControlItemGrid.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemExit
            // 
            this.layoutControlItemExit.Control = this.btnExit;
            this.layoutControlItemExit.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemExit.Location = new System.Drawing.Point(508, 279);
            this.layoutControlItemExit.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItemExit.MinSize = new System.Drawing.Size(73, 26);
            this.layoutControlItemExit.Name = "layoutControlItemExit";
            this.layoutControlItemExit.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItemExit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemExit.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemExit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemExit.TextVisible = false;
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItemSave.Location = new System.Drawing.Point(417, 279);
            this.layoutControlItemSave.MaxSize = new System.Drawing.Size(91, 26);
            this.layoutControlItemSave.MinSize = new System.Drawing.Size(91, 26);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(91, 26);
            this.layoutControlItemSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSave.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemRefresh
            // 
            this.layoutControlItemRefresh.Control = this.btnRefresh;
            this.layoutControlItemRefresh.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItemRefresh.Location = new System.Drawing.Point(0, 279);
            this.layoutControlItemRefresh.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.Name = "layoutControlItemRefresh";
            this.layoutControlItemRefresh.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemRefresh.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRefresh.TextVisible = false;
            // 
            // emptySpaceItemCenterButton
            // 
            this.emptySpaceItemCenterButton.AllowHotTrack = false;
            this.emptySpaceItemCenterButton.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItemCenterButton.Location = new System.Drawing.Point(96, 279);
            this.emptySpaceItemCenterButton.Name = "emptySpaceItemCenterButton";
            this.emptySpaceItemCenterButton.Size = new System.Drawing.Size(242, 26);
            this.emptySpaceItemCenterButton.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemDelete
            // 
            this.layoutControlItemDelete.Control = this.btnDelete;
            this.layoutControlItemDelete.Location = new System.Drawing.Point(338, 279);
            this.layoutControlItemDelete.MaxSize = new System.Drawing.Size(79, 26);
            this.layoutControlItemDelete.MinSize = new System.Drawing.Size(79, 26);
            this.layoutControlItemDelete.Name = "layoutControlItemDelete";
            this.layoutControlItemDelete.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItemDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemDelete.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDelete.TextVisible = false;
            // 
            // btn_excel
            // 
            this.btn_excel.ImageIndex = 1;
            this.btn_excel.ImageList = this.imageCollectionSmall;
            this.btn_excel.Location = new System.Drawing.Point(42, 291);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(62, 22);
            this.btn_excel.StyleController = this.layoutControl;
            this.btn_excel.TabIndex = 11;
            this.btn_excel.Text = "Excel";
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_excel;
            this.layoutControlItem1.Location = new System.Drawing.Point(30, 279);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(66, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // imageCollectionSmall
            // 
            this.imageCollectionSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionSmall.ImageStream")));
            this.imageCollectionSmall.Images.SetKeyName(0, "1306399919_application_get.png");
            this.imageCollectionSmall.Images.SetKeyName(1, "excel.png");
            this.imageCollectionSmall.Images.SetKeyName(2, "exit.png");
            this.imageCollectionSmall.Images.SetKeyName(3, "refresh.png");
            this.imageCollectionSmall.Images.SetKeyName(4, "save.png");
            this.imageCollectionSmall.Images.SetKeyName(5, "2-32.png");
            // 
            // cms_excel
            // 
            this.cms_excel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_exportExcel,
            this.mnu_importExcel});
            this.cms_excel.Name = "cms_excel";
            this.cms_excel.ShowImageMargin = false;
            this.cms_excel.Size = new System.Drawing.Size(128, 70);
            // 
            // mnu_exportExcel
            // 
            this.mnu_exportExcel.Name = "mnu_exportExcel";
            this.mnu_exportExcel.Size = new System.Drawing.Size(127, 22);
            this.mnu_exportExcel.Text = "Xuất excel";
            this.mnu_exportExcel.Click += new System.EventHandler(this.mnu_exportExcel_Click);
            // 
            // mnu_importExcel
            // 
            this.mnu_importExcel.Name = "mnu_importExcel";
            this.mnu_importExcel.Size = new System.Drawing.Size(127, 22);
            this.mnu_importExcel.Text = "Import excel";
            this.mnu_importExcel.Click += new System.EventHandler(this.mnu_importExcel_Click);
            // 
            // frm_Grd_LuoiHienThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 325);
            this.Controls.Add(this.layoutControl);
            this.Name = "frm_Grd_LuoiHienThi";
            this.Text = "UIS - Lưới hiển thị";
            this.Load += new System.EventHandler(this.frm_Grd_LuoiHienThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditNewRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditMultiMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            this.cms_excel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraGrid.GridControl gridControlData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCenterButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditNewRow;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditMultiMode;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDelete;
        private DevExpress.XtraEditors.SimpleButton btn_excel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
        private System.Windows.Forms.ContextMenuStrip cms_excel;
        private System.Windows.Forms.ToolStripMenuItem mnu_exportExcel;
        private System.Windows.Forms.ToolStripMenuItem mnu_importExcel;
    }
}