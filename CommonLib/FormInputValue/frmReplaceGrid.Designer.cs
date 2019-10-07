namespace CommonLib.FormInputValue
{
    partial class frmReplaceGrid
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.radByColumn = new System.Windows.Forms.RadioButton();
            this.radByRow = new System.Windows.Forms.RadioButton();
            this.radByAll = new System.Windows.Forms.RadioButton();
            this.btnSearch = new CommonLib.Buttons.pscButton();
            this.btnReplaceAll = new CommonLib.Buttons.pscButton();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.chkSerachUp = new System.Windows.Forms.CheckBox();
            this.btnReplace = new CommonLib.Buttons.pscButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm (^ F)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thay thế (^ H)";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(97, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(194, 21);
            this.txtSearch.TabIndex = 0;
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(97, 31);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(194, 21);
            this.txtReplace.TabIndex = 1;
            // 
            // radByColumn
            // 
            this.radByColumn.AutoSize = true;
            this.radByColumn.Checked = true;
            this.radByColumn.Location = new System.Drawing.Point(32, 65);
            this.radByColumn.Name = "radByColumn";
            this.radByColumn.Size = new System.Drawing.Size(67, 17);
            this.radByColumn.TabIndex = 2;
            this.radByColumn.TabStop = true;
            this.radByColumn.Text = "Theo cột";
            this.radByColumn.UseVisualStyleBackColor = true;
            // 
            // radByRow
            // 
            this.radByRow.AutoSize = true;
            this.radByRow.Location = new System.Drawing.Point(119, 65);
            this.radByRow.Name = "radByRow";
            this.radByRow.Size = new System.Drawing.Size(76, 17);
            this.radByRow.TabIndex = 3;
            this.radByRow.Text = "Theo dòng";
            this.radByRow.UseVisualStyleBackColor = true;
            // 
            // radByAll
            // 
            this.radByAll.AutoSize = true;
            this.radByAll.Location = new System.Drawing.Point(215, 65);
            this.radByAll.Name = "radByAll";
            this.radByAll.Size = new System.Drawing.Size(55, 17);
            this.radByAll.TabIndex = 4;
            this.radByAll.Text = "Tất cả";
            this.radByAll.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnSearch.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnSearch.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnSearch.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnSearch.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnSearch.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnSearch.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnSearch.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(106, 148);
            this.btnSearch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Tìm kiếm (F3)";
            this.btnSearch.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnReplaceAll.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnReplaceAll.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnReplaceAll.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnReplaceAll.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnReplaceAll.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnReplaceAll.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnReplaceAll.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnReplaceAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReplaceAll.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnReplaceAll.Location = new System.Drawing.Point(202, 177);
            this.btnReplaceAll.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(97, 23);
            this.btnReplaceAll.TabIndex = 9;
            this.btnReplaceAll.Text = "Thay tất cả";
            this.btnReplaceAll.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Location = new System.Drawing.Point(12, 97);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(131, 17);
            this.chkMatchCase.TabIndex = 5;
            this.chkMatchCase.Text = "Phân biệt hoa/thường";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // chkSerachUp
            // 
            this.chkSerachUp.AutoSize = true;
            this.chkSerachUp.Location = new System.Drawing.Point(12, 120);
            this.chkSerachUp.Name = "chkSerachUp";
            this.chkSerachUp.Size = new System.Drawing.Size(75, 17);
            this.chkSerachUp.TabIndex = 6;
            this.chkSerachUp.Text = "Tìm ngược";
            this.chkSerachUp.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnReplace.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnReplace.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnReplace.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnReplace.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnReplace.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnReplace.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnReplace.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnReplace.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReplace.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnReplace.Location = new System.Drawing.Point(202, 148);
            this.btnReplace.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(97, 23);
            this.btnReplace.TabIndex = 8;
            this.btnReplace.Text = "Thay thế (^ R)";
            this.btnReplace.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // frmReplaceGrid
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 205);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.chkSerachUp);
            this.Controls.Add(this.chkMatchCase);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.radByAll);
            this.Controls.Add(this.radByRow);
            this.Controls.Add(this.radByColumn);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "Xmas 2008 Blue";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.MaximizeBox = false;
            this.Name = "frmReplaceGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay thế";
            this.Load += new System.EventHandler(this.frmReplaceGrid_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReplaceGrid_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.RadioButton radByColumn;
        private System.Windows.Forms.RadioButton radByRow;
        private System.Windows.Forms.RadioButton radByAll;
        private CommonLib.Buttons.pscButton btnSearch;
        private CommonLib.Buttons.pscButton btnReplaceAll;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.CheckBox chkSerachUp;
        private CommonLib.Buttons.pscButton btnReplace;
    }
}