using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UserDesigner;
using XtraReportsDemos;
using System.IO;
using DevExpress.XtraEditors;
using System.Xml;
using DevExpress.XtraReports.Parameters;
using GrdCore.BLL;
using GrdReports.Reports;
using GrdReports.Reports.UEL;

namespace GrdReports
{
    public partial class frmGrdReports : Form
    {
        #region Variables
        public static XtraReport XReport;
        public DataTable _dtDataSource = new DataTable();
        public DataSet _dsDataSource = new DataSet();
        public string _reportName = string.Empty;

        public string _reportTemplateName = string.Empty;
        public string _reportTemplateBinary = string.Empty;
        public string _reportTemplateXML = string.Empty;
        public string _reportFileName = string.Empty;
        public int _reportTemplateGroupID = 0;
        public string _updateStaff = string.Empty;
        public string _formId = string.Empty;
        public string _formName = string.Empty;
        public string _reportFormId = string.Empty;
        public string _reportFormName = string.Empty;
        public string _reportNameUser = string.Empty;
        public string _path = string.Empty;
        public string _editTime = string.Empty;
        #endregion

        #region Inits
        #region public frmScoreReports()
        public frmGrdReports()
        {
            InitializeComponent();
        }
        #endregion
        #endregion

        #region Functions      
        #region GetReportPath(XtraReport fReport, string ext)
        private static string GetReportPath(XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string repName = fReport.Name;
            if (repName.Length == 0)
                repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName, String.Format("{0}.{1}", repName, ext));
        }
        #endregion

        #region ShowDesignerForm(Form designForm, Form parentForm)
        private static void ShowDesignerForm(Form designForm, Form parentForm)
        {
            try
            {
                designForm.MinimumSize = parentForm.MinimumSize;
                if (parentForm.WindowState == FormWindowState.Normal)
                    designForm.Bounds = parentForm.Bounds;
                designForm.WindowState = parentForm.WindowState;
                parentForm.Visible = false;
                designForm.ShowDialog();
                parentForm.Visible = true;
            }
            catch { }
        }
        #endregion

        #region private void OpenLayout(XtraReport report)
        private void OpenLayout(XtraReport report)
        {
            Type t = report.GetType();
            DataTable _dtTemplateReports = BL_Reports.GetTemplateReports(t.Name);

            if (_dtTemplateReports.Rows.Count > 0)
                if (_dtTemplateReports.Rows[0]["Data"] != DBNull.Value)//ReportTemplateBinary
                {
                    XReport.DataSource = _dtDataSource;
                    XReport.LoadLayout(new MemoryStream((byte[])_dtTemplateReports.Rows[0]["Data"]));
                }

            foreach (Parameter p in XReport.Parameters)
                p.Visible = false;
        }
        #endregion

        #region InitReport
        private void InitReport(string ReportTemplateName, string ReportTemplateXML, string ReportFileName, string UpdateStaff
           , string EditTime, int ReportTemplateGroupID, string FormID, string FormName, string ReportFormId, string ReportFormName, string ReportNameUser)//, string Path, string ReportNameUser)
        {
            _reportTemplateName = ReportTemplateName;
            _reportTemplateXML = ReportTemplateXML;
            _reportFileName = ReportFileName;
            _updateStaff = UpdateStaff;
            _editTime = EditTime;
            _reportTemplateGroupID = ReportTemplateGroupID;
            _formId = FormID;
            _formName = FormName;
            _reportFormId = ReportFormId;
            _reportFormName = ReportFormName;
            //_path = Path;
            _reportNameUser = ReportNameUser;
        }
        #endregion

        #region GetReportTemplateGroupID
        private int GetReportTemplateGroupID(string Text)
        {
            int _rtgID = 0;
            if (Text.Contains("BẢNG ĐIỂM"))
                _rtgID = 0;
            else if (Text.Contains("GIẤY CHỨNG NHẬN"))
                _rtgID = 1;
            else if (Text.Contains("THỐNG KÊ"))
                _rtgID = 3;
            else
                _rtgID = 2;
            return _rtgID;
        }
        #endregion

        #region OpenLayout
        
        #endregion

