namespace CommonLib.UserControls
{
    partial class PopupControl
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
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultgData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pscButton1 = new CommonLib.Buttons.pscButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultgData)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultgData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 125);
            this.panel1.TabIndex = 0;
            // 
            // ultgData
            // 
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultgData.DisplayLayout.Appearance = appearance5;
            this.ultgData.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            appearance1.BackColor = System.Drawing.Color.PowderBlue;
            appearance1.BackColor2 = System.Drawing.Color.DeepSkyBlue;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            ultraGridBand1.Header.Appearance = appearance1;
            this.ultgData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultgData.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance14.BackColor = System.Drawing.Color.PowderBlue;
            appearance14.BackColor2 = System.Drawing.Color.DeepSkyBlue;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultgData.DisplayLayout.CaptionAppearance = appearance14;
            this.ultgData.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultgData.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultgData.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultgData.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultgData.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultgData.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultgData.DisplayLayout.MaxColScrollRegions = 1;
            this.ultgData.DisplayLayout.MaxRowScrollRegions = 1;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultgData.DisplayLayout.Override.ActiveCellAppearance = appearance13;
            appearance8.BackColor = System.Drawing.SystemColors.Highlight;
            appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultgData.DisplayLayout.Override.ActiveRowAppearance = appearance8;
            this.ultgData.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultgData.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultgData.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance6.BorderColor = System.Drawing.Color.Silver;
            appearance6.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultgData.DisplayLayout.Override.CellAppearance = appearance6;
            this.ultgData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ultgData.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.ultgData.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance12.TextHAlignAsString = "Center";
            this.ultgData.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.ultgData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultgData.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultgData.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultgData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultgData.DisplayLayout.Override.TemplateAddRowAppearance = appearance9;
            this.ultgData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultgData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultgData.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultgData.Location = new System.Drawing.Point(0, 0);
            this.ultgData.Name = "ultgData";
            this.ultgData.Size = new System.Drawing.Size(265, 125);
            this.ultgData.TabIndex = 0;
            this.ultgData.DisplayLayout.Override.MaxSelectedRows = 1;
            this.ultgData.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultgData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            
            this.ultgData.Text = "ultraGrid1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pscButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 25);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // pscButton1
            // 
            this.pscButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pscButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.pscButton1.Location = new System.Drawing.Point(107, 1);
            this.pscButton1.Name = "pscButton1";
            this.pscButton1.Size = new System.Drawing.Size(52, 21);
            this.pscButton1.TabIndex = 0;
            this.pscButton1.Text = "Lưu";
            // 
            // PopupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "PopupControl";
            this.Size = new System.Drawing.Size(265, 150);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PopupControl_Paint);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultgData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public Infragistics.Win.UltraWinGrid.UltraGrid ultgData;
        public CommonLib.Buttons.pscButton pscButton1;

    }
}
