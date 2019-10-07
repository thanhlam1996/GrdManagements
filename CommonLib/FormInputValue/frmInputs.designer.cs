namespace CommonLib.FormInputValue
{
    partial class frmInputs
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
            this.lblValue = new DevExpress.XtraEditors.LabelControl();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSearch = new CommonLib.Buttons.pscButton();
            this.btnInput = new CommonLib.Buttons.pscButton();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            this.lblValue.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.lblValue.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lblValue.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.lblValue.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.lblValue.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.lblValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.lblValue.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.lblValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.lblValue.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.lblValue.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.lblValue.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.lblValue.Location = new System.Drawing.Point(12, 31);
            this.lblValue.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(44, 13);
            this.lblValue.TabIndex = 0;
            this.lblValue.Text = "Nhập tên";
            this.lblValue.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.lblValue.TextChanged += new System.EventHandler(this.lblValue_TextChanged);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(68, 28);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(257, 21);
            this.txtValue.TabIndex = 1;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnSearch.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnSearch.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnSearch.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnSearch.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnSearch.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnSearch.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(325, 27);
            this.btnSearch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(27, 21);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "...";
            this.toolTip1.SetToolTip(this.btnSearch, "Tìm kiếm");
            this.btnSearch.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnInput
            // 
            this.btnInput.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnInput.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnInput.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnInput.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnInput.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnInput.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnInput.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnInput.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnInput.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInput.Location = new System.Drawing.Point(359, 27);
            this.btnInput.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(75, 21);
            this.btnInput.TabIndex = 2;
            this.btnInput.Text = "Đồng ý";
            this.btnInput.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // frmInputs
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 70);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.LookAndFeel.SkinName = "Money Twins";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximumSize = new System.Drawing.Size(1024, 108);
            this.MinimumSize = new System.Drawing.Size(350, 97);
            this.Name = "frmInputs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIS - Nhap Ten";
            this.Load += new System.EventHandler(this.frmInputs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lblValue;
        public System.Windows.Forms.TextBox txtValue;
        public CommonLib.Buttons.pscButton btnInput;
        public CommonLib.Buttons.pscButton btnSearch;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}