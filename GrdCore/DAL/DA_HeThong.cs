using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.DAL
{
    public class DA_HeThong
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static DataSet GetDataSetDataDictionary(string staffID, string groupD)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataSet ds = new DataSet();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_DataSetDataDictionary";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "0|1|2|3|4|5|6";
                string[] TableName = ArrTable.Split('|');
                ds.Load(dr, LoadOption.PreserveChanges, TableName);
                dbCmd.Parameters.Clear();
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateCurrentValues(string userID, string currentTerm, string currentYearStudy, string currentGraduateLevelID, string currentStudyTypeID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Upd_psc_urm_Staffs_GiaTriHienHanh";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, userID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurrentTerm", DbType.String, currentTerm));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurrentYearStudy", DbType.String, currentYearStudy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurrentGraduateLevelID", DbType.String, currentGraduateLevelID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurrentStudyTypeID", DbType.String, currentStudyTypeID));
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
    }
}
