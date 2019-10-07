using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;

namespace CommonLib.UserControls
{
    public partial class uctTwoGrids : UserControl
    {
        #region Init
        public uctTwoGrids()
        {
            InitializeComponent();
            btnDown.Visible = AllowDownUp;
            btnUp.Visible = AllowDownUp;
        }        
        #endregion

        #region Variables
        string _StatusColumn1 = null;
        string _StatusColumn2 = null;
        object _titleGrid1=null,_titleGrid2=null;
        string[] _visibleColumngrid1 = null, _visibleColumngrid2 = null;
        int _widthCheck = 70;        

        Color _colorUnEditableColumn = Color.OldLace;        

        DataTable _dtGrid1 = null;
        DataTable _dtGrid2 = null;

        //DataTable _dtRollback = null;
        string[] _columnEdit = null;    
        bool _allowDownUp = true;
        #endregion

        #region Properties 
        #region AllowDownUp
        public bool AllowDownUp
        {
            get { return _allowDownUp; }
            set {
                
                _allowDownUp = value;
                btnUp.Visible = value;
                btnDown.Visible = value;
            }
        }
        #endregion

        #region AllowResize
        public bool AllowResizeColumnsGrid1
        {
            get { return viewData1.OptionsCustomization.AllowColumnResizing; }
            set { viewData1.OptionsCustomization.AllowColumnResizing = value; }
        }
        public bool AllowResizeRowsGrid1
        {
            get { return viewData1.OptionsCustomization.AllowRowSizing; }
            set { viewData1.OptionsCustomization.AllowRowSizing = value; }
        }

        public bool AllowResizeColumnsGrid2
        {
            get { return viewData2.OptionsCustomization.AllowColumnResizing; }
            set { viewData2.OptionsCustomization.AllowColumnResizing = value; }
        }
        public bool AllowResizeRowsGrid2
        {
            get { return viewData2.OptionsCustomization.AllowRowSizing; }
            set { viewData2.OptionsCustomization.AllowRowSizing = value; }
        }
        #endregion

        #region AllowAddRow
        public bool AllowAddNewRowsGrid1
        {
            get { return viewData1.OptionsView.NewItemRowPosition== NewItemRowPosition.Top? true : false ; }
            set 
            {
                if (value)
                    viewData1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                else
                    viewData1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;

            }
        }
        public bool AllowAddNewRowsGrid2
        {
            get { return viewData2.OptionsView.NewItemRowPosition == NewItemRowPosition.Top ? true : false; }
            set
            {
                if (value)
                    viewData1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                else
                    viewData1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }
        }
        #endregion

        #region AllowDeleteRow
        public bool AllowDeleteRowsGrid1
        {
            get { return dtg1.AllowUserToDeleteRows; }
            set { dtg1.AllowUserToDeleteRows = value; }
        }
        public bool AllowDeleteRowsGrid2
        {
            get { return dtg2.AllowUserToDeleteRows; }
            set { dtg2.AllowUserToDeleteRows = value; }
        }
        #endregion

        #region WidthCheck
        /// <summary>
        /// Width of check column
        /// </summary>
        public int WidthCheck
        {
            get { return _widthCheck; }
            set { _widthCheck = value; }
        }
        #endregion

        #region ColorUnEditableColumn
        /// <summary>
        /// Set color for uneditable columns
        /// </summary>
        public ColorColumn ColorUnEditableColumn
        {
            get { return dtg1.ColorUnEditableColumn; }
            set
            {
                dtg1.ColorUnEditableColumn = value;
                dtg2.ColorUnEditableColumn = value;
            }
        }
        #endregion

        #region DataSourceGrid1
        /// <summary>
        /// Get or set datasource for top datagridview
        /// </summary>
    
        public DataTable DataSourceGrid1
        {            
            get 
            {
                if (dtg1.DataSource != null)
                    _dtGrid1 = ((DataView)dtg1.DataSource).Table;                
                return _dtGrid1;
            }
            set
            {
                _dtGrid1 = value;
                if (_dtGrid1 != null)
                {
                    dtg1.DataSource = new DataView(_dtGrid1);
                    SetTitleGrid(dtg1, HeaderColumnGrid1);
                    //SetVisibleColumn(dtg1, SetVisibleColumnGrid1);
                    //if(_StatusColumn1==null)
                    //    if (!_dtGrid1.Columns.Contains(_StatusColumn2) && dtg1.Columns[_StatusColumn2] == null)
                    CreateStatusColumnName(dtg1);
                    //_dtRollback = _dtGrid1.Copy();
                }
            }
        }
        #endregion

