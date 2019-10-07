using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using GrdCore.Entities;

namespace GrdCore.DAL
{
    class DA_Decision
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static DataTable GetDecisionByDecisionNumber(string decisionNumber)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_Decisions_DecisionNumber";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionNumber", DbType.String, decisionNumber));
                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int InsertDecision(Decisions decisionInfo)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_Decisions";
                
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionAlias", DbType.String, decisionInfo.DecisionAlias));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignStaff", DbType.String, decisionInfo.SignStaff));
                if (decisionInfo.SignDate == string.Empty) dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignDate", DbType.String, DBNull.Value));
                else dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignDate", DbType.String, decisionInfo.SignDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Reason", DbType.String, decisionInfo.Reason));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, decisionInfo.UpdateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int16, decisionInfo.DecisionTypeID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@IsInUsed", DbType.Boolean, decisionInfo.IsInUsed));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Note", DbType.String, decisionInfo.Note));
                DbParameter dbReVal = dbCmd.CreateParameter(); dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteDecision(string decisionNumberHuy, int decisionTypeID, string StaffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DecisionNumber";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionNumber", DbType.String, decisionNumberHuy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, StaffID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int16, decisionTypeID));
                DbParameter dbReVal = dbCmd.CreateParameter(); dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteDecision(string decisionNumber)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DecisionNumber";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionNumber", DbType.String, decisionNumber));
                DbParameter dbReVal = dbCmd.CreateParameter(); dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateDecision(Decisions decisionInfo, string oldDecisionNumber)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Upd_SYNONYMS_Decisions_DecisionNumber";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionAlias", DbType.String, decisionInfo.DecisionAlias));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignStaff", DbType.String, decisionInfo.SignStaff));
                if (decisionInfo.SignDate == string.Empty) dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignDate", DbType.String, DBNull.Value));
                else dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SignDate", DbType.String, decisionInfo.SignDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Reason", DbType.String, decisionInfo.Reason));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, decisionInfo.UpdateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int16, decisionInfo.DecisionTypeID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@IsInUsed", DbType.Boolean, decisionInfo.IsInUsed));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OldDecisionNumber", DbType.String, oldDecisionNumber));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Note", DbType.String, decisionInfo.Note));
                DbParameter dbReVal = dbCmd.CreateParameter(); dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetDecisionsByDate(string fromDate, string toDate, bool filterBySignDate, int decisionTypeID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_Decisions_Sel_FromDate_ToDate";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FromDate", DbType.String, fromDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ToDate", DbType.String, toDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FilterBySignDate", DbType.Boolean, filterBySignDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int32, decisionTypeID));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
