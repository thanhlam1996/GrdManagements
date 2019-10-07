namespace GrdUI.HeThong
{
    partial class frm_Grd_DoiMatKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_DoiMatKhau));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.txtXacNhanMatKhau = new DevExpress.XtraEditors.TextEdit();
            this.txtMatKhauMoi = new DevExpress.XtraEditors.TextEdit();
            this.txtMatKhauCu = new DevExpress.XtraEditors.TextEdit();
            this.txtNguoiDung = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemNguoiDung = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMatKhauCu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMatKhauMoi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemXacNhanMatKhau = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnDongY = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXacNhanMatKhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhauMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhauCu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNguoiDung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMatKhauCu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMatKhauMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemXacNhanMatKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnDongY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 373F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.layoutControlMain, 1, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(569, 258);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnThoat);
            this.layoutControlMain.Controls.Add(this.btnDongY);
            this.layoutControlMain.Controls.Add(this.txtXacNhanMatKhau);
            this.layoutControlMain.Controls.Add(this.txtMatKhauMoi);
            this.layoutControlMain.Controls.Add(this.txtMatKhauCu);
            this.layoutControlMain.Controls.Add(this.txtNguoiDung);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(101, 58);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(367, 142);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 35;
            this.btnThoat.ImageList = this.imageCollectionSmall;
            this.btnThoat.Location = new System.Drawing.Point(279, 108);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(76, 22);
            this.btnThoat.StyleController = this.layoutControlMain;
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
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
            // btnDongY
            // 
            this.btnDongY.ImageIndex = 40;
            this.btnDongY.ImageList = this.imageCollectionSmall;
            this.btnDongY.Location = new System.Drawing.Point(199, 108);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(76, 22);
            this.btnDongY.StyleController = this.layoutControlMain;
            this.btnDongY.TabIndex = 8;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(119, 84);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.Properties.PasswordChar = '*';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(236, 20);
            this.txtXacNhanMatKhau.StyleController = this.layoutControlMain;
            this.txtXacNhanMatKhau.TabIndex = 7;
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(119, 60);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Properties.PasswordChar = '*';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(236, 20);
            this.txtMatKhauMoi.StyleController = this.layoutControlMain;
            this.txtMatKhauMoi.TabIndex = 6;
            // 
            // txtMatKhauCu
            // 
            this.txtMatKhauCu.Location = new System.Drawing.Point(119, 36);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.Properties.PasswordChar = '*';
            this.txtMatKhauCu.Size = new System.Drawing.Size(236, 20);
            this.txtMatKhauCu.StyleController = this.layoutControlMain;
            this.txtMatKhauCu.TabIndex = 5;
            // 
            // txtNguoiDung
            // 
            this.txtNguoiDung.Location = new System.Drawing.Point(119, 12);
            this.txtNguoiDung.Name = "txtNguoiDung";
            this.txtNguoiDung.Properties.ReadOnly = true;
            this.txtNguoiDung.Size = new System.Drawing.Size(236, 20);
            this.txtNguoiDung.StyleController = this.layoutControlMain;
            this.txtNguoiDung.TabIndex = 4;
            this.txtNguoiDung.TabStop = false;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemNguoiDung,
            this.layoutControlItemMatKhauCu,
            this.layoutControlItemMatKhauMoi,
            this.layoutControlItemXacNhanMatKhau,
            this.layoutControlItemBtnDongY,
            this.layoutControlItemBtnThoat,
            this.emptySpaceItemBtn});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(367, 142);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemNguoiDung
            // 
            this.layoutControlItemNguoiDung.Control = this.txtNguoiDung;
            this.layoutControlItemNguoiDung.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemNguoiDung.Name = "layoutControlItemNguoiDung";
            this.layoutControlItemNguoiDung.Size = new System.Drawing.Size(347, 24);
            this.layoutControlItemNguoiDung.Text = "Người dùng hiện tại";
            this.layoutControlItemNguoiDung.TextSize = new System.Drawing.Size(104, 13);
            // 
            // layoutControlItemMatKhauCu
            // 
            this.layoutControlItemMatKhauCu.Control = this.txtMatKhauCu;
            this.layoutControlItemMatKhauCu.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemMatKhauCu.Name = "layoutControlItemMatKhauCu";
            this.layoutControlItemMatKhauCu.Size = new System.Drawing.Size(347, 24);
            this.layoutControlItemMatKhauCu.Text = "Mật khẩu cũ";
            this.layoutControlItemMatKhauCu.TextSize = new System.Drawing.Size(104, 13);
            // 
            // layoutControlItemMatKhauMoi
            // 
            this.layoutControlItemMatKhauMoi.Control = this.txtMatKhauMoi;
            this.layoutControlItemMatKhauMoi.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemMatKhauMoi.Name = "layoutControlItemMatKhauMoi";
            this.layoutControlItemMatKhauMoi.Size = new System.Drawing.Size(347, 24);
            this.layoutControlItemMatKhauMoi.Text = "Mật khẩu mới";
            this.layoutControlItemMatKhauMoi.TextSize = new System.Drawing.Size(104, 13);
            // 
            // layoutControlItemXacNhanMatKhau
            // 
            this.layoutControlItemXacNhanMatKhau.Control = this.txtXacNhanMatKhau;
            this.layoutControlItemXacNhanMatKhau.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemXacNhanMatKhau.Name = "layoutControlItemXacNhanMatKhau";
            this.layoutControlItemXacNhanMatKhau.Size = new System.Drawing.Size(347, 24);
            this.layoutControlItemXacNhanMatKhau.Text = "Xác nhận lại mật khẩu";
            this.layoutControlItemXacNhanMatKhau.TextSize = new System.Drawing.Size(104, 13);
            // 
            // layoutControlItemBtnDongY
            // 
            this.layoutControlItemBtnDongY.Control = this.btnDongY;
            this.layoutControlItemBtnDongY.Location = new System.Drawing.Point(187, 96);
            this.layoutControlItemBtnDongY.MaxSize = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnDongY.MinSize = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnDongY.Name = "layoutControlItemBtnDongY";
            this.layoutControlItemBtnDongY.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnDongY.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnDongY.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnDongY.TextVisible = false;
            // 
            // layoutControlItemBtnThoat
            // 
            this.layoutControlItemBtnThoat.Control = this.btnThoat;
            this.layoutControlItemBtnThoat.Location = new System.Drawing.Point(267, 96);
            this.layoutControlItemBtnThoat.MaxSize = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnThoat.MinSize = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnThoat.Name = "layoutControlItemBtnThoat";
            this.layoutControlItemBtnThoat.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItemBtnThoat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnThoat.TextVisible = false;
            // 
            // emptySpaceItemBtn
            // 
            this.emptySpaceItemBtn.AllowHotTrack = false;
            this.emptySpaceItemBtn.Location = new System.Drawing.Point(0, 96);
            this.emptySpaceItemBtn.Name = "emptySpaceItemBtn";
            this.emptySpaceItemBtn.Size = new System.Drawing.Size(187, 26);
            this.emptySpaceItemBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frm_Grd_DoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 258);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "frm_Grd_DoiMatKhau";
            this.Text = "UIS - Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frm_Grd_DoiMatKhau_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXacNhanMatKhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhauMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhauCu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNguoiDung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMatKhauCu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMatKhauMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemXacNhanMatKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnDongY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.TextEdit txtXacNhanMatKhau;
        private DevExpress.XtraEditors.TextEdit txtMatKhauMoi;
        private DevExpress.XtraEditors.TextEdit txtMatKhauCu;
        private DevExpress.XtraEditors.TextEdit txtNguoiDung;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNguoiDung;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMatKhauCu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMatKhauMoi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemXacNhanMatKhau;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnDongY;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBtn;
    }
}