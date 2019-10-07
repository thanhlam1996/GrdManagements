using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace GrdReports
{
    public partial class XtraReport_CDYTBD_GiayChungNhanTN : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport_CDYTBD_GiayChungNhanTN()
        {
            InitializeComponent();
        }

        public void Init_Report(DataTable tbPrint, string _NgayIn, string _CapBac, string _NguoiKy, string _AdministrativeUnit, string _CollegeName)
        {
            this.DataSource = tbPrint;
            xrTblTenTruong.Text = _CollegeName.ToUpper();
            txtDV.Text = _AdministrativeUnit.ToUpper();
            txtTruong.Text = "Hiệu trưởng " + UppercaseWords(_CollegeName);
            txtNgayThi.Text = "Đã tốt nghiệp kỳ thi tháng [NgayThiTN] tại " + UppercaseWords(_CollegeName);
            txtNgayKy.Text = _NgayIn;
            txtCapBac.Text = _CapBac;
            txtHoTen.Text = _NguoiKy;
            txtSoQD.Text = "Số: [ID_SoQD]";
        }

        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}
