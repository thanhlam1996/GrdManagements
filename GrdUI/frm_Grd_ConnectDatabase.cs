using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;

namespace GrdUI
{
    public partial class frm_Grd_ConnectDatabase : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        string _provider = "System.Data.SqlClient", _authType = string.Empty;
        #endregion

        #region Inits
        public frm_Grd_ConnectDatabase()
        {
            InitializeComponent();
        }

        private void frm_Grd_ConnectDatabase_Load(object sender, EventArgs e)
        {
            GetProviders();
            lkuProvider.EditValue = "System.Data.SqlClient";

            string pathApp = Application.StartupPath;
            if (System.IO.File.Exists(pathApp + "\\Grd_Config.xml"))
                GetConfig();
        }
        #endregion

        #region Functions
        private void GetConfig()
        {
            try
            {
                string str2;
                string path = "./Grd_Config.xml";
                using (StreamReader reader = new StreamReader(path))
                {
                    str2 = reader.ReadToEnd();
                }
                string[] strArray = str2.Split(new char[] { '"' });

                lkuProvider.EditValue = strArray[3];

                string connect1 = GrdUI.CommonFunctions.DecodeString(strArray[1], true);
                string connect2 = GrdUI.CommonFunctions.DecodeString(strArray[5], true);

                strArray = connect1.Split(new char[] { ';' });

                txtServerName.Text = strArray[0].Split(new char[] { '=' })[1];
                txtDatabase1.Text = strArray[1].Split(new char[] { '=' })[1];

                if (strArray.Length == 5)
                {
                    lkuAuthenticationType.EditValue = "ServerAuth";
                    txtUserName.Text = strArray[3].Split(new char[] { '=' })[1];
                    txtPassword.Text = strArray[4].Split(new char[] { '=' })[1];
                }
                else
                {
                    lkuAuthenticationType.EditValue = "WinAuth";
                    txtUserName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }

                strArray = connect2.Split(new char[] { ';' });
                txtDatabase2.Text = strArray[1].Split(new char[] { '=' })[1];
            }
            catch { }
        }

        private void GetProviders()
        {
            try
            {
                DataTable dtProviders = new DataTable();
                dtProviders.Columns.Add("ID", typeof(string));
                dtProviders.Columns.Add("Name", typeof(string));

                dtProviders.Rows.Add("System.Data.SqlClient", "Microsoft SQL Server");

                lkuProvider.Properties.DataSource = dtProviders;
                lkuProvider.Properties.DisplayMember = "Name";
                lkuProvider.Properties.ValueMember = "ID";

                LookUpColumnInfoCollection coll = lkuProvider.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("Name", 0, "Providers"));

                lkuProvider.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuProvider.Properties.SearchMode = SearchMode.AutoComplete;
                lkuProvider.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }

        private void GetAuthentications()
        {
            try
            {
                DataTable dtAuthentications = new DataTable();
                dtAuthentications.Columns.Add("ID", typeof(string));
                dtAuthentications.Columns.Add("Name", typeof(string));

                dtAuthentications.Rows.Add("WinAuth", "Windows authentication");
                dtAuthentications.Rows.Add("ServerAuth", "Server authentication");

                lkuAuthenticationType.Properties.DataSource = dtAuthentications;
                lkuAuthenticationType.Properties.DisplayMember = "Name";
                lkuAuthenticationType.Properties.ValueMember = "ID";

                LookUpColumnInfoCollection coll = lkuAuthenticationType.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("Name", 0, "Authentications"));

                lkuAuthenticationType.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuAuthenticationType.Properties.SearchMode = SearchMode.AutoComplete;
                lkuAuthenticationType.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }

        private void FinishConfig()
        {
            try
            {
                if (MessageBox.Show("Lưu thông tin cấu hình ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string pathApp = Application.StartupPath;
                    if (!System.IO.File.Exists(pathApp + "\\Grd_Config.xml"))
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(pathApp + "\\Grd_Config.xml", System.IO.FileMode.Create);
                        fs.Close();

                        string strTemp = "<appSettings\r\nConnectionString = \"\"\r\nProviderName = \"\"\r\nConnectionString2 = \"\"/>";
                        using (StreamWriter writer = new StreamWriter(pathApp + "\\Grd_Config.xml"))
                        {
                            writer.Write(strTemp);
                        }
                    }

                    string str2;
                    string[] strArray2;
                    string path = "./Grd_Config.xml";
                    using (StreamReader reader = new StreamReader(path))
                    {
                        str2 = reader.ReadToEnd();
                    }
                    string[] strArray = str2.Split(new char[] { '"' });
                    if (_authType != "WinAuth")
                    {
                        strArray[1] = "Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + txtDatabase1.Text.Trim() + ";Persist Security Info=True;User ID=" + txtUserName.Text.Trim() + "; Password=";
                        if (txtPassword.Text.Trim() == string.Empty)
                        {
                            (strArray2 = strArray)[1] = strArray2[1] + "''";
                        }
                        else
                        {
                            (strArray2 = strArray)[1] = strArray2[1] + txtPassword.Text.Trim();
                        }

                        strArray[5] = "Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + txtDatabase2.Text.Trim() + ";Persist Security Info=True;User ID=" + txtUserName.Text.Trim() + "; Password=";
                        if (txtPassword.Text.Trim() == string.Empty)
                        {
                            (strArray2 = strArray)[5] = strArray2[5] + "''";
                        }
                        else
                        {
                            (strArray2 = strArray)[5] = strArray2[5] + txtPassword.Text.Trim();
                        }
                    }
                    else
                    {
                        strArray[1] = "Server=" + txtServerName.Text.Trim() + ";Database=" + txtDatabase1.Text.Trim() + ";Trusted_Connection=True;";
                        strArray[5] = "Server=" + txtServerName.Text.Trim() + ";Database=" + txtDatabase2.Text.Trim() + ";Trusted_Connection=True;";
                    }
                    strArray[1] = GrdUI.CommonFunctions.EncodeString(strArray[1], true);
                    strArray[5] = GrdUI.CommonFunctions.EncodeString(strArray[5], true);
                    strArray[3] = _provider;

                    string str3 = strArray[0];
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        str3 = str3 + "\"" + strArray[i];
                    }
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.Write(str3);
                    }

                    XtraMessageBox.Show("Cấu hình thành công.", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Events
        private void lkuProvider_EditValueChanged(object sender, EventArgs e)
        {
            _provider = lkuProvider.EditValue.ToString();

            switch (_provider)
            {
                case "System.Data.SqlClient":
                    layoutControlItemAuthType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    GetAuthentications();
                    lkuAuthenticationType.EditValue = "WinAuth";
                    break;
                default:
                    layoutControlItemAuthType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
            }
        }

        private void lkuAuthenticationType_EditValueChanged(object sender, EventArgs e)
        {
            _authType = lkuAuthenticationType.EditValue.ToString();

            if (_provider == "System.Data.SqlClient")
            {
                layoutControlItemUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItemPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;

                if (_authType == "WinAuth")
                {
                    txtUserName.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                }
                else
                {
                    txtUserName.ReadOnly = false;
                    txtPassword.ReadOnly = false;
                }
            }
            else
            {
                layoutControlItemUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FinishConfig();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}