using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.Buttons
{
    public class btnUp: pscButton
    {
        public btnUp()
        {
            this.Text = "";
            this.Image = Properties.Resources.page_up;
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

        private static Keys _shortcut = Keys.None;

        public static Keys Shortcut
        {
            get
            {
                return _shortcut;
            }
            set
            {
                _shortcut = value;
            }
        }

        bool fullyPainted = false;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15 && !fullyPainted)
            {
                Form parent = this.FindForm();
                if (parent != null)
                {
                    if (Shortcut != Keys.None)
                    {
                        ShortKeyReg.RegisterHotKey(parent, this, Shortcut);
                    }

                    fullyPainted = true;
                }
            }
        }
    }
}
