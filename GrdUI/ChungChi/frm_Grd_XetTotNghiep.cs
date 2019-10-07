using DevExpress.Common.Grid;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using GrdCore.BLL;
using GrdReports;
using ScrUI.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.Net;
using System.Runtime.Serialization.Json;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_XetTotNghiep : Form
    {
        #region Variables
        DataTable _dtDSSinhVien = new DataTable();
        DataTable dtNhomTuChonDinhHuong = new DataTable();
        DataTable dtMonTuChonDinhHuong = new DataTable();
        DataTable _dtBangDiemTotNghiep = new DataTable();
        DataTable _dtDataExcel = new DataTable();
        DataTable _dtXetBatBuoc = new DataTable();
        DataTable _dtGridColumns = new DataTable();
        DataRow _drGrids;
        DataSet _dsThongTinXet = new DataSet();
        string _studentID = string.Empty;
        int _loaiLocDuLieu = 0;
        #endregion

        #region Init

        public frm_Grd_XetTotNghiep()
        {
            InitializeComponent();
        }

        #region private void frm_Grd_XetTotNghiep_Load(object sender, EventArgs e)
        private void frm_Grd_XetTotNghiep_Load(object sender, EventArgs e)
        {
            #region Phân quyền
            CommonFunctions.SetFormPermiss(this);

            #region Định nghĩa lưới
            DataTable dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();

            try
            {
                dtGrid = BL_DoiTuongPhanQuyen.LuoiHienThi();
                _drGrids = (DataRow)dtGrid.Select("GridID = 'XetTotNghiep'").GetValue(0);

                _dtGridColumns = BL_DoiTuongPhanQuyen.CotLuoiHienThi(_drGrids["ID"].ToString());
            }
            catch
            {
                XtraMessageBox.Show("Chưa định nghĩa tính năng.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #endregion

            GetGraduateLevels();
            GetStudyTypes();
            GetYearStudy();
            GetLoaiXet();
            //DieuKienXet();
            // DotXet();
            //KiemTraNoSachThuVien("11005035");
            if (User._CollegeID==46)
            {
                mnu_46_DNU_KetQuaXetTN.Visible = true;
            }
        }
        #endregion

        #endregion

        #region Functions
        #region private void GetYearStudy()
        private void GetYearStudy()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("YearStudy", typeof(string));
                dtData.Columns.Add("YearStudyID", typeof(string));

                foreach (DataRow dr in User._dsDataDictionaries.Tables["Terms"].Rows)
                    if (dtData.Select("YearStudy = '" + dr["YearStudy"].ToString() + "'").Length == 0)
                        dtData.Rows.Add(new object[] { dr["YearStudy"].ToString(), dr["YearStudy"].ToString() });


                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "YearStudy DESC";

                lookUpEdit_NamHoc.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_NamHoc.Properties.DisplayMember = "YearStudy";
                lookUpEdit_NamHoc.Properties.ValueMember = "YearStudy";

                LookUpColumnInfoCollection coll = lookUpEdit_NamHoc.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("YearStudy", 0, "Năm học"));

                lookUpEdit_NamHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_NamHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_NamHoc.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_NamHoc.Properties.NullText = "";
                lookUpEdit_NamHoc.EditValue = User._CurrentYearStudy;

            }
            catch { }
        }
        #endregion

        #region public void GetTerms(string yearStudy)
        public void GetTerms(string yearStudy)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("TermID", typeof(string));
                dtData.Columns.Add("TermName", typeof(string));

                DataRow[] drSelect = User._dsDataDictionaries.Tables["Terms"].Select("YearStudy = '" + yearStudy + "'");
                foreach (DataRow dr in drSelect)
                    dtData.Rows.Add(new object[] { dr["TermID"].ToString(), dr["TermName"].ToString() });

                DataView dv = new DataView(dtData);
                dv.Sort = "TermName";

                lookUpEdit_HocKy.Properties.DataSource = dv.ToTable();
                lookUpEdit_HocKy.Properties.DisplayMember = "TermName";
                lookUpEdit_HocKy.Properties.ValueMember = "TermID";

                LookUpColumnInfoCollection coll = lookUpEdit_HocKy.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lookUpEdit_HocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_HocKy.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_HocKy.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_HocKy.Properties.NullText = "";
                lookUpEdit_HocKy.EditValue = User._CurrentTerm;

            }
            catch { }
        }
        #endregion

        #region private void GetGraduateLevels()
        private void GetGraduateLevels()
        {
            try
            {
                DataTable _dtGraduateLevels = User._dsDataDictionaries.Tables["GraduateLevels"].Copy();
                lookUpEdit_BacDT.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtGraduateLevels.Copy());
                myDataView.Sort = "GraduateLevelName";

                lookUpEdit_BacDT.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_BacDT.Properties.DisplayMember = "GraduateLevelName";
                lookUpEdit_BacDT.Properties.ValueMember = "GraduateLevelID";

                LookUpColumnInfoCollection coll = lookUpEdit_BacDT.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("GraduateLevelName", 0, "Bậc đào tạo"));

                lookUpEdit_BacDT.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_BacDT.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_BacDT.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_BacDT.ItemIndex = 0;
                lookUpEdit_BacDT.Properties.NullText = "";
                lookUpEdit_BacDT.EditValue = User._CurrentGraduateLevelID;

            }
            catch { }
        }
        #endregion

        #region private void GetStudyTypes()
        private void GetStudyTypes()
        {
            try
            {
                DataTable _dtStudyTypes = User._dsDataDictionaries.Tables["StudyTypes"].Copy();
                lookUpEdit_LHDT.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtStudyTypes.Copy());
                myDataView.Sort = "StudyTypeName";

                lookUpEdit_LHDT.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_LHDT.Properties.DisplayMember = "StudyTypeName";
                lookUpEdit_LHDT.Properties.ValueMember = "StudyTypeID";

                LookUpColumnInfoCollection coll = lookUpEdit_LHDT.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("StudyTypeName", 0, "Loại hình đào tạo"));

                lookUpEdit_LHDT.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_LHDT.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_LHDT.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_LHDT.ItemIndex = 0;
                lookUpEdit_LHDT.Properties.NullText = "";
                lookUpEdit_LHDT.EditValue = User._CurrentStudyTypeID;
            }
            catch { }
        }
        #endregion

        #region private void GetLoaiXet()
        private void GetLoaiXet()
        {
            try
            {
                DataTable _dtLoaiChungChi = BL_ChungChi.LayDanhSachLoaiChungChi();
                lookUpEdit_LoaiHinhXet.Properties.DataSource = null;

                DataView myDataViewCC = new DataView(_dtLoaiChungChi.Copy());
                myDataViewCC.Sort = "TenLoaiChungChi";

                lookUpEdit_LoaiHinhXet.Properties.DataSource = myDataViewCC.ToTable();
                lookUpEdit_LoaiHinhXet.Properties.DisplayMember = "TenLoaiChungChi";
                lookUpEdit_LoaiHinhXet.Properties.ValueMember = "MaLoaiChungChi";

                LookUpColumnInfoCollection col2 = lookUpEdit_LoaiHinhXet.Properties.Columns;
                col2.Clear();
                col2.Add(new LookUpColumnInfo("TenLoaiChungChi", 0, "Loại chứng chỉ"));

                lookUpEdit_LoaiHinhXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_LoaiHinhXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_LoaiHinhXet.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_LoaiHinhXet.ItemIndex = 0;
                lookUpEdit_LoaiHinhXet.Properties.NullText = "";
                lookUpEdit_LoaiHinhXet.EditValue = "TN";
            }
            catch { }
        }
        #endregion

        #region Đợt xét
        private void DotXet()
        {
            try
            {
                DataTable _dtDotXet = BL_ChungChi.DotXetTheoBacHe(lookUpEdit_BacDT.EditValue.ToString()
                    , lookUpEdit_LHDT.EditValue.ToString(), lookUpEdit_NamHoc.EditValue.ToString()
                    , lookUpEdit_HocKy.EditValue.ToString(), lookUpEdit_LoaiHinhXet.EditValue.ToString());
                lookUpEdit_DotXet.Properties.DataSource = null;

                DataView myDataView = new DataView(_dtDotXet.Copy());
                myDataView.Sort = "TenDot";

                lookUpEdit_DotXet.Properties.DataSource = myDataView.ToTable();
                lookUpEdit_DotXet.Properties.DisplayMember = "TenDot";
                lookUpEdit_DotXet.Properties.ValueMember = "MaDot";

                LookUpColumnInfoCollection coll = lookUpEdit_DotXet.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TenDot", 0, "Đợt xét"));

                lookUpEdit_DotXet.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookUpEdit_DotXet.Properties.SearchMode = SearchMode.AutoComplete;
                lookUpEdit_DotXet.Properties.AutoSearchColumnIndex = 0;
                lookUpEdit_DotXet.ItemIndex = 0;
                lookUpEdit_DotXet.Properties.NullText = "";

            }
            catch { }
        }
        #endregion

        #region XetTuChonDinhHuong

        #region private bool XetTuChonDinhHuongChoSinhVien(string StudentID, string MaChuanXet)
        private bool XetTuChonDinhHuongChoSinhVien(string StudentID, string MaChuanXet)
        {
            try
            {
                foreach (DataRow dr in dtNhomTuChonDinhHuong.Rows)
                {
                    if (dr["SelectionID"].ToString() != string.Empty && dr["SelectionParentID"].ToString() == string.Empty)
                    {
                        if (!XetNhomTuChonDinhHuong(dr["SelectionID"].ToString(), dr["SelectionParentID"].ToString())) return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                XtraMessageBox.Show("Lỗi xét tự chọn", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private bool XetNhomTuChonDinhHuong(string NhomTCCon, string NhomTCCha)
        private bool XetNhomTuChonDinhHuong(string NhomTCCon, string NhomTCCha)
        {
            if (dtNhomTuChonDinhHuong.Select("SelectionParentID = '" + NhomTCCon + "'").Length > 0)
            {
                decimal SoTC = 0;
                DataRow[] Row = dtNhomTuChonDinhHuong.Select("SelectionParentID = '" + NhomTCCon + "'");
                for (int i = 0; i < Row.Length; i++)
                {
                    if (XetNhomTuChonDinhHuong(Row[i]["SelectionID"].ToString(), Row[i]["SelectionParentID"].ToString()))
                    {
                        SoTC += Decimal.Parse(Row[i]["GatherCredits"].ToString());
                    }

                    if (SoTC >= Convert.ToDecimal(((DataRow)dtNhomTuChonDinhHuong.Select("SelectionID = '" + NhomTCCon + "'").GetValue(0))["GatherCredits"].ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                decimal TC = 0;
                foreach (DataRow drow in _dtBangDiemTotNghiep.Rows)
                {
                    if (drow["SelectionID"].ToString() == NhomTCCon && drow["SelectionParentID"].ToString() == NhomTCCha)
                    {
                        bool Pass = false;
                        if (_dtBangDiemTotNghiep.Select("CurriculumID = '" + drow["CurriculumID"].ToString() + "' and Ispass = 'x'").Length > 0)
                        {
                            Pass = true;
                        }

                        if (Pass == true)
                        {
                            TC += Decimal.Parse(drow["Credits"].ToString());
                        }
                    }

                    if (TC >= Convert.ToDecimal(((DataRow)dtNhomTuChonDinhHuong.Select("SelectionID = '" + NhomTCCon + "' and SelectionParentID ='" + NhomTCCha + "'").GetValue(0))["GatherCredits"].ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion

        #endregion

        #region private void XetTheoCTDT()
        private void XetTheoCTDT()
        {
            try
            {
                foreach (DataRow Dr in _dtDSSinhVien.Rows)
                {
                    if (Dr["Chon"].ToString().ToUpper() == "TRUE")
                    {
                        Dr["GhiChu"] = "";
                        Dr["MaXepLoai"] = "";
                        Dr["TenXepLoai"] = "";
                        Dr["TenKetQua"] = "Đạt";
                        Dr["KetQua"] = false;
                        bool KQ = true;
                        DataSet _dsData = BL_ChungChi.XetBB_TC(Dr["StudentID"].ToString(), Dr["MaChuanXet"].ToString(), lookUpEdit_DotXet.EditValue.ToString(), 0, lookUpEdit_LoaiHinhXet.EditValue.ToString());
                        dtNhomTuChonDinhHuong = _dsData.Tables["NhomTuChon"].Copy();
                        _dtBangDiemTotNghiep = _dsData.Tables["MonTrongNhomTuChon"].Copy();
                        _dtXetBatBuoc = _dsData.Tables["KetQuaXet"].Copy();

                        if (_dtXetBatBuoc.Rows.Count == 0)
                        {
                            Dr["GhiChu"] = "ERROR";
                            KQ = false;
                            continue;
                        }

                        if (_dtXetBatBuoc.Rows[0]["MonBB"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] = "Chưa đạt môn bắt buộc";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatSTCBB"].ToString().ToUpper() != "TRUE" && KQ == true)
                        {
                            Dr["GhiChu"] = (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "Chưa đạt số tín chỉ bắt buộc";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["TCDK"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "Chưa đạt tín chỉ điều kiện";
                            KQ = false;
                        }

                        //Xét tự chọn
                        if (!XetTuChonDinhHuongChoSinhVien(Dr["StudentID"].ToString(), Dr["MaChuanXet"].ToString()))
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "Nhóm tự chọn chưa đạt";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatSTCTC"].ToString().ToUpper() != "TRUE" && KQ == true)
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "Chưa đạt số tín chỉ tự chọn";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatDTB"].ToString().ToUpper() != "TRUE" && KQ == true)
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "Điểm trung bình chưa đạt";
                            KQ = false;
                        }

                        if (KQ == true)
                        {
                            Dr["MaXepLoai"] = _dtXetBatBuoc.Rows[0]["MaXepLoai"].ToString();
                            Dr["TenXepLoai"] = _dtXetBatBuoc.Rows[0]["TenXepLoai"].ToString();
                            Dr["GhiChu"] = _dtXetBatBuoc.Rows[0]["LyDoHaBac"].ToString();
                            Dr["TenKetQua"] = "Đạt";
                            Dr["KetQua"] = true;
                        }
                        else
                        {
                            Dr["TenKetQua"] = "Không đạt";
                            Dr["KetQua"] = false;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Lỗi xét tốt nghiệp", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void XetTN()
        private void XetTN()
        {
            try
            {
                foreach (DataRow Dr in _dtDSSinhVien.Rows)
                {
                    if (Dr["Chon"].ToString().ToUpper() == "TRUE")
                    {
                        Dr["GhiChu"] = "";
                        Dr["MaXepLoai"] = "";
                        Dr["TenXepLoai"] = "";
                        Dr["TenKetQua"] = "";
                        Dr["KetQua"] = false;
                        bool KQ = true;
                        DataSet _dsData = BL_ChungChi.XetBB_TC(Dr["StudentID"].ToString(), Dr["MaChuanXet"].ToString(), lookUpEdit_DotXet.EditValue.ToString(), 1, lookUpEdit_LoaiHinhXet.EditValue.ToString());
                        //"KetQuaXet|NhomTuChon|MonTrongNhomTuChon"
                        dtNhomTuChonDinhHuong = _dsData.Tables["NhomTuChon"].Copy();
                        _dtBangDiemTotNghiep = _dsData.Tables["MonTrongNhomTuChon"].Copy();
                        _dtXetBatBuoc = _dsData.Tables["KetQuaXet"].Copy();

                        if (_dtXetBatBuoc.Rows.Count == 0)
                        {
                            Dr["GhiChu"] = "ERROR";
                            KQ = false;
                            continue;
                        }

                        if (_dtXetBatBuoc.Rows[0]["MonBB"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] = "\n Chưa đạt môn bắt buộc";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatSTCBB"].ToString().ToUpper() != "TRUE" && KQ == true)
                        {
                            Dr["GhiChu"] = (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Chưa đạt số tín chỉ bắt buộc";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["TCDK"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Chưa đạt tín chỉ điều kiện";
                            KQ = false;
                        }

                        //Xét tự chọn
                        if (!XetTuChonDinhHuongChoSinhVien(Dr["StudentID"].ToString(), Dr["MaChuanXet"].ToString()))
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Nhóm tự chọn chưa đạt";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatSTCTC"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Chưa đạt số tín chỉ tự chọn";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatDTB"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Điểm trung bình chưa đạt";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DRL"].ToString().ToUpper() != "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Điểm rèn luyện chưa đạt";
                            KQ = false;
                        }

                        if (KQ == true)
                        {
                            Dr["MaXepLoai"] = _dtXetBatBuoc.Rows[0]["MaXepLoai"].ToString();
                            Dr["TenXepLoai"] = _dtXetBatBuoc.Rows[0]["TenXepLoai"].ToString();
                            Dr["GhiChu"] = _dtXetBatBuoc.Rows[0]["LyDoHaBac"].ToString();
                            Dr["TenKetQua"] = "Đạt khung CT";
                            Dr["HoanThanhCT"] = true;
                        }
                        else
                        {
                            Dr["HoanThanhCT"] = false;
                            Dr["TenKetQua"] = "Không đạt";
                            Dr["KetQua"] = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["DatCTDT1"].ToString().ToUpper() == "FALSE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Chưa tốt nghiệp chương trình 1";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["NoPhi"].ToString().ToUpper() == "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Còn nợ học phí";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["ThieuHoSo"].ToString().ToUpper() == "TRUE")
                        {
                            Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Chưa đủ hồ sơ";
                            KQ = false;
                        }

                        if (_dtXetBatBuoc.Rows[0]["XetNoSach"].ToString().ToUpper() == "TRUE")
                        {
                            if (KiemTraNoSachThuVien(Dr["StudentID"].ToString()))
                            {
                                Dr["GhiChu"] += (Dr["GhiChu"].ToString() == string.Empty ? "" : ";") + "\n Còn nợ sách thư viện";
                                KQ = false;
                            }
                        }

                        if (KQ == true)
                        {
                            Dr["GhiChu"] = _dtXetBatBuoc.Rows[0]["LyDoHaBac"].ToString();
                            Dr["TenKetQua"] = "Đạt TN";
                            Dr["KetQua"] = true;
                        }
                        else
                        {
                            Dr["TenKetQua"] = "Không đạt";
                            Dr["KetQua"] = false;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Lỗi xét tốt nghiệp", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region KiemTraNoSachThuVien
        private bool KiemTraNoSachThuVien(string StudentID)
        {
            try
            {
                string link = Class_URLThuVien.SERVICE_URL + StudentID;
                HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                DataContractJsonSerializer jSerializer = new DataContractJsonSerializer(typeof(Class_SinhVienNoSach[]));
                object oResponse = jSerializer.ReadObject(response.GetResponseStream());
                Class_SinhVienNoSach[] dsSinhVienNo = oResponse as Class_SinhVienNoSach[];
                if (dsSinhVienNo.Length > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        #region Events
        #region private void lookUpEdit_NamHoc_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_NamHoc_EditValueChanged(object sender, EventArgs e)
        {
            GetTerms(lookUpEdit_NamHoc.EditValue.ToString());
            DotXet();
        }
        #endregion

        #region private void simpleButton_Loc_Click(object sender, EventArgs e)
        private void simpleButton_Loc_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlData.DataSource = null;
                gridViewData.Columns.Clear();
                #region Lọc theo mã chuẩn
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                _dtDSSinhVien = BL_ChungChi.DanhSachSinhVien_ChuanXet( lookUpEdit_DotXet.EditValue.ToString(), checkEdit_XetMoi.Checked);

                foreach (DataColumn dc in _dtDSSinhVien.Columns)
                {
                    dc.ReadOnly = false;
                }

                gridControlData.DataSource = _dtDSSinhVien;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
                //AppGridView.InitGridView(gridViewData, true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect, false, false);
                //AppGridView.ShowField(gridViewData,
                //    new string[] { "Chon", "StudentID", "LastName", "FirstName", "Gender", "GioiTinh_TV", "BirthDay", "BirthPlace", "ClassStudentName", "TenChuanXet", "TenDieuKien", "DiemTBTL", "SoTC_BB", "SoTC_TC", "SoTCHocLai", "TenKetQua", "TenXepLoai", "GhiChu", "ChiTiet" },
                //    new string[] { "Chọn", "Mã sinh viên", "Họ lót", "Tên", "Giới tính", "Giới tính", "Ngày sinh", "Nơi sinh", "Lớp", "Chuẩn xét", "Tên ĐK xét", "Điểm TB Tích lũy", "Số TCBB", "Số TCTC", "Số TC học lại", "Kết quả", "Xếp loại", "Ghi chú", "Chi tiết" }
                //    , new int[] { 40, 70, 150, 50, 40, 40, 70, 70, 100, 150, 100, 70, 70, 70, 70, 70, 100, 200, 50 });
                //AppGridView.AlignField(gridViewData, new string[] { "Chon", "StudentID", "Gender", "BirthDay", "DiemTBC", "DiemTBTL", "SoTC_BB", "SoTC_TC", "SoTCThiLai", "SoTCHocLai" },
                //    DevExpress.Utils.HorzAlignment.Center);


                AppGridView.ReadOnlyColumn(gridViewData, new string[] { "LastName", "FirstName", "Gender", "BirthDay", "ClassStudentName", "TenChuanXet", "DiemTBTL", "SoTC_BB", "SoTC_TC", "SoTCThiLai", "SoTCHocLai" });
                gridViewData.Columns["ChiTiet"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
                AppGridView.RegisterControlField(gridViewData, "ChiTiet", repositoryItemButtonEdit_ChiTiet);

                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
                
                textBox_Dat.Text = _dtDSSinhVien.Select("KetQua = 1").Length.ToString();
                textBox_KhongDat.Text = _dtDSSinhVien.Select("KetQua = 0").Length.ToString();

                AppGridView.HideField(gridViewData, new string[] { "GioiTinh_TV" });
                AppGridView.SummaryField(gridViewData, "StudentID", " TS = {0:#,0}", DevExpress.Data.SummaryItemType.Count);

                AppGridView.SummaryField(gridViewData, "StudentName", " Đạt: " + textBox_Dat.Text + "\n Không đạt: " + textBox_KhongDat.Text, DevExpress.Data.SummaryItemType.Count);

                SplashScreenManager.CloseForm(false);
                #endregion
                       
            }
            catch(Exception ex)
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        #endregion

        #region private void simpleButton_Xet_Click(object sender, EventArgs e)
        private void simpleButton_Xet_Click(object sender, EventArgs e)
        {
            SplashScreenManager splashScreen = new SplashScreenManager();
            SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);
            try
            {
                XetTN();
                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
                textBox_Dat.Text = _dtDSSinhVien.Select("TenKetQua = 'Đạt TN'").Length.ToString();
                textBox_KhongDat.Text = _dtDSSinhVien.Select("TenKetQua = 'Không đạt'").Length.ToString();
                AppGridView.SummaryField(gridViewData, "StudentID", "Tổng số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.SummaryField(gridViewData, "LastName", " Đạt: " + textBox_Dat.Text + "\n Không đạt: " + textBox_KhongDat.Text, DevExpress.Data.SummaryItemType.Count);
            }
            catch { }
            SplashScreenManager.CloseForm(false);
            //contextMenuStrip_Xet.Show(Cursor.Position.X, Cursor.Position.Y);
        }
        #endregion

        #region private void checkEdit_All_CheckedChanged(object sender, EventArgs e)
        private void checkEdit_All_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_dtDSSinhVien.Columns.Count == 0)
                    return;

                string _studentID = string.Empty, _studyProgramID = string.Empty;

                for (int i = 0; i < gridViewData.DataRowCount; i++)
                {
                    gridViewData.GetDataRow(i)["Chon"] = checkEdit_All.Checked;
                }
            }

            catch { }
        }
        #endregion

        #region private void simpleButton_TinhDiemTB_Click(object sender, EventArgs e)
        private void simpleButton_TinhDiemTB_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewData.RowCount > 0)
                {
                    SplashScreenManager splashScreen = new SplashScreenManager();
                    SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);

                    int KQ;
                    foreach (DataRow Dr in _dtDSSinhVien.Rows)
                    {
                        if (Dr["Chon"].ToString().ToUpper() == "TRUE")
                        {
                            _studentID = Dr["StudentID"].ToString();
                            KQ = BL_ChungChi.TinhDiemTB(Dr["StudentID"].ToString(), Dr["MaChuanXet"].ToString());
                        }
                    }
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Tính điểm thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    simpleButton_Loc_Click(null, null);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Lỗi tính điểm " + _studentID, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void simpleButton_Luu_Click(object sender, EventArgs e)
        private void simpleButton_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                int KQ = 0;
                foreach (DataRow Dr in _dtDSSinhVien.Rows)
                {
                    if (Dr["Chon"].ToString().ToUpper() == "TRUE")
                    {
                        string _strXmlHocVien = "<Datas StudentID = \"" + Dr["StudentID"].ToString() +
                       "\" MaChuanXet = \"" + Dr["MaChuanXet"].ToString() +
                       "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString() +
                       "\" DiemTB10 = \"" + Dr["DiemTBTL10"].ToString() +
                       "\" DiemTB4 = \"" + Dr["DiemTBTL4"].ToString() +
                       "\" SoTC_TC = \"" + Dr["SoTC_TC"].ToString() +
                       "\" SoTC_BB = \"" + Dr["SoTC_BB"].ToString() +
                       "\" KetQua = \"" + (Dr["KetQua"].ToString().ToUpper() == "TRUE" ? "1" : "0") +
                       "\" SoTCHocLai = \"" + Dr["SoTCHocLai"].ToString() +
                       "\" SoTCThiLai = \"" + Dr["SoTCThiLai"].ToString() +
                       "\" GhiChu = \"" + Dr["GhiCHu"].ToString() +
                       "\" UpDateStaff = \"" + User._User.StaffID +
                       "\" MaLoaiChungChi = \"" + Dr["MaLoaiChungChi"].ToString() +
                       "\" DiemTBC10 = \"" + Dr["DiemTBC10"].ToString() +
                       "\" DiemTBC4 = \"" + Dr["DiemTBC4"].ToString() +
                       "\" DefaultRankID = \"" + Dr["MaXepLoai"].ToString() +
                       "\" HoanThanhCT = \"" + Dr["HoanThanhCT"].ToString() +
                       "\"/>";

                        _strXmlHocVien = "<Root>" + _strXmlHocVien + "</Root>";

                        KQ += BL_ChungChi.LuuKetQuaXet(_strXmlHocVien, "Upd");
                    }
                }
                if (KQ > 0)
                {
                    XtraMessageBox.Show("Lỗi lưu kết quả", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    XtraMessageBox.Show("Lưu thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    checkEdit_All.Checked = false;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Lỗi lưu kết quả", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void simpleButton_Xoa_Click(object sender, EventArgs e)
        private void simpleButton_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                int KQ = 0;
                switch (_loaiLocDuLieu)
                {
                    case 0:
                        if (_dtDSSinhVien.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in _dtDSSinhVien.Rows)
                            {
                                if (Dr["Chon"].ToString().ToUpper() == "TRUE")
                                {
                                    string _strXmlHocVien = "<Datas StudentID = \"" + Dr["StudentID"].ToString() +
                                   "\" MaChuanXet = \"" + Dr["MaChuanXet"].ToString() +
                                   "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString() +
                                   "\"/>";
                                    _strXmlHocVien = "<Root>" + _strXmlHocVien + "</Root>";
                                    KQ += BL_ChungChi.LuuKetQuaXet(_strXmlHocVien, "Del");
                                }
                            }
                            if (KQ > 0)
                            {
                                XtraMessageBox.Show("Lỗi xóa kết quả", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                XtraMessageBox.Show("Xóa thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                simpleButton_Loc_Click(null, null);
                                checkEdit_All.Checked = false;
                            }
                        }
                        else
                            checkEdit_All.Checked = false;
                        break;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Lỗi xóa kết quả", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void repositoryItemButtonEdit_ChiTiet_Click(object sender, EventArgs e)
        private void repositoryItemButtonEdit_ChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                frmChiTietDiem_XetTotNghiep frm = new frmChiTietDiem_XetTotNghiep();
                frm._StudentID = gridViewData.GetFocusedDataRow()["StudentID"].ToString();
                frm._StudentName = gridViewData.GetFocusedDataRow()["StudentName"].ToString();
                frm._TenChuanXet = gridViewData.GetFocusedDataRow()["TenChuanXet"].ToString();
                frm._DTB = gridViewData.GetFocusedDataRow()["DiemTBTL"].ToString();
                frm._STCBB = gridViewData.GetFocusedDataRow()["SoTC_BB"].ToString();
                frm._STCTC = gridViewData.GetFocusedDataRow()["SoTC_TC"].ToString();
                frm._TenKetQua = gridViewData.GetFocusedDataRow()["TenKetQua"].ToString();
                frm._MaChuanXet = gridViewData.GetFocusedDataRow()["MaChuanXet"].ToString();
                frm._GhiChu = gridViewData.GetFocusedDataRow()["GhiChu"].ToString();
                frm.ShowDialog();

            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region simpleButton_Excel_Click
        private void simpleButton_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                cms_ImportExcel.Show(Cursor.Position.X, Cursor.Position.Y);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void lookUpEdit_LoaiHinhXet_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_LoaiHinhXet_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }
        #endregion

        # region private void lookUpEdit_HocKy_EditValueChanged(object sender, EventArgs e)
        private void lookUpEdit_HocKy_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }
        #endregion

        #region private void toolSMI_CTDT_Click(object sender, EventArgs e)
        private void toolSMI_CTDT_Click(object sender, EventArgs e)
        {
            SplashScreenManager splashScreen = new SplashScreenManager();
            SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);
            try
            {
                XetTheoCTDT();
                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
                textBox_Dat.Text = _dtDSSinhVien.Select("TenKetQua = 'Đạt'").Length.ToString();
                textBox_KhongDat.Text = _dtDSSinhVien.Select("TenKetQua = 'Không đạt'").Length.ToString();
                AppGridView.SummaryField(gridViewData, "StudentID", "Tổng số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.SummaryField(gridViewData, "LastName", " Đạt: " + textBox_Dat.Text + "\n Không đạt: " + textBox_KhongDat.Text, DevExpress.Data.SummaryItemType.Count);
            }
            catch { }
            SplashScreenManager.CloseForm(false);
        }
        #endregion

        #region private void toolSMI_TN_Click(object sender, EventArgs e)
        private void toolSMI_TN_Click(object sender, EventArgs e)
        {
            SplashScreenManager splashScreen = new SplashScreenManager();
            SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);
            try
            {
                XetTN();
                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();
                textBox_Dat.Text = _dtDSSinhVien.Select("TenKetQua = 'Đạt'").Length.ToString();
                textBox_KhongDat.Text = _dtDSSinhVien.Select("TenKetQua = 'Không đạt'").Length.ToString();
                AppGridView.SummaryField(gridViewData, "StudentID", "Tổng số SV = {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                AppGridView.SummaryField(gridViewData, "LastName", " Đạt: " + textBox_Dat.Text + "\n Không đạt: " + textBox_KhongDat.Text, DevExpress.Data.SummaryItemType.Count);
            }
            catch { }
            SplashScreenManager.CloseForm(false);
        }
        #endregion

        #region In reports
        private void simpleButton_InDuLieu_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
        }
        #endregion

        #region private void simpleButton_Thoat_Click(object sender, EventArgs e)
        private void simpleButton_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region checkEdit_XetMoi_CheckedChanged(object sender, EventArgs e)
        private void checkEdit_XetMoi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit_XetMoi.Checked == true)
            {
                layoutControlItem_Luu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem_Luu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        #endregion
        
        #region lookUpEdit_BacDT_EditValueChanged
        private void lookUpEdit_BacDT_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }
        #endregion
                                                                                                                                                                                                                                                           
        #region lookUpEdit_LHDT_EditValueChanged
        private void lookUpEdit_LHDT_EditValueChanged(object sender, EventArgs e)
        {
            DotXet();
        }
        #endregion

        #region btnHienThi_Click
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            
            HeThong.frm_Grd_CotHienThi frm = new HeThong.frm_Grd_CotHienThi();
            frm._chucNang = "XetTotNghiep";
            frm._dtData = _dtGridColumns;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm._isAccepted)
            {
                _dtGridColumns = frm._dtData;
                AppGridView.InitGridView(gridViewData, _drGrids, _dtGridColumns, User._foreignLanguage);
            }
        }
        #endregion

        #region MenuItems
        #region Bảng điểm tốt nghiệp Đại Học Sư Phạm không tiêu đề
        private void mnu_04_HCMUP_BangDiemTN_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemTotNghiep_HCMUP(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region Bảng điểm tốt nghiệp Đại Học Sư Phạm có tiêu đề
        private void mnu_04_HCMUP_BangDiemTN_DayDu_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemTotNghiepDayDu_HCMUP(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region private void mnu_04_HCMUP_ChayChungNhanDiemTN_Click(object sender, EventArgs e)
        private void mnu_04_HCMUP_ChayChungNhanDiemTN_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._XtraReport_GiayChungNhanDiem_HCMUP(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch { }
        }
        #endregion

        #region mnu_45_DNU_BangDiemTN_Click
        private void mnu_45_DNU_BangDiemTN_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region mnu_46_DNU_BangDiemTN_Click
        private void mnu_46_DNU_BangDiemTN_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                string _nguoiLap = frmSoQD._NguoiLap;
                

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemTotNghiep_DNU(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemTotNghiep_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch(Exception ex) { }
        }
        #endregion

        #region mnu_Dat_Click
        private void mnu_Dat_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachDatTotNghiep(lookUpEdit_DotXet.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_DanhSachSVDatTN_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_DanhSachSinhVienDatTheoKhoa_Click
        private void mnu_DanhSachSinhVienDatTheoKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachDatTotNghiep(lookUpEdit_DotXet.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_Yersin_DanhSachSVDatTN_Khoa(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_KhongDat_Click
        private void mnu_KhongDat_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachKhongDatTotNghiep(lookUpEdit_DotXet.EditValue.ToString(), "#");

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_DanhSachSVKhongDatTN_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._CollegeName, User._AdministrativeUnit);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_KhongDatAV_Click
        private void mnu_KhongDatAV_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachKhongDatTotNghiep_NhomAV(lookUpEdit_DotXet.EditValue.ToString(), "AV");

                if (_tbPrint.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu để hiển thị", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_DanhSachSVKhongDatTN_NhomAV(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._CollegeName, User._AdministrativeUnit);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_DanhSachSinhVienKhongDatTheoNganh_Click
        private void mnu_DanhSachSinhVienKhongDatTheoNganh_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachKhongDatTotNghiep(lookUpEdit_DotXet.EditValue.ToString(), "#");

                if (_tbPrint.Rows.Count == 0)
                    return;
                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_Yersin_DanhSachSVKhongDatTN_Nganh(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_DanhSachSinhVienKhongDatTheoKhoa_Click
        private void mnu_DanhSachSinhVienKhongDatTheoKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachKhongDatTotNghiep(lookUpEdit_DotXet.EditValue.ToString(), "#");

                if (_tbPrint.Rows.Count == 0)
                    return;
                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_Yersin_DanhSachSVKhongDatTN_Khoa(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_mauImport_Click
        private void mnu_mauImport_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region mnu_exportExcel_Click
        private void mnu_exportExcel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SaveFileDialog sfdFiles = new SaveFileDialog();
            //    sfdFiles.Filter = "Microsoft Excel|*.xlsx";
            //    sfdFiles.FileName = "UIS - Xét tốt nghiệp "+lookUpEdit_BacDT.Text+lookUpEdit_LHDT.Text+ "-Đợt " + lookUpEdit_DotXet.EditValue.ToString();

            //    if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
            //    {
            //        GridControl gridControlExcel = new DevExpress.XtraGrid.GridControl();
            //        GridView gridViewExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            //        gridControlExcel.ViewCollection.Add(gridViewExcel);
            //        gridControlExcel.MainView = gridViewExcel;

            //        DataTable dtExcelLoad = new DataTable();
            //        dtExcelLoad.Columns.Add("Mã sinh viên");
            //        dtExcelLoad.Columns.Add("Họ lót");
            //        dtExcelLoad.Columns.Add("Tên");
            //        dtExcelLoad.Columns.Add("Giới tính");
            //        dtExcelLoad.Columns.Add("Ngày sinh");
            //        dtExcelLoad.Columns.Add("Nơi sinh");
            //        dtExcelLoad.Columns.Add("Lớp");
            //        dtExcelLoad.Columns.Add("Chuẩn xét");
            //        dtExcelLoad.Columns.Add("Tên ĐK xét");
            //        dtExcelLoad.Columns.Add("Điểm TB Tích lũy");
            //        dtExcelLoad.Columns.Add("Số TCBB");
            //        dtExcelLoad.Columns.Add("Số TCTC");
            //        dtExcelLoad.Columns.Add("Số TC học lại");
            //        dtExcelLoad.Columns.Add("Kết quả");
            //        dtExcelLoad.Columns.Add("Xếp loại");
            //        dtExcelLoad.Columns.Add("Ghi chú");
            //        DataRow dr;
            //        for (int i = 0; i < gridViewData.RowCount; i++)
            //        {
            //            dr = dtExcelLoad.NewRow();
            //            dr["Mã sinh viên"] = gridViewData.GetDataRow(i)["StudentID"].ToString();
            //            dr["Họ lót"] = gridViewData.GetDataRow(i)["LastName"].ToString();
            //            dr["Tên"] = gridViewData.GetDataRow(i)["FirstName"].ToString();
            //            dr["Giới tính"] = gridViewData.GetDataRow(i)["GioiTinh_TV"].ToString();
            //            dr["Ngày sinh"] = gridViewData.GetDataRow(i)["BirthDay"].ToString();
            //            dr["Nơi sinh"] = gridViewData.GetDataRow(i)["BirthPlace"].ToString();
            //            dr["Lớp"] = gridViewData.GetDataRow(i)["ClassStudentName"].ToString();
            //            dr["Chuẩn xét"] = gridViewData.GetDataRow(i)["TenChuanXet"].ToString();
            //            dr["Tên ĐK xét"] = gridViewData.GetDataRow(i)["TenDieuKien"].ToString();
            //            dr["Điểm TB Tích lũy"] = gridViewData.GetDataRow(i)["DiemTBTL"].ToString();
            //            dr["Số TCBB"] = gridViewData.GetDataRow(i)["SoTC_BB"].ToString();
            //            dr["Số TCTC"] = gridViewData.GetDataRow(i)["SoTC_TC"].ToString();
            //            dr["Số TC học lại"] = gridViewData.GetDataRow(i)["SoTCHocLai"].ToString();
            //            dr["Kết quả"] = gridViewData.GetDataRow(i)["TenKetQua"].ToString();
            //            dr["Xếp loại"] = gridViewData.GetDataRow(i)["TenXepLoai"].ToString();
            //            dr["Ghi chú"] = gridViewData.GetDataRow(i)["GhiChu"].ToString();
            //            dtExcelLoad.Rows.Add(dr);
            //        }
            //        foreach (DataColumn tCol in dtExcelLoad.Columns)
            //        {
            //            GridColumn gCol = new GridColumn();
            //            gCol.Name = "col" + tCol.ColumnName;
            //            gCol.FieldName = tCol.ColumnName;
            //            gCol.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
            //            gridViewExcel.Columns.Add(gCol);
            //            gCol.Visible = true;
            //        }
            //        gridControlExcel.DataSource = dtExcelLoad;
            //        Controls.Add(gridControlExcel);
            //        gridControlExcel.ForceInitialize();



            //        gridViewExcel.OptionsSelection.MultiSelect = false;
            //        ExportSettings.DefaultExportType = ExportType.WYSIWYG;
            //        gridControlExcel.Visible = true;
            //        var options = new XlsxExportOptions();

            //        options.SheetName = lookUpEdit_DotXet.Text;

            //        gridViewExcel.ExportToXlsx(sfdFiles.FileName, options);

            //        gridViewExcel.OptionsSelection.MultiSelect = true;
            //        Controls.Remove(gridControlExcel);
            //        XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gridViewData.OptionsSelection.MultiSelect = true;
            //    XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                SaveFileDialog sfdFiles = new SaveFileDialog();

                sfdFiles.Filter = "Microsoft Excel|*.xlsx";
                sfdFiles.FileName = "UIS - Kết quả xét tốt nghiệp";

                if (sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)
                {
                    gridViewData.OptionsSelection.MultiSelect = false;
                    ExportSettings.DefaultExportType = ExportType.WYSIWYG;

                    var options = new XlsxExportOptions();

                    options.SheetName = "Kết quả xét_Đợt xét " +lookUpEdit_DotXet.EditValue.ToString() ;

                    gridControlData.ExportToXlsx(sfdFiles.FileName, options);

                    gridViewData.OptionsSelection.MultiSelect = true;
                    XtraMessageBox.Show("Xuất file thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gridViewData.OptionsSelection.MultiSelect = true;
                XtraMessageBox.Show("Quá trình xuất file thất bại : " + ex.Message, "UIS - Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region mnu_31_UEL_BangDiemTNCQA3_Click
        private void mnu_31_UEL_BangDiemTNCQA3_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem StudentID = \"" + row["StudentID"].ToString()
                            + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                string _ngayKyTenTA = frmSoQD._ngayQDEng;
                string _nguoiKyTenTA = frmSoQD._hoVaTenEng;
                string _CapBacTA = frmSoQD._capBacEng;

                DataTable _tbTiengViet = new DataTable();
                DataTable _tbTiengAnh = new DataTable();
                _tbTiengViet = BL_ChungChi.BangDiemTotNghiepCQ_A3(strXml);
                _tbTiengAnh = BL_ChungChi.BangDiemTotNghiepCQ_A3_TiengAnh(strXml);

                if (_tbTiengViet.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                //frm._load_XtraReport_BangDiemTNCQ_A3_UEL(_tbTiengViet, _tbTiengAnh, _ngayKyTen, _CapBac, _nguoiKyTen, _ngayKyTenTA
                //    , _nguoiKyTenTA, _CapBacTA, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch(Exception ex) { }
        }
        #endregion

        #region mnu_46_DNU_KetQuaXetTN_Click
        private void mnu_46_DNU_KetQuaXetTN_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD._NgayThi = true;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                string _nguoiLap = frmSoQD._NguoiLap;
                string _NgayThi = frmSoQD._ngayThi;
                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.KetQuaXetTotNghiep_DNU(lookUpEdit_DotXet.EditValue.ToString());

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                //frm._load_XtraReport_DanhSachCongNhanTotNghiep_DNU(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._AdministrativeUnit, User._CollegeName);
                frm._load_XtraReport_KetQuaXetTotNghiep_DNU(_tbPrint, _ngayKyTen, _NgayThi, _CapBac, _nguoiKyTen, _nguoiLap, User._AdministrativeUnit, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region mnu_DanhSachSinhVienNoPhi_Click
        private void mnu_DanhSachSinhVienNoPhi_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachNoSach_TV(lookUpEdit_DotXet.EditValue.ToString(), 1);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_Yersin_DanhSachSVNoPhi(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._CollegeName, User._User.StaffName.ToUpper());
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_DanhSachSinhVienNoSach_Click
        private void mnu_DanhSachSinhVienNoSach_Click(object sender, EventArgs e)
        {
            try
            {
                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;

                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_InBang.DanhSachNoSach_TV(lookUpEdit_DotXet.EditValue.ToString(), 2);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_Yersin_DanhSachSVNoSach(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._CollegeName, User._User.StaffName.ToUpper());
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region mnu_31_UEL_GiayChungNhanHoanThanhKH_Click
        private void mnu_31_UEL_GiayChungNhanHoanThanhKH_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<GiayCNTNTemp StudentID = \"" + row["StudentID"].ToString()
                        + "\" MaChuanXet = \"" + row["MaChuanXet"].ToString()
                        + "\" MaDot = \"" + lookUpEdit_DotXet.EditValue.ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên in.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                frmSoQD._quyetDinh = false;
                frmSoQD.ShowDialog();

                if (frmSoQD._dongY == false)
                    return;

                string _ngayKyTen = frmSoQD._ngayQD;
                string _nguoiKyTen = frmSoQD._hoVaTen;
                string _CapBac = frmSoQD._capBac;
                string _nguoiLap = frmSoQD._NguoiLap;


                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.DSChungNhaHoanThanhKhoaHoc_UEL(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_ChungNhanHocThanhKhoaHoc_UEL(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen
                    , User._CollegeLogo, User._AdministrativeUnit, User._CollegeName);
                //frm._load_XtraReport_ChungNhanHocThanhKhoaHoc_UEL(_tbPrint, _ngayKyTen, _CapBac, _nguoiKyTen, User._User.StaffName
                //    , DBNull.Value.ToString(), this.Name, NameControl, TextControl, User._CollegeLogo, User._CollegeName, User._AdministrativeUnit);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }
        #endregion

        #endregion

        #endregion

        private void mnu_31_UEL_BangDiemXetTN_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = "<Root>";
                foreach (DataRow row in _dtDSSinhVien.Rows)
                {
                    if (row["Chon"].ToString().ToLower() == "true")
                    {
                        strXml += "<BangDiem MaChuanXet = \"" + row["MaChuanXet"].ToString()
                            + "\" StudentID = \"" + row["StudentID"].ToString()
                            + "\"/>";
                    }
                }
                strXml += "</Root>";
                if (strXml == "<Root></Root>")
                {
                    XtraMessageBox.Show("Chưa chọn sinh viên cần in bảng điểm.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //frmSoQuyetDinh frmSoQD = new frmSoQuyetDinh();
                //frmSoQD.StartPosition = FormStartPosition.CenterScreen;
                //frmSoQD._quyetDinh = false;
                //frmSoQD.ShowDialog();

                //if (frmSoQD._dongY == false)
                //    return;

                //string _ngayKyTen = frmSoQD._ngayQD;
                //string _nguoiKyTen = frmSoQD._hoVaTen;
                //string _CapBac = frmSoQD._capBac;
                //string _nguoiLap = frmSoQD._NguoiLap;


                DataTable _tbPrint = new DataTable();
                _tbPrint = BL_ChungChi.BangDiemXetTotNghiep(strXml);

                if (_tbPrint.Rows.Count == 0)
                    return;

                frmGrdReports frm = new frmGrdReports();
                DataTable dtConfig = User._dsDataDictionaries.Tables["ReportConfig"];
                frm._load_XtraReport_BangDiemXetTotNghiep_UEL(_tbPrint, User._User.Department, User._CollegeName);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }
    }
}
