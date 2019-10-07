using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GrdCore.Entities
{
    public class Staffs
    {
        #region Variables
        private string _staffID = string.Empty;
        private string _lastName = string.Empty;
        private string _middleName = string.Empty;
        private string _firstName = string.Empty;
        private string _staffName = string.Empty;
        private string _passWord = string.Empty;
        private string _Department = string.Empty;
        #endregion

        #region Properties
        public string StaffID
        {
            get { return _staffID; }
            set { _staffID = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string StaffName
        {
            get { return (_lastName.Trim() + " " + _middleName.Trim() + " " + _firstName.Trim()).Trim(); }
            set { _staffName = value; }
        }
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        #endregion

        #region Constructions
        public Staffs()
        {
            _staffID = "";
            _lastName = "";
            _middleName = "";
            _firstName = "";
            _passWord = "";
        }
        #endregion
    }
}
