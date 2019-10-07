using GrdCore.DAL;
using GrdCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.BLL
{
    public class BL_Reports
    {
        public static DataTable GetTemplateReports(string reportName)
        {
            try
            {
                return DA_Reports.GetTemplateReports(reportName);
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
                return DA_Reports.GetTemplateReportsGrd(reportName);
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
                DA_Reports.SaveTemplateReports(reportName, reportData);
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
                DA_Reports.DeleteTemplateReports(reportName);
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
                return DA_Reports.GetReportTemplateID(reportTemplateID);
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
                return DA_Reports.GetReportTemplateByGroupID(reportTemplateGroupID);
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
                return DA_Reports.GetTemplateReports(reportName, updateStaff, formID, editTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveTemplateReports(string XmlData)
        {
            try
            {
                DA_Reports.SaveTemplateReports(XmlData);
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
                DA_Reports.SaveAsTemplateReports(reportName, reportData, path, updateStaff, FormID, FormName, ReportFormID, ReportFormName, ReportNameUser);
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
                DA_Reports.DeleteTemplateReports(reportName, updateStaff, formID, reportFormID);
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
                return DA_Reports.UpdateReportTemplate(reportTemplateInfo);
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
                return DA_Reports.DeleteReportTemplate(reportTemplateID);
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
                return DA_Reports.GetReportGroups();
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
                return DA_Reports.InsertReports(reportInfo);
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
                return DA_Reports.DeleteReports(reportID);
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
                return DA_Reports.GetReportByReportTemplateID(reportTemplateID);
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
                return DA_Reports.GetReportByReportID(reportID);
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
                return DA_Reports.GetReportTemplates(reportFileName);
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
                return DA_Reports.ThongKeTotNghieptheoDot(strXml);
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
                return DA_Reports.ThongKeXepLoaiTotNghiepTheoNganh(strXml);
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
                return DA_Reports.BienBanKiemVanBang(PeriodOfGrantID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