        #region DataSourceGrid2
        /// <summary>
        /// Get or set datasource for datagridview on bottom
        /// </summary>

        public DataTable DataSourceGrid2
        {
            get 
            {
                if (dtg2.DataSource != null)
                    _dtGrid2 = ((DataView)dtg2.DataSource).Table;
                return _dtGrid2;
            }
            set 
            {
                _dtGrid2 = value;
                if (_dtGrid2 != null)
                {
                    dtg2.DataSource = new DataView(_dtGrid2);
                    SetTitleGrid(dtg2, HeaderColumnGrid2);
                    //SetVisibleColumn(dtg2, SetVisibleColumnGrid2);
                    //if (_StatusColumn2 != null)
                    //    if (!_dtGrid2.Columns.Contains(_StatusColumn2) && dtg2.Columns[_StatusColumn2]==null)
                    CreateStatusColumnName(dtg2);
                }
            }
        }
        #endregion

        #region HeaderColumnGrid1
         //<summary>
         //Get or set header column (string[][]) of top datagridview (NameColumn,Title)
         //</summary>
        public object HeaderColumnGrid1
        {
            get 
            {
                if (dtg1.DataSource != null)
                {
                    if (_titleGrid1 == null)
                    {
                        string[][] arr = new string[][] { };
                        Array.Resize(ref arr, 2);
                        _titleGrid1 = arr;

                        for (int i = 0; i < dtg1.Columns.Count; i++)
                        {
                            ((string[][])_titleGrid1)[i] = new string[2] { "", "" };
                            ((string[][])_titleGrid1)[i][0] = dtg1.Columns[i].FieldName;
                            ((string[][])_titleGrid1)[i][1] = dtg1.Columns[i].Caption;
                        }
                    }
                }
                if (_titleGrid1 == null)
                    _titleGrid1 = new string[][] { };
                return _titleGrid1; 
            }
            set 
            { 
                _titleGrid1 = value;
                if(_titleGrid1!=null)
                    SetTitleGrid(dtg1, _titleGrid1);
            }
        }
        #endregion

        #region HeaderColumnGrid2
         //<summary>
         //Get or set header column (string[][]) of bottom datagridview (NameColumn,Title)
         //</summary>
        public object HeaderColumnGrid2
        {
            get
            {
                if (dtg2.DataSource != null)
                {
                    if (_titleGrid2 == null)
                    {
                        string[][] arr = new string[][] { };
                        Array.Resize(ref arr, 2);
                        _titleGrid2 = arr;

                        for (int i = 0; i < dtg2.Columns.Count; i++)
                        {
                            ((string[][])_titleGrid2)[i] = new string[2] { "", "" };
                            ((string[][])_titleGrid2)[i][0] = dtg2.Columns[i].FieldName;
                            ((string[][])_titleGrid2)[i][1] = dtg2.Columns[i].Caption;
                        }
                    }
                }
                if (_titleGrid2 == null)
                    _titleGrid2 = new string[][] { };
                return _titleGrid2;
            }
            set 
            { 
                _titleGrid2 = value;
                if(_titleGrid2!=null)
                    SetTitleGrid(dtg2, _titleGrid2);
            }
        }
        #endregion

        #region ChangedDataOfGrid1
        /// <summary>
        /// Get rows be changed of top datagridview
        /// </summary>
        public DataTable ChangedDataOfGrid1
        {
            get 
            {
                DataTable dt = null;
                if (dtg1.DataSource != null)
                {
                    if (DataSourceGrid1.Columns.Contains(_StatusColumn1))
                    {
                        dt = ((DataView)dtg1.DataSource).Table.Clone();
                        dt.Columns.Remove(_StatusColumn1);
                        foreach (DataRow dr in DataSourceGrid1.Select(_StatusColumn1 + " = 2 or " + _StatusColumn1 + " = 3"))
                        {
                            DataRow drAdd = dt.NewRow();
                            foreach (DataColumn col in dt.Columns)
                            {                                
                                drAdd[col.ColumnName] = dr[col.ColumnName];
                            }
                            dt.Rows.Add(drAdd);
                        }
                        
                    }
                    if(dt!=null)
                        foreach (DataColumn col in dt.Columns)
                            col.ReadOnly = false;
                }
                return dt;
            }
        }
        #endregion

