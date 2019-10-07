namespace GrdUI.PhoiBang
{
    partial class frm_Grd_DanhMucDotCapPhoiBang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_DanhMucDotCapPhoiBang));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnXoaDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefesh = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuuDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlData = new DevExpress.XtraGrid.GridControl();
            this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEdit_ReciviedDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemLookUpEdit_LoaiPhoi = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnLuuDuLieu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCenterBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemBtnExcel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItembtnXoaDuLieu = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateTime_ReciviedDate = new DevExpress.XtraEditors.DateTimeChartRangeControlClient();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_ReciviedDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_LoaiPhoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItembtnXoaDuLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTime_ReciviedDate)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnXoaDuLieu);
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
            // btnXoaDuLieu
            // 
            this.btnXoaDuLieu.ImageIndex = 5;
            this.btnXoaDuLieu.ImageList = this.imageCollection;
            this.btnXoaDuLieu.Location = new System.Drawing.Point(359, 410);
            this.btnXoaDuLieu.Name = "btnXoaDuLieu";
            this.btnXoaDuLieu.Size = new System.Drawing.Size(89, 22);
            this.btnXoaDuLieu.StyleController = this.layoutControlMain;
            this.btnXoaDuLieu.TabIndex = 7;
            this.btnXoaDuLieu.Text = "Xóa dữ liệu";
            this.btnXoaDuLieu.Click += new System.EventHandler(this.btnXoaDuLieu_Click);
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
            this.repositoryItemLookUpEdit_ReciviedDate,
            this.repositoryItemTextEdit1,
            this.repositoryItemLookUpEdit_LoaiPhoi});
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
            this.gridViewData.OptionsView.ShowAutoFilterRow = true;
            this.gridViewData.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEdit_ReciviedDate
            // 
            this.repositoryItemLookUpEdit_ReciviedDate.AutoHeight = false;
            this.repositoryItemLookUpEdit_ReciviedDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties.DisplayFormat.FormatString = "u";
            this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties.EditFormat.FormatString = "u";
            this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties.Mask.EditMask = "u";
            this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemLookUpEdit_ReciviedDate.EditFormat.FormatString = "u";
            this.repositoryItemLookUpEdit_ReciviedDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemLookUpEdit_ReciviedDate.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemLookUpEdit_ReciviedDate.Name = "repositoryItemLookUpEdit_ReciviedDate";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemLookUpEdit_LoaiPhoi
            // 
            this.repositoryItemLookUpEdit_LoaiPhoi.AutoHeight = false;
            this.repositoryItemLookUpEdit_LoaiPhoi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_LoaiPhoi.Name = "repositoryItemLookUpEdit_LoaiPhoi";
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
            this.layoutControlItemBtnExcel,
            this.layoutControlItembtnXoaDuLieu});
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
            this.emptySpaceItemCenterBtn.Size = new System.Drawing.Size(258, 26);
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
            // layoutControlItembtnXoaDuLieu
            // 
            this.layoutControlItembtnXoaDuLieu.Control = this.btnXoaDuLieu;
            this.layoutControlItembtnXoaDuLieu.Location = new System.Drawing.Point(347, 398);
            this.layoutControlItembtnXoaDuLieu.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItembtnXoaDuLieu.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItembtnXoaDuLieu.Name = "layoutControlItembtnXoaDuLieu";
            this.layoutControlItembtnXoaDuLieu.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItembtnXoaDuLieu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItembtnXoaDuLieu.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItembtnXoaDuLieu.TextVisible = false;
            // 
            // frm_Grd_DanhMucDotCapPhoiBang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 444);
            this.Controls.Add(this.layoutControlMain);
            this.Name = "frm_Grd_DanhMucDotCapPhoiBang";
            this.Text = "UIS - Danh mục đợt cấp phôi bằng";
            this.Load += new System.EventHandler(this.frm_Grd_DanhMucDotCapPhoiBang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_ReciviedDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_ReciviedDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_LoaiPhoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenterBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItembtnXoaDuLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTime_ReciviedDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraEditors.SimpleButton btnRefesh;
        private DevExpress.XtraEditors.SimpleButton btnLuuDuLieu;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraGrid.GridControl gridControlData;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnLuuDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnRefresh;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCenterBtn;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnExcel;
        private DevExpress.XtraEditors.SimpleButton btnXoaDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItembtnXoaDuLieu;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemLookUpEdit_ReciviedDate;
        private DevExpress.XtraEditors.DateTimeChartRangeControlClient dateTime_ReciviedDate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit_LoaiPhoi;
    }
}