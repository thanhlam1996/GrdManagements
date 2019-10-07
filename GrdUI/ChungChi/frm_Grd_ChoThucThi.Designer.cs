namespace GrdUI.ChungChi
{
    partial class frm_Grd_ChoThucThi
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
            this.progressPanel_ThucThiCapNhat = new DevExpress.XtraWaitForm.ProgressPanel();
            this.tableLayoutPanel_ThucHien = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_ThucHien.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressPanel_ThucThiCapNhat
            // 
            this.progressPanel_ThucThiCapNhat.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel_ThucThiCapNhat.Appearance.Options.UseBackColor = true;
            this.progressPanel_ThucThiCapNhat.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel_ThucThiCapNhat.AppearanceCaption.Options.UseFont = true;
            this.progressPanel_ThucThiCapNhat.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel_ThucThiCapNhat.AppearanceDescription.Options.UseFont = true;
            this.progressPanel_ThucThiCapNhat.Caption = "Đang thực hiện ...";
            this.progressPanel_ThucThiCapNhat.Description = "";
            this.progressPanel_ThucThiCapNhat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressPanel_ThucThiCapNhat.ImageHorzOffset = 20;
            this.progressPanel_ThucThiCapNhat.Location = new System.Drawing.Point(0, 17);
            this.progressPanel_ThucThiCapNhat.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.progressPanel_ThucThiCapNhat.Name = "progressPanel_ThucThiCapNhat";
            this.progressPanel_ThucThiCapNhat.Size = new System.Drawing.Size(246, 39);
            this.progressPanel_ThucThiCapNhat.TabIndex = 0;
            this.progressPanel_ThucThiCapNhat.Text = "Đang thực hiện...";
            // 
            // tableLayoutPanel_ThucHien
            // 
            this.tableLayoutPanel_ThucHien.AutoSize = true;
            this.tableLayoutPanel_ThucHien.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_ThucHien.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel_ThucHien.ColumnCount = 1;
            this.tableLayoutPanel_ThucHien.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_ThucHien.Controls.Add(this.progressPanel_ThucThiCapNhat, 0, 0);
            this.tableLayoutPanel_ThucHien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_ThucHien.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_ThucHien.Name = "tableLayoutPanel_ThucHien";
            this.tableLayoutPanel_ThucHien.Padding = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this.tableLayoutPanel_ThucHien.RowCount = 1;
            this.tableLayoutPanel_ThucHien.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_ThucHien.Size = new System.Drawing.Size(246, 73);
            this.tableLayoutPanel_ThucHien.TabIndex = 1;
            // 
            // ThucThiCapNhat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(246, 73);
            this.Controls.Add(this.tableLayoutPanel_ThucHien);
            this.DoubleBuffered = true;
            this.Name = "ThucThiCapNhat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.tableLayoutPanel_ThucHien.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel progressPanel_ThucThiCapNhat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ThucHien;
    }
}
