using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Resources;
using System.Threading;
using System.Globalization;


namespace CommonLib.Buttons
{
    public class pscButton : SimpleButton
    {
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.ComponentModel.IContainer components;

        public bool UseVisualStyleBackColor = true;
        public static bool UseLookAndFeel = true;

        public pscButton()
        {
            this.Text = "pscButton";
            this.LookAndFeel.UseDefaultLookAndFeel = UseLookAndFeel;
            this.LookAndFeel.UseWindowsXPTheme = !UseLookAndFeel;
        }
        
        protected override void InitLayout()
        {

            this.ImageLocation = ImageLocation.MiddleLeft;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pscButton));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            OnLoad(this, new EventArgs());
        }

        public event EventHandler Load;

        protected void OnLoad(object sender, EventArgs e)
        {
            if (Load != null)
                Load(sender, e);            
        }

        public static bool UseTextShortKey = false;

        public Keys Shortcut = Keys.None;
    }
}
