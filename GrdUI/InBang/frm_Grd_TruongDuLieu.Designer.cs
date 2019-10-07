namespace GrdUI.InBang
{
    partial class frm_Grd_TruongDuLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_TruongDuLieu));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnXoaDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection();
            this.btnLuuDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlData = new DevExpress.XtraGrid.GridControl();
            this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnLuuDuLieu = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemXoaDuLieu = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemXoaDuLieu)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnXoaDuLieu);
            this.layoutControlMain.Controls.Add(this.btnLuuDuLieu);
            this.layoutControlMain.Controls.Add(this.btnRefresh);
            this.layoutControlMain.Controls.Add(this.btnThoat);
            this.layoutControlMain.Controls.Add(this.gridControlData);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(679, 418);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // btnXoaDuLieu
            // 
            this.btnXoaDuLieu.ImageIndex = 5;
            this.btnXoaDuLieu.ImageList = this.imageCollectionSmall;
            this.btnXoaDuLieu.Location = new System.Drawing.Point(393, 384);
            this.btnXoaDuLieu.Name = "btnXoaDuLieu";
            this.btnXoaDuLieu.Size = new System.Drawing.Size(99, 22);
            this.btnXoaDuLieu.StyleController = this.layoutControlMain;
            this.btnXoaDuLieu.TabIndex = 8;
            this.btnXoaDuLieu.Text = "Xóa dữ liệu";
            this.btnXoaDuLieu.Click += new System.EventHandler(this.btnXoaDuLieu_Click);
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
            // btnLuuDuLieu
            // 
            this.btnLuuDuLieu.ImageIndex = 4;
            this.btnLuuDuLieu.ImageList = this.imageCollectionSmall;
            this.btnLuuDuLieu.Location = new System.Drawing.Point(496, 384);
            this.btnLuuDuLieu.Name = "btnLuuDuLieu";
            this.btnLuuDuLieu.Size = new System.Drawing.Size(93, 22);
            this.btnLuuDuLieu.StyleController = this.layoutControlMain;
            this.btnLuuDuLieu.TabIndex = 7;
            this.btnLuuDuLieu.Text = "Lưu dữ liệu";
            this.btnLuuDuLieu.Click += new System.EventHandler(this.btnLuuDuLieu_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 3;
            this.btnRefresh.ImageList = this.imageCollectionSmall;
            this.btnRefresh.Location = new System.Drawing.Point(12, 384);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 22);
            this.btnRefresh.StyleController = this.layoutControlMain;
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 2;
            this.btnThoat.ImageList = this.imageCollectionSmall;
            this.btnThoat.Location = new System.Drawing.Point(593, 384);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 22);
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
            this.gridControlData.Size = new System.Drawing.Size(655, 368);
            this.gridControlData.TabIndex = 4;
            this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewData});
            // 
            // gridViewData
            // 
            this.gridViewData.GridControl = this.gridControlData;
            this.gridViewData.Name = "gridViewData";
            this.gridViewData.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid,
            this.layoutControlItemBtnThoat,
            this.layoutControlItemBtnRefresh,
            this.layoutControlItemBtnLuuDuLieu,
            this.emptySpaceItemBtn,
            this.layoutControlItemXoaDuLieu});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(679, 418);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlData;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(659, 372);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemBtnThoat
            // 
            this.layoutControlItemBtnThoat.Control = this.btnThoat;
            this.layoutControlItemBtnThoat.Location = new System.Drawing.Point(581, 372);
            this.layoutControlItemBtnThoat.MaxSize = new System.Drawing.Size(78, 26);
            this.layoutControlItemBtnThoat.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItemBtnThoat.Name = "layoutControlItemBtnThoat";
            this.layoutControlItemBtnThoat.Size = new System.Drawing.Size(78, 26);
            this.layoutControlItemBtnThoat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnThoat.TextVisible = false;
            // 
            // layoutControlItemBtnRefresh
            // 
            this.layoutControlItemBtnRefresh.Control = this.btnRefresh;
            this.layoutControlItemBtnRefresh.Location = new System.Drawing.Point(0, 372);
            this.layoutControlItemBtnRefresh.MaxSize = new System.Drawing.Size(29, 26);
            this.layoutControlItemBtnRefresh.MinSize = new System.Drawing.Size(29, 26);
            this.layoutControlItemBtnRefresh.Name = "layoutControlItemBtnRefresh";
            this.layoutControlItemBtnRefresh.Size = new System.Drawing.Size(29, 26);
            this.layoutControlItemBtnRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnRefresh.TextVisible = false;
            // 
            // layoutControlItemBtnLuuDuLieu
            // 
            this.layoutControlItemBtnLuuDuLieu.Control = this.btnLuuDuLieu;
            this.layoutControlItemBtnLuuDuLieu.Location = new System.Drawing.Point(484, 372);
            this.layoutControlItemBtnLuuDuLieu.MaxSize = new System.Drawing.Size(97, 26);
            this.layoutControlItemBtnLuuDuLieu.MinSize = new System.Drawing.Size(97, 26);
            this.layoutControlItemBtnLuuDuLieu.Name = "layoutControlItemBtnLuuDuLieu";
            this.layoutControlItemBtnLuuDuLieu.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItemBtnLuuDuLieu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnLuuDuLieu.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnLuuDuLieu.TextVisible = false;
            // 
            // emptySpaceItemBtn
            // 
            this.emptySpaceItemBtn.AllowHotTrack = false;
            this.emptySpaceItemBtn.Location = new System.Drawing.Point(29, 372);
            this.emptySpaceItemBtn.Name = "emptySpaceItemBtn";
            this.emptySpaceItemBtn.Size = new System.Drawing.Size(352, 26);
            this.emptySpaceItemBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemXoaDuLieu
            // 
            this.layoutControlItemXoaDuLieu.Control = this.btnXoaDuLieu;
            this.layoutControlItemXoaDuLieu.Location = new System.Drawing.Point(381, 372);
            this.layoutControlItemXoaDuLieu.MaxSize = new System.Drawing.Size(103, 26);
            this.layoutControlItemXoaDuLieu.MinSize = new System.Drawing.Size(103, 26);
            this.layoutControlItemXoaDuLieu.Name = "layoutControlItemXoaDuLieu";
            this.layoutControlItemXoaDuLieu.Size = new System.Drawing.Size(103, 26);
            this.layoutControlItemXoaDuLieu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemXoaDuLieu.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemXoaDuLieu.TextVisible = false;
            // 
            // frm_Grd_TruongDuLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 418);
            this.Controls.Add(this.layoutControlMain);
            this.Name = "frm_Grd_TruongDuLieu";
            this.Text = "UIS - Trường dữ liệu";
            this.Load += new System.EventHandler(this.frm_Grd_TruongDuLieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnLuuDuLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemXoaDuLieu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraEditors.SimpleButton btnLuuDuLieu;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraGrid.GridControl gridControlData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnRefresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnLuuDuLieu;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBtn;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
        private DevExpress.XtraEditors.SimpleButton btnXoaDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemXoaDuLieu;
    }
}