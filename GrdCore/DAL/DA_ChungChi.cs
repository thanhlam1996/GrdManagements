using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.DAL
{
    class DA_ChungChi
    {
        private static DbConnection dbConn = Provider.GetConnection();

        public static int LuuLoaiXet(string XmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_LoaiXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@xmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static int XoaLoaiXet(string XmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Del_psc_LoaiChungChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@xmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static DataTable LoaiXet()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_LoaiXet";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LayDanhSachLoaiChungChi()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhSachChungChi";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LayDanhSachLoaiChungChi(string MaNhomMon)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhSachChungChiNhomMon";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaNhomMon", DbType.String, MaNhomMon));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LayThangXepLoai(string MaThangXepLoai)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ThangXepLoai";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaThangXepLoai", DbType.String, MaThangXepLoai));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CapNhatThangXepLoai(string MaThangXepLoai, string TenThangXepLoai, string MaThangXepLoai_Old, string LoaiCapNhat, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Upd_ThangXepLoai";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaThangXepLoai", DbType.String, MaThangXepLoai));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@TenThangXepLoai", DbType.String, TenThangXepLoai));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaThangXepLoai_Old", DbType.String, MaThangXepLoai_Old));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiCapNhat", DbType.String, LoaiCapNhat));
                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable XepLoai()
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_XepLoai";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable ThangXepLoaiChiTiet(string MaThangXepLoai, bool ScoreSystem)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ThangXepLoaiChiTiet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaThangXepLoai", DbType.String, MaThangXepLoai));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ScoreSystem", DbType.Boolean, ScoreSystem));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LoaiChungChiThayThe(string _MaLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_LoaiChungChiThayThe";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, _MaLoaiChungChi));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int LuuThangXepLoaiChiTiet(string XmlData, string MaThangXepLoai, bool ScoreSystem, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Save_ThangXepLoaiChiTiet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaThangXepLoai", DbType.String, MaThangXepLoai));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ScoreSystem", DbType.Boolean, ScoreSystem));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static int LuuChungChiThayThe(string XmlData, string MaLoaiChungChi)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Save_LoaiChungChiThayThe";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, MaLoaiChungChi));
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

        #region DataTable LayChuanXet(string CourseID)
        public static DataTable LayChuanXet(string CourseID, string MaLoaiXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ChuanXet";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, CourseID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiXet", DbType.String, MaLoaiXet));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region LayNhomTuChonTrongChuan(string MaChuan)
        public static DataTable LayNhomTuChonTrongChuan(string MaChuan)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_GroupSelections_MaChuan";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuan));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static DataTable LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(string graduateLevelID, string studyTypeID)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_vw_Graduate_Courses_GraduateLevelID_StudyTypeID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduateLevelID", DbType.String, graduateLevelID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyTypeID", DbType.String, studyTypeID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(string DotXet)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "[cust_GRD_Sel_vw_Graduate_Courses_psc_Grd_DotXet]";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, DotXet));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region GetRegulations()
        public static DataTable GetRegulations()
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_GRD_Sel_Regulations";
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region GetStudyPrograms(string CourseID, string OlogyID)
        public static DataTable GetStudyPrograms(string StudyProgramSearch, string CourseID)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_StudyPrograms";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, StudyProgramSearch));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, CourseID));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region GetStudyPrograms_Search(string CourseID, string StudyProgramSearch)
        public static DataTable GetStudyPrograms_Search(string CourseID, string StudyProgramSearch, string MaLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_StudyPrograms_Search";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramSearch", DbType.String, @StudyProgramSearch));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, CourseID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, MaLoaiChungChi));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region string LuuChuanXet(string strXml)
        public static int LuuChuanXet(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Ins_ChuanXet";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            //return dbReVal.Value.ToString();
            return int.Parse(dbReVal.Value.ToString());
        }
        #endregion

        #region string LuuChuanXet(string strXml)
        public static string LuuChuanXet_Copy(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_ChuanXet_Ins_Copy";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region string XoaChuanXet(string ChuanID)
        public static string XoaChuanXet(string ChuanID, string XmlData)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Del_ChuanXet";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region string LuuMonHocChuan(string strXml)
        public static string LuuMonHocChuan(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Ins_MonHocTrongChuan";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string UpdCredits(string ChuanID, string Credits, string MinGatherCredits, string SelectionCredits)
        public static string UpdCredits(string ChuanID, string Credits, string MinGatherCredits, string SelectionCredits)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_UpdCredits_ChuanXet";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Credits", DbType.String, Credits));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MinGatherCredits", DbType.String, MinGatherCredits));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@SelectionCredits", DbType.String, SelectionCredits));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string XoaMonHocChuan(string strXml)
        public static string XoaMonHocChuan(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Del_MonHocTrongChuan";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string LuuPhanNhom(string strXml)
        public static string LuuPhanNhom(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_UpdIns_GroupSelectionCurriculum";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string XoaPhanNhom(string strXml)
        public static string XoaPhanNhom(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Del_GroupSelectionCurriculum";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region string LuuHocVienChuan(string strXml)
        public static string LuuHocVienChuan(string strXml,string MaChuanXet)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Save_StudentStudyPrograms";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region string XoaHocVienChuan(string strXml)
        public static string XoaHocVienChuan(string strXml)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Del_StudentStudyPrograms";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region DataTable LayMonHocTrongChuan(string MaChuan)
        public static DataSet LayMonHocTrongChuan(string MaChuan, string MaDieuKien)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_MonHocTrongChuan";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuan));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "MonHocDinhNghiaXet|MonHocChuanXet";
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
        #endregion

        #region DataTable LayMonHocTrongChuan(string MaChuan)
        public static DataTable LayDanhSachKhoiKienThuc()
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_psc_StudyParts";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataTable GetCurriculums()
        public static DataTable GetCurriculums(string StudyProgramID)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_Curriculums";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, StudyProgramID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region DataSet NhomMonHocTuChon(string ChuanID, string maNhomTuChon)
        public static DataSet NhomMonHocTuChon(string ChuanID, string MaNhomCha, string maNhomTuChon, string MaDieuKien)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_MonHocTuChonTrongChuanTheoNhomTuChonChuan";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaNhomCon", DbType.String, maNhomTuChon));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaNhomCha", DbType.String, MaNhomCha));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "ChuaPhanNhom|PhanNhom";
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
        #endregion

        #region DataSet HocVienChuanXet(string ChuanID)
        public static DataSet HocVienChuanXet(string ChuanID)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_SinhVienChuanXet";
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "Show|Save";
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
        #endregion

        #region public static string SaveCopyMon(string ChuanID, string StudyProgramID)
        public static string SaveCopyMon(string ChuanID, string StudyProgramID, string UpdateStaff)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Ins_CopyMonHocTrongChuan";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, StudyProgramID));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string SaveCopyMon_Chuan(string ChuanID, string ChuanCopy)
        public static string SaveCopyMon_Chuan(string ChuanID, string ChuanCopy, string UpdateStaff)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Ins_CopyMonHocTrongChuan_Chuan";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, ChuanID));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanCopy", DbType.String, ChuanCopy));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region public static string XoaNhomTuChon(string ChuanID, string StudyProgramID)
        public static string XoaNhomTuChon(string MaNhomCha, string MaNhomCon)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_psc_Grd_XoaNhomTuChon";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaNhomCha", DbType.String, MaNhomCha));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaNhomCon", DbType.String, MaNhomCon));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region GetGroupSelections()
        public static DataTable GetGroupSelections(string MaChuanXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_GroupSelections";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static string SaveGroupSelections(string strXml)
        public static string SaveGroupSelections(string strXml, string UpdateStaff)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_UpdIns_GroupSelections";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, strXml));
            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region string DelGroupSelections(string GroupID)
        public static string DelGroupSelections(string GroupID)
        {
            DbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandText = "sp_Grd_Del_GroupSelections";

            dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GroupID", DbType.String, GroupID));
            DbParameter dbReVal = dbCmd.CreateParameter();
            dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
            dbCmd.Parameters.Add(dbReVal);

            DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            return dbReVal.Value.ToString();
        }
        #endregion

        #region DataTable DieuKienLamLuanVan()
        public static DataTable DieuKienLamLuanVan()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DieuKienXetLamLuanVan"; //sp_PRJ_psc_PRJ_LV_DieuKienXetLamLuanVan_Sel

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
        #endregion

        #region DataSet ThongTinChiTietDieuKienXet(string maDieuKien)
        public static DataSet ThongTinChiTietDieuKienXet(string maDieuKien)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ThongTinChiTiet_MaDieuKien";//sp_PRJ_ThongTinChiTiet_Sel_MaDieuKien

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, maDieuKien));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "CTDTApDung|MonKhongXet|DieuKienDiem";
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
        #endregion

        #region DataTable GetStudyPrograms(string courseID, string ologyID)
        //public static DataTable GetStudyPrograms(string courseID, string ologyID)
        //{
        //    try
        //    {
        //        DbCommand dbCmd = dbConn.CreateCommand();
        //        dbCmd.CommandType = CommandType.StoredProcedure;
        //        dbCmd.CommandText = "sp_Grd_ChuanXet_Sel_CourseID_OlogyID";//sp_PRJ_psc1_StudyPrograms_Sel_CourseID_OlogyID

        //        dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, courseID));
        //        dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OlogyID", DbType.String, ologyID));

        //        DataTable dt = new DataTable();
        //        DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
        //        dt.Load(dr);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        #region DataTable GetCurriculumStudyPrograms(string studyProgramID)
        public static DataTable GetCurriculumStudyPrograms(string studyProgramID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_CurriculumStudyPrograms_MaChuanXet";//sp_PRJ_psc1_CurriculumStudyPrograms_Sel_StudyProgramID

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, studyProgramID));

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
        #endregion

        #region void public static void LuuDieuKienXet(string maDieuKien, string tenDieuKien, string ghiChu, bool chungChiAV, string ctdt, string monKhongXet, string dieuKienDiem, string updateStaff, bool themMoi, bool HocPhi)
        public static void LuuDieuKienXet(string maDieuKien, string tenDieuKien, string ghiChu, bool chungChiAV, string ctdt, string monKhongXet, string dieuKienDiem, string updateStaff, bool themMoi, bool HocPhi)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Save_DieuKienXet_MaDieuKien";//sp_PRJ_DieuKienXetLamLuanVan_Save_MaDieuKien

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, maDieuKien));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@TenDieuKien", DbType.String, tenDieuKien));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GhiChu", DbType.String, ghiChu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlCTDT", DbType.String, ctdt));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlMonKhongXet", DbType.String, monKhongXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlDieuKienDiem", DbType.String, dieuKienDiem));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ThemMoi", DbType.Boolean, themMoi));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ChungChiAnhVan", DbType.Boolean, chungChiAV));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@HocPhi", DbType.Boolean, HocPhi));

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region void public static void XoaDieuKienXet(string maDieuKien, string updateStaff)
        public static void XoaDieuKienXet(string maDieuKien, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Del_DieuKienXet_MaDieuKien";//sp_PRJ_psc_PRJ_LV_DieuKienXetLamLuanVan_Del_MaDieuKien

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, maDieuKien));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion 

        public static DataTable LayHinhThucCap()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_HinhThucCap";

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

        #region void public static void CapNhatThangXepLoai(string MaThangXepLoai, string TenThangXepLoai, string MaThangXepLoai_Old,string LoaiCapNhat )
        
        #endregion 

        #region ThangXepLoaiChiTiet(string MaThangXepLoai, bool ScoreSystem)
        
        #endregion              

        #region XepLoai()
        
        #endregion

        public static DataTable DieuKienXet(string maLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DieuKienXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataSet DieuKienXetChiTiet(string maDieuKien, string maLoaiChungChi)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DieuKienXetChiTiet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, maDieuKien));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "DieuKienXet|LoaiChungChi|ThangXepLoai|ThangXepLoaiChiTiet";
                string[] TableName = ArrTable.Split('|');
                ds.Load(dr, LoadOption.PreserveChanges, TableName);
                dbCmd.Parameters.Clear();
                return ds;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #region điều kiện xét()
        public static DataTable DieuKienXetLoaiChungChi(string MaDieuKien, string MaLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DieuKienXetLoaiChungChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, MaLoaiChungChi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static int LuuDieuKien(string xmlDK, string xmlCC, string xmlXL, string updateStaff, int flagID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_DieuKienXetChiTiet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlDK", DbType.String, xmlDK));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlCC", DbType.String, xmlCC));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlXL", DbType.String, xmlXL));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FlagID", DbType.Int32, flagID));

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

        public static int XoaDieuKien(string MaDieuKien)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DieuKienXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));
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

        #region Đợt xét()
        public static DataTable DotXet(string MaDot)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDot", DbType.String, MaDot));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static int XoaDotXet(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static int LuuDotXet(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static DataTable DotXetTheoBacHe(string BacDT, string HeDT, string NamHoc, string HocKy, string loaiXet)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DotXet_GraduateLevelID_StudyTypeID_YearStudy_TermID_MaLoaiChungChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduateLevelID", DbType.String, BacDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyTypeID", DbType.String, HeDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@YearStudy", DbType.String, NamHoc));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@TermID", DbType.String, HocKy));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, loaiXet));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable SoChuanDat(string CourseID)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_SoChuanDat";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, CourseID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int LuuSoChuanDat(string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_SoChuanDat";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        #region ChuanXetTheoKhoaNganh()
        public static DataTable ChuanXetTheoKhoaNganh(string KhoaHoc, string NganhHoc, string MaDieuKien)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ChuanXetTheoKhoaNganh";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@KhoaHoc", DbType.String, KhoaHoc));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@NganhHoc", DbType.String, NganhHoc));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        public static DataTable DanhSachSinhVien_ChuanXet(string MaDot, bool XetMoi)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhSachSinhVien_ChuanXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDot", DbType.String, MaDot));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XetMoi", DbType.String, XetMoi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataSet XetBB_TC(string StudentID, string MaChuanXet, string MaDotXet, int LoaiXet, string LoaiCC)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_XetMonBatBuoc_XetTinChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, StudentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiXet", DbType.Int32, LoaiXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiCC", DbType.String, LoaiCC));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "KetQuaXet|NhomTuChon|MonTrongNhomTuChon";
                string[] TableName = ArrTable.Split('|');
                ds.Load(dr, LoadOption.PreserveChanges, TableName);
                dbCmd.Parameters.Clear();
                return ds;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #region ChuanXetTheoKhoaNganh() 
        public static int TinhDiemTB(string StudentID, string MaChuanXet)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_psc_Grd_StudentAverageScore_Graduation";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, StudentID));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region LuuKetQuaXet() 
        public static int LuuKetQuaXet(string XmlData, string ChucNang)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_psc_Grd_Save_KetQuaXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ChucNang", DbType.String, ChucNang));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region LuuChuongTrinhVaoDotXet() 
        public static int LuuChuongTrinhVaoDotXet(string XmlData)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_ThemChuongTrinhDaoTao_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region LuuChuongTrinhVaoDotXet() 
        public static int XoaChuongTrinhVaoDotXet(string XmlData)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Del_XoaChuongTrinhDaoTao_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));

                DbParameter dbReVal = dbCmd.CreateParameter();
                dbReVal = DACommon.CreateOutputParameter(dbCmd, "@ReVal", DbType.Int32, 4);
                dbCmd.Parameters.Add(dbReVal);

                DataAccessHelper.ExecuteNonQuery(dbCmd);
                return int.Parse(dbReVal.Value.ToString());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable ChiTietXet(string StudentID,string MaChuanXet)
        public static DataSet ChiTietXet(string StudentID,string MaChuanXet)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_ChiTietXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, StudentID));


                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "BangDiem|ChungChi|HoSo";
                string[] TableName = ArrTable.Split('|');
                ds.Load(dr, LoadOption.PreserveChanges, TableName);
                dbCmd.Parameters.Clear();
                return ds;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable BangDiemTotNghiep(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_BangDiemTotNghiep_HCMUP";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataTable BangDiemTotNghiepCQ_A3(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_psc_Grd_InBangDiem_Xml_ToanKhoa_TotNghiep_Diem_4_New";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public static DataTable BangDiemTotNghiepCQ_A3_TiengAnh(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_psc_Grd_InBangDiem_Xml_ToanKhoa_TotNghiep_TiengAnh_Diem_4_New";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable BangDiemTotNghiep_DNU(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_BangDiemTotNghiep_HCMUP";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region public static DataTable BangDiemXetTotNghiep(string XmlData)
        public static DataTable BangDiemXetTotNghiep(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_psc_Sel_BangDiemXetTotNghiep";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
        public static DataTable TimKiemThongTinSinhVien(string search, int loaiLocDuLieu)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_vw_Graduate_StudentInfos_Basic_Search";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StrSearch", DbType.String, search));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.Int16, loaiLocDuLieu));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable LayNganhHoc_BacDaoTao_LoaiHinhDaoTao_KhoaQuanLy(string graduateLevelID, string studyTypeID, string departmentID)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc_Ologies_GraduateLevelID_StudyTypeID_DepartmentID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@GraduateLevelID", DbType.String, graduateLevelID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyTypeID", DbType.String, studyTypeID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DepartmentID", DbType.String, departmentID));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable ChuongTrinhDaoTao(string courseID, string ologyID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc1_StudyPrograms_CourseID_OlogyID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OlogyID", DbType.String, ologyID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, courseID));

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

        public static DataTable ChuongTrinhDaoTao_TheoKhoa(string courseID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_psc1_StudyPrograms_CourseID";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, courseID));

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

        public static DataTable LayDanhSachLop(string courseID, string ologyID, string departmentID, int filter)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_vw_Graduate_ClassStudentInfos";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DepartmentID", DbType.String, departmentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, courseID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@OlogyID", DbType.String, ologyID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@Filter", DbType.Int32, filter));

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

        public static DataTable NopHoSoChungChi(string xmlData, string studentID, string classStudentID, string studyProgramID, int loaiLocDuLieu, string maLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_NopHoSoChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ClassStudentID", DbType.String, classStudentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, studyProgramID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DSNopHoSoChungChi(string fromDate, string toDate, string classStudentID, string studyProgramID, int loaiLocDuLieu, string maLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_DSDaNopHoSoChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@FromDate", DbType.String, fromDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ToDate", DbType.String, toDate));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ClassStudentID", DbType.String, classStudentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, studyProgramID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int LuuHoSoChungChi(string xmlData, string studentID, int loaiLocDuLieu, string maLoaiChungChi, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_NopHoSoChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static int XoaHoSoChungChi(string xmlData, string studentID, int loaiLocDuLieu, string maLoaiChungChi, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_NopHoSoChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static DataTable LayNhomMon()
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_CurriculumGroups";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachDieuKienXet()
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_ThongTinChiTietDieuKienXet_Sel";

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable TaoChuanXetChoCTDT(string MaCTDT, string Khoa, string MaDieuKien)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_TaoChuanXetChoCTDT_Sel";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, MaCTDT));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, Khoa));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKien", DbType.String, MaDieuKien));

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
        public static int BangChuanTemp(string XmlData,string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_TaoBangChuanTemp_Ins";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));

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

        public static DataTable LayTongTinChi(string MaChuanXet)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "psc_Grd_NhomMon_TinChi_Sel";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));

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
        #region public static DataTable LayDanhSachSinhVienTotNghiep(string MaCTDT)
        public static DataTable LayDanhSachSinhVienTotNghiep(string MaCTDT)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_DanhSachSinhVien_TotNghiep_Sel";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProGramID", DbType.String, MaCTDT));

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
        #endregion
        #region public static int LuuSinhVienHinhThuc(string DotXet, string CTDT, string XmlData, string updateStaff)
        public static int LuuSinhVienHinhThuc(string DotXet, string CTDT, string XmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_SinhVien_DotXet_HinhThuc_Ins";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@DotXet", DbType.String, DotXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, CTDT));

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@xmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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
        #endregion

        public static DataTable DanhSachCapBangChungChi(string maDotCapBang, bool lamMoiDuLieu)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_DanhSachCapBangChungChi_MaDotCapBang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCapBang", DbType.String, maDotCapBang));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LamMoiDuLieu", DbType.Boolean, lamMoiDuLieu));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachChuongTrinhDaoTao(string CourseID, string MaDieuKien)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                DataTable dt = new DataTable();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DanhSachChuongTrinhDaoTao";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@CourseID", DbType.String, CourseID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDieuKienXet", DbType.String, MaDieuKien));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable TimKiemThongTinSinhVienNhanBang(string search)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Graduate_Sel_DanhSachSinhVienCapBangChungChi";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StrSearch", DbType.String, search));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet DanhSachChuongTrinhDaoTao_DotXet(string MoDotXet)
        {
            try
            {
                DataSet ds = new DataSet();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhSachChuongTrinhDaoTao_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MoDotXet));

                DbDataReader dr = DataAccessHelper.ExecuteReader(dbCmd);
                string ArrTable = "CTDTTrongDot|SinhVien|CTDTNgoaiDot";
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

        public static DataTable XacNhanSinhVienNhanBang(string xmlData, string studentID, string classStudentID, string studyProgramID, string MaDotCap, int loaiLocDuLieu, string maLoaiChungChi)
        {
            try
            {
                DataTable dt = new DataTable();

                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_SYNONYMS_DanhSachCapBangChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@ClassStudentID", DbType.String, classStudentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudyProgramID", DbType.String, studyProgramID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotCap", DbType.String, MaDotCap));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));

                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //LuuDSSinhVienNhanBang
        public static int LuuDSSinhVienNhanBang(string xmlData, string studentID, int loaiLocDuLieu, string maLoaiChungChi, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Save_SYNONYMS_DanhSachCapBangChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@LoaiLocDuLieu", DbType.String, loaiLocDuLieu));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        public static int XoaSinhVienNhanBangCC(string xmlData, string studentID, string maLoaiChungChi, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Del_SYNONYMS_DanhSachNhanBangChungChi_LoaiLocDuLieu";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@StudentID", DbType.String, studentID));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
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

        #region DataTable PhuongThucSapXepDuLieu(string maChucNang)
        public static DataTable PhuongThucSapXepDuLieu(string maChucNang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_GRD_PhuongThuSapXep_Sel_MaChucNang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChucNang", DbType.String, maChucNang));

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
        #endregion

        #region DataTable CotHienThiTrenLuoi(string maChucNang)
        public static DataTable CotHienThiTrenLuoi(string maChucNang)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_GRD_psc_GRD_HienThi_Sel_MaChucNang";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChucNang", DbType.String, maChucNang));

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
        #endregion

        #region void LuuPhuongThucCapNhat(string maChucNang, string xmlData, string updateStaff)
        public static void LuuPhuongThucCapNhat(string maChucNang, string xmlData, string updateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_GRD_PhuongThuSapXep_Upd";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChucNang", DbType.String, maChucNang));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, updateStaff));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, xmlData));

                DAL.DataAccessHelper.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static DataTable DanhSachSinhVien_DotXet(string MaDot)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_DanhSachSinhVien_DotXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDotXet", DbType.String, MaDot));

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

        public static DataTable DanhHieu()
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_GRD_Sel_DanhHieu";

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

        public static DataTable MauBangMauChungChi_DanhHieu(string maLoaiChungChi, string GraduationDegreeID)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "cust_Grd_Sel_SYNONYMS_MauBangMauChungChi_MaLoaiChungChi_DanhHieu";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaLoaiChungChi", DbType.String, maLoaiChungChi));
                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@GraduationDegreeID", DbType.String, GraduationDegreeID));
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

        public static int DanhSachSinhVienKhongXet(string XmlData, string MaDot, string UpdateStaff)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Ins_DanhSachSinhVienKhongXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaDot", DbType.String, MaDot));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@UpdateStaff", DbType.String, UpdateStaff));

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

        public static DataTable KetQuaXetTotNghiep_DNU(string MaDotxet )
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_KetQuaXetTN_DNU";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@MaDotxet", DbType.String, MaDotxet));
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

        public static int KhoaChuanXet(string MaChuanXet, bool KhoaChuan)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Upd_KhoaChuanXet";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@MaChuanXet", DbType.String, MaChuanXet));
                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@State", DbType.Boolean, KhoaChuan));

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

        public static DataTable DSChungNhaHoanThanhKhoaHoc_UEL(string XmlData)
        {
            try
            {
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_KetQuaXet_HoanThanhKhoaHoc";

                dbCmd.Parameters.Add(DAL.DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, XmlData));
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

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable LayTieuChuanTotNghiep(string XmlData)
        {
            try
            {
                DataTable dt = new DataTable();
                DbCommand dbCmd = dbConn.CreateCommand();
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandText = "sp_Grd_Sel_TieuChuanTotNghiep";

                dbCmd.Parameters.Add(DACommon.CreateInputParameter(dbCmd, "@XmlData", DbType.String, @XmlData));


                DbDataReader dr = DAL.DataAccessHelper.ExecuteReader(dbCmd);
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
    }
}
