using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CommonLib
{
    public abstract  class BaseObject
    {

        #region "Variable"
        int _reVal = Constants.NullInt;
        object _objID = null;
        object _objName = null;
        object _objOriID = null;
        object _objParentID = null;
        object _objChildID = null;
        object _objMe = null;
        string _strXml = Constants.NullXml;
        string _tableName = Constants.NullString;
#endregion

        #region "Constructor"
        public BaseObject()
        {
            _objOriID = _objID;
            _objMe = this;
        }
        #endregion

        #region "Properties"

        public object  ObjMe
        {
            get { return _objMe; }
            set { _objMe = value;
            _objID = ((BaseObject)_objMe).ObjID;
            _objName = ((BaseObject)_objMe).ObjName;
            _objChildID = ((BaseObject)_objMe).ObjChildID;
            _objOriID = ((BaseObject)_objMe).ObjOriID;
            _objParentID = ((BaseObject)_objMe).ObjParentID;
            _strXml = ((BaseObject)_objMe).StrXML;
            _reVal = ((BaseObject)_objMe).ReVal;

            }
        }

        public object ObjID
        {
            get { return _objID; }
            set { _objID = value;
            //((BaseObject)_objMe).ObjID = _objID;
            }
        }
        
        public object ObjName
        {
            get { return _objName; }
            set { _objName = value;
            //((BaseObject)_objMe).ObjName = _objName;
            }
        }

        public object ObjOriID
        {
            get { return _objOriID; }
            set { _objOriID = value;
            //((BaseObject)_objMe).ObjOriID = _objOriID; 
            }
        }

        public object ObjParentID
        {
            get { return _objParentID; }
            set { _objParentID = value;
            //((BaseObject)_objMe).ObjParentID = _objParentID; 
            }
        }

        public object ObjChildID
        {
            get { return _objChildID; }
            set { _objChildID = value;
            //((BaseObject)_objMe).ObjChildID = _objChildID; 
            }
        }


        /// <summary>
        /// Gia tri tra ve trong Store
        /// </summary>
        public int ReVal
        {
            get { return _reVal; }
            set { _reVal = value;
            //((BaseObject)_objMe).ReVal = _reVal; 
            }
        }
        
        /// <summary>
        /// Gia tri Chuoi XML 
        /// </summary>
        public string StrXML
        {
            get { return _strXml ; }
            set { _strXml = value;
            //((BaseObject)_objMe).StrXML = _strXml; 
            }
        }

        /// <summary>
        /// Tan bang trong CSDL
        /// </summary>
        public string TableName
        {
            get { return _tableName ; }
            set
            {
                _tableName  = value;
            }
        }


        #endregion

        #region "Basic Funstions"
        //// <summary>
        /// Ham chuyen doi du lieu tu DataRow sang Object cho phep nguoi dung Override theo tung doi tuong cu the
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        /// 
        public virtual bool MapData(DataRow row)
        {
            //You can put common data mapping items here (e.g. create date, modified date, etc)
            return true;
        }

        /// <summary>
        /// Ham chuyen doi du lieu tu DataSet sang ObjectCollections cho phep nguoi dung Override theo tung doi tuong cu the
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public virtual bool MapData(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return MapData(ds.Tables[0].Rows[0]);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Ham chuyen doi du lieu tu DataTable sang ObjectCollections cho phep nguoi dung Override theo tung doi tuong cu the
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public virtual bool MapData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return MapData(dt.Rows[0]);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Ham Insert 
        /// </summary>
        /// <returns></returns>
        public virtual int Insert()
        {
            //Cai dat ham insert o day
            return 0;
        }

        /// <summary>
        /// Ham Delete
        /// </summary>
        /// <returns></returns>
        public virtual int Delete()
        {
            //Cai dat ham insert o day
            return 0;
        }

        /// <summary>
        /// Ham Update
        /// </summary>
        /// <returns></returns>
        public virtual int Update()
        {
            //Cai dat ham insert o day
            return 0;
        }

        /// <summary>
        /// Ham Save
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            //Cai dat ham Save o day
            return 0;
        }


        /// <summary>
        /// Lay danh sach doi tuong 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public virtual DataSet GetListObjects()
        {
            //Qua ben kia cai dat
            DataSet ds = null;
            return ds;
        }

        /// <summary>
        /// Ham lay du lieu theo dieu kien where
        /// </summary>
        /// <param name="strFilter"></param>
        /// <returns></returns>
        public virtual DataSet GetListObjects(string strWhereCondition)
        {
            //Qua ben kia cai dat
            DataSet ds = null;
            return ds;
        }

        /// <summary>
        /// Ham lay du lieu theo dieu kien where va cot de Order
        /// </summary>
        /// <param name="strFilter"></param>
        /// <param name="strColumName"></param>
        /// <returns></returns>
        public virtual DataSet GetListObjects(string strWhereCondition , string strColumnName)
        {
            //Qua ben kia cai dat
            DataSet ds = null;
            return ds;
        }

        /// <summary>
        /// Ham lay du lieu theo dieu kien where cua tung cot kem theo kieu du lieu
        /// Moi doi tuong phai tuong ung nhau
        /// </summary>
        /// <param name="strFilter"></param>
        /// <param name="strColumName"></param>
        /// <returns></returns>
        public virtual DataSet GetListObjects(string[] strColumnName, string[] strWhereCondition, DbType[] type)
        {
            //Qua ben kia cai dat
            DataSet ds = null;
            return ds;
        }        

        /// <summary>
        /// Reset Data
        /// </summary>
        public virtual void Reset()
        {
            this._objChildID = null;
            this._objID  = null;
            this._objMe  = null;
            this._objName  = null;
            this._objOriID   = null;
            this._objParentID  = null;
            this._reVal  = Constants.NullInt ;
            this._strXml  = Constants.NullString ;
            this._tableName  = Constants.NullString ;
        }

        /// <summary>
        /// Check value of object
        /// </summary>
        /// <returns></returns>
        public virtual string CheckValidate(bool IsUpdate)
        {
            string s = "";
            return s;
        }

        public string RealStringValidate(string strVal)
        {
            return strVal.Replace("\n", "");
        }
        #endregion

        #region "Support Functions"

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Int 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static int GetInt(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToInt32(row[columnName]) : Constants.NullInt;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu DateTime 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static DateTime GetDateTime(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToDateTime(row[columnName]) : Constants.NullDateTime;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Decimal 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static Decimal GetDecimal(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToDecimal(row[columnName]) : Constants.NullDecimal;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Bool 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static bool GetBool(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToBoolean(row[columnName]) : false;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu String 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static string GetString(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToString(row[columnName]) : Constants.NullString;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Double 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static double GetDouble(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToDouble(row[columnName]) : Constants.NullDouble;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Float 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static float GetFloat(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? Convert.ToSingle(row[columnName]) : Constants.NullFloat;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu GUID 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static Guid GetGuid(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? (Guid)(row[columnName]) : Constants.NullGuid;
        }

        /// <summary>
        /// Ham chuyen doi tu DataRow sang Kieu du lieu Long Int 
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">TenCot</param>
        /// <returns></returns>
        protected static long GetLong(DataRow row, string columnName)
        {
            return (row[columnName] != DBNull.Value) ? (long)(row[columnName]) : Constants.NullLong;
        }

        #endregion
    }
}
