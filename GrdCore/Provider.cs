using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;

namespace GrdCore
{
    public class Provider
    {
        #region Variables
        public static string _provider;
        public static string _connectionString;
        public static string _userID;

        public static string _sndConnectionString;
        #endregion

        #region Functions
        public static DbConnection GetConnection()
        {
            switch (_provider)
            {
                case "System.Data.SqlClient":
                    {
                        return new SqlConnection(_connectionString);
                    }
                case "OleDb":
                    {
                        return new OleDbConnection(_connectionString);
                    }
                case "Odbc":
                    {
                        return new OdbcConnection(_connectionString);
                    }
                default:
                    {
                        throw new Exception("Not support this provider type.");                        
                    }            
            }
        }

        public static DbConnection GetSecondConnection()
        {
            switch (_provider)
            {
                case "System.Data.SqlClient":
                    {
                        return new SqlConnection(_sndConnectionString);
                    }
                case "OleDb":
                    {
                        return new OleDbConnection(_sndConnectionString);
                    }
                case "Odbc":
                    {
                        return new OdbcConnection(_sndConnectionString);
                    }
                default:
                    {
                        throw new Exception("Not support this provider type.");
                    }
            }
        }
        #endregion
    }
}
