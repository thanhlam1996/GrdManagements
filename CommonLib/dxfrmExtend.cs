using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins.XtraForm;
using DevExpress.Skins;

namespace CommonLib
{
    public partial class dxfrmExtend : DevExpress.XtraEditors.XtraForm
    {
        static bool InitSkins = false;
        internal bool flagChangeLabel = false;

        private void xfrmExtend_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;

            if (!GlobalLib.IsUISMain && !InitSkins)
            {
                DevExpress.UserSkins.BonusSkins.Register();
                this.LookAndFeel.SetSkinStyle(GlobalLib.SkinName.ToString().Replace("_"," "));
                InitSkins = true;
            }
            else
            {
                DevExpress.Skins.SkinManager.DisableFormSkins();
                DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            }
            if (!flagChangeLabel)
            {
                if (CommonLib.ChangeLabelControl.DefaultChangeLabelController != null)
                {
                    try
                    {
                        CommonLib.ChangeLabelControl.DefaultChangeLabelController.ChangeLabel(this);
                        flagChangeLabel = true;
                    }
                    catch { }
                }
            }
        }

        public static Icon InitIcon= null;
    }
}