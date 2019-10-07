using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CommonLib.OtherControls
{
    public class txtNumber : TextBox
    {
        #region Variables
        bool _autoGenNumber = true;
        int _numberDecimals = 0;
        bool _allowNegative = false;

        #endregion

        #region Properties
        public bool AllowNegative
        {
            get { return _allowNegative; }
            set { _allowNegative = value; }
        }

        public bool AutoGenNumber
        {
            set { _autoGenNumber = value; }
            get { return _autoGenNumber; }
        }

        public int NumberDecimals
        {
            get { return _numberDecimals; }
            set { _numberDecimals = value; }
        }

        #endregion

        #region Text
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (!this.AutoGenNumber)
                    base.Text = value;
                else
                {
                    base.Text = Functions.MoneyToString(value, NumberDecimals);
                }
            }
        }
        #endregion

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    if(this.Text.Trim()!="")
        //        this.Text = Functions.StringToMoney(this.Text).ToString();
        //}

        #region OnLostFocus
        protected override void OnLostFocus(EventArgs e)
        {
            try
            {
                base.OnLostFocus(e);
                if (!AutoGenNumber) return;
                string s = this.Text;
                this.Text = Functions.MoneyToString(s, NumberDecimals);
            }
            catch
            {
                this.Text = "";
            }
        }
        #endregion

        #region OnKeyPress
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            try
            {
                if (!AutoGenNumber) { base.OnKeyPress(e); return; }
                string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                string groupChar = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
                char decimalChar = Convert.ToChar(decimalString);
                int[] numGroup = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSizes; 
                int curPos = this.SelectionStart;
                if (e.KeyChar == '-' && AllowNegative)
                {
                    if (curPos != 0 || this.Text.IndexOf("-") != -1) e.Handled = true;
                    return;
                }
                else if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
                else if ((e.KeyChar.ToString() == groupChar))
                {
                    if (curPos == 0) e.Handled = true;
                    if (e.Handled) return;
                    int indexdecimal = this.Text.IndexOf(decimalString);
                    if (indexdecimal != -1 && curPos > indexdecimal) e.Handled = true;
                    if (e.Handled) return;

                    //int index1 = this.Text.LastIndexOf(groupChar,curPos);
                    //int index2 = this.Text.IndexOf(groupChar, curPos);
                    //if (index1 != -1 && index1 + numGroup[0]< curPos && curPos<this.Text.Length)
                    //    e.Handled = true;
                    //if (e.Handled) return;
                    //if (index2 != -1 && index2 - numGroup[0]> curPos)
                    //    e.Handled = true;
                    //if (e.Handled) return;

                    //if (index1 == -1 && this.Text.Substring(0, curPos).Length < numGroup[0])
                    //    e.Handled = true;
                    //if (e.Handled) return;
                    //if (index2 == -1 && indexdecimal == -1 && this.Text.Substring(curPos).Length < numGroup[0] && curPos < this.Text.Length)
                    //    e.Handled = true;
                    //if (e.Handled) return;
                    //if (index2 == -1 && this.Text.Substring(curPos,indexdecimal-curPos).Length < numGroup[0])
                    //    e.Handled = true;
                    //if (e.Handled) return;
                }
                else if ((e.KeyChar.ToString() == decimalString) && this.Text.IndexOf(decimalString) == -1) { }
                else
                {
                    e.Handled = true;
                }
                if(!e.Handled)
                    base.OnKeyPress(e);
            }
            catch { }
        }
        #endregion
    }
}
