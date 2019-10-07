using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Data;
using Infragistics.Win.UltraWinGrid;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Collections;
using DevExpress.XtraEditors;
using CommonLib.FormInputValue;
using Infragistics.Win;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace CommonLib
{
    public class Functions
    {
        public static bool IsForBuildModule = false;

        public static string MoneyToString(string money, int decimals)
        {
            string result = string.Empty;
            try
            {
                string decimalChar = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                string groupChar = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
                int[] numGroup = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSizes;

                string[] split = money.Split(Convert.ToChar(decimalChar));

                if (split.Length == 0)
                    result = string.Empty;
                else if (split.Length == 1)
                {
                    if (decimals == 0)
                    {
                        decimal tmp = 0;
                        decimal.TryParse(money, out tmp);
                        tmp = Math.Round(tmp, 0);
                        result = ToMoney(tmp + "");
                        //result = ToMoney(split[0]);
                    }
                    else
                    {
                        string num0 = "";
                        for (int i = 0; i < decimals; i++)
                            num0 += "0";
                        result = ToMoney(split[0]) + decimalChar + num0;
                    }
                }
                else
                {
                    if (decimals == 0)
                    {
                        decimal tmp = 0;
                        decimal.TryParse(money, out tmp);
                        tmp = Math.Round(tmp, 0);
                        result = ToMoney(tmp + "");
                        //result = ToMoney(split[0]);
                    }
                    else
                    {
                        decimal dec = 0;
                        decimal.TryParse(0 + decimalChar + split[1], out dec);
                        string num0 = "";
                        for (int i = 0; i < decimals; i++)
                            num0 += "0";
                        string s = "";
                        if (num0 != "")
                            s = string.Format("{0:0." + num0 + "}", dec);

                        if (s.Substring(0, 1) == "1")
                        {
                            decimal d = 0;
                            decimal.TryParse(split[0], out d);
                            result = ToMoney((d + 1) + "") + decimalChar + s.Substring(2);
                        }
                        else
                        {
                            result = ToMoney(split[0]) + decimalChar + s.Substring(2);
                        }
                    }
                }
                while (result.StartsWith("0"))
                {
                    if (result.Length > 1)
                    {
                        if (result.Substring(1, 1) != decimalChar)
                            result = result.Substring(1);
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        private static string ToMoney(string money)
        {
            string groupChar = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            int[] numGroup = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSizes;

            string result = "";
            string[] header = new string[] { };
            money = money.Replace(groupChar, "");
            while (money.Length > numGroup[0])
            {
                Array.Resize(ref header, header.Length + 1);
                header[header.Length - 1] = RightString(money, numGroup[0]);
                money = LeftString(money, money.Length - header[header.Length - 1].Length);
            }
            if (money.Length > 0)
            {
                Array.Resize(ref header, header.Length + 1);
                header[header.Length - 1] = money;
            }

            for (int i = header.Length - 1; i >= 0; i--)
                result += (result == "" ? "" : groupChar) + header[i];

            return result;
        }

        /// <summary>
        /// Ham kiem tra de mo Form con cung loai
        /// </summary>
        /// <param name="type"></param>
        /// <param name="frmParent"></param>
        /// <returns></returns>
        public static bool IsFocusForm(Type type, Form frmParent, string titleCheckUsedCommon)
        {
            if (frmParent == null) return false;
            foreach (Form frm in frmParent.MdiChildren)
            {
                if (frm.GetType() == type)
                {
                    if (frm.Text == titleCheckUsedCommon)
                    {
                        if (frm.MinimizeBox)
                        {
                            frm.Focus();
                            frm.WindowState = FormWindowState.Normal;
                        }
                        frm.Focus();
                        return true;
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// Them note vao cay
        /// </summary>
        /// <param name="parentNode"></param>
        private static void AddNote(TreeNode parentNode, DataView dv, string parentColumnID, string columnID, string columnName)
        {
            dv.RowFilter = parentColumnID + " = '" + parentNode.Name + "'";
            foreach (DataRowView row in dv)
            {
                parentNode.Nodes.Add(row[columnID].ToString(), row[columnName].ToString());
                AddNote(parentNode.Nodes[row[columnID].ToString()], dv, parentColumnID, columnID, columnName);
            }
        }

        #region "Data Password"
        /// <summary>
        /// Ham convert tu Binary sang Character
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string ConvertFromBinaryToCharacter(string b)
        {
            double result = 0;
            for (int i = 0; i < 16; i++)
            {
                if (b[i] == '1') result += Math.Pow(2, 15 - i);
            }
            return Convert.ToChar(Convert.ToInt32(result)).ToString();
        }

        /// <summary>
        /// Ham convert tu Character sang Binary
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static string ConvertFromCharacterToBinary(char c)
        {
            string result = string.Empty;
            double value = Convert.ToDouble(Convert.ToInt32(c));
            for (double i = 15; i >= 0; i--)
            {
                if (value >= Math.Pow(2, i))
                {
                    result += "1";
                    value -= Math.Pow(2, i);
                }
                else
                    result += "0";
            }
            return result;
        }

        /// <summary>
        /// Ham gia ma chuoi da ma hoa
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecodeString(string s)
        {
            string result = string.Empty;
            if (s == string.Empty) return string.Empty;
            for (int i = 0; i < s.Length; i += 16)
            {
                string temp = string.Empty;
                for (int j = 0; j < 16; j++)
                {
                    temp += s[i + j].ToString();
                }
                result += ConvertFromBinaryToCharacter(temp);
            }
            return result;
        }
        #endregion

        #region "Error Managements"
        /// <summary>
        /// Ham quan ly loi
        /// </summary>
        /// <param name="messageError">Loi he thong</param>
        /// <param name="layer">Tang quan ly code : 0-DAL , 1-BLL, 2:Object;3-Application</param>
        /// <returns></returns>
        public static int PscException(string messageError, int layer)
        {
            int result = -1;
            foreach (DataRow dr in GlobalLib.DataError.Rows)
            {
                if (messageError.Contains(dr["SystemName"].ToString()))
                {
                    result = int.Parse(dr["Code"].ToString());
                    break;
                }
            }

            if (!GlobalLib.NotDefineError.Contains(messageError))
            {
                if (layer == 0)
                    GlobalLib.NotDefineError += "Uis-ExceptionDAL : " + messageError + "\n";
                if (layer == 1)
                    GlobalLib.NotDefineError += "Uis-ExceptionBLL : " + messageError + "\n";
                if (layer == 2)
                    GlobalLib.NotDefineError += "Uis-ExceptionObj : " + messageError + "\n";
                if (layer == 3)
                    GlobalLib.NotDefineError += "Uis-ExceptionAPP : " + messageError + "\n";
            }
            return result;
        }
        #endregion

        #region function string        
        /// <summary>
        /// Get left string 
        /// </summary>
        /// <param name="SrcString"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string LeftString(string SrcString, int Length)
        {
            if (SrcString.Length <= Length)
                return SrcString;
            else
                return SrcString.Substring(0, Length);
        }

        /// <summary>
        /// Get right string
        /// </summary>
        /// <param name="SrcString"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string RightString(string SrcString, int Length)
        {
            if (SrcString.Length <= Length)
            {
                return SrcString;
            }
            else
            {
                return SrcString.Substring(SrcString.Length - Length);
            }
        }
        #endregion

        #region funtion datetime
        public static string GetDateFormat()
        {
            return "dd/MM/yyyy";
        }

        public static string ConvertDateToStringSQL(DateTime date, string Format)
        {
            string ret = "";
            if (Format == null || Format == "")
                ret = date.ToString("yyyy-MM-dd HH:mm:ss");
            else
                ret = date.ToString(Format);
            return ret;
        }
        #endregion

        #region ConvertUniToChar
        const string UNICODEVowels = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";
        const string PSCVowels = "az4az1az2az3az5azzazzz4azzz1azzz2azzz3azzz5azzzazzzz4azzzz1azzzz2azzzz3azzzz5ez4ez1ez2ez3ez5ezzezzz4ezzz1ezzz2ezzz3ezzz5iz4iz1iz2iz3iz5oz4oz1oz2oz3oz5ozzozzz4ozzz1ozzz2ozzz3ozzz5ozzzozzzz4ozzzz1ozzzz2ozzzz3ozzzz5uz4uz1uz2uz3uz5uzzuzzz4uzzz1uzzz2uzzz3uzzz5yz4yz1yz2yz3yz5dzAZ4AZ1AZ2AZ3AZ5AZZAZZZ4AZZZ1AZZZ2AZZZ3AZZZ5AZZZAZZZZ4AZZZZ1AZZZZ2AZZZZ3AZZZZ5EZ4EZ1EZ2EZ3EZ5EZZEZZZ4EZZZ1EZZZ2EZZZ3EZZZ5IZ4IZ1IZ2IZ3IZ5OZ4OZ1OZ2OZ3OZ5OZZOZZZ4OZZZ1OZZZ2OZZZ3OZZZ5OZZZOZZZZ4OZZZZ1OZZZZ2OZZZZ3OZZZZ5UZ4UZ1UZ2UZ3UZ5UZZUZZZ4UZZZ1UZZZ2UZZZ3UZZZ5YZ4YZ1YZ2YZ3YZ5DZ";
        const string PSCVowelsLen = "33333355555466666333333555553333333333355555466666333333555553333323333335555546666633333355555333333333335555546666633333355555333332";

        static string PscCharsAt(int index)
        {
            int i = 0;
            int pscIndex = 0;
            while (i < index)
            {
                pscIndex += int.Parse(PSCVowelsLen.Substring(i, 1));
                i++;
            }
            return PSCVowels.Substring(pscIndex, int.Parse(PSCVowelsLen.Substring(index, 1)));
        }

        public static string Unicode2Psc(string inStr)
        {
            string outStr = "";
            int i = 0;
            while (i < inStr.Length)
            {
                string letter = inStr.Substring(i, 1);
                int index = UNICODEVowels.IndexOf(letter);
                if (index >= 0)
                    outStr += PscCharsAt(index);
                else
                    outStr += letter;
                i++;
            }
            return outStr;
        }
        #endregion
    }

    public class ChangeUILabel
    {
        int _iD = 0;

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        string _oldName = "";

        public string OldName
        {
            get { return _oldName; }
            set { _oldName = value; }
        }
        string _newName = "";

        public string NewName
        {
            get { return _newName; }
            set { _newName = value; }
        }

        public ChangeUILabel()
        {

        }

        public ChangeUILabel(int iD, string oldName, string newName)
        {
            this._iD = iD;
            this._newName = newName;
            this._oldName = oldName;
        }
    }

    public class ChangeUILabelCollection : IList<ChangeUILabel>
    {
        List<ChangeUILabel> lstLabel = new List<ChangeUILabel>();

        #region IList<ChangeUILabel> Members
        public int IndexOf(ChangeUILabel item)
        {
            return lstLabel.IndexOf(item);
        }

        public void Insert(int index, ChangeUILabel item)
        {
            lstLabel.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            lstLabel.RemoveAt(index);
        }

        public ChangeUILabel this[int index]
        {
            get
            {
                return lstLabel[index];
            }
            set
            {
                lstLabel[index] = value;
            }
        }
        #endregion

        #region ICollection<ChangeUILabel> Members
        public void Add(ChangeUILabel item)
        {
            lstLabel.Add(item);
        }

        public void Clear()
        {
            lstLabel.Clear();
        }

        public bool Contains(ChangeUILabel item)
        {
            return lstLabel.Contains(item);
        }

        public void CopyTo(ChangeUILabel[] array, int arrayIndex)
        {
            lstLabel.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return lstLabel.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChangeUILabel item)
        {
            return lstLabel.Remove(item);
        }
        #endregion

        #region IEnumerable<ChangeUILabel> Members

        public IEnumerator<ChangeUILabel> GetEnumerator()
        {
            return lstLabel.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return lstLabel.GetEnumerator();
        }

        #endregion

        #region FindLabel
        public ChangeUILabel FindLabel(string labelString)
        {
            return lstLabel.FirstOrDefault(r => labelString.Contains(r.OldName));
        }
        #endregion
    }

    public class ChangeLabelControl
    {
        ChangeUILabelCollection _listLabel = null;
        static bool _changeValueGrid = false;

        public static bool ChangeValueGrid
        {
            get { return ChangeLabelControl._changeValueGrid; }
            set { ChangeLabelControl._changeValueGrid = value; }
        }
        static bool _changeValueTextbox = false;

        public static bool ChangeValueTextbox
        {
            get { return ChangeLabelControl._changeValueTextbox; }
            set { ChangeLabelControl._changeValueTextbox = value; }
        }

        static ChangeLabelControl _defaultChangeLabelController = null;

        internal static ChangeLabelControl DefaultChangeLabelController
        {
            get { return _defaultChangeLabelController; }
        }

        public static bool IsChangedFromLoad(Control ctrl)
        {
            bool isLoad = false;
            try
            {
                dxfrmExtend baseForm = ctrl as dxfrmExtend;
                if (ctrl != null)
                    isLoad = baseForm.flagChangeLabel;
            }
            catch { }
            return isLoad;
        }

        protected virtual ChangeUILabelCollection ListLabel
        {
            get { return _listLabel; }
        }



        public ChangeLabelControl()
        {
            _listLabel = new ChangeUILabelCollection();
            ChangeLabelControl._defaultChangeLabelController = this;
        }

        public ChangeLabelControl(ChangeUILabelCollection listLabel)
        {
            this._listLabel = listLabel;
            ChangeLabelControl._defaultChangeLabelController = this;
        }

        #region  ChangeLabel
        void ChangeLabel(MenuItem Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            foreach (MenuItem item in Parent.MenuItems)
            {
                ChangeLabel(item);
            }
        }

        void ChangeLabel(ToolStripItem Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            Parent.TextChanged += new EventHandler(ToolStripItem_TextChanged);
        }

        void ToolStripItem_TextChanged(object sender, EventArgs e)
        {
            ToolStripItem Parent = sender as ToolStripItem;
            if (Parent != null)
            {
                ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
                if (label != null)
                    Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            }
        }

        Hashtable hashControls = new Hashtable();

        public virtual void ChangeLabel(Control Parent)
        {
            try
            {
                if (hashControls.ContainsKey(Parent))
                    return;
                else
                    hashControls.Add(Parent, Parent.Handle);
                Parent.HandleDestroyed += new EventHandler(Parent_HandleDestroyed);

                if (this.ListLabel.Count == 0) return;
                ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
                if (label != null)
                {
                    if (Parent is TextBoxBase)
                    {
                        if (ChangeLabelControl.ChangeValueTextbox)
                            Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
                    }
                    else
                        Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
                }

                Parent.TextChanged += new EventHandler(Control_TextChanged);
                if (Parent is DataGridView)
                {
                    foreach (DataGridViewColumn col in ((DataGridView)Parent).Columns)
                    {
                        label = this.ListLabel.FindLabel(col.HeaderText);
                        if (label != null)
                            col.HeaderText = col.HeaderText.Replace(label.OldName, label.NewName);
                    }
                    ((DataGridView)Parent).CellValueChanged += new DataGridViewCellEventHandler(ChangeLabelControl_CellValueChanged);
                }
                else if (Parent is UltraGrid)
                {
                    foreach (UltraGridBand band in ((UltraGrid)Parent).DisplayLayout.Bands)
                    {
                        foreach (UltraGridColumn col in band.Columns)
                        {
                            label = this.ListLabel.FindLabel(col.Header.Caption);
                            if (label != null)
                                col.Header.Caption = col.Header.Caption.Replace(label.OldName, label.NewName);
                        }
                    }
                    ((UltraGrid)Parent).PropertyChanged += new Infragistics.Win.PropertyChangedEventHandler(ChangeLabelControl_PropertyChanged);
                }
                else if (Parent is GridControl)
                {
                    foreach (var view in ((GridControl)Parent).Views)
                    {
                        ColumnView cView = view as ColumnView;
                        if (cView != null)
                        {
                            foreach (GridColumn col in cView.Columns)
                            {
                                label = this.ListLabel.FindLabel(col.Caption);
                                if (label != null)
                                    col.Caption = col.Caption.Replace(label.OldName, label.NewName);
                            }
                            cView.ColumnChanged += new EventHandler(cView_ColumnChanged);
                        }
                    }
                }
                else if (Parent is BaseControl)
                {
                    if (((BaseControl)Parent).SuperTip != null)
                    {
                        foreach (ToolTipItem tip in ((BaseControl)Parent).SuperTip.Items)
                        {
                            label = this.ListLabel.FindLabel(tip.Text);
                            if (label != null)
                                tip.Text = tip.Text.Replace(label.OldName, label.NewName);
                        }
                    }
                    else if (((BaseControl)Parent).ToolTip != "")
                    {
                        label = this.ListLabel.FindLabel(((BaseControl)Parent).ToolTip);
                        if (label != null)
                            ((BaseControl)Parent).ToolTip = ((BaseControl)Parent).ToolTip.Replace(label.OldName, label.NewName);
                    }
                }
                //
                PropertyInfo pitems = null;

                if ((pitems = Parent.GetType().GetProperty("Items")) != null)
                {

                    try
                    {
                        FieldInfo fItems = Parent.GetType().GetField("itemsCollection", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (fItems == null)
                            fItems = Parent.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
                        IList items = fItems.GetValue(Parent) as IList;
                        for (int i = 0; i < items.Count; i++)
                        {
                            string v1 = items[i].ToString();
                            label = this.ListLabel.FindLabel(v1);
                            if (label != null)
                                items[i] = v1.Replace(label.OldName, label.NewName);
                        }
                    }
                    catch { }
                }

                //
                if (Parent.ContextMenu != null)
                {
                    foreach (MenuItem item in Parent.ContextMenu.MenuItems)
                    {
                        ChangeLabel(item);
                    }

                }
                if (Parent.ContextMenuStrip != null)
                {
                    foreach (ToolStripItem item in Parent.ContextMenuStrip.Items)
                    {
                        ChangeLabel(item);
                    }
                }
                try
                {
                    System.Reflection.FieldInfo fInfo = Parent.GetType().GetField("components", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    System.ComponentModel.Container compInfo = fInfo.GetValue(Parent) as System.ComponentModel.Container;
                    foreach (var ctrl in compInfo.Components)
                    {
                        if (ctrl is ContextMenu)
                        {
                            foreach (MenuItem item in ((ContextMenu)ctrl).MenuItems)
                            {
                                ChangeLabel(item);
                            }
                        }
                        else if (ctrl is ContextMenuStrip)
                        {
                            foreach (ToolStripItem item in ((ContextMenuStrip)ctrl).Items)
                            {
                                ChangeLabel(item);
                            }
                        }
                        else if (ctrl is System.Windows.Forms.ToolTip)
                        {
                            try
                            {
                                System.Reflection.FieldInfo tipField = ctrl.GetType().GetField("tools", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                                Hashtable tipHash = tipField.GetValue(ctrl) as Hashtable;

                                System.Reflection.FieldInfo topLevel = ctrl.GetType().GetField("topLevelControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                                Control topCtrl = topLevel.GetValue(ctrl) as Control;
                                System.Reflection.FieldInfo tipWindowInfo = ctrl.GetType().GetField("window", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                                IWin32Window tipWindow = tipWindowInfo.GetValue(ctrl) as IWin32Window;

                                foreach (System.Collections.DictionaryEntry tipInfo in tipHash)
                                {
                                    System.Reflection.FieldInfo tipInfoValue = tipInfo.Value.GetType().GetField("caption", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                                    string caption = tipInfoValue.GetValue(tipInfo.Value) as string;
                                    Control tipInfoControl = tipInfo.Key as Control;
                                    ToolTipTrySetArgs args = new ToolTipTrySetArgs { Caption = caption };
                                    OnToolTipTrySet(tipInfoControl, args);
                                    caption = args.Caption;
                                    bool flag4;
                                    TOOLINFO_TOOLTIP lParam = this.GetTOOLINFO(topCtrl, tipInfoControl, caption, out flag4);
                                    SendMessage(new HandleRef(ctrl, tipWindow.Handle), TTM_SETTOOLINFO, 0, lParam);
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }

                //
                if (Parent is DevExpress.XtraBars.Ribbon.RibbonControl)
                {
                    DevExpress.XtraBars.Ribbon.RibbonControl ribbon = Parent as DevExpress.XtraBars.Ribbon.RibbonControl;
                    foreach (DevExpress.XtraBars.Ribbon.RibbonPage p in ribbon.Pages)
                    {
                        ChangeLabel(p);
                    }

                    foreach (DevExpress.XtraBars.BarItemLink item in ribbon.Toolbar.ItemLinks)
                    {
                        ChangeLabel(item.Item);
                    }
                }
                else if (Parent is DevExpress.XtraTab.XtraTabControl)
                {
                    foreach (DevExpress.XtraTab.XtraTabPage tPage in ((DevExpress.XtraTab.XtraTabControl)Parent).TabPages)
                    {
                        ChangeLabel(tPage);
                    }

                }
                else
                {
                    foreach (Control ctrl in Parent.Controls)
                        ChangeLabel(ctrl);
                }
            }
            catch { }
        }

        void Parent_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                hashControls.Remove(sender);
            }
            catch { }
        }

        public static event ToolTipTrySetHandler ToolTipTrySet;

        internal static void OnToolTipTrySet(object sender, ToolTipTrySetArgs args)
        {
            if (ToolTipTrySet != null)
            {
                ToolTipTrySet(sender, args);
            }
        }

        #region ToolTip Attach
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, TOOLINFO_TOOLTIP lParam);
        internal static IntPtr InvalidIntPtr = ((IntPtr)(-1));
        internal static int TTM_SETTOOLINFO = Marshal.SystemDefaultCharSize == 1 ? 0x409 : 0x436;

        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public Size Size
            {
                get
                {
                    return new Size(this.right - this.left, this.bottom - this.top);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class TOOLINFO_TOOLTIP
        {
            public int cbSize = Marshal.SizeOf(typeof(TOOLINFO_TOOLTIP));
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public RECT rect;
            public IntPtr hinst = IntPtr.Zero;
            public IntPtr lpszText;
            public IntPtr lParam = IntPtr.Zero;
        }

        private TOOLINFO_TOOLTIP GetMinTOOLINFO(Control ctl)
        {
            return new TOOLINFO_TOOLTIP { cbSize = Marshal.SizeOf(typeof(TOOLINFO_TOOLTIP)), hwnd = ctl.Handle, uFlags = 1, uId = ctl.Handle };
        }

        private TOOLINFO_TOOLTIP GetTOOLINFO(Control topLevelControl, Control ctl, string caption, out bool allocatedString)
        {
            allocatedString = false;
            TOOLINFO_TOOLTIP minTOOLINFO = GetMinTOOLINFO(ctl);
            minTOOLINFO.cbSize = Marshal.SizeOf(typeof(TOOLINFO_TOOLTIP));
            minTOOLINFO.uFlags |= 0x110;
            if (((topLevelControl != null) && (topLevelControl.RightToLeft == RightToLeft.Yes)) && !ctl.IsMirrored)
            {
                minTOOLINFO.uFlags |= 4;
            }
            if ((ctl is TreeView) || (ctl is ListView))
            {
                TreeView view = ctl as TreeView;
                if ((view != null) && view.ShowNodeToolTips)
                {
                    minTOOLINFO.lpszText = InvalidIntPtr;
                    return minTOOLINFO;
                }
                ListView view2 = ctl as ListView;
                if ((view2 != null) && view2.ShowItemToolTips)
                {
                    minTOOLINFO.lpszText = InvalidIntPtr;
                    return minTOOLINFO;
                }
                minTOOLINFO.lpszText = Marshal.StringToHGlobalAuto(caption);
                allocatedString = true;
                return minTOOLINFO;
            }
            minTOOLINFO.lpszText = Marshal.StringToHGlobalAuto(caption);
            allocatedString = true;
            return minTOOLINFO;
        }
        #endregion

        void cView_ColumnChanged(object sender, EventArgs e)
        {
            try
            {
                GridColumn colView = sender as GridColumn;
                if (colView != null)
                {
                    ChangeUILabel label = this.ListLabel.FindLabel(colView.Caption);
                    if (label != null)
                        colView.Caption = colView.Caption.Replace(label.OldName, label.NewName);
                }
            }
            catch { }
        }

        void ChangeLabelControl_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                DataGridView grd = sender as DataGridView;
                if (grd.Columns[e.ColumnIndex] != null)
                {
                    ChangeUILabel label = this.ListLabel.FindLabel(grd.Columns[e.ColumnIndex].HeaderText);
                    if (label != null)
                        grd.Columns[e.ColumnIndex].HeaderText = grd.Columns[e.ColumnIndex].HeaderText.Replace(label.OldName, label.NewName);
                }
            }
        }

        void ChangeLabelControl_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            if ((PropertyIds)e.ChangeInfo.PropId == PropertyIds.DisplayLayout)
            {
                Infragistics.Shared.PropChangeInfo col = e.ChangeInfo.Trigger.FindPropId(PropertyIds.Caption);
                if (col != null)
                {
                    Infragistics.Win.UltraWinGrid.HeaderBase head = col.Source as Infragistics.Win.UltraWinGrid.HeaderBase;
                    ChangeUILabel label = this.ListLabel.FindLabel(head.Caption);
                    if (label != null)
                        head.Caption = head.Caption.Replace(label.OldName, label.NewName);
                }
            }
        }

        void Control_TextChanged(object sender, EventArgs e)
        {
            Control Parent = sender as Control;
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
            {
                if (Parent.GetType().GetProperty("DataSource") == null && Parent.GetType().GetProperty("Items") == null)
                {
                    Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
                }
            }
        }

        void ChangeLabel(DevExpress.XtraBars.Ribbon.RibbonPage Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);

            foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup gPage in Parent.Groups)
            {
                ChangeLabel(gPage);
            }
        }

        void ChangeLabel(DevExpress.XtraBars.Ribbon.RibbonPageGroup Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            foreach (DevExpress.XtraBars.BarItemLink item in Parent.ItemLinks)
            {
                ChangeLabel(item.Item);
            }
        }

        void ChangeLabel(DevExpress.XtraBars.BarItem Parent)
        {
            ChangeUILabel label = null;
            label = this.ListLabel.FindLabel(Parent.Caption);
            if (label != null)
                Parent.Caption = Parent.Caption.Replace(label.OldName, label.NewName);
            if (Parent.SuperTip != null)
            {
                foreach (ToolTipItem tip in Parent.SuperTip.Items)
                {
                    label = this.ListLabel.FindLabel(tip.Text);
                    if (label != null)
                        tip.Text = tip.Text.Replace(label.OldName, label.NewName);
                }
            }

            if (Parent is DevExpress.XtraBars.BarEditItem)
            {
                label = this.ListLabel.FindLabel(((DevExpress.XtraBars.BarEditItem)Parent).EditValue.ToString());
                if (label != null)
                {
                    string s = ((DevExpress.XtraBars.BarEditItem)Parent).EditValue.ToString();
                    ((DevExpress.XtraBars.BarEditItem)Parent).EditValue = s.Replace(label.OldName, label.NewName);
                }
            }
            else if (Parent is DevExpress.XtraBars.BarSubItem)
            {
                foreach (DevExpress.XtraBars.BarItemLink item in ((DevExpress.XtraBars.BarSubItem)Parent).ItemLinks)
                {
                    ChangeLabel(item.Item);
                }
            }
        }

        void ChangeLabel(DevExpress.XtraTab.XtraTabControl Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            foreach (DevExpress.XtraTab.XtraTabPage tPage in Parent.TabPages)
            {
                ChangeLabel(tPage);
            }
        }

        void ChangeLabel(DevExpress.XtraTab.XtraTabPage Parent)
        {
            ChangeUILabel label = this.ListLabel.FindLabel(Parent.Text);
            if (label != null)
                Parent.Text = Parent.Text.Replace(label.OldName, label.NewName);
            foreach (Control ctrl in Parent.Controls)
            {
                if (ctrl is DevExpress.XtraTab.XtraTabControl)
                    ChangeLabel((DevExpress.XtraTab.XtraTabControl)ctrl);
                else
                    ChangeLabel(ctrl);
            }
        }
        #endregion
    }

    public delegate void ToolTipTrySetHandler(object sender, ToolTipTrySetArgs args);

    public class ToolTipTrySetArgs : EventArgs
    {
        public string Caption { get; set; }
    }

    public class Permission
    {
        string _controlID = "";

        public string ControlID
        {
            get { return _controlID; }
            set { _controlID = value; }
        }
        bool _enable = false, _visible = false, _readOnly = false;

        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        public Permission() { }
        public Permission(string controlID, bool enable, bool visible, bool readOnly)
        {
            this._controlID = controlID;
            this._enable = enable;
            this._visible = visible;
            this._readOnly = readOnly;
        }

    }

    public abstract class PermissionUI
    {
        List<string> _ignoreListUser = new List<string>();
        public static bool DefaultVisible { get; set; }
        public static bool DefaultEnable { get; set; }

        static PermissionUI()
        {
            DefaultVisible = true;
            DefaultEnable = false;
        }

        public PermissionUI()
        {
            Init_IgnoreListUser();
        }

        protected List<string> IgnoreListUser
        {
            get
            {
                if (_ignoreListUser == null)
                    _ignoreListUser = new List<string>();
                return _ignoreListUser;
            }
        }

        protected abstract List<Permission> CreatePermission(DataTable dtDecentralizations);
        public abstract bool IgnoreUserPermission { get; }
        protected abstract void Init_IgnoreListUser();

        public virtual void SetPermission(Control Parent, DataTable dtDecentralizations)
        {
            try
            {
                if (this.IgnoreUserPermission) return;

                List<Permission> permission = this.CreatePermission(dtDecentralizations);
                if (permission.Count > 0 || !PermissionUI.DefaultVisible || !PermissionUI.DefaultEnable)
                {
                    SetPermission(Parent, permission);
                }
            }
            catch { }
        }

        #region Set Permission Detail

        #region Set Permission on Control
        void SetPermission(Control Parent, List<Permission> dtPermission)
        {
            if (Parent is DevExpress.XtraBars.Ribbon.RibbonControl)
                SetPermission((DevExpress.XtraBars.Ribbon.RibbonControl)Parent, dtPermission);
            else if (Parent is DevExpress.XtraTab.XtraTabControl)
                SetPermission((DevExpress.XtraTab.XtraTabControl)Parent, dtPermission);
            else if (Parent is CommonLib.UserControls.TabControlExtend)
                SetPermission((CommonLib.UserControls.TabControlExtend)Parent, dtPermission);
            else if (Parent is TabControl)
                SetPermission((TabControl)Parent, dtPermission);
            else
            {

                Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
                if (permission != null)
                {
                    Parent.Visible = permission.Visible;
                    Parent.Enabled = permission.Enable;
                    dtPermission.Remove(permission);
                    if (Parent.Visible)
                    {
                        if (Parent is DevExpress.XtraEditors.TextEdit)
                        {
                            ((DevExpress.XtraEditors.TextEdit)Parent).Properties.ReadOnly = permission.ReadOnly;
                        }
                        else if (Parent is System.Windows.Forms.TextBox)
                        {
                            ((System.Windows.Forms.TextBox)Parent).ReadOnly = permission.ReadOnly;
                        }
                    }
                }
                if (Parent.Visible)
                {
                    foreach (Control ctrl in Parent.Controls)
                    {
                        SetPermission(ctrl, dtPermission);
                    }
                }
            }
        }

        void SetPermission(CommonLib.UserControls.TabControlExtend Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                if (Parent.Visible)
                {
                    foreach (TabPage tPage in Parent.TabPagesX)
                    {
                        permission = dtPermission.FirstOrDefault(p => p.ControlID == tPage.Name);
                        if (permission != null)
                        {
                            if (permission.Visible)
                            {
                                if (!Parent.TabPages.ContainsKey(tPage.Name))
                                    Parent.TabPages.Add(tPage);
                            }
                            else
                            {
                                if (!Parent.TabPages.ContainsKey(tPage.Name))
                                    Parent.TabPages.RemoveByKey(tPage.Name);
                            }
                            dtPermission.Remove(permission);
                        }
                    }
                }
            }
            if (Parent.Visible)
            {
                foreach (TabPage tPage in Parent.TabPages)
                {
                    foreach (Control ctrl in tPage.Controls)
                    {
                        SetPermission(ctrl, dtPermission);
                    }
                }
            }
        }

        void SetPermission(TabControl Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
            }
            if (Parent.Visible)
            {
                foreach (TabPage tPage in Parent.TabPages)
                {
                    foreach (Control ctrl in tPage.Controls)
                    {
                        SetPermission(ctrl, dtPermission);
                    }
                }
            }
        }
        #endregion

        #region Set Permission on Grid
        void SetPermission(UltraGrid Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                if (Parent.Visible)
                {
                    foreach (UltraGridBand band in Parent.DisplayLayout.Bands)
                    {
                        var ePermission = from col in band.Columns.Cast<UltraGridColumn>()
                                          join per in dtPermission on col.Key equals per.ControlID
                                          select new { Column = col, Permission = per };
                        foreach (var col in ePermission)
                        {
                            col.Column.Hidden = !col.Permission.Visible;
                            col.Column.CellActivation = col.Permission.Enable ? Activation.AllowEdit : Activation.NoEdit;
                        }
                    }
                }
            }
        }
        void SetPermission(DataGridView Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                if (Parent.Visible)
                {
                    var ePermission = from col in Parent.Columns.Cast<DataGridViewColumn>()
                                      join per in dtPermission on col.Name equals per.ControlID
                                      select new { Column = col, Permission = per };
                    foreach (var col in ePermission)
                    {
                        col.Column.Visible = col.Permission.Visible;
                        col.Column.ReadOnly = col.Permission.Enable;
                    }
                }
            }
        }

        void SetPermission(GridControl Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                if (Parent.Visible)
                {
                    foreach (ColumnView view in Parent.Views)
                    {
                        var ePermission = from col in view.Columns.Cast<GridColumn>()
                                          join per in dtPermission on col.Name equals per.ControlID
                                          select new { Column = col, Permission = per };
                        foreach (var col in ePermission)
                        {
                            col.Column.Visible = col.Permission.Visible;
                            col.Column.OptionsColumn.AllowEdit = col.Permission.Enable;
                        }
                    }
                }
            }

        }
        #endregion

        #region Set Permission on XtraTabControl
        void SetPermission(DevExpress.XtraTab.XtraTabControl Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
            }
            if (Parent.Visible)
            {
                foreach (DevExpress.XtraTab.XtraTabPage tPage in Parent.TabPages)
                {
                    SetPermission(tPage, dtPermission);
                }
            }
        }

        void SetPermission(DevExpress.XtraTab.XtraTabPage Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.PageVisible = permission.Visible;
                dtPermission.Remove(permission);
            }
            if (Parent.PageVisible)
            {
                foreach (Control ctrl in Parent.Controls)
                {
                    if (ctrl is DevExpress.XtraTab.XtraTabControl)
                        SetPermission((DevExpress.XtraTab.XtraTabControl)ctrl, dtPermission);
                    else
                        SetPermission(ctrl, dtPermission);
                }
            }
        }
        #endregion

        #region SetPermission on Ribbon
        void SetPermission(DevExpress.XtraBars.Ribbon.RibbonControl Parent, List<Permission> dtPermission)
        {
            foreach (DevExpress.XtraBars.Ribbon.RibbonPage rPage in Parent.Pages)
            {
                SetPermission(rPage, dtPermission);
            }
            foreach (DevExpress.XtraBars.BarItemLink item in Parent.Toolbar.ItemLinks)
            {
                SetPermission(item.Item, dtPermission);
            }

            foreach (DevExpress.XtraBars.BarItemLink item in Parent.StatusBar.ItemLinks)
            {
                SetPermission(item.Item, dtPermission);
            }

        }

        void SetPermission(DevExpress.XtraBars.Ribbon.RibbonPage Parent, List<Permission> dtPermission)
        {
            bool visCount = false;
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                dtPermission.Remove(permission);
            }
            foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup gPage in Parent.Groups)
            {
                SetPermission(gPage, dtPermission);
                if (gPage.Visible)
                    visCount = true;
            }
            if (visCount)
                Parent.Visible = true;
            else
                Parent.Visible = PermissionUI.DefaultVisible;
        }

        void SetPermission(DevExpress.XtraBars.Ribbon.RibbonPageGroup Parent, List<Permission> dtPermission)
        {
            bool visCount = false;
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
            if (permission != null)
            {
                Parent.Visible = permission.Visible;
                dtPermission.Remove(permission);
            }
            if (Parent.Visible)
            {
                foreach (DevExpress.XtraBars.BarItemLink item in Parent.ItemLinks)
                {
                    SetPermission(item.Item, dtPermission);
                    if (item.Item.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
                        visCount = true;
                }
                if (visCount)
                    Parent.Visible = true;
                else
                    Parent.Visible = PermissionUI.DefaultVisible;
            }
        }

        void SetPermission(DevExpress.XtraBars.BarSubItem Parent, List<Permission> dtPermission)
        {
            Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);

            if (permission != null)
            {
                Parent.Visibility = permission.Visible ? DevExpress.XtraBars.BarItemVisibility.Always : (PermissionUI.DefaultVisible ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                Parent.Enabled = permission.Enable;
                dtPermission.Remove(permission);
            }
            if (Parent.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
            {
                bool visCount = false;
                foreach (DevExpress.XtraBars.BarItemLink item in Parent.ItemLinks)
                {
                    SetPermission(item.Item, dtPermission);
                    if (item.Item.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
                        visCount = true;
                }
                if (visCount)
                    Parent.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    Parent.Visibility = (PermissionUI.DefaultVisible ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
            }
        }

        void SetPermission(DevExpress.XtraBars.BarItem Parent, List<Permission> dtPermission)
        {
            if (Parent is DevExpress.XtraBars.BarSubItem)
            {
                SetPermission(((DevExpress.XtraBars.BarSubItem)Parent), dtPermission);
            }
            else
            {
                Permission permission = dtPermission.FirstOrDefault(p => p.ControlID == Parent.Name);
                if (permission != null)
                {
                    Parent.Visibility = permission.Visible ? DevExpress.XtraBars.BarItemVisibility.Always : (PermissionUI.DefaultVisible ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                    Parent.Enabled = permission.Enable;
                    dtPermission.Remove(permission);

                    if (Parent is DevExpress.XtraBars.BarEditItem)
                    {
                        ((DevExpress.XtraBars.BarEditItem)Parent).Edit.ReadOnly = permission.ReadOnly;
                    }

                }
                else
                {
                    Parent.Visibility = PermissionUI.DefaultVisible ? DevExpress.XtraBars.BarItemVisibility.Always : (PermissionUI.DefaultVisible ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                    Parent.Enabled = PermissionUI.DefaultEnable;
                }
            }
        }
        #endregion

        #endregion
    }

    public class GridSearch
    {
        static List<ObjChooseFilter> list = new List<ObjChooseFilter> { };

        #region ApplySearch
        public static void ApplySearch(object Grid, Control txtSearch, bool IsDataViewFilter, string[] DefaultSearchColumn)
        {
            try
            {
                Form frm = ((Control)Grid).FindForm();

                if (frm != null)
                {
                    if (!list.Any(o => o.TxtSearch == txtSearch))
                    {
                        list.Add(new ObjChooseFilter(frm, Grid, txtSearch, DefaultSearchColumn, IsDataViewFilter));
                        CommonLib.ShortKeyReg.RegisterHotKey(frm, ShowFormChooseColumnFilters, Keys.Shift | Keys.F3);
                        frm.FormClosing += new FormClosingEventHandler(frm_FormClosing);
                        txtSearch.KeyUp += new KeyEventHandler(txtSearch_KeyUp);
                    }
                }
            }
            catch { }
        }

        public static void ApplySearch(object Grid, Control txtSearch, bool IsDataViewFilter, string[] DefaultSearchColumn, bool NotifyNotFound)
        {
            try
            {
                if (NotifyNotFound)
                {
                    ApplySearch(Grid, txtSearch, IsDataViewFilter, DefaultSearchColumn);
                }
                else
                {
                    Form frm = ((Control)Grid).FindForm();

                    if (frm != null)
                    {
                        if (!list.Any(o => o.TxtSearch == txtSearch))
                        {
                            list.Add(new ObjChooseFilter(frm, Grid, txtSearch, DefaultSearchColumn, IsDataViewFilter, NotifyNotFound));
                            CommonLib.ShortKeyReg.RegisterHotKey(frm, ShowFormChooseColumnFilters, Keys.Shift | Keys.F3);
                            frm.FormClosing += new FormClosingEventHandler(frm_FormClosing);
                            txtSearch.KeyUp += new KeyEventHandler(txtSearch_KeyUp);
                        }
                    }
                }
            }
            catch { }
        }

        static void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Form frm = sender as Form;
                if (frm == null) return;
                ObjChooseFilter objFilter = list.FirstOrDefault(f => f.FormSearch == frm);
                if (objFilter == null) return;
                list.Remove(objFilter);
            }
            catch { }
        }

        const int EM_SETSEL = 0x00B1;
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        static void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Control txt = sender as Control;
                ObjChooseFilter objFilter = list.Find(delegate(ObjChooseFilter obj)
                {
                    return obj.TxtSearch == txt;
                });

                if (objFilter == null) return;
                string val = txt.Text.Trim().ToLower();

                if (objFilter.IsDataViewFilter)
                {
                    DataView dv = null;
                    if (objFilter.Grid is DataGridView)
                        dv = ((DataGridView)objFilter.Grid).DataSource as DataView;
                    else if (objFilter.Grid is UltraGrid)
                        dv = ((UltraGrid)objFilter.Grid).DataSource as DataView;
                    else if (objFilter.Grid is GridControl)
                        dv = ((GridControl)objFilter.Grid).DataSource as DataView;
                    if (dv != null)
                    {
                        string filter = string.Empty;
                        foreach (string col in objFilter.DefaultSearch)
                        {
                            if (dv.Table.Columns[col].DataType == typeof(string) || dv.Table.Columns[col].DataType == typeof(DateTime))
                            {
                                filter += (filter == "" ? "" : " or ") + col + " like '%" + val + "%'";
                            }
                            else
                            {
                                filter += (filter == "" ? "" : " or ") + col + " =" + val;
                            }
                        }

                        dv.RowFilter = filter;
                        SendMessage(txt.Handle, EM_SETSEL, 0, -1);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        bool match = false;

                        if (objFilter.Grid is UltraGrid)
                        {
                            #region Tim all UltraGrid
                            IEnumerable<UltraGridRow> rows = ((UltraGrid)objFilter.Grid).Rows.Cast<UltraGridRow>();

                            string ColumnMatched = "";
                            UltraGridRow rFind = FindRow(val, objFilter.DefaultSearch, rows, ref ColumnMatched);
                            if (rFind != null)
                            {
                                ((UltraGrid)objFilter.Grid).Selected.Rows.Clear();
                                rFind.Activate();
                                if (ColumnMatched != "")
                                    rFind.Cells[ColumnMatched].Activate();
                                match = true;
                            }
                            #endregion
                        }
                        else if (objFilter.Grid is DataGridView)
                        {
                            IEnumerable<DataGridViewRow> allRow = ((DataGridView)objFilter.Grid).Rows.Cast<DataGridViewRow>();
                            #region Tim all DataGridViewRow
                            string colFind = "";
                            DataGridViewRow rFind = FindRow(val, objFilter.DefaultSearch, allRow, ref colFind);
                            if (rFind != null)
                            {
                                ((DataGridView)objFilter.Grid).ClearSelection();
                                rFind.Selected = true;
                                ((DataGridView)objFilter.Grid).CurrentCell = rFind.Cells[colFind];
                                match = true;
                            }
                            #endregion
                        }
                        else if (objFilter.Grid is GridControl)
                        {
                            GridView view = ((GridControl)objFilter.Grid).MainView as GridView;
                            match = FindRow(val, objFilter.DefaultSearch, view, 0, 0);
                        }
                        else
                            return;
                        SendMessage(txt.Handle, EM_SETSEL, 0, -1);
                        if (!match && objFilter.NotifyNotFound) XtraMessageBox.Show("Không tìm thấy mẫu tin nào", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (e.KeyCode == Keys.F3 && !e.Shift && !e.Alt && !e.Control)
                    {
                        bool match = false;
                        if (objFilter.Grid is UltraGrid)
                        {
                            bool includeCurRow = false;
                            UltraGridRow curRow = ((UltraGrid)objFilter.Grid).ActiveRow;
                            if (curRow == null && ((UltraGrid)objFilter.Grid).ActiveCell != null)
                                curRow = ((UltraGrid)objFilter.Grid).ActiveCell.Row;
                            if (curRow == null && ((UltraGrid)objFilter.Grid).Rows.Count > 0)
                            {
                                curRow = ((UltraGrid)objFilter.Grid).Rows[0];
                                includeCurRow = true;
                            }
                            string colFind = "";
                            UltraGridRow rFind = FindRow(val, objFilter.DefaultSearch, curRow, ref colFind, includeCurRow);
                            if (rFind != null)
                            {
                                ((UltraGrid)objFilter.Grid).Selected.Rows.Clear();
                                rFind.Activate();
                                rFind.Cells[colFind].Activate();
                                match = true;
                            }
                        }
                        else if (objFilter.Grid is DataGridView)
                        {
                            int index = 0;

                            DataGridViewRow curRow = null;
                            if (((DataGridView)objFilter.Grid).CurrentRow != null)
                                curRow = ((DataGridView)objFilter.Grid).CurrentRow;
                            else if (((DataGridView)objFilter.Grid).CurrentCell != null)
                                curRow = ((DataGridView)objFilter.Grid).Rows[((DataGridView)objFilter.Grid).CurrentCell.RowIndex];
                            if (curRow != null)
                                index = curRow.Index + 1;
                            IEnumerable<DataGridViewRow> allRow = ((DataGridView)objFilter.Grid).Rows.Cast<DataGridViewRow>().Where(r => r.Index >= index);
                            #region Tim cac row ben duoi DataGridViewRow
                            string colFind = "";
                            DataGridViewRow rFind = FindRow(val, objFilter.DefaultSearch, allRow, ref colFind);
                            if (rFind != null)
                            {
                                ((DataGridView)objFilter.Grid).ClearSelection();
                                rFind.Selected = true;
                                ((DataGridView)objFilter.Grid).CurrentCell = rFind.Cells[colFind];
                                match = true;
                            }
                            #endregion
                        }
                        else if (objFilter.Grid is GridControl)
                        {
                            #region
                            GridView view = ((GridControl)objFilter.Grid).FocusedView as GridView;
                            match = FindRow(val, objFilter.DefaultSearch, view, -1, -1);
                            #endregion
                        }
                        else
                            return;
                        SendMessage(txt.Handle, EM_SETSEL, 0, -1);
                        if (!match && objFilter.NotifyNotFound) XtraMessageBox.Show("Đã tìm hết danh sách", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Function FindRow
        static UltraGridRow FindRow(string SearchValue, string[] ColumnSearch, IEnumerable<UltraGridRow> listRow, ref string ColumnMatched)
        {
            if (listRow == null) return null;
            UltraGridRow rFind = FindRowExt.FindRow(listRow).FirstOrDefault(r => ColumnSearch.Any(c => r.Band.Columns.Exists(c) && !r.Band.Columns[c].Hidden && r.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0));

            if (rFind != null)
            {
                ColumnMatched = ColumnSearch.FirstOrDefault(c => rFind.Band.Columns.Exists(c) && !rFind.Band.Columns[c].Hidden && rFind.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                return rFind;
            }
            return null;
        }

        static UltraGridRow FindRow(string SearchValue, string[] ColumnSearch, UltraGridRow BeginRow, ref string ColumnMatched, bool includeBeginRow)
        {
            if (BeginRow == null) return null;
            if (includeBeginRow)
            {
                if (ColumnSearch.Any(c => BeginRow.Band.Columns.Exists(c) && !BeginRow.Band.Columns[c].Hidden && BeginRow.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    ColumnMatched = ColumnSearch.FirstOrDefault(c => BeginRow.Band.Columns.Exists(c) && !BeginRow.Band.Columns[c].Hidden && BeginRow.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                    return BeginRow;
                }
            }
            //Tim child row
            if (BeginRow.ChildBands != null)
            {
                foreach (UltraGridChildBand band in BeginRow.ChildBands)
                {
                    IEnumerable<UltraGridRow> allRow = band.Rows.Cast<UltraGridRow>();
                    UltraGridRow mRow = FindRow(SearchValue, ColumnSearch, allRow, ref ColumnMatched);
                    if (mRow != null) return mRow;
                }
            }
            //Tim row cung Band va trong Band
            while (BeginRow.HasNextSibling())
            {
                BeginRow = BeginRow.GetSibling(SiblingRow.Next);
                if (ColumnSearch.Any(c => BeginRow.Band.Columns.Exists(c) && !BeginRow.Band.Columns[c].Hidden && BeginRow.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    ColumnMatched = ColumnSearch.FirstOrDefault(c => BeginRow.Band.Columns.Exists(c) && !BeginRow.Band.Columns[c].Hidden && BeginRow.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                    return BeginRow;
                }

                if (BeginRow.ChildBands != null)
                {
                    foreach (UltraGridChildBand band in BeginRow.ChildBands)
                    {
                        IEnumerable<UltraGridRow> allRow = band.Rows.Cast<UltraGridRow>();
                        UltraGridRow mRow = FindRow(SearchValue, ColumnSearch, allRow, ref ColumnMatched);
                        if (mRow != null) return mRow;
                    }
                }
            }
            //Tim row cung Parent khac Band
            if (BeginRow.ParentRow != null)
            {
                foreach (UltraGridChildBand band in BeginRow.ParentRow.ChildBands)
                {
                    if (band.Index > BeginRow.Band.Index)
                    {
                        IEnumerable<UltraGridRow> allRow = band.Rows.Cast<UltraGridRow>();
                        UltraGridRow mRow = FindRow(SearchValue, ColumnSearch, allRow, ref ColumnMatched);
                        if (mRow != null) return mRow;
                    }
                }
            }
            //Tim row con lai tiep theo
            if (BeginRow.ParentRow != null)
            {
                if (BeginRow.ParentRow.HasNextSibling())
                {
                    BeginRow = BeginRow.ParentRow.GetSibling(SiblingRow.Next);
                    return FindRow(SearchValue, ColumnSearch, BeginRow, ref ColumnMatched, false);
                }
            }
            return null;
        }

        static DataGridViewRow FindRow(string SearchValue, string[] ColumnSearch, IEnumerable<DataGridViewRow> listRow, ref string ColumnMatched)
        {
            if (listRow == null) return null;
            DataGridViewRow mRow = listRow.FirstOrDefault(r => ColumnSearch.Any(c => r.DataGridView.Columns.Contains(c) && r.DataGridView.Columns[c].Visible && r.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0));
            if (mRow != null)
            {
                ColumnMatched = ColumnSearch.FirstOrDefault(c => mRow.DataGridView.Columns.Contains(c) && mRow.DataGridView.Columns[c].Visible && mRow.Cells[c].Value.ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                return mRow;
            }
            return null;
        }

        static void ScrollView(BaseView view, int rowIndex)
        {
            try
            {
                if (view != null)
                {
                    System.Reflection.FieldInfo fInfo = typeof(GridView).GetField("scrollInfo", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    DevExpress.XtraGrid.Scrolling.ScrollInfo scrollInfo = fInfo.GetValue(view) as DevExpress.XtraGrid.Scrolling.ScrollInfo;
                    scrollInfo.VScroll.Value = rowIndex;
                }
            }
            catch { }
        }

        static bool FindRow(string SearchValue, string[] ColumnSearch, GridView view, int BeginRow, int ParentRow)
        {
            int index = -1;
            if (BeginRow >= 0)
                index = BeginRow;
            else
                index = view.FocusedRowHandle + 1;
            GridView parentView = view.ParentView as GridView;
            if (ParentRow < 0 && parentView != null)
            {
                for (int i = 0; i < parentView.RowCount; i++)
                {
                    if (parentView.GetVisibleDetailView(i) == view)
                    {
                        ParentRow = i;
                        break;
                    }
                }
            }
            if (ParentRow < 0) ParentRow = 0;

            DevExpress.Data.DataController dataCtrl = view.DataController;

            for (int i = index; i < view.RowCount; i++)
            {
                if (ColumnSearch.Any(c => view.Columns.ColumnByFieldName(c) != null && view.Columns[c].Visible && view.GetRowCellValue(i, c).ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    view.ClearSelection();
                    view.GridControl.FocusedView = view;
                    view.MakeRowVisible(i, true);
                    view.FocusedRowHandle = i;
                    string ColumnMatched = ColumnSearch.FirstOrDefault(c => view.Columns.ColumnByFieldName(c) != null && view.Columns[c].Visible && view.GetRowCellValue(i, c).ToString().IndexOf(SearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                    view.FocusedColumn = view.Columns[ColumnMatched];
                    ScrollView(view.ParentView, ParentRow);
                    return true;
                }
                if (dataCtrl.IsDetailRowExpanded(i))
                {
                    GridView cView = view.GetVisibleDetailView(i) as GridView;
                    bool isFound = false;
                    isFound = FindRow(SearchValue, ColumnSearch, cView, 0, i);
                    if (isFound) return true;
                }
                else
                {
                    view.ExpandMasterRow(i);
                    GridView cView = view.GetVisibleDetailView(i) as GridView;
                    bool isFound = false;
                    if (cView != null)
                        isFound = FindRow(SearchValue, ColumnSearch, cView, 0, i);
                    if (!isFound)
                        view.CollapseMasterRow(i);
                    else
                        return true;
                }
            }
            if (view.ParentView != null)
            {
                return FindRow(SearchValue, ColumnSearch, parentView, ParentRow + 1, -1);
            }
            return false;
        }
        #endregion

        static void ShowFormChooseColumnFilters(object sender, EventArgs e)
        {
            try
            {
                Form f = Form.ActiveForm;
                if (f.IsMdiContainer)
                    f = f.ActiveMdiChild;
                TextBox txt = f.ActiveControl as TextBox;
                if (txt == null) return;
                ObjChooseFilter objFilter = list.Find(delegate(ObjChooseFilter obj)
                {
                    return obj.TxtSearch == txt;
                });

                if (objFilter == null) return;

                CommonLib.FormInputValue.frmChooseColumnFilters frm = new CommonLib.FormInputValue.frmChooseColumnFilters();
                frm.DefaultSearchColumn = objFilter.DefaultSearch;
                if (objFilter.Grid is DataGridView)
                    frm.LoadData((DataGridView)objFilter.Grid);
                else if (objFilter.Grid is UltraGrid)
                    frm.LoadData((UltraGrid)objFilter.Grid);
                else if (objFilter.Grid is GridControl)
                    frm.LoadData((GridControl)objFilter.Grid);

                if (frm.ShowDialog(f) == DialogResult.OK)
                {
                    objFilter.DefaultSearch = frm.SelectedColumn;
                }
            }
            catch { }
        }
    }

    internal static class FindRowExt
    {
        internal static IEnumerable<UltraGridRow> FindRow(this IEnumerable<UltraGridRow> parent)
        {
            foreach (UltraGridRow child in parent)
            {
                yield return child;

                if (child.ChildBands != null)
                {
                    foreach (UltraGridChildBand band in child.ChildBands)
                    {
                        IEnumerable<UltraGridRow> cRow = band.Rows.Cast<UltraGridRow>();
                        foreach (UltraGridRow grandChild in FindRow(cRow))
                            yield return grandChild;
                    }
                }
            }
        }
    }

    public class UltraGridDefaultConfig
    {
        class FKeys
        {
            string[] _readOnlyColumn = null;
            public string[] ReadOnlyColumn
            {
                get
                {
                    if (this._readOnlyColumn == null)
                        this._readOnlyColumn = new string[] { };

                    return this._readOnlyColumn;
                }
                set
                {
                    this._readOnlyColumn = value;
                }
            }
            public ContextMenuStrip CopyPasteMenu { get; set; }
            public ToolStripMenuItem CopyMenu { get; set; }
            public ToolStripMenuItem PasteMenu { get; set; }
        }

        class FExpand
        {
            public ContextMenuStrip cmsExpand { get; set; }
            public ToolStripMenuItem mnuExpAll { get; set; }
            public ToolStripMenuItem mnuColAll { get; set; }
        }

        static Dictionary<UltraGrid, FExpand> ExpandMenu = new Dictionary<UltraGrid, FExpand>();

        #region InitExpand

        static void InitExpand(UltraGrid grid)
        {
            if (!ExpandMenu.ContainsKey(grid))
            {                

                ContextMenuStrip cmsExpand = new ContextMenuStrip();
                cmsExpand.Tag = grid;
                ToolStripMenuItem mnuExpAll = new ToolStripMenuItem();
                ToolStripMenuItem mnuColAll = new ToolStripMenuItem();
                cmsExpand.SuspendLayout();
                cmsExpand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                mnuExpAll,
                mnuColAll});
                cmsExpand.Name = "cmsExpand";
                cmsExpand.Size = new System.Drawing.Size(146, 48);
                mnuExpAll.Name = "mnuExpAll";
                mnuExpAll.Size = new System.Drawing.Size(145, 22);
                mnuExpAll.Text = "Mở rộng tất cả";
                mnuExpAll.Click += new System.EventHandler(mnuExpAll_Click);
                mnuExpAll.Tag = cmsExpand;
                mnuColAll.Name = "mnuColAll";
                mnuColAll.Size = new System.Drawing.Size(145, 22);
                mnuColAll.Text = "Thu nhỏ tất cả";
                mnuColAll.Click += new System.EventHandler(mnuColAll_Click);
                mnuColAll.Tag = cmsExpand;
                cmsExpand.ResumeLayout(false);
                grid.MouseClick += new MouseEventHandler(grid_MouseClick1);
                grid.Disposed+=new EventHandler(grid_Disposed);

                ExpandMenu.Add(grid, new FExpand { cmsExpand = cmsExpand, mnuColAll = mnuColAll, mnuExpAll = mnuExpAll });
            }
        }

        static void grid_MouseClick1(object sender, MouseEventArgs e)
        {
            try
            {
                UltraGrid grid = sender as UltraGrid;
                if (grid != null && e.Button == MouseButtons.Right)
                {
                    Infragistics.Win.UltraWinGrid.ExpansionIndicatorUIElement element = grid.DisplayLayout.UIElement.LastElementEntered as Infragistics.Win.UltraWinGrid.ExpansionIndicatorUIElement;
                    if (element != null)
                    {
                        ExpandMenu[grid].mnuColAll.Visible = element.IsOpen;
                        ExpandMenu[grid].mnuExpAll.Visible = !element.IsOpen;
                        ExpandMenu[grid].cmsExpand.Show(Form.MousePosition.X + 3, Form.MousePosition.Y + 3);
                    }
                }
            }
            catch { }
        }

        static void mnuExpAll_Click(object sender, EventArgs e)
        {
            try
            {
                ContextMenuStrip cms = ((ToolStripMenuItem)sender).Tag as ContextMenuStrip;
                UltraGrid grid = cms.Tag as UltraGrid;
                grid.DisplayLayout.Rows.ExpandAll(true);
            }
            catch { }
        }

        static void mnuColAll_Click(object sender, EventArgs e)
        {
            try
            {
                ContextMenuStrip cms = ((ToolStripMenuItem)sender).Tag as ContextMenuStrip;
                UltraGrid grid = cms.Tag as UltraGrid;
                grid.DisplayLayout.Rows.CollapseAll(true);
            }
            catch { }
        }
        #endregion

        #region DefaultInitLayout
        public static void DefaultInitLayout(UltraGrid grid)
        {
            grid.InitializeLayout += new InitializeLayoutEventHandler(grid_InitializeLayout);
            InitExpand(grid);
        }

        static void grid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.SeparateElement;
                e.Layout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex;
                e.Layout.UseFixedHeaders = true;
                e.Layout.Override.FixedRowIndicator = FixedRowIndicator.None;
            }
            catch { }
        }
        #endregion

        #region DefaultKeyvsLayout
        static Dictionary<UltraGrid, FKeys> ConfigValue = new Dictionary<UltraGrid, FKeys>();

        public static void DefaultKeyvsLayout(UltraGrid grid)
        {
            grid.Disposed += new EventHandler(grid_Disposed);
            grid.InitializeLayout += new InitializeLayoutEventHandler(grid_InitializeLayout);
            grid.KeyUp += new KeyEventHandler(grid_KeyUp);
            grid.MouseClick += new MouseEventHandler(grid_MouseClick);
            grid.MouseDoubleClick += new MouseEventHandler(grid_MouseDoubleClick);
            ConfigValue.Add(grid, new FKeys { ReadOnlyColumn = new string[] { } });
            InitExpand(grid);
        }

        public static void DefaultKeyvsLayout(UltraGrid grid, string[] readOnlyColumn)
        {
            grid.Disposed += new EventHandler(grid_Disposed);
            grid.InitializeLayout += new InitializeLayoutEventHandler(grid_InitializeLayout);
            grid.KeyUp += new KeyEventHandler(grid_KeyUp);
            grid.MouseClick += new MouseEventHandler(grid_MouseClick);
            grid.MouseDoubleClick += new MouseEventHandler(grid_MouseDoubleClick);
            ConfigValue.Add(grid, new FKeys { ReadOnlyColumn = readOnlyColumn });
            InitExpand(grid);
        }

        public static void DefaultKeyvsLayout(UltraGrid grid, string[] readOnlyColumn, ContextMenuStrip cmsCopyPaste, ToolStripMenuItem mnuCopy, ToolStripMenuItem mnuPaste)
        {
            grid.Disposed += new EventHandler(grid_Disposed);
            grid.InitializeLayout += new InitializeLayoutEventHandler(grid_InitializeLayout);
            grid.KeyUp += new KeyEventHandler(grid_KeyUp);
            grid.MouseClick += new MouseEventHandler(grid_MouseClick);
            grid.MouseDoubleClick += new MouseEventHandler(grid_MouseDoubleClick);
            if (mnuCopy != null)
                mnuCopy.Click += new EventHandler(mnuCopy_Click);
            if (mnuPaste != null)
                mnuPaste.Click += new EventHandler(mnuPaste_Click);
            Form frm = grid.FindForm();
            if (frm != null)
            {
                if (mnuCopy != null)
                    CommonLib.ShortKeyReg.RegisterHotKey(frm, mnuCopy, Keys.Control | Keys.C);
                if (mnuPaste != null)
                    CommonLib.ShortKeyReg.RegisterHotKey(frm, mnuPaste, Keys.Control | Keys.V);
            }
            ConfigValue.Add(grid, new FKeys { ReadOnlyColumn = readOnlyColumn, CopyPasteMenu = cmsCopyPaste, CopyMenu = mnuCopy, PasteMenu = mnuPaste });
            InitExpand(grid);
        }

        static void mnuPaste_Click(object sender, EventArgs e)
        {
            try
            {
                var grid = ConfigValue.FirstOrDefault(g => g.Value.PasteMenu == sender);
                if (grid.Key != null)
                    HotKeyController.ProcessGridPaste(grid.Key);
            }
            catch { }
        }

        static void mnuCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var grid = ConfigValue.FirstOrDefault(g => g.Value.CopyMenu == sender);
                if (grid.Key != null)
                    HotKeyController.ProcessGridCopy(grid.Key);
            }
            catch { }
        }

        static void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                UltraGrid grdData = sender as UltraGrid;
                UIElement element = grdData.DisplayLayout.UIElement.LastElementEntered;
                UltraGridRow row = null;
                if (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem is UltraGridRow)
                    row = grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem as UltraGridRow;
                else if (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem is UltraGridCell)
                    row = (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem as UltraGridCell).Row;
                if (row != null)
                {
                    if (row.IsGroupByRow)
                    {
                        if (row.Expanded)
                            row.CollapseAll();
                        else
                            row.ExpandAll();
                    }
                    else if (grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem is UltraGridCell && ((UltraGridCell)grdData.DisplayLayout.UIElement.LastElementEntered.SelectableItem).CanEnterEditMode)
                    {
                        grdData.PerformAction(UltraGridAction.EnterEditMode);
                    }
                    else
                    {
                        if (row.Expanded)
                            row.CollapseAll();
                        else
                            row.ExpandAll();
                    }
                }
            }
            catch { }
        }

        static void grid_Disposed(object sender, EventArgs e)
        {
            var grid = ConfigValue.FirstOrDefault(g => g.Key == sender);
            if (grid.Key != null)
                ConfigValue.Remove(grid.Key);
            var gridExpand = ExpandMenu.FirstOrDefault(g => g.Key == sender);
            if (gridExpand.Key != null)
                ExpandMenu.Remove(gridExpand.Key);
        }

        static void grid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var grid = ConfigValue.FirstOrDefault(g => g.Key == sender);

                if (e.Button == MouseButtons.Right)
                {
                    UIElement element = grid.Key.DisplayLayout.UIElement.ElementFromPoint(e.Location);
                    UltraGridCell cell = element.SelectableItem as UltraGridCell;
                    if (cell == null) return;
                    if (!cell.Selected) cell.Selected = true;
                    cell.Activate();
                    grid.Value.CopyMenu.Enabled = cell.Value.ToString() != "";
                    string value = Clipboard.GetText();
                    grid.Value.PasteMenu.Enabled = value != "";

                    grid.Value.CopyPasteMenu.Show(grid.Key.PointToScreen(e.Location));
                    grid.Key.Update();
                }
                else if (e.Button == MouseButtons.Left)
                {
                    UIElement element = grid.Key.DisplayLayout.UIElement.LastElementEntered;
                    if (element is RowSelectorHeaderUIElement)
                    {
                        grid.Key.Selected.Rows.AddRange((UltraGridRow[])grid.Key.Rows.All);
                    }
                }
            }
            catch { }
        }

        static void grid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                UltraGrid grdData = sender as UltraGrid;
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        {
                            foreach (UltraGridCell cell in grdData.Selected.Cells)
                            {
                                if (!ConfigValue[grdData].ReadOnlyColumn.Contains(cell.Column.Key))
                                {
                                    cell.Value = DBNull.Value;
                                }
                            }
                            grdData.UpdateData();
                            grdData.Refresh();

                        }
                        break;
                    case Keys.Enter:
                        {
                            bool isEdit = false;
                            if (grdData.ActiveCell != null)
                            {
                                isEdit = grdData.ActiveCell.IsInEditMode;
                                if (grdData.ActiveCell.IsInEditMode)
                                {
                                    grdData.PerformAction(UltraGridAction.BelowCell);
                                }
                            }
                            if (isEdit)
                                grdData.PerformAction(UltraGridAction.EnterEditMode);
                        }
                        break;
                    case Keys.Escape:
                        {

                        }
                        break;

                    default:
                        {
                            if (Form.ModifierKeys == Keys.None && e.KeyCode == Keys.Up)
                            {
                                if (grdData.ActiveCell != null)
                                {
                                    if (grdData.ActiveCell.IsInEditMode)
                                        grdData.PerformAction(UltraGridAction.ExitEditMode);
                                }
                            }
                            else if (Form.ModifierKeys == Keys.None && e.KeyCode == Keys.Down)
                            {
                                if (grdData.ActiveCell != null)
                                {
                                    if (grdData.ActiveCell.IsInEditMode)
                                        grdData.PerformAction(UltraGridAction.ExitEditMode);
                                }
                            }
                            else if (e.KeyCode == Keys.Left)
                            {

                            }
                            else if (e.KeyCode == Keys.Right)
                            {

                            }
                            else if (Form.ModifierKeys == Keys.None && !grdData.ActiveCell.IsInEditMode && grdData.ActiveCell.CanEnterEditMode && !e.Control && grdData.Selected.Cells.Count == 1)
                            {
                                grdData.PerformAction(UltraGridAction.EnterEditMode);
                            }
                            else
                            {

                            }
                        }
                        break;
                }
            }
            catch { }
        }
        #endregion

        #region InitColumnCaption
        public static void InitColumnCaption(UltraGridBand gridBand, CaptionColumn[] columnCaptions, string[] readOnlyColumns)
        {
            try
            {
                List<CaptionColumn> lst = new List<CaptionColumn>(columnCaptions);
                InitColumnCaption(gridBand, lst, readOnlyColumns);
            }
            catch { }
        }

        public static void InitColumnCaption(UltraGridBand gridBand, List<CaptionColumn> columnCaptions, string[] readOnlyColumns)
        {
            try
            {
                InitColumnCaption(gridBand, columnCaptions, false, readOnlyColumns);
            }
            catch { }
        }

        public static void InitColumnCaption(UltraGridBand gridBand, string[] columns, string[] captions, string[] readOnlyColumns)
        {
            try
            {
                InitColumnCaption(gridBand, columns, captions, false, readOnlyColumns);
            }
            catch { }
        }

        public static void InitColumnCaption(UltraGrid grid, string[] columns, string[] captions, string[] readOnlyColumns)
        {
            if (grid.DisplayLayout.Bands.Count > 0)
                InitColumnCaption(grid.DisplayLayout.Bands[0], columns, captions, grid.CreationFilter is CommonLib.UserControls.CheckBoxOnHeader, readOnlyColumns);
        }

        public static void InitColumnCaption(UltraGrid grid, string[][] bandColumns, string[][] bandCaptions, string[] readOnlyColumns)
        {
            if (bandColumns.Length == bandCaptions.Length)
            {
                for (int i = 0; i < grid.DisplayLayout.Bands.Count; i++)
                {
                    if (bandColumns.Length > i)
                        InitColumnCaption(grid.DisplayLayout.Bands[i], bandColumns[i], bandCaptions[i], grid.CreationFilter is CommonLib.UserControls.CheckBoxOnHeader, readOnlyColumns);
                }
            }
        }

        public static void InitColumnCaption(UltraGrid grid, CaptionColumn[] bandColumnCaptions, string[] readOnlyColumns)
        {
            List<CaptionColumn> lst = new List<CaptionColumn>(bandColumnCaptions);
            InitColumnCaption(grid, lst, readOnlyColumns);
        }

        public static void InitColumnCaption(UltraGrid grid, List<CaptionColumn> bandColumnCaptions, string[] readOnlyColumns)
        {
            foreach (UltraGridBand band in grid.DisplayLayout.Bands)
            {                
                List<CaptionColumn> lst = new List<CaptionColumn>(bandColumnCaptions.Where(b => b.Band == band.Index));
                if (lst.Count > 0)
                    InitColumnCaption(band, lst, grid.CreationFilter is CommonLib.UserControls.CheckBoxOnHeader, readOnlyColumns);
            }
        }

        static void InitColumnCaption(UltraGridBand gridBand, string[] columns, string[] captions, bool CheckBoxOnHeader, string[] readOnlyColumns)
        {
            try
            {
                List<CaptionColumn> lst = new List<CaptionColumn>();
                if (columns.Length >= captions.Length)
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        lst.Add(new CaptionColumn { ColumnName = columns[i], Caption = captions[i] });
                    }
                }
                else
                {
                    for (int i = 0; i < captions.Length; i++)
                    {
                        lst.Add(new CaptionColumn { ColumnName = columns[i], Caption = captions[i] });
                    }
                }

                InitColumnCaption(gridBand, lst, CheckBoxOnHeader, readOnlyColumns);
            }
            catch { }
        }

        static void InitColumnCaption(UltraGridBand gridBand, List<CaptionColumn> columnCaptions, bool CheckBoxOnHeader, string[] readOnlyColumns)
        {
            try
            {
                if (readOnlyColumns == null) readOnlyColumns = new string[] { };
                var lstColumn = from gCol in gridBand.Columns.Cast<UltraGridColumn>()
                                join c in columnCaptions on gCol.Key equals c.ColumnName into g_Cols
                                join ronly in readOnlyColumns on gCol.Key equals ronly into g_ReadOnly
                                select new { Column = gCol, ColumnCaption = g_Cols.FirstOrDefault(), ReadOnly = g_ReadOnly.Any() };
                foreach (var col in lstColumn)
                {
                    if (col.ColumnCaption != null)
                    {
                        col.Column.Header.Caption = col.ColumnCaption.Caption;
                        col.Column.Header.Fixed = col.ColumnCaption.Fixed;
                        col.Column.Header.FixedHeaderIndicator = col.ColumnCaption.FixedHeaderIndicator;
                        col.Column.PerformAutoResize();
                        col.Column.MinWidth = col.ColumnCaption.MinWidth;
                        if (col.Column.DataType == typeof(bool) && (CheckBoxOnHeader || col.ColumnCaption.CheckBoxOnHeader))
                        {
                            col.Column.AutoSizeMode = ColumnAutoSizeMode.None;
                            col.Column.LockedWidth = true;
                            col.Column.FilterOperandStyle = FilterOperandStyle.Disabled;
                            col.Column.MinWidth = 70;
                            col.Column.MaxWidth = 70;
                            col.Column.CellActivation = Activation.AllowEdit;
                            col.Column.CellClickAction = CellClickAction.Edit;
                        }

                    }
                    else
                    {
                        col.Column.Hidden = true;
                    }
                    if (col.ReadOnly)
                        col.Column.CellActivation = Activation.NoEdit;
                }
            }
            catch { }
        }
        #endregion
    }

    public class CaptionColumn
    {
        static bool _defaultFixed = false;
        public static bool DefaultFixed
        {
            get { return CaptionColumn._defaultFixed; }
            set { CaptionColumn._defaultFixed = value; }
        }

        static FixedHeaderIndicator _defaultFixedHeaderIndicator = FixedHeaderIndicator.None;
        public static FixedHeaderIndicator DefaultFixedHeaderIndicator
        {
            get { return CaptionColumn._defaultFixedHeaderIndicator; }
            set { CaptionColumn._defaultFixedHeaderIndicator = value; }
        }

        static Activation _defaultCellActivation = Activation.AllowEdit;
        public static Activation DefaultCellActivation
        {
            get { return CaptionColumn._defaultCellActivation; }
            set { CaptionColumn._defaultCellActivation = value; }
        }

        static int _defaultMinWidth = 100;
        public static int DefaultMinWidth
        {
            get { return CaptionColumn._defaultMinWidth; }
            set { CaptionColumn._defaultMinWidth = value; }
        }

        static bool _defaultCheckBoxOnHeader = true;
        public static bool DefaultCheckBoxOnHeader
        {
            get { return CaptionColumn._defaultCheckBoxOnHeader; }
            set { CaptionColumn._defaultCheckBoxOnHeader = value; }
        }

        public int Band { get; set; }
        public int MinWidth { get; set; }
        public bool CheckBoxOnHeader { get; set; }
        public string ColumnName { get; set; }
        public string Caption { get; set; }
        public CaptionColumn(string columnName, string caption)
        {
            this.Band = 0;
            this.ColumnName = columnName;
            this.Caption = caption;
            this.Fixed = CaptionColumn.DefaultFixed;
            this.FixedHeaderIndicator = CaptionColumn.DefaultFixedHeaderIndicator;
            this.CellActivation = CaptionColumn.DefaultCellActivation;
            this.MinWidth = CaptionColumn._defaultMinWidth;
            this.CheckBoxOnHeader = true;
        }
        public CaptionColumn()
        {
            this.Band = 0;
            this.ColumnName = "";
            this.Caption = "";
            this.Fixed = CaptionColumn.DefaultFixed;
            this.FixedHeaderIndicator = CaptionColumn.DefaultFixedHeaderIndicator;
            this.CellActivation = CaptionColumn.DefaultCellActivation;
            this.MinWidth = CaptionColumn._defaultMinWidth;
            this.CheckBoxOnHeader = true;
        }
        public bool Fixed { get; set; }
        public FixedHeaderIndicator FixedHeaderIndicator { get; set; }
        public Activation CellActivation { get; set; }
        public override string ToString()
        {
            return this.ColumnName + " => " + this.Caption;
        }
    }

    public static class Crypto
    {
        public static string Encrypt<T>(string value, string password)
             where T : SymmetricAlgorithm, new()
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, keyArray);

            SymmetricAlgorithm algorithm = new T();
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.BlockSize = 256;
            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static string Decrypt<T>(string text, string password)
           where T : SymmetricAlgorithm, new()
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, keyArray);

            SymmetricAlgorithm algorithm = new T();
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.BlockSize = 256;
            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        static string user = Environment.UserDomainName + "\\" + Environment.UserName;
        public static bool WriteRegKey(string KeyName, string ValueName, object Value)
        {
            try
            {
                RegistrySecurity rs = new RegistrySecurity();
                rs.AddAccessRule(new RegistryAccessRule(user,
                            RegistryRights.WriteKey | RegistryRights.ReadKey | RegistryRights.Delete,
                            InheritanceFlags.None,
                            PropagationFlags.None,
                            AccessControlType.Allow));
                RegistryKey pscKey = Registry.LocalMachine.OpenSubKey("Software", true).OpenSubKey("PSC", true);

                if (pscKey == null)
                {
                    pscKey = Registry.LocalMachine.OpenSubKey("Software", true).CreateSubKey("PSC", RegistryKeyPermissionCheck.ReadWriteSubTree, rs);
                }
                RegistryKey keyVal = pscKey.OpenSubKey(KeyName, true);
                if (keyVal == null)
                {
                    keyVal = pscKey.CreateSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree, rs);
                }
                keyVal.SetValue(ValueName, Value);
            }
            catch
            {
                return false;
            }

            return true;
        }        
    }
}
