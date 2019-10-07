using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GrdCore.BLL;

namespace GrdCore.Entities
{
    public class Reports
    {
        #region "Declare Variables"
        private int _reportID = 0;
        private string _reportDataXml = string.Empty;
        private int _reportTemplateID = 0;
        private string _reportDescription = string.Empty;
        private string _updateDate = string.Empty;
        private string _updateBy = string.Empty;
        private string _updateStaffName = string.Empty;
        #endregion

        #region "Properties"
        public int ReportID
        {
            get { return _reportID; }
            set { _reportID=value;}
        }
        public string ReportDataXml
        {
            get { return _reportDataXml; }
            set { _reportDataXml = value; }
        }
        public int ReportTemplateID
        {
            get { return _reportTemplateID; }
            set { _reportTemplateID = value; }
        }
        public string ReportDescription
        {
            get { return _reportDescription; }
            set { _reportDescription = value; }
        }
        public string UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value; }
        }
        public string UpdateBy
        {
            get { return _updateBy; }
            set { _updateBy = value; }
        }
        public string UpdateStaffName
        {
            get { return _updateStaffName; }
            set { _updateStaffName = value; }
        }
        #endregion

        #region "Constructions"
        #region Reports()
        public Reports()
        {
            _reportID = 0;
            _reportDataXml = string.Empty;
            _reportTemplateID = 0;
            _reportDescription = string.Empty;
            _updateDate = string.Empty;
            _updateBy = string.Empty;
            _updateStaffName = string.Empty;
        }
        #endregion

        #region Reports(int reportID)
        public Reports(int reportID)
        {
            try
            {
                DataTable dt = BL_Reports.GetReportByReportID(reportID);
                if (dt.Rows.Count == 0)
                {
                    _reportID = 0;
                    _reportDataXml = string.Empty;
                    _reportTemplateID = 0;
                    _reportDescription = string.Empty;
                    _updateDate = string.Empty;
                    _updateBy = string.Empty;
                    _updateStaffName = string.Empty;
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    _reportID = (int)dr["ReportID"];
                    _reportDataXml = dr["ReportDataXml"].ToString();
                    _reportTemplateID = (int)dr["ReportTemplateID"];
                    _reportDescription = dr["ReportDescription"].ToString();
                    _updateDate = dr["UpdateDate"].ToString();
                    _updateBy = dr["UpdateBy"].ToString();
                    _updateStaffName = dr["UpdateStaffName"].ToString();
                }
            }
            catch { }
        }
        #endregion
        #endregion

        #region "InsertUpdate Reports"
        #region InsertReports()
        public int InsertReports()
        {
            return BL_Reports.InsertReports(this);
        }
        #endregion

        #region DeleteReports()
        public int DeleteReports()
        {
            return BL_Reports.DeleteReports(_reportID);
        }
        #endregion
        #endregion
    }
}
