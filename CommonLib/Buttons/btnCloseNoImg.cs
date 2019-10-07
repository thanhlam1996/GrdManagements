using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.Buttons
{
    public class btnCloseNoImg : pscButton 
    {
        public btnCloseNoImg() 
        {
            this.Text = "Đóng";
            this.Width = 81;
            this.Height = 23;
            this.Image = Properties.Resources.close;
            this.Load += new EventHandler(btnCloseNoImg_Load);
        }

        void btnCloseNoImg_Load(object sender, EventArgs e)
        {
            if (IsDesignMode) return;
            Form frm = this.FindForm();
            if (frm != null && frm.Modal)
            {
                CommonLib.ShortKeyReg.RegisterHotKey(frm, this, Keys.Escape);
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (pscButton.UseTextShortKey)
                {
                    base.Text = SKey;
                    this.ToolTip = "Đóng (ESC)";
                }
                else
                    base.Text = "Đóng";
            }
        }

        protected override void OnClick(EventArgs e)
        {
            try
            {
                if (IsDesignMode) return;
                base.OnClick(e);
                Form frm = this.FindForm();
                if (frm != null)
                    frm.Close();
            }
            catch { }
        }

        public static string SKey = "Đóng";        
    }
}
