using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.UserControls
{
    public class TabControlExtend : TabControl
    {
        private TabPage[] _TabPagesX = null;

        /// <summary>
        /// TabPageX contains all tabpage
        /// </summary>
        public TabPage[] TabPagesX
        {
            get { return _TabPagesX; }
        }

        public TabControlExtend()
        {
            _TabPagesX = new TabPage[0] { };
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is TabPage)
                if (Array.Find(_TabPagesX, delegate(TabPage tab) { if (tab.Name == e.Control.Name && e.Control.Name != "") return true; else return false; }) == null)
                {
                    Array.Resize(ref _TabPagesX, _TabPagesX.Length + 1);
                    _TabPagesX[_TabPagesX.Length - 1] = (TabPage)e.Control;
                }
        }
    }
}
