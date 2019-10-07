using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.FormInputValue
{
    public partial class frmReplaceGrid : CommonLib.dxfrmExtend
    {
        static string _searchValue = "", _replaceValue = "";
        static GridSearchBy _replaceBy = GridSearchBy.ByColumn;
        static bool _matchCase = false, _searchUp= false;

        #region Init
        public frmReplaceGrid()
        {
            InitializeComponent();
        }

        private void frmReplaceGrid_Load(object sender, EventArgs e)
        {
            CommonLib.ShortKeyReg.RegisterHotKey(this, ctrl_F, Keys.Control | Keys.F);
            CommonLib.ShortKeyReg.RegisterHotKey(this, ctrl_H, Keys.Control | Keys.H);
            CommonLib.ShortKeyReg.RegisterHotKey(this, btnSearch, Keys.F3);
            CommonLib.ShortKeyReg.RegisterHotKey(this, btnSearch, Keys.Control | Keys.R);
        }

        void ctrl_F(object sender, EventArgs e)
        {
            txtSearch.Focus();
            txtSearch.SelectAll();
        }
        void ctrl_H(object sender, EventArgs e)
        {
            txtReplace.Focus();
            txtReplace.SelectAll();
        }
        #endregion

        #region properties
        protected string SearchValue
        {
            get { return txtSearch.Text; }
        }
        protected string ReplaceValue
        {
            get { return txtReplace.Text; }
        }
        protected GridSearchBy ReplaceBy
        {
            get 
            {
                if (radByColumn.Checked)
                    return GridSearchBy.ByColumn;
                else if (radByRow.Checked)
                    return GridSearchBy.ByRow;
                else
                    return GridSearchBy.All;
            }
        }
        protected bool MatchCase
        {
            get { return chkMatchCase.Checked; }
        }
        protected bool SearchUp
        {
            get { return chkSerachUp.Checked; }
        }
        public object Grid { get; set; }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchValueGrid != null)
            {
                SearchGirdArgs args = new SearchGirdArgs { Grid= this.Grid, SearchValue = this.SearchValue, SearchBy = this.ReplaceBy, MatchCase = this.MatchCase, SearchUp = this.SearchUp };
                SearchValueGrid(this, args);
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ReplaceValueGrid != null)
            {
                ReplaceGirdArgs args = new ReplaceGirdArgs { Grid = this.Grid, SearchValue = this.SearchValue, SearchBy = this.ReplaceBy, MatchCase = this.MatchCase, SearchUp = this.SearchUp, ReplaceValue = this.ReplaceValue, ReplaceAll = false };
                ReplaceValueGrid(this, args);
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            if (ReplaceValueGrid != null)
            {
                ReplaceGirdArgs args = new ReplaceGirdArgs { Grid = this.Grid, SearchValue = this.SearchValue, SearchBy = this.ReplaceBy, MatchCase = this.MatchCase, SearchUp = this.SearchUp, ReplaceValue = this.ReplaceValue, ReplaceAll = true };
                ReplaceValueGrid(this, args);
            }
        }

        public event SearchGirdHandler SearchValueGrid;
        public event ReplaceGirdHandler ReplaceValueGrid;

        private void frmReplaceGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            _searchValue = this.SearchValue;
            _replaceValue = this.ReplaceValue;
            _replaceBy = this.ReplaceBy;
            _matchCase = this.MatchCase;
            _searchUp = this.SearchUp;
        }
    }

    public delegate void SearchGirdHandler(object sender, SearchGirdArgs e);
    public delegate void ReplaceGirdHandler(object sender, ReplaceGirdArgs e);

    public class SearchGirdArgs: EventArgs
    {
        public object Grid { get; set; }
        public string SearchValue { get; set; }
        public GridSearchBy SearchBy { get; set; }
        public bool MatchCase { get; set; }
        public bool SearchUp { get; set; }

        public SearchGirdArgs()
        {
            this.SearchValue = "";
            this.SearchBy = GridSearchBy.ByColumn;
            this.MatchCase = false;
            this.SearchUp = false;
        }
    }

    public class ReplaceGirdArgs : SearchGirdArgs
    {
        public string ReplaceValue { get; set; }
        public bool ReplaceAll { get; set; }

        public ReplaceGirdArgs()
            : base()
        {
            this.ReplaceValue = "";
            this.ReplaceAll = false;
        }
    }

    public enum GridSearchBy
    {
        ByColumn, ByRow, All
    }
}
