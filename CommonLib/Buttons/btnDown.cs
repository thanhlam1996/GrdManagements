using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.Buttons
{
    public class btnDown: pscButton
    {
        public btnDown()
        {
            this.Text = "";
            this.Image = Properties.Resources.page_down;
            this.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.Width = 24;
            this.Height = 23;
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
                    base.Text = SKey;
                else
                    base.Text = "";
            }
        }

        public static string SKey = "";
    }
}
