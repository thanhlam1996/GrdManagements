using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using System.Drawing;

namespace CommonLib.UserControls
{
    public class CheckBoxOnHeader : IUIElementCreationFilter
    {
        string[] _applyTodColumns = new string[] { };

        string[] ApplyTodColumns 
        {
            get 
            {
                if (this._applyTodColumns == null) this._applyTodColumns = new string[] { };
                return this._applyTodColumns; 
            } 
        }

        public delegate void HeaderCheckBoxClickedHandler(object sender, HeaderCheckBoxEventArgs e);
        public event HeaderCheckBoxClickedHandler _CLICKED;
        public CheckBoxOnHeader()
        {
            _CLICKED += new HeaderCheckBoxClickedHandler(CheckBoxOnHeader_CreationFilter_HeaderCheckBoxClicked);
        }
        public CheckBoxOnHeader(params string[] applyToColumns)
        {
            _CLICKED += new HeaderCheckBoxClickedHandler(CheckBoxOnHeader_CreationFilter_HeaderCheckBoxClicked);
            this._applyTodColumns = applyToColumns;
        }

        private void CheckBoxOnHeader_CreationFilter_HeaderCheckBoxClicked(object sender, CheckBoxOnHeader.HeaderCheckBoxEventArgs e)
        {
            if (e.Header.Column.DataType == typeof(bool))
            {
                foreach (UltraGridRow row in e.Rows)
                {
                    row.Cells[e.Header.Column.Index].Value = (e.CurrentCheckState == CheckState.Checked);
                }
            }
        }
        #region HeaderCheckBoxEventArgs
        public class HeaderCheckBoxEventArgs : EventArgs
        {
            private Infragistics.Win.UltraWinGrid.ColumnHeader mvarColumnHeader;
            private CheckState mvarCheckState;
            private RowsCollection mvarRowsCollection;
            public HeaderCheckBoxEventArgs(Infragistics.Win.UltraWinGrid.ColumnHeader hdrColumnHeader, CheckState chkCheckState, RowsCollection Rows)
            {
                mvarColumnHeader = hdrColumnHeader;
                mvarCheckState = chkCheckState;
                mvarRowsCollection = Rows;
            }
            public RowsCollection Rows
            {
                get
                {
                    return mvarRowsCollection;
                }
            }
            public Infragistics.Win.UltraWinGrid.ColumnHeader Header
            {
                get
                {
                    return mvarColumnHeader;
                }
            }
            public CheckState CurrentCheckState
            {
                get
                {
                    return mvarCheckState;
                }
                set
                {
                    mvarCheckState = value;
                }
            }
        }
        #endregion
        private void chkUI_ElementClick(Object sender, Infragistics.Win.UIElementEventArgs e)
        {
            CheckBoxUIElement chkUI = (CheckBoxUIElement)e.Element;
            Infragistics.Win.UltraWinGrid.ColumnHeader colHeader = (Infragistics.Win.UltraWinGrid.ColumnHeader)chkUI.GetAncestor(typeof(HeaderUIElement)).GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader));
            colHeader.Tag = chkUI.CheckState;
            HeaderUIElement aHeaderUIElement = chkUI.GetAncestor(typeof(HeaderUIElement)) as HeaderUIElement;
            RowsCollection hRows = aHeaderUIElement.GetContext(typeof(RowsCollection)) as RowsCollection;
            if (_CLICKED != null)
                _CLICKED(this, new HeaderCheckBoxEventArgs(colHeader, chkUI.CheckState, hRows));
        }
        public bool BeforeCreateChildElements(Infragistics.Win.UIElement parent)
        {
            return false;
        }
        public void AfterCreateChildElements(Infragistics.Win.UIElement parent)
        {
            if (parent is HeaderUIElement)
            {
                Infragistics.Win.UltraWinGrid.HeaderBase aHeader = ((HeaderUIElement)parent).Header;
                if (aHeader.Column.DataType == typeof(bool) && (this.ApplyTodColumns.Length == 0 || this.ApplyTodColumns.Any(c => c == aHeader.Column.Key)))
                {
                    TextUIElement txtUI;
                    CheckBoxUIElement chkUI = (CheckBoxUIElement)parent.GetDescendant(typeof(CheckBoxUIElement));
                    if (chkUI == null)
                    {
                        chkUI = new CheckBoxUIElement(parent);
                    }
                    txtUI = (TextUIElement)parent.GetDescendant(typeof(TextUIElement));

                    if (txtUI == null)
                        return;
                    Infragistics.Win.UltraWinGrid.ColumnHeader colHeader =
                        (Infragistics.Win.UltraWinGrid.ColumnHeader)chkUI.GetAncestor(typeof(HeaderUIElement))
                        .GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader));
                    if (colHeader.Tag == null)
                        colHeader.Tag = CheckState.Indeterminate;
                    else
                        chkUI.CheckState = (CheckState)colHeader.Tag;
                    chkUI.ElementClick += new UIElementEventHandler(chkUI_ElementClick);
                    parent.ChildElements.Add(chkUI);
                    chkUI.Rect = new Rectangle(parent.Rect.X + parent.Rect.Width / 2 - chkUI.CheckSize.Width / 2, parent.Rect.Y + ((parent.Rect.Height - chkUI.CheckSize.Height) / 2), chkUI.CheckSize.Width, chkUI.CheckSize.Height);
                    txtUI.Rect = new Rectangle(chkUI.Rect.Right + 3, txtUI.Rect.Y, parent.Rect.Width - (chkUI.Rect.Right - parent.Rect.X), txtUI.Rect.Height);
                }
                else
                {
                    CheckBoxUIElement chkUI = (CheckBoxUIElement)parent.GetDescendant(typeof(CheckBoxUIElement));
                    if (chkUI != null)
                    {
                        parent.ChildElements.Remove(chkUI);
                        chkUI.Dispose();
                    }
                }
            }
        }
    }  
}
