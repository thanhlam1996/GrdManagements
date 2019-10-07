using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.DAL
{
   public class DA_PhoiBang
    {
        private static DbConnection dbConn = Provider.GetConnection();
        /// <summary>
        /// Danh mục phôi bằng
        /// </summary>
        /// <returns>List all </returns>
        public static DataTable Danhmucphoibang(int PeriodOfGrantID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhMucPhoiBang";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd,"@PeriodOfGrantID", DbType.Int32, PeriodOfGrantID));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Danh mục loại phôi bằng
        /// </summary>
        /// <returns>Get all</returns>
        public static DataTable DanhMucLoaiPhoiBang()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhMucLoaiPhoiBang";
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get All bảng psc_Grd_DiplomasType_Ology
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_CauHinhLoaiPhoi_Nganh()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_CauHinhLoaiPhoi_Nganh";
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Lưu cấu hình loại phôi với ngành
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Update_CauHinhLoaiPhoi_Nganh(string strXml, string UpdateStaff)
        {
            try
            {   
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Upd_CauHinhLoaiPhoi_Nganh";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd,"@Reval", DbType.String,200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Them moi hoặc cập nhật danh muc phoi  
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Insert_DanhMucPhoi(string strXml, string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_DanhMucPhoi";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff",DbType.String, UpdateStaff));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Xoa danh muc phoi bang
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Delete_DanhMucPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Del_DanhMucPhoi";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Câp nhật hoặc tao mới danh mục loại phôi
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Insert_DanhMucLoaiPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_DanhMucLoaiPhoiBang";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Xoa danh muc loai phooi
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Delete_DanhMucLoaiPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Del_DanhMucLoaiPhoi";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Kiểm tra Update, Delete Danh muc phoi co thoa dieu kien
        /// </summary>
        /// <returns></returns>
        public static DataTable Check_DanhMucPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Check_IsDelete_Update";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Kiem tra delete Loai Phoi
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static DataTable Check_DanhMucLoaiPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Check_IsDelete_Update_DiplomasType";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Danh sách phôi theo lô
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static DataTable DanhSachPhoiTheoLo(int PeriodOfGrantID, string _ShipmentsID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_PhoiChiTiet";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ShipmentsID", DbType.String, _ShipmentsID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@PeriodOfGrantID", DbType.Int32, PeriodOfGrantID));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Lay danh muc phoi join loai phoi
        /// </summary>
        /// <param name="_ShipmentsID"></param>
        /// <returns></returns>
        public static DataTable DanhSachPhoiJoinLoaiPhoi(int _PeriodOfGrantID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhMucPhoiBangJoinDiplomasType";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@PeriodOfGrantID", DbType.Int32, _PeriodOfGrantID));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Update chi tiet phoi(Lý do hủy, huy phoi)
        /// </summary>
        /// <param name="_Reason"></param>
        /// <param name="_isUpdate"></param>
        /// <param name="_AutoID"></param>
        /// <param name="_SerialNumberID"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Update_ChiTietPhoi(string _Reason,bool _isUpdate, int _AutoID,string _SerialNumberID, string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_StatusDiplomas";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Reason", DbType.String, _Reason));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@isUpdate", DbType.Boolean, _isUpdate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@AutoID", DbType.Int16, _AutoID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SerialNumberID", DbType.String, _SerialNumberID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Sử dụng lại phôi đã hủy, chuyển trạng thái phôi từ -1 sang 1 và xóa lý do hủy phôi
        /// </summary>
        /// <param name="_AutoID"></param>
        /// <param name="_SerialNumberID"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Update_SuDungLaiPhoiDaHuy(int _AutoID, string _SerialNumberID, string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Upd_StatusDiplomas_Reuse";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@AutoID", DbType.Int16, _AutoID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SerialNumberID", DbType.String, _SerialNumberID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get All Danh muc dot cap phoi
        /// </summary>
        /// <returns></returns>
        public static DataTable DanhMucDotCapPhoi()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhMucDotCapPhoiBang";
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// insert and update Danh muc Dot cap phoi
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Insert_DanhMucDoiCapPhoi(string strXml, string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_DanhMucDotCapPhoiBang";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Check Delete PeriodOfGrant if(exist) => Not delete
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static DataTable Check_DanhMucDotCapPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Check_IsDelete_Update_PeriodOfGrant";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete đợt cấp phôi nếu điều kiện check ở trên là hợp lệ
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Delete_DotCapPhoi(string strXml)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DbParameter dbPrmRe = dbCmd.CreateParameter();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Del_DotCapPhoi";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@strXml", DbType.String, strXml));
                dbPrmRe = DACommon.CreateOutputParameter(dbCmd, "@Reval", DbType.String, 200);
                dbCmd.Parameters.Add(dbPrmRe);
                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return dbPrmRe.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
