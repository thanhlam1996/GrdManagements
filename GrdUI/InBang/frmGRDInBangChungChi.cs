using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraReports.UI;
using GrdCore.BLL;

namespace GrdUI.InBang
{
    public partial class frmGRDInBangChungChi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public DataTable _dtDataSource = new DataTable();
        public string _reportName = string.Empty;
        public bool _Print = false;
        #endregion

        #region Inits
        #region public frmGRDInBangTotNghiep()
        public frmGRDInBangChungChi()
        {
            InitializeComponent();
        }
        #endregion

        #region private void frmGRDInBangTotNghiep_Load(object sender, EventArgs e)
        private void frmGRDInBangTotNghiep_Load(object sender, EventArgs e)
        {
            XtraReport reportPrint = new XtraReport();
            DataTable _dtTemplateReports = BL_InBang.GetTemplateReports(_reportName);

            if (_dtTemplateReports.Rows.Count > 0)
                if (_dtTemplateReports.Rows[0]["MauIn"] != DBNull.Value)
                {
                    reportPrint.DataSource = _dtDataSource;
                    reportPrint.LoadLayout(new MemoryStream((byte[])_dtTemplateReports.Rows[0]["MauIn"]));

                    printControl.PrintingSystem = reportPrint.PrintingSystem;
                    if (_Print == false)
                    {
                        reportPrint.CreateDocument();
                        reportPrint.PrintingSystem.ShowMarginsWarning = false;
                    }
                    else
                    {
                        reportPrint.ShowPrintStatusDialog = false;
                        reportPrint.ShowPrintMarginsWarning = false;
                        ReportPrintTool  rpt = new DevExpress.XtraReports.UI.ReportPrintTool(reportPrint);
                        rpt.Print();
                        rpt.Dispose();
                        this.Close();
                    }
                }
        }
        #endregion 
        #endregion
    }
}