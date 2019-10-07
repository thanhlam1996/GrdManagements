using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Common;

namespace GrdCore.DAL
{
    public class DA_DecentralizationManagements
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static string LockUser(string StaffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_urm_GroupUser_StaffID_Lock";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, StaffID));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.String, 255);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbReVal.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetGroupUserByStaffID(string staffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_urm_GroupUser_StaffID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));

                DataTable dt = new DataTable();
                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int Login(string staffID, string password)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_Login";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "Password", DbType.String, password));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal1", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetStaffs(string userID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_urm_Staffs_StaffID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, userID));

                DataTable dt = new DataTable();
                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetStaffs()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_urm_Staffs";

                DataTable dt = new DataTable();
                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int InsertGroups(string groupID, string groupName, string description)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_urm_Group";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupName", DbType.String, groupName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Description", DbType.String, description));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteGroups(string groupID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Del_psc_urm_Group_GroupID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateGroups(string groupID, string groupName, string description, string oriGroupID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Upd_psc_urm_Group_GroupID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupName", DbType.String, groupName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Description", DbType.String, description));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OldGroupID", DbType.String, oriGroupID));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetDecentralizationByGroupIDandFormID(string groupID, string formID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_urm_Decentralization_FormID";

                DbParameter dbGroupID = dbCmd.CreateParameter();
                dbGroupID = DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID);
                dbCmd.Parameters.Add(dbGroupID);

                DbParameter dbFormID = dbCmd.CreateParameter();
                dbFormID = DACommon.CreateInputParameter(dbCmd, "@FormID", DbType.String, formID);
                dbCmd.Parameters.Add(dbFormID);

                DataTable dt = new DataTable();
                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ChangePassword(string staffID, string newPassword, string oldPassword)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Upd_psc_urm_Staffs_ChangePassword";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@NewPassword", DbType.String, newPassword));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OldPassword", DbType.String, oldPassword));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.String, 255);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbReVal.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
