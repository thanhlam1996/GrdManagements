namespace GrdUI.ChungChi
{
    partial class frm_Grd_CapNhatThangXepLoai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_CapNhatThangXepLoai));
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.btnLuuDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_TenThangXepLoai = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_MaThangXepLoai = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMaThangXepLoai = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTenThangXepLoai = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemLeftBtn = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TenThangXepLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_MaThangXepLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMaThangXepLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTenThangXepLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeftBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.btnThoat);
            this.layoutControlMain.Controls.Add(this.btnLuuDuLieu);
            this.layoutControlMain.Controls.Add(this.textEdit_TenThangXepLoai);
            this.layoutControlMain.Controls.Add(this.textEdit_MaThangXepLoai);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(380, 98);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControl1";
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 0;
            this.btnThoat.ImageList = this.imageCollectionSmall;
            this.btnThoat.Location = new System.Drawing.Point(308, 64);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(60, 22);
            this.btnThoat.StyleController = this.layoutControlMain;
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // imageCollectionSmall
            // 
            this.imageCollectionSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionSmall.ImageStream")));
            this.imageCollectionSmall.Images.SetKeyName(0, "exit.png");
            this.imageCollectionSmall.Images.SetKeyName(1, "save.png");
            // 
            // btnLuuDuLieu
            // 
            this.btnLuuDuLieu.ImageIndex = 1;
            this.btnLuuDuLieu.ImageList = this.imageCollectionSmall;
            this.btnLuuDuLieu.Location = new System.Drawing.Point(213, 64);
            this.btnLuuDuLieu.Name = "btnLuuDuLieu";
            this.btnLuuDuLieu.Size = new System.Drawing.Size(91, 22);
            this.btnLuuDuLieu.StyleController = this.layoutControlMain;
            this.btnLuuDuLieu.TabIndex = 9;
            this.btnLuuDuLieu.Text = "Lưu dữ liệu";
            this.btnLuuDuLieu.Click += new System.EventHandler(this.btnLuuDuLieu_Click);
            // 
            // textEdit_TenThangXepLoai
            // 
            this.textEdit_TenThangXepLoai.Location = new System.Drawing.Point(121, 38);
            this.textEdit_TenThangXepLoai.Name = "textEdit_TenThangXepLoai";
            this.textEdit_TenThangXepLoai.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit_TenThangXepLoai.Properties.Appearance.Options.UseFont = true;
            this.textEdit_TenThangXepLoai.Size = new System.Drawing.Size(247, 22);
            this.textEdit_TenThangXepLoai.StyleController = this.layoutControlMain;
            this.textEdit_TenThangXepLoai.TabIndex = 5;
            // 
            // textEdit_MaThangXepLoai
            // 
            this.textEdit_MaThangXepLoai.Location = new System.Drawing.Point(121, 12);
            this.textEdit_MaThangXepLoai.Name = "textEdit_MaThangXepLoai";
            this.textEdit_MaThangXepLoai.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit_MaThangXepLoai.Properties.Appearance.Options.UseFont = true;
            this.textEdit_MaThangXepLoai.Size = new System.Drawing.Size(247, 22);
            this.textEdit_MaThangXepLoai.StyleController = this.layoutControlMain;
            this.textEdit_MaThangXepLoai.TabIndex = 4;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMaThangXepLoai,
            this.layoutControlItemTenThangXepLoai,
            this.layoutControlItemBtnSave,
            this.layoutControlItemBtnThoat,
            this.emptySpaceItemLeftBtn});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(380, 98);
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlItemMaThangXepLoai
            // 
            this.layoutControlItemMaThangXepLoai.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItemMaThangXepLoai.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemMaThangXepLoai.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemMaThangXepLoai.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemMaThangXepLoai.Control = this.textEdit_MaThangXepLoai;
            this.layoutControlItemMaThangXepLoai.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemMaThangXepLoai.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMaThangXepLoai.Name = "layoutControlItemMaThangXepLoai";
            this.layoutControlItemMaThangXepLoai.Size = new System.Drawing.Size(360, 26);
            this.layoutControlItemMaThangXepLoai.Text = "Mã thang xếp loại";
            this.layoutControlItemMaThangXepLoai.TextSize = new System.Drawing.Size(106, 16);
            // 
            // layoutControlItemTenThangXepLoai
            // 
            this.layoutControlItemTenThangXepLoai.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItemTenThangXepLoai.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemTenThangXepLoai.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemTenThangXepLoai.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemTenThangXepLoai.Control = this.textEdit_TenThangXepLoai;
            this.layoutControlItemTenThangXepLoai.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemTenThangXepLoai.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemTenThangXepLoai.Name = "layoutControlItemTenThangXepLoai";
            this.layoutControlItemTenThangXepLoai.Size = new System.Drawing.Size(360, 26);
            this.layoutControlItemTenThangXepLoai.Text = "Tên thang xếp loại";
            this.layoutControlItemTenThangXepLoai.TextSize = new System.Drawing.Size(106, 16);
            // 
            // layoutControlItemBtnSave
            // 
            this.layoutControlItemBtnSave.Control = this.btnLuuDuLieu;
            this.layoutControlItemBtnSave.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItemBtnSave.Location = new System.Drawing.Point(201, 52);
            this.layoutControlItemBtnSave.MaxSize = new System.Drawing.Size(95, 26);
            this.layoutControlItemBtnSave.MinSize = new System.Drawing.Size(95, 26);
            this.layoutControlItemBtnSave.Name = "layoutControlItemBtnSave";
            this.layoutControlItemBtnSave.Size = new System.Drawing.Size(95, 26);
            this.layoutControlItemBtnSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnSave.TextVisible = false;
            // 
            // layoutControlItemBtnThoat
            // 
            this.layoutControlItemBtnThoat.Control = this.btnThoat;
            this.layoutControlItemBtnThoat.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItemBtnThoat.Location = new System.Drawing.Point(296, 52);
            this.layoutControlItemBtnThoat.MaxSize = new System.Drawing.Size(64, 26);
            this.layoutControlItemBtnThoat.MinSize = new System.Drawing.Size(64, 26);
            this.layoutControlItemBtnThoat.Name = "layoutControlItemBtnThoat";
            this.layoutControlItemBtnThoat.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItemBtnThoat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnThoat.TextVisible = false;
            // 
            // emptySpaceItemLeftBtn
            // 
            this.emptySpaceItemLeftBtn.AllowHotTrack = false;
            this.emptySpaceItemLeftBtn.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItemLeftBtn.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItemLeftBtn.Name = "emptySpaceItemLeftBtn";
            this.emptySpaceItemLeftBtn.Size = new System.Drawing.Size(201, 26);
            this.emptySpaceItemLeftBtn.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frm_Grd_CapNhatThangXepLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 98);
            this.Controls.Add(this.layoutControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_Grd_CapNhatThangXepLoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIS - Thang xếp loại";
            this.Load += new System.EventHandler(this.frmCapNhatThangDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_TenThangXepLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_MaThangXepLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMaThangXepLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTenThangXepLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeftBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraEditors.TextEdit textEdit_TenThangXepLoai;
        private DevExpress.XtraEditors.TextEdit textEdit_MaThangXepLoai;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMaThangXepLoai;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTenThangXepLoai;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnLuuDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnThoat;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemLeftBtn;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
    }
}