        #region SaveTemplateReports
        private void SaveTemplateReports(XtraReport report)
        {
            try
            {
                string strXml = string.Empty;
                using (MemoryStream _loadFirstLayout = new MemoryStream())
                {
                    report.SaveLayoutToXml(_loadFirstLayout);
                    strXml += "<ReportInfo ReportTemplateName = \"" + _reportTemplateName
                        + "\" ReportTemplateBinary = \"" + DBNull.Value
                        + "\" ReportTemplateXML = \"" + GetReportXML(report)
                        + "\" ReportFileName = \"" + _reportFileName
                        + "\" EditTimes = \"" + _editTime
                        + "\" ReportTemplateGroupID = \"" + _reportTemplateGroupID
                        + "\" FormID = \"" + _formId
                        + "\" FormName = \"" + _formName
                        + "\" ReportFormID = \"" + _reportFormId
                        + "\" ReportFormName = \"" + _reportFormName
                        + "\" ReportPath = \"" + string.Empty
                        + "\" UpdateStaff = \"" + _updateStaff
                        + "\" ReportNameUser  = \"" + _reportNameUser.ToString() + "\"/>";
                    strXml = "<Root>" + strXml + "</Root>";
                    BL_Reports.SaveTemplateReports(strXml);
               
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    report.SaveLayout(ms, true);
                    BL_Reports.SaveTemplateReports(report.GetType().Name, ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private static string GetReportXML(XtraReport report)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                report.SaveLayoutToXml(ms);
                ms.Position = 0;
                StreamReader reader = new StreamReader(ms);
                byte[] output = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                string send = Convert.ToBase64String(output);
                return send;
            }
        }
        #endregion

        #region Events
        #region private void btn_Designs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        private void btn_Designs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {


                Cursor.Current = Cursors.WaitCursor;
                string saveFileName = GetReportPath(XReport, "repx");
                XReport.PrintingSystem.ExecCommand(PrintingSystemCommand.StopPageBuilding);
                XReport.SaveLayout(saveFileName);

                using (XtraReport newReport = XtraReport.FromFile(saveFileName, true))
                {
                    XRDesignFormExBase designForm = new CustomDesignForm();
                    designForm.OpenReport(newReport);
                    designForm.FileName = saveFileName;
                    ShowDesignerForm(designForm, FindForm());
                    if (designForm.FileName != saveFileName && File.Exists(designForm.FileName))
                        File.Copy(designForm.FileName, saveFileName, true);
                    designForm.OpenReport((XtraReport)null);
                    designForm.Dispose();
                }
                if (File.Exists(saveFileName))
                {
                    XReport.LoadLayout(saveFileName);
                    File.Delete(saveFileName);
                    XReport.CreateDocument(true);
                }
                File.Delete(saveFileName);
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Bạn có muốn lưu thay thế mẫu báo cáo mới cho mẫu báo cáo cũ không ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XReport.SaveLayout(ms);

                        BL_Reports.SaveTemplateReports(XReport.Name, ms.ToArray());
                        XtraMessageBox.Show("Lưu report thành công...", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Cách design cũ
            //DialogResult dtlResult = MessageBox.Show("Bạn có muốn xóa các mẫu báo cáo mới và xài mẫu báo cáo cũ mặc định không ???", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //if (dtlResult == DialogResult.OK)
            //{
            //    if (File.Exists(GetReportPath(XReport, "repx")))
            //        File.Delete(GetReportPath(XReport, "repx"));
            //} 
            #endregion

            try
            {
                if (XtraMessageBox.Show("Bạn có muốn xóa các mẫu báo cáo mới và xài mẫu báo cáo mặc định không ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BL_Reports.DeleteTemplateReports(XReport.Name);

                    XtraMessageBox.Show("Trả về mẫu báo cáo mặc định thành công.", "Messages", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion

        #region Mẫu Report in cho các trường
        #region _load_XtraReport_BangDiemTotNghiep_HCMUP
        public void _load_XtraReport_BangDiemTotNghiep_HCMUP(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemTotNghiep_HCMUP _report = new XtraReport_BangDiemTotNghiep_HCMUP();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_DanhSachCongNhanTotNghiep
        public void _load_XtraReport_DanhSachCongNhanTotNghiep(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH CÔNG NHẬN TỐT NGHIỆP";
            XtraReport_Yersin_DanhSachCongNhanTN _report = new XtraReport_Yersin_DanhSachCongNhanTN();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_DanhSachCongNhanTotNghiep_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH CÔNG NHẬN TỐT NGHIỆP";
            XtraReport_DanhSachCongNhanTN_UEL _report = new XtraReport_DanhSachCongNhanTN_UEL();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_DanhSachCongNhanTotNghiep
        public void _load_XtraReport_GiayChungNhanTotNghiep_UEL(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy
            , string _NguoiLap, byte[] _CollegeLogo, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "GIẤY CHỨNG NHẬN TỐT NGHIỆP";
            XtraReport_GiayChungNhanTotNghiep_UEL _report = new XtraReport_GiayChungNhanTotNghiep_UEL();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _NguoiLap, _CollegeLogo, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVDatTN
        public void _load_XtraReport_Yersin_DanhSachSVDatTN(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH ĐẠT TỐT NGHIỆP";
            XtraReport_Yersin_DanhSachSVDatTN _report = new XtraReport_Yersin_DanhSachSVDatTN();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVDatTN
        public void _load_XtraReport_DanhSachSVDatTN_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH ĐẠT TỐT NGHIỆP";
            XtraReport_DanhSachSVDatTN_DNU _report = new XtraReport_DanhSachSVDatTN_DNU();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVKhongDatTN
        public void _load_XtraReport_Yersin_DanhSachSVKhongDatTN(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH KHÔNG ĐẠT TỐT NGHIỆP";
            XtraReport_Yersin_DanhSachSVKhongDatTN _report = new XtraReport_Yersin_DanhSachSVKhongDatTN();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVKhongDatTN
        public void _load_XtraReport_DanhSachSVKhongDatTN_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName, string _AdministrativeUnit)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH KHÔNG ĐẠT TỐT NGHIỆP";
            XtraReport_DanhSachSVKhongDatTN_DNU _report = new XtraReport_DanhSachSVKhongDatTN_DNU();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_DanhSachSVKhongDatTN_NhomAV(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName, string _AdministrativeUnit)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH KHÔNG ĐẠT TỐT NGHIỆP";
            XtraReport_Yersin_DanhSachSVKhongDatTN_NhomAV _report = new XtraReport_Yersin_DanhSachSVKhongDatTN_NhomAV();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion


        #region _load_XtraReport_KetQuaXepLoaiBangTN
        public void _load_XtraReport_KetQuaXepLoaiBangTN(DataTable tbPrint, string _NgayIn, string _nguoiIn, bool groupBacHe, bool groupKhoa, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "THỐNG KÊ XẾP LOẠI TỐT NGHIỆP";
            XtraReport_Yersin_KetQuaXepLoaiBangTN _report = new XtraReport_Yersin_KetQuaXepLoaiBangTN();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _nguoiIn, groupBacHe, groupKhoa, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_KetQuaXepLoaiBangTN
        public void _load_XtraReport_KetQuaXepLoaiBangTN_NienChe(DataTable tbPrint, string _NgayIn, string _nguoiIn, bool groupBacHe, bool groupKhoa, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "THỐNG KÊ XẾP LOẠI TỐT NGHIỆP";
            XtraReport_Yersin_KetQuaXepLoaiBangTN_NienChe _report = new XtraReport_Yersin_KetQuaXepLoaiBangTN_NienChe();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _nguoiIn, groupBacHe, groupKhoa, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiepDayDu_HCMUP
        public void _load_XtraReport_BangDiemTotNghiepDayDu_HCMUP(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemTotNghiepDayDu_HCMUP _report = new XtraReport_BangDiemTotNghiepDayDu_HCMUP();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiepDayDu_HCMUP
        public void _load_XtraReport_BangDiemTotNghiepDayDu_Yersin(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemTotNghiepDayDu_Yersin _report = new XtraReport_BangDiemTotNghiepDayDu_Yersin();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _NguoiLap, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiepDayDu_HCMUP
        public void _load_XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe _report = new XtraReport_BangDiemTotNghiepDayDu_Yersin_NienChe();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _NguoiLap, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _XtraReport_GiayChungNhanDiem_HCMUP
        public void _XtraReport_GiayChungNhanDiem_HCMUP (DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN GIẤY CHỨNG NHẬN ĐIỂM";
            XtraReport_GiayChungNhanDiem_HCMUP _report = new XtraReport_GiayChungNhanDiem_HCMUP();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_UTE_SoBang
        public void _load_XtraReport_UTE_SoBang(DataTable _dtData)
        {
            _dtDataSource = _dtData;
            this.Text = "UIS - In sổ bằng";
            XtraReport_UTE_SoBang _xtraReport_UTE_SoBang = new XtraReport_UTE_SoBang();
            XReport = _xtraReport_UTE_SoBang;
            if (File.Exists(GetReportPath(XReport, "repx")))
                XReport.LoadLayout(GetReportPath(XReport, "repx"));
            _xtraReport_UTE_SoBang.InitReports(_dtData);
            printControl.PrintingSystem = _xtraReport_UTE_SoBang.PrintingSystem;
            _xtraReport_UTE_SoBang.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVDatTN
        public void _load_XtraReport_Yersin_DanhSachSVDatTN_Khoa(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH ĐẠT TỐT NGHIỆP";
            XtraReport_DanhSachSVDatTN_TheoKhoa _report = new XtraReport_DanhSachSVDatTN_TheoKhoa();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVKhongDatTN
        public void _load_XtraReport_Yersin_DanhSachSVKhongDatTN_Khoa(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH KHÔNG ĐẠT TỐT NGHIỆP";
            XtraReport_DanhSachSVKhongDatTN_TheoKhoa _report = new XtraReport_DanhSachSVKhongDatTN_TheoKhoa();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_Yersin_DanhSachSVKhongDatTN_Nganh(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH KHÔNG ĐẠT TỐT NGHIỆP";
            XtraReport_DanhSachSVKhongDatTN_TheoNganh _report = new XtraReport_DanhSachSVKhongDatTN_TheoNganh();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiep_DNU
        public void _load_XtraReport_BangDiemTotNghiep_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemTNCaNhan_2Cot_DHDN _report = new XtraReport_BangDiemTNCaNhan_2Cot_DHDN();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiep_DNU
        public void _load_XtraReport_KetQuaXetTotNghiep_DNU(DataTable tbPrint, string _NgayIn, string _NgayThi, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "KẾT QUẢ XÉT TỐT NGHIỆP";
            XtraReport_KetQuaXetTN_DNU _report = new XtraReport_KetQuaXetTN_DNU();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _NgayThi, _CapBac, _NguoiKy, _NguoiLap, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_ChungNhanHocThanhKhoaHoc
        public void _load_XtraReport_ChungNhanHocThanhKhoaHoc_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _NguoiLap, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "CHỨNG NHẬN HOÀN THÀNH KHÓA HỌC";
            XtraReport_DNU_ChungNhanHoanThanhKhoaHoc _report = new XtraReport_DNU_ChungNhanHoanThanhKhoaHoc();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _NguoiLap, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_ChungNhanHocThanhKhoaHoc_UEL(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy
             , byte[] _CollegeLogo, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "CHỨNG NHẬN HOÀN THÀNH KHÓA HỌC";
            XtraReport_GiayChungNhanHoanThanhKhoaHoc_UEL _report = new XtraReport_GiayChungNhanHoanThanhKhoaHoc_UEL();
            XReport = _report;

            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _CollegeLogo, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        public void _load_XtraReport_SoGocCapBang_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "SỔ GỐC CẤP BẰNG TỐT NGHIỆP";
            XtraReport_SoGocCapBangTN_DNU _report = new XtraReport_SoGocCapBangTN_DNU();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        //ERR 500
        public void _load_XtraReport_ThonkeTotNghiep_TheoDot(DataSet tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dsDataSource = tbPrint;
            this.Text = "THỐNG KÊ SỐ LƯỢNG TỐT NGHIỆP THEO ĐỢT";
            XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot _report = new XtraReport_ThongKeSLTotNghiepTheoQuyetDinhDot();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        public void _load_XtraReport_BienBanKiemKeVanBang(DataSet tbPrint,DataTable _dtSluong, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName, string _Nguoilapbang)
        {
            _dsDataSource = tbPrint;
            this.Text = "BIÊN BẢN KIỂM KÊ VĂN BẰNG";
            XtraReport_BienBanKiemKeTinhSuDungVanBang _report = new XtraReport_BienBanKiemKeTinhSuDungVanBang();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _dtSluong, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName, _Nguoilapbang);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        public void _load_XtraReport_ThonkeTotNghiep_TheoNganh(DataSet tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dsDataSource = tbPrint;
            this.Text = "THỐNG KÊ SỐ LƯỢNG TỐT NGHIỆP THEO ĐỢT NGÀNH";
            XtraReport_ThongKeXepLoaiTotNghiepTheoQuyetDinhDot _report = new XtraReport_ThongKeXepLoaiTotNghiepTheoQuyetDinhDot();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        public void _load_XtraReport_SoGocCapChungChi_DNU(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "SỔ GỐC CẤP CHỨNG CHỈ";
            XtraReport_SoGocCapChungChi_DNU _report = new XtraReport_SoGocCapChungChi_DNU();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_SoGocCapBangTN_UEL(DataTable tbPrint, string NgayIn, string CapBac, string NguoiKy, string AdministrativeUnit, string CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "SỔ GỐC CẤP BẰNG";
            XtraReport_SoGocCapBangTN_UEL _report = new XtraReport_SoGocCapBangTN_UEL();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, NgayIn, CapBac, NguoiKy, AdministrativeUnit, CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion
        DataTable tbPrintTV, tbTiengAnh;
        string _NgayIn, _ngayKyTenTA, _CapBac, _NguoiKy, _CapBacTA;
        #region _load_XtraReport_BangDiemTNCQ_A3_UEL
        public void _load_XtraReport_BangDiemTNCQ_A3_UEL(DataTable tbPrint, DataTable tbTiengAnh, string _NgayIn, string _CapBac, string _NguoiKy, string _ngayKyTenTA
                    , string _nguoiKyTenTA, string _CapBacTA, string _NguoiLap, string GridID, string FormID, string ReportFormId, string ReportFormName, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM CÁ NHÂN";
            XtraReport_BangDiemTotNghiepAnhVietA3 _report = new XtraReport_BangDiemTotNghiepAnhVietA3();
            XReport = _report;
            this.tbTiengAnh = tbTiengAnh;
            this._NgayIn = _NgayIn;
            this._ngayKyTenTA = _ngayKyTenTA;
            this._CapBac = _CapBac;
            this._NguoiKy = _NguoiKy;
            this._CapBacTA = _CapBacTA;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, tbTiengAnh, _NgayIn, _ngayKyTenTA, _CapBac, _NguoiKy
                    , _CapBacTA);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }

        public void _load_XtraReport_BangDiemTNKCQ_A3_UEL(DataTable tbPrint, DataTable tbTiengAnh, string _NgayIn, string _CapBac, string _NguoiKy, string _ngayKyTenTA
                    , string _nguoiKyTenTA, string _CapBacTA, string _NguoiLap, string GridID, string FormID, string ReportFormId, string ReportFormName, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM CÁ NHÂN";
            XtraReport_BangDiemTotNghiepAnhVietA3_KCQ _report = new XtraReport_BangDiemTotNghiepAnhVietA3_KCQ();
            XReport = _report;
            this.tbTiengAnh = tbTiengAnh;
            this._NgayIn = _NgayIn;
            this._ngayKyTenTA = _ngayKyTenTA;
            this._CapBac = _CapBac;
            this._NguoiKy = _NguoiKy;
            this._CapBacTA = _CapBacTA;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, tbTiengAnh, _NgayIn, _ngayKyTenTA, _CapBac, _NguoiKy
                    , _CapBacTA);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVNoPhi
        public void _load_XtraReport_Yersin_DanhSachSVNoPhi(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName, string _NguoiLap)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH SINH VIÊN NỢ PHÍ";
            XtraReport_Yersin_DanhSachSVNoPhi _report = new XtraReport_Yersin_DanhSachSVNoPhi();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _CollegeName, _NguoiLap);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_Yersin_DanhSachSVNoSach
        public void _load_XtraReport_Yersin_DanhSachSVNoSach(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _CollegeName, string _NguoiLap)
        {
            _dtDataSource = tbPrint;
            this.Text = "DANH SÁCH SINH VIÊN NỢ SÁCH";
            XtraReport_Yersin_DanhSachSVNoSach _report = new XtraReport_Yersin_DanhSachSVNoSach();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _CollegeName, _NguoiLap);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_TieuChuanTotNghiep_Yersin
        public void _load_XtraReport_TieuChuanTotNghiep_Yersin(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "TIÊU CHUẨN TỐT NGHIỆP";
            XtraReport_Yersin_TieuChuanTotNghiep _report = new XtraReport_Yersin_TieuChuanTotNghiep();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_BangDiemTotNghiep_DNU
        public void _load_XtraReport_BangDiemXetTotNghiep_UEL(DataTable tbPrint, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_BangDiemXetTotNghiep_UEL _report = new XtraReport_BangDiemXetTotNghiep_UEL();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion

        #region _load_XtraReport_DanhSachCongNhanTN_UEL_KhongXetDaCapQD
        public void _load_XtraReport_DanhSachCongNhanTN_UEL_KhongXetDaCapQD(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            _dtDataSource = tbPrint;
            this.Text = "IN BẢNG ĐIỂM TỐT NGHIỆP";
            XtraReport_DanhSachCongNhanTN_UEL_KhongXetDaCapQD _report = new XtraReport_DanhSachCongNhanTN_UEL_KhongXetDaCapQD();
            XReport = _report;
            OpenLayout(_report);
            _report.Init_Report(tbPrint, _NgayIn, _CapBac, _NguoiKy, _AdministrativeUnit, _CollegeName);
            printControl.PrintingSystem = _report.PrintingSystem;
            _report.CreateDocument();
        }
        #endregion
    }
}