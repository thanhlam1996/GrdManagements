using GrdCore.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.BLL
{
    public class BL_ChungChi
    {
        public static int LuuLoaiXet(string XmlData, string updateStaff)
        {
            try
            {
                return DA_ChungChi.LuuLoaiXet(XmlData, updateStaff);
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
                return DA_ChungChi.XoaLoaiXet(XmlData, updateStaff);
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
                return DA_ChungChi.LoaiXet();
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
                return DA_ChungChi.LayDanhSachLoaiChungChi();
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
                return DA_ChungChi.LayDanhSachLoaiChungChi(MaNhomMon);
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
                return DA_ChungChi.LayThangXepLoai(MaThangXepLoai);
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
                DA_ChungChi.CapNhatThangXepLoai(MaThangXepLoai, TenThangXepLoai, MaThangXepLoai_Old, LoaiCapNhat, updateStaff);
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
                return DA_ChungChi.XepLoai();
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
                return DA_ChungChi.ThangXepLoaiChiTiet(MaThangXepLoai, ScoreSystem);
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
                return DA_ChungChi.LoaiChungChiThayThe(_MaLoaiChungChi);
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
                return DA_ChungChi.LuuThangXepLoaiChiTiet(XmlData, MaThangXepLoai, ScoreSystem, updateStaff);
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
                return DA_ChungChi.LuuChungChiThayThe(XmlData, MaLoaiChungChi);
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
                return DA_ChungChi.LayChuanXet(CourseID, MaLoaiXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region LayNhomTuChonTrongChuan(string MaChuan)
        public static DataTable LayNhomTuChonTrongChuan(string MaChuan)
        {
            try
            {
                return DA_ChungChi.LayNhomTuChonTrongChuan(MaChuan);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static DataTable LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(string graduateLevelID, string studyTypeID)
        {
            try
            {
                return DA_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(graduateLevelID,  studyTypeID);
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
                return DA_ChungChi.LayKhoaHoc_BacDaoTao_LoaiHinhDaoTao(DotXet);
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
                return DA_ChungChi.GetRegulations();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetStudyPrograms(string CourseID)
        public static DataTable GetStudyPrograms(string StudyProgramSearch, string CourseID)
        {
            try
            {
                return DA_ChungChi.GetStudyPrograms(StudyProgramSearch, CourseID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetStudyPrograms_Search(string CourseID, string StudyProgramSearch)
        public static DataTable GetStudyPrograms_Search(string CourseID, string StudyProgramSearch, string MaLoaiChungChi)
        {
            try
            {
                return DA_ChungChi.GetStudyPrograms_Search(CourseID, StudyProgramSearch, MaLoaiChungChi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string LuuChuanXet(string strXml)
        public static int LuuChuanXet(string strXml)
        {
            try
            {
                return DA_ChungChi.LuuChuanXet(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string LuuChuanXet(string strXml)
        public static string LuuChuanXet_Copy(string strXml)
        {
            try
            {
                return DA_ChungChi.LuuChuanXet_Copy(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string XoaChuanXet(string ChuanID)
        public static string XoaChuanXet(string ChuanID, string XmlData)
        {
            try
            {
                return DA_ChungChi.XoaChuanXet(ChuanID, XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string LuuMonHocChuan(string strXml)
        public static string LuuMonHocChuan(string strXml)
        {
            try
            {
                return DA_ChungChi.LuuMonHocChuan(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string UpdCredits(string ChuanID, string Credits, string MinGatherCredits, string SelectionCredits)
        public static string UpdCredits(string ChuanID, string Credits, string MinGatherCredits, string SelectionCredits)
        {
            try
            {
                return DA_ChungChi.UpdCredits(ChuanID, Credits, MinGatherCredits, SelectionCredits);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string XoaMonHocChuan(string strXml)
        public static string XoaMonHocChuan(string strXml)
        {
            try
            {
                return DA_ChungChi.XoaMonHocChuan(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string LuuPhanNhom(string strXml)
        public static string LuuPhanNhom(string strXml)
        {
            try
            {
                return DA_ChungChi.LuuPhanNhom(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string XoaPhanNhom(string strXml)
        public static string XoaPhanNhom(string strXml)
        {
            try
            {
                return DA_ChungChi.XoaPhanNhom(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string LuuHocVienChuan(string strXml)
        public static string LuuHocVienChuan(string strXml, string MaChuanXet)
        {
            try
            {
                return DA_ChungChi.LuuHocVienChuan(strXml, MaChuanXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string XoaHocVienChuan(string strXml)
        public static string XoaHocVienChuan(string strXml)
        {
            try
            {
                return DA_ChungChi.XoaHocVienChuan(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static DataSet LayMonHocTrongChuan(string MaChuan, string MaDieuKien)
        public static DataSet LayMonHocTrongChuan(string MaChuan, string MaDieuKien)
        {
            try
            {
                return DA_ChungChi.LayMonHocTrongChuan(MaChuan, MaDieuKien);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataTable LayDanhSachKhoiKienThuc(string MaChuan)
        public static DataTable LayDanhSachKhoiKienThuc()
        {
            try
            {
                return DA_ChungChi.LayDanhSachKhoiKienThuc();
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
                return DA_ChungChi.GetCurriculums(StudyProgramID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataSet NhomMonHocTuChon(string ChuanID, string maNhomTuChon)
        public static DataSet NhomMonHocTuChon(string ChuanID, string MaNhomCha, string maNhomTuChon, string MaDieuKien)
        {
            try
            {
                return DA_ChungChi.NhomMonHocTuChon(ChuanID, MaNhomCha, maNhomTuChon, MaDieuKien);
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
                return DA_ChungChi.HocVienChuanXet(ChuanID);
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
            try
            {
                return DA_ChungChi.SaveCopyMon(ChuanID, StudyProgramID, UpdateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string SaveCopyMon_Chuan(string ChuanID, string StudyProgramID)
        public static string SaveCopyMon_Chuan(string ChuanID, string ChuanCopy, string UpdateStaff)
        {
            try
            {
                return DA_ChungChi.SaveCopyMon_Chuan(ChuanID, ChuanCopy, UpdateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string XoaNhomTuChon(string ChuanID, string StudyProgramID)
        public static string XoaNhomTuChon(string MaNhomCha, string MaNhomCon)
        {
            try
            {
                return DA_ChungChi.XoaNhomTuChon(MaNhomCha, MaNhomCon);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetGroupSelections()
        public static DataTable GetGroupSelections(string MaChuanXet)
        {
            try
            {
                return DA_ChungChi.GetGroupSelections(MaChuanXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static string SaveGroupSelections(string strXml)
        public static string SaveGroupSelections(string strXml, string UpdateStaff)
        {
            try
            {
                return DA_ChungChi.SaveGroupSelections(strXml, UpdateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region string DelGroupSelections(string GroupID)
        public static string DelGroupSelections(string GroupID)
        {
            try
            {
                return DA_ChungChi.DelGroupSelections(GroupID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataTable DieuKienLamLuanVan()
        public static DataTable DieuKienLamLuanVan()
        {
            try
            {
                return DA_ChungChi.DieuKienLamLuanVan();
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
                return DA_ChungChi.ThongTinChiTietDieuKienXet(maDieuKien);
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
        //        return DA_ChungChi.GetStudyPrograms(courseID, ologyID);
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
                return DA_ChungChi.GetCurriculumStudyPrograms(studyProgramID);
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
                DA_ChungChi.LuuDieuKienXet(maDieuKien, tenDieuKien, ghiChu, chungChiAV, ctdt, monKhongXet, dieuKienDiem, updateStaff, themMoi, HocPhi);
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
                DA_ChungChi.XoaDieuKienXet(maDieuKien, updateStaff);
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
                return DA_ChungChi.LayHinhThucCap();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region LayThangXepLoai(string MaThangXepLoai)
        
        #endregion


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
                return DA_ChungChi.DieuKienXet(maLoaiChungChi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet DieuKienXetChiTiet(string maDieuKien, string maLoaiChungChi)
        {
            try
            {
                return DA_ChungChi.DieuKienXetChiTiet(maDieuKien, maLoaiChungChi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region điều kiện xét()
        public static DataTable DieuKienXetLoaiChungChi(string MaDieuKien, string MaLoaiChungChi)
        {
            try
            {
                return DA_ChungChi.DieuKienXetLoaiChungChi(MaDieuKien, MaLoaiChungChi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static int LuuDieuKien(string xmlDK, string xmlCC, string xmlXL, string updateStaff, int flagID)
        {
            try
            {
                return DA_ChungChi.LuuDieuKien(xmlDK, xmlCC, xmlXL, updateStaff, flagID);
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
                return DA_ChungChi.XoaDieuKien(MaDieuKien);
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
                return DA_ChungChi.DotXet(MaDot);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static int XoaDotXet(string xmlData, string updateStaff)
        {
            try
            {
                return DA_ChungChi.XoaDotXet(xmlData, updateStaff);
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
                return DA_ChungChi.LuuDotXet(xmlData, updateStaff);
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
                return DA_ChungChi.DotXetTheoBacHe(BacDT, HeDT, NamHoc, HocKy, loaiXet);
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
                return DA_ChungChi.ChuanXetTheoKhoaNganh(KhoaHoc, NganhHoc, MaDieuKien);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ChuanXetTheoKhoaNganh() sp_Grd_Sel_DanhSachSinhVien_ChuanXet
        public static DataTable DanhSachSinhVien_ChuanXet( string MaDot, bool XetMoi)
        {
            try
            {
                return DA_ChungChi.DanhSachSinhVien_ChuanXet( MaDot, XetMoi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static DataSet XetBB_TC(string StudentID, string MaChuanXet, string MaDotXet, int LoaiXet, string LoaiCC)
        {
            try
            {
                return DA_ChungChi.XetBB_TC(StudentID, MaChuanXet, MaDotXet, LoaiXet, LoaiCC);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region public static DataTable XetBB_TC(string StudentID, string MaChuanXet)

        #region ChuanXetTheoKhoaNganh() 
        public static int TinhDiemTB(string StudentID, string MaChuanXet)
        {
            try
            {
                return DA_ChungChi.TinhDiemTB(StudentID, MaChuanXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region LuuKetQuaXet() 
        public static int LuuKetQuaXet(string XmlData, string ChucNang)
        {
            try
            {
                return DA_ChungChi.LuuKetQuaXet(XmlData, ChucNang);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region LuuChuongTrinhVaoDotXet() 
        public static int LuuChuongTrinhVaoDotXet(string XmlData)
        {
            try
            {
                return DA_ChungChi.LuuChuongTrinhVaoDotXet(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region XoaChuongTrinhVaoDotXet() 
        public static int XoaChuongTrinhVaoDotXet(string XmlData)
        {
            try
            {
                return DA_ChungChi.XoaChuongTrinhVaoDotXet(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static DataTable ChiTietXet(string StudentID,string MaChuanXet)
        public static DataSet ChiTietXet(string StudentID, string MaChuanXet)
        {
            try
            {
                return DA_ChungChi.ChiTietXet(StudentID, MaChuanXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable BangDiemTotNghiep(string XmlData)
        {
            try
            {
                return DA_ChungChi.BangDiemTotNghiep(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable BangDiemTotNghiepCQ_A3(string XmlData)
        {
            try
            {
                return DA_ChungChi.BangDiemTotNghiepCQ_A3(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable BangDiemTotNghiepCQ_A3_TiengAnh(string XmlData)
        {
            try
            {
                return DA_ChungChi.BangDiemTotNghiepCQ_A3_TiengAnh(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static DataTable BangDiemTotNghiep(string XmlData)
        public static DataTable BangDiemTotNghiep_DNU(string XmlData)
        {
            try
            {
                return DA_ChungChi.BangDiemTotNghiep_DNU(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region public static DataTable BangDiemXetTotNghiep(string XmlData)
        public static DataTable BangDiemXetTotNghiep(string XmlData)
        {
            try
            {
                return DA_ChungChi.BangDiemXetTotNghiep(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion

        public static DataTable SoChuanDat(string CourseID)
        {
            try
            {
                return DA_ChungChi.SoChuanDat(CourseID);
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
                return DA_ChungChi.LuuSoChuanDat(xmlData, updateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable TimKiemThongTinSinhVien(string search, int loaiLocDuLieu)
        {
            try
            {
                return DA_ChungChi.TimKiemThongTinSinhVien(search, loaiLocDuLieu);
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
                return DA_ChungChi.LayNganhHoc_BacDaoTao_LoaiHinhDaoTao_KhoaQuanLy(graduateLevelID, studyTypeID, departmentID);
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
                return DA_ChungChi.ChuongTrinhDaoTao(courseID, ologyID);
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
                return DA_ChungChi.ChuongTrinhDaoTao_TheoKhoa(courseID);
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
                return DA_ChungChi.LayDanhSachLop(courseID, ologyID, departmentID, filter);
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
                return DA_ChungChi.NopHoSoChungChi(xmlData, studentID, classStudentID, studyProgramID, loaiLocDuLieu, maLoaiChungChi);
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
                return DA_ChungChi.DSNopHoSoChungChi(fromDate, toDate, classStudentID, studyProgramID, loaiLocDuLieu, maLoaiChungChi);
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
                return DA_ChungChi.LuuHoSoChungChi(xmlData, studentID, loaiLocDuLieu, maLoaiChungChi, updateStaff);
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
                return DA_ChungChi.XoaHoSoChungChi(xmlData, studentID, loaiLocDuLieu, maLoaiChungChi, updateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region DataTable LayNhomMon(string MaNhomMon)
        public static DataTable LayNhomMon()
        {
            try
            {
                return DA_ChungChi.LayNhomMon();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataTable DanhSachDieuKienXet()
        public static DataTable DanhSachDieuKienXet()
        {
            try
            {
                return DA_ChungChi.DanhSachDieuKienXet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public static DataTable TaoChuanXetChoCTDT(string MaCTDT, string Khoa, string MaDieuKien)
        {
            try
            {
                return DA_ChungChi.TaoChuanXetChoCTDT(MaCTDT, Khoa, MaDieuKien);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int  BangChuanTemp(string strXml , string UpdateStaff)
        {
            try
            {
                return DA_ChungChi.BangChuanTemp(strXml,UpdateStaff);
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
                return DA_ChungChi.LayTongTinChi(MaChuanXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
         public static DataTable LayDanhSachSinhVienTotNghiep(string MaCTDT)
        {
            try
            {
                return DA_ChungChi.LayDanhSachSinhVienTotNghiep(MaCTDT);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int LuuSinhVienHinhThuc(string DotXet, string CTDT, string XmlData, string updateStaff)
        {
            try
            {
                return DA_ChungChi.LuuSinhVienHinhThuc(DotXet, CTDT, XmlData, updateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachCapBangChungChi(string maDotCapBang, bool lamMoiDuLieu)
        {
            try
            {
                return DA_ChungChi.DanhSachCapBangChungChi(maDotCapBang, lamMoiDuLieu);
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
                return DA_ChungChi.DanhSachChuongTrinhDaoTao(CourseID, MaDieuKien);
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
                return DA_ChungChi.TimKiemThongTinSinhVienNhanBang(search);
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
                return DA_ChungChi.DanhSachChuongTrinhDaoTao_DotXet(MoDotXet);
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
                return DA_ChungChi.XacNhanSinhVienNhanBang(xmlData, studentID, classStudentID, studyProgramID, MaDotCap, loaiLocDuLieu, maLoaiChungChi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int LuuDSSinhVienNhanBang(string xmlData, string studentID, int loaiLocDuLieu, string maLoaiChungChi, string updateStaff)
        {
            try
            {
                return DA_ChungChi.LuuDSSinhVienNhanBang(xmlData, studentID, loaiLocDuLieu, maLoaiChungChi, updateStaff);
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
                return DA_ChungChi.XoaSinhVienNhanBangCC(xmlData, studentID, maLoaiChungChi, updateStaff);
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
                return DA_ChungChi.PhuongThucSapXepDuLieu(maChucNang);
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
                return DA_ChungChi.CotHienThiTrenLuoi(maChucNang);
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
                DA_ChungChi.LuuPhuongThucCapNhat(maChucNang, xmlData, updateStaff);
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
                return DA_ChungChi.DanhSachSinhVien_DotXet(MaDot);
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
                return DA_ChungChi.DanhHieu();
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
                return DA_ChungChi.MauBangMauChungChi_DanhHieu(maLoaiChungChi, GraduationDegreeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DanhSachSinhVienKhongXet(string strXml, string MaDot, string UpdateStaff)
        {
            try
            {
                return DA_ChungChi.DanhSachSinhVienKhongXet(strXml,MaDot, UpdateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable KetQuaXetTotNghiep_DNU(string MaDotXet)
        {
            try
            {
                return DA_ChungChi.KetQuaXetTotNghiep_DNU(MaDotXet);
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
                return DA_ChungChi.KhoaChuanXet(MaChuanXet, KhoaChuan);
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
                return DA_ChungChi.DSChungNhaHoanThanhKhoaHoc_UEL(XmlData);
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
                return DA_ChungChi.LayTieuChuanTotNghiep(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
