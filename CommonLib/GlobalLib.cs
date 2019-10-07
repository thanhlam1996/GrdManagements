using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace CommonLib
{
    public class GlobalLib
    {
        private static string appPath = new FileInfo(Application.ExecutablePath).DirectoryName.ToString();
        //He thong tu quan ly , khong qua ly o CSDL
        private static DataTable _dtError;
        private static string _notDefineError = string.Empty;
        private static bool _isUISMain = false;

        public static bool IsRibbonMain = false;
        public static string _userID = string.Empty;

        public static Skin SkinName = Skin.Money_Twins;

        public static DevExpress.XtraBars.BarStaticItem ProgressBar = null;

        public static bool IsUISMain
        {
            get
            {
                if (IsRibbonMain) return false;
                return GlobalLib._isUISMain;
            }
            set { GlobalLib._isUISMain = value; }
        }

        public static string AppPath
        {
            get { return GlobalLib.appPath; }
            set { GlobalLib.appPath = value; }
        }

        public static string NotDefineError
        {
            get { return GlobalLib._notDefineError ; }
            set { GlobalLib._notDefineError  = value; }
        }

        public static DataTable DataError
        {
            get {
                return GlobalLib._dtError;
            }
        }

        public static void InitSystemError()
        {
            //Tao bang
            _dtError = new DataTable();
            _dtError.Columns.Add("Code", typeof(int));
            _dtError.Columns.Add("SystemName", typeof(string));
            _dtError.Columns.Add("Description", typeof(string));
            //Them du lieu
            _dtError.Rows.Add(0, "<Do not define>", " thành công");
            _dtError.Rows.Add(-1, "<Do not define>", " thất bại. \n Vui lòng liên hệ người quản lý.");
            _dtError.Rows.Add(-2, "Violation of PRIMARY KEY constraint", "Không thể thêm mẫu tin này do đã có trong hệ thống.");
            _dtError.Rows.Add(-3, "An error has occurred while establishing a connection to the server", "Không thể kết nối tới cơ sở dữ liệu trên máy chủ. \n Vui lòng kiểm tra lại kết nối.");
            _dtError.Rows.Add(-4, "ExecuteNonQuery requires an open and available Connection. The connection's current state is closed", "Tình trạng kết nối CSDL đã đóng.");
            _dtError.Rows.Add(-5, "DELETE statement conflicted with TABLE REFERENCE constraint", "Không thể xóa dữ liệu. Vui lòng xóa dữ liệu liên quan trước.");
            _dtError.Rows.Add(-6, "DELETE statement conflicted with COLUMN REFERENCE constraint", "Dữ liệu đang được sử dụng . Vui lòng xóa những dự liệu liên quan trước. ");
            _dtError.Rows.Add(-7, "which was not supplied", "Thiếu tham số");
            _dtError.Rows.Add(-10, "<Do not define>", "Thông tin không hợp lệ");
            //Con nua....
            _dtError.Rows.Add(-20, "Unable to update the data value: Value could not be converted to System.Byte.", "Dữ liệu phải là kiểu số nguyên");
            _dtError.Rows.Add(-21, "Unable to update the data value: Value could not be converted to System.Decimal.", "Dữ liệu phải là kiểu thập phân");
            _dtError.Rows.Add(-22, "Unable to update the data value: Value could not be converted to System.Int32", "Dữ liệu phải là kiểu số nguyên");
            //Con nua....
            _dtError.Rows.Add(-23, "Could not find stored procedure", "Chưa định nghĩa Store trong DB");
            _dtError.Rows.Add(-24, "String was not recognized as a valid DateTime.", "Dữ liệu phải nhập kiểu " + Functions.GetDateFormat());
            _dtError.Rows.Add(-25, "Unable to update the data value: Value could not be converted to System.Int64", "Dữ liệu phải là kiểu số nguyên");
        }

        public static string GetError(int error)
        {
            string msg = "";
            DataRow[] dr = _dtError.Select("Code = " + error);
            if (dr.Length > 0)
                msg = dr[0]["Description"].ToString();
            return msg;
        }

        /// <summary>
        /// Get row error with a error number. If not have error return a new row
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static DataRow GetRowError(int error)
        {
            DataRow[] dr = _dtError.Select("Code= " + error);
            if (dr.Length > 0)
                return dr[0];
            else
            {
                DataRow drAdd = _dtError.NewRow();
                drAdd["Code"] = error;
                _dtError.Rows.Add(drAdd);
                return drAdd;
            }
        }

        /// <summary>
        /// Modify row error, return error number of current row
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Error"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static int EditError(DataRow Row, string Error, string Description)
        {
            DataRow drMod = null;
            if (Row == null)
                Row = GetRowError(-100);
            if (Row.Table == _dtError)
            {
                drMod = Row;
            }
            else
            {
                drMod = GetRowError(-100);
            }
            if (Row["Code"] == DBNull.Value)
                Row["Code"] = -100;
            Row["SystemName"] = Error;
            Row["Description"] = Description;
            return int.Parse(Row["Code"].ToString());
        }
    }

    public enum Skin
    {
        Caramel,
        Money_Twins,
        Lilian,
        The_Asphalt_World,
        iMaginary,
        Black,
        Blue,
        Office_2007_Blue,
        Office_2007_Black,
        Office_2007_Silver,
        Office_2007_Green,
        Office_2007_Pink,
        Coffee,
        Liquid_Sky,
        London_Liquid_Sky,
        Glass_Oceans,
        Stardust,
        Xmas_2008_Blue,
        Valentine,
        McSkin,
        Summer_2008,
        Pumpkin,
        Dark_Side
    }
}
