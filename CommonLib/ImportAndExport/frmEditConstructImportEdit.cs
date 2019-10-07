using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CommonLib.ImportAndExport
{
    public partial class frmEditConstructImportEdit : DevExpress.XtraEditors.XtraForm
    {
        public DataSet dsConstruct = null;
        public DefineColumns _mapCol = null;

        #region Init
        public frmEditConstructImportEdit()
        {
            InitializeComponent();
        }

        private void frmEditConstructImportEdit_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DataType", typeof(string));
                dt.Columns.Add("DataTypeName", typeof(string));
                dt.Rows.Add("String", "Kiểu ký tự");
                dt.Rows.Add("Int", "Kiểu số nguyên");
                dt.Rows.Add("Decimal", "Kiểu số thực");
                dt.Rows.Add("DateTime", "Kiểu ngày tháng");
                repDataType.DataSource = dt;
                repDataType.DisplayMember = "DataTypeName";
                repDataType.ValueMember = "DataType";
                repDataType.ShowHeader = false;
                repDataType.PopulateColumns();
                repDataType.Columns["DataType"].Visible = false;

                DataTable dtTableDS = new DataTable();
                dtTableDS.Columns.Add("TableName", typeof(string));
                dtTableDS.Columns.Add("DisplayName", typeof(string));
                foreach (DataTable dtCons in dsConstruct.Tables)
                {
                    dtTableDS.Rows.Add(dtCons.TableName, _mapCol.GetCaption(dtCons.TableName, true));
                }
                cboTable.DataSource = dtTableDS;
                cboTable.DisplayMember = "DisplayName";
                cboTable.ValueMember = "TableName";
                cboTable_SelectedIndexChanged(null, null);

            }
            catch { }
        }
        #endregion

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dsConstruct.Tables[cboTable.SelectedValue.ToString()];
                DataTable dtColumn = new DataTable();
                dtColumn.Columns.Add("ColumnName", typeof(string));
                dtColumn.Columns.Add("TypeData", typeof(string));
                dtColumn.Columns["ColumnName"].AllowDBNull = false;
                dtColumn.Columns["TypeData"].AllowDBNull = false;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType== typeof(string))
                    {
                        dtColumn.Rows.Add(col.ColumnName, "String");
                    }
                    else if (col.DataType == typeof(DateTime))
                    {
                        dtColumn.Rows.Add(col.ColumnName, "DateTime");
                    }
                    else if (col.DataType == typeof(int))
                    {
                        dtColumn.Rows.Add(col.ColumnName, "Int");
                    }
                    else if (col.DataType == typeof(decimal) || col.DataType == typeof(float) || col.DataType == typeof(double))
                    {
                        dtColumn.Rows.Add(col.ColumnName, "Decimal");
                    }
                }

                grdData.DataSource = dtColumn;

            }
            catch { }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                grvData.UpdateCurrentRow();
                for(int i=0;i<grvData.RowCount;i++)
                {
                    string colName = grvData.GetRowCellValue(i, grcNameField).ToString();
                    string colData = grvData.GetRowCellValue(i, grcTypeData).ToString();
                    if (colName != "" && !dsConstruct.Tables[cboTable.SelectedValue.ToString()].Columns.Contains(colName))
                    {
                        Type type = typeof(object);
                        switch (colData)
                        {
                            case "String":
                                type = typeof(string);
                                break;
                            case "Int":
                                type=typeof(int);
                                break;
                            case "Decimal":
                                type = typeof(decimal);
                                break;
                            case "DateTime":
                                type = typeof(DateTime);
                                break;
                        }
                        dsConstruct.Tables[cboTable.SelectedValue.ToString()].Columns.Add(colName, type);
                    }
                }
            }
            catch { }
        }
    }
}