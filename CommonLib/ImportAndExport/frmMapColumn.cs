using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace CommonLib.ImportAndExport
{
    public partial class frmMapColumn : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dtMap = new DataTable();

        public frmMapColumn()
        {
            InitializeComponent();
        }

        private void frmMapColumn_Load(object sender, EventArgs e)
        {
            try
            {
                this.AcceptButton = btnFolder;
            }
            catch { }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }

        public void LoadData(DataTable dtInputColumn, DataTable dtMapColumn, DefineColumns mapColumn)
        {
            try
            {
                dtMap.Columns.Add("InputColumn", typeof(string));
                dtMap.Columns.Add("MapColumn", typeof(string));
                dtMap.Columns.Add("DisplayInputColumn", typeof(string));
                dtMap.Columns.Add("DisplayMapColumn", typeof(string));

                if (mapColumn == null)
                    mapColumn = new DefineColumns();
                foreach (DataColumn col in dtInputColumn.Columns)
                {
                    DataRow drAdd = dtMap.NewRow();
                    drAdd["InputColumn"] = col.ColumnName;
                    drAdd["DisplayInputColumn"] = mapColumn.GetCaption(col.ColumnName, true);
                    if (dtMapColumn.Columns.Contains(col.ColumnName))
                    {
                        drAdd["MapColumn"] = col.ColumnName;
                        drAdd["DisplayMapColumn"] = mapColumn.GetCaption(col.ColumnName, true);
                    }
                    dtMap.Rows.Add(drAdd);
                }

                DataTable dtRep = new DataTable();
                dtRep.Columns.Add("ColumnName", typeof(string));
                dtRep.Columns.Add("DisplayColumnName", typeof(string));

                foreach (DataColumn col in dtMapColumn.Columns)
                {
                    dtRep.Rows.Add(col.ColumnName, mapColumn.GetCaption(col.ColumnName, true));
                }

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rep = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                rep.DataSource = dtRep;
                rep.DisplayMember = "DisplayColumnName";
                rep.ValueMember = "ColumnName";
                rep.PopulateColumns();
                rep.Columns["ColumnName"].Visible = false;
                rep.ShowHeader = false;

                grdData.DataSource = dtMap;
                grvData.PopulateColumns();
                grvData.BestFitColumns();
                foreach (GridColumn col in grvData.Columns)
                {
                    switch (col.FieldName)
                    {
                        case "DisplayInputColumn":
                            col.Caption = "Tên cột nhập vào";
                            col.OptionsColumn.AllowEdit = false;
                            break;
                        case "DisplayMapColumn":
                            col.Caption = "Tên cột của dữ liệu";
                            col.ColumnEdit = rep;
                            break;
                        default:
                            col.Visible = false;
                            break;
                    }
                }
            }
            catch { }
        }
    }
}