namespace GrdUI.PhoiBang
{
    partial class frm_Grd_CauHinhLoaiPhoi_Nganh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_CauHinhLoaiPhoi_Nganh));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefesh = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuuDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlData = new DevExpress.XtraGrid.GridControl();
            this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnLuuDuLieu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCenterBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemBtnExcel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_DanhMucLoaiPhoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnExcel);
            this.layoutControlMain.Controls.Add(this.btnRefesh);
            this.layoutControlMain.Controls.Add(this.btnLuuDuLieu);
            this.layoutControlMain.Controls.Add(this.btnThoat);
            this.layoutControlMain.Controls.Add(this.gridControlData);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(623, 444);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "1306399919_application_get.png");
            this.imageCollection.Images.SetKeyName(1, "excel.png");
            this.imageCollection.Images.SetKeyName(2, "exit.png");
            this.imageCollection.Images.SetKeyName(3, "refresh.png");
            this.imageCollection.Images.SetKeyName(4, "save.png");
            this.imageCollection.Images.SetKeyName(5, "2-32.png");
            // 
            // btnExcel
            // 
            this.btnExcel.ImageIndex = 1;
            this.btnExcel.ImageList = this.imageCollection;
            this.btnExcel.Location = new System.Drawing.Point(38, 410);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(59, 22);
            this.btnExcel.StyleController = this.layoutControlMain;
            this.btnExcel.TabIndex = 8;
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnRefesh
            // 
            this.btnRefesh.ImageIndex = 3;
            this.btnRefesh.ImageList = this.imageCollection;
            this.btnRefesh.Location = new System.Drawing.Point(12, 410);
            this.btnRefesh.Name = "btnRefesh";
            this.btnRefesh.Size = new System.Drawing.Size(22, 22);
            this.btnRefesh.StyleController = this.layoutControlMain;
            this.btnRefesh.TabIndex = 7;
            this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
            // 
            // btnLuuDuLieu
            // 
            this.btnLuuDuLieu.ImageIndex = 4;
            this.btnLuuDuLieu.ImageList = this.imageCollection;
            this.btnLuuDuLieu.Location = new System.Drawing.Point(452, 410);
            this.btnLuuDuLieu.Name = "btnLuuDuLieu";
            this.btnLuuDuLieu.Size = new System.Drawing.Size(90, 22);
            this.btnLuuDuLieu.StyleController = this.layoutControlMain;
            this.btnLuuDuLieu.TabIndex = 6;
            this.btnLuuDuLieu.Text = "Lưu dữ liệu";
            this.btnLuuDuLieu.Click += new System.EventHandler(this.btnLuuDuLieu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 2;
            this.btnThoat.ImageList = this.imageCollection;
            this.btnThoat.Location = new System.Drawing.Point(546, 410);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(65, 22);
            this.btnThoat.StyleController = this.layoutControlMain;
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // gridControlData
            // 
            this.gridControlData.Location = new System.Drawing.Point(12, 12);
            this.gridControlData.MainView = this.gridViewData;
            this.gridControlData.Name = "gridControlData";
            this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi});
            this.gridControlData.Size = new System.Drawing.Size(599, 394);
            this.gridControlData.TabIndex = 4;
            this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewData});
            // 
            // gridViewData
            // 
            this.gridViewData.GridControl = this.gridControlData;
            this.gridViewData.GroupPanelText = "Thả tiêu đề cột muốn nhóm vào đây";
            this.gridViewData.Name = "gridViewData";
            this.gridViewData.NewItemRowText = "Nhấn vào đây để thêm mới";
            this.gridViewData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            // 
            // repositoryItemLookUpEdit_DanhMucLoaiPhoi
            // 
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi.AutoHeight = false;
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi.EditFormat.FormatString = "d";
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemLookUpEdit_DanhMucLoaiPhoi.Name = "repositoryItemLookUpEdit_DanhMucLoaiPhoi";
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid,
            this.layoutControlItemBtnThoat,
            this.layoutControlItemBtnLuuDuLieu,
            this.layoutControlItemBtnRefresh,
            this.emptySpaceItemCenterBtn,
            this.layoutControlItemBtnExcel});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "Root";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(623, 444);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlData;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(603, 398);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemBtnThoat
            // 
            this.layoutControlItemBtnThoat.Control = this.btnThoat;
            this.layoutControlItemBtnThoat.Location = new System.Drawing.Point(534, 398);
            this.layoutControlItemBtnThoat.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnThoat.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnThoat.Name = "layoutControlItemBtnThoat";
            this.layoutControlItemBtnThoat.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItemBtnThoat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnThoat.TextVisible = false;
            // 
            // layoutControlItemBtnLuuDuLieu
            // 
            this.layoutControlItemBtnLuuDuLieu.Control = this.btnLuuDuLieu;
            this.layoutControlItemBtnLuuDuLieu.Location = new System.Drawing.Point(440, 398);
            this.layoutControlItemBtnLuuDuLieu.MaxSize = new System.Drawing.Size(94, 26);
            this.layoutControlItemBtnLuuDuLieu.MinSize = new System.Drawing.Size(94, 26);
            this.layoutControlItemBtnLuuDuLieu.Name = "layoutControlItemBtnLuuDuLieu";
            this.layoutControlItemBtnLuuDuLieu.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItemBtnLuuDuLieu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnLuuDuLieu.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnLuuDuLieu.TextVisible = false;
            // 
            // layoutControlItemBtnRefresh
            // 
            this.layoutControlItemBtnRefresh.Control = this.btnRefesh;
            this.layoutControlItemBtnRefresh.Location = new System.Drawing.Point(0, 398);
            this.layoutControlItemBtnRefresh.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItemBtnRefresh.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItemBtnRefresh.Name = "layoutControlItemBtnRefresh";
            this.layoutControlItemBtnRefresh.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItemBtnRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnRefresh.TextVisible = false;
            // 
            // emptySpaceItemCenterBtn
            // 
            this.emptySpaceItemCenterBtn.AllowHotTrack = false;
            this.emptySpaceItemCenterBtn.Location = new System.Drawing.Point(89, 398);
            this.emptySpaceItemCenterBtn.Name = "emptySpaceItemCenterBtn";
            this.emptySpaceItemCenterBtn.Size = new System.Drawing.Size(351, 26);
            this.emptySpaceItemCenterBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemBtnExcel
            // 
            this.layoutControlItemBtnExcel.Control = this.btnExcel;
            this.layoutControlItemBtnExcel.Location = new System.Drawing.Point(26, 398);
            this.layoutControlItemBtnExcel.MaxSize = new System.Drawing.Size(63, 26);
            this.layoutControlItemBtnExcel.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItemBtnExcel.Name = "layoutControlItemBtnExcel";
            this.layoutControlItemBtnExcel.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItemBtnExcel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnExcel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnExcel.TextVisible = false;
            // 
            // frm_Grd_CauHinhLoaiPhoi_Nganh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 444);
            this.Controls.Add(this.layoutControlMain);
            this.Name = "frm_Grd_CauHinhLoaiPhoi_Nganh";
            this.Text = "UIS - Cấu hình phôi - ngành ";
            this.Load += new System.EventHandler(this.frm_Grd_CauHinhLoaiPhoi_Nganh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_DanhMucLoaiPhoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnExcel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraEditors.SimpleButton btnRefesh;
        private DevExpress.XtraEditors.SimpleButton btnLuuDuLieu;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraGrid.GridControl gridControlData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnLuuDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCenterBtn;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnExcel;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit_DanhMucLoaiPhoi;
       
    }
}