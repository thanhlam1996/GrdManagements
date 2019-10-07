using GrdCore.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.BLL
{
   public class BL_PhoiBang
    { 
        /// <summary>
        /// Danh mục phôi bằng
        /// </summary>
        /// <returns></returns>
       public static DataTable Danhmucphoibang(int periodOfGrantID)
        {
            try
            {
                return DA_PhoiBang.Danhmucphoibang(periodOfGrantID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Danh mục loại phôi bằng
        /// </summary>
        /// <returns></returns>
        public static DataTable DanhMucLoaiPhoiBang()
        {
            try
            {
                return DA_PhoiBang.DanhMucLoaiPhoiBang();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get All bang psc_Grd_DiplomasType_Ology - Cấu hình loại phôi ngành
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_CauHinhLoaiPhoi_Nganh()
        {
            try
            {
                return DA_PhoiBang.Get_CauHinhLoaiPhoi_Nganh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Lưu cấu hình loại phôi với Ngành
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Update_CauHinhLoaiPhoi_Nganh(string strXml,string UpdateStaff)
        {
            try
            {
                return DA_PhoiBang.Update_CauHinhLoaiPhoi_Nganh(strXml, UpdateStaff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Them moi danh muc phoi
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="UpdateStaff"></param>
        /// <returns></returns>
        public static string Insert_DanhMucPhoi(string strXml, string UpdateStaff)
        {
            try
            {
                return DA_PhoiBang.Insert_DanhMucPhoi(strXml, UpdateStaff);
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
                return DA_PhoiBang.Delete_DanhMucPhoi(strXml);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật, thêm mới doanh mục loại phôi
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Insert_DanhMucLoaiPhoi(string strXml)
        {
            try
            {
                return DA_PhoiBang.Insert_DanhMucLoaiPhoi(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Xóa danh mục loại phôi
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string Delete_DanhMucLoaiPhoi(string strXml)
        {
            try
            {
                return DA_PhoiBang.Delete_DanhMucLoaiPhoi(strXml);
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
                return DA_PhoiBang.Check_DanhMucPhoi(strXml);
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
                return DA_PhoiBang.Check_DanhMucLoaiPhoi(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Lấy danh sách phôi chi tiết theo mã lô phôi
        /// </summary>
        /// <param name="_ShipmentsID"></param>
        /// <returns></returns>
        public static DataTable DanhSachPhoiTheoLo(int _PeriodOfGrantID, string _ShipmentsID)
        {
            try
            {
                return DA_PhoiBang.DanhSachPhoiTheoLo(_PeriodOfGrantID, _ShipmentsID);
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
                return DA_PhoiBang.DanhSachPhoiJoinLoaiPhoi(_PeriodOfGrantID);
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
        public static string Update_ChiTietPhoi(string _Reason, bool _isUpdate, int _AutoID, string _SerialNumberID, string UpdateStaff)
        {
            try
            {
                return DA_PhoiBang.Update_ChiTietPhoi(_Reason, _isUpdate, _AutoID, _SerialNumberID, UpdateStaff);
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
                return DA_PhoiBang.Update_SuDungLaiPhoiDaHuy(_AutoID, _SerialNumberID, UpdateStaff);
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
                return DA_PhoiBang.DanhMucDotCapPhoi();
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
                return DA_PhoiBang.Insert_DanhMucDoiCapPhoi(strXml, UpdateStaff);
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
                return DA_PhoiBang.Check_DanhMucDotCapPhoi(strXml);
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
                return DA_PhoiBang.Delete_DotCapPhoi(strXml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
