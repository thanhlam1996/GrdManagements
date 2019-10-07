using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace CommonLib
{
    public class ExcelBL
    {
        static System.Globalization.CultureInfo InitCulture()
        {
            System.Globalization.CultureInfo cul = new System.Globalization.CultureInfo("en-US");
            cul.DateTimeFormat.LongDatePattern = "	dddd, MMMM dd, yyyy";
            cul.DateTimeFormat.LongTimePattern = "h:mm:ss tt";
            cul.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cul.DateTimeFormat.ShortTimePattern = "h:mm tt";

            return cul;
        }

        public static DataTable GetSchema(string filePath)
        {
            if (filePath.ToLower().EndsWith(".dbf"))
            {
                string[] s = filePath.Split('\\');
                s = s[s.Length - 1].Split('.');
                return ReadDBF.ReadFileSchemaDBF(filePath, s[0]);
            }
            else
            {
                SpreadsheetGear.IWorkbook wk = SpreadsheetGear.Factory.GetWorkbook(filePath, InitCulture());
                DataTable dt = new DataTable();
                dt.Columns.Add("FullSheetName", typeof(string));
                dt.Columns.Add("SheetName", typeof(string));

                DataSet ds = wk.GetDataSet(SpreadsheetGear.Data.GetDataFlags.FormattedText);
                foreach (DataTable dtSchema in ds.Tables)
                {
                    DataRow drNew = dt.NewRow();
                    drNew["FullSheetName"] = dtSchema.TableName;
                    drNew["SheetName"] = dtSchema.TableName;
                    dt.Rows.Add(drNew);
                }
                return dt;
            }
        }
 
        public static DataTable GetConstructColumn(int excludeTopRow, string filePath, string fullSheetName)
        {
            SpreadsheetGear.IWorkbook wk = SpreadsheetGear.Factory.GetWorkbook(filePath, InitCulture());
            SpreadsheetGear.IWorksheet wSheet = wk.Worksheets[fullSheetName.Replace("$", "")];
            int colCount = wSheet.Range.CurrentRegion.EntireColumn.ColumnCount;
            DataTable dt1 = wSheet.Range[excludeTopRow, 0, excludeTopRow, colCount].GetDataTable(SpreadsheetGear.Data.GetDataFlags.FormattedText);

            return dt1.Clone();
        }

        public static DataTable GetSheetContent(string filePath, string fullSheetName)
        {
            if (filePath.ToLower().EndsWith(".dbf"))
            {
                return ReadDBF.ReadFileDBF(filePath, fullSheetName);
            }
            else
            {
                SpreadsheetGear.IWorkbook wk = SpreadsheetGear.Factory.GetWorkbook(filePath, InitCulture());

                DataSet ds = wk.GetDataSet(SpreadsheetGear.Data.GetDataFlags.FormattedText);
                return ds.Tables[fullSheetName.Replace("$", "")];
            }
        }

        public static DataTable CreateMapConstruct()
        {
            DataTable dtMap = new DataTable();
            dtMap.Columns.Add("ColumnDataSetMap", typeof(string));
            dtMap.Columns.Add("ColumnSheet", typeof(string));
            return dtMap;
        }

        public static bool GetSheetContentFromConstruct(int excludeTopRow, string filePath, string fullSheetName, DataTable MapColumn, ref DataTable ConsData)
        {
            SpreadsheetGear.IWorkbook wk = SpreadsheetGear.Factory.GetWorkbook(filePath, InitCulture());

            SpreadsheetGear.IWorksheet wSheet = wk.Worksheets[fullSheetName.Replace("$", "")];
            int colCount = wSheet.Range.CurrentRegion.EntireColumn.ColumnCount;
            int rowCount = wSheet.Range.CurrentRegion.EntireRow.RowCount;
            DataTable dtData = wSheet.Range[excludeTopRow, 0, rowCount - 1, colCount].GetDataTable(SpreadsheetGear.Data.GetDataFlags.FormattedText);

            var eColumns = from r in MapColumn.AsEnumerable()
                           select new { ColumnSheet = r["ColumnSheet"].ToString().ToLower(), ColumnDataSetMap = r["ColumnDataSetMap"].ToString().ToLower() };
            var eValidColumns = from r1 in dtData.Columns.Cast<DataColumn>()
                                join r2 in eColumns on r1.ColumnName.ToLower() equals r2.ColumnSheet
                                join r3 in ConsData.Columns.Cast<DataColumn>() on r2.ColumnDataSetMap equals r3.ColumnName.ToLower()
                                select new { ColumnSheet = r1.ColumnName, ColumnSheetType = r1.DataType, ColumnDataSetMap = r3.ColumnName, ColumnDataSetMapType = r3.DataType, IsSameType = r1.DataType == r3.DataType };
            string[] boolTrue = new string[] { "1", "true", "TRUE", "True" };
            string[] boolFalse = new string[] { "0", "false", "FALSE", "False" };
            System.Globalization.DateTimeFormatInfo fDate = new System.Globalization.DateTimeFormatInfo();
            fDate.ShortDatePattern = "dd/MM/yyyy";

            string currentConvertValue = "";
            int currentConvertRow = 0;
            string currentConvertColumn = "";
            try
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    currentConvertRow++;
                    DataRow drNew = ConsData.NewRow();
                    foreach (var col in eValidColumns)
                    {
                        currentConvertColumn = col.ColumnSheet;
                        if (col.IsSameType)
                        {
                            drNew[col.ColumnDataSetMap] = dr[col.ColumnSheet];
                        }
                        else
                        {
                            string rVal = dr[col.ColumnSheet].ToString();
                            currentConvertValue = rVal;

                            if (rVal.Trim() != "")
                            {
                                object val = DBNull.Value;
                                if (col.ColumnDataSetMapType == typeof(bool))
                                {
                                    string v1 = dr[col.ColumnSheet].ToString();
                                    if (boolTrue.Any(r => r == v1))
                                        val = true;
                                    else if (boolFalse.Any(r => r == v1))
                                        val = false;
                                }
                                else if (col.ColumnDataSetMapType == typeof(byte))
                                {
                                    val = byte.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(char))
                                {
                                    val = char.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(decimal))
                                {
                                    val = decimal.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(double))
                                {
                                    val = double.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(int))
                                {
                                    val = int.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(long))
                                {
                                    val = long.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(short))
                                {
                                    val = short.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(Single))
                                {
                                    val = Single.Parse(dr[col.ColumnSheet].ToString());
                                }
                                else if (col.ColumnDataSetMapType == typeof(DateTime))
                                {
                                    try
                                    {
                                        val = DateTime.Parse(dr[col.ColumnSheet].ToString(), fDate);
                                    }
                                    catch
                                    {
                                        val = DateTime.Parse(dr[col.ColumnSheet].ToString());
                                    }
                                }
                                else if (col.ColumnDataSetMapType == typeof(string))
                                {
                                    val = dr[col.ColumnSheet].ToString();
                                }
                                else
                                {
                                    val = dr[col.ColumnSheet];
                                }

                                drNew[col.ColumnDataSetMap] = val;
                            }
                        }
                    }
                    ConsData.Rows.Add(drNew);
                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Error += "\r\n" + ex.Message;
                }
                Error += "\r\n Col: [" + currentConvertColumn + "] Row: [" + currentConvertRow + "] Val: [" + currentConvertValue + "]";
                XtraMessageBox.Show("Đọc file có lỗi: " + Error, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }

    public class OleDbConnect
    {
        private OleDbConnection cn = null;

        public OleDbConnect(string connectionString) { cn = new OleDbConnection(connectionString); }

        private void OpenConnection()
        {
            if (cn.State == ConnectionState.Closed)
                cn.Open();
        }

        public void CloseConnection()
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        public int ExcuteSQLNotCloseConnect(string sql)
        {
            try
            {
                OpenConnection();
                using (OleDbCommand cmd = new OleDbCommand(sql, cn) { CommandType = CommandType.Text })
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex) { throw new Exception(ex.Message); }
        }
    }
}

