using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GrdUI
{
    public class User
    {
        public static GrdCore.Entities.Staffs _User;

        public static string _UserID;
        public static string _UserPass = string.Empty;
        public static string _UserName = string.Empty;

        public static string _UserGroup = string.Empty;
        public static DataTable _dtUserGroups = new DataTable();

        
        public static DataSet _dsDataDictionaries = new DataSet();

        public static byte[] _CollegeLogo;
        public static int _CollegeID = 0;
        public static string _CollegeName = string.Empty;
        public static string _AdministrativeUnit = string.Empty;
        public static string _CollegeAddress = string.Empty;

        public static string _CurrentYearStudy = string.Empty;
        public static string _CurrentTerm = string.Empty;
        public static string _CurrentGraduateLevelID = string.Empty;
        public static string _CurrentStudyTypeID = string.Empty;

        public static string[] _ParaFromUIS = null;

        public static int _ThuTuSapXepDanhSach = 0;

        private static CommonLib.ChangeUILabelCollection _listLabel = new CommonLib.ChangeUILabelCollection();
        public static CommonLib.ChangeUILabelCollection ListLabel
        {
            get { return User._listLabel; }
        }

        public static bool _foreignLanguage = false;
    }
}
