using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using GrdCore.DAL;
using System.IO;
using DevExpress.XtraReports.UI;

namespace GrdUI.InBang
{
    public partial class frm_Grd_MauChungChi : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        DataTable _dtDataSource = new DataTable();
        public int _maMauIn = 0;
        public bool _mauBang = false;
        XtraReportMauBangChungChi report = new XtraReportMauBangChungChi();
        #endregion

        #region Inits
        public frm_Grd_MauChungChi()
        {
            InitializeComponent();
        }

        private void frm_Grd_MauChungChi_Load(object sender, EventArgs e)
        {
            DataTable dtTemp = BL_InBang.TruongDuLieuIn();
            _dtDataSource = new DataTable();
            _dtDataSource.TableName = "TruongDuLieuIn";
            foreach (DataRow dr in dtTemp.Rows)
                _dtDataSource.Columns.Add(dr["TenTruongDuLieu"].ToString(), typeof(string));            

            DataTable _dtTemplateReports = BL_InBang.GetTemplateReports(_maMauIn, _mauBang);

            report = new XtraReportMauBangChungChi();
            report.DataSource = _dtDataSource;

            if (_dtTemplateReports.Rows.Count > 0)
                if (_dtTemplateReports.Rows[0]["MauIn"] != DBNull.Value)
                    report.LoadLayout(new MemoryStream((byte[])_dtTemplateReports.Rows[0]["MauIn"]));

            xrDesignPanel.OpenReport(report);
        }
        #endregion

        #region Events
        private void commandBarItem_newReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            report = new XtraReportMauBangChungChi();

            report.DataSource = _dtDataSource;
            xrDesignPanel.OpenReport(report);
        }

        private void barButtonItem_saveReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                report = (XtraReportMauBangChungChi)xrDesignPanel.Report;

                using (MemoryStream ms = new MemoryStream())
                {
                    report.SaveLayout(ms);

                    BL_InBang.SaveTemplateReports(ms.ToArray(), _maMauIn, _mauBang, User._UserID);
                    XtraMessageBox.Show("Lưu report thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}