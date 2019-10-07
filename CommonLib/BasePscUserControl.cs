using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public class BasePscUserControl : UserControl
    {
        internal bool flagChangeLabel = false;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!flagChangeLabel)
            {
                if (CommonLib.ChangeLabelControl.DefaultChangeLabelController != null)
                {
                    CommonLib.ChangeLabelControl.DefaultChangeLabelController.ChangeLabel(this);
                    flagChangeLabel = true;
                }
            }
        }
    }
}
