using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.OtherControls
{
    public class txtSearch : TextBox 
    {
        public txtSearch()
        {
 
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value ;
            }
        }

        protected override void InitLayout()
        {
            this.Text = "Vui lòng nhập thông tin tìm kiếm vào đây";
        }

        protected override void OnClick(EventArgs e)
        {
            this.Clear ();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.Text = "Vui lòng nhập thông tin tìm kiếm vào đây";
        }
    }
}
