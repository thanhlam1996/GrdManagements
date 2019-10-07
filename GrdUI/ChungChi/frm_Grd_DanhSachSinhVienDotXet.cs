using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GrdCore.BLL;
using DevExpress.Common.Grid;
using DevExpress.XtraSplashScreen
    ;
using DevExpress.XtraGrid;

namespace GrdUI.ChungChi
{
    public partial class frm_Grd_DanhSachSinhVienDotXet : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public string _MaDot = string.Empty;
        DataTable _dtData = new DataTable();
        #endregion

        #region Inits
        public frm_Grd_DanhSachSinhVienDotXet()
        {
            InitializeComponent();
        }


        private void frm_Grd_ChuongTrinhDaoTao_Load(object sender, EventArgs e)
        {
            try
            {
                #region Phân quyền
                CommonFunctions.SetFormPermiss(this);
                #endregion

                GetData();
            }
            catch { }
        }
        #endregion 

        #region Functions
        private void GetData()
        {
            try
            {
                _dtData = BL_ChungChi.DanhSachSinhVien_DotXet(_MaDot);

                _dtData.Columns["KhongXet"].ReadOnly = false;

                gridControlData.DataSource = _dtData;
                AppGridView.InitGridView(gridViewData, true,false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect,false, false);
                AppGridView.ShowField(gridViewData,
                    new string[] { "KhongXet","StudentID", "StudentName", "BirthDay", "BirthPlace", "StudyProgramID", "StudyProgramName", "ClassStudentName", "CourseName", "CourseTime", "OlogyName" },
                    new string[] {"Không xét" ,"Mã SV", "Họ tên", "Ngày sinh", "Nơi sinh", "Mã CTĐT", "Tên CTĐT", "Lớp", "Khóa học", "Niên khóa", "Ngành" }
                    , new int[] { 70,70,150,50,200,100,200,50,100,50,50 });
                AppGridView.AlignField(gridViewData, new string[] { "KhongXet","StudentID", "BirthDay", "BirthPlace", "StudyProgramID" },
                    DevExpress.Utils.HorzAlignment.Center);

                AppGridView.SummaryField(gridViewData, "StudentID", " TS = {0:#,0}", DevExpress.Data.SummaryItemType.Count);

                gridViewData.OptionsView.ColumnAutoWidth = true;
                gridViewData.BestFitColumns();

                StyleFormatCondition khongDuocChapNhan9 = new DevExpress.XtraGrid.StyleFormatCondition();
                khongDuocChapNhan9.Appearance.BackColor = Color.Aqua;
                khongDuocChapNhan9.Appearance.Options.UseBackColor = true;
                khongDuocChapNhan9.Condition = FormatConditionEnum.Expression;
                khongDuocChapNhan9.Expression = "[DaXet] = 1";
                gridViewData.FormatConditions.Add(khongDuocChapNhan9);
            }
            catch { SplashScreenManager.CloseForm(false); }
        }
        #endregion

        #region Events
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void simpleButton_LuuDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager splashScreen = new SplashScreenManager();
                SplashScreenManager.ShowForm((Form)(this), typeof(frm_Grd_ChoThucThi), true, true, false);
                string strXml = string.Empty;
                foreach (DataRow Dr in _dtData.Rows)
                {
                    if (Dr["KhongXet"].ToString().ToUpper() == "TRUE" && Dr["DaXet"].ToString().ToUpper() != "TRUE")
                    {
                        strXml += "<Data StudentID = \"" + Dr["StudentID"].ToString() +
                                "\" MaDot = \"" + _MaDot +
                                "\"/>";
                    }
                }

                strXml = "<Root>" + strXml + "</Root>";

                int KQ = BL_ChungChi.DanhSachSinhVienKhongXet(strXml, _MaDot, User._User.StaffID.ToString());

                SplashScreenManager.CloseForm(false);

                if (KQ == 0)
                {
                    XtraMessageBox.Show("Quá trình cập nhật thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Cập nhật không thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}