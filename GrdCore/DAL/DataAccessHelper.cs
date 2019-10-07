using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;
using System.Net;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GrdCore.DAL
{
    class DataAccessHelper
    {
        public static void ExecuteNonQuery(DbCommand dbCmd)
        {
            try
            {
                if (dbCmd.Connection.State != ConnectionState.Open)
                {
                    try
                    {
                        dbCmd.Connection.Open();
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                }
                dbCmd.CommandTimeout = 5000;
                dbCmd.ExecuteNonQuery();

                //Chay phan Log
                try
                {
                    ExecuteForLog(dbCmd);
                }
                catch { }

                //Stored procedure is in use
                try
                {
                    StoredProcedureIsInUsed(dbCmd);
                }
                catch { }

                dbCmd.Parameters.Clear();
                dbCmd.Connection.Close();
            }
            catch (SqlException ex)
            {
                dbCmd.Parameters.Clear();
                dbCmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                dbCmd.Parameters.Clear();
                dbCmd.Connection.Close();
                throw new Exception(ex.Message);
            }
        }

        public static DbDataReader ExecuteReader(DbCommand dbCmd)
        {
            try
            {
                if (dbCmd.Connection.State != ConnectionState.Closed)
                {
                    dbCmd.Connection.Close();
                }
                //
                if (dbCmd.Connection.State != ConnectionState.Open)
                {
                    try
                    {
                        dbCmd.Connection.Open();
                        
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                }

                dbCmd.CommandTimeout = 5000;

                //Stored procedure is in use
                try
                {
                    StoredProcedureIsInUsed(dbCmd);
                }
                catch { }

                DbDataReader rv = dbCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dbCmd.Parameters.Clear();
                return rv;
            }
            catch(Exception ex)
            {
                dbCmd.Connection.Close();
                throw new Exception(ex.Message); 
            }
        }

        public static void ExecuteForLog(DbCommand dbCmd)
        {
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                string strConn = dbCmd.Connection.ConnectionString;
                string strComm = dbCmd.CommandText;
                string hostName = Dns.GetHostName();
                string userID = Provider._userID;
                string value = "<Root>";
                foreach (System.Data.Common.DbParameter para in dbCmd.Parameters)
                {
                    value += "<Error " + para.ParameterName + " = \"" + para.Value.ToString()
                            + "\"/>";
                }
                value += "</Root>";
                string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
                System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(strConn);
                sqlConn.Open();
                sqlComm = new System.Data.SqlClient.SqlCommand(strComm, sqlConn);
                sqlComm.CommandText = "insert into [CoreUis_Log].dbo.psc_SystemLog (HostName, UserID, Command, Value, UpdateDate, UpdateStaff, IPAddress, DataBaseName) values "
                                    + "('" + hostName + "','" + SystemInformation.UserName + "','" + strComm + "','" + value + "', getdate() ,'" + userID + "','" + ip + "', 'GrdManagements')";

                sqlComm.CommandType = CommandType.Text;
                sqlComm.ExecuteNonQuery();
                sqlComm.Connection.Close();
            }
            catch
            {
                sqlComm.Connection.Close();
            }
        }

        public static void StoredProcedureIsInUsed(DbCommand dbCmd)
        {
            SqlCommand sqlComm = null;
            try
            {
                string strConn = dbCmd.Connection.ConnectionString;
                string strComm = dbCmd.CommandText;
                SqlConnection sqlConn = new SqlConnection(strConn);
                sqlConn.Open();
                sqlComm = new SqlCommand(strComm, sqlConn);
                sqlComm.CommandText = "sp_Graduate_Save_psc_Graduate_Command";
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Command", DbType.String, strComm));

                sqlComm.ExecuteNonQuery();
                sqlComm.Connection.Close();
            }
            catch
            {
                sqlComm.Connection.Close();
            }
        } 
    }
}
