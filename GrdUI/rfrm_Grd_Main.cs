using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Globalization;
using System.Xml;
using DevExpress.XtraEditors;
using GrdCore.BLL;
using GrdUI;

namespace GrdUI
{
    public partial class rfrm_Grd_Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Variables
        public static rfrm_Grd_Main me;

        public static int _pageCount;
        public static string _userName = string.Empty, _passwordUser = string.Empty;

        public DataTable dtGroup = new DataTable();
        private bool _firstGraduateLevels = true, _firstStudyTypes = true;


        // SoThuTuTotnghiepMoi
        // 201: HCMUP
        // 202: USSH-SDH
        // 203: Yersin
        #endregion

        #region Inits
        public rfrm_Grd_Main()
        {
            InitializeComponent();

        }

        private void rfrm_Grd_Main_Load(object sender, EventArgs e)
        {
            try
            {
                barButtonItemLuuMacDinh.Visibility = BarItemVisibility.Never;
                barButtonItemDoiMatKhau.Visibility = BarItemVisibility.Never;
                barButtonItemDangNhapLai.Visibility = BarItemVisibility.Never;
                barButtonItemThoatUngDung.Visibility = BarItemVisibility.Never;

                ribbonPageHeThong.Visible = false;
                ribbonPageChungChi.Visible = false;
                ribbonPageBangChungChi.Visible = false;
                ribbonPage_Phoibang.Visible = false;

                grpCommonInfo.Visible = false;

                barButtonItemHocKyHienTai.Visibility = BarItemVisibility.Never;

                txtTenDangNhap.Focus();
                InitCulture();

            }
            catch { }

            if (User._UserID.Length > 0)
            {
                Login(true);
            }
        }
        #endregion

        #region Functions
        private void InitCulture()
        {
            CultureInfo cul = new CultureInfo(("vi-VN"));
            cul.NumberFormat.CurrencyDecimalSeparator = ".";
            cul.NumberFormat.CurrencyGroupSeparator = ",";
            cul.NumberFormat.NumberDecimalSeparator = ".";
            cul.NumberFormat.NumberGroupSeparator = ",";
            cul.NumberFormat.PercentDecimalSeparator = ".";
            cul.NumberFormat.PercentGroupSeparator = ",";
            cul.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Application.CurrentCulture = cul;
        }

        public void Login()
        {
            Login(false);
        }

