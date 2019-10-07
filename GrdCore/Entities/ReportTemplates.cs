using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using GrdCore.BLL;

namespace GrdCore.Entities
{
    public class ReportTemplates
    {
        #region "Declare Variables"
        private int _reportTemplateID = 0;
        private string _reportTemplateName = string.Empty;
        private Byte[] _reportTemplateBinary = null;
        private string _reportTemplateXML = string.Empty;
        private string _updateDate = string.Empty;
        private string _updateBy = string.Empty;
        private string _reportFileName = string.Empty;
        private int _reportTemplateGroupID = 0;
        private int _numberOfPrintedReport;
        private bool _isSaveHistoryReport = false;
        private bool _quocHieu = false;
        private bool _logo = false;
        #endregion

        #region "Properties"
        public int ReportTemplateID
        {
            get { return _reportTemplateID; }
            set { _reportTemplateID = value; }
        }
        public string ReportTemplateName
        {
            get { return _reportTemplateName; }
            set { _reportTemplateName = value; }
        }
        public byte[] ReportTemplateBinary
        {
            get { return _reportTemplateBinary; }
            set { _reportTemplateBinary = value; }
        }
        public string ReportTemplateXMLForSaving
        {
            get { return _reportTemplateXML; }
            set { _reportTemplateXML = value; }
        }
        //public string ReportTemplateXML
        //{
        //    get { return Common.Functions.DecompressString(_reportTemplateXML); }
        //    set { _reportTemplateXML = Common.Functions.CompressString(value); }
        //}
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
        public string ReportFileName
        {
            get { return _reportFileName; }
            set { _reportFileName = value; }
        }
        public int ReportTemplateGroupID
        {
            get { return _reportTemplateGroupID; }
            set { _reportTemplateGroupID = value; }
        }
        public int NumberOfPrintedReport
        {
            get { return _numberOfPrintedReport; }
            set { _numberOfPrintedReport = value; }
        }
        public bool IsSaveHistoryReport
        {
            get { return _isSaveHistoryReport; }
            set { _isSaveHistoryReport = value; }
        }

        public bool QuocHieu
        {
            get { return _quocHieu; }
            set { _quocHieu = value; }
        }

        public bool Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }
        //public TextReader ReportTemplateDefinitionXML
        //{
        //    get
        //    {
        //        TextReader reader = new StringReader(Common.Functions.DecompressString(_reportTemplateXML));
        //        try
        //        {
        //            XmlDocument doc = null;
        //            doc.Load(reader);
        //            reader = new StringReader(doc.OuterXml);
        //        }
        //        catch { }
        //        return reader;
        //    }
        //}
        #endregion

        #region "Constructions"
        #region ReportTemplates()
        public ReportTemplates()
        {
            _reportTemplateID = 0;
            _reportTemplateName = string.Empty;
            _reportTemplateBinary = null;
            _reportTemplateXML = string.Empty;
            _updateDate = string.Empty;
            _updateBy = string.Empty;
            _reportFileName = string.Empty;
            _reportTemplateGroupID = 0;
            //_isSaveHistoryReport = false;
            _quocHieu = false;
            _logo = false;
        }
        #endregion

        #region ReportTemplates(int reportTemplateID)
        public ReportTemplates(int reportTemplateID)
        {
            try
            {
                DataTable dt = BL_Reports.GetReportTemplateID(reportTemplateID);
                if (dt.Rows.Count == 0)
                {
                    _reportTemplateID = 0;
                    _reportTemplateName = string.Empty;
                    _reportTemplateBinary = null;
                    _reportTemplateXML = string.Empty;
                    _updateDate = string.Empty;
                    _updateBy = string.Empty;
                    _reportFileName = string.Empty;
                    _reportTemplateGroupID = 0;
                    //_isSaveHistoryReport = false;
                    _quocHieu = false;
                    _logo = false;
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    _reportTemplateID = reportTemplateID;
                    _reportTemplateName = dr["ReportTemplateName"].ToString();
                    try
                    {
                        _reportTemplateBinary = (byte[])dr["ReportTemplateBinary"];
                    }
                    catch
                    {
                        _reportTemplateBinary = null;
                    }
                    _reportTemplateXML = dr["ReportTemplateXML"].ToString();
                    _updateDate = dr["UpdateDate"].ToString();
                    _updateBy = dr["UpdateStaff"].ToString();
                    _reportFileName = dr["ReportFileName"].ToString();
                    _reportTemplateGroupID = (int)dr["ReportTemplateGroupID"];
                    //_isSaveHistoryReport = (dr["IsSaveHistoryReport"].ToString().ToUpper() == "TRUE");
                    _quocHieu = (dr["QuocHieu"].ToString().ToUpper() == "TRUE");
                    _logo = (dr["Logo"].ToString().ToUpper() == "TRUE");
                }
            }
            catch { }
        }
        #endregion

