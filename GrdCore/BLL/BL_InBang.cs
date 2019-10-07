using GrdCore.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.BLL
{
    public class BL_InBang
    {
        public static DataTable TruongDuLieuIn()
        {
            try
            {
                return DA_InBang.TruongDuLieuIn();
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
                return DA_InBang.LuuTruongDuLieuIn(xmlData, updateStaff);
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
                return DA_InBang.XoaTruongDuLieuIn(xmlData, updateStaff);
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
                return DA_InBang.LoaiChungChi_MaHinhThucCapChungChi(maHinhThucCapChungChi);
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
                return DA_InBang.MauBangMauChungChi(mauBang);
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
                return DA_InBang.LuuMauBangMauChungChi(xmlData, updateStaff, mauBang);
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
                return DA_InBang.XoaMauBangMauChungChi(xmlData, updateStaff, mauBang);
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
                return DA_InBang.GetTemplateReports(maMauIn, mauBang);
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
                return DA_InBang.SaveTemplateReports(reportData, maMauIn, mauBang, updateStaff);
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
                return DA_InBang.DotCap(maLoaiChungChi, namHoc, hocKy, bacDT, loaiHinhDaoTao, excel);
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
                return DA_InBang.LuuDotCap(xmlData, maLoaiChungChi, nguoiCapNhat, namHoc, hocKy, bacDT, loaiHinhDaoTao);
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
                return DA_InBang.XoaDotCap(xmlData, maLoaiChungChi, nguoiCapNhat, namHoc, hocKy, bacDT, loaiHinhDaoTao);
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
                return DA_InBang.DanhSachCapQuyetDinhCongNhan(maLoaiChungChi, maDotXet);
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
                return DA_InBang.CapNhatVaHuySoThuTuQuyetDinhCongNhan(maLoaiChungChi, maDotXet, maChuan, maChuongTrinh, maSinhVien, soThuTu, nguoiCapNhat);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string CapNhatVaHuyQuyetDinhCongNhan(string maLoaiChungChi, string maDotXet, string maChuan, string maChuongTrinh, string maSinhVien, string soQuyetDinh, int loaiQuyetDinh, string nguoiCapNhat, int TinhTrang)
        {
            try
            {
                return DA_InBang.CapNhatVaHuyQuyetDinhCongNhan(maLoaiChungChi, maDotXet, maChuan, maChuongTrinh, maSinhVien, soQuyetDinh, loaiQuyetDinh, nguoiCapNhat, TinhTrang);
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
                return DA_InBang.DanhSachCapQuyetDinhHoanThanh(maLoaiChungChi, maDotXet);
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
                return DA_InBang.CapNhatVaHuySoThuTuQuyetDinhHoanThanh(maLoaiChungChi, maDotXet, maChuan, maChuongTrinh, maSinhVien, soThuTu, nguoiCapNhat);
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
                return DA_InBang.CapNhatVaHuyQuyetDinhHoanThanh(maLoaiChungChi, maDotXet, maChuan, maChuongTrinh, maSinhVien, soQuyetDinh, loaiQuyetDinh, nguoiCapNhat);
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
                return DA_InBang.GetStudyStatus();
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
                return DA_InBang.QuyetDinhCongNhanTotNghiepDotCapBang(BacDT, HeDT, NamHoc, HocKy, loaiXet, maDotCap);
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
                return DA_InBang.DanhSachCapDotXet(BacDT, HeDT, NamHoc, HocKy, loaiXet, maDotXet, maDotCap);
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
                return DA_InBang.LuuQuyetDinhVaDotCapBang(xmlData, maDotCap, nguoiCapNhat);
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
                return DA_InBang.HuyQuyetDinhVaDotCapBang(xmlData, maDotCap, nguoiCapNhat);
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
                return DA_InBang.DanhSachDuocCapBangChungChi(maDotCapBang, lamMoiDuLieu, GraduationDegreeID);
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
                DA_InBang.HuyTinhTrangDaInBang(xmlData, updateStaff);
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
                DA_InBang.LuuThongTinBangChungChi(xmlData, updateStaff);
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
                return DA_InBang.MauBangMauChungChi(maLoaiChungChi, MaMauIn);
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
                return DA_InBang.GetTemplateReports(reportName);
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
                return DA_InBang.ThongTinCapBangChungChi(xmlData, MaMauIn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachCongNhanTotNghiep(string strXml)
        {
            try
            {
                return DA_InBang.DanhSachCongNhanTotNghiep(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GiayChungNhanTotNghiep_CDYT(string MaDotXet, string MaLoaiCC, string XmlData)
        {
            try
            {
                return DA_InBang.GiayChungNhanTotNghiep_CDYT(MaDotXet, MaLoaiCC, XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachDatTotNghiep(string MaDotXet)
        {
            try
            {
                return DA_InBang.DanhSachDatTotNghiep(MaDotXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachKhongDatTotNghiep(string MaDotXet, string CurriculumGroupID)
        {
            try
            {
                return DA_InBang.DanhSachKhongDatTotNghiep(MaDotXet, CurriculumGroupID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable DanhSachKhongDatTotNghiep_NhomAV(string MaDotXet, string CurriculumGroupID)
        {
            try
            {
                return DA_InBang.DanhSachKhongDatTotNghiep_NhomAV(MaDotXet, CurriculumGroupID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable KetQuaXepLoaiBangTN(string MaDotXet)
        {
            try
            {
                return DA_InBang.KetQuaXepLoaiBangTN(MaDotXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GiayChungNhanTotNghiepTamThoi_DNU(string XmlData)
        {
            try
            {
                return DA_InBang.GiayChungNhanTotNghiepTamThoi_DNU(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GiayChungNhanHoanThanh_UEL(string XmlData)
        {
            try
            {
                return DA_InBang.GiayChungNhanHoanThanh_UEL(XmlData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable SoGocCapbangChungChi_DNU(string MaDotCap)
        {
            try
            {
                return DA_InBang.SoGocCapbangChungChi_DNU(MaDotCap);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable DanhSachNoSach_TV(string MaDotXet, int No)
        {
            try
            {
                return DA_InBang.DanhSachNoSach_TV(MaDotXet, No);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable SoGocCapBangTN_UEL(string strXml)
        {
            try
            {
                return DA_InBang.SoGocCapBangTN_UEL(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region DanhSachCongNhanTotNghiep_KhongXetQD_UEL (Công nhận TN / In UEL)
        public static DataTable DanhSachCongNhanTotNghiep_KhongXetQD_UEL(string MaDotXet)
        {
            try
            {
                return DA_InBang.DanhSachCongNhanTotNghiep_KhongXetQD_UEL(MaDotXet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
