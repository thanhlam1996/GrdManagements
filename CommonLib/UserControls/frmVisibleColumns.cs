using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;

namespace CommonLib.UserControls
{
    public partial class frmVisibleColumns : dxfrmExtend
    {
        #region Init
        public frmVisibleColumns(DataGridViewColumn[] columns)
        {
            InitializeComponent();
            _type = 1;
            _columns1 = columns;
        }

        public frmVisibleColumns(GridColumn[] columns)
        {
            InitializeComponent();
            _type = 2;
            _columns2 = columns;
        }
        #endregion

        #region Variables
        int _type = 0;
        DataGridViewColumn[] _columns1 = null;
        GridColumn[] _columns2 = null;
        #endregion

        #region Load
        private void frmVisibleColumns_Load(object sender, EventArgs e)
        {
            try
            {
                if (_type == 1)
                {
                    if (_columns1 != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Check", typeof(bool));
                        dt.Columns.Add("Header", typeof(string));
                        dt.Columns.Add("Name", typeof(string));
                        foreach (DataGridViewColumn col in _columns1)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Check"] = col.Visible;
                            dr["Header"] = col.HeaderText;
                            dr["Name"] = col.Name;
                            dt.Rows.Add(dr);
                        }
                        dt.Columns["Header"].ReadOnly = true;
                        dt.Columns["Name"].ReadOnly = true;
                        grdData.DataSource = new DataView(dt);
                        grdData.Columns["Check"].Caption = "Chọn";
                        grdData.Columns["Check"].OptionsColumn.FixedWidth = true;
                        grdData.Columns["Check"].Width = 110;
                        grdData.Columns["Header"].Caption = "Cột hiển thị";
                        grdData.Columns["Name"].Visible = false;
                    }
                }
                else if(_type==2)
                {
                    if (_columns2 != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Check", typeof(bool));
                        dt.Columns.Add("Header", typeof(string));
                        dt.Columns.Add("Name", typeof(string));
                        foreach (GridColumn col in _columns2)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Check"] = col.Visible;
                            dr["Header"] = col.Caption;
                            dr["Name"] = col.FieldName;
                            dt.Rows.Add(dr);
                        }
                        dt.Columns["Header"].ReadOnly = true;
                        dt.Columns["Name"].ReadOnly = true;
                        grdData.DataSource = new DataView(dt);
                        grdData.Columns["Check"].Caption = "Chọn";
                        grdData.Columns["Check"].OptionsColumn.FixedWidth = true;
                        grdData.Columns["Check"].Width = 110;
                        grdData.Columns["Header"].Caption = "Cột hiển thị";
                        grdData.Columns["Name"].Visible = false;
                    }
                }
            }
            catch { }
        }
        #endregion

        #region public function
        public string[] ColumnChoosed()
        {
            string[] scol = new string[] { };
            try
            {
                DataRow[] dr = ((DataView)grdData.DataSource).Table.Select("Check = 1");
                Array.Resize(ref scol, dr.Length);
                for (int i = 0; i < dr.Length; i++)
                    scol[i] = dr[i]["Name"].ToString();
            }
            catch { }
            return scol;
        }
        #endregion

        #region btnOK_Click
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }
        #endregion
    }
}