        #region ReportTemplates(string reportFileName)
        public ReportTemplates(string reportFileName)
        {
            GetLastReportTemplate(reportFileName);
        }
        #endregion
        #endregion

        #region GetLastReportTemplate(string reportFileName)
        public void GetLastReportTemplate(string reportFileName)
        {
            try
            {
                DataTable dt = BL_Reports.GetTemplateReports(reportFileName);
                if (dt.Rows.Count == 0)
                {
                    _reportTemplateID = 0;
                    _reportTemplateName = string.Empty;
                    _reportTemplateBinary = null;
                    _reportTemplateXML = string.Empty;
                    _updateDate = string.Empty;
                    _updateBy = string.Empty;
                    _reportFileName = string.Empty;
                    _reportTemplateGroupID = 0;
                    //_isSaveHistoryReport = false;
                    _quocHieu = false;
                    _logo = false;
                }
                else
                {
                    DataRow dr = dt.Rows[dt.Rows.Count - 1];
                    _reportTemplateID = (int)dr["ReportTemplateID"];
                    _reportTemplateName = dr["ReportTemplateName"].ToString();
                    try
                    {
                        _reportTemplateBinary = (byte[])dr["ReportTemplateBinary"];
                    }
                    catch
                    {
                        _reportTemplateBinary = null;
                    }
                    _reportTemplateXML = dr["ReportTemplateXML"].ToString();
                    _updateDate = dr["UpdateDate"].ToString();
                    _updateBy = dr["UpdateBy"].ToString();
                    _reportFileName = dr["ReportFileName"].ToString();
                    _reportTemplateGroupID = (int)dr["ReportTemplateGroupID"];
                    //_isSaveHistoryReport = (dr["IsSaveHistoryReport"].ToString().ToUpper() == "TRUE");
                    _quocHieu = (dr["QuocHieu"].ToString().ToUpper() == "TRUE");
                    _logo = (dr["Logo"].ToString().ToUpper() == "TRUE");
                }
            }
            catch { }
        }
        #endregion

        #region PrintReport(Microsoft.Reporting.WinForms.ReportViewer rv, string staffID)
        //public void PrintReport(Microsoft.Reporting.WinForms.ReportViewer rv, string staffID)
        //{
        //    try
        //    {
        //        if (!_isSaveHistoryReport) return;
        //        //Report Template
        //        string strXml = "<Root><ReportContaint Name = \"ReportTemplate\" Value = \"" + Common.Functions.RefreshXmlString(_reportTemplateXML) + "\"/>";

        //        //Parameters
        //        string strParameters = "<Root><Parameters>";
        //        Microsoft.Reporting.WinForms.ReportParameterInfoCollection pr = rv.LocalReport.GetParameters();
        //        if (pr.Count != 0)
        //        {
        //            foreach (Microsoft.Reporting.WinForms.ReportParameterInfo pri in pr)
        //            {
        //                strParameters += "<Parameter Name = \"" + pri.Name
        //                    + "\" Value = \"" + pri.Values[0].ToString() + "\"/>";
        //            }
        //        }
        //        strParameters += "</Parameters></Root>";
        //        strXml += "<ReportContaint Name = \"Parameters\" Value = \"" + Common.Functions.CompressString(strParameters) + "\"/>";

        //        //DataSet
        //        DataSet ds = new DataSet();
        //        Microsoft.Reporting.WinForms.ReportDataSourceCollection rdsc = rv.LocalReport.DataSources;
        //        if (rdsc.Count != 0)
        //        {
        //            foreach (Microsoft.Reporting.WinForms.ReportDataSource rds in rdsc)
        //            {
        //                DataTable dt = (DataTable)rds.Value;
        //                dt.TableName = rds.Name;
        //                ds.Tables.Add(dt);
        //            }
        //        }
        //        strXml += "<ReportContaint Name = \"DataTables\" Value = \"" + Common.Functions.CompressString(ds.GetXml()) + "\"/>";
        //        strXml += "</Root>";

        //        Reports rpt = new Reports();
        //        rpt.ReportDataXml = strXml;
        //        rpt.ReportDescription = string.Empty;
        //        rpt.ReportTemplateID = _reportTemplateID;
        //        rpt.UpdateBy = staffID;
        //        rpt.InsertReports();
        //    }
        //    catch { }
        //}
        #endregion

        #region "InsertDeleteUpdate ReportTemplates"
        #region InsertReportTemplates()
        //public int InsertReportTemplates()
        //{
        //    return BL_Reports.InsertReportTemplate(this);
        //}
        #endregion

        // #region DeleteReportTemplates()
        // //public int DeleteReportTemplates()
        // //{
        // //    return BL_Reports.DeleteReportTemplate(_reportTemplateID);
        // //}
        // #endregion

        #region UpdateReportTemplates()
        public int UpdateReportTemplates()
        {
            return BL_Reports.UpdateReportTemplate(this);
        }
        #endregion
        #endregion
    }
}
