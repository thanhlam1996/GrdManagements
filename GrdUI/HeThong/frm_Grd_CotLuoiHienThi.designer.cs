namespace GrdUI.HeThong
{
    partial class frm_Grd_CotLuoiHienThi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_CotLuoiHienThi));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEdit_luoiHienThi = new DevExpress.XtraEditors.LookUpEdit();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlData = new DevExpress.XtraGrid.GridControl();
            this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEditAlignHeader = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEditAlignData = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEditSummaryType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEditFixed = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemExit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCenterButton = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemGrids = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemRightFilter = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemDelete = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_luoiHienThi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlignHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlignData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditSummaryType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditFixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrids)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRightFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.btnDelete);
            this.layoutControl.Controls.Add(this.btnFilter);
            this.layoutControl.Controls.Add(this.lookUpEdit_luoiHienThi);
            this.layoutControl.Controls.Add(this.btnRefresh);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnExit);
            this.layoutControl.Controls.Add(this.gridControlData);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(602, 314);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // btnDelete
            // 
            this.btnDelete.ImageIndex = 4;
            this.btnDelete.ImageList = this.imageCollectionSmall;
            this.btnDelete.Location = new System.Drawing.Point(348, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 22);
            this.btnDelete.StyleController = this.layoutControl;
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.bbtnDelete_Click);
            // 
            // imageCollectionSmall
            // 
            this.imageCollectionSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionSmall.ImageStream")));
            this.imageCollectionSmall.Images.SetKeyName(0, "1306399919_application_get.png");
            this.imageCollectionSmall.Images.SetKeyName(1, "exit.png");
            this.imageCollectionSmall.Images.SetKeyName(2, "refresh.png");
            this.imageCollectionSmall.Images.SetKeyName(3, "save.png");
            this.imageCollectionSmall.Images.SetKeyName(4, "2-32.png");
            // 
            // btnFilter
            // 
            this.btnFilter.ImageIndex = 0;
            this.btnFilter.ImageList = this.imageCollectionSmall;
            this.btnFilter.Location = new System.Drawing.Point(332, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(89, 22);
            this.btnFilter.StyleController = this.layoutControl;
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Lọc dữ liệu";
            this.btnFilter.Click += new System.EventHandler(this.btn_LocDuLieu_Click);
            // 
            // lookUpEdit_luoiHienThi
            // 
            this.lookUpEdit_luoiHienThi.Location = new System.Drawing.Point(73, 12);
            this.lookUpEdit_luoiHienThi.Name = "lookUpEdit_luoiHienThi";
            this.lookUpEdit_luoiHienThi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_luoiHienThi.Properties.NullText = "";
            this.lookUpEdit_luoiHienThi.Size = new System.Drawing.Size(255, 20);
            this.lookUpEdit_luoiHienThi.StyleController = this.layoutControl;
            this.lookUpEdit_luoiHienThi.TabIndex = 8;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 2;
            this.btnRefresh.ImageList = this.imageCollectionSmall;
            this.btnRefresh.Location = new System.Drawing.Point(12, 280);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(26, 22);
            this.btnRefresh.StyleController = this.layoutControl;
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 3;
            this.btnSave.ImageList = this.imageCollectionSmall;
            this.btnSave.Location = new System.Drawing.Point(430, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 22);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu dữ liệu";
            this.btnSave.Click += new System.EventHandler(this.btn_LuuDuLieu_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 1;
            this.btnExit.ImageList = this.imageCollectionSmall;
            this.btnExit.Location = new System.Drawing.Point(528, 280);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(62, 22);
            this.btnExit.StyleController = this.layoutControl;
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // gridControlData
            // 
            this.gridControlData.Location = new System.Drawing.Point(12, 38);
            this.gridControlData.MainView = this.gridViewData;
            this.gridControlData.Name = "gridControlData";
            this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditAlignHeader,
            this.repositoryItemLookUpEditAlignData,
            this.repositoryItemLookUpEditSummaryType,
            this.repositoryItemLookUpEditFixed});
            this.gridControlData.Size = new System.Drawing.Size(578, 238);
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
            // repositoryItemLookUpEditAlignHeader
            // 
            this.repositoryItemLookUpEditAlignHeader.AutoHeight = false;
            this.repositoryItemLookUpEditAlignHeader.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditAlignHeader.Name = "repositoryItemLookUpEditAlignHeader";
            // 
            // repositoryItemLookUpEditAlignData
            // 
            this.repositoryItemLookUpEditAlignData.AutoHeight = false;
            this.repositoryItemLookUpEditAlignData.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditAlignData.Name = "repositoryItemLookUpEditAlignData";
            // 
            // repositoryItemLookUpEditSummaryType
            // 
            this.repositoryItemLookUpEditSummaryType.AutoHeight = false;
            this.repositoryItemLookUpEditSummaryType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditSummaryType.Name = "repositoryItemLookUpEditSummaryType";
            // 
            // repositoryItemLookUpEditFixed
            // 
            this.repositoryItemLookUpEditFixed.AutoHeight = false;
            this.repositoryItemLookUpEditFixed.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditFixed.Name = "repositoryItemLookUpEditFixed";
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
            this.layoutControlItemGrids,
            this.layoutControlItemFilter,
            this.emptySpaceItemRightFilter,
            this.layoutControlItemDelete});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(602, 314);
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlData;
            this.layoutControlItemGrid.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(582, 242);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemExit
            // 
            this.layoutControlItemExit.Control = this.btnExit;
            this.layoutControlItemExit.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemExit.Location = new System.Drawing.Point(516, 268);
            this.layoutControlItemExit.MaxSize = new System.Drawing.Size(66, 26);
            this.layoutControlItemExit.MinSize = new System.Drawing.Size(66, 26);
            this.layoutControlItemExit.Name = "layoutControlItemExit";
            this.layoutControlItemExit.Size = new System.Drawing.Size(66, 26);
            this.layoutControlItemExit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemExit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemExit.TextVisible = false;
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItemSave.Location = new System.Drawing.Point(418, 268);
            this.layoutControlItemSave.MaxSize = new System.Drawing.Size(98, 26);
            this.layoutControlItemSave.MinSize = new System.Drawing.Size(98, 26);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItemSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemRefresh
            // 
            this.layoutControlItemRefresh.Control = this.btnRefresh;
            this.layoutControlItemRefresh.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItemRefresh.Location = new System.Drawing.Point(0, 268);
            this.layoutControlItemRefresh.MaxSize = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.MinSize = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.Name = "layoutControlItemRefresh";
            this.layoutControlItemRefresh.Size = new System.Drawing.Size(30, 26);
            this.layoutControlItemRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRefresh.TextVisible = false;
            // 
            // emptySpaceItemCenterButton
            // 
            this.emptySpaceItemCenterButton.AllowHotTrack = false;
            this.emptySpaceItemCenterButton.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItemCenterButton.Location = new System.Drawing.Point(30, 268);
            this.emptySpaceItemCenterButton.Name = "emptySpaceItemCenterButton";
            this.emptySpaceItemCenterButton.Size = new System.Drawing.Size(306, 26);
            this.emptySpaceItemCenterButton.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemGrids
            // 
            this.layoutControlItemGrids.Control = this.lookUpEdit_luoiHienThi;
            this.layoutControlItemGrids.CustomizationFormText = "Chức năng";
            this.layoutControlItemGrids.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrids.MaxSize = new System.Drawing.Size(320, 26);
            this.layoutControlItemGrids.MinSize = new System.Drawing.Size(320, 26);
            this.layoutControlItemGrids.Name = "layoutControlItemGrids";
            this.layoutControlItemGrids.Size = new System.Drawing.Size(320, 26);
            this.layoutControlItemGrids.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrids.Text = "Lưới hiển thị";
            this.layoutControlItemGrids.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItemFilter
            // 
            this.layoutControlItemFilter.Control = this.btnFilter;
            this.layoutControlItemFilter.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItemFilter.Location = new System.Drawing.Point(320, 0);
            this.layoutControlItemFilter.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItemFilter.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItemFilter.Name = "layoutControlItemFilter";
            this.layoutControlItemFilter.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItemFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFilter.TextVisible = false;
            // 
            // emptySpaceItemRightFilter
            // 
            this.emptySpaceItemRightFilter.AllowHotTrack = false;
            this.emptySpaceItemRightFilter.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItemRightFilter.Location = new System.Drawing.Point(413, 0);
            this.emptySpaceItemRightFilter.Name = "emptySpaceItemRightFilter";
            this.emptySpaceItemRightFilter.Size = new System.Drawing.Size(169, 26);
            this.emptySpaceItemRightFilter.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemDelete
            // 
            this.layoutControlItemDelete.Control = this.btnDelete;
            this.layoutControlItemDelete.Location = new System.Drawing.Point(336, 268);
            this.layoutControlItemDelete.MaxSize = new System.Drawing.Size(82, 26);
            this.layoutControlItemDelete.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItemDelete.Name = "layoutControlItemDelete";
            this.layoutControlItemDelete.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItemDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemDelete.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDelete.TextVisible = false;
            // 
            // frm_Grd_CotLuoiHienThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 314);
            this.Controls.Add(this.layoutControl);
            this.Name = "frm_Grd_CotLuoiHienThi";
            this.Text = "UIS - Các cột hiển thị trên lưới";
            this.Load += new System.EventHandler(this.frm_Grd_CotLuoiHienThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_luoiHienThi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlignHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditAlignData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditSummaryType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditFixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrids)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRightFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_luoiHienThi;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraGrid.GridControl gridControlData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCenterButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrids;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRightFilter;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditAlignHeader;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditAlignData;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditSummaryType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditFixed;
    }
}