        #region ChangedDataOfGrid2
        /// <summary>
        /// Get rows be changed of bottom datagridview
        /// </summary>
        public DataTable ChangedDataOfGrid2
        {
            get
            {
                DataTable dt = null;
                if (dtg2.DataSource != null)
                {
                    if (DataSourceGrid2.Columns.Contains(_StatusColumn2))
                    {
                        dt = ((DataView)dtg2.DataSource).Table.Clone();
                        dt.Columns.Remove(_StatusColumn2);
                        foreach (DataRow dr in DataSourceGrid2.Select(_StatusColumn2 + " = 1 or " + _StatusColumn2 + " = 3"))
                        {
                            DataRow drAdd = dt.NewRow();
                            foreach (DataColumn col in dt.Columns)
                            {
                                drAdd[col.ColumnName] = dr[col.ColumnName];
                            }
                            dt.Rows.Add(drAdd);
                        }
                    }
                    if(dt!=null)
                        foreach (DataColumn col in dt.Columns)
                            col.ReadOnly = false;
                }
                return dt;
            }
        }
        #endregion

        #region SetVisibleColumnGrid1
        /// <summary>
        /// Set column will be displayed of top datagridview
        /// </summary>
        public string[] SetVisibleColumnGrid1
        {
            get { return _visibleColumngrid1; }
            set 
            {
                if (dtg1.DataSource != null)
                {
                    _visibleColumngrid1 = value;
                    foreach (GridColumn col in dtg1.Columns)
                    {
                        if (_visibleColumngrid1.Contains(col.FieldName))
                            col.Visible = true;
                        else
                            col.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region SetVisibleColumnGrid2
        /// <summary>
        /// Set column will be displayed of bottom datagridview
        /// </summary>
        public string[] SetVisibleColumnGrid2
        {
            get { return _visibleColumngrid2; }
            set
            {
                if (dtg2.DataSource != null)
                {
                    _visibleColumngrid2 = value;
                    foreach (GridColumn col in dtg2.Columns)
                    {
                        if (_visibleColumngrid2.Contains(col.FieldName))
                            col.Visible = true;
                        else
                            col.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region Title Groupbox1
        /// <summary>
        /// Get or set tilte of top datagridview
        /// </summary>
        public string TitleGrid1
        {
            set { grp1.Text = value; }
            get { return grp1.Text; }
        }
        
        #endregion

        #region Title Groupbox2
        /// <summary>
        /// Get or set tilte of bottom datagridview
        /// </summary>
        public string TitleGrid2
        {
            set { grp2.Text = value; }
            get { return grp2.Text; }
        }

        #endregion

        #region ColumnEdit
        /// <summary>
        ///When value of current row is being changed, status of that row will be changed. (On bottom datagridview)
        /// </summary>
        public string[] ColumnEdit
        {
            get { return _columnEdit; }
            set { _columnEdit = value; }
        }
        #endregion

        #endregion

        #region Function local

        #region SetVisibleColumn
        private void SetVisibleColumn(XtraGridExtend dtg, string[] visibleColumn)
        {
            try
            {
                if (visibleColumn == null) return;
                foreach (GridColumn col in dtg.Columns)
                {
                    if (visibleColumn.Contains(col.FieldName))
                        col.Visible = true;
                    else
                        col.Visible = false;
                }
            }
            catch { }
        }
        #endregion 

        #region Set title datagrid
        private void SetTitleGrid(XtraGridExtend dtg, object title)
        {
            try
            {
                if (title != null && dtg != null)
                {
                    if (title.GetType() == typeof(string[][]))
                    {
                        string[][] arr = (string[][])title;
                        foreach (string[] col in arr)
                        {
                            if (col.Length == 2)
                                if (dtg.Columns[col[0]]!=null)
                                    dtg.Columns[col[0]].Caption = col[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Chương trình xảy ra lỗi: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetTitleGrid(DataGridView dtg, object title)
        {
            try
            {
                if (title != null && dtg != null)
                {
                    if (title.GetType() == typeof(string[][]))
                    {
                        string[][] arr = (string[][])title;
                        foreach (string[] col in arr)
                        {
                            if (col.Length == 2)
                                if (dtg.Columns.Contains(col[0]))
                                    dtg.Columns[col[0]].HeaderText = col[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Chương trình xảy ra lỗi: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Create status column
        private void CreateStatusColumnName(XtraGridExtend dtg)
        {
            try
            {
                string statusCol = "";
                if (dtg.DataSource == null)
                    statusCol = "";
                else
                {
                    if (dtg.Columns["Status"]!=null)
                        statusCol = RandomText(dtg.Columns);
                    else
                        statusCol = "Status";
                }
                if (dtg == dtg1)
                    _StatusColumn1 = statusCol;
                else if (dtg == dtg2)
                    _StatusColumn2 = statusCol;

                if (statusCol != "")
                {
                    DataTable dt = ((DataView)dtg.DataSource).Table;
                    dt.Columns.Add(statusCol, typeof(int));
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    dr[statusCol] = dtg == dtg1 ? 0 : 1;
                    //}
                    //dtg.Columns[statusCol].Visible = false;
                }
                dtg.Columns["Check"].OptionsColumn.FixedWidth = true;
                dtg.Columns["Check"].Width = this.WidthCheck;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Chương trình xảy ra lỗi: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RandomText(DataGridViewColumnCollection col)
        {
            string Rtxt="";
            if (col != null)
            {
                Random rand = new Random(100);
                bool cont = true;
                int value = 0;
                while (cont)
                {
                    value = rand.Next(100);
                    if (!col.Contains("Status" + value))
                    {
                        cont = false;
                        Rtxt = "Status" + value;
                    }
                }                
            }
            return Rtxt;
        }

        private string RandomText(GridColumnCollection col)
        {
            string Rtxt="";
            if (col != null)
            {
                Random rand = new Random(100);
                bool cont = true;
                int value = 0;
                while (cont)
                {
                    value = rand.Next(100);
                    if (col["Status" + value]==null)
                    {
                        cont = false;
                        Rtxt = "Status" + value;
                    }
                }                
            }
            return Rtxt;
        }
        #endregion

        #region Check construction of 2 datasource
        private bool CheckData()
        {            
            if (dtg1.DataSource == null || dtg2.DataSource==null)
                return false;
            else
            {
                bool check = true;
                DataTable dtCheck = ((DataView)dtg1.DataSource).Table;
                DataTable dt = ((DataView)dtg2.DataSource).Table;

                if (dtCheck.Columns.Count != dt.Columns.Count)
                    check = false;
                else
                {
                    for(int i=0;i<dtCheck.Columns.Count;i++)
                    {
                        if (dtCheck.Columns[i].DataType != dt.Columns[i].DataType)
                        {
                            check = false;
                            break;
                        }
                    }
                }
                return check;
            }
        }
        #endregion

        #region SelectRow
        #region  ChooseRow(DataGridView dtg, index index)
        private void ChooseRow(DataGridView dtg, int index)
        {
            if(dtg==null || dtg.Rows.Count<index)
                return;
            DataGridViewRow row = dtg.Rows[index];
            row.Selected = true;
            int i = 0;
            foreach(DataGridViewColumn col in dtg.Columns)
                if (col.Visible == true)
                {
                    i = col.Index;
                    break;
                }
            dtg.CurrentCell = row.Cells[i];
        }

        private void ChooseRow(XtraGridExtend dtg, int index)
        {
            try
            {
                if (dtg == null || dtg.VisibleRowCount < index)
                    return;
                viewData1.FocusedRowHandle = index;
                //GridRow row = dtg.Rows[index];
                //row.Selected = true;
                //int i = 0;
                //foreach (DataGridViewColumn col in dtg.Columns)
                //    if (col.Visible == true)
                //    {
                //        i = col.Index;
                //        break;
                //    }
                //dtg.CurrentCell = row.Cells[i];
            }
            catch { }
        }
        #endregion
        #endregion


        #endregion

        #region Event
        //quy dinh 1 -> grid1; 2->grid2, 3-> both 

        #region btnDown_Click
        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                    XtraMessageBox.Show("Chưa đủ dữ liệu cho 2 lưới hoặc dữ liệu không hợp nhau", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (dtg1.Columns["Check"]==null || dtg2.Columns["Check"]==null)
                        XtraMessageBox.Show("Dữ liệu trên lưới phải có cột 'Check' kiểu bool", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        DataTable dt1 = ((DataView)dtg1.DataSource).Table;
                        DataTable dt2 = ((DataView)dtg2.DataSource).Table;
                        foreach (DataRow dr in dt1.Select("Check =1"))
                        {
                            DataRow daNew = dt2.NewRow();
                            foreach (DataColumn col in dt2.Columns)
                            {
                                if (dt2.Columns.Contains(col.ColumnName))
                                    daNew[col.ColumnName] = dr[col.ColumnName];
                            }                            
                            if(daNew[_StatusColumn1].ToString()=="2")
                                daNew[_StatusColumn2] = DBNull.Value;
                            else
                                daNew[_StatusColumn2] = 1;
                            dt2.Rows.Add(daNew);
                            dt1.Rows.Remove(dr);                            
                        }
                        ChooseRow(dtg2, dtg2.VisibleRowCount - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Chương trình xảy ra lỗi: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region btnUp_Click
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckData())
                    XtraMessageBox.Show("Chưa đủ dữ liệu cho 2 lưới hoặc dữ liệu không hợp nhau", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (dtg1.Columns["Check"]==null || dtg2.Columns["Check"]==null)
                        XtraMessageBox.Show("Dữ liệu trên lưới phải có cột 'Check' kiểu bool", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        DataTable dt1 = ((DataView)dtg1.DataSource).Table;
                        DataTable dt2 = ((DataView)dtg2.DataSource).Table;
                        foreach (DataRow dr in dt2.Select("Check =1"))
                        {
                            DataRow daNew = dt1.NewRow();
                            foreach (DataColumn col in dt1.Columns)
                            {
                                if (dt1.Columns.Contains(col.ColumnName))
                                    daNew[col.ColumnName] = dr[col.ColumnName];
                            }
                            if (daNew[_StatusColumn2].ToString() == "1")
                                daNew[_StatusColumn1] = DBNull.Value;
                            else
                                daNew[_StatusColumn1] = 2;
                            dt1.Rows.Add(daNew);
                            dt2.Rows.Remove(dr);
                        }
                        ChooseRow(dtg1, dtg1.VisibleRowCount - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Chương trình xảy ra lỗi: " + ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion        
        
        #region viewData1_ValidateRow
        private void viewData1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (e == null) return;
                if (viewData1.FocusedColumn.FieldName == "Check") return;
                DataRow dr = null;
                if (e.Row is DataRow)
                    dr = e.Row as DataRow;
                else if (e.Row is DataRowView)
                    dr = ((DataRowView)e.Row).Row;
                if (dr[_StatusColumn1].ToString() != "2")
                    dr[_StatusColumn1] = 3;
            }
            catch { }
        }
        #endregion

        #region viewData2_ValidateRow
        private void viewData2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (e == null) return;
                if (viewData2.FocusedColumn.FieldName == "Check") return;
                DataRow dr = null;
                if (e.Row is DataRow)
                    dr = e.Row as DataRow;
                else if (e.Row is DataRowView)
                    dr = ((DataRowView)e.Row).Row;
                if (dr[_StatusColumn2].ToString() != "1")
                    dr[_StatusColumn2] = 3;
            }
            catch { }
        }
        #endregion

        #endregion        

        #region public function

        #region Filter(string StrFilterGrid1,string StrFilterGrid2)
        /// <summary>
        /// Filter data on two datagridview
        /// </summary>
        /// <param name="StrFilterGrid1"></param>
        /// <param name="StrFilterGrid2"></param>
        public void Filter(string StrFilterGrid1,string StrFilterGrid2)
        {
            try
            {
                if (dtg1.DataSource != null)
                {
                    DataView dv = (DataView)dtg1.DataSource;
                    if (StrFilterGrid1 != "")
                        dv.RowFilter = StrFilterGrid1;
                    else
                        dv.RowFilter = string.Empty;
                }
                if (dtg2.DataSource != null)
                {
                    DataView dv = (DataView)dtg2.DataSource;
                    if (StrFilterGrid2 != "")
                        dv.RowFilter = StrFilterGrid2;
                    else
                        dv.RowFilter = string.Empty;
                }
            }
            catch { }
        }
        #endregion

        #region Reset
        public void Reset()
        {
            if (DataSourceGrid1 != null)
                DataSourceGrid1.Clear();
            if (DataSourceGrid2 != null)
                DataSourceGrid2.Clear();
        }

        public void Reset(bool IsAll)
        {
            if (IsAll)
            {
                DataSourceGrid1 = null;
                DataSourceGrid2 = null;
            }
            else
                Reset();
        }

        public void ResetState()
        {
            try
            {
                foreach (DataRow dr in DataSourceGrid1.Rows)
                {
                    dr[_StatusColumn1] = DBNull.Value;
                }
                foreach (DataRow dr in DataSourceGrid2.Rows)
                {
                    dr[_StatusColumn2] = DBNull.Value;
                }
            }
            catch { }
        }
        #endregion                

        #endregion

    }
}
