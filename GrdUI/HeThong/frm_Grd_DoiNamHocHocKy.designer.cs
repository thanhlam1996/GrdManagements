namespace GrdUI.HeThong
{
    partial class frm_Grd_DoiNamHocHocKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_DoiNamHocHocKy));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.lkuHocKy = new DevExpress.XtraEditors.LookUpEdit();
            this.lkuNamHoc = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemNamHoc = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemHocKy = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnDongY = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemTop = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemLeft = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemRight = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemCenter = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkuHocKy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuNamHoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNamHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHocKy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnDongY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnThoat);
            this.layoutControlMain.Controls.Add(this.btnDongY);
            this.layoutControlMain.Controls.Add(this.lkuHocKy);
            this.layoutControlMain.Controls.Add(this.lkuNamHoc);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(444, 101);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 35;
            this.btnThoat.ImageList = this.imageCollectionSmall;
            this.btnThoat.Location = new System.Drawing.Point(342, 57);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(80, 22);
            this.btnThoat.StyleController = this.layoutControlMain;
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 40;
            this.btnDongY.ImageList = this.imageCollectionSmall;
            this.btnDongY.Location = new System.Drawing.Point(259, 57);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(79, 22);
            this.btnDongY.StyleController = this.layoutControlMain;
            this.btnDongY.TabIndex = 6;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btn_DongY_Click);
            // 
            // lkuHocKy
            // 
            this.lkuHocKy.Location = new System.Drawing.Point(277, 22);
            this.lkuHocKy.Name = "lkuHocKy";
            this.lkuHocKy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkuHocKy.Properties.NullText = "";
            this.lkuHocKy.Size = new System.Drawing.Size(145, 20);
            this.lkuHocKy.StyleController = this.layoutControlMain;
            this.lkuHocKy.TabIndex = 5;
            this.lkuHocKy.EditValueChanged += new System.EventHandler(this.lookUpEdit_Terms_EditValueChanged);
            // 
            // lkuNamHoc
            // 
            this.lkuNamHoc.Location = new System.Drawing.Point(66, 22);
            this.lkuNamHoc.Name = "lkuNamHoc";
            this.lkuNamHoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkuNamHoc.Properties.NullText = "";
            this.lkuNamHoc.Size = new System.Drawing.Size(163, 20);
            this.lkuNamHoc.StyleController = this.layoutControlMain;
            this.lkuNamHoc.TabIndex = 4;
            this.lkuNamHoc.EditValueChanged += new System.EventHandler(this.lookUpEdit_YearStudy_EditValueChanged);
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemNamHoc,
            this.layoutControlItemHocKy,
            this.layoutControlItemBtnDongY,
            this.emptySpaceItemBtn,
            this.layoutControlItemBtnThoat,
            this.emptySpaceItemTop,
            this.emptySpaceItemLeft,
            this.emptySpaceItemBottom,
            this.emptySpaceItemRight,
            this.emptySpaceItemCenter});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(444, 101);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemNamHoc
            // 
            this.layoutControlItemNamHoc.Control = this.lkuNamHoc;
            this.layoutControlItemNamHoc.CustomizationFormText = "Năm học";
            this.layoutControlItemNamHoc.Location = new System.Drawing.Point(10, 10);
            this.layoutControlItemNamHoc.Name = "layoutControlItemNamHoc";
            this.layoutControlItemNamHoc.Size = new System.Drawing.Size(211, 24);
            this.layoutControlItemNamHoc.Text = "Năm học";
            this.layoutControlItemNamHoc.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemNamHoc.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItemHocKy
            // 
            this.layoutControlItemHocKy.Control = this.lkuHocKy;
            this.layoutControlItemHocKy.CustomizationFormText = "  Học kỳ";
            this.layoutControlItemHocKy.Location = new System.Drawing.Point(221, 10);
            this.layoutControlItemHocKy.Name = "layoutControlItemHocKy";
            this.layoutControlItemHocKy.Size = new System.Drawing.Size(193, 24);
            this.layoutControlItemHocKy.Text = "  Học kỳ";
            this.layoutControlItemHocKy.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemHocKy.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItemBtnDongY
            // 
            this.layoutControlItemBtnDongY.Control = this.btnDongY;
            this.layoutControlItemBtnDongY.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItemBtnDongY.Location = new System.Drawing.Point(247, 45);
            this.layoutControlItemBtnDongY.MaxSize = new System.Drawing.Size(83, 26);
            this.layoutControlItemBtnDongY.MinSize = new System.Drawing.Size(83, 26);
            this.layoutControlItemBtnDongY.Name = "layoutControlItemBtnDongY";
            this.layoutControlItemBtnDongY.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItemBtnDongY.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnDongY.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemBtnDongY.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnDongY.TextVisible = false;
            // 
            // emptySpaceItemBtn
            // 
            this.emptySpaceItemBtn.AllowHotTrack = false;
            this.emptySpaceItemBtn.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItemBtn.Location = new System.Drawing.Point(10, 45);
            this.emptySpaceItemBtn.Name = "emptySpaceItemBtn";
            this.emptySpaceItemBtn.Size = new System.Drawing.Size(237, 26);
            this.emptySpaceItemBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemBtnThoat
            // 
            this.layoutControlItemBtnThoat.Control = this.btnThoat;
            this.layoutControlItemBtnThoat.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItemBtnThoat.Location = new System.Drawing.Point(330, 45);
            this.layoutControlItemBtnThoat.MaxSize = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnThoat.MinSize = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnThoat.Name = "layoutControlItemBtnThoat";
            this.layoutControlItemBtnThoat.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnThoat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnThoat.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnThoat.TextVisible = false;
            // 
            // emptySpaceItemTop
            // 
            this.emptySpaceItemTop.AllowHotTrack = false;
            this.emptySpaceItemTop.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItemTop.Location = new System.Drawing.Point(10, 0);
            this.emptySpaceItemTop.Name = "emptySpaceItemTop";
            this.emptySpaceItemTop.Size = new System.Drawing.Size(404, 10);
            this.emptySpaceItemTop.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemLeft
            // 
            this.emptySpaceItemLeft.AllowHotTrack = false;
            this.emptySpaceItemLeft.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItemLeft.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItemLeft.Name = "emptySpaceItemLeft";
            this.emptySpaceItemLeft.Size = new System.Drawing.Size(10, 81);
            this.emptySpaceItemLeft.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemBottom
            // 
            this.emptySpaceItemBottom.AllowHotTrack = false;
            this.emptySpaceItemBottom.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItemBottom.Location = new System.Drawing.Point(10, 71);
            this.emptySpaceItemBottom.Name = "emptySpaceItemBottom";
            this.emptySpaceItemBottom.Size = new System.Drawing.Size(404, 10);
            this.emptySpaceItemBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemRight
            // 
            this.emptySpaceItemRight.AllowHotTrack = false;
            this.emptySpaceItemRight.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItemRight.Location = new System.Drawing.Point(414, 0);
            this.emptySpaceItemRight.Name = "emptySpaceItemRight";
            this.emptySpaceItemRight.Size = new System.Drawing.Size(10, 81);
            this.emptySpaceItemRight.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemCenter
            // 
            this.emptySpaceItemCenter.AllowHotTrack = false;
            this.emptySpaceItemCenter.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItemCenter.Location = new System.Drawing.Point(10, 34);
            this.emptySpaceItemCenter.Name = "emptySpaceItemCenter";
            this.emptySpaceItemCenter.Size = new System.Drawing.Size(404, 11);
            this.emptySpaceItemCenter.TextSize = new System.Drawing.Size(0, 0);
            // 
            // imageCollectionSmall
            // 
            this.imageCollectionSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionSmall.ImageStream")));
            this.imageCollectionSmall.Images.SetKeyName(0, "CaThi.png");
            this.imageCollectionSmall.Images.SetKeyName(1, "1306308961_money_bag.png");
            this.imageCollectionSmall.Images.SetKeyName(2, "1306313151_Home.png");
            this.imageCollectionSmall.Images.SetKeyName(3, "Calendar.png");
            this.imageCollectionSmall.Images.SetKeyName(4, "HinhThucThi.png");
            this.imageCollectionSmall.Images.SetKeyName(5, "KyThi.png");
            this.imageCollectionSmall.Images.SetKeyName(6, "PhongThi.png");
            this.imageCollectionSmall.Images.SetKeyName(7, "QuanLyLopThi.png");
            this.imageCollectionSmall.Images.SetKeyName(8, "TaoCaThi.png");
            this.imageCollectionSmall.Images.SetKeyName(9, "ThoiGianThi.png");
            this.imageCollectionSmall.Images.SetKeyName(10, "she_users.png");
            this.imageCollectionSmall.Images.SetKeyName(11, "user_accept.png");
            this.imageCollectionSmall.Images.SetKeyName(12, "users.png");
            this.imageCollectionSmall.Images.SetKeyName(13, "1306330192_roomsbg9.png");
            this.imageCollectionSmall.Images.SetKeyName(14, "1306330283_new doc.png");
            this.imageCollectionSmall.Images.SetKeyName(15, "1306330315_contact-new.png");
            this.imageCollectionSmall.Images.SetKeyName(16, "1306330331_preferences-system-time.png");
            this.imageCollectionSmall.Images.SetKeyName(17, "1306330385_todo_list_add.png");
            this.imageCollectionSmall.Images.SetKeyName(18, "1306330283_new doc.png");
            this.imageCollectionSmall.Images.SetKeyName(19, "1306330385_todo_list_add.png");
            this.imageCollectionSmall.Images.SetKeyName(20, "1306330551_gtk-execute.png");
            this.imageCollectionSmall.Images.SetKeyName(21, "1306330590_List.png");
            this.imageCollectionSmall.Images.SetKeyName(22, "1306330602_Paper-pencil.png");
            this.imageCollectionSmall.Images.SetKeyName(23, "1306330623_Paper-add.png");
            this.imageCollectionSmall.Images.SetKeyName(24, "1306249192_Help.png");
            this.imageCollectionSmall.Images.SetKeyName(25, "1306399919_application_get.png");
            this.imageCollectionSmall.Images.SetKeyName(26, "1306470760_agt_update_drivers.png");
            this.imageCollectionSmall.Images.SetKeyName(27, "1306727802_Synchronize.png");
            this.imageCollectionSmall.Images.SetKeyName(28, "1306830734_print.png");
            this.imageCollectionSmall.Images.SetKeyName(29, "1306830750_print.png");
            this.imageCollectionSmall.Images.SetKeyName(30, "1306983835_check_box.png");
            this.imageCollectionSmall.Images.SetKeyName(31, "1307070374_list-accept.png");
            this.imageCollectionSmall.Images.SetKeyName(32, "1307377907_stock_page-total-number.png");
            this.imageCollectionSmall.Images.SetKeyName(33, "CaThi.png");
            this.imageCollectionSmall.Images.SetKeyName(34, "excel.png");
            this.imageCollectionSmall.Images.SetKeyName(35, "exit.png");
            this.imageCollectionSmall.Images.SetKeyName(36, "printer.png");
            this.imageCollectionSmall.Images.SetKeyName(37, "refresh.png");
            this.imageCollectionSmall.Images.SetKeyName(38, "save.png");
            this.imageCollectionSmall.Images.SetKeyName(39, "rss_remove.png");
            this.imageCollectionSmall.Images.SetKeyName(40, "accept.png");
            this.imageCollectionSmall.Images.SetKeyName(41, "add.png");
            this.imageCollectionSmall.Images.SetKeyName(42, "help.png");
            this.imageCollectionSmall.Images.SetKeyName(43, "info.png");
            this.imageCollectionSmall.Images.SetKeyName(44, "remove.png");
            this.imageCollectionSmall.Images.SetKeyName(45, "rss.png");
            this.imageCollectionSmall.Images.SetKeyName(46, "up.png");
            this.imageCollectionSmall.Images.SetKeyName(47, "1338365301_document-save-as.png");
            this.imageCollectionSmall.Images.SetKeyName(48, "1338365425_Calculator.png");
            this.imageCollectionSmall.Images.SetKeyName(49, "1338365461_Microsoft Office 2007 Excel.png");
            this.imageCollectionSmall.Images.SetKeyName(50, "1338368015_3floppy_unmount.png");
            this.imageCollectionSmall.Images.SetKeyName(51, "1338368069_001_52.gif");
            this.imageCollectionSmall.Images.SetKeyName(52, "1338368146_Documents_folder.png");
            this.imageCollectionSmall.Images.SetKeyName(53, "check.png");
            this.imageCollectionSmall.Images.SetKeyName(54, "unCheck.png");
            this.imageCollectionSmall.Images.SetKeyName(55, "1341797510_up.png");
            this.imageCollectionSmall.Images.SetKeyName(56, "1341797582_down.png");
            // 
            // frm_Grd_DoiNamHocHocKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 101);
            this.Controls.Add(this.layoutControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Grd_DoiNamHocHocKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIS - Đổi năm học, học kỳ";
            this.Load += new System.EventHandler(this.frm_Grd_DoiNamHocHocKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkuHocKy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkuNamHoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNamHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHocKy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnDongY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LookUpEdit lkuHocKy;
        private DevExpress.XtraEditors.LookUpEdit lkuNamHoc;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNamHoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHocKy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnDongY;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBottom;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemTop;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemLeft;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRight;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCenter;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
    }
}