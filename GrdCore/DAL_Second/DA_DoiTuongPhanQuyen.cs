using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace GrdCore.DAL_Second
{
    public class DA_DoiTuongPhanQuyen
    {
        private static DbConnection dbConn = Provider.GetSecondConnection();

        public static DataTable GetGroup()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_urm_Group";

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteGroups(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_urm_Group";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveGroups(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_Group";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
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
                dbCmd.CommandText = "sp_Score_Sel_psc_urm_Staffs";

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteStaffsByXml(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_urm_Staffs";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveStaffsByXml(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Save_psc_urm_Staffs";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet PhanQuyenNhomNguoiDung(string groupID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataSet ds = new DataSet();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_PhanQuyenHeThongNhomNguoiDung";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, "GRD"));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
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

        public static int SaveGroupGraduateLevels(string groupID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_GroupGraduateLevels";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveGroupStudyTypes(string groupID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_GroupStudyTypes";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveGroupDepartments(string groupID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_GroupDepartments";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveDecentralizations(string groupID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_Decentralization";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet PhanQuyenNguoiDung(string staffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataSet ds = new DataSet();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_PhanQuyenNguoiDung";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "0|1|2|3|4";
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

        public static int SaveGroupUsers(string staffID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_GroupUser";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveGroupUsers_PQ(string groupID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_GroupUser_GroupID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, groupID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveStaffGraduateLevels(string staffID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_StaffGraduateLevel";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveStaffStudyTypes(string staffID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_StaffStudyTypes";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveStaffDepartments(string staffID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_StaffDepartments";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbPrmRe.Value.ToString());
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
                dbCmd.CommandText = "cust_SYS_Upd_psc_urm_Staffs_GiaTriHienHanh";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, userID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@CurrentTerm", DbType.String, currentTerm));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@CurrentYearStudy", DbType.String, currentYearStudy));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@CurrentGraduateLevelID", DbType.String, currentGraduateLevelID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@CurrentStudyTypeID", DbType.String, currentStudyTypeID));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);
                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LuoiHienThi()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_sys_DynamicGrids";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, "GRD"));

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string LuuLuoiHienThi(string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_sys_DynamicGrids_ModuleID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, "GRD"));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Lưu thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string XoaLuoiHienThi(string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_sys_DynamicGrids_ModuleID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, "GRD"));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Xóa thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable CotLuoiHienThi(string gridID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_sys_DynamicGridColumns_GridID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GridID", DbType.String, gridID));

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string LuuCotLuoiHienThi(string gridID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_sys_DynamicGridColumns_GridID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GridID", DbType.String, gridID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Lưu thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string XoaCotLuoiHienThi(string gridID, string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Del_psc_sys_DynamicGridColumns_GridID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GridID", DbType.String, gridID));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Xóa thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetModule()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_urm_Module_ModuleID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, "GRD"));

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveModules(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_Module";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetFormByModuleID(string moduleID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_urm_Form_ModuleID";

                DbParameter dbModuleID = dbCmd.CreateParameter();
                dbModuleID = DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, moduleID);
                dbCmd.Parameters.Add(dbModuleID);

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveForms(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_Form";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetObjects(string moduleID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_urm_FormObject_ModuleID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@ModuleID", DbType.String, moduleID));

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int SaveObjects(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_urm_FormObject";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable ThongTinDongDauKyTen(string staffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Sel_psc_Scr_ThongTinNguoiDongDauKyTen_StaffID";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StaffID", DbType.String, staffID));

                DataTable dt = new DataTable();
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string LuuThongTinDongDauKyTen(string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Score_Upd_psc_Scr_ThongTinNguoiDongDauKyTen";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbPrmRe = dbCmd.CreateParameter(); dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                if (Convert.ToInt32(dbPrmRe.Value.ToString()) == 0)
                    return "Cập nhật thành công...";

                return "Cập nhật không thành công.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
