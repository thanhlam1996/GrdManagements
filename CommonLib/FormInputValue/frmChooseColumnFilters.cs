using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Infragistics.Win.UltraWinGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using DevExpress.XtraGrid.Views.Base;

namespace CommonLib.FormInputValue
{
    public partial class frmChooseColumnFilters : CommonLib.dxfrmExtend
    {
        public frmChooseColumnFilters()
        {
            InitializeComponent();
        }

        DataTable dtColumn = null;
        public string[] DefaultSearchColumn = new string[] { };

        void InitColumn()
        {
            if (dtColumn == null)
            {
                dtColumn = new DataTable();
                dtColumn.Columns.Add("ColumnName", typeof(string));
                dtColumn.Columns.Add("ColumnHeader", typeof(string));
                dtColumn.Columns.Add("Check", typeof(bool));
            }
            else
                dtColumn.Clear();
        }

        void InitGrid()
        {
            try
            {
                grdData.DataSource = dtColumn;
                grdData.Columns["ColumnName"].Visible = false;
                grdData.Columns["ColumnHeader"].Caption = "Cột";
                grdData.Columns["Check"].Caption = "Chọn";
            }
            catch { }
        }

        public void LoadData(DataGridView dtg)
        {
            try
            {
                InitColumn();
                foreach (DataGridViewColumn col in dtg.Columns)
                {
                    if (col.Visible)
                        dtColumn.Rows.Add(col.Name, col.HeaderText, Array.IndexOf(DefaultSearchColumn, col.Name) >= 0 ? true : false);
                }
                InitGrid();
            }
            catch { }
        }
        public void LoadData(UltraGrid dtg)
        {
            try
            {
                InitColumn();
                foreach (UltraGridBand band in dtg.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn col in band.Columns)
                    {
                        if (!col.Hidden)
                            dtColumn.Rows.Add(col.Key, col.Header.Caption, Array.IndexOf(DefaultSearchColumn, col.Key) >= 0 ? true : false);
                    }
                }
                InitGrid();
            }
            catch { }
        }
        public void LoadData(GridControl dtg)
        {
            try
            {
                InitColumn();
                GridView view = dtg.MainView as GridView;
                foreach (GridColumn col in view.Columns)
                {
                    if (col.Visible)
                        dtColumn.Rows.Add(col.FieldName, col.Caption, Array.IndexOf(DefaultSearchColumn, col.FieldName) >= 0 ? true : false);
                }
                InitGrid();
            }
            catch { }
        }

        private void frmChooseColumnFilters_Load(object sender, EventArgs e)
        {
            SelectedColumn = DefaultSearchColumn;
        }

        public string[] SelectedColumn = new string[] { };

        private void btnOK1_Click(object sender, EventArgs e)
        {
            try
            {
                grdData.MainView.UpdateCurrentRow();
                DataRow[] drSel = dtColumn.Select("Check=True or Check=1");
                if (drSel.Length > 0)
                {
                    SelectedColumn = new string[] { };
                    foreach (DataRow dr in drSel)
                    {
                        Array.Resize(ref SelectedColumn, SelectedColumn.Length + 1);
                        SelectedColumn[SelectedColumn.Length - 1] = dr["ColumnName"].ToString();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }

    }

    internal class ObjChooseFilter
    {
        object _grid = null;
        Control _txtSearch = null;
        string[] _defaultSearch = new string[] { };
        bool _isDataViewFilter = false;
        Form _form = null;

        public object Grid
        {
            get { return _grid; }
        }

        public object FormSearch
        {
            get { return _form; }
        }

        public Control TxtSearch
        {
            get { return _txtSearch; }
        }

        public string[] DefaultSearch
        {
            get { return _defaultSearch; }
            set { _defaultSearch = value; }
        }

        public bool IsDataViewFilter
        {
            get { return _isDataViewFilter; }
        }

        bool _notifyNotFound = true;
        public bool NotifyNotFound
        {
            get { return _notifyNotFound; }
        }

        public ObjChooseFilter(Form formSearch, object grid, Control txtSearch, string[] defaultSearch, bool isDataViewFilter)
        {
            _grid = grid;
            _txtSearch = txtSearch;
            _defaultSearch = defaultSearch;
            _isDataViewFilter = isDataViewFilter;
            _form = formSearch;
        }

        public ObjChooseFilter(Form formSearch, object grid, Control txtSearch, string[] defaultSearch, bool isDataViewFilter, bool notifyNotFound)
        {
            _grid = grid;
            _txtSearch = txtSearch;
            _defaultSearch = defaultSearch;
            _isDataViewFilter = isDataViewFilter;
            _form = formSearch;
            _notifyNotFound = notifyNotFound;
        }
    }

    public class ChooseColumnFilters
    {
        public static void ApplySearch(object Grid,TextBox txtSearch,bool IsDataViewFilter,string[] DefaultSearchColumn)
        {
            CommonLib.GridSearch.ApplySearch(Grid, txtSearch, IsDataViewFilter, DefaultSearchColumn);
        }        
    }    
}