using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.DAL
{
    public class DA_InBang
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static DataTable TruongDuLieuIn()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_TruongDuLieuIn";

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

        public static int LuuTruongDuLieuIn(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_TruongDuLieuIn";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NguoiCapNhat", DbType.String, updateStaff));

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

        public static int XoaTruongDuLieuIn(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Del_psc_Graduate_TruongDuLieuIn";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NguoiCapNhat", DbType.String, updateStaff));

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

        public static DataTable LoaiChungChi_MaHinhThucCapChungChi(string maHinhThucCapChungChi)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_LoaiXet_MaHinhThuc";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaHinhThuc", DbType.String, maHinhThucCapChungChi));

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

        public static DataTable MauBangMauChungChi(bool mauBang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_MauBangMauChungChi";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MauBang", DbType.Boolean, mauBang));

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

        public static string LuuMauBangMauChungChi(string xmlData, string updateStaff, bool mauBang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_MauBangMauChungChi";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MauBang", DbType.Boolean, mauBang));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

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

        public static string XoaMauBangMauChungChi(string xmlData, string updateStaff, bool mauBang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Del_psc_Graduate_MauBangMauChungChi";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MauBang", DbType.Boolean, mauBang));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

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

        public static DataTable GetTemplateReports(int maMauIn, bool mauBang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_TemplateReports";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaMauIn", DbType.Int32, maMauIn));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MauBang", DbType.Boolean, mauBang));

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

        public static string SaveTemplateReports(byte[] reportData, int maMauIn, bool mauBang, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_TemplateReports";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MauBang", DbType.Boolean, mauBang));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaMauIn", DbType.Int32, maMauIn));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Data", DbType.Binary, reportData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

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

        public static DataTable DotCap(string maLoaiChungChi, string namHoc, string hocKy, string bacDT, string loaiHinhDaoTao, int excel)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_DotCap";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NamHoc", DbType.String, namHoc));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@HocKy", DbType.String, hocKy));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@BacDaoTao", DbType.String, bacDT));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@LoaiHinhDaoTao", DbType.String, loaiHinhDaoTao));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@Excel", DbType.Int32, excel));

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

        public static string LuuDotCap(string xmlData, string maLoaiChungChi, string nguoiCapNhat, string namHoc, string hocKy, string bacDT, string loaiHinhDaoTao)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_DotCap";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NguoiCapNhat", DbType.String, nguoiCapNhat));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NamHoc", DbType.String, namHoc));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@HocKy", DbType.String, hocKy));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@BacDaoTao", DbType.String, bacDT));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@LoaiHinhDaoTao", DbType.String, loaiHinhDaoTao));

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

        public static string XoaDotCap(string xmlData, string maLoaiChungChi, string nguoiCapNhat, string namHoc, string hocKy, string bacDT, string loaiHinhDaoTao)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Del_psc_Graduate_DotCap";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NguoiCapNhat", DbType.String, nguoiCapNhat));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@NamHoc", DbType.String, namHoc));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@HocKy", DbType.String, hocKy));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@BacDaoTao", DbType.String, bacDT));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@LoaiHinhDaoTao", DbType.String, loaiHinhDaoTao));

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

        public static DataTable DanhSachCapQuyetDinhCongNhan(string maLoaiChungChi, string maDotXet)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_QuyetDinhCongNhan_MaDotXet";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));

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

        public static string CapNhatVaHuySoThuTuQuyetDinhCongNhan(string maLoaiChungChi, string maDotXet, string maChuan, string maChuongTrinh, string maSinhVien, string soThuTu, string nguoiCapNhat)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_QuyetDinhCongNhan_STT";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuan", DbType.String, maChuan));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuongTrinhDaoTao", DbType.String, maChuongTrinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaSinhVien", DbType.String, maSinhVien));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@STT", DbType.String, soThuTu));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                {
                    if (soThuTu == string.Empty)
                        result = "Hủy thành công...";
                    else
                        result = "Cập nhật thành công...";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string CapNhatVaHuyQuyetDinhCongNhan(string maLoaiChungChi, string maDotXet, string maChuan, string maChuongTrinh, string maSinhVien
            , string soQuyetDinh, int loaiQuyetDinh, string nguoiCapNhat, int TinhTrang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_QuyetDinhCongNhan_CapQuyetDinhCongNhan";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuan", DbType.String, maChuan));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuongTrinhDaoTao", DbType.String, maChuongTrinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaSinhVien", DbType.String, maSinhVien));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@DecisionNumber", DbType.String, soQuyetDinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int32, loaiQuyetDinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@StudyStatusID", DbType.Int32, TinhTrang));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Cấp quyết định công nhận thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachCapQuyetDinhHoanThanh(string maLoaiChungChi, string maDotXet)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_QuyetDinhHoanThanh_MaDotXet";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));

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

        public static string CapNhatVaHuySoThuTuQuyetDinhHoanThanh(string maLoaiChungChi, string maDotXet, string maChuan, string maChuongTrinh, string maSinhVien, string soThuTu, string nguoiCapNhat)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_QuyetDinhHoanThanh_STT";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuan", DbType.String, maChuan));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuongTrinhDaoTao", DbType.String, maChuongTrinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaSinhVien", DbType.String, maSinhVien));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@STT", DbType.String, soThuTu));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                {
                    if (soThuTu == string.Empty)
                        result = "Hủy thành công...";
                    else
                        result = "Cập nhật thành công...";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string CapNhatVaHuyQuyetDinhHoanThanh(string maLoaiChungChi, string maDotXet, string maChuan, string maChuongTrinh, string maSinhVien, string soQuyetDinh, int loaiQuyetDinh, string nguoiCapNhat)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Save_psc_Graduate_QuyetDinhHoanThanh_CapQuyetDinhHoanThanh";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuan", DbType.String, maChuan));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaChuongTrinhDaoTao", DbType.String, maChuongTrinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaSinhVien", DbType.String, maSinhVien));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@DecisionNumber", DbType.String, soQuyetDinh));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@DecisionTypeID", DbType.Int32, loaiQuyetDinh));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Cấp quyết định hoàn thành thành công...";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetStudyStatus()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_StudyStatus";

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

        public static DataSet QuyetDinhCongNhanTotNghiepDotCapBang(string BacDT, string HeDT, string NamHoc, string HocKy, string loaiXet, int maDotCap)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_QuyetDinhCongNhan_MaDotCap";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduateLevelID", DbType.String, BacDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyTypeID", DbType.String, HeDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@YearStudy", DbType.String, NamHoc));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@TermID", DbType.String, HocKy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, loaiXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.Int32, maDotCap));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "QDNgoaiDotcap|QDDotCap";
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

        public static DataSet DanhSachCapDotXet(string BacDT, string HeDT, string NamHoc, string HocKy, string loaiXet, string maDotXet, int maDotCap)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Graduate_DanhSachCapTheoDotCap_MaDotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduateLevelID", DbType.String, BacDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyTypeID", DbType.String, HeDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@YearStudy", DbType.String, NamHoc));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@TermID", DbType.String, HocKy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, loaiXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.Int32, maDotCap));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "DanhSachNgoai|DanhSachTrongDot";
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

        public static string LuuQuyetDinhVaDotCapBang(string xmlData, int maDotCap, string nguoiCapNhat)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Upd_SYNONYMS_QuyetDinhCongNhan_DotCapBang";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.Int32, maDotCap));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Thêm thành công...";

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string HuyQuyetDinhVaDotCapBang(string xmlData, int maDotCap, string nguoiCapNhat)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_QuyetDinhCongNhan_DotCapBang";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.Int32, maDotCap));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, nguoiCapNhat));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DAL.DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);

                string result = string.Empty;
                if (Convert.ToInt32(dbReVal.Value.ToString()) == 0)
                    result = "Hủy thành công...";

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachDuocCapBangChungChi(string maDotCapBang, bool lamMoiDuLieu, string GraduationDegreeID)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_psc_GRD_DanhSachCapBangChungChi_MaDotCapBang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.String, maDotCapBang));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LamMoiDuLieu", DbType.Boolean, lamMoiDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduationDegreeID", DbType.String, GraduationDegreeID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void HuyTinhTrangDaInBang(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Upd_psc_GRD_DanhSachCapBangChungChi_HuyDaInBang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void LuuThongTinBangChungChi(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Upd_psc_GRD_DanhSachCapBangChungChi_SoHieuBang_SoVaoSo_NgayNhanBang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable MauBangMauChungChi(string maLoaiChungChi, int MaMauIn)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_SYNONYMS_MauBangMauChungChi_MaLoaiChungChi";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaMauIn", DbType.Int32, MaMauIn));
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

        public static DataTable GetTemplateReports(string reportName)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_psc_Graduate_MauBangMauChungChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ReportName", DbType.String, reportName));

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

        public static DataTable ThongTinCapBangChungChi(string xmlData, string MaMauIn)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_psc_GRD_DanhSachCapBangChungChi_XmlData";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaMauIn", DbType.String, MaMauIn));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region public static DataTable DanhSachCongNhanTotNghiep(string XmlData)
        public static DataTable DanhSachCongNhanTotNghiep(string strXml)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienCongNhanTN_UEL";
                //dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienCongNhanTN";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable GiayChungNhanTotNghiep_CDYT(string XmlData)
        public static DataTable GiayChungNhanTotNghiep_CDYT(string MaDotXet, string MaLoaiCC, string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_psc_GiayChungNhanTN_CDYT";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, MaLoaiCC));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable DanhSachDatTotNghiep(string MaDotXet)
        public static DataTable DanhSachDatTotNghiep(string MaDotXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienDatTN";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable DanhSachKhongDatTotNghiep(string MaDotXet)
        public static DataTable DanhSachKhongDatTotNghiep(string MaDotXet, string CurriculumGroupID)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienKhongDatTN";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));
                //dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurriculumGroupID", DbType.String, CurriculumGroupID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataTable DanhSachKhongDatTotNghiep_NhomAV(string MaDotXet, string CurriculumGroupID)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienKhongDatTN_NhomAV";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CurriculumGroupID", DbType.String, CurriculumGroupID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable KetQuaXepLoaiBangTN(string MaDotXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_KetQuaXepLoaiTN";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable GiayChungNhanTotNghiepTamThoi_DNU(string XmlData)
        public static DataTable GiayChungNhanTotNghiepTamThoi_DNU(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_psc_GiayChungNhanTN_CDYT";
                //"sp_Grd_Sel_KetQuaXet_HoanThanhKhoaHoc";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static DataTable GiayChungNhanHoanThanh_UEL(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_KetQuaXet_HoanThanhKhoaHoc";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #region SoGocCapbangChungChi_DNU
        public static DataTable SoGocCapbangChungChi_DNU(string MaDotCap)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_SoGocCapChungChi_DanhSachSinhVienTN_DNU";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.String, MaDotCap));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static DataTable DanhSachNoSach_TV(string MaDotXet, int No)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienNoPhi_Sach";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@No", DbType.Int16, No));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataTable SoGocCapBangTN_UEL(string strXml)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_GRD_Sel_SoGocCapBangTN_UEL";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #region DanhSachCongNhanTotNghiep_KhongXetQD_UEL (Công nhận TN / In UEL)
        internal static DataTable DanhSachCongNhanTotNghiep_KhongXetQD_UEL(string maDotXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_DanhSachSinhVienCongNhanTN_KhongXetQD";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, maDotXet));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
    }
}
