using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GrdCore.DAL;

namespace GrdCore.BLL
{
    public class BL_DoiTuongPhanQuyen
    {
        public static DataTable LuoiHienThi()
        {
            try
            {
                return DA_DoiTuongPhanQuyen.LuoiHienThi();
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
                return DA_DoiTuongPhanQuyen.LuuLuoiHienThi(strXml, updateStaff);
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
                return DA_DoiTuongPhanQuyen.XoaLuoiHienThi(strXml, updateStaff);
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
                return DA_DoiTuongPhanQuyen.CotLuoiHienThi(gridID);
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
                return DA_DoiTuongPhanQuyen.LuuCotLuoiHienThi(gridID, strXml, updateStaff);
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
                return DA_DoiTuongPhanQuyen.XoaCotLuoiHienThi(gridID, strXml, updateStaff);
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
                return DA_DoiTuongPhanQuyen.ThongTinDongDauKyTen(staffID);
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
                return DA_DoiTuongPhanQuyen.LuuThongTinDongDauKyTen(strXml, updateStaff);
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
                return DA_DoiTuongPhanQuyen.XoaThongTinDongDauKyTen(strXml, updateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
