using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using GrdCore.BLL;

namespace GrdUI
{
    class CommonFunctions
    {
        public static DataTable _dtDecentralizations = new DataTable();

        #region "FormPermition"
        internal class FormPermission : CommonLib.PermissionUI
        {
            protected override List<CommonLib.Permission> CreatePermission(DataTable dtDecentralizations)
            {
                List<CommonLib.Permission> permission = new List<CommonLib.Permission>();
                foreach (DataRow dr in dtDecentralizations.Rows)
                {
                    permission.Add(new CommonLib.Permission((string)dr["ControlID"], (bool)dr["Enable"], (bool)dr["Visible"], (bool)dr["ReadOnly"]));
                }
                return permission;
            }

            protected override void Init_IgnoreListUser()
            {
                this.IgnoreListUser.Add("ADMIN");
                this.IgnoreListUser.Add("UISTEAM");
            }

            public override bool IgnoreUserPermission
            {
                get
                {
                    return this.IgnoreListUser.Any(user => string.Compare(user, User._UserID, true) == 0);
                }
            }
        }

        static FormPermission _fPermission = new FormPermission();

        #region SetFormPermiss(Control Parent)
        public static void SetFormPermiss(Control Parent)
        {
            try
            {
                if (!CommonLib.ChangeLabelControl.IsChangedFromLoad(Parent))
                    ChangeLabel(Parent);
            }
            catch { }

            try
            {
                if (_fPermission.IgnoreUserPermission) return;
                DataTable tblPermiss = new DataTable();
                if (_dtDecentralizations.Columns.Count == 0)
                    tblPermiss = BL_DecentralizationManagements.GetDecentralizationByGroupIDandFormID(User._UserGroup, Parent.Name);
                else
                {
                    DataView dv = new DataView(_dtDecentralizations);
                    dv.RowFilter = "FormID = '" + Parent.Name + "'";
                    tblPermiss = dv.ToTable(true, new string[] { "ControlID", "Enable", "Visible", "ReadOnly" });
                }
                _fPermission.SetPermission(Parent, tblPermiss);
            }
            catch { }
        }

        #region  ChangeLabel
        static CommonLib.ChangeLabelControl _cLabelControl = null;

        public static void ChangeLabel(Control Parent)
        {
            if (_cLabelControl == null)
                _cLabelControl = new CommonLib.ChangeLabelControl(User.ListLabel);
            if (!CommonLib.ChangeLabelControl.IsChangedFromLoad(Parent))
                _cLabelControl.ChangeLabel(Parent);
        }
        #endregion
        #endregion
        #endregion

        #region RefreshXmlString
        public static string RefreshXmlString(string value)
        {
            try
            {
                if (value.Contains("&"))
                {
                    value = value.Replace("&", "&#038;");
                }
                if (value.Contains(">"))
                {
                    value = value.Replace(">", "&#062;");
                }
                if (value.Contains("<"))
                {
                    value = value.Replace("<", "&#060;");
                }
                if (value.Contains("\""))
                {
                    value = value.Replace("\"", "&#034;");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region Encode and decode
        public static string EncodeString(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                // Key lock
                string key = "PSCGRD";

                //If hashing use get hashcode regards to your key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice

                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray =
                  cTransform.TransformFinalBlock(toEncryptArray, 0,
                  toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor
                tdes.Clear();
                //Return the encrypted data into unreadable string format
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch { return toEncrypt; }
        }

        public static string DecodeString(string cipherString, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                //get the byte code of the string

                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                //Key to open the lock!
                string key = "PSCGRD";

                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //release any resource held by the MD5CryptoServiceProvider

                    hashmd5.Clear();
                }
                else
                {
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor                
                tdes.Clear();
                //return the Clear decrypted TEXT
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch { return cipherString; }
        }

        public static string EncodeMD5(string userName, string password)
        {
            string result = string.Empty;
            try
            {
                result = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("UisStaffID=" + userName.ToUpper() + ";UisPassword=" + password, "MD5");
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }
        #endregion

        #region IsFocusForm
        public static bool IsFocusForm(Type type, Form frmParent, string title)
        {
            int i = 0;
            foreach (Form frm in frmParent.MdiChildren)
            {
                if (frm.GetType() == type)
                {
                    if (frm.Text.Trim() == title.Trim())
                    {
                        if (frm.MinimizeBox)
                        {
                            frm.Focus();
                            frm.WindowState = FormWindowState.Normal;
                        }
                        frm.Focus();
                        return true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            if (i != 0)
                return false;
            return false;
        }

        public static bool IsFocusForm(Type type, Form frmParent)
        {
            int i = 0;
            foreach (Form frm in frmParent.MdiChildren)
            {
                if (frm.GetType() == type)
                {
                    if (frm.MinimizeBox)
                    {
                        frm.Focus();
                        frm.WindowState = FormWindowState.Normal;
                    }
                    frm.Focus();
                    return true;
                }
                else
                {
                    i++;
                }

            }
            if (i != 0)
                return false;
            return false;
        }
        #endregion

        public static DateTime GetDate(string sdate)
        {
            DateTime result = new DateTime();

            try
            {
                string dateFormat = ((DataRow)User._dsDataDictionaries.Tables["SystemConfig"].Select("SettingName = 'FormatDate'").GetValue(0))["SettingStringData"].ToString();
                string[] s = sdate.Split(new char[] { '/' });
                int v0 = int.Parse(s[0]), v1 = int.Parse(s[1]), v2 = int.Parse(s[2]);
                switch (dateFormat)
                {
                    case "dd/MM/yyyy": result = new DateTime(v2, v1, v0); break;
                    case "dd/yyyy/MM": result = new DateTime(v1, v2, v0); break;
                    case "MM/dd/yyyy": result = new DateTime(v2, v0, v1); break;
                    case "MM/yyyy/dd": result = new DateTime(v1, v0, v2); break;
                    case "yyyy/dd/MM": result = new DateTime(v0, v2, v1); break;
                    case "yyyy/MM/dd": result = new DateTime(v0, v1, v2); break;
                }
            }
            catch
            {
                result = DateTime.Now;
            }

            return result;
        }
    }
}
