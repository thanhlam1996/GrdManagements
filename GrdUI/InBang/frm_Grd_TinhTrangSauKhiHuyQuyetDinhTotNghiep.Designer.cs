namespace GrdUI.InBang
{
    partial class frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lookUpEditTinhTrang = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemTinhTrang = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnHuyQuyetDinh = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTinhTrang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTinhTrang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.btnThoat);
            this.layoutControl.Controls.Add(this.btnHuyQuyetDinh);
            this.layoutControl.Controls.Add(this.lookUpEditTinhTrang);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(303, 70);
            this.layoutControl.TabIndex = 0;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTinhTrang,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(303, 70);
            this.layoutControlGroup.TextVisible = false;
            // 
            // lookUpEditTinhTrang
            // 
            this.lookUpEditTinhTrang.Location = new System.Drawing.Point(65, 12);
            this.lookUpEditTinhTrang.Name = "lookUpEditTinhTrang";
            this.lookUpEditTinhTrang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTinhTrang.Properties.NullText = "";
            this.lookUpEditTinhTrang.Size = new System.Drawing.Size(226, 20);
            this.lookUpEditTinhTrang.StyleController = this.layoutControl;
            this.lookUpEditTinhTrang.TabIndex = 4;
            // 
            // layoutControlItemTinhTrang
            // 
            this.layoutControlItemTinhTrang.Control = this.lookUpEditTinhTrang;
            this.layoutControlItemTinhTrang.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTinhTrang.Name = "layoutControlItemTinhTrang";
            this.layoutControlItemTinhTrang.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItemTinhTrang.Text = "Tình trạng";
            this.layoutControlItemTinhTrang.TextSize = new System.Drawing.Size(49, 13);
            // 
            // btnHuyQuyetDinh
            // 
            this.btnHuyQuyetDinh.ImageIndex = 5;
            this.btnHuyQuyetDinh.ImageList = this.imageCollection;
            this.btnHuyQuyetDinh.Location = new System.Drawing.Point(110, 36);
            this.btnHuyQuyetDinh.Name = "btnHuyQuyetDinh";
            this.btnHuyQuyetDinh.Size = new System.Drawing.Size(112, 22);
            this.btnHuyQuyetDinh.StyleController = this.layoutControl;
            this.btnHuyQuyetDinh.TabIndex = 5;
            this.btnHuyQuyetDinh.Text = "Hủy quyết định";
            this.btnHuyQuyetDinh.Click += new System.EventHandler(this.btnHuyQuyetDinh_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnHuyQuyetDinh;
            this.layoutControlItem1.Location = new System.Drawing.Point(98, 24);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(116, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(116, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(116, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnThoat
            // 
            this.btnThoat.ImageIndex = 2;
            this.btnThoat.ImageList = this.imageCollection;
            this.btnThoat.Location = new System.Drawing.Point(226, 36);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(65, 22);
            this.btnThoat.StyleController = this.layoutControl;
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnThoat;
            this.layoutControlItem2.Location = new System.Drawing.Point(214, 24);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(98, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            this.imageCollection.Images.SetKeyName(6, "vmware-128.png");
            this.imageCollection.Images.SetKeyName(7, "04_download-128.png");
            this.imageCollection.Images.SetKeyName(8, "614397-x-32.png");
            this.imageCollection.Images.SetKeyName(9, "close-circle-red-32.png");
            this.imageCollection.Images.SetKeyName(10, "vmware.png");
            this.imageCollection.Images.SetKeyName(11, "Smart_Phone.png");
            // 
            // frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 70);
            this.Controls.Add(this.layoutControl);
            this.Name = "frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep";
            this.Text = "UIS - Tình trạng";
            this.Load += new System.EventHandler(this.frm_Grd_TinhTrangSauKhiHuyQuyetDinhTotNghiep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTinhTrang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTinhTrang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnHuyQuyetDinh;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTinhTrang;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTinhTrang;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.Utils.ImageCollection imageCollection;
    }
}