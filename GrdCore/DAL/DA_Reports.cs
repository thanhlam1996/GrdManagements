using GrdCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.DAL
{
    class DA_Reports
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static DataTable GetTemplateReports(string reportName)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_SCR_TemplateReports_ReportName";
                //

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));

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
        public static DataTable GetTemplateReportsGrd(string reportName)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_SCR_TemplateReports_ReportName";
                //sp_Grd_Sel_psc_SCR_TemplateReports_ReportName

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));

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

        public static void SaveTemplateReports(string reportName, byte[] reportData)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Save_psc_SCR_TemplateReports_ReportName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Data", DbType.Binary, reportData));

                DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteTemplateReports(string reportName)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_SCR_TemplateReports_ReportName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));

                DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetReportTemplateID(int reportTemplateID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_ReportTemplates_ReportTemplateID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportTemplateID));

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

        public static DataTable GetTemplateReports(string reportName, string updateStaff, string formID, string editTime)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_SCR_TemplateReports_ReportName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FormID", DbType.String, formID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormID", DbType.String, reportFormID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@EditTime", DbType.Int32, editTime));

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

        //public static void SaveTemplateReports(string reportName, byte[] reportData, string path, string updateStaff, string FormID, string FormName
        //    , string ReportFormID, string ReportFormName, string ReportNameUser, string editTime)
        public static void SaveTemplateReports(string XmlData)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Save_psc_Grd_ReportTemplates_ReportName";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@xmlData", DbType.String, XmlData));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Data", DbType.Binary, reportData));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Path", DbType.String, path));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FormID", DbType.String, FormID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormID", DbType.String, ReportFormID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormName", DbType.String, ReportFormName));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportNameUser", DbType.String, ReportNameUser));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@EditTime", DbType.Int32, editTime));
                DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveAsTemplateReports(string reportName, byte[] reportData, string path, string updateStaff, string FormID, string FormName
            , string ReportFormID, string ReportFormName, string ReportNameUser)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_SaveAs_psc_SCR_TemplateReports_ReportName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Data", DbType.Binary, reportData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Path", DbType.String, path));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FormID", DbType.String, FormID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FormName", DbType.String, FormName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormID", DbType.String, ReportFormID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormName", DbType.String, ReportFormName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportNameUser", DbType.String, ReportNameUser));

                DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteTemplateReports(string reportName, string updateStaff, string formID, string reportFormID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_SCR_TemplateReports_ReportName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FormID", DbType.String, formID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFormID", DbType.String, reportFormID));

                DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetReportGroups()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_ReportTemplateGroups";

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

        public static int DeleteReports(int reportID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_Reports_ReportID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportID", DbType.Int32, reportID));
                DbParameter dbPrmRe = dbCmd.CreateParameter(); dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbPrmRe);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetReportByReportTemplateID(int reportTemplateID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_Reports_ReportTemplateID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportTemplateID));

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

        public static DataTable GetReportByReportID(int reportID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_Reports_ReportID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportID", DbType.Int32, reportID));

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

        public static DataTable GetReportTemplateByGroupID(int reportTemplateGroupID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_ReportTemplates_ReportTemplateGroupID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateGroupID", DbType.Int32, reportTemplateGroupID));

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

        public static DataTable GetReportTemplate(int reportTemplateID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_ReportTemplates_ReportTemplateID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportTemplateID));

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

        public static DataTable GetReportTemplates(string reportFileName)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_ReportTemplates_ReportFileName";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFileName", DbType.String, reportFileName));

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

        public static int InsertReportTemplate(ReportTemplates reportTemplateInfo)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Save_psc_ReportTemplates";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateName", DbType.String, reportTemplateInfo.ReportTemplateName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateXML", DbType.String, reportTemplateInfo.ReportTemplateXMLForSaving));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateBy", DbType.String, reportTemplateInfo.UpdateBy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFileName", DbType.String, reportTemplateInfo.ReportFileName));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateGroupID", DbType.Int32, reportTemplateInfo.ReportTemplateGroupID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@IsSaveHistoryReport", DbType.Boolean, reportTemplateInfo.IsSaveHistoryReport));

                if (reportTemplateInfo.ReportTemplateBinary != null)
                    dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateBinary", DbType.String, reportTemplateInfo.ReportTemplateBinary));
                else
                    dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateBinary", DbType.String, DBNull.Value));

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

        public static int DeleteReportTemplate(int reportTemplateID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_ReportTemplates_ReportTemplateID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportTemplateID));

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

        public static int UpdateReportTemplate(ReportTemplates reportTemplateInfo)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Upd_psc_ReportTemplates";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportTemplateInfo.ReportTemplateID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateName", DbType.String, reportTemplateInfo.ReportTemplateName));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateXML", DbType.String, reportTemplateInfo.ReportTemplateXMLForSaving));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateBy", DbType.String, reportTemplateInfo.UpdateBy));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportFileName", DbType.String, reportTemplateInfo.ReportFileName));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateGroupID", DbType.Int32, reportTemplateInfo.ReportTemplateGroupID));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@IsSaveHistoryReport", DbType.Boolean, reportTemplateInfo.IsSaveHistoryReport));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@QuocHieu", DbType.Boolean, reportTemplateInfo.QuocHieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Logo", DbType.Boolean, reportTemplateInfo.Logo));

                //if (reportTemplateInfo.ReportTemplateBinary != null)
                //    dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateBinary", DbType.String, reportTemplateInfo.ReportTemplateBinary));
                //else
                //    dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateBinary", DbType.String, DBNull.Value));

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

        public static int InsertReports(Reports reportInfo)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Save_psc_Reports";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportDataXml", DbType.String, reportInfo.ReportDataXml));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportTemplateID", DbType.Int32, reportInfo.ReportTemplateID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportDescription", DbType.String, reportInfo.ReportDescription));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateBy", DbType.String, reportInfo.UpdateBy));

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
        /// <summary>
        /// Thong ke so luong cap bang tot nghiep theo dot
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static DataSet ThongKeTotNghieptheoDot(string strXml)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ThongKeTotNghiepTheoDot";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string name = "Decision|DiplomasTypeQuantity|DiplomasType";
                string[] n = name.Split('|');
                ds.Load(dr, LoadOption.OverwriteChanges, n);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Thống kê xếp loại tốt nghiệp theo ngành, đợt
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static DataSet ThongKeXepLoaiTotNghiepTheoNganh(string strXml)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ThongKeTotNghiepTheoNganh";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string name = "Decision|OlogyDetail|Total";
                string[] n = name.Split('|');
                ds.Load(dr, LoadOption.OverwriteChanges, n);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Bien ban kiem ke van bang
        /// </summary>
        /// <param name="PeriodOfGrantID"></param>
        /// <returns></returns>
        public static DataSet BienBanKiemVanBang(int PeriodOfGrantID)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_BienBanKiemKeVanBang";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@PeriodOfGrantID", DbType.Int32, PeriodOfGrantID));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string name = "DecisionDetail|Decision|Total";
                string[] n = name.Split('|');
                ds.Load(dr, LoadOption.OverwriteChanges, n);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
