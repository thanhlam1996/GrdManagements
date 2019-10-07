using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace GrdCore.DAL
{
    public class DA_DoiTuongPhanQuyen
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static DataTable LuoiHienThi()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DynamicGrids";

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
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_DynamicGrids";

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
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DynamicGrids";

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
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DynamicGridColumns_GridID";

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
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_DynamicGridColumns_GridID";

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
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DynamicGridColumns_GridID";

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

        public static DataTable ThongTinDongDauKyTen(string staffID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_NguoiDongDauKyTen_StaffID";

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
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_NguoiDongDauKyTen_UpdateStaff";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbPrmRe = dbCmd.CreateParameter(); dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                if (Convert.ToInt32(dbPrmRe.Value.ToString()) == 0)
                    return "Lưu thành công...";

                return "Lưu không thành công.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string XoaThongTinDongDauKyTen(string strXml, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_NguoiDongDauKyTen_UpdateStaff";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
                DbParameter dbPrmRe = dbCmd.CreateParameter(); dbPrmRe = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4); dbCmd.Parameters.Add(dbPrmRe);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
                if (Convert.ToInt32(dbPrmRe.Value.ToString()) == 0)
                    return "Xóa thành công...";

                return "Xóa không thành công.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
