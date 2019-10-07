namespace CommonLib.UserControls
{
    partial class uctChooseFilterGrids
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboChoose = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboChoose
            // 
            this.cboChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChoose.FormattingEnabled = true;
            this.cboChoose.Location = new System.Drawing.Point(0, 0);
            this.cboChoose.Name = "cboChoose";
            this.cboChoose.Size = new System.Drawing.Size(170, 21);
            this.cboChoose.TabIndex = 0;
            this.cboChoose.SelectedIndexChanged += new System.EventHandler(this.cboChooses_SelectedIndexChanged);
            // 
            // uctChooseFilterGrids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboChoose);
            this.Name = "uctChooseFilterGrids";
            this.Size = new System.Drawing.Size(170, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboChoose;
    }
}
