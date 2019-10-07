using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CommonLib.ImportAndExport
{
    public class DefineColumns
    {
        #region Table map column to Caption and reverse
        DataTable _map = null;
        #endregion

        #region Init
        public DefineColumns()
        {
            _map = new DataTable();
            _map.Columns.Add("SrcCaption", typeof(string));
            _map.Columns.Add("DesCaption", typeof(string));
        }
        #endregion

        #region Add Caption (source and destination)
        /// <summary>
        /// Used to add a column (English or Vietnamese)
        /// SourceCaption: name of source column
        /// DestinationCaption: name of destination column
        /// </summary>
        /// <param name="SourceCaption"></param>
        /// <param name="DestinationCaption"></param>
        public void AddCaption(string SourceCaption, string DestinationCaption)
        {
            if (!_map.AsEnumerable().Any(c => string.Compare(c["SrcCaption"].ToString(), SourceCaption, true) == 0))
            {
                DataRow dr = _map.NewRow();
                dr["SrcCaption"] = SourceCaption;
                dr["DesCaption"] = DestinationCaption;
                _map.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Add array string, every element is a array string has 2 elements
        /// </summary>
        /// <param name="Range"></param>
        public void AddCaption(string[][] Range)
        {
            try
            {
                foreach (string[] s in Range)
                {
                    AddCaption(s[0], s[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetCaption(string ColumnName)
        /// <summary>
        /// Get column. English to Vietnamese and reverse
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="IsExport"></param>
        /// <returns></returns>
        public string GetCaption(string ColumnName,bool IsExport)
        {
            string Cap = ColumnName;
            DataRow dr = null;
            if (IsExport)
            {
                dr = _map.AsEnumerable().FirstOrDefault(c => string.Compare(c["SrcCaption"].ToString(), ColumnName, true) == 0);
                if(dr!=null)
                    Cap = dr["DesCaption"].ToString();
            }
            else
            {
                dr = _map.AsEnumerable().FirstOrDefault(c => string.Compare(c["DesCaption"].ToString(), ColumnName, true) == 0);
                if (dr != null)
                    Cap = dr["SrcCaption"].ToString();
            }
            return Cap;
        }

        public string GetCaption(DataColumn Column, bool IsExport)
        {
            string Cap = Column.Caption != "" ? Column.Caption : Column.ColumnName;
            DataRow dr = null;
            if (IsExport)
            {
                dr = _map.AsEnumerable().FirstOrDefault(c => string.Compare(c["SrcCaption"].ToString(),Column.ColumnName, true) == 0);
                if (dr != null)
                    Cap = dr["DesCaption"].ToString();
            }
            else
            {
                dr = _map.AsEnumerable().FirstOrDefault(c => string.Compare(c["DesCaption"].ToString(),Column.ColumnName, true) == 0);
                if (dr != null)
                    Cap = dr["SrcCaption"].ToString();
            }
            return Cap;
        }
        #endregion

        #region ChangeCaption(string ColumnName, string Value)
        /// <summary>
        /// Change caption
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="IsExport"></param>
        /// <returns></returns>
        public bool ChangeCaption(string ColumnName, string Value,bool IsExport)
        {
            bool ret = false;
            try
            {
                if (IsExport)
                {
                    foreach (DataRow dr in _map.Select("DesCaption='" + ColumnName + "'"))
                        dr["SrcCaption"] = Value;
                }
                else
                {
                    foreach (DataRow dr in _map.Select("SrcCaption='" + ColumnName + "'"))
                        dr["DesCaption"] = Value;
                }
                ret = true;
            }
            catch { }
            return ret;
        }
        #endregion
    }
}
