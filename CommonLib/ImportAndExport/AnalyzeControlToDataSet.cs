using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win.UltraWinGrid;
using System.Data;
using System.Windows.Forms;
using CommonLib.UserControls;
using DevExpress.XtraEditors.Repository;

namespace CommonLib.ImportAndExport
{
    public class AnalyzeControlToDataSet
    {
        #region GetDataSet(UltraGrid UControl)
        /// <summary>
        /// Get DataSet from UltraGrid (If it has ValueList columns, that columns must be UltraCombo)
        /// </summary>
        /// <param name="UControl"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(UltraGrid UControl)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = ((DataView)UControl.DataSource).ToTable();                
                string s = dt1.Columns[0].ColumnName;
                dt1.TableName = s.Contains("ID") ? s.Substring(0, s.Length - 2) : s;
                ds.Tables.Add(dt1);
                foreach (UltraGridColumn col in UControl.DisplayLayout.Bands[0].Columns)
                {
                    if (col.ValueList != null)
                    {
                        UltraCombo cbo = (UltraCombo)col.ValueList;
                        DataTable dt = ((DataView)cbo.DataSource).ToTable();
                        string s2 = dt.Columns[0].ColumnName;
                        dt.TableName = s2.Contains("ID") ? s2.Substring(0, s2.Length - 2) : s2;
                        ds.Tables.Add(dt);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region GetDataSet(DataGridView UControl)
        /// <summary>
        /// Get DataSet from DataGridView (If it has ValueList columns, that columns must be DataGridViewComboBoxColumn)
        /// </summary>
        /// <param name="UControl"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(DataGridView UControl)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = ((DataView)UControl.DataSource).ToTable();                
                string s = dt1.Columns[0].ColumnName;
                dt1.TableName = s.Contains("ID") ? s.Substring(0, s.Length - 2) : s;
                ds.Tables.Add(dt1);
                for (int j = 0; j < UControl.Columns.Count; j++)
                {
                    if (UControl[j,0].OwningColumn.CellType.Name == "DataGridViewComboBoxCell")
                    {
                        DataGridViewComboBoxCell comboCell = UControl[j,0] as DataGridViewComboBoxCell;
                        DataTable dt = ((DataView)comboCell.DataSource).ToTable();
                        string s2 = dt.Columns[0].ColumnName;
                        dt.TableName = s2.Contains("ID") ? s2.Substring(0, s2.Length - 2) : s2;
                        ds.Tables.Add(dt);
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region GetDataSet(ComboBox UControl)
        /// <summary>
        /// Get DataSet from ComboBox
        /// </summary>
        /// <param name="UControl"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(ComboBox UControl)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = ((DataView)UControl.DataSource).ToTable();
                string s = dt1.Columns[0].ColumnName;
                dt1.TableName = s.Contains("ID") ? s.Substring(0, s.Length - 2) : s;
                ds.Tables.Add(dt1);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region GetDataSet(UltraCombo UControl)
        /// <summary>
        /// Get DataSet from UltraCombo
        /// </summary>
        /// <param name="UControl"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(UltraCombo UControl)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = ((DataView)UControl.DataSource).ToTable();
                string s = dt1.Columns[0].ColumnName;
                dt1.TableName = s.Contains("ID") ? s.Substring(0, s.Length - 2) : s;
                ds.Tables.Add(dt1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        #region GetDataSet(XtraGridExtend UControl)
        /// <summary>
        /// Get DataSet from DataGridView (If it has ValueList columns, that columns must be DataGridViewComboBoxColumn)
        /// </summary>
        /// <param name="UControl"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(XtraGridExtend UControl)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = ((DataView)UControl.DataSource).ToTable();
                string s = dt1.Columns[0].ColumnName;
                dt1.TableName = s.Contains("ID") ? s.Substring(0, s.Length - 2) : s;
                ds.Tables.Add(dt1);
                for (int j = 0; j < UControl.Columns.Count; j++)
                {
                        RepositoryItemLookUpEdit rep = UControl[j].ColumnEdit as RepositoryItemLookUpEdit;
                        if (rep != null)
                        {
                            DataTable dt =null;
                            if (rep.DataSource is DataView)
                                dt = ((DataView)rep.DataSource).ToTable();
                            else if (rep.DataSource is DataTable)
                                dt = (DataTable)rep.DataSource;
                            if (dt != null)
                            {
                                string s2 = rep.ValueMember;
                                dt.TableName = s2.Contains("ID") ? s2.Substring(0, s2.Length - 2) : s2;
                                ds.Tables.Add(dt);
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion
    }
}