        public void Login(bool auto)
        {
            try
            {
                try
                {
                    LoadConfig();
                }
                catch { }

                if (auto)
                {
                    txtTenDangNhap.Text = User._UserID;
                    txtMatKhau.Text = User._UserPass;
                }

                if (txtTenDangNhap.Text == String.Empty)
                {
                    XtraMessageBox.Show("Chưa nhập mã người dùng", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadLock();
            }
            catch
            {
                txtTenDangNhap.Text = string.Empty;
                txtMatKhau.Text = string.Empty;
            }
        }

        public void LoadConfig()
        {
            try
            {
                //Lay thong tin cau hinh may
                string pathApp = Application.StartupPath;
                if (System.IO.File.Exists(pathApp + "\\Grd_Config.xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    XmlNode node;
                    doc.Load(pathApp + "\\Grd_Config.xml");
                    node = doc.SelectSingleNode("appSettings");

                    GrdCore.Provider._connectionString = CommonFunctions.DecodeString(node.Attributes["ConnectionString"].Value.ToString(), true);
                    GrdCore.Provider._sndConnectionString = CommonFunctions.DecodeString(node.Attributes["ConnectionString2"].Value.ToString(), true);
                    GrdCore.Provider._provider = node.Attributes["ProviderName"].Value.ToString();

                    GrdCore.SqlHelper help = new GrdCore.SqlHelper(GrdCore.Provider._connectionString);
                    if (!help.CheckConnection())
                    {
                        XtraMessageBox.Show("Không thể kết nối với server (Chuỗi kết nối 1)", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        frm_Grd_ConnectDatabase frm = new frm_Grd_ConnectDatabase();
                        frm.ShowDialog();
                    }

                    help = new GrdCore.SqlHelper(GrdCore.Provider._sndConnectionString);
                    if (!help.CheckConnection())
                    {
                        XtraMessageBox.Show("Không thể kết nối với server (Chuỗi kết nối 2)", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        frm_Grd_ConnectDatabase frm = new frm_Grd_ConnectDatabase();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Không tìm thấy file cấu hình", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    frm_Grd_ConnectDatabase frm = new frm_Grd_ConnectDatabase();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadLock()
        {
            string strFalse = "";

            try
            {
                GrdCore.Provider._userID = txtTenDangNhap.Text.Trim();

                string rv = GrdCore.DAL.DA_DecentralizationManagements.LockUser(txtTenDangNhap.Text);

                if (!rv.Contains("..."))
                {
                    strFalse = rv + "\n";
                }
                else
                {
                    dtGroup = BL_DecentralizationManagements.GetGroupUserByStaffID(txtTenDangNhap.Text);
                    if (dtGroup.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("User " + txtTenDangNhap.Text + " này chưa có nhóm. Liên hệ với Người quản trị để đưa vào nhóm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTenDangNhap.Text = string.Empty;
                        txtMatKhau.Text = string.Empty;
                        this.Show();
                        txtTenDangNhap.Focus();
                    }

                    if (dtGroup.Rows.Count != 0)
                    {
                        string password = CommonFunctions.EncodeMD5(txtTenDangNhap.Text.Trim(), txtMatKhau.Text.Trim());
                        int result = GrdCore.DAL.DA_DecentralizationManagements.Login(txtTenDangNhap.Text, password);
                        if (result != 0)
                            strFalse = "Đăng nhập thất bại.";
                        else
                        {
                            User._User = GrdCore.BLL.BL_DecentralizationManagements.GetStaffs(txtTenDangNhap.Text.Trim());
                            User._UserID = txtTenDangNhap.Text.Trim();
                            User._dtUserGroups = dtGroup.Copy();

                            foreach (DataRow dr in dtGroup.Rows)
                            {
                                if (User._UserGroup == string.Empty)
                                    User._UserGroup = dr["GroupID"].ToString();
                                else
                                    User._UserGroup = User._UserGroup + ";" + dr["GroupID"].ToString();
                            }

                            GrdCore.Provider._userID = txtTenDangNhap.Text.Trim();
                            User._UserName = ((User._User.LastName.Trim() + " " + User._User.MiddleName.Trim()).Trim() + " " + User._User.FirstName.Trim()).Trim();
                            User._UserPass = txtMatKhau.Text;
                            Init();
                            grpControlLogin.Dispose();
                        }
                    }
                }

                if (strFalse != "")
                {
                    XtraMessageBox.Show(strFalse, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDangNhap.Focus();
                    txtTenDangNhap.Text = string.Empty;
                    txtMatKhau.Text = string.Empty;
                }
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Text = string.Empty;
                txtMatKhau.Text = string.Empty;
            }
        }

        public static void GetDataSet()
        {
            try
            {
                User._dsDataDictionaries = new DataSet();

                DataSet ds = BL_HeThong.GetDataSetDataDictionary(User._UserID, User._UserGroup);
                if (ds.Tables.Count == 0)
                {
                    XtraMessageBox.Show("Không thể kết nối được với máy chủ. Vui lòng kiểm tra lại kết nối.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtSystemConfig = ds.Tables[0].Copy();
                dtSystemConfig.TableName = "SystemConfig";
                User._dsDataDictionaries.Tables.Add(dtSystemConfig);

                #region "Phân quyền các đối tượng trên Forms"
                try
                {
                    CommonFunctions._dtDecentralizations = ds.Tables[5];
                }
                catch { }
                #endregion

                #region table "GraduateLevels"
                try
                {
                    DataTable dtGraduateLevels = ds.Tables[1].Copy();
                    dtGraduateLevels.TableName = "GraduateLevels";
                    User._dsDataDictionaries.Tables.Add(dtGraduateLevels);
                }
                catch { }
                #endregion

                #region table "StudyTypes"
                try
                {
                    DataTable dtStudyTypes = ds.Tables[2].Copy();
                    dtStudyTypes.TableName = "StudyTypes";
                    User._dsDataDictionaries.Tables.Add(dtStudyTypes);
                }
                catch { }
                #endregion

                #region table "Departments"
                try
                {
                    DataTable dtDepartments = ds.Tables[3].Copy();
                    dtDepartments.TableName = "Departments";
                    User._dsDataDictionaries.Tables.Add(dtDepartments);
                }
                catch { }
                #endregion

                #region table "Terms"
                try
                {
                    DataTable dtTerms = ds.Tables[4].Copy();
                    dtTerms.TableName = "Terms";
                    User._dsDataDictionaries.Tables.Add(dtTerms);
                }
                catch { }
                #endregion

                #region table "GraduateLevels"
                try
                {
                    DataTable dtOlogies = ds.Tables[6].Copy();
                    dtOlogies.TableName = "Ologies";
                    User._dsDataDictionaries.Tables.Add(dtOlogies);
                }
                catch { }
                #endregion

                #region table "SystemConfigs"
                try
                {
                    DataRow[] drSelect;

                    #region CurrentYearStudy 
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CurrentYearStudy'");
                        if (drSelect.Length != 0)
                        {
                            User._CurrentYearStudy = drSelect[0]["SettingStringData"].ToString();
                        }
                    }
                    catch
                    {
                        User._CurrentYearStudy = string.Empty;
                    }
                    #endregion

                    #region CurrentTerm
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CurrentTerm'");
                        if (drSelect.Length != 0)
                        {
                            User._CurrentTerm = drSelect[0]["SettingStringData"].ToString();
                        }
                    }
                    catch
                    {
                        User._CurrentTerm = string.Empty;
                    }
                    #endregion

                    #region GraduateLevelID
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'GraduateLevelID'");
                        if (drSelect.Length != 0)
                        {
                            User._CurrentGraduateLevelID = drSelect[0]["SettingStringData"].ToString();
                        }
                    }
                    catch
                    {
                        User._CurrentGraduateLevelID = string.Empty;
                    }
                    #endregion

                    #region StudyTypeID
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'StudyTypeID'");
                        if (drSelect.Length != 0)
                        {
                            User._CurrentStudyTypeID = drSelect[0]["SettingStringData"].ToString();
                        }
                    }
                    catch
                    {
                        User._CurrentStudyTypeID = string.Empty;
                    }
                    #endregion

                    #region CollegeNumber
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CollegeNumber'");
                        if (drSelect.Length != 0)
                        {
                            User._CollegeID = int.Parse(drSelect[0]["SettingStringData"].ToString());
                        }
                    }
                    catch
                    {
                        User._CollegeID = 0;
                    }
                    #endregion

                    #region ThuTuSapXepSinhVien
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'ThuTuSapXepSinhVien'");
                        if (drSelect.Length != 0)
                            User._ThuTuSapXepDanhSach = int.Parse(drSelect[0]["SettingStringData"].ToString());
                    }
                    catch
                    {
                        User._ThuTuSapXepDanhSach = 0;
                    }
                    #endregion

                    #region CollegeLogo
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CollegeLogo'");
                        if (drSelect.Length != 0)
                        {
                            User._CollegeLogo = (byte[])drSelect[0]["SettingBinaryData"];
                        }
                    }
                    catch { }
                    #endregion

                    #region AdministrativeUnit
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'AdministrativeUnit'");
                        if (drSelect.Length != 0)
                            User._AdministrativeUnit = drSelect[0]["SettingStringData"].ToString();
                    }
                    catch
                    {
                        User._AdministrativeUnit = string.Empty;
                    }
                    #endregion

                    #region CollegeName
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CollegeName'");
                        if (drSelect.Length != 0)
                            User._CollegeName = drSelect[0]["SettingStringData"].ToString();
                    }
                    catch
                    {
                        User._CollegeName = string.Empty;
                    }
                    #endregion

                    #region CollegeAddress
                    try
                    {
                        drSelect = ds.Tables[0].Select("SettingName = 'CollegeAddress'");
                        if (drSelect.Length != 0)
                            User._CollegeAddress = drSelect[0]["SettingStringData"].ToString();
                    }
                    catch
                    {
                        User._CollegeAddress = string.Empty;
                    }
                    #endregion
                }
                catch { }
                #endregion

                #region table "Khoa"
                DataTable dtKhoa = new DataTable();

                try
                {
                    dtKhoa.Columns.Add("DepartmentID", typeof(string));
                    dtKhoa.Columns.Add("DepartmentName", typeof(string));
                    foreach (DataRow dr in User._dsDataDictionaries.Tables["Departments"].Rows)
                    {
                        if (dr["Parent"].ToString() == string.Empty)
                            dtKhoa.Rows.Add(new object[] { dr["DepartmentID"].ToString(), dr["DepartmentName"].ToString() });
                    }
                }
                catch
                {
                    dtKhoa = new DataTable();
                }

                dtKhoa.TableName = "Khoa";
                User._dsDataDictionaries.Tables.Add(dtKhoa);
                #endregion

                #region table "BoMon"
                DataTable dtBoMon = new DataTable();

                try
                {
                    dtBoMon.Columns.Add("DepartmentID", typeof(string));
                    dtBoMon.Columns.Add("DepartmentName", typeof(string));
                    foreach (DataRow dr in User._dsDataDictionaries.Tables["Departments"].Rows)
                    {
                        if (dr["Parent"].ToString() != string.Empty)
                            dtBoMon.Rows.Add(new object[] { dr["DepartmentID"].ToString(), dr["DepartmentName"].ToString() });
                    }
                }
                catch
                {
                    dtBoMon = new DataTable();
                }

                dtBoMon.TableName = "BoMon";
                User._dsDataDictionaries.Tables.Add(dtBoMon);
                #endregion

            }
            catch { }
        }

        private void Init()
        {
            GetDataSet();

            #region Bậc đào tạo
            GetGraduateLevels();
            if (User._CurrentGraduateLevelID == string.Empty)
                chkComboBacDaoTao.CheckAll();
            else
            {
                bool macDinh = false;
                foreach (string str in User._CurrentGraduateLevelID.Split(';'))
                    if (((DataTable)chkComboBacDaoTao.Properties.DataSource).Select("GraduateLevelID = '" + str + "'").Length > 0)
                    {
                        macDinh = true;
                        break;
                    }

                if (macDinh == false)
                    chkComboBacDaoTao.CheckAll();
                else
                    chkComboBacDaoTao.EditValue = User._CurrentGraduateLevelID;
            }
            chkComboBacDaoTao.RefreshEditValue();
            #endregion

            #region Loại hình đào tạo
            GetStudyTypes();
            if (User._CurrentStudyTypeID == string.Empty)
                chkComboLoaiHinhDaoTao.CheckAll();
            else
            {
                bool macDinh = false;
                foreach (string str in User._CurrentStudyTypeID.Split(';'))
                    if (((DataTable)chkComboLoaiHinhDaoTao.Properties.DataSource).Select("StudyTypeID = '" + str + "'").Length > 0)
                    {
                        macDinh = true;
                        break;
                    }

                if (macDinh == false)
                    chkComboLoaiHinhDaoTao.CheckAll();
                else
                    chkComboLoaiHinhDaoTao.EditValue = User._CurrentStudyTypeID;
            }
            chkComboLoaiHinhDaoTao.RefreshEditValue();
            #endregion

            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);
            #endregion

            grpCommonInfo.Visible = true;

            barButtonItemLuuMacDinh.Visibility = BarItemVisibility.Always;
            barButtonItemDangNhapLai.Visibility = BarItemVisibility.Always;
            barButtonItemDoiMatKhau.Visibility = BarItemVisibility.Always;
            barButtonItemThoatUngDung.Visibility = BarItemVisibility.Always;

            barButtonItemLuuMacDinh.Enabled = true;
            barButtonItemDangNhapLai.Enabled = true;
            barButtonItemDoiMatKhau.Enabled = true;
            barButtonItemThoatUngDung.Enabled = true;

            LayThongTinHocKyNamHoc();
            barButtonItemNguoiDungHienTai.Caption = "Người dùng hiện hành: " + User._User.StaffName.ToString();
            barButtonItemHocKyHienTai.Caption = String.Format("Năm học - học kỳ hiện tại: {0} - {1}", User._CurrentYearStudy, User._CurrentTerm);
            barButtonItemNguoiDungHienTai.Visibility = BarItemVisibility.Always;
            barButtonItemHocKyHienTai.Visibility = BarItemVisibility.Always;
            barButtonItemNguoiDungHienTai.Enabled = true;
            barButtonItemHocKyHienTai.Enabled = true;
 
            #region Config form main
            ribbonPageHeThong.Visible = true;
            ribbonPageChungChi.Visible = true;
            ribbonPageBangChungChi.Visible = true;
            ribbonPage_Phoibang.Visible = true;
            #endregion

            #region Tab phân quyền không bị ảnh hưởng bởi phân quyền khi đăng nhập ADMIN, UISTEAM hay nhóm ADMIN
            if (User._UserID.ToUpper() == "ADMIN" || User._UserID.ToUpper() == "UISTEAM")
            {
                ribbonPageGroupHeThong.Visible = true;
                barButtonItemNhomChucNang.Visibility = BarItemVisibility.Always;
                barButtonItemChucNang.Visibility = BarItemVisibility.Always;
                barButtonItemDoiTuong.Visibility = BarItemVisibility.Always;
                barButtonItemLuoiHienThi.Visibility = BarItemVisibility.Always;
                barButtonItemCacCotHienThi.Visibility = BarItemVisibility.Always;
                barButtonItemQuanLyNguoiDung.Visibility = BarItemVisibility.Always;
                barButtonItemNhomQuyen.Visibility = BarItemVisibility.Always;
                barButtonItemPhanQuyenNguoiDung.Visibility = BarItemVisibility.Always;
                barButtonItemPhanQuyenHeThong.Visibility = BarItemVisibility.Always;
                barButtonItemTruongDuLieu.Visibility = BarItemVisibility.Always;
            }
            else
            {
                ribbonPageGroupHeThong.Visible = false;
            }

            #region Cấu hình hiển thị chức năng
            barButtonItem_DSSVTotNghiep.Visibility = BarItemVisibility.Never;
            barButtonItem_DSKyNhanBangCC.Visibility = BarItemVisibility.Never;

            switch (User._CollegeID)
            {
                case 201:
                    break;
                case 27:
                case 202:
                    barButtonItem_DSSVTotNghiep.Visibility = BarItemVisibility.Always;
                    break;
                case 203:
                    break;
            }
            #endregion
            #endregion

            //------------- Tạm bỏ khi nào viết thì hiện ra lại --------------------
            barButtonItemCauHinhModule.Visibility = BarItemVisibility.Never;
           // ribbonPage_Phoibang.Visible = false;

            barButtonItemTraCuuBang.Visibility = BarItemVisibility.Never;
            //----------------------------------------------------------------------

            me = this;
            _pageCount = 0;
        }

        void LayThongTinHocKyNamHoc()
        {
            try
            {
                txtNamHoc.Text = string.Empty;
                txtHocKy.Text = string.Empty;
                txtNgayThangNam.Text = string.Empty;
                txtNguoiDungHienTai.Text = string.Empty;
                barButtonItemNguoiDungHienTai.Caption = string.Empty;
                barButtonItemHocKyHienTai.Caption = string.Empty;

                try
                {
                    txtNamHoc.Text = User._CurrentYearStudy;
                }
                catch { }

                try
                {
                    txtHocKy.Text = User._CurrentTerm;
                }
                catch { }

                try
                {
                    barButtonItemHocKyHienTai.Caption = "Năm học: " + User._CurrentYearStudy + "  Học kỳ: " + User._CurrentTerm;
                }
                catch { }

                try
                {
                    DataRow[] drc = User._dsDataDictionaries.Tables["Terms"].Copy().Select("YearStudy = '" + User._CurrentYearStudy + "' and TermID = '" + User._CurrentTerm + "'");
                }
                catch { }

                try
                {
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            txtNgayThangNam.Text = "Thứ Hai, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Tuesday:
                            txtNgayThangNam.Text = "Thứ Ba, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Wednesday:
                            txtNgayThangNam.Text = "Thứ Tư, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Thursday:
                            txtNgayThangNam.Text = "Thứ Năm, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Friday:
                            txtNgayThangNam.Text = "Thứ Sáu, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Saturday:
                            txtNgayThangNam.Text = "Thứ Bảy, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                        case DayOfWeek.Sunday:
                            txtNgayThangNam.Text = "Chủ Nhật, " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            break;
                    }
                }
                catch { }

                try
                {
                    txtNguoiDungHienTai.Text = User._UserName.ToString();
                    barButtonItemNguoiDungHienTai.Caption = "Người dùng hiện hành: " + User._UserName.ToString();
                }
                catch { }
            }
            catch { }
        }

        private void GetGraduateLevels()
        {
            try
            {
                chkComboBacDaoTao.Properties.DataSource = null;

                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();

                chkComboBacDaoTao.Properties.DataSource = _dtGraduateLevels;
                chkComboBacDaoTao.Properties.DisplayMember = "GraduateLevelName";
                chkComboBacDaoTao.Properties.ValueMember = "GraduateLevelID";

                chkComboBacDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }

        private void GetStudyTypes()
        {
            try
            {
                chkComboLoaiHinhDaoTao.Properties.DataSource = null;

                DataTable _dtStudyTypes = User._dsDataDictionaries.Tables["StudyTypes"].Copy();

                chkComboLoaiHinhDaoTao.Properties.DataSource = _dtStudyTypes;
                chkComboLoaiHinhDaoTao.Properties.DisplayMember = "StudyTypeName";
                chkComboLoaiHinhDaoTao.Properties.ValueMember = "StudyTypeID";

                chkComboLoaiHinhDaoTao.Properties.SeparatorChar = ';';
            }
            catch { }
        }
        #endregion

        #region Events
        #region MainForm
        private void mnuDongCuaSoNay_Click(object sender, EventArgs e)
        {
            try
            {
                Form frm = xtraTabbedMdiManager.SelectedPage.MdiChild as Form;
                if (frm != null)
                {
                    frm.Close();
                }
            }
            catch { }
        }

        private void mnuDongTatCaCuaSoKhac_Click(object sender, EventArgs e)
        {
            try
            {
                Form frm = xtraTabbedMdiManager.SelectedPage.MdiChild as Form;
                foreach (Form frmOther in xtraTabbedMdiManager.MdiParent.MdiChildren)
                    if (frmOther != frm)
                        frmOther.Close();
            }
            catch { }
        }

        private void mnuDongTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frmOther in xtraTabbedMdiManager.MdiParent.MdiChildren)
                    frmOther.Close();
            }
            catch { }
        }

        private void barButtonItemLuuMacDinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_pageCount != 0)
                {
                    XtraMessageBox.Show("Phải đóng tất cả các cửa sổ đang mở.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (XtraMessageBox.Show("Lưu các giá trị năm học, học kỳ, bậc đào tạo và loại hình đào tạo đang chọn làm mặc định ?"
                    , "UIS - Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                int result = BL_HeThong.UpdateCurrentValues(User._UserID, User._CurrentTerm, User._CurrentYearStudy, User._CurrentGraduateLevelID, User._CurrentStudyTypeID);

                if (result == 0)
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    XtraMessageBox.Show("Lưu dữ liệu không thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemDoiMatKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(HeThong.frm_Grd_DoiMatKhau), this))
            {
                HeThong.frm_Grd_DoiMatKhau f = new HeThong.frm_Grd_DoiMatKhau();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItemDangNhapLai_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Bạn có muốn thoát ứng dụng và đăng nhập lại ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                System.Diagnostics.Process.Start(Application.ExecutablePath);
                this.Close();
            }
            catch { }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            Login(false);
        }

        private void btnThoatUngDung_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn thoát ứng dụng ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Application.Exit();        
        }

        private void xtraTabbedMdiManager_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            try
            {
                _pageCount++;
                grpCommonInfo.Visible = (_pageCount == 0);
                layoutCommonInfo.Visible = (_pageCount == 0);
            }
            catch { }
        }

        private void xtraTabbedMdiManager_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            try
            {
                _pageCount--;
                grpCommonInfo.Visible = (_pageCount == 0);
                layoutCommonInfo.Visible = (_pageCount == 0);
            }
            catch { }
        }

        private void xtraTabbedMdiManager_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    if (e.Button == MouseButtons.Right && xtraTabbedMdiManager.SelectedPage != null)
                    {
                        Rectangle rec = xtraTabbedMdiManager.SelectedPage.TabControl.ViewInfo.SelectedTabPageViewInfo.Bounds;
                        if (e.X > rec.Left && e.X < rec.Right && e.Y > rec.Top && e.Y < rec.Bottom)
                        {
                            if (xtraTabbedMdiManager.Pages.Count == 1)
                                mnuDongTatCaCuaSoKhac.Enabled = false;
                            else
                                mnuDongTatCaCuaSoKhac.Enabled = true;
                            cmsDongTab.Show(new Point(MousePosition.X + 3, MousePosition.Y + 3));
                        }
                    }
                }
            }
            catch { }
        }

        private void barButtonItemHocKyHienTai_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_pageCount != 0)
                {
                    if (XtraMessageBox.Show("Đóng tất cả các form đang mở ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                mnuDongTatCa_Click(null, null);
                btnDoiHocKy_Click(null, null);
            }
            catch { }
        }

        private void btnDoiHocKy_Click(object sender, EventArgs e)
        {
            try
            {
                HeThong.frm_Grd_DoiNamHocHocKy frm = new HeThong.frm_Grd_DoiNamHocHocKy();
                frm._currentYearStudy = User._CurrentYearStudy;
                frm._currentTermID = User._CurrentTerm;
                frm.ShowDialog();
                if (frm._isSubmitted)
                {
                    User._CurrentYearStudy = frm._currentYearStudy;
                    User._CurrentTerm = frm._currentTermID;

                    txtNamHoc.Text = frm._currentYearStudy;
                    txtHocKy.Text = frm._currentTermID;
                    LayThongTinHocKyNamHoc();
                }
            }
            catch { }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDangNhap_Click(null, null);
        }

        private void chkComboBacDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            if (!_firstGraduateLevels)
                User._CurrentGraduateLevelID = chkComboBacDaoTao.EditValue.ToString();
            _firstGraduateLevels = false;
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMatKhau.Focus();
        }

        private void chkComboLoaiHinhDaoTao_EditValueChanged(object sender, EventArgs e)
        {
            if (!_firstStudyTypes)
                User._CurrentStudyTypeID = chkComboLoaiHinhDaoTao.EditValue.ToString();
            _firstStudyTypes = false;
        }

        private void barButtonItemThoatUngDung_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn có muốn thoát ứng dụng ?", "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Application.Exit();
        }
        #endregion

        #region Ribbon
        #region Hệ thống
        #region Lưới hiển thị
        private void barButtonItemLuoiHienThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(HeThong.frm_Grd_LuoiHienThi), this))
            {
                HeThong.frm_Grd_LuoiHienThi f = new HeThong.frm_Grd_LuoiHienThi();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Các cột hiển thị trên lưới
        private void barButtonItemCacCotHienThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(HeThong.frm_Grd_CotLuoiHienThi), this))
            {
                HeThong.frm_Grd_CotLuoiHienThi f = new HeThong.frm_Grd_CotLuoiHienThi();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Cấu hình module
        private void barButtonItemCauHinhModule_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(HeThong.frm_Grd_CauHinhModule), this))
            {
                HeThong.frm_Grd_CauHinhModule f = new HeThong.frm_Grd_CauHinhModule();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Thông tin đóng dấu, ký tên
        private void barButtonItemDongDauKyTen_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(HeThong.frm_Grd_DongDauKyTen), this))
            {
                HeThong.frm_Grd_DongDauKyTen f = new HeThong.frm_Grd_DongDauKyTen();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion
        #endregion

        #region Tốt nghiệp - chứng chỉ
        #region Loại xét
        private void barButtonItemLoaiChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DanhMucLoaiChungChi), this))
            {
                ChungChi.frm_Grd_DanhMucLoaiChungChi f = new ChungChi.frm_Grd_DanhMucLoaiChungChi();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Điều kiện xét
        private void barButtonItemDieuKienXetChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DieuKienXet), this))
            {
                ChungChi.frm_Grd_DieuKienXet f = new ChungChi.frm_Grd_DieuKienXet();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Nộp hồ sơ chứng chỉ
        private void barButtonItemNopChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_NopHoSoChungChi), this))
            {
                ChungChi.frm_Grd_NopHoSoChungChi f = new ChungChi.frm_Grd_NopHoSoChungChi();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Chuẩn xét
        private void barButtonItemChuanXetChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_ChuanXet), this))
            {
                ChungChi.frm_Grd_ChuanXet f = new ChungChi.frm_Grd_ChuanXet();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Tạo chuẩn nhanh
        private void barButtonItemTaoChuanNhanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_TaoChuanXetNhanh), this))
            {
                ChungChi.frm_Grd_TaoChuanXetNhanh f = new ChungChi.frm_Grd_TaoChuanXetNhanh();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        } 
        #endregion

        #region Số chuẩn xét đạt
        private void barButtonItemSoChuanXetDat_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_SoChuanXetDat), this))
            {
                ChungChi.frm_Grd_SoChuanXetDat f = new ChungChi.frm_Grd_SoChuanXetDat();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Đợt xét
        private void barButtonItemDotXetChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DotXet), this))
            {
                ChungChi.frm_Grd_DotXet f = new ChungChi.frm_Grd_DotXet();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Xét tốt nghiệp, chứng chỉ
        private void barButtonItemXetChungChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_XetTotNghiep), this))
            {
                ChungChi.frm_Grd_XetTotNghiep f = new ChungChi.frm_Grd_XetTotNghiep();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Phương thức tốt nghiệp - (USSH)
        private void barButtonItem_DSSVTotNghiep_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DS_SinhVienTotNghiep), this))
            {
                ChungChi.frm_Grd_DS_SinhVienTotNghiep f = new ChungChi.frm_Grd_DS_SinhVienTotNghiep();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        } 
        #endregion
        #endregion
        #endregion

        #region In bằng
        #region Trường dữ liệu
        private void barButtonItemTruongDuLieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_TruongDuLieu), this))
            {
                InBang.frm_Grd_TruongDuLieu f = new InBang.frm_Grd_TruongDuLieu();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Quản lý mẫu in
        private void barButtonItemQuanLyMauIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_QuanLyMauIn), this))
            {
                InBang.frm_Grd_QuanLyMauIn f = new InBang.frm_Grd_QuanLyMauIn();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Quản lý quyết định
        private void barButtonItemQuanLyQuyetDinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_QuanLyQuyetDinh), this))
            {
                InBang.frm_Grd_QuanLyQuyetDinh f = new InBang.frm_Grd_QuanLyQuyetDinh();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Cấp quyết định công nhận
        private void barButtonItemChungNhanTotNghiep_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_QuyetDinhCongNhan), this))
            {
                InBang.frm_Grd_QuyetDinhCongNhan f = new InBang.frm_Grd_QuyetDinhCongNhan();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Cấp quyết định hoàn thành
        private void barButtonItemChungNhanHoanThanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_QuyetDinhHoanThanh), this))
            {
                InBang.frm_Grd_QuyetDinhHoanThanh f = new InBang.frm_Grd_QuyetDinhHoanThanh();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Đợt cấp
        private void barButtonItemDotCapBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_DotCap), this))
            {
                InBang.frm_Grd_DotCap f = new InBang.frm_Grd_DotCap();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItemTraCuuBang_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem_DSKyNhanBangCC_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_XacNhanNhanBang), this))
            {
                InBang.frm_Grd_XacNhanNhanBang f = new InBang.frm_Grd_XacNhanNhanBang();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }

        #region Chứng chỉ có thể thay thế cho chứng chỉ cần xét
        private void barButtonItem_ChungChiThayThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_ChungChiNopThayThe), this))
            {
                ChungChi.frm_Grd_ChungChiNopThayThe f = new ChungChi.frm_Grd_ChungChiNopThayThe();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Chứng chỉ thay thế";
                f.Show();
            }
        }

        #region Cập nhật chương trình đào tạo đợt xét
        private void barButtonItem_CTDT_DotXet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DotCap_ChuongTrinh), this))
            {
                ChungChi.frm_Grd_DotCap_ChuongTrinh f = new ChungChi.frm_Grd_DotCap_ChuongTrinh();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Đợt xét chương trình đào tạo";
                f.Show();
            }
        }
        #endregion

        #region Hiển thị danh sách sinh viên đã nộp HSCC
        private void barButtonItemDSSCNopHoSoCC_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(ChungChi.frm_Grd_DotCap_ChuongTrinh), this))
            {
                ChungChi.frm_Grd_DSSinhVienNopHoSoChungChi f = new ChungChi.frm_Grd_DSSinhVienNopHoSoChungChi();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Danh sách sinh viên đã nộp HSCC";
                f.Show();
            }
        }

        private void bar_Danhmucphoi_ItemClick(object sender, ItemClickEventArgs e)
        {
           if(!CommonFunctions.IsFocusForm(typeof(PhoiBang.frm_Grd_DanhMucPhoiBang), this))
            {
                PhoiBang.frm_Grd_DanhMucPhoiBang f = new PhoiBang.frm_Grd_DanhMucPhoiBang();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Danh mục phôi bằng";
                f.Show();
            }
        }

        private void bar_Danhmucloaiphoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(PhoiBang.frm_Grd_DanhMucLoaiPhoiBang), this))
            {
                PhoiBang.frm_Grd_DanhMucLoaiPhoiBang f = new PhoiBang.frm_Grd_DanhMucLoaiPhoiBang();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Danh mục loại phôi bằng";
                f.Show();
            }
        }

        private void bar_Cauhinhloaiphoinganh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(PhoiBang.frm_Grd_CauHinhLoaiPhoi_Nganh), this))
            {
                PhoiBang.frm_Grd_CauHinhLoaiPhoi_Nganh f = new PhoiBang.frm_Grd_CauHinhLoaiPhoi_Nganh();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Cấu hình loại phôi bằng - Ngành";
                f.Show();
            }
        }

        private void bar_Chitietphoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(PhoiBang.frm_Grd_Chitietphoi), this))
            {
                PhoiBang.frm_Grd_Chitietphoi f = new PhoiBang.frm_Grd_Chitietphoi();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Cấu hình loại phôi bằng - Ngành";
                f.Show();
            }
        }

        private void barDanhMucDotCapPhoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(PhoiBang.frm_Grd_DanhMucDotCapPhoiBang), this))
            {
                PhoiBang.frm_Grd_DanhMucDotCapPhoiBang f = new PhoiBang.frm_Grd_DanhMucDotCapPhoiBang();
                f.Owner = this;
                f.MdiParent = this;
                f.Text = "Danh mục đợt cấp phôi bằng";
                f.Show();
            }
        }

        #endregion

        #endregion

        #endregion

        #region Quyết định công nhận và đợt cấp
        private void barButtonItemDanhSachCapBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_QuyetDinhCongNhanVaDotCap), this))
            {
                InBang.frm_Grd_QuyetDinhCongNhanVaDotCap f = new InBang.frm_Grd_QuyetDinhCongNhanVaDotCap();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion

        #region Cấp bằng, chứng chỉ
        private void barButtonItemCapBangBangDiem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CommonFunctions.IsFocusForm(typeof(InBang.frm_Grd_CapBangChungChi), this))
            {
                InBang.frm_Grd_CapBangChungChi f = new InBang.frm_Grd_CapBangChungChi();
                f.Owner = this;
                f.MdiParent = this;
                f.Show();
            }
        }
        #endregion
        #endregion
        #endregion
    }
}