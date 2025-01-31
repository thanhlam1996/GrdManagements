using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Runtime.InteropServices;
using CommonLib.ImportAndExport;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using DevExpress.XtraEditors;

namespace CommonLib
{
    public class ReadDBF
    {
        #region ReadFileSchemaDBF(string path, string filename)
        public static DataTable ReadFileSchemaDBF(string path, string filename)
        {
            System.Globalization.CultureInfo m_CurrentCulture = null;
            m_CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DataTable dtType;
            //tao ket noi voi Visual FoxPro, phai install driver cho FoxPro vao he thong            
            OleDbConnection Connection = new OleDbConnection("Provider=vfpoledb;Data Source=" + path + ";");
            Connection.Open();
            OleDbCommand Command = Connection.CreateCommand();
            Command.CommandText = @"SELECT * FROM " + filename;
            using (OleDbDataReader reader = Command.ExecuteReader())
            {
                dtType = reader.GetSchemaTable();
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
            return dtType;
        }
        #endregion

        #region ReadFileDBF
        public static DataTable ReadFileDBF(string path, string filename)
        {
            System.Globalization.CultureInfo m_CurrentCulture = null;
            m_CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //tao ket noi voi Visual FoxPro, phai install driver cho FoxPro vao he thong            
            OleDbConnection oleConn = new OleDbConnection("Provider=vfpoledb;Data Source=" + path + ";");
            DataTable dt = new DataTable();
            try
            {
                OleDbCommand command = new OleDbCommand("select * from [" + filename + "]", oleConn);
                oleConn.Open();
                OleDbDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                command.Dispose();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                if (oleConn.State != ConnectionState.Closed)
                    oleConn.Close();
                System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
                throw ex;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
            return dt;
        }


        //public static DataTable ReadFileDBF(string path, string filename, ConvertFont FromFont,ConvertFont ToFont)
        //{
        //    System.Globalization.CultureInfo m_CurrentCulture = null;
        //    m_CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    //tao ket noi voi Visual FoxPro, phai install driver cho FoxPro vao he thong            
        //    OleDbConnection oleConn = new OleDbConnection("Provider=vfpoledb;Data Source=" + path + ";");
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        OleDbCommand command = new OleDbCommand("select * from [" + filename + "]", oleConn);
        //        oleConn.Open();
        //        OleDbDataReader reader = command.ExecuteReader();
        //        dt.Load(reader);
        //        command.Dispose();
        //        oleConn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (oleConn.State != ConnectionState.Closed)
        //            oleConn.Close();
        //        System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        //        throw ex;
        //    }
        //    System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        //    return dt;
        //}
        #endregion

        #region CreateMapConstruct
        public static DataTable CreateMapConstruct()
        {
            DataTable dtMap = new DataTable();
            dtMap.Columns.Add("ColumnDataMap", typeof(string));
            dtMap.Columns.Add("ColumnDBF", typeof(string));
            return dtMap;
        }
        #endregion

        #region ReadFileDBFFromConstruct
        public static void ReadFileDBFFromConstruct(string path, string filename, DataTable MapColumn, ref DataTable ConsData)
        {
            System.Globalization.CultureInfo m_CurrentCulture = null;
            m_CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            OleDbConnection oleConn = new OleDbConnection("Provider=vfpoledb;Data Source=" + path + ";");
            int currentConvertRow = 0;
            try
            {
                string sqlCol = "";
                foreach (DataRow dr in MapColumn.Rows)
                {
                    sqlCol += (sqlCol == "" ? "" : ",") + dr["ColumnDBF"] + " as " + dr["ColumnDataMap"];
                }
                OleDbCommand command = new OleDbCommand("select " + sqlCol + " from [" + filename + "]", oleConn);
                oleConn.Open();
                OleDbDataReader reader = command.ExecuteReader();
                DataTable dtRead = new DataTable();
                dtRead.Load(reader);
                foreach (DataRow dr in dtRead.Rows)
                {
                    currentConvertRow++;
                    ConsData.ImportRow(dr);
                }
                oleConn.Close();
            }
            catch (Exception ex)
            {
                if (oleConn.State != ConnectionState.Closed)
                    oleConn.Close();
                System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
                string Error = "\r\n Row: [" + currentConvertRow + "]";
                throw ex;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        }
        #endregion

        public static void Export(DataTable dtData)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dtData.Copy());
                Export(ds);
            }
            catch { }
        }

        public static void Export(DataSet dsData)
        {
            frmStatus fSta = new frmStatus();
            frmQConvertFontImport frmConvert = new frmQConvertFontImport();

            bool error = false;
            try
            {
                string sameFile = "";
                int timeSameFile = 0;
                frmMapColumn frmM = null;
                bool NotShowMapColumnAgain = false;
                foreach (DataTable dtData in dsData.Tables)
                {
                    string[] s = null;
                    if (timeSameFile < 3)
                    {
                        OpenFileDialog sfdFiles = new OpenFileDialog();
                        sfdFiles.Title = "Chon File de xuat du lieu: " + dtData.TableName;
                        sfdFiles.Filter = "FoxPro file|*.dbf";
                        if (!(sfdFiles.ShowDialog() == DialogResult.OK && sfdFiles.FileName != string.Empty)) return;
                        sameFile = sfdFiles.FileName;
                        timeSameFile++;
                    }
                    s = sameFile.Split('\\');
                    s = s[s.Length - 1].Split('.');

                    DataTable dtDBF = CommonLib.ReadDBF.ReadFileSchemaDBF(sameFile, s[0]);
                    DataTable dtInput = new DataTable();
                    foreach (DataRow dr in dtDBF.Rows)
                    {
                        dtInput.Columns.Add(dr["ColumnName"].ToString(), Type.GetType(dr["DataType"].ToString()));
                    }

                    OleDbConnect conDBF = new OleDbConnect(String.Format("Provider=VFPOLEDB.1;Data Source={0};", sameFile));
                    try
                    {
                        if (!NotShowMapColumnAgain || frmM == null)
                        {
                            frmM = new frmMapColumn();
                            frmM.chkNotShowAgain.Visible = true;
                            frmM.LoadData(dtInput, dtData, null);

                            if (frmM.ShowDialog() != DialogResult.OK)
                                break;
                        }

                        if (!NotShowMapColumnAgain)
                            NotShowMapColumnAgain = frmM.chkNotShowAgain.Checked;

                        int i = 0, count = dtData.Rows.Count;
                        ConvertFont fCon = new ConvertFont();
                        FontIndex conv = FontIndex.iNotKnown;
                        frmConvert.IsImport = false;
                        if (!frmConvert.chkDefault.Checked)
                        {
                            if (frmConvert.ShowDialog() == DialogResult.OK)
                            {
                                if (frmConvert.cboChangefont.SelectedIndex == 0)
                                    conv = FontIndex.iTCV;
                                else if (frmConvert.cboChangefont.SelectedIndex == 1)
                                    conv = FontIndex.iVNI;
                            }
                        }
                        int timeCount = 0;
                        string sqlError = "";

                        foreach (DataRow dr in dtData.Rows)
                        {
                            string insCol = "";
                            string insVal = "";

                            foreach (DataColumn colInput in dtInput.Columns)
                            {

                                DataRow[] drMapCol = frmM.dtMap.Select("InputColumn='" + colInput.ColumnName + "'");
                                if (drMapCol.Length > 0)
                                {

                                    string mapCol = drMapCol[0]["MapColumn"].ToString().Trim();

                                    if (mapCol != "" && dr[mapCol] != DBNull.Value)
                                    {
                                        insCol += (insCol == "" ? "" : ",") + colInput.ColumnName;

                                        if (colInput.DataType == typeof(string))
                                        {
                                            string ss = dr[mapCol].ToString().Trim();
                                            if (conv != FontIndex.iNotKnown)
                                            {
                                                fCon.Convert(ref ss, FontIndex.iUNI, conv);
                                            }
                                            insVal += (insVal == "" ? "" : ",") + "'" + ss + "'";
                                        }
                                        else if (colInput.DataType == typeof(DateTime))
                                        {
                                            insVal += (insVal == "" ? "" : ",") + "{^" + CommonLib.Functions.ConvertDateToStringSQL((DateTime)dr[mapCol], "yyyy-MM-dd HH:mm:ss") + "}";
                                        }
                                        else if (colInput.DataType == typeof(bool))
                                        {
                                            string val = "1";
                                            if (dr[mapCol].ToString() == false.ToString())
                                                val = "0";
                                            insVal += (insVal == "" ? "" : ",") + (dr[colInput.ColumnName] == DBNull.Value ? "null" : val);
                                        }
                                        else
                                        {
                                            insVal += (insVal == "" ? "" : ",") + dr[mapCol];
                                        }
                                    }
                                }
                            }

                            fSta.UpdateStatus("Đang lưu dữ liệu (" + ((int)(i * 100 / count)) + "%)");
                            try
                            {
                                string sql = "insert into " + s[0] + "(" + insCol + ") values(" + insVal + ")";
                                int result = conDBF.ExcuteSQLNotCloseConnect(sql);
                                if (result < 0)
                                {
                                    if (timeCount < 3)
                                    {
                                        if (XtraMessageBox.Show("Có lỗi ở mẫu tin " + (i + 1), "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            error = true;
                                            break;
                                        }
                                        else
                                            timeCount++;
                                    }
                                    sqlError += "\n" + sql;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (timeCount < 3)
                                {
                                    if (XtraMessageBox.Show("Có lỗi ở mẫu tin " + (i + 1) + ": " + ex.Message, "UIS - Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        error = true;
                                        break;
                                    }
                                    else
                                        timeCount++;
                                }
                                sqlError += "\n" + sqlError;
                            }
                            i++;
                        }
                        if (timeCount > 0)
                        {
                            frmMessageString frmMess = new frmMessageString();
                            frmMess.txtValue.Text = sqlError;
                            frmMess.Text = "Error!";
                            frmMess.ShowDialog();
                        }
                    }
                    catch { }

                    conDBF.CloseConnection();
                }
            }
            catch
            {
                error = true;
            }
            fSta.Hide();
            if (!error)
                XtraMessageBox.Show("Xuất FoxPro thành công", "UIS - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ConvertFont
    {
        private string[,] Code;
        private const int iCP1258 = 4;
        private const int iNCR = 0;
        private const int iNOSIGN = 7;
        private const int iTCV = 2;
        private const int iUNI = 6;
        private const int iUTF = 1;
        private const int iUTH = 5;
        private const int iVIQ = 8;
        private const int iVNI = 3;
        private FontCase m_CharCase = FontCase.Normal;
        private const int nCode = 8;

        public ConvertFont()
        {
            this.InitData();
        }

        public bool Convert(ref string strConv, FontIndex iSource, FontIndex iDestination)
        {
            int num = (int)iSource;
            int num2 = (int)iDestination;
            if (strConv.Trim() == "")
            {
                return false;
            }
            if (num == num2)
            {
                return false;
            }
            string str = "";
            string str2 = "";
            if (num == -1)
            {
                int num3 = (int)this.getCode(strConv);
                if (num3 <= -1)
                {
                    return false;
                }
                num = num3;
            }
            if (num2 == -1)
            {
                num2 = 0;
            }
            int nChar = this.GetnChar((FontIndex)int.Parse(this.Code[0, num]));
            int num5 = (nChar > 1) ? (nChar - 1) : 1;
            string str3 = "";
            bool flag = false;
            strConv = strConv + "       ";
            int startIndex = 0;
            while (startIndex < (strConv.Length - 7))
            {
                for (int i = nChar; i >= num5; i--)
                {
                    str2 = "";
                    if (strConv.Substring(startIndex, 1) == " ")
                    {
                        str = " ";
                        break;
                    }
                    str = strConv.Substring(startIndex, i);
                    for (int j = 1; j < 0x87; j++)
                    {
                        if (str == this.Code[j, num])
                        {
                            if ((this.m_CharCase == FontCase.UpperCase) && (j < 0x44))
                            {
                                str2 = this.Code[j + 0x43, num2];
                            }
                            else if ((this.m_CharCase == FontCase.LowerCase) && (j >= 0x44))
                            {
                                str2 = this.Code[j - 0x43, num2];
                            }
                            else
                            {
                                str2 = this.Code[j, num2];
                            }
                            startIndex += i;
                            break;
                        }
                    }
                    if ((str2 != "") || (i == 5))
                    {
                        break;
                    }
                }
                if (str2 != "")
                {
                    str3 = str3 + str2;
                    flag = true;
                }
                else
                {
                    if (this.m_CharCase == FontCase.UpperCase)
                    {
                        str3 = str3 + str.Substring(0, 1).ToUpper();
                    }
                    else if (this.m_CharCase == FontCase.LowerCase)
                    {
                        str3 = str3 + str.Substring(0, 1).ToLower();
                    }
                    else
                    {
                        str3 = str3 + str.Substring(0, 1);
                    }
                    startIndex++;
                }
            }
            if (!flag)
            {
                strConv = strConv.Remove(strConv.Length - 7, 7);
                if (this.m_CharCase == FontCase.UpperCase)
                {
                    strConv = strConv.ToUpper();
                    return true;
                }
                if (this.m_CharCase == FontCase.LowerCase)
                {
                    strConv = strConv.ToLower();
                    return true;
                }
                return false;
            }
            strConv = str3.TrimEnd(new char[0]);
            return true;
        }

        private FontIndex getCode(string str)
        {
            if (str.Trim() == "")
            {
                return FontIndex.iNotKnown;
            }
            int num = -1;
            string s = "";
            str = str + "       ";
            int startIndex = 0;
            while (startIndex < (str.Length - 7))
            {
                if (str.Substring(startIndex, 1) == " ")
                {
                    startIndex++;
                }
                else
                {
                    for (int i = 7; i > 0; i--)
                    {
                        s = str.Substring(startIndex, i);
                        for (int j = 0; j < 7; j++)
                        {
                            if (((i == 4) || (i == 5)) || ((i >= 6) && (j != 0)))
                            {
                                break;
                            }
                            if (((i != 3) || (j == 1)) && ((((j != 3) && (j != 2)) && ((j != 5) && (j != 4))) || (i <= 2)))
                            {
                                for (int k = 1; k < 0x87; k++)
                                {
                                    if (s == this.Code[k, j])
                                    {
                                        if (!this.isSpecialChar(s, (j == 5) || (j == 6)))
                                        {
                                            return (FontIndex)int.Parse(this.Code[0, j]);
                                        }
                                        num = j;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    startIndex++;
                }
            }
            return ((num >= 0) ? ((FontIndex)int.Parse(this.Code[0, num])) : FontIndex.iNotKnown);
        }

        private int getIntCode(string code)
        {
            for (int i = 0; i < 8; i++)
            {
                if (this.Code[0, i] == code)
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetnChar(FontIndex index)
        {
            if (index == FontIndex.iUTF)
            {
                return 3;
            }
            if (((index == FontIndex.iUNI) || (index == FontIndex.iUTH)) || (index == FontIndex.iNOSIGN))
            {
                return 1;
            }
            if (index == FontIndex.iNCR)
            {
                return 7;
            }
            return 2;
        }

        private void InitData()
        {
            this.Code = new string[0x87, 8];
            this.Code[0, 0] = 0.ToString();
            this.Code[0, 1] = 1.ToString();
            this.Code[0, 2] = 2.ToString();
            this.Code[0, 3] = 3.ToString();
            this.Code[0, 5] = 5.ToString();
            this.Code[0, 6] = 6.ToString();
            this.Code[0, 4] = 4.ToString();
            this.Code[0, 7] = 7.ToString();
            this.MapUnicode();
            this.MapVNI();
            this.MapTCV();
            this.MapUTH();
            this.MapUTF8();
            this.MapNCR();
            this.MapNoSign();
            this.MapCP1258();
        }

        private bool isSpecialChar(string s)
        {
            return this.isSpecialChar(s, false);
        }

        private bool isSpecialChar(string s, bool isUNI)
        {
            if (s.Length <= 2)
            {
                string[] strArray3;
                string[] strArray = new string[] { 
                    "\x00ed", "\x00ec", "\x00f3", "\x00f2", "\x00f4", "\x00f1", "\x00ee", "\x00ca", "\x00c8", "\x00c9", "\x00e1", "\x00e0", "\x00e2", "\x00e8", "\x00e9", "\x00ea", 
                    "\x00f9", "\x00fd", "\x00fa", "\x00f6", "\x00cd", "\x00cc", "\x00d3", "\x00d2", "\x00d4", "\x00d1", "\x00ce", "\x00d5", "\x00dd", "\x00c3", "o\x00e0", "o\x00e1", 
                    "o\x00e3", "u\x00fb", "O\x00c1", "O\x00c0", "O\x00c3"
                 };
                string[] strArray2 = new string[] { 
                    "ă", "\x00e2", "\x00ea", "\x00f4", "ơ", "ư", "đ", "\x00ed", "\x00ec", "\x00f3", "\x00f2", "\x00f4", "\x00f1", "\x00ee", "\x00ca", "\x00c8", 
                    "\x00c9", "\x00e1", "\x00e0", "\x00e2", "\x00e8", "\x00e9", "\x00ea", "\x00f9", "\x00fd", "\x00fa", "\x00f6", "\x00cd", "\x00cc", "\x00d3", "\x00d2", "\x00d4", 
                    "\x00d1", "\x00ce", "\x00d5", "\x00dd", "\x00c3", "o\x00e0", "o\x00e1", "o\x00e3", "u\x00fb", "O\x00c1", "O\x00c0", "O\x00c3"
                 };
                if (!isUNI)
                {
                    strArray3 = strArray;
                }
                else
                {
                    strArray3 = strArray2;
                }
                for (int i = 0; i < strArray3.Length; i++)
                {
                    if (string.Compare(s, strArray3[i], true) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isVietnamese(string s)
        {
            return (this.getCode(s) != FontIndex.iNotKnown);
        }

        public bool isVietnamese(string s, ref FontIndex code)
        {
            code = this.getCode(s);
            return (code > FontIndex.iNotKnown);
        }

        private void MapBKHCM1()
        {
            this.Code[1, 6] = "\x00be";
            this.Code[2, 6] = "\x00bf";
            this.Code[3, 6] = "\x00c0";
            this.Code[4, 6] = "\x00c1";
            this.Code[5, 6] = "\x00c2";
            this.Code[6, 6] = "\x00d7";
            this.Code[7, 6] = "\x00d8";
            this.Code[8, 6] = "\x00d9";
            this.Code[9, 6] = "\x00da";
            this.Code[10, 6] = "\x00db";
            this.Code[11, 6] = "\x00dc";
            this.Code[12, 6] = "\x00dd";
            this.Code[13, 6] = "\x00de";
            this.Code[14, 6] = "\x00df";
            this.Code[15, 6] = "\x00e0";
            this.Code[0x10, 6] = "\x00e1";
            this.Code[0x11, 6] = "\x00e2";
            this.Code[0x12, 6] = "\x00c3";
            this.Code[0x13, 6] = "\x00c4";
            this.Code[20, 6] = "\x00c5";
            this.Code[0x15, 6] = "\x00c6";
            this.Code[0x16, 6] = "\x00c7";
            this.Code[0x17, 6] = "\x00e3";
            this.Code[0x18, 6] = "\x00e4";
            this.Code[0x19, 6] = "\x00e5";
            this.Code[0x1a, 6] = "\x00e6";
            this.Code[0x1b, 6] = "\x00e7";
            this.Code[0x1c, 6] = "\x00e8";
            this.Code[0x1d, 6] = "\x00c8";
            this.Code[30, 6] = "\x00c9";
            this.Code[0x1f, 6] = "\x00ca";
            this.Code[0x20, 6] = "\x00cb";
            this.Code[0x21, 6] = "\x00cc";
            this.Code[0x22, 6] = "\x00cd";
            this.Code[0x23, 6] = "\x00ce";
            this.Code[0x24, 6] = "\x00cf";
            this.Code[0x25, 6] = "\x00d0";
            this.Code[0x26, 6] = "\x00d1";
            this.Code[0x27, 6] = "\x00e9";
            this.Code[40, 6] = "\x00ea";
            this.Code[0x29, 6] = "\x00eb";
            this.Code[0x2a, 6] = "\x00ec";
            this.Code[0x2b, 6] = "\x00ed";
            this.Code[0x2c, 6] = "\x00ee";
            this.Code[0x2d, 6] = "\x00ef";
            this.Code[0x2e, 6] = "\x00f0";
            this.Code[0x2f, 6] = "\x00f1";
            this.Code[0x30, 6] = "\x00f2";
            this.Code[0x31, 6] = "\x00f3";
            this.Code[50, 6] = "\x00f4";
            this.Code[0x33, 6] = "\x00d2";
            this.Code[0x34, 6] = "\x00d3";
            this.Code[0x35, 6] = "\x00d4";
            this.Code[0x36, 6] = "\x00d5";
            this.Code[0x37, 6] = "\x00d6";
            this.Code[0x38, 6] = "\x00f5";
            this.Code[0x39, 6] = "\x00f6";
            this.Code[0x3a, 6] = "\x00f7";
            this.Code[0x3b, 6] = "\x00f8";
            this.Code[60, 6] = "\x00f9";
            this.Code[0x3d, 6] = "\x00fa";
            this.Code[0x3e, 6] = "\x00fb";
            this.Code[0x3f, 6] = "\x00fc";
            this.Code[0x40, 6] = "\x00fd";
            this.Code[0x41, 6] = "\x00fe";
            this.Code[0x42, 6] = "\x00ff";
            this.Code[0x43, 6] = "\x00bd";
            this.Code[0x44, 6] = "€";
            this.Code[0x45, 6] = "\x0081";
            this.Code[70, 6] = "‚";
            this.Code[0x47, 6] = "ƒ";
            this.Code[0x48, 6] = "„";
            this.Code[0x49, 6] = "™";
            this.Code[0x4a, 6] = "š";
            this.Code[0x4b, 6] = "›";
            this.Code[0x4c, 6] = "œ";
            this.Code[0x4d, 6] = "\x009d";
            this.Code[0x4e, 6] = "˜";
            this.Code[0x4f, 6] = "Ÿ";
            this.Code[80, 6] = "~";
            this.Code[0x51, 6] = "\x00a1";
            this.Code[0x52, 6] = "\x00a2";
            this.Code[0x53, 6] = "\x00a3";
            this.Code[0x54, 6] = "\x00a4";
            this.Code[0x55, 6] = "…";
            this.Code[0x56, 6] = "†";
            this.Code[0x57, 6] = "‡";
            this.Code[0x58, 6] = "ˆ";
            this.Code[0x59, 6] = "‰";
            this.Code[90, 6] = "\x00a5";
            this.Code[0x5b, 6] = "\x00a6";
            this.Code[0x5c, 6] = "\x00a7";
            this.Code[0x5d, 6] = "\x00a8";
            this.Code[0x5e, 6] = "\x00a9";
            this.Code[0x5f, 6] = "\x00aa";
            this.Code[0x60, 6] = "Š";
            this.Code[0x61, 6] = "‹";
            this.Code[0x62, 6] = "Œ";
            this.Code[0x63, 6] = "\x008d";
            this.Code[100, 6] = "Ž";
            this.Code[0x65, 6] = "\x008f";
            this.Code[0x66, 6] = "\x0090";
            this.Code[0x67, 6] = "‘";
            this.Code[0x68, 6] = "’";
            this.Code[0x69, 6] = "“";
            this.Code[0x6a, 6] = "\x00ab";
            this.Code[0x6b, 6] = "\x00ac";
            this.Code[0x6c, 6] = "\x00ad";
            this.Code[0x6d, 6] = "\x00ae";
            this.Code[110, 6] = "\x00af";
            this.Code[0x6f, 6] = "\x00b0";
            this.Code[0x70, 6] = "\x00b1";
            this.Code[0x71, 6] = "\x00b2";
            this.Code[0x72, 6] = "\x00b3";
            this.Code[0x73, 6] = "\x00b4";
            this.Code[0x74, 6] = "\x00b5";
            this.Code[0x75, 6] = "\x00b6";
            this.Code[0x76, 6] = "”";
            this.Code[0x77, 6] = "•";
            this.Code[120, 6] = "–";
            this.Code[0x79, 6] = "—";
            this.Code[0x7a, 6] = "˜";
            this.Code[0x7b, 6] = "\x00b7";
            this.Code[0x7c, 6] = "\x00b8";
            this.Code[0x7d, 6] = "\x00b9";
            this.Code[0x7e, 6] = "\x00ba";
            this.Code[0x7f, 6] = "\x00bb";
            this.Code[0x80, 6] = "\x00bc";
            this.Code[0x81, 6] = "{";
            this.Code[130, 6] = "^";
            this.Code[0x83, 6] = "`";
            this.Code[0x84, 6] = "|";
            this.Code[0x85, 6] = "Ž";
            this.Code[0x86, 6] = "}";
        }

        private void MapBKHCM2()
        {
            this.Code[1, 6] = "a\x00e1";
            this.Code[2, 6] = "a\x00e2";
            this.Code[3, 6] = "a\x00e3";
            this.Code[4, 6] = "a\x00e4";
            this.Code[5, 6] = "a\x00e5";
            this.Code[6, 6] = "\x00f9";
            this.Code[7, 6] = "\x00f9\x00e6";
            this.Code[8, 6] = "\x00f9\x00e7";
            this.Code[9, 6] = "\x00f9\x00e8";
            this.Code[10, 6] = "\x00f9\x00e9";
            this.Code[11, 6] = "\x00f9\x00e5";
            this.Code[12, 6] = "\x00ea";
            this.Code[13, 6] = "\x00ea\x00eb";
            this.Code[14, 6] = "\x00ea\x00ec";
            this.Code[15, 6] = "\x00ea\x00ed";
            this.Code[0x10, 6] = "\x00ea\x00ee";
            this.Code[0x11, 6] = "\x00ea\x00e5";
            this.Code[0x12, 6] = "e\x00e1";
            this.Code[0x13, 6] = "e\x00e2";
            this.Code[20, 6] = "e\x00e3";
            this.Code[0x15, 6] = "e\x00e4";
            this.Code[0x16, 6] = "e\x00e5";
            this.Code[0x17, 6] = "\x00ef";
            this.Code[0x18, 6] = "\x00ef\x00eb";
            this.Code[0x19, 6] = "\x00ef\x00ec";
            this.Code[0x1a, 6] = "\x00ef\x00ed";
            this.Code[0x1b, 6] = "\x00ef\x00ee";
            this.Code[0x1c, 6] = "\x00ef\x00e5";
            this.Code[0x1d, 6] = "\x00f1";
            this.Code[30, 6] = "\x00f2";
            this.Code[0x1f, 6] = "\x00f3";
            this.Code[0x20, 6] = "\x00f4";
            this.Code[0x21, 6] = "\x00f5";
            this.Code[0x22, 6] = "o\x00e1";
            this.Code[0x23, 6] = "o\x00e2";
            this.Code[0x24, 6] = "o\x00e3";
            this.Code[0x25, 6] = "o\x00e4";
            this.Code[0x26, 6] = "o\x00e5";
            this.Code[0x27, 6] = "\x00f6";
            this.Code[40, 6] = "\x00f6\x00eb";
            this.Code[0x29, 6] = "\x00f6\x00ec";
            this.Code[0x2a, 6] = "\x00f6\x00ed";
            this.Code[0x2b, 6] = "\x00f6\x00ee";
            this.Code[0x2c, 6] = "\x00f6\x00e5";
            this.Code[0x2d, 6] = "\x00fa";
            this.Code[0x2e, 6] = "\x00fa\x00e1";
            this.Code[0x2f, 6] = "\x00fa\x00e2";
            this.Code[0x30, 6] = "\x00fa\x00e3";
            this.Code[0x31, 6] = "\x00fa\x00e4";
            this.Code[50, 6] = "\x00fa\x00e5";
            this.Code[0x33, 6] = "u\x00e1";
            this.Code[0x34, 6] = "u\x00e2";
            this.Code[0x35, 6] = "u\x00e3";
            this.Code[0x36, 6] = "u\x00e4";
            this.Code[0x37, 6] = "u\x00e5";
            this.Code[0x38, 6] = "\x00fb";
            this.Code[0x39, 6] = "\x00fb\x00e1";
            this.Code[0x3a, 6] = "\x00fb\x00e2";
            this.Code[0x3b, 6] = "\x00fb\x00e3";
            this.Code[60, 6] = "\x00fb\x00e4";
            this.Code[0x3d, 6] = "\x00fb\x00e5";
            this.Code[0x3e, 6] = "y\x00e1";
            this.Code[0x3f, 6] = "y\x00e2";
            this.Code[0x40, 6] = "y\x00e3";
            this.Code[0x41, 6] = "y\x00e4";
            this.Code[0x42, 6] = "y\x00e5";
            this.Code[0x43, 6] = "\x00e0";
            this.Code[0x44, 6] = "A\x00c1";
            this.Code[0x45, 6] = "A\x00c2";
            this.Code[70, 6] = "A\x00c3";
            this.Code[0x47, 6] = "A\x00c4";
            this.Code[0x48, 6] = "A\x00c5";
            this.Code[0x49, 6] = "\x00d9";
            this.Code[0x4a, 6] = "\x00d9\x00c6";
            this.Code[0x4b, 6] = "\x00d9\x00c7";
            this.Code[0x4c, 6] = "\x00d9\x00c8";
            this.Code[0x4d, 6] = "\x00d9\x00c9";
            this.Code[0x4e, 6] = "\x00d9\x00c5";
            this.Code[0x4f, 6] = "\x00ca";
            this.Code[80, 6] = "\x00ca\x00cb";
            this.Code[0x51, 6] = "\x00ca\x00cc";
            this.Code[0x52, 6] = "\x00ca\x00cd";
            this.Code[0x53, 6] = "\x00ca\x00ce";
            this.Code[0x54, 6] = "\x00ca\x00c5";
            this.Code[0x55, 6] = "E\x00c1";
            this.Code[0x56, 6] = "E\x00c2";
            this.Code[0x57, 6] = "E\x00c3";
            this.Code[0x58, 6] = "E\x00c4";
            this.Code[0x59, 6] = "E\x00c5";
            this.Code[90, 6] = "\x00cf";
            this.Code[0x5b, 6] = "\x00cf\x00cb";
            this.Code[0x5c, 6] = "\x00cf\x00cc";
            this.Code[0x5d, 6] = "\x00cf\x00cd";
            this.Code[0x5e, 6] = "\x00cf\x00ce";
            this.Code[0x5f, 6] = "\x00cf\x00e5";
            this.Code[0x60, 6] = "\x00d1";
            this.Code[0x61, 6] = "\x00d2";
            this.Code[0x62, 6] = "\x00d3";
            this.Code[0x63, 6] = "\x00d4";
            this.Code[100, 6] = "\x00d5";
            this.Code[0x65, 6] = "O\x00c1";
            this.Code[0x66, 6] = "O\x00c2";
            this.Code[0x67, 6] = "O\x00c3";
            this.Code[0x68, 6] = "O\x00c4";
            this.Code[0x69, 6] = "O\x00c5";
            this.Code[0x6a, 6] = "\x00d6";
            this.Code[0x6b, 6] = "\x00d6\x00cb";
            this.Code[0x6c, 6] = "\x00d6\x00cc";
            this.Code[0x6d, 6] = "\x00d6\x00cd";
            this.Code[110, 6] = "\x00d6\x00ce";
            this.Code[0x6f, 6] = "\x00d6\x00c5";
            this.Code[0x70, 6] = "\x00da";
            this.Code[0x71, 6] = "\x00da\x00c1";
            this.Code[0x72, 6] = "\x00da\x00c2";
            this.Code[0x73, 6] = "\x00da\x00c3";
            this.Code[0x74, 6] = "\x00da\x00c4";
            this.Code[0x75, 6] = "\x00da\x00c5";
            this.Code[0x76, 6] = "U\x00c1";
            this.Code[0x77, 6] = "U\x00c2";
            this.Code[120, 6] = "U\x00c3";
            this.Code[0x79, 6] = "U\x00c4";
            this.Code[0x7a, 6] = "U\x00c5";
            this.Code[0x7b, 6] = "\x00db";
            this.Code[0x7c, 6] = "\x00db\x00c1";
            this.Code[0x7d, 6] = "\x00db\x00c2";
            this.Code[0x7e, 6] = "\x00db\x00c3";
            this.Code[0x7f, 6] = "\x00db\x00c4";
            this.Code[0x80, 6] = "\x00db\x00c5";
            this.Code[0x81, 6] = "Y\x00c1";
            this.Code[130, 6] = "Y\x00c2";
            this.Code[0x83, 6] = "Y\x00c3";
            this.Code[0x84, 6] = "Y\x00c4";
            this.Code[0x85, 6] = "Y\x00c5";
            this.Code[0x86, 6] = "\x00c0";
        }

        private void MapCP1258()
        {
            this.Code[1, 4] = "a\x00ec";
            this.Code[2, 4] = "a\x00cc";
            this.Code[3, 4] = "a\x00d2";
            this.Code[4, 4] = "a\x00de";
            this.Code[5, 4] = "a\x00f2";
            this.Code[6, 4] = "\x00e3";
            this.Code[7, 4] = "\x00e3\x00ec";
            this.Code[8, 4] = "\x00e3\x00cc";
            this.Code[9, 4] = "\x00e3\x00d2";
            this.Code[10, 4] = "\x00e3\x00de";
            this.Code[11, 4] = "\x00e3\x00f2";
            this.Code[12, 4] = "\x00e2";
            this.Code[13, 4] = "\x00e2\x00ec";
            this.Code[14, 4] = "\x00e2\x00cc";
            this.Code[15, 4] = "\x00e2\x00d2";
            this.Code[0x10, 4] = "\x00e2\x00de";
            this.Code[0x11, 4] = "\x00e2\x00f2";
            this.Code[0x12, 4] = "e\x00ec";
            this.Code[0x13, 4] = "e\x00cc";
            this.Code[20, 4] = "e\x00d2";
            this.Code[0x15, 4] = "e\x00de";
            this.Code[0x16, 4] = "e\x00f2";
            this.Code[0x17, 4] = "\x00ea";
            this.Code[0x18, 4] = "\x00ea\x00ec";
            this.Code[0x19, 4] = "\x00ea\x00cc";
            this.Code[0x1a, 4] = "\x00ea\x00d2";
            this.Code[0x1b, 4] = "\x00ea\x00de";
            this.Code[0x1c, 4] = "\x00ea\x00f2";
            this.Code[0x1d, 4] = "i\x00ec";
            this.Code[30, 4] = "i\x00cc";
            this.Code[0x1f, 4] = "i\x00d2";
            this.Code[0x20, 4] = "i\x00de";
            this.Code[0x21, 4] = "i\x00f2";
            this.Code[0x22, 4] = "o\x00ec";
            this.Code[0x23, 4] = "o\x00cc";
            this.Code[0x24, 4] = "o\x00d2";
            this.Code[0x25, 4] = "o\x00de";
            this.Code[0x26, 4] = "o\x00f2";
            this.Code[0x27, 4] = "\x00f4";
            this.Code[40, 4] = "\x00f4\x00ec";
            this.Code[0x29, 4] = "\x00f4\x00cc";
            this.Code[0x2a, 4] = "\x00f4\x00d2";
            this.Code[0x2b, 4] = "\x00f4\x00de";
            this.Code[0x2c, 4] = "\x00f4\x00f2";
            this.Code[0x2d, 4] = "\x00f5";
            this.Code[0x2e, 4] = "\x00f5\x00ec";
            this.Code[0x2f, 4] = "\x00f5\x00cc";
            this.Code[0x30, 4] = "\x00f5\x00d2";
            this.Code[0x31, 4] = "\x00f5\x00de";
            this.Code[50, 4] = "\x00f5\x00f2";
            this.Code[0x33, 4] = "u\x00ec";
            this.Code[0x34, 4] = "u\x00cc";
            this.Code[0x35, 4] = "u\x00d2";
            this.Code[0x36, 4] = "u\x00de";
            this.Code[0x37, 4] = "u\x00f2";
            this.Code[0x38, 4] = "\x00fd";
            this.Code[0x39, 4] = "\x00fd\x00ec";
            this.Code[0x3a, 4] = "\x00fd\x00cc";
            this.Code[0x3b, 4] = "\x00fd\x00d2";
            this.Code[60, 4] = "\x00fd\x00de";
            this.Code[0x3d, 4] = "\x00fd\x00f2";
            this.Code[0x3e, 4] = "y\x00ec";
            this.Code[0x3f, 4] = "y\x00cc";
            this.Code[0x40, 4] = "y\x00d2";
            this.Code[0x41, 4] = "y\x00de";
            this.Code[0x42, 4] = "y\x00f2";
            this.Code[0x43, 4] = "\x00f0";
            this.Code[0x44, 4] = "A\x00ec";
            this.Code[0x45, 4] = "A\x00cc";
            this.Code[70, 4] = "A\x00d2";
            this.Code[0x47, 4] = "A\x00de";
            this.Code[0x48, 4] = "A\x00f2";
            this.Code[0x49, 4] = "\x00c3";
            this.Code[0x4a, 4] = "\x00c3\x00ec";
            this.Code[0x4b, 4] = "\x00c3\x00cc";
            this.Code[0x4c, 4] = "\x00c3\x00d2";
            this.Code[0x4d, 4] = "\x00c3\x00de";
            this.Code[0x4e, 4] = "\x00c3\x00f2";
            this.Code[0x4f, 4] = "\x00c2";
            this.Code[80, 4] = "\x00c2\x00ec";
            this.Code[0x51, 4] = "\x00c2\x00cc";
            this.Code[0x52, 4] = "\x00c2\x00d2";
            this.Code[0x53, 4] = "\x00c2\x00de";
            this.Code[0x54, 4] = "\x00c2\x00f2";
            this.Code[0x55, 4] = "E\x00ec";
            this.Code[0x56, 4] = "E\x00cc";
            this.Code[0x57, 4] = "E\x00d2";
            this.Code[0x58, 4] = "E\x00de";
            this.Code[0x59, 4] = "E\x00f2";
            this.Code[90, 4] = "\x00ca";
            this.Code[0x5b, 4] = "\x00ca\x00ec";
            this.Code[0x5c, 4] = "\x00ca\x00cc";
            this.Code[0x5d, 4] = "\x00ca\x00d2";
            this.Code[0x5e, 4] = "\x00ca\x00de";
            this.Code[0x5f, 4] = "\x00ca\x00f2";
            this.Code[0x60, 4] = "I\x00ec";
            this.Code[0x61, 4] = "I\x00cc";
            this.Code[0x62, 4] = "I\x00d2";
            this.Code[0x63, 4] = "I\x00de";
            this.Code[100, 4] = "I\x00f2";
            this.Code[0x65, 4] = "O\x00ec";
            this.Code[0x66, 4] = "O\x00cc";
            this.Code[0x67, 4] = "O\x00d2";
            this.Code[0x68, 4] = "O\x00de";
            this.Code[0x69, 4] = "O\x00f2";
            this.Code[0x6a, 4] = "\x00d4";
            this.Code[0x6b, 4] = "\x00d4\x00ec";
            this.Code[0x6c, 4] = "\x00d4\x00cc";
            this.Code[0x6d, 4] = "\x00d4\x00d2";
            this.Code[110, 4] = "\x00d4\x00de";
            this.Code[0x6f, 4] = "\x00d4\x00f2";
            this.Code[0x70, 4] = "\x00d5";
            this.Code[0x71, 4] = "\x00d5\x00ec";
            this.Code[0x72, 4] = "\x00d5\x00cc";
            this.Code[0x73, 4] = "\x00d5\x00d2";
            this.Code[0x74, 4] = "\x00d5\x00de";
            this.Code[0x75, 4] = "\x00d5\x00f2";
            this.Code[0x76, 4] = "U\x00ec";
            this.Code[0x77, 4] = "U\x00cc";
            this.Code[120, 4] = "U\x00d2";
            this.Code[0x79, 4] = "U\x00de";
            this.Code[0x7a, 4] = "U\x00f2";
            this.Code[0x7b, 4] = "\x00dd";
            this.Code[0x7c, 4] = "\x00dd\x00ec";
            this.Code[0x7d, 4] = "\x00dd\x00cc";
            this.Code[0x7e, 4] = "\x00dd\x00d2";
            this.Code[0x7f, 4] = "\x00dd\x00de";
            this.Code[0x80, 4] = "\x00dd\x00f2";
            this.Code[0x81, 4] = "Y\x00ec";
            this.Code[130, 4] = "Y\x00cc";
            this.Code[0x83, 4] = "Y\x00d2";
            this.Code[0x84, 4] = "Y\x00de";
            this.Code[0x85, 4] = "Y\x00f2";
            this.Code[0x86, 4] = "\x00d0";
        }

        private void MapCString()
        {
            this.Code[1, 6] = "\x00e1";
            this.Code[2, 6] = "\x00e0";
            this.Code[3, 6] = "ả";
            this.Code[4, 6] = "\x00e3";
            this.Code[5, 6] = "ạ";
            this.Code[6, 6] = "ă";
            this.Code[7, 6] = "ắ";
            this.Code[8, 6] = "ằ";
            this.Code[9, 6] = "ẳ";
            this.Code[10, 6] = "ẵ";
            this.Code[11, 6] = "ặ";
            this.Code[12, 6] = "\x00e2";
            this.Code[13, 6] = "ấ";
            this.Code[14, 6] = "ầ";
            this.Code[15, 6] = "ẩ";
            this.Code[0x10, 6] = "ẫ";
            this.Code[0x11, 6] = "ậ";
            this.Code[0x12, 6] = "\x00e9";
            this.Code[0x13, 6] = "\x00e8";
            this.Code[20, 6] = "ẻ";
            this.Code[0x15, 6] = "ẽ";
            this.Code[0x16, 6] = "ẹ";
            this.Code[0x17, 6] = "\x00ea";
            this.Code[0x18, 6] = "ế";
            this.Code[0x19, 6] = "ề";
            this.Code[0x1a, 6] = "ể";
            this.Code[0x1b, 6] = "ễ";
            this.Code[0x1c, 6] = "ệ";
            this.Code[0x1d, 6] = "\x00ed";
            this.Code[30, 6] = "\x00ec";
            this.Code[0x1f, 6] = "ỉ";
            this.Code[0x20, 6] = "ĩ";
            this.Code[0x21, 6] = "ị";
            this.Code[0x22, 6] = "\x00f3";
            this.Code[0x23, 6] = "\x00f2";
            this.Code[0x24, 6] = "ỏ";
            this.Code[0x25, 6] = "\x00f5";
            this.Code[0x26, 6] = "ọ";
            this.Code[0x27, 6] = "\x00f4";
            this.Code[40, 6] = "ố";
            this.Code[0x29, 6] = "ồ";
            this.Code[0x2a, 6] = "ổ";
            this.Code[0x2b, 6] = "ỗ";
            this.Code[0x2c, 6] = "ộ";
            this.Code[0x2d, 6] = "ơ";
            this.Code[0x2e, 6] = "ớ";
            this.Code[0x2f, 6] = "ờ";
            this.Code[0x30, 6] = "ở";
            this.Code[0x31, 6] = "ỡ";
            this.Code[50, 6] = "ợ";
            this.Code[0x33, 6] = "\x00fa";
            this.Code[0x34, 6] = "\x00f9";
            this.Code[0x35, 6] = "ủ";
            this.Code[0x36, 6] = "ũ";
            this.Code[0x37, 6] = "ụ";
            this.Code[0x38, 6] = "ư";
            this.Code[0x39, 6] = "ứ";
            this.Code[0x3a, 6] = "ừ";
            this.Code[0x3b, 6] = "ử";
            this.Code[60, 6] = "ữ";
            this.Code[0x3d, 6] = "ự";
            this.Code[0x3e, 6] = "\x00fd";
            this.Code[0x3f, 6] = "ỳ";
            this.Code[0x40, 6] = "ỷ";
            this.Code[0x41, 6] = "ỹ";
            this.Code[0x42, 6] = "ỵ";
            this.Code[0x43, 6] = "đ";
            this.Code[0x44, 6] = "\x00c1";
            this.Code[0x45, 6] = "\x00c0";
            this.Code[70, 6] = "Ả";
            this.Code[0x47, 6] = "\x00c3";
            this.Code[0x48, 6] = "Ạ";
            this.Code[0x49, 6] = "Ă";
            this.Code[0x4a, 6] = "Ắ";
            this.Code[0x4b, 6] = "Ằ";
            this.Code[0x4c, 6] = "Ẳ";
            this.Code[0x4d, 6] = "Ẵ";
            this.Code[0x4e, 6] = "Ặ";
            this.Code[0x4f, 6] = "\x00c2";
            this.Code[80, 6] = "Ấ";
            this.Code[0x51, 6] = "Ầ";
            this.Code[0x52, 6] = "Ẩ";
            this.Code[0x53, 6] = "Ẫ";
            this.Code[0x54, 6] = "Ậ";
            this.Code[0x55, 6] = "\x00c9";
            this.Code[0x56, 6] = "\x00c8";
            this.Code[0x57, 6] = "Ẻ";
            this.Code[0x58, 6] = "Ẽ";
            this.Code[0x59, 6] = "Ẹ";
            this.Code[90, 6] = "\x00ca";
            this.Code[0x5b, 6] = "Ế";
            this.Code[0x5c, 6] = "Ề";
            this.Code[0x5d, 6] = "Ể";
            this.Code[0x5e, 6] = "Ễ";
            this.Code[0x5f, 6] = "Ệ";
            this.Code[0x60, 6] = "\x00cd";
            this.Code[0x61, 6] = "\x00cc";
            this.Code[0x62, 6] = "Ỉ";
            this.Code[0x63, 6] = "Ĩ";
            this.Code[100, 6] = "Ị";
            this.Code[0x65, 6] = "\x00d3";
            this.Code[0x66, 6] = "\x00d2";
            this.Code[0x67, 6] = "Ỏ";
            this.Code[0x68, 6] = "\x00d5";
            this.Code[0x69, 6] = "Ọ";
            this.Code[0x6a, 6] = "\x00d4";
            this.Code[0x6b, 6] = "Ố";
            this.Code[0x6c, 6] = "Ồ";
            this.Code[0x6d, 6] = "Ổ";
            this.Code[110, 6] = "Ỗ";
            this.Code[0x6f, 6] = "Ộ";
            this.Code[0x70, 6] = "Ơ";
            this.Code[0x71, 6] = "Ớ";
            this.Code[0x72, 6] = "Ờ";
            this.Code[0x73, 6] = "Ở";
            this.Code[0x74, 6] = "Ỡ";
            this.Code[0x75, 6] = "Ợ";
            this.Code[0x76, 6] = "\x00da";
            this.Code[0x77, 6] = "\x00d9";
            this.Code[120, 6] = "Ủ";
            this.Code[0x79, 6] = "Ũ";
            this.Code[0x7a, 6] = "Ụ";
            this.Code[0x7b, 6] = "Ư";
            this.Code[0x7c, 6] = "Ứ";
            this.Code[0x7d, 6] = "Ừ";
            this.Code[0x7e, 6] = "Ử";
            this.Code[0x7f, 6] = "Ữ";
            this.Code[0x80, 6] = "Ự";
            this.Code[0x81, 6] = "\x00dd";
            this.Code[130, 6] = "Ỳ";
            this.Code[0x83, 6] = "Ỷ";
            this.Code[0x84, 6] = "Ỹ";
            this.Code[0x85, 6] = "Ỵ";
            this.Code[0x86, 6] = "Đ";
        }

        private void MapNCR()
        {
            this.Code[1, 0] = "&#225;";
            this.Code[2, 0] = "&#224;";
            this.Code[3, 0] = "&#7843;";
            this.Code[4, 0] = "&#227;";
            this.Code[5, 0] = "&#7841;";
            this.Code[6, 0] = "&#259;";
            this.Code[7, 0] = "&#7855;";
            this.Code[8, 0] = "&#7857;";
            this.Code[9, 0] = "&#7859;";
            this.Code[10, 0] = "&#7861;";
            this.Code[11, 0] = "&#7863;";
            this.Code[12, 0] = "&#226;";
            this.Code[13, 0] = "&#7845;";
            this.Code[14, 0] = "&#7847;";
            this.Code[15, 0] = "&#7849;";
            this.Code[0x10, 0] = "&#7851;";
            this.Code[0x11, 0] = "&#7853;";
            this.Code[0x12, 0] = "&#233;";
            this.Code[0x13, 0] = "&#232;";
            this.Code[20, 0] = "&#7867;";
            this.Code[0x15, 0] = "&#7869;";
            this.Code[0x16, 0] = "&#7865;";
            this.Code[0x17, 0] = "&#234;";
            this.Code[0x18, 0] = "&#7871;";
            this.Code[0x19, 0] = "&#7873;";
            this.Code[0x1a, 0] = "&#7875;";
            this.Code[0x1b, 0] = "&#7877;";
            this.Code[0x1c, 0] = "&#7879;";
            this.Code[0x1d, 0] = "&#237;";
            this.Code[30, 0] = "&#236;";
            this.Code[0x1f, 0] = "&#7881;";
            this.Code[0x20, 0] = "&#297;";
            this.Code[0x21, 0] = "&#7883;";
            this.Code[0x22, 0] = "&#243;";
            this.Code[0x23, 0] = "&#242;";
            this.Code[0x24, 0] = "&#7887;";
            this.Code[0x25, 0] = "&#245;";
            this.Code[0x26, 0] = "&#7885;";
            this.Code[0x27, 0] = "&#244;";
            this.Code[40, 0] = "&#7889;";
            this.Code[0x29, 0] = "&#7891;";
            this.Code[0x2a, 0] = "&#7893;";
            this.Code[0x2b, 0] = "&#7895;";
            this.Code[0x2c, 0] = "&#7897;";
            this.Code[0x2d, 0] = "&#417;";
            this.Code[0x2e, 0] = "&#7899;";
            this.Code[0x2f, 0] = "&#7901;";
            this.Code[0x30, 0] = "&#7903;";
            this.Code[0x31, 0] = "&#7905;";
            this.Code[50, 0] = "&#7907;";
            this.Code[0x33, 0] = "&#250;";
            this.Code[0x34, 0] = "&#249;";
            this.Code[0x35, 0] = "&#7911;";
            this.Code[0x36, 0] = "&#361;";
            this.Code[0x37, 0] = "&#7909;";
            this.Code[0x38, 0] = "&#432;";
            this.Code[0x39, 0] = "&#7913;";
            this.Code[0x3a, 0] = "&#7915;";
            this.Code[0x3b, 0] = "&#7917;";
            this.Code[60, 0] = "&#7919;";
            this.Code[0x3d, 0] = "&#7921;";
            this.Code[0x3e, 0] = "&#253;";
            this.Code[0x3f, 0] = "&#7923;";
            this.Code[0x40, 0] = "&#7927;";
            this.Code[0x41, 0] = "&#7929;";
            this.Code[0x42, 0] = "&#7925;";
            this.Code[0x43, 0] = "&#273;";
            this.Code[0x44, 0] = "&#193;";
            this.Code[0x45, 0] = "&#192;";
            this.Code[70, 0] = "&#7842;";
            this.Code[0x47, 0] = "&#195;";
            this.Code[0x48, 0] = "&#7840;";
            this.Code[0x49, 0] = "&#258;";
            this.Code[0x4a, 0] = "&#7854;";
            this.Code[0x4b, 0] = "&#7856;";
            this.Code[0x4c, 0] = "&#7858;";
            this.Code[0x4d, 0] = "&#7860;";
            this.Code[0x4e, 0] = "&#7862;";
            this.Code[0x4f, 0] = "&#194;";
            this.Code[80, 0] = "&#7844;";
            this.Code[0x51, 0] = "&#7846;";
            this.Code[0x52, 0] = "&#7848;";
            this.Code[0x53, 0] = "&#7850;";
            this.Code[0x54, 0] = "&#7852;";
            this.Code[0x55, 0] = "&#201;";
            this.Code[0x56, 0] = "&#200;";
            this.Code[0x57, 0] = "&#7866;";
            this.Code[0x58, 0] = "&#7868;";
            this.Code[0x59, 0] = "&#7864;";
            this.Code[90, 0] = "&#202;";
            this.Code[0x5b, 0] = "&#7870;";
            this.Code[0x5c, 0] = "&#7872;";
            this.Code[0x5d, 0] = "&#7874;";
            this.Code[0x5e, 0] = "&#7876;";
            this.Code[0x5f, 0] = "&#7878;";
            this.Code[0x60, 0] = "&#205;";
            this.Code[0x61, 0] = "&#204;";
            this.Code[0x62, 0] = "&#7880;";
            this.Code[0x63, 0] = "&#296;";
            this.Code[100, 0] = "&#7882;";
            this.Code[0x65, 0] = "&#211;";
            this.Code[0x66, 0] = "&#210;";
            this.Code[0x67, 0] = "&#7886;";
            this.Code[0x68, 0] = "&#213;";
            this.Code[0x69, 0] = "&#7884;";
            this.Code[0x6a, 0] = "&#212;";
            this.Code[0x6b, 0] = "&#7888;";
            this.Code[0x6c, 0] = "&#7890;";
            this.Code[0x6d, 0] = "&#7892;";
            this.Code[110, 0] = "&#7894;";
            this.Code[0x6f, 0] = "&#7896;";
            this.Code[0x70, 0] = "&#416;";
            this.Code[0x71, 0] = "&#7898;";
            this.Code[0x72, 0] = "&#7900;";
            this.Code[0x73, 0] = "&#7902;";
            this.Code[0x74, 0] = "&#7904;";
            this.Code[0x75, 0] = "&#7906;";
            this.Code[0x76, 0] = "&#218;";
            this.Code[0x77, 0] = "&#217;";
            this.Code[120, 0] = "&#7910;";
            this.Code[0x79, 0] = "&#360;";
            this.Code[0x7a, 0] = "&#7908;";
            this.Code[0x7b, 0] = "&#431;";
            this.Code[0x7c, 0] = "&#7912;";
            this.Code[0x7d, 0] = "&#7914;";
            this.Code[0x7e, 0] = "&#7916;";
            this.Code[0x7f, 0] = "&#7918;";
            this.Code[0x80, 0] = "&#7920;";
            this.Code[0x81, 0] = "&#221;";
            this.Code[130, 0] = "&#7922;";
            this.Code[0x83, 0] = "&#7926;";
            this.Code[0x84, 0] = "&#7928;";
            this.Code[0x85, 0] = "&#7924;";
            this.Code[0x86, 0] = "&#272;";
        }

        private void MapNCRHex()
        {
            this.Code[1, 6] = "\x00e1";
            this.Code[2, 6] = "\x00e0";
            this.Code[3, 6] = "&#x1EA3;";
            this.Code[4, 6] = "\x00e3";
            this.Code[5, 6] = "&#x1EA1;";
            this.Code[6, 6] = "&#x103;";
            this.Code[7, 6] = "&#x1EAF;";
            this.Code[8, 6] = "&#x1EB1;";
            this.Code[9, 6] = "&#x1EB3;";
            this.Code[10, 6] = "&#x1EB5;";
            this.Code[11, 6] = "&#x1EB7;";
            this.Code[12, 6] = "\x00e2";
            this.Code[13, 6] = "&#x1EA5;";
            this.Code[14, 6] = "&#x1EA7;";
            this.Code[15, 6] = "&#x1EA9;";
            this.Code[0x10, 6] = "&#x1EAB;";
            this.Code[0x11, 6] = "&#x1EAD;";
            this.Code[0x12, 6] = "\x00e9";
            this.Code[0x13, 6] = "\x00e8";
            this.Code[20, 6] = "&#x1EBB;";
            this.Code[0x15, 6] = "&#x1EBD;";
            this.Code[0x16, 6] = "&#x1EB9;";
            this.Code[0x17, 6] = "\x00ea";
            this.Code[0x18, 6] = "&#x1EBF;";
            this.Code[0x19, 6] = "&#x1EC1;";
            this.Code[0x1a, 6] = "&#x1EC3;";
            this.Code[0x1b, 6] = "&#x1EC5;";
            this.Code[0x1c, 6] = "&#x1EC7;";
            this.Code[0x1d, 6] = "\x00ed";
            this.Code[30, 6] = "\x00ec";
            this.Code[0x1f, 6] = "&#x1EC9;";
            this.Code[0x20, 6] = "&#x129;";
            this.Code[0x21, 6] = "&#x1ECB;";
            this.Code[0x22, 6] = "\x00f3";
            this.Code[0x23, 6] = "\x00f2";
            this.Code[0x24, 6] = "&#x1ECF;";
            this.Code[0x25, 6] = "\x00f5";
            this.Code[0x26, 6] = "&#x1ECD;";
            this.Code[0x27, 6] = "\x00f4";
            this.Code[40, 6] = "&#x1ED1;";
            this.Code[0x29, 6] = "&#x1ED3;";
            this.Code[0x2a, 6] = "&#x1ED5;";
            this.Code[0x2b, 6] = "&#x1ED7;";
            this.Code[0x2c, 6] = "&#x1ED9;";
            this.Code[0x2d, 6] = "&#x1A1;";
            this.Code[0x2e, 6] = "&#x1EDB;";
            this.Code[0x2f, 6] = "&#x1EDD;";
            this.Code[0x30, 6] = "&#x1EDF;";
            this.Code[0x31, 6] = "&#x1EE1;";
            this.Code[50, 6] = "&#x1EE3;";
            this.Code[0x33, 6] = "\x00fa";
            this.Code[0x34, 6] = "\x00f9";
            this.Code[0x35, 6] = "&#x1EE7;";
            this.Code[0x36, 6] = "&#x169;";
            this.Code[0x37, 6] = "&#x1EE5;";
            this.Code[0x38, 6] = "&#x1B0;";
            this.Code[0x39, 6] = "&#x1EE9;";
            this.Code[0x3a, 6] = "&#x1EEB;";
            this.Code[0x3b, 6] = "&#x1EED;";
            this.Code[60, 6] = "&#x1EEF;";
            this.Code[0x3d, 6] = "&#x1EF1;";
            this.Code[0x3e, 6] = "\x00fd";
            this.Code[0x3f, 6] = "&#x1EF3;";
            this.Code[0x40, 6] = "&#x1EF7;";
            this.Code[0x41, 6] = "&#x1EF9;";
            this.Code[0x42, 6] = "&#x1EF5;";
            this.Code[0x43, 6] = "&#x111;";
            this.Code[0x44, 6] = "\x00c1";
            this.Code[0x45, 6] = "\x00c0";
            this.Code[70, 6] = "&#x1EA2;";
            this.Code[0x47, 6] = "\x00c3";
            this.Code[0x48, 6] = "&#x1EA0;";
            this.Code[0x49, 6] = "&#x102;";
            this.Code[0x4a, 6] = "&#x1EAE;";
            this.Code[0x4b, 6] = "&#x1EB0;";
            this.Code[0x4c, 6] = "&#x1EB2;";
            this.Code[0x4d, 6] = "&#x1EB4;";
            this.Code[0x4e, 6] = "&#x1EB6;";
            this.Code[0x4f, 6] = "\x00c2";
            this.Code[80, 6] = "&#x1EA4;";
            this.Code[0x51, 6] = "&#x1EA6;";
            this.Code[0x52, 6] = "&#x1EA8;";
            this.Code[0x53, 6] = "&#x1EAA;";
            this.Code[0x54, 6] = "&#x1EAC;";
            this.Code[0x55, 6] = "\x00c9";
            this.Code[0x56, 6] = "\x00c8";
            this.Code[0x57, 6] = "&#x1EBA;";
            this.Code[0x58, 6] = "&#x1EBC;";
            this.Code[0x59, 6] = "&#x1EB8;";
            this.Code[90, 6] = "\x00ca";
            this.Code[0x5b, 6] = "&#x1EBE;";
            this.Code[0x5c, 6] = "&#x1EC0;";
            this.Code[0x5d, 6] = "&#x1EC2;";
            this.Code[0x5e, 6] = "&#x1EC4;";
            this.Code[0x5f, 6] = "&#x1EC6;";
            this.Code[0x60, 6] = "\x00cd";
            this.Code[0x61, 6] = "\x00cc";
            this.Code[0x62, 6] = "&#x1EC8;";
            this.Code[0x63, 6] = "&#x128;";
            this.Code[100, 6] = "&#x1ECA;";
            this.Code[0x65, 6] = "\x00d3";
            this.Code[0x66, 6] = "\x00d2";
            this.Code[0x67, 6] = "&#x1ECE;";
            this.Code[0x68, 6] = "\x00d5";
            this.Code[0x69, 6] = "&#x1ECC;";
            this.Code[0x6a, 6] = "\x00d4";
            this.Code[0x6b, 6] = "&#x1ED0;";
            this.Code[0x6c, 6] = "&#x1ED2;";
            this.Code[0x6d, 6] = "&#x1ED4;";
            this.Code[110, 6] = "&#x1ED6;";
            this.Code[0x6f, 6] = "&#x1ED8;";
            this.Code[0x70, 6] = "&#x1A0;";
            this.Code[0x71, 6] = "&#x1EDA;";
            this.Code[0x72, 6] = "&#x1EDC;";
            this.Code[0x73, 6] = "&#x1EDE;";
            this.Code[0x74, 6] = "&#x1EE0;";
            this.Code[0x75, 6] = "&#x1EE2;";
            this.Code[0x76, 6] = "\x00da";
            this.Code[0x77, 6] = "\x00d9";
            this.Code[120, 6] = "&#x1EE6;";
            this.Code[0x79, 6] = "&#x168;";
            this.Code[0x7a, 6] = "&#x1EE4;";
            this.Code[0x7b, 6] = "&#x1AF;";
            this.Code[0x7c, 6] = "&#x1EE8;";
            this.Code[0x7d, 6] = "&#x1EEA;";
            this.Code[0x7e, 6] = "&#x1EEC;";
            this.Code[0x7f, 6] = "&#x1EEE;";
            this.Code[0x80, 6] = "&#x1EF0;";
            this.Code[0x81, 6] = "\x00dd";
            this.Code[130, 6] = "&#x1EF2;";
            this.Code[0x83, 6] = "&#x1EF6;";
            this.Code[0x84, 6] = "&#x1EF8;";
            this.Code[0x85, 6] = "&#x1EF4;";
            this.Code[0x86, 6] = "&#x110;";
        }

        private void MapNoSign()
        {
            this.Code[1, 7] = "a";
            this.Code[2, 7] = "a";
            this.Code[3, 7] = "a";
            this.Code[4, 7] = "a";
            this.Code[5, 7] = "a";
            this.Code[6, 7] = "a";
            this.Code[7, 7] = "a";
            this.Code[8, 7] = "a";
            this.Code[9, 7] = "a";
            this.Code[10, 7] = "a";
            this.Code[11, 7] = "a";
            this.Code[12, 7] = "a";
            this.Code[13, 7] = "a";
            this.Code[14, 7] = "a";
            this.Code[15, 7] = "a";
            this.Code[0x10, 7] = "a";
            this.Code[0x11, 7] = "a";
            this.Code[0x12, 7] = "e";
            this.Code[0x13, 7] = "e";
            this.Code[20, 7] = "e";
            this.Code[0x15, 7] = "e";
            this.Code[0x16, 7] = "e";
            this.Code[0x17, 7] = "e";
            this.Code[0x18, 7] = "e";
            this.Code[0x19, 7] = "e";
            this.Code[0x1a, 7] = "e";
            this.Code[0x1b, 7] = "e";
            this.Code[0x1c, 7] = "e";
            this.Code[0x1d, 7] = "i";
            this.Code[30, 7] = "i";
            this.Code[0x1f, 7] = "i";
            this.Code[0x20, 7] = "i";
            this.Code[0x21, 7] = "i";
            this.Code[0x22, 7] = "o";
            this.Code[0x23, 7] = "o";
            this.Code[0x24, 7] = "o";
            this.Code[0x25, 7] = "o";
            this.Code[0x26, 7] = "o";
            this.Code[0x27, 7] = "o";
            this.Code[40, 7] = "o";
            this.Code[0x29, 7] = "o";
            this.Code[0x2a, 7] = "o";
            this.Code[0x2b, 7] = "o";
            this.Code[0x2c, 7] = "o";
            this.Code[0x2d, 7] = "o";
            this.Code[0x2e, 7] = "o";
            this.Code[0x2f, 7] = "o";
            this.Code[0x30, 7] = "o";
            this.Code[0x31, 7] = "o";
            this.Code[50, 7] = "o";
            this.Code[0x33, 7] = "u";
            this.Code[0x34, 7] = "u";
            this.Code[0x35, 7] = "u";
            this.Code[0x36, 7] = "u";
            this.Code[0x37, 7] = "u";
            this.Code[0x38, 7] = "u";
            this.Code[0x39, 7] = "u";
            this.Code[0x3a, 7] = "u";
            this.Code[0x3b, 7] = "u";
            this.Code[60, 7] = "u";
            this.Code[0x3d, 7] = "u";
            this.Code[0x3e, 7] = "y";
            this.Code[0x3f, 7] = "y";
            this.Code[0x40, 7] = "y";
            this.Code[0x41, 7] = "y";
            this.Code[0x42, 7] = "y";
            this.Code[0x43, 7] = "d";
            this.Code[0x44, 7] = "A";
            this.Code[0x45, 7] = "A";
            this.Code[70, 7] = "A";
            this.Code[0x47, 7] = "A";
            this.Code[0x48, 7] = "A";
            this.Code[0x49, 7] = "A";
            this.Code[0x4a, 7] = "A";
            this.Code[0x4b, 7] = "A";
            this.Code[0x4c, 7] = "A";
            this.Code[0x4d, 7] = "A";
            this.Code[0x4e, 7] = "A";
            this.Code[0x4f, 7] = "A";
            this.Code[80, 7] = "A";
            this.Code[0x51, 7] = "A";
            this.Code[0x52, 7] = "A";
            this.Code[0x53, 7] = "A";
            this.Code[0x54, 7] = "A";
            this.Code[0x55, 7] = "E";
            this.Code[0x56, 7] = "E";
            this.Code[0x57, 7] = "E";
            this.Code[0x58, 7] = "E";
            this.Code[0x59, 7] = "E";
            this.Code[90, 7] = "E";
            this.Code[0x5b, 7] = "E";
            this.Code[0x5c, 7] = "E";
            this.Code[0x5d, 7] = "E";
            this.Code[0x5e, 7] = "E";
            this.Code[0x5f, 7] = "E";
            this.Code[0x60, 7] = "I";
            this.Code[0x61, 7] = "I";
            this.Code[0x62, 7] = "I";
            this.Code[0x63, 7] = "I";
            this.Code[100, 7] = "I";
            this.Code[0x65, 7] = "O";
            this.Code[0x66, 7] = "O";
            this.Code[0x67, 7] = "O";
            this.Code[0x68, 7] = "O";
            this.Code[0x69, 7] = "O";
            this.Code[0x6a, 7] = "O";
            this.Code[0x6b, 7] = "O";
            this.Code[0x6c, 7] = "O";
            this.Code[0x6d, 7] = "O";
            this.Code[110, 7] = "O";
            this.Code[0x6f, 7] = "O";
            this.Code[0x70, 7] = "O";
            this.Code[0x71, 7] = "O";
            this.Code[0x72, 7] = "O";
            this.Code[0x73, 7] = "O";
            this.Code[0x74, 7] = "O";
            this.Code[0x75, 7] = "O";
            this.Code[0x76, 7] = "U";
            this.Code[0x77, 7] = "U";
            this.Code[120, 7] = "U";
            this.Code[0x79, 7] = "U";
            this.Code[0x7a, 7] = "U";
            this.Code[0x7b, 7] = "U";
            this.Code[0x7c, 7] = "U";
            this.Code[0x7d, 7] = "U";
            this.Code[0x7e, 7] = "U";
            this.Code[0x7f, 7] = "U";
            this.Code[0x80, 7] = "U";
            this.Code[0x81, 7] = "Y";
            this.Code[130, 7] = "Y";
            this.Code[0x83, 7] = "Y";
            this.Code[0x84, 7] = "Y";
            this.Code[0x85, 7] = "Y";
            this.Code[0x86, 7] = "D";
        }

        private void MapTCV()
        {
            this.Code[1, 2] = "\x00b8";
            this.Code[2, 2] = "\x00b5";
            this.Code[3, 2] = "\x00b6";
            this.Code[4, 2] = "\x00b7";
            this.Code[5, 2] = "\x00b9";
            this.Code[6, 2] = "\x00a8";
            this.Code[7, 2] = "\x00be";
            this.Code[8, 2] = "\x00bb";
            this.Code[9, 2] = "\x00bc";
            this.Code[10, 2] = "\x00bd";
            this.Code[11, 2] = "\x00c6";
            this.Code[12, 2] = "\x00a9";
            this.Code[13, 2] = "\x00ca";
            this.Code[14, 2] = "\x00c7";
            this.Code[15, 2] = "\x00c8";
            this.Code[0x10, 2] = "\x00c9";
            this.Code[0x11, 2] = "\x00cb";
            this.Code[0x12, 2] = "\x00d0";
            this.Code[0x13, 2] = "\x00cc";
            this.Code[20, 2] = "\x00ce";
            this.Code[0x15, 2] = "\x00cf";
            this.Code[0x16, 2] = "\x00d1";
            this.Code[0x17, 2] = "\x00aa";
            this.Code[0x18, 2] = "\x00d5";
            this.Code[0x19, 2] = "\x00d2";
            this.Code[0x1a, 2] = "\x00d3";
            this.Code[0x1b, 2] = "\x00d4";
            this.Code[0x1c, 2] = "\x00d6";
            this.Code[0x1d, 2] = "\x00dd";
            this.Code[30, 2] = "\x00d7";
            this.Code[0x1f, 2] = "\x00d8";
            this.Code[0x20, 2] = "\x00dc";
            this.Code[0x21, 2] = "\x00de";
            this.Code[0x22, 2] = "\x00e3";
            this.Code[0x23, 2] = "\x00df";
            this.Code[0x24, 2] = "\x00e1";
            this.Code[0x25, 2] = "\x00e2";
            this.Code[0x26, 2] = "\x00e4";
            this.Code[0x27, 2] = "\x00ab";
            this.Code[40, 2] = "\x00e8";
            this.Code[0x29, 2] = "\x00e5";
            this.Code[0x2a, 2] = "\x00e6";
            this.Code[0x2b, 2] = "\x00e7";
            this.Code[0x2c, 2] = "\x00e9";
            this.Code[0x2d, 2] = "\x00ac";
            this.Code[0x2e, 2] = "\x00ed";
            this.Code[0x2f, 2] = "\x00ea";
            this.Code[0x30, 2] = "\x00eb";
            this.Code[0x31, 2] = "\x00ec";
            this.Code[50, 2] = "\x00ee";
            this.Code[0x33, 2] = "\x00f3";
            this.Code[0x34, 2] = "\x00ef";
            this.Code[0x35, 2] = "\x00f1";
            this.Code[0x36, 2] = "\x00f2";
            this.Code[0x37, 2] = "\x00f4";
            this.Code[0x38, 2] = "\x00ad";
            this.Code[0x39, 2] = "\x00f8";
            this.Code[0x3a, 2] = "\x00f5";
            this.Code[0x3b, 2] = "\x00f6";
            this.Code[60, 2] = "\x00f7";
            this.Code[0x3d, 2] = "\x00f9";
            this.Code[0x3e, 2] = "\x00fd";
            this.Code[0x3f, 2] = "\x00fa";
            this.Code[0x40, 2] = "\x00fb";
            this.Code[0x41, 2] = "\x00fc";
            this.Code[0x42, 2] = "\x00fe";
            this.Code[0x43, 2] = "\x00ae";
            this.Code[0x44, 2] = "\x00b8";
            this.Code[0x45, 2] = "\x00b5";
            this.Code[70, 2] = "\x00b6";
            this.Code[0x47, 2] = "\x00b7";
            this.Code[0x48, 2] = "\x00b9";
            this.Code[0x49, 2] = "\x00a1";
            this.Code[0x4a, 2] = "\x00be";
            this.Code[0x4b, 2] = "\x00bb";
            this.Code[0x4c, 2] = "\x00bc";
            this.Code[0x4d, 2] = "\x00bd";
            this.Code[0x4e, 2] = "\x00c6";
            this.Code[0x4f, 2] = "\x00a2";
            this.Code[80, 2] = "\x00ca";
            this.Code[0x51, 2] = "\x00c7";
            this.Code[0x52, 2] = "\x00c8";
            this.Code[0x53, 2] = "\x00c9";
            this.Code[0x54, 2] = "\x00cb";
            this.Code[0x55, 2] = "\x00d0";
            this.Code[0x56, 2] = "\x00cc";
            this.Code[0x57, 2] = "\x00ce";
            this.Code[0x58, 2] = "\x00cf";
            this.Code[0x59, 2] = "\x00d1";
            this.Code[90, 2] = "\x00a3";
            this.Code[0x5b, 2] = "\x00d5";
            this.Code[0x5c, 2] = "\x00d2";
            this.Code[0x5d, 2] = "\x00d3";
            this.Code[0x5e, 2] = "\x00d4";
            this.Code[0x5f, 2] = "\x00d6";
            this.Code[0x60, 2] = "\x00dd";
            this.Code[0x61, 2] = "\x00d7";
            this.Code[0x62, 2] = "\x00d8";
            this.Code[0x63, 2] = "\x00dc";
            this.Code[100, 2] = "\x00de";
            this.Code[0x65, 2] = "\x00e3";
            this.Code[0x66, 2] = "\x00df";
            this.Code[0x67, 2] = "\x00e1";
            this.Code[0x68, 2] = "\x00e2";
            this.Code[0x69, 2] = "\x00e4";
            this.Code[0x6a, 2] = "\x00a4";
            this.Code[0x6b, 2] = "\x00e8";
            this.Code[0x6c, 2] = "\x00e5";
            this.Code[0x6d, 2] = "\x00e6";
            this.Code[110, 2] = "\x00e7";
            this.Code[0x6f, 2] = "\x00e9";
            this.Code[0x70, 2] = "\x00a5";
            this.Code[0x71, 2] = "\x00ed";
            this.Code[0x72, 2] = "\x00ea";
            this.Code[0x73, 2] = "\x00eb";
            this.Code[0x74, 2] = "\x00ec";
            this.Code[0x75, 2] = "\x00ee";
            this.Code[0x76, 2] = "\x00f3";
            this.Code[0x77, 2] = "\x00ef";
            this.Code[120, 2] = "\x00f1";
            this.Code[0x79, 2] = "\x00f2";
            this.Code[0x7a, 2] = "\x00f4";
            this.Code[0x7b, 2] = "\x00a6";
            this.Code[0x7c, 2] = "\x00f8";
            this.Code[0x7d, 2] = "\x00f5";
            this.Code[0x7e, 2] = "\x00f6";
            this.Code[0x7f, 2] = "\x00f7";
            this.Code[0x80, 2] = "\x00f9";
            this.Code[0x81, 2] = "\x00fd";
            this.Code[130, 2] = "\x00fa";
            this.Code[0x83, 2] = "\x00fb";
            this.Code[0x84, 2] = "\x00fc";
            this.Code[0x85, 2] = "\x00fe";
            this.Code[0x86, 2] = "\x00a7";
        }

        private void MapUnicode()
        {
            this.Code[1, 6] = "\x00e1";
            this.Code[2, 6] = "\x00e0";
            this.Code[3, 6] = "ả";
            this.Code[4, 6] = "\x00e3";
            this.Code[5, 6] = "ạ";
            this.Code[6, 6] = "ă";
            this.Code[7, 6] = "ắ";
            this.Code[8, 6] = "ằ";
            this.Code[9, 6] = "ẳ";
            this.Code[10, 6] = "ẵ";
            this.Code[11, 6] = "ặ";
            this.Code[12, 6] = "\x00e2";
            this.Code[13, 6] = "ấ";
            this.Code[14, 6] = "ầ";
            this.Code[15, 6] = "ẩ";
            this.Code[0x10, 6] = "ẫ";
            this.Code[0x11, 6] = "ậ";
            this.Code[0x12, 6] = "\x00e9";
            this.Code[0x13, 6] = "\x00e8";
            this.Code[20, 6] = "ẻ";
            this.Code[0x15, 6] = "ẽ";
            this.Code[0x16, 6] = "ẹ";
            this.Code[0x17, 6] = "\x00ea";
            this.Code[0x18, 6] = "ế";
            this.Code[0x19, 6] = "ề";
            this.Code[0x1a, 6] = "ể";
            this.Code[0x1b, 6] = "ễ";
            this.Code[0x1c, 6] = "ệ";
            this.Code[0x1d, 6] = "\x00ed";
            this.Code[30, 6] = "\x00ec";
            this.Code[0x1f, 6] = "ỉ";
            this.Code[0x20, 6] = "ĩ";
            this.Code[0x21, 6] = "ị";
            this.Code[0x22, 6] = "\x00f3";
            this.Code[0x23, 6] = "\x00f2";
            this.Code[0x24, 6] = "ỏ";
            this.Code[0x25, 6] = "\x00f5";
            this.Code[0x26, 6] = "ọ";
            this.Code[0x27, 6] = "\x00f4";
            this.Code[40, 6] = "ố";
            this.Code[0x29, 6] = "ồ";
            this.Code[0x2a, 6] = "ổ";
            this.Code[0x2b, 6] = "ỗ";
            this.Code[0x2c, 6] = "ộ";
            this.Code[0x2d, 6] = "ơ";
            this.Code[0x2e, 6] = "ớ";
            this.Code[0x2f, 6] = "ờ";
            this.Code[0x30, 6] = "ở";
            this.Code[0x31, 6] = "ỡ";
            this.Code[50, 6] = "ợ";
            this.Code[0x33, 6] = "\x00fa";
            this.Code[0x34, 6] = "\x00f9";
            this.Code[0x35, 6] = "ủ";
            this.Code[0x36, 6] = "ũ";
            this.Code[0x37, 6] = "ụ";
            this.Code[0x38, 6] = "ư";
            this.Code[0x39, 6] = "ứ";
            this.Code[0x3a, 6] = "ừ";
            this.Code[0x3b, 6] = "ử";
            this.Code[60, 6] = "ữ";
            this.Code[0x3d, 6] = "ự";
            this.Code[0x3e, 6] = "\x00fd";
            this.Code[0x3f, 6] = "ỳ";
            this.Code[0x40, 6] = "ỷ";
            this.Code[0x41, 6] = "ỹ";
            this.Code[0x42, 6] = "ỵ";
            this.Code[0x43, 6] = "đ";
            this.Code[0x44, 6] = "\x00c1";
            this.Code[0x45, 6] = "\x00c0";
            this.Code[70, 6] = "Ả";
            this.Code[0x47, 6] = "\x00c3";
            this.Code[0x48, 6] = "Ạ";
            this.Code[0x49, 6] = "Ă";
            this.Code[0x4a, 6] = "Ắ";
            this.Code[0x4b, 6] = "Ằ";
            this.Code[0x4c, 6] = "Ẳ";
            this.Code[0x4d, 6] = "Ẵ";
            this.Code[0x4e, 6] = "Ặ";
            this.Code[0x4f, 6] = "\x00c2";
            this.Code[80, 6] = "Ấ";
            this.Code[0x51, 6] = "Ầ";
            this.Code[0x52, 6] = "Ẩ";
            this.Code[0x53, 6] = "Ẫ";
            this.Code[0x54, 6] = "Ậ";
            this.Code[0x55, 6] = "\x00c9";
            this.Code[0x56, 6] = "\x00c8";
            this.Code[0x57, 6] = "Ẻ";
            this.Code[0x58, 6] = "Ẽ";
            this.Code[0x59, 6] = "Ẹ";
            this.Code[90, 6] = "\x00ca";
            this.Code[0x5b, 6] = "Ế";
            this.Code[0x5c, 6] = "Ề";
            this.Code[0x5d, 6] = "Ể";
            this.Code[0x5e, 6] = "Ễ";
            this.Code[0x5f, 6] = "Ệ";
            this.Code[0x60, 6] = "\x00cd";
            this.Code[0x61, 6] = "\x00cc";
            this.Code[0x62, 6] = "Ỉ";
            this.Code[0x63, 6] = "Ĩ";
            this.Code[100, 6] = "Ị";
            this.Code[0x65, 6] = "\x00d3";
            this.Code[0x66, 6] = "\x00d2";
            this.Code[0x67, 6] = "Ỏ";
            this.Code[0x68, 6] = "\x00d5";
            this.Code[0x69, 6] = "Ọ";
            this.Code[0x6a, 6] = "\x00d4";
            this.Code[0x6b, 6] = "Ố";
            this.Code[0x6c, 6] = "Ồ";
            this.Code[0x6d, 6] = "Ổ";
            this.Code[110, 6] = "Ỗ";
            this.Code[0x6f, 6] = "Ộ";
            this.Code[0x70, 6] = "Ơ";
            this.Code[0x71, 6] = "Ớ";
            this.Code[0x72, 6] = "Ờ";
            this.Code[0x73, 6] = "Ở";
            this.Code[0x74, 6] = "Ỡ";
            this.Code[0x75, 6] = "Ợ";
            this.Code[0x76, 6] = "\x00da";
            this.Code[0x77, 6] = "\x00d9";
            this.Code[120, 6] = "Ủ";
            this.Code[0x79, 6] = "Ũ";
            this.Code[0x7a, 6] = "Ụ";
            this.Code[0x7b, 6] = "Ư";
            this.Code[0x7c, 6] = "Ứ";
            this.Code[0x7d, 6] = "Ừ";
            this.Code[0x7e, 6] = "Ử";
            this.Code[0x7f, 6] = "Ữ";
            this.Code[0x80, 6] = "Ự";
            this.Code[0x81, 6] = "\x00dd";
            this.Code[130, 6] = "Ỳ";
            this.Code[0x83, 6] = "Ỷ";
            this.Code[0x84, 6] = "Ỹ";
            this.Code[0x85, 6] = "Ỵ";
            this.Code[0x86, 6] = "Đ";
        }

        private void MapUTF8()
        {
            this.Code[1, 1] = "\x00c3\x00a1";
            this.Code[2, 1] = "\x00c3\x00a0";
            this.Code[3, 1] = "\x00e1\x00ba\x00a3";
            this.Code[4, 1] = "\x00c3\x00a3";
            this.Code[5, 1] = "\x00e1\x00ba\x00a1";
            this.Code[6, 1] = "\x00c4ƒ";
            this.Code[7, 1] = "\x00e1\x00ba\x00af";
            this.Code[8, 1] = "\x00e1\x00ba\x00b1";
            this.Code[9, 1] = "\x00e1\x00ba\x00b3";
            this.Code[10, 1] = "\x00e1\x00ba\x00b5";
            this.Code[11, 1] = "\x00e1\x00ba\x00b7";
            this.Code[12, 1] = "\x00c3\x00a2";
            this.Code[13, 1] = "\x00e1\x00ba\x00a5";
            this.Code[14, 1] = "\x00e1\x00ba\x00a7";
            this.Code[15, 1] = "\x00e1\x00ba\x00a9";
            this.Code[0x10, 1] = "\x00e1\x00ba\x00ab";
            this.Code[0x11, 1] = "\x00e1\x00ba\x00ad";
            this.Code[0x12, 1] = "\x00c3\x00a9";
            this.Code[0x13, 1] = "\x00c3\x00a8";
            this.Code[20, 1] = "\x00e1\x00ba\x00bb";
            this.Code[0x15, 1] = "\x00e1\x00ba\x00bd";
            this.Code[0x16, 1] = "\x00e1\x00ba\x00b9";
            this.Code[0x17, 1] = "\x00c3\x00aa";
            this.Code[0x18, 1] = "\x00e1\x00ba\x00bf";
            this.Code[0x19, 1] = "\x00e1\x00bb\x0081";
            this.Code[0x1a, 1] = "\x00e1\x00bbƒ";
            this.Code[0x1b, 1] = "\x00e1\x00bb…";
            this.Code[0x1c, 1] = "\x00e1\x00bb‡";
            this.Code[0x1d, 1] = "\x00c3\x00ad";
            this.Code[30, 1] = "\x00c3\x00ac";
            this.Code[0x1f, 1] = "\x00e1\x00bb‰";
            this.Code[0x20, 1] = "\x00c4\x00a9";
            this.Code[0x21, 1] = "\x00e1\x00bb‹";
            this.Code[0x22, 1] = "\x00c3\x00b3";
            this.Code[0x23, 1] = "\x00c3\x00b2";
            this.Code[0x24, 1] = "\x00e1\x00bb\x008f";
            this.Code[0x25, 1] = "\x00c3\x00b5";
            this.Code[0x26, 1] = "\x00e1\x00bb\x008d";
            this.Code[0x27, 1] = "\x00c3\x00b4";
            this.Code[40, 1] = "\x00e1\x00bb‘";
            this.Code[0x29, 1] = "\x00e1\x00bb“";
            this.Code[0x2a, 1] = "\x00e1\x00bb•";
            this.Code[0x2b, 1] = "\x00e1\x00bb—";
            this.Code[0x2c, 1] = "\x00e1\x00bb™";
            this.Code[0x2d, 1] = "\x00c6\x00a1";
            this.Code[0x2e, 1] = "\x00e1\x00bb›";
            this.Code[0x2f, 1] = "\x00e1\x00bb\x009d";
            this.Code[0x30, 1] = "\x00e1\x00bbŸ";
            this.Code[0x31, 1] = "\x00e1\x00bb\x00a1";
            this.Code[50, 1] = "\x00e1\x00bb\x00a3";
            this.Code[0x33, 1] = "\x00c3\x00ba";
            this.Code[0x34, 1] = "\x00c3\x00b9";
            this.Code[0x35, 1] = "\x00e1\x00bb\x00a7";
            this.Code[0x36, 1] = "\x00c5\x00a9";
            this.Code[0x37, 1] = "\x00e1\x00bb\x00a5";
            this.Code[0x38, 1] = "\x00c6\x00b0";
            this.Code[0x39, 1] = "\x00e1\x00bb\x00a9";
            this.Code[0x3a, 1] = "\x00e1\x00bb\x00ab";
            this.Code[0x3b, 1] = "\x00e1\x00bb\x00ad";
            this.Code[60, 1] = "\x00e1\x00bb\x00af";
            this.Code[0x3d, 1] = "\x00e1\x00bb\x00b1";
            this.Code[0x3e, 1] = "\x00c3\x00bd";
            this.Code[0x3f, 1] = "\x00e1\x00bb\x00b3";
            this.Code[0x40, 1] = "\x009d\x00e1\x00bb\x00b7".Substring(1);
            this.Code[0x41, 1] = "\x00e1\x00bb\x00b9";
            this.Code[0x42, 1] = "\x00e1\x00bb\x00b5";
            this.Code[0x43, 1] = "\x00c4‘";
            this.Code[0x44, 1] = "\x00c3\x0081";
            this.Code[0x45, 1] = "\x00c3€";
            this.Code[70, 1] = "\x00e1\x00ba\x00a2";
            this.Code[0x47, 1] = "\x00c3ƒ";
            this.Code[0x48, 1] = "\x00e1\x00ba ";
            this.Code[0x49, 1] = "\x00c4‚";
            this.Code[0x4a, 1] = "\x00e1\x00ba\x00ae";
            this.Code[0x4b, 1] = "\x00e1\x00ba\x00b0";
            this.Code[0x4c, 1] = "\x00e1\x00ba\x00b2";
            this.Code[0x4d, 1] = "\x00e1\x00ba\x00b4";
            this.Code[0x4e, 1] = "\x00e1\x00ba\x00b6";
            this.Code[0x4f, 1] = "\x00c3‚";
            this.Code[80, 1] = "\x00e1\x00ba\x00a4";
            this.Code[0x51, 1] = "\x00e1\x00ba\x00a6";
            this.Code[0x52, 1] = "\x00e1\x00ba\x00a8";
            this.Code[0x53, 1] = "\x00e1\x00ba\x00aa";
            this.Code[0x54, 1] = "\x00e1\x00ba\x00ac";
            this.Code[0x55, 1] = "\x00c3‰";
            this.Code[0x56, 1] = "\x00c3ˆ";
            this.Code[0x57, 1] = "\x00e1\x00ba\x00ba";
            this.Code[0x58, 1] = "\x00e1\x00ba\x00bc";
            this.Code[0x59, 1] = "\x00e1\x00ba\x00b8";
            this.Code[90, 1] = "\x00c3Š";
            this.Code[0x5b, 1] = "\x00e1\x00ba\x00be";
            this.Code[0x5c, 1] = "\x00e1\x00bb€";
            this.Code[0x5d, 1] = "\x00e1\x00bb‚";
            this.Code[0x5e, 1] = "\x00e1\x00bb„";
            this.Code[0x5f, 1] = "\x00e1\x00bb†";
            this.Code[0x60, 1] = "\x00c3\x008d";
            this.Code[0x61, 1] = "\x00c3Œ";
            this.Code[0x62, 1] = "\x00e1\x00bbˆ";
            this.Code[0x63, 1] = "\x00c4\x00a8";
            this.Code[100, 1] = "\x00e1\x00bbŠ";
            this.Code[0x65, 1] = "\x00c3“";
            this.Code[0x66, 1] = "\x00c3’";
            this.Code[0x67, 1] = "\x00e1\x00bbŽ";
            this.Code[0x68, 1] = "\x00c3•";
            this.Code[0x69, 1] = "\x00e1\x00bbŒ";
            this.Code[0x6a, 1] = "\x00c3”";
            this.Code[0x6b, 1] = "\x00e1\x00bb\x0090";
            this.Code[0x6c, 1] = "\x00e1\x00bb’";
            this.Code[0x6d, 1] = "\x00e1\x00bb”";
            this.Code[110, 1] = "\x00e1\x00bb–";
            this.Code[0x6f, 1] = "\x00e1\x00bb˜";
            this.Code[0x70, 1] = "\x00c6 ";
            this.Code[0x71, 1] = "\x00e1\x00bbš";
            this.Code[0x72, 1] = "\x00e1\x00bbœ";
            this.Code[0x73, 1] = "\x00e1\x00bbž";
            this.Code[0x74, 1] = "\x00e1\x00bb ";
            this.Code[0x75, 1] = "\x00e1\x00bb\x00a2";
            this.Code[0x76, 1] = "\x00c3š";
            this.Code[0x77, 1] = "\x00c3™";
            this.Code[120, 1] = "\x00e1\x00bb\x00a6";
            this.Code[0x79, 1] = "\x00c5\x00a8";
            this.Code[0x7a, 1] = "\x00e1\x00bb\x00a4";
            this.Code[0x7b, 1] = "\x00c6\x00af";
            this.Code[0x7c, 1] = "\x00e1\x00bb\x00a8";
            this.Code[0x7d, 1] = "\x00e1\x00bb\x00aa";
            this.Code[0x7e, 1] = "\x00e1\x00bb\x00ac";
            this.Code[0x7f, 1] = "\x00e1\x00bb\x00ae";
            this.Code[0x80, 1] = "\x00e1\x00bb\x00b0";
            this.Code[0x81, 1] = "\x00c3\x009d";
            this.Code[130, 1] = "\x00e1\x00bb\x00b2";
            this.Code[0x83, 1] = "\x00e1\x00bb\x00b6";
            this.Code[0x84, 1] = "\x00e1\x00bb\x00b8";
            this.Code[0x85, 1] = "\x00e1\x00bb\x00b4";
            this.Code[0x86, 1] = "\x00c4\x0090";
        }

        private void MapUTH()
        {
            this.Code[1, 5] = "á";
            this.Code[2, 5] = "à";
            this.Code[3, 5] = "ả";
            this.Code[4, 5] = "ã";
            this.Code[5, 5] = "ạ";
            this.Code[6, 5] = "ă";
            this.Code[7, 5] = "ắ";
            this.Code[8, 5] = "ằ";
            this.Code[9, 5] = "ẳ";
            this.Code[10, 5] = "ẵ";
            this.Code[11, 5] = "ặ";
            this.Code[12, 5] = "\x00e2";
            this.Code[13, 5] = "\x00e2́";
            this.Code[14, 5] = "\x00e2̀";
            this.Code[15, 5] = "\x00e2̉";
            this.Code[0x10, 5] = "\x00e2̃";
            this.Code[0x11, 5] = "\x00e2̣";
            this.Code[0x12, 5] = "é";
            this.Code[0x13, 5] = "è";
            this.Code[20, 5] = "ẻ";
            this.Code[0x15, 5] = "ẽ";
            this.Code[0x16, 5] = "ẹ";
            this.Code[0x17, 5] = "\x00ea";
            this.Code[0x18, 5] = "\x00eá";
            this.Code[0x19, 5] = "\x00eà";
            this.Code[0x1a, 5] = "\x00eả";
            this.Code[0x1b, 5] = "\x00eã";
            this.Code[0x1c, 5] = "\x00eạ";
            this.Code[0x1d, 5] = "í";
            this.Code[30, 5] = "ì";
            this.Code[0x1f, 5] = "ỉ";
            this.Code[0x20, 5] = "ĩ";
            this.Code[0x21, 5] = "ị";
            this.Code[0x22, 5] = "ó";
            this.Code[0x23, 5] = "ò";
            this.Code[0x24, 5] = "ỏ";
            this.Code[0x25, 5] = "õ";
            this.Code[0x26, 5] = "ọ";
            this.Code[0x27, 5] = "\x00f4";
            this.Code[40, 5] = "\x00f4́";
            this.Code[0x29, 5] = "\x00f4̀";
            this.Code[0x2a, 5] = "\x00f4̉";
            this.Code[0x2b, 5] = "\x00f4̃";
            this.Code[0x2c, 5] = "\x00f4̣";
            this.Code[0x2d, 5] = "ơ";
            this.Code[0x2e, 5] = "ớ";
            this.Code[0x2f, 5] = "ờ";
            this.Code[0x30, 5] = "ở";
            this.Code[0x31, 5] = "ỡ";
            this.Code[50, 5] = "ợ";
            this.Code[0x33, 5] = "ú";
            this.Code[0x34, 5] = "ù";
            this.Code[0x35, 5] = "ủ";
            this.Code[0x36, 5] = "ũ";
            this.Code[0x37, 5] = "ụ";
            this.Code[0x38, 5] = "ư";
            this.Code[0x39, 5] = "ứ";
            this.Code[0x3a, 5] = "ừ";
            this.Code[0x3b, 5] = "ử";
            this.Code[60, 5] = "ữ";
            this.Code[0x3d, 5] = "ự";
            this.Code[0x3e, 5] = "ý";
            this.Code[0x3f, 5] = "ỳ";
            this.Code[0x40, 5] = "ỷ";
            this.Code[0x41, 5] = "ỹ";
            this.Code[0x42, 5] = "ỵ";
            this.Code[0x43, 5] = "đ";
            this.Code[0x44, 5] = "Á";
            this.Code[0x45, 5] = "À";
            this.Code[70, 5] = "Ả";
            this.Code[0x47, 5] = "Ã";
            this.Code[0x48, 5] = "Ạ";
            this.Code[0x49, 5] = "Ă";
            this.Code[0x4a, 5] = "Ắ";
            this.Code[0x4b, 5] = "Ằ";
            this.Code[0x4c, 5] = "Ẳ";
            this.Code[0x4d, 5] = "Ẵ";
            this.Code[0x4e, 5] = "Ặ";
            this.Code[0x4f, 5] = "\x00c2";
            this.Code[80, 5] = "\x00c2́";
            this.Code[0x51, 5] = "\x00c2̀";
            this.Code[0x52, 5] = "\x00c2̉";
            this.Code[0x53, 5] = "\x00c2̃";
            this.Code[0x54, 5] = "\x00c2̣";
            this.Code[0x55, 5] = "É";
            this.Code[0x56, 5] = "È";
            this.Code[0x57, 5] = "Ẻ";
            this.Code[0x58, 5] = "Ẽ";
            this.Code[0x59, 5] = "Ẹ";
            this.Code[90, 5] = "\x00ca";
            this.Code[0x5b, 5] = "\x00cá";
            this.Code[0x5c, 5] = "\x00cà";
            this.Code[0x5d, 5] = "\x00cả";
            this.Code[0x5e, 5] = "\x00cã";
            this.Code[0x5f, 5] = "\x00cạ";
            this.Code[0x60, 5] = "Í";
            this.Code[0x61, 5] = "Ì";
            this.Code[0x62, 5] = "Ỉ";
            this.Code[0x63, 5] = "Ĩ";
            this.Code[100, 5] = "Ị";
            this.Code[0x65, 5] = "Ó";
            this.Code[0x66, 5] = "Ò";
            this.Code[0x67, 5] = "Ỏ";
            this.Code[0x68, 5] = "Õ";
            this.Code[0x69, 5] = "Ọ";
            this.Code[0x6a, 5] = "\x00d4";
            this.Code[0x6b, 5] = "\x00d4́";
            this.Code[0x6c, 5] = "\x00d4̀";
            this.Code[0x6d, 5] = "\x00d4̉";
            this.Code[110, 5] = "\x00d4̃";
            this.Code[0x6f, 5] = "\x00d4̣";
            this.Code[0x70, 5] = "Ơ";
            this.Code[0x71, 5] = "Ớ";
            this.Code[0x72, 5] = "Ờ";
            this.Code[0x73, 5] = "Ở";
            this.Code[0x74, 5] = "Ỡ";
            this.Code[0x75, 5] = "Ợ";
            this.Code[0x76, 5] = "Ú";
            this.Code[0x77, 5] = "Ù";
            this.Code[120, 5] = "Ủ";
            this.Code[0x79, 5] = "Ũ";
            this.Code[0x7a, 5] = "Ụ";
            this.Code[0x7b, 5] = "Ư";
            this.Code[0x7c, 5] = "Ứ";
            this.Code[0x7d, 5] = "Ừ";
            this.Code[0x7e, 5] = "Ử";
            this.Code[0x7f, 5] = "Ữ";
            this.Code[0x80, 5] = "Ự";
            this.Code[0x81, 5] = "Ý";
            this.Code[130, 5] = "Ỳ";
            this.Code[0x83, 5] = "Ỷ";
            this.Code[0x84, 5] = "Ỹ";
            this.Code[0x85, 5] = "Ỵ";
            this.Code[0x86, 5] = "Đ";
        }

        private void MapVietwareF()
        {
            this.Code[1, 6] = "\x00c0";
            this.Code[2, 6] = "\x00aa";
            this.Code[3, 6] = "\x00b6";
            this.Code[4, 6] = "\x00ba";
            this.Code[5, 6] = "\x00c1";
            this.Code[6, 6] = "Ÿ";
            this.Code[7, 6] = "\x00c5";
            this.Code[8, 6] = "\x00c2";
            this.Code[9, 6] = "\x00c3";
            this.Code[10, 6] = "\x00c4";
            this.Code[11, 6] = "\x00c6";
            this.Code[12, 6] = "\x00a1";
            this.Code[13, 6] = "\x00ca";
            this.Code[14, 6] = "\x00c7";
            this.Code[15, 6] = "\x00c8";
            this.Code[0x10, 6] = "\x00c9";
            this.Code[0x11, 6] = "\x00cb";
            this.Code[0x12, 6] = "\x00cf";
            this.Code[0x13, 6] = "\x00cc";
            this.Code[20, 6] = "\x00cd";
            this.Code[0x15, 6] = "\x00ce";
            this.Code[0x16, 6] = "\x00d1";
            this.Code[0x17, 6] = "\x00a3";
            this.Code[0x18, 6] = "\x00d5";
            this.Code[0x19, 6] = "\x00d2";
            this.Code[0x1a, 6] = "\x00d3";
            this.Code[0x1b, 6] = "\x00d4";
            this.Code[0x1c, 6] = "\x00d6";
            this.Code[0x1d, 6] = "\x00db";
            this.Code[30, 6] = "\x00d8";
            this.Code[0x1f, 6] = "\x00d9";
            this.Code[0x20, 6] = "\x00da";
            this.Code[0x21, 6] = "\x00dc";
            this.Code[0x22, 6] = "\x00e2";
            this.Code[0x23, 6] = "\x00df";
            this.Code[0x24, 6] = "\x00e0";
            this.Code[0x25, 6] = "\x00e1";
            this.Code[0x26, 6] = "\x00e3";
            this.Code[0x27, 6] = "\x00a4";
            this.Code[40, 6] = "\x00e7";
            this.Code[0x29, 6] = "\x00e4";
            this.Code[0x2a, 6] = "\x00e5";
            this.Code[0x2b, 6] = "\x00e6";
            this.Code[0x2c, 6] = "\x00e8";
            this.Code[0x2d, 6] = "\x00a5";
            this.Code[0x2e, 6] = "\x00ec";
            this.Code[0x2f, 6] = "\x00e9";
            this.Code[0x30, 6] = "\x00ea";
            this.Code[0x31, 6] = "\x00eb";
            this.Code[50, 6] = "\x00ed";
            this.Code[0x33, 6] = "\x00f2";
            this.Code[0x34, 6] = "\x00ee";
            this.Code[0x35, 6] = "\x00ef";
            this.Code[0x36, 6] = "\x00f1";
            this.Code[0x37, 6] = "\x00f3";
            this.Code[0x38, 6] = "\x00a7";
            this.Code[0x39, 6] = "\x00f7";
            this.Code[0x3a, 6] = "\x00f4";
            this.Code[0x3b, 6] = "\x00f5";
            this.Code[60, 6] = "\x00f6";
            this.Code[0x3d, 6] = "\x00f8";
            this.Code[0x3e, 6] = "\x00fc";
            this.Code[0x3f, 6] = "\x00f9";
            this.Code[0x40, 6] = "\x00fa";
            this.Code[0x41, 6] = "\x00fb";
            this.Code[0x42, 6] = "\x00ff";
            this.Code[0x43, 6] = "\x00a2";
            this.Code[0x44, 6] = "\x00c0";
            this.Code[0x45, 6] = "\x00aa";
            this.Code[70, 6] = "\x00b6";
            this.Code[0x47, 6] = "\x00ba";
            this.Code[0x48, 6] = "\x00c1";
            this.Code[0x49, 6] = "–";
            this.Code[0x4a, 6] = "\x00c5";
            this.Code[0x4b, 6] = "\x00c2";
            this.Code[0x4c, 6] = "\x00c3";
            this.Code[0x4d, 6] = "\x00c4";
            this.Code[0x4e, 6] = "\x00c6";
            this.Code[0x4f, 6] = "—";
            this.Code[80, 6] = "\x00ca";
            this.Code[0x51, 6] = "\x00c7";
            this.Code[0x52, 6] = "\x00c8";
            this.Code[0x53, 6] = "\x00c9";
            this.Code[0x54, 6] = "\x00cb";
            this.Code[0x55, 6] = "\x00cf";
            this.Code[0x56, 6] = "\x00cc";
            this.Code[0x57, 6] = "\x00cd";
            this.Code[0x58, 6] = "\x00ce";
            this.Code[0x59, 6] = "\x00d1";
            this.Code[90, 6] = "™";
            this.Code[0x5b, 6] = "\x00d5";
            this.Code[0x5c, 6] = "\x00d2";
            this.Code[0x5d, 6] = "\x00d3";
            this.Code[0x5e, 6] = "\x00d4";
            this.Code[0x5f, 6] = "\x00d6";
            this.Code[0x60, 6] = "\x00db";
            this.Code[0x61, 6] = "\x00d8";
            this.Code[0x62, 6] = "\x00d9";
            this.Code[0x63, 6] = "\x00da";
            this.Code[100, 6] = "\x00dc";
            this.Code[0x65, 6] = "\x00e2";
            this.Code[0x66, 6] = "\x00df";
            this.Code[0x67, 6] = "\x00e0";
            this.Code[0x68, 6] = "\x00e1";
            this.Code[0x69, 6] = "\x00e3";
            this.Code[0x6a, 6] = "š";
            this.Code[0x6b, 6] = "\x00e7";
            this.Code[0x6c, 6] = "\x00e4";
            this.Code[0x6d, 6] = "\x00e5";
            this.Code[110, 6] = "\x00e6";
            this.Code[0x6f, 6] = "\x00e8";
            this.Code[0x70, 6] = "›";
            this.Code[0x71, 6] = "\x00ec";
            this.Code[0x72, 6] = "\x00e9";
            this.Code[0x73, 6] = "\x00ea";
            this.Code[0x74, 6] = "\x00eb";
            this.Code[0x75, 6] = "\x00ed";
            this.Code[0x76, 6] = "\x00f2";
            this.Code[0x77, 6] = "\x00ee";
            this.Code[120, 6] = "\x00ef";
            this.Code[0x79, 6] = "\x00f1";
            this.Code[0x7a, 6] = "\x00f3";
            this.Code[0x7b, 6] = "œ";
            this.Code[0x7c, 6] = "\x00f7";
            this.Code[0x7d, 6] = "\x00f4";
            this.Code[0x7e, 6] = "\x00f5";
            this.Code[0x7f, 6] = "\x00f6";
            this.Code[0x80, 6] = "\x00f8";
            this.Code[0x81, 6] = "\x00fc";
            this.Code[130, 6] = "\x00f9";
            this.Code[0x83, 6] = "\x00fa";
            this.Code[0x84, 6] = "\x00fb";
            this.Code[0x85, 6] = "\x00ff";
            this.Code[0x86, 6] = "˜";
        }

        private void MapVietwareX()
        {
            this.Code[1, 6] = "a\x00ef";
            this.Code[2, 6] = "a\x00ec";
            this.Code[3, 6] = "a\x00ed";
            this.Code[4, 6] = "a\x00ee";
            this.Code[5, 6] = "a\x00fb";
            this.Code[6, 6] = "\x00e0";
            this.Code[7, 6] = "\x00e0\x00f5";
            this.Code[8, 6] = "\x00e0\x00f2";
            this.Code[9, 6] = "\x00e0\x00f3";
            this.Code[10, 6] = "\x00e0\x00f4";
            this.Code[11, 6] = "\x00e0\x00fb";
            this.Code[12, 6] = "\x00e1";
            this.Code[13, 6] = "\x00e1\x00fa";
            this.Code[14, 6] = "\x00e1\x00f6";
            this.Code[15, 6] = "\x00e1\x00f8";
            this.Code[0x10, 6] = "\x00e1\x00f9";
            this.Code[0x11, 6] = "\x00e1\x00fb";
            this.Code[0x12, 6] = "e\x00ef";
            this.Code[0x13, 6] = "e\x00ec";
            this.Code[20, 6] = "e\x00ed";
            this.Code[0x15, 6] = "e\x00ee";
            this.Code[0x16, 6] = "e\x00fb";
            this.Code[0x17, 6] = "\x00e3";
            this.Code[0x18, 6] = "\x00e3\x00fa";
            this.Code[0x19, 6] = "\x00e3\x00f6";
            this.Code[0x1a, 6] = "\x00e3\x00f8";
            this.Code[0x1b, 6] = "\x00e3\x00f9";
            this.Code[0x1c, 6] = "\x00e3\x00fb";
            this.Code[0x1d, 6] = "\x00ea";
            this.Code[30, 6] = "\x00e7";
            this.Code[0x1f, 6] = "\x00e8";
            this.Code[0x20, 6] = "\x00e9";
            this.Code[0x21, 6] = "\x00eb";
            this.Code[0x22, 6] = "o\x00ef";
            this.Code[0x23, 6] = "o\x00ec";
            this.Code[0x24, 6] = "o\x00ed";
            this.Code[0x25, 6] = "o\x00ee";
            this.Code[0x26, 6] = "o\x00fc";
            this.Code[0x27, 6] = "\x00e4";
            this.Code[40, 6] = "\x00e4\x00fa";
            this.Code[0x29, 6] = "\x00e4\x00f6";
            this.Code[0x2a, 6] = "\x00e4\x00f8";
            this.Code[0x2b, 6] = "\x00e4\x00f9";
            this.Code[0x2c, 6] = "\x00e4\x00fc";
            this.Code[0x2d, 6] = "\x00e5";
            this.Code[0x2e, 6] = "\x00e5\x00ef";
            this.Code[0x2f, 6] = "\x00e5\x00ec";
            this.Code[0x30, 6] = "\x00e5\x00ed";
            this.Code[0x31, 6] = "\x00e5\x00ee";
            this.Code[50, 6] = "\x00e5\x00fc";
            this.Code[0x33, 6] = "u\x00ef";
            this.Code[0x34, 6] = "u\x00ec";
            this.Code[0x35, 6] = "u\x00ed";
            this.Code[0x36, 6] = "u\x00ee";
            this.Code[0x37, 6] = "u\x00fb";
            this.Code[0x38, 6] = "\x00e6";
            this.Code[0x39, 6] = "\x00e6\x00ef";
            this.Code[0x3a, 6] = "\x00e6\x00ec";
            this.Code[0x3b, 6] = "\x00e6\x00ed";
            this.Code[60, 6] = "\x00e6\x00ee";
            this.Code[0x3d, 6] = "\x00e6\x00fb";
            this.Code[0x3e, 6] = "y\x00ef";
            this.Code[0x3f, 6] = "y\x00ec";
            this.Code[0x40, 6] = "y\x00ed";
            this.Code[0x41, 6] = "y\x00ee";
            this.Code[0x42, 6] = "y\x00f1";
            this.Code[0x43, 6] = "\x00e2";
            this.Code[0x44, 6] = "A\x00cf";
            this.Code[0x45, 6] = "A\x00cc";
            this.Code[70, 6] = "A\x00cd";
            this.Code[0x47, 6] = "A\x00ce";
            this.Code[0x48, 6] = "A\x00db";
            this.Code[0x49, 6] = "\x00c0";
            this.Code[0x4a, 6] = "\x00c0\x00d5";
            this.Code[0x4b, 6] = "\x00c0\x00d2";
            this.Code[0x4c, 6] = "\x00c0\x00d3";
            this.Code[0x4d, 6] = "\x00c0\x00d4";
            this.Code[0x4e, 6] = "\x00c0\x00db";
            this.Code[0x4f, 6] = "\x00c1";
            this.Code[80, 6] = "\x00c1\x00da";
            this.Code[0x51, 6] = "\x00c1\x00d6";
            this.Code[0x52, 6] = "\x00c1\x00d8";
            this.Code[0x53, 6] = "\x00c1\x00d9";
            this.Code[0x54, 6] = "\x00c1\x00db";
            this.Code[0x55, 6] = "E\x00cf";
            this.Code[0x56, 6] = "E\x00cc";
            this.Code[0x57, 6] = "E\x00cd";
            this.Code[0x58, 6] = "E\x00ce";
            this.Code[0x59, 6] = "E\x00db";
            this.Code[90, 6] = "\x00c3";
            this.Code[0x5b, 6] = "\x00c3\x00da";
            this.Code[0x5c, 6] = "\x00c3\x00d6";
            this.Code[0x5d, 6] = "\x00c3\x00d8";
            this.Code[0x5e, 6] = "\x00c3\x00d9";
            this.Code[0x5f, 6] = "\x00c3\x00db";
            this.Code[0x60, 6] = "\x00ca";
            this.Code[0x61, 6] = "\x00c7";
            this.Code[0x62, 6] = "\x00c8";
            this.Code[0x63, 6] = "\x00c9";
            this.Code[100, 6] = "\x00cb";
            this.Code[0x65, 6] = "O\x00cf";
            this.Code[0x66, 6] = "O\x00cc";
            this.Code[0x67, 6] = "O\x00cd";
            this.Code[0x68, 6] = "O\x00ce";
            this.Code[0x69, 6] = "O\x00dc";
            this.Code[0x6a, 6] = "\x00c4";
            this.Code[0x6b, 6] = "\x00c4\x00da";
            this.Code[0x6c, 6] = "\x00c4\x00d6";
            this.Code[0x6d, 6] = "\x00c4\x00d8";
            this.Code[110, 6] = "\x00c4\x00d9";
            this.Code[0x6f, 6] = "\x00c4\x00dc";
            this.Code[0x70, 6] = "\x00c5";
            this.Code[0x71, 6] = "\x00c5\x00cf";
            this.Code[0x72, 6] = "\x00c5\x00cc";
            this.Code[0x73, 6] = "\x00c5\x00cd";
            this.Code[0x74, 6] = "\x00c5\x00ce";
            this.Code[0x75, 6] = "\x00c5\x00dc";
            this.Code[0x76, 6] = "U\x00cf";
            this.Code[0x77, 6] = "U\x00cc";
            this.Code[120, 6] = "U\x00cd";
            this.Code[0x79, 6] = "U\x00ce";
            this.Code[0x7a, 6] = "U\x00db";
            this.Code[0x7b, 6] = "\x00c6";
            this.Code[0x7c, 6] = "\x00c6\x00cf";
            this.Code[0x7d, 6] = "\x00c6\x00cc";
            this.Code[0x7e, 6] = "\x00c6\x00cd";
            this.Code[0x7f, 6] = "\x00c6\x00ce";
            this.Code[0x80, 6] = "\x00c6\x00db";
            this.Code[0x81, 6] = "Y\x00cf";
            this.Code[130, 6] = "Y\x00cc";
            this.Code[0x83, 6] = "Y\x00cd";
            this.Code[0x84, 6] = "Y\x00ce";
            this.Code[0x85, 6] = "Y\x00d1";
            this.Code[0x86, 6] = "\x00c2";
        }

        private void MapVIQR()
        {
            this.Code[1, 8] = "a'";
            this.Code[2, 8] = "a`";
            this.Code[3, 8] = "a?";
            this.Code[4, 8] = "a~";
            this.Code[5, 8] = "a.";
            this.Code[6, 8] = "a(";
            this.Code[7, 8] = "a('";
            this.Code[8, 8] = "a(`";
            this.Code[9, 8] = "a(?";
            this.Code[10, 8] = "a(~";
            this.Code[11, 8] = "a(.";
            this.Code[12, 8] = "a^";
            this.Code[13, 8] = "a^'";
            this.Code[14, 8] = "a^`";
            this.Code[15, 8] = "a^?";
            this.Code[0x10, 8] = "a^~";
            this.Code[0x11, 8] = "a^.";
            this.Code[0x12, 8] = "e'";
            this.Code[0x13, 8] = "e`";
            this.Code[20, 8] = "e?";
            this.Code[0x15, 8] = "e~";
            this.Code[0x16, 8] = "e.";
            this.Code[0x17, 8] = "e^";
            this.Code[0x18, 8] = "e^'";
            this.Code[0x19, 8] = "e^`";
            this.Code[0x1a, 8] = "e^?";
            this.Code[0x1b, 8] = "e^~";
            this.Code[0x1c, 8] = "e^.";
            this.Code[0x1d, 8] = "i'";
            this.Code[30, 8] = "i`";
            this.Code[0x1f, 8] = "i?";
            this.Code[0x20, 8] = "i~";
            this.Code[0x21, 8] = "i.";
            this.Code[0x22, 8] = "o'";
            this.Code[0x23, 8] = "o`";
            this.Code[0x24, 8] = "o?";
            this.Code[0x25, 8] = "o~";
            this.Code[0x26, 8] = "o.";
            this.Code[0x27, 8] = "o^";
            this.Code[40, 8] = "o^'";
            this.Code[0x29, 8] = "o^`";
            this.Code[0x2a, 8] = "o^?";
            this.Code[0x2b, 8] = "o^~";
            this.Code[0x2c, 8] = "o^.";
            this.Code[0x2d, 8] = "o+";
            this.Code[0x2e, 8] = "o+'";
            this.Code[0x2f, 8] = "o+`";
            this.Code[0x30, 8] = "o+?";
            this.Code[0x31, 8] = "o+~";
            this.Code[50, 8] = "o+.";
            this.Code[0x33, 8] = "u'";
            this.Code[0x34, 8] = "u`";
            this.Code[0x35, 8] = "u?";
            this.Code[0x36, 8] = "u~";
            this.Code[0x37, 8] = "u.";
            this.Code[0x38, 8] = "u+";
            this.Code[0x39, 8] = "u+'";
            this.Code[0x3a, 8] = "u+`";
            this.Code[0x3b, 8] = "u+?";
            this.Code[60, 8] = "u+~";
            this.Code[0x3d, 8] = "u+.";
            this.Code[0x3e, 8] = "y'";
            this.Code[0x3f, 8] = "y`";
            this.Code[0x40, 8] = "y?";
            this.Code[0x41, 8] = "y~";
            this.Code[0x42, 8] = "y.";
            this.Code[0x43, 8] = "dd";
            this.Code[0x44, 8] = "A'";
            this.Code[0x45, 8] = "A`";
            this.Code[70, 8] = "A?";
            this.Code[0x47, 8] = "A~";
            this.Code[0x48, 8] = "A.";
            this.Code[0x49, 8] = "A(";
            this.Code[0x4a, 8] = "A('";
            this.Code[0x4b, 8] = "A(`";
            this.Code[0x4c, 8] = "A(?";
            this.Code[0x4d, 8] = "A(~";
            this.Code[0x4e, 8] = "A(.";
            this.Code[0x4f, 8] = "A^";
            this.Code[80, 8] = "A^'";
            this.Code[0x51, 8] = "A^`";
            this.Code[0x52, 8] = "A^?";
            this.Code[0x53, 8] = "A^~";
            this.Code[0x54, 8] = "A^.";
            this.Code[0x55, 8] = "E'";
            this.Code[0x56, 8] = "E`";
            this.Code[0x57, 8] = "E?";
            this.Code[0x58, 8] = "E~";
            this.Code[0x59, 8] = "E.";
            this.Code[90, 8] = "E^";
            this.Code[0x5b, 8] = "E^'";
            this.Code[0x5c, 8] = "E^`";
            this.Code[0x5d, 8] = "E^?";
            this.Code[0x5e, 8] = "E^~";
            this.Code[0x5f, 8] = "E^.";
            this.Code[0x60, 8] = "I'";
            this.Code[0x61, 8] = "I`";
            this.Code[0x62, 8] = "I?";
            this.Code[0x63, 8] = "I~";
            this.Code[100, 8] = "I.";
            this.Code[0x65, 8] = "O'";
            this.Code[0x66, 8] = "O`";
            this.Code[0x67, 8] = "O?";
            this.Code[0x68, 8] = "O~";
            this.Code[0x69, 8] = "O.";
            this.Code[0x6a, 8] = "O^";
            this.Code[0x6b, 8] = "O^'";
            this.Code[0x6c, 8] = "O^`";
            this.Code[0x6d, 8] = "O^?";
            this.Code[110, 8] = "O^~";
            this.Code[0x6f, 8] = "O^.";
            this.Code[0x70, 8] = "O+";
            this.Code[0x71, 8] = "O+'";
            this.Code[0x72, 8] = "O+`";
            this.Code[0x73, 8] = "O+?";
            this.Code[0x74, 8] = "O+~";
            this.Code[0x75, 8] = "O+.";
            this.Code[0x76, 8] = "U'";
            this.Code[0x77, 8] = "U`";
            this.Code[120, 8] = "U?";
            this.Code[0x79, 8] = "U~";
            this.Code[0x7a, 8] = "U.";
            this.Code[0x7b, 8] = "U+";
            this.Code[0x7c, 8] = "U+'";
            this.Code[0x7d, 8] = "U+`";
            this.Code[0x7e, 8] = "U+?";
            this.Code[0x7f, 8] = "U+~";
            this.Code[0x80, 8] = "U+.";
            this.Code[0x81, 8] = "Y'";
            this.Code[130, 8] = "Y`";
            this.Code[0x83, 8] = "Y?";
            this.Code[0x84, 8] = "Y~";
            this.Code[0x85, 8] = "Y.";
            this.Code[0x86, 8] = "DD";
        }

        private void MapVISCII()
        {
            this.Code[1, 6] = "\x00e1";
            this.Code[2, 6] = "\x00e0";
            this.Code[3, 6] = "\x00e4";
            this.Code[4, 6] = "\x00e3";
            this.Code[5, 6] = "\x00d5";
            this.Code[6, 6] = "\x00e5";
            this.Code[7, 6] = "\x00a1";
            this.Code[8, 6] = "\x00a2";
            this.Code[9, 6] = "\x00c6";
            this.Code[10, 6] = "\x00c7";
            this.Code[11, 6] = "\x00a3";
            this.Code[12, 6] = "\x00e2";
            this.Code[13, 6] = "\x00a4";
            this.Code[14, 6] = "\x00a5";
            this.Code[15, 6] = "\x00a6";
            this.Code[0x10, 6] = "\x00e7";
            this.Code[0x11, 6] = "\x00a7";
            this.Code[0x12, 6] = "\x00e9";
            this.Code[0x13, 6] = "\x00e8";
            this.Code[20, 6] = "\x00eb";
            this.Code[0x15, 6] = "\x00a8";
            this.Code[0x16, 6] = "\x00a9";
            this.Code[0x17, 6] = "\x00ea";
            this.Code[0x18, 6] = "\x00aa";
            this.Code[0x19, 6] = "\x00ab";
            this.Code[0x1a, 6] = "\x00ac";
            this.Code[0x1b, 6] = "\x00ad";
            this.Code[0x1c, 6] = "\x00ae";
            this.Code[0x1d, 6] = "\x00ed";
            this.Code[30, 6] = "\x00ec";
            this.Code[0x1f, 6] = "\x00ef";
            this.Code[0x20, 6] = "\x00ee";
            this.Code[0x21, 6] = "\x00b8";
            this.Code[0x22, 6] = "\x00f3";
            this.Code[0x23, 6] = "\x00f2";
            this.Code[0x24, 6] = "\x00f6";
            this.Code[0x25, 6] = "\x00f5";
            this.Code[0x26, 6] = "\x00f7";
            this.Code[0x27, 6] = "\x00f4";
            this.Code[40, 6] = "\x00af";
            this.Code[0x29, 6] = "\x00b0";
            this.Code[0x2a, 6] = "\x00b1";
            this.Code[0x2b, 6] = "\x00b2";
            this.Code[0x2c, 6] = "\x00b5";
            this.Code[0x2d, 6] = "\x00bd";
            this.Code[0x2e, 6] = "\x00be";
            this.Code[0x2f, 6] = "\x00b6";
            this.Code[0x30, 6] = "\x00b7";
            this.Code[0x31, 6] = "\x00de";
            this.Code[50, 6] = "\x00fe";
            this.Code[0x33, 6] = "\x00fa";
            this.Code[0x34, 6] = "\x00f9";
            this.Code[0x35, 6] = "\x00fc";
            this.Code[0x36, 6] = "\x00fb";
            this.Code[0x37, 6] = "\x00f8";
            this.Code[0x38, 6] = "\x00df";
            this.Code[0x39, 6] = "\x00d1";
            this.Code[0x3a, 6] = "\x00d7";
            this.Code[0x3b, 6] = "\x00d8";
            this.Code[60, 6] = "\x00e6";
            this.Code[0x3d, 6] = "\x00f1";
            this.Code[0x3e, 6] = "\x00fd";
            this.Code[0x3f, 6] = "\x00cf";
            this.Code[0x40, 6] = "\x00d6";
            this.Code[0x41, 6] = "\x00db";
            this.Code[0x42, 6] = "\x00dc";
            this.Code[0x43, 6] = "\x00f0";
            this.Code[0x44, 6] = "\x00c1";
            this.Code[0x45, 6] = "\x00c0";
            this.Code[70, 6] = "\x00c4";
            this.Code[0x47, 6] = "\x00c3";
            this.Code[0x48, 6] = "€";
            this.Code[0x49, 6] = "\x00c5";
            this.Code[0x4a, 6] = "\x0081";
            this.Code[0x4b, 6] = "‚";
            this.Code[0x4c, 6] = "\x00c6";
            this.Code[0x4d, 6] = "\x00c7";
            this.Code[0x4e, 6] = "ƒ";
            this.Code[0x4f, 6] = "\x00c2";
            this.Code[80, 6] = "„";
            this.Code[0x51, 6] = "…";
            this.Code[0x52, 6] = "†";
            this.Code[0x53, 6] = "\x00e7";
            this.Code[0x54, 6] = "‡";
            this.Code[0x55, 6] = "\x00c9";
            this.Code[0x56, 6] = "\x00c8";
            this.Code[0x57, 6] = "\x00cb";
            this.Code[0x58, 6] = "ˆ";
            this.Code[0x59, 6] = "‰";
            this.Code[90, 6] = "\x00ca";
            this.Code[0x5b, 6] = "Š";
            this.Code[0x5c, 6] = "‹";
            this.Code[0x5d, 6] = "Œ";
            this.Code[0x5e, 6] = "\x008d";
            this.Code[0x5f, 6] = "Ž";
            this.Code[0x60, 6] = "\x00cd";
            this.Code[0x61, 6] = "\x00cc";
            this.Code[0x62, 6] = "›";
            this.Code[0x63, 6] = "\x00ce";
            this.Code[100, 6] = "˜";
            this.Code[0x65, 6] = "\x00d3";
            this.Code[0x66, 6] = "\x00d2";
            this.Code[0x67, 6] = "™";
            this.Code[0x68, 6] = "\x00f5";
            this.Code[0x69, 6] = "š";
            this.Code[0x6a, 6] = "\x00d4";
            this.Code[0x6b, 6] = "\x008f";
            this.Code[0x6c, 6] = "\x0090";
            this.Code[0x6d, 6] = "‘";
            this.Code[110, 6] = "’";
            this.Code[0x6f, 6] = "“";
            this.Code[0x70, 6] = "\x00b4";
            this.Code[0x71, 6] = "•";
            this.Code[0x72, 6] = "–";
            this.Code[0x73, 6] = "—";
            this.Code[0x74, 6] = "\x00b3";
            this.Code[0x75, 6] = "”";
            this.Code[0x76, 6] = "\x00da";
            this.Code[0x77, 6] = "\x00d9";
            this.Code[120, 6] = "œ";
            this.Code[0x79, 6] = "\x009d";
            this.Code[0x7a, 6] = "ž";
            this.Code[0x7b, 6] = "\x00bf";
            this.Code[0x7c, 6] = "\x00ba";
            this.Code[0x7d, 6] = "\x00bb";
            this.Code[0x7e, 6] = "\x00bc";
            this.Code[0x7f, 6] = "\x00ff";
            this.Code[0x80, 6] = "\x00b9";
            this.Code[0x81, 6] = "\x00dd";
            this.Code[130, 6] = "Ÿ";
            this.Code[0x83, 6] = "\x00d6";
            this.Code[0x84, 6] = "\x00db";
            this.Code[0x85, 6] = "\x00dc";
            this.Code[0x86, 6] = "\x00d0";
        }

        private void MapVNI()
        {
            this.Code[1, 3] = "a\x00f9";
            this.Code[2, 3] = "a\x00f8";
            this.Code[3, 3] = "a\x00fb";
            this.Code[4, 3] = "a\x00f5";
            this.Code[5, 3] = "a\x00ef";
            this.Code[6, 3] = "a\x00ea";
            this.Code[7, 3] = "a\x00e9";
            this.Code[8, 3] = "a\x00e8";
            this.Code[9, 3] = "a\x00fa";
            this.Code[10, 3] = "a\x00fc";
            this.Code[11, 3] = "a\x00eb";
            this.Code[12, 3] = "a\x00e2";
            this.Code[13, 3] = "a\x00e1";
            this.Code[14, 3] = "a\x00e0";
            this.Code[15, 3] = "a\x00e5";
            this.Code[0x10, 3] = "a\x00e3";
            this.Code[0x11, 3] = "a\x00e4";
            this.Code[0x12, 3] = "e\x00f9";
            this.Code[0x13, 3] = "e\x00f8";
            this.Code[20, 3] = "e\x00fb";
            this.Code[0x15, 3] = "e\x00f5";
            this.Code[0x16, 3] = "e\x00ef";
            this.Code[0x17, 3] = "e\x00e2";
            this.Code[0x18, 3] = "e\x00e1";
            this.Code[0x19, 3] = "e\x00e0";
            this.Code[0x1a, 3] = "e\x00e5";
            this.Code[0x1b, 3] = "e\x00e3";
            this.Code[0x1c, 3] = "e\x00e4";
            this.Code[0x1d, 3] = "\x00ed";
            this.Code[30, 3] = "\x00ec";
            this.Code[0x1f, 3] = "\x00e6";
            this.Code[0x20, 3] = "\x00f3";
            this.Code[0x21, 3] = "\x00f2";
            this.Code[0x22, 3] = "o\x00f9";
            this.Code[0x23, 3] = "o\x00f8";
            this.Code[0x24, 3] = "o\x00fb";
            this.Code[0x25, 3] = "o\x00f5";
            this.Code[0x26, 3] = "o\x00ef";
            this.Code[0x27, 3] = "o\x00e2";
            this.Code[40, 3] = "o\x00e1";
            this.Code[0x29, 3] = "o\x00e0";
            this.Code[0x2a, 3] = "o\x00e5";
            this.Code[0x2b, 3] = "o\x00e3";
            this.Code[0x2c, 3] = "o\x00e4";
            this.Code[0x2d, 3] = "\x00f4";
            this.Code[0x2e, 3] = "\x00f4\x00f9";
            this.Code[0x2f, 3] = "\x00f4\x00f8";
            this.Code[0x30, 3] = "\x00f4\x00fb";
            this.Code[0x31, 3] = "\x00f4\x00f5";
            this.Code[50, 3] = "\x00f4\x00ef";
            this.Code[0x33, 3] = "u\x00f9";
            this.Code[0x34, 3] = "u\x00f8";
            this.Code[0x35, 3] = "u\x00fb";
            this.Code[0x36, 3] = "u\x00f5";
            this.Code[0x37, 3] = "u\x00ef";
            this.Code[0x38, 3] = "\x00f6";
            this.Code[0x39, 3] = "\x00f6\x00f9";
            this.Code[0x3a, 3] = "\x00f6\x00f8";
            this.Code[0x3b, 3] = "\x00f6\x00fb";
            this.Code[60, 3] = "\x00f6\x00f5";
            this.Code[0x3d, 3] = "\x00f6\x00ef";
            this.Code[0x3e, 3] = "y\x00f9";
            this.Code[0x3f, 3] = "y\x00f8";
            this.Code[0x40, 3] = "y\x00fb";
            this.Code[0x41, 3] = "y\x00f5";
            this.Code[0x42, 3] = "\x00ee";
            this.Code[0x43, 3] = "\x00f1";
            this.Code[0x44, 3] = "A\x00d9";
            this.Code[0x45, 3] = "A\x00d8";
            this.Code[70, 3] = "A\x00db";
            this.Code[0x47, 3] = "A\x00d5";
            this.Code[0x48, 3] = "A\x00cf";
            this.Code[0x49, 3] = "A\x00ca";
            this.Code[0x4a, 3] = "A\x00c9";
            this.Code[0x4b, 3] = "A\x00c8";
            this.Code[0x4c, 3] = "A\x00da";
            this.Code[0x4d, 3] = "A\x00dc";
            this.Code[0x4e, 3] = "A\x00cb";
            this.Code[0x4f, 3] = "A\x00c2";
            this.Code[80, 3] = "A\x00c1";
            this.Code[0x51, 3] = "A\x00c0";
            this.Code[0x52, 3] = "A\x00c5";
            this.Code[0x53, 3] = "A\x00c3";
            this.Code[0x54, 3] = "A\x00c4";
            this.Code[0x55, 3] = "E\x00d9";
            this.Code[0x56, 3] = "E\x00d8";
            this.Code[0x57, 3] = "E\x00db";
            this.Code[0x58, 3] = "E\x00d5";
            this.Code[0x59, 3] = "E\x00cf";
            this.Code[90, 3] = "E\x00c2";
            this.Code[0x5b, 3] = "E\x00c1";
            this.Code[0x5c, 3] = "E\x00c0";
            this.Code[0x5d, 3] = "E\x00c5";
            this.Code[0x5e, 3] = "E\x00c3";
            this.Code[0x5f, 3] = "E\x00c4";
            this.Code[0x60, 3] = "\x00cd";
            this.Code[0x61, 3] = "\x00cc";
            this.Code[0x62, 3] = "\x00c6";
            this.Code[0x63, 3] = "\x00d3";
            this.Code[100, 3] = "\x00d2";
            this.Code[0x65, 3] = "O\x00d9";
            this.Code[0x66, 3] = "O\x00d8";
            this.Code[0x67, 3] = "O\x00db";
            this.Code[0x68, 3] = "O\x00d5";
            this.Code[0x69, 3] = "O\x00cf";
            this.Code[0x6a, 3] = "O\x00c2";
            this.Code[0x6b, 3] = "O\x00c1";
            this.Code[0x6c, 3] = "O\x00c0";
            this.Code[0x6d, 3] = "O\x00c5";
            this.Code[110, 3] = "O\x00c3";
            this.Code[0x6f, 3] = "O\x00c4";
            this.Code[0x70, 3] = "\x00d4";
            this.Code[0x71, 3] = "\x00d4\x00d9";
            this.Code[0x72, 3] = "\x00d4\x00d8";
            this.Code[0x73, 3] = "\x00d4\x00db";
            this.Code[0x74, 3] = "\x00d4\x00d5";
            this.Code[0x75, 3] = "\x00d4\x00cf";
            this.Code[0x76, 3] = "U\x00d9";
            this.Code[0x77, 3] = "U\x00d8";
            this.Code[120, 3] = "U\x00db";
            this.Code[0x79, 3] = "U\x00d5";
            this.Code[0x7a, 3] = "U\x00cf";
            this.Code[0x7b, 3] = "\x00d6";
            this.Code[0x7c, 3] = "\x00d6\x00d9";
            this.Code[0x7d, 3] = "\x00d6\x00d8";
            this.Code[0x7e, 3] = "\x00d6\x00db";
            this.Code[0x7f, 3] = "\x00d6\x00d5";
            this.Code[0x80, 3] = "\x00d6\x00cf";
            this.Code[0x81, 3] = "Y\x00d9";
            this.Code[130, 3] = "Y\x00d8";
            this.Code[0x83, 3] = "Y\x00db";
            this.Code[0x84, 3] = "Y\x00d5";
            this.Code[0x85, 3] = "\x00ce";
            this.Code[0x86, 3] = "\x00d1";
        }

        private void MapVPS()
        {
            this.Code[1, 6] = "\x00e1";
            this.Code[2, 6] = "\x00e0";
            this.Code[3, 6] = "\x00e4";
            this.Code[4, 6] = "\x00e3";
            this.Code[5, 6] = "\x00e5";
            this.Code[6, 6] = "\x00e6";
            this.Code[7, 6] = "\x00a1";
            this.Code[8, 6] = "\x00a2";
            this.Code[9, 6] = "\x00a3";
            this.Code[10, 6] = "\x00a4";
            this.Code[11, 6] = "\x00a5";
            this.Code[12, 6] = "\x00e2";
            this.Code[13, 6] = "\x00c3";
            this.Code[14, 6] = "\x00c0";
            this.Code[15, 6] = "\x00c4";
            this.Code[0x10, 6] = "\x00c5";
            this.Code[0x11, 6] = "\x00c6";
            this.Code[0x12, 6] = "\x00e9";
            this.Code[0x13, 6] = "\x00e8";
            this.Code[20, 6] = "\x00c8";
            this.Code[0x15, 6] = "\x00eb";
            this.Code[0x16, 6] = "\x00cb";
            this.Code[0x17, 6] = "\x00ea";
            this.Code[0x18, 6] = "‰";
            this.Code[0x19, 6] = "Š";
            this.Code[0x1a, 6] = "‹";
            this.Code[0x1b, 6] = "\x00cd";
            this.Code[0x1c, 6] = "Œ";
            this.Code[0x1d, 6] = "\x00ed";
            this.Code[30, 6] = "\x00ec";
            this.Code[0x1f, 6] = "\x00cc";
            this.Code[0x20, 6] = "\x00ef";
            this.Code[0x21, 6] = "\x00ce";
            this.Code[0x22, 6] = "\x00f3";
            this.Code[0x23, 6] = "\x00f2";
            this.Code[0x24, 6] = "\x00d5";
            this.Code[0x25, 6] = "\x00f5";
            this.Code[0x26, 6] = "†";
            this.Code[0x27, 6] = "\x00f4";
            this.Code[40, 6] = "\x00d3";
            this.Code[0x29, 6] = "\x00d2";
            this.Code[0x2a, 6] = "\x00b0";
            this.Code[0x2b, 6] = "‡";
            this.Code[0x2c, 6] = "\x00b6";
            this.Code[0x2d, 6] = "\x00d6";
            this.Code[0x2e, 6] = "\x00a7";
            this.Code[0x2f, 6] = "\x00a9";
            this.Code[0x30, 6] = "\x00aa";
            this.Code[0x31, 6] = "\x00ab";
            this.Code[50, 6] = "\x00ae";
            this.Code[0x33, 6] = "\x00fa";
            this.Code[0x34, 6] = "\x00f9";
            this.Code[0x35, 6] = "\x00fb";
            this.Code[0x36, 6] = "\x00db";
            this.Code[0x37, 6] = "\x00f8";
            this.Code[0x38, 6] = "\x00dc";
            this.Code[0x39, 6] = "\x00d9";
            this.Code[0x3a, 6] = "\x00d8";
            this.Code[0x3b, 6] = "\x00ba";
            this.Code[60, 6] = "\x00bb";
            this.Code[0x3d, 6] = "\x00bf";
            this.Code[0x3e, 6] = "š";
            this.Code[0x3f, 6] = "\x00ff";
            this.Code[0x40, 6] = "›";
            this.Code[0x41, 6] = "\x00cf";
            this.Code[0x42, 6] = "œ";
            this.Code[0x43, 6] = "\x00c7";
            this.Code[0x44, 6] = "\x00c1";
            this.Code[0x45, 6] = "€";
            this.Code[70, 6] = "\x0081";
            this.Code[0x47, 6] = "‚";
            this.Code[0x48, 6] = "\x00e5";
            this.Code[0x49, 6] = "ˆ";
            this.Code[0x4a, 6] = "\x008d";
            this.Code[0x4b, 6] = "Ž";
            this.Code[0x4c, 6] = "\x008f";
            this.Code[0x4d, 6] = "\x00f0";
            this.Code[0x4e, 6] = "\x00a5";
            this.Code[0x4f, 6] = "\x00c2";
            this.Code[80, 6] = "ƒ";
            this.Code[0x51, 6] = "„";
            this.Code[0x52, 6] = "…";
            this.Code[0x53, 6] = "\x00c5";
            this.Code[0x54, 6] = "\x00c6";
            this.Code[0x55, 6] = "\x00c9";
            this.Code[0x56, 6] = "\x00d7";
            this.Code[0x57, 6] = "\x00de";
            this.Code[0x58, 6] = "\x00fe";
            this.Code[0x59, 6] = "\x00cb";
            this.Code[90, 6] = "\x00ca";
            this.Code[0x5b, 6] = "\x0090";
            this.Code[0x5c, 6] = "“";
            this.Code[0x5d, 6] = "”";
            this.Code[0x5e, 6] = "•";
            this.Code[0x5f, 6] = "Œ";
            this.Code[0x60, 6] = "\x00b4";
            this.Code[0x61, 6] = "\x00b5";
            this.Code[0x62, 6] = "\x00b7";
            this.Code[0x63, 6] = "\x00b8";
            this.Code[100, 6] = "\x00ce";
            this.Code[0x65, 6] = "\x00b9";
            this.Code[0x66, 6] = "\x00bc";
            this.Code[0x67, 6] = "\x00bd";
            this.Code[0x68, 6] = "\x00be";
            this.Code[0x69, 6] = "†";
            this.Code[0x6a, 6] = "\x00d4";
            this.Code[0x6b, 6] = "–";
            this.Code[0x6c, 6] = "—";
            this.Code[0x6d, 6] = "˜";
            this.Code[110, 6] = "™";
            this.Code[0x6f, 6] = "\x00b6";
            this.Code[0x70, 6] = "\x00f7";
            this.Code[0x71, 6] = "\x009d";
            this.Code[0x72, 6] = "ž";
            this.Code[0x73, 6] = "Ÿ";
            this.Code[0x74, 6] = "\x00a6";
            this.Code[0x75, 6] = "\x00ae";
            this.Code[0x76, 6] = "\x00da";
            this.Code[0x77, 6] = "\x00a8";
            this.Code[120, 6] = "\x00d1";
            this.Code[0x79, 6] = "\x00ac";
            this.Code[0x7a, 6] = "\x00f8";
            this.Code[0x7b, 6] = "\x00d0";
            this.Code[0x7c, 6] = "\x00ad";
            this.Code[0x7d, 6] = "\x00af";
            this.Code[0x7e, 6] = "\x00b1";
            this.Code[0x7f, 6] = "\x00bb";
            this.Code[0x80, 6] = "\x00bf";
            this.Code[0x81, 6] = "\x00dd";
            this.Code[130, 6] = "\x00b2";
            this.Code[0x83, 6] = "\x00fd";
            this.Code[0x84, 6] = "\x00b3";
            this.Code[0x85, 6] = "œ";
            this.Code[0x86, 6] = "\x00f1";
        }

        public FontCase CharCase
        {
            get
            {
                return this.m_CharCase;
            }
            set
            {
                this.m_CharCase = value;
            }
        }
    }

    public class ConFontPsc
    {
        static string PSCVowels = "az4az1az2az3az5azzazzz4azzz1azzz2azzz3azzz5azzzazzzz4azzzz1azzzz2azzzz3azzzz5ez4ez1ez2ez3ez5ezzezzz4ezzz1ezzz2ezzz3ezzz5iz4iz1iz2iz3iz5oz4oz1oz2oz3oz5ozzozzz4ozzz1ozzz2ozzz3ozzz5ozzzozzzz4ozzzz1ozzzz2ozzzz3ozzzz5uz4uz1uz2uz3uz5uzzuzzz4uzzz1uzzz2uzzz3uzzz5yz4yz1yz2yz3yz5dzAZ4AZ1AZ2AZ3AZ5AZZAZZZ4AZZZ1AZZZ2AZZZ3AZZZ5AZZZAZZZZ4AZZZZ1AZZZZ2AZZZZ3AZZZZ5EZ4EZ1EZ2EZ3EZ5EZZEZZZ4EZZZ1EZZZ2EZZZ3EZZZ5IZ4IZ1IZ2IZ3IZ5OZ4OZ1OZ2OZ3OZ5OZZOZZZ4OZZZ1OZZZ2OZZZ3OZZZ5OZZZOZZZZ4OZZZZ1OZZZZ2OZZZZ3OZZZZ5UZ4UZ1UZ2UZ3UZ5UZZUZZZ4UZZZ1UZZZ2UZZZ3UZZZ5YZ4YZ1YZ2YZ3YZ5DZ";
        static string PSCVowelsLen = "33333355555466666333333555553333333333355555466666333333555553333323333335555546666633333355555333333333335555546666633333355555333332";
        static string UNICODEVowels = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

        public static string Convert(string scr)
        {
            string outStr = "", letter = "";
            int i = 0, index = 0;
            while (i < scr.Length)
            {
                letter = scr.Substring(i, 1);
                index = UNICODEVowels.IndexOf(letter);
                if (index >= 0)
                    outStr += PscCharsAt(index);
                else
                    outStr += letter;
                i += 1;
            }
            return outStr;
        }
        static string PscCharsAt(int index)
        {
            string PSCChars = "";
            int i = 0;
            int PSCIndex = 0;
            while (i < index)
            {
                PSCIndex = PSCIndex + int.Parse(PSCVowelsLen.Substring(i, 1));
                i += 1;
            }
            PSCChars = PSCVowels.Substring(PSCIndex, int.Parse(PSCVowelsLen.Substring(index, 1)));
            return PSCChars;
        }
    }

    public enum FontCase
    {
        UpperCase,
        LowerCase,
        Normal
    }

    public enum FontIndex
    {
        iCP1258 = 4,
        iNCR = 0,
        iNOSIGN = 7,
        iNotKnown = -1,
        iTCV = 2,
        iUNI = 6,
        iUTF = 1,
        iUTH = 5,
        iVIQ = 8,
        iVNI = 3
    }

    #region DBF
    class DBFColumnMapping
    {
        internal string SourceName { get; set; }
        internal byte Size { get; set; }
        internal byte DecimalPlaces { get; set; }
        internal string F_Name { get; set; }
    }

    public class DBFExport
    {
        static List<DBFColumnMapping> MapCols = new List<DBFColumnMapping>();
        public static void AddMap(string SourceName, string F_Name)
        {
            if (!MapCols.Any(c => string.Compare(c.SourceName, SourceName, true) == 0 && string.Compare(c.F_Name, F_Name, true) == 0))
                MapCols.Add(new DBFColumnMapping { SourceName = SourceName, F_Name = F_Name });
            else
                throw new Exception("Exists column: " + SourceName + " and " + F_Name);
        }

        public DataTable DataSource { get; set; }

        private readonly List<IField> _fields = new List<IField>();
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public IList<IField> Fields { get { return _fields; } }

        /// <summary>
        /// Gets the data file format.
        /// </summary>
        /// <value>The data file format.</value>
        public IDBFExportFormatFactory ExportFormatFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBFExport"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="exportFormatFactory">The data file format.</param>
        public DBFExport(DataTable dataSource)
        {
            DataSource = dataSource.Copy();
            ExportFormatFactory = new DbfDBFExportFormatFactory();
            var eCols = from c1 in DataSource.Columns.Cast<DataColumn>()
                        join c2 in MapCols on c1.ColumnName.ToLower() equals c2.SourceName.ToLower()
                        select new { c1, c2 };
            foreach (var col in eCols)
            {
                col.c1.ColumnName = col.c2.F_Name;
            }
        }

        public void AutoCreateFields()
        {
            try
            {
                if (this.DataSource != null && this.DataSource.Rows.Count > 0)
                {
                    this.Fields.Clear();
                    var eRows = this.DataSource.AsEnumerable();
                    CommonLib.ImportAndExport.StatusForm.Self.UpdateStatus("Đang tạo file...");

                    foreach (DataColumn col in this.DataSource.Columns)
                    {
                        if (col.DataType == typeof(byte))
                        {
                            this.AddNumericField(col.ColumnName, (byte)byte.MaxValue.ToString().Length, 0);
                        }
                        else if (col.DataType == typeof(short))
                        {
                            this.AddNumericField(col.ColumnName, (byte)short.MaxValue.ToString().Length, 0);
                        }
                        else if (col.DataType == typeof(int))
                        {
                            this.AddNumericField(col.ColumnName, (byte)int.MaxValue.ToString().Length, 0);
                        }
                        else if (col.DataType == typeof(long))
                        {
                            this.AddNumericField(col.ColumnName, (byte)long.MaxValue.ToString().Length, 0);
                        }
                        else if (col.DataType == typeof(Single) || col.DataType == typeof(float) || col.DataType == typeof(double) || col.DataType == typeof(decimal))
                        {
                            this.AddNumericField(col.ColumnName, (byte)float.MaxValue.ToString().Length, 4);
                        }
                        else if (col.DataType == typeof(DateTime))
                        {
                            this.AddDateField(col.ColumnName, col.ColumnName);
                        }
                        else if (col.DataType == typeof(bool))
                        {
                            this.AddBooleanField(col.ColumnName, col.ColumnName);
                        }
                        else if (col.DataType == typeof(string))
                        {
                            int max = 0;
                            foreach (var row in eRows)
                            {
                                var val = row[col.ColumnName].ToString();
                                if (val.Length > max)
                                    max = val.Length;
                            }
                            if (max < 20) max = 20;
                            if (max > 255) max = 255;
                            this.AddTextField(col.ColumnName, (byte)max, col.ColumnName);
                        }
                        else
                        {
                            this.AddGenericField(col.ColumnName, col.ColumnName);
                        }
                    }
                }
            }
            catch { }
            CommonLib.ImportAndExport.StatusForm.Self.Hide();
        }

        public bool Export()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "FoxPro file|*.dbf";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return Export(dlg.FileName);
            }

            return false;
        }

        public bool Export(string filePath)
        {
            try
            {
                if (this.Fields.Count == 0)
                {
                    this.AutoCreateFields();
                }
                Write(filePath);
                return true;
            }
            catch { }

            return false;
        }

        DBFExport Write(Stream stream)
        {
            DbfCommon.Against<ArgumentNullException>(null == stream, "stream");

            using (IDataFileWriter writer = this.ExportFormatFactory.CreateWriter(stream))
            {
                writer.Write(this);
            }
            return this;
        }

        DBFExport Write(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Write(stream);
            }
            return this;
        }
    }

    public static class DBFExportExtensions
    {
        /// <summary>
        /// Adds a new <see cref="TextField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddTextField(this DBFExport dataFileExport, string name, byte totalSize)
        {
            TextField field = new TextField(name, totalSize);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name and with the default size of 255.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddTextField(this DBFExport dataFileExport, string name)
        {
            TextField field = new TextField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to the <see cref="DBFExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddTextField(this DBFExport dataFileExport, string name, byte totalSize, string sourceName)
        {
            TextField field = new TextField(name, totalSize, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="TextField"/> to the <see cref="DBFExport"/> with the default size of 255.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddTextField(this DBFExport dataFileExport, string name, string sourceName)
        {
            TextField field = new TextField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddNumericField(this DBFExport dataFileExport, string name, byte totalSize, byte decimalPlaces)
        {
            NumericField field = new NumericField(name, totalSize, decimalPlaces);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name and with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddNumericField(this DBFExport dataFileExport, string name)
        {
            NumericField field = new NumericField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to the <see cref="DBFExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddNumericField(this DBFExport dataFileExport, string name, byte totalSize, byte decimalPlaces, string sourceName)
        {
            NumericField field = new NumericField(name, totalSize, decimalPlaces, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="NumericField"/> to the <see cref="DBFExport"/> with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddNumericField(this DBFExport dataFileExport, string name, string sourceName)
        {
            NumericField field = new NumericField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="DateField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddDateField(this DBFExport dataFileExport, string name)
        {
            DateField field = new DateField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="DateField"/> to the <see cref="DBFExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        /// <remarks>The date field might contain time information but the will be only written if the writter supports it. 
        /// For example DBF date fields contain only the date without time information.</remarks>
        public static DBFExport AddDateField(this DBFExport dataFileExport, string name, string sourceName)
        {
            DateField field = new DateField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="BooleanField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddBooleanField(this DBFExport dataFileExport, string name)
        {
            BooleanField field = new BooleanField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="BooleanField"/> to the <see cref="DBFExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddBooleanField(this DBFExport dataFileExport, string name, string sourceName)
        {
            BooleanField field = new BooleanField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="GenericField"/> to this <see cref="DBFExport"/> with data source field name the same as the field name.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddGenericField(this DBFExport dataFileExport, string name)
        {
            GenericField field = new GenericField(name);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }

        /// <summary>
        /// Adds a new <see cref="GenericField"/> to the <see cref="DBFExport"/>.
        /// </summary>
        /// <param name="dataFileExport">The data file export.</param>
        /// <param name="name">The field name.</param>
        /// <param name="sourceName">The data source field name.</param>
        /// <returns>The <see cref="DBFExport"/>.</returns>
        public static DBFExport AddGenericField(this DBFExport dataFileExport, string name, string sourceName)
        {
            GenericField field = new GenericField(name, sourceName);
            dataFileExport.Fields.Add(field);
            return dataFileExport;
        }
    }

    public class DbfDataFileWriter : IDataFileWriter, IDisposable
    {
        /// <summary>
        /// Gets the output stream.
        /// </summary>
        /// <value>The output stream.</value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the binary writer used to write to the output stream.
        /// </summary>
        /// <value>The binary writer.</value>
        public BinaryWriter BinaryWriter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbfDataFileWriter"/> class.
        /// </summary>
        /// <param name="stream">The output stream to write to.</param>
        public DbfDataFileWriter(Stream stream)
        {
            this.Stream = stream;
            this.BinaryWriter = new BinaryWriter(stream, Encoding.ASCII);
        }

        /// <summary>
        /// Writes the data file to the output stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        public void Write(DBFExport dataFileExport)
        {
            WriteFileHeader(dataFileExport);
            WriteRows(dataFileExport);
            this.BinaryWriter.Flush();
        }

        void WriteFileHeader(DBFExport dataFileExport)
        {
            DbfHeaderStructure headerStructure = new DbfHeaderStructure();
            FillDbfFileHeaderStructure(dataFileExport, ref headerStructure);

            DbfStructureBinaryWriter.Write(this.BinaryWriter, headerStructure);

            WriteFieldsHeader(dataFileExport);

            this.BinaryWriter.Write(DbfConstants.HeaderTerminator);
        }

        void WriteFieldsHeader(DBFExport dataFileExport)
        {
            int offset = 1;
            for (int i = 0; i < dataFileExport.Fields.Count; i++)
            {
                IField field = dataFileExport.Fields[i];
                field.GetFieldWriter(dataFileExport.ExportFormatFactory).WriteHeader(offset);
                offset += field.TotalSize;
            }
        }

        void WriteRows(DBFExport dataFileExport)
        {
            int rowCount = dataFileExport.DataSource.Rows.Count;
            if (rowCount > 0)
            {
                for (int rowIndex = 0; rowIndex != rowCount; rowIndex++)
                {
                    WriteRow(dataFileExport, rowIndex);
                }

                // rows end separator is written only if there are rows in the data source
                this.BinaryWriter.Write(DbfConstants.RowsEndTerminator);
            }
        }

        CommonLib.ConvertFont fCon = new CommonLib.ConvertFont();

        void WriteRow(DBFExport dataFileExport, int rowIndex)
        {
            // deleted marker
            this.BinaryWriter.Write(DbfDeletedFlags.Valid);

            // values
            for (int fieldIndex = 0; fieldIndex != dataFileExport.Fields.Count; fieldIndex++)
            {
                IField field = dataFileExport.Fields[fieldIndex];
                IFieldWriter fieldWriter = field.GetFieldWriter(dataFileExport.ExportFormatFactory);

                object value = dataFileExport.DataSource.Rows[rowIndex][field.SourceName];
                if (field is TextField)
                {
                    string ss = value.ToString();
                    fCon.Convert(ref ss, CommonLib.FontIndex.iUNI, CommonLib.FontIndex.iTCV);
                    value = ss;
                }
                fieldWriter.WriteValue(value);
            }
        }

        static void FillDbfFileHeaderStructure(DBFExport dataFileExport, ref DbfHeaderStructure hdr)
        {
            hdr.VersionNumber = DbfConstants.DbfVersionDBase3;
            DateTime lastModifiedDate = DateTime.Now;
            hdr.LastUpdateYear = (byte)(lastModifiedDate.Year % 100);
            hdr.LastUpdateMonth = (byte)lastModifiedDate.Month;
            hdr.LastUpdateDay = (byte)lastModifiedDate.Day;
            hdr.EncryptionFlag = 0;
            hdr.HeaderSize = GetHeaderSize(dataFileExport);
            hdr.LanguageDriverId = DbfLanguageDrivers.WindowsAnsi;
            hdr.MdxFlag = 0;
            hdr.NumberOfRecords = dataFileExport.DataSource.Rows.Count;
            hdr.RecordSize = GetTotalRecordSize(dataFileExport);
            hdr.Reserved1 = 0;
            hdr.IncompleteTransaction = 0;
            hdr.FreeRecordThreadReserved = 0;
            hdr.ReservedMultiUser1 = 0;
            hdr.ReservedMultiUser2 = 0;
            hdr.Reserved2 = 0;
        }

        static short GetTotalRecordSize(DBFExport dataFileExport)
        {
            int totalSize = dataFileExport.Fields.Sum(f => (int)f.TotalSize);
            return (short)(1 + totalSize);
        }

        static short GetHeaderSize(DBFExport dataFileExport)
        {
            return (short)(DbfHeaderStructure.StructureSize
                           + DbfConstants.HeaderTerminatorSize
                           + DbfFieldHeaderStructure.StructureSize * dataFileExport.Fields.Count);
        }

        void IDisposable.Dispose()
        {
            this.BinaryWriter.BaseStream.Dispose();
        }
    }

    public class DbfDBFExportFormatFactory : IDBFExportFormatFactory
    {
        internal DbfDataFileWriter DbfDataFileWriter { get; private set; }

        /// <summary>
        /// Creates a DBF data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A DBF data file writer.</returns>
        public IDataFileWriter CreateWriter(Stream stream)
        {
            if (null != this.DbfDataFileWriter)
                throw new InvalidOperationException("Data file export format factory instance cannot be reused.");
            return this.DbfDataFileWriter = new DbfDataFileWriter(stream);
        }

        /// <summary>
        /// Creates a <see cref="DbfCharacterFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfCharacterFieldWriter"/> field writer</returns>
        public IFieldWriter CreateTextFieldWriter(IField field)
        {
            return new DbfCharacterFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfNumericFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfNumericFieldWriter"/> field writer</returns>
        public IFieldWriter CreateNumericFieldWriter(IField field)
        {
            return new DbfNumericFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfDateFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfDateFieldWriter"/> field writer</returns>
        public IFieldWriter CreateDateFieldWriter(IField field)
        {
            return new DbfDateFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfLogicalFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfLogicalFieldWriter"/> field writer</returns>
        public IFieldWriter CreateBooleanFieldWriter(IField field)
        {
            return new DbfLogicalFieldWriter(this.DbfDataFileWriter, field);
        }

        /// <summary>
        /// Creates a <see cref="DbfCharacterFieldWriter"/> field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A <see cref="DbfCharacterFieldWriter"/> field writer</returns>
        public IFieldWriter CreateGenericFieldWriter(IField field)
        {
            return new DbfCharacterFieldWriter(this.DbfDataFileWriter, field);
        }
    }

    class DbfDateFieldWriter : DbfFieldWriterBase
    {
        public DbfDateFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        { }

        public override char FieldType
        {
            get { return 'D'; }
        }

        public const string DataFormatPattern = "yyyyMMdd";

        public override string FormatValue(object value)
        {
            DateTime dateValue = Convert.ToDateTime(value);
            string formatedValue = dateValue.ToString(DataFormatPattern);
            return formatedValue;
        }
    }

    class DbfNumericFieldWriter : DbfFieldWriterBase
    {
        public DbfNumericFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        { }

        public override char FieldType
        {
            get { return 'N'; }
        }

        public override string FormatValue(object value)
        {
            decimal decimalValue = Convert.ToDecimal(value);
            string decimalPlacesPlaceHolders = String.Empty.PadRight(this.Field.DecimalPlaces, '#');
            string formatString = "#0." + decimalPlacesPlaceHolders;
            string stringValue = decimalValue.ToString(formatString);
            string formatedValue = DbfCommon.GetFixedLengthString(stringValue, this.Field.TotalSize,
                                                              DbfConstants.FieldValuePaddingChar, PaddingPositions.Left);
            return formatedValue;
        }
    }

    class DbfLogicalFieldWriter : DbfFieldWriterBase
    {
        public DbfLogicalFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        { }

        public override char FieldType
        {
            get { return 'L'; }
        }

        public override string FormatValue(object value)
        {
            bool booleanValue = Convert.ToBoolean(value);
            string formattedValue = booleanValue ? DbfLogicalValues.True : DbfLogicalValues.False;
            return formattedValue;
        }
    }

    class DbfCharacterFieldWriter : DbfFieldWriterBase
    {
        public DbfCharacterFieldWriter(DbfDataFileWriter dbfDataFileWriter, IField field)
            : base(dbfDataFileWriter, field)
        { }

        public override char FieldType
        {
            get { return 'C'; }
        }

        public override string FormatValue(object value)
        {
            string stringValue = Convert.ToString(value);
            string fixedLengthValue = DbfCommon.GetFixedLengthString(stringValue, this.Field.TotalSize,
                                                                 DbfConstants.FieldValuePaddingChar,
                                                                 PaddingPositions.Right);
            return fixedLengthValue;
        }
    }

    abstract class DbfFieldWriterBase : IFieldWriter
    {
        public IField Field { get; private set; }

        public abstract char FieldType { get; }

        public DbfDataFileWriter DbfDataFileWriter { get; private set; }

        protected DbfFieldWriterBase(DbfDataFileWriter dbfDataFileWriter, IField field)
        {
            this.DbfDataFileWriter = dbfDataFileWriter;
            this.Field = field;
        }

        public abstract string FormatValue(object value);

        public virtual void WriteValue(object value)
        {
            if (IsNullValue(value))
            {
                WriteNull();
            }
            else
            {
                string formattedValue = this.FormatValue(value);
                var v = Encoding.GetEncoding(Encoding.Default.CodePage).GetBytes(formattedValue);
                this.DbfDataFileWriter.BinaryWriter.Write(v);
            }
        }

        public void WriteHeader(int offsetInRecord)
        {
            DbfFieldHeaderStructure fieldHeaderStructure =
                new DbfFieldHeaderStructure
                {
                    FieldName = this.Field.Name,
                    FieldType = this.FieldType,
                    TotalSize = this.Field.TotalSize,
                    OffsetInRecord = offsetInRecord,
                    DecimalPlaces = this.Field.DecimalPlaces,
                    Reserved1 = 0,
                    WorkAreaId = 0,
                    MultiUser = 0,
                    SetField = 0,
                    Reserved21 = 0,
                    Reserver22 = 0,
                    Reserved23 = 0,
                    IncludeInMdx = 0
                };

            DbfStructureBinaryWriter.Write(this.DbfDataFileWriter.BinaryWriter, fieldHeaderStructure);
        }

        protected bool IsNullValue(object value)
        {
            if (null == value) return true;
            if (DBNull.Value == value) return true;
            return false;
        }

        protected void WriteNull()
        {
            // null values are filled with spaces in DBF
            string nullValueString = DbfCommon.GetFixedLengthString(String.Empty, this.Field.TotalSize, ' ', PaddingPositions.Right);
            this.DbfDataFileWriter.BinaryWriter.Write(Encoding.ASCII.GetBytes(nullValueString));
        }
    }

    public class BooleanField : FieldBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        public BooleanField(string name)
            : base(name, DbfConstants.LogicalFieldLength, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public BooleanField(string name, string sourceName)
            : base(name, DbfConstants.LogicalFieldLength, 0, sourceName)
        { }

        /// <summary>
        /// Gets the field writer instance.
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfLogicalFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateBooleanFieldWriter(this);
        }
    }

    public class DateField : FieldBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        public DateField(string name)
            : base(name, 8, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public DateField(string name, string sourceName)
            : base(name, 8, 0, sourceName)
        { }

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfDateFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateDateFieldWriter(this);
        }
    }

    public class GenericField : FieldBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        public GenericField(string name)
            : base(name, NumericField.MaximumTotalSize, NumericField.MaximumDecimalPlaces)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public GenericField(string name, string sourceName)
            : base(name, NumericField.MaximumTotalSize, NumericField.MaximumDecimalPlaces, sourceName)
        { }

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="IFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateGenericFieldWriter(this);
        }
    }

    public class NumericField : FieldBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places.</param>
        public NumericField(string name, byte totalSize, byte decimalPlaces)
            : base(name, totalSize, decimalPlaces)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with data source field name the same as the field name and with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        public NumericField(string name)
            : base(name, MaximumTotalSize, MaximumDecimalPlaces)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The total size.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <param name="sourceName">The data source field name.</param>
        public NumericField(string name, byte totalSize, byte decimalPlaces, string sourceName)
            : base(name, totalSize, decimalPlaces, sourceName)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericField"/> class with the default total size of 255 and 15 decimal places.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public NumericField(string name, string sourceName)
            : base(name, MaximumTotalSize, MaximumDecimalPlaces, sourceName)
        { }

        /// <summary>
        /// Gets the maximum total size for numeric fields.
        /// </summary>
        public const byte MaximumTotalSize = 255;

        /// <summary>
        /// Gets the maximum decimal places for numeric fields.
        /// </summary>
        public const byte MaximumDecimalPlaces = 15;

        /// <summary>
        /// Gets the field writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfNumericFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateNumericFieldWriter(this);
        }
    }

    public class TextField : FieldBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The field total size.</param>
        public TextField(string name, byte totalSize)
            : base(name, totalSize, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with data source field name the same as the field name and with the default size of 255.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextField(string name)
            : base(name, MaximumTotalSize, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="sourceName">The data source field name.</param>
        public TextField(string name, byte totalSize, string sourceName)
            : base(name, totalSize, 0, sourceName)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class with the default size of 255.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sourceName">The data source field name.</param>
        public TextField(string name, string sourceName)
            : base(name, MaximumTotalSize, 0, sourceName)
        { }

        /// <summary>
        /// Gets the maximum total size for character fields.
        /// </summary>
        public const byte MaximumTotalSize = 255;

        /// <summary>
        /// Gets the character writer instance. 
        /// </summary>
        /// <returns>
        /// A new instance of a <see cref="DbfCharacterFieldWriter"/>.
        /// </returns>
        protected override IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory)
        {
            return exportFormatFactory.CreateTextFieldWriter(this);
        }
    }

    public interface IField
    {
        /// <summary>
        /// Gets or sets the DBF field header name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the source field name.
        /// </summary>
        /// <value>The source field name.</value>
        /// <remarks>
        /// The source field name is used to get data from a <see cref="IDataSource"/>.
        /// </remarks>
        string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the total size of the field values. For numeric type this include the . decimal separator.
        /// </summary>
        /// <value>The total size.</value>
        byte TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the decimal places for numeric types. For other types the value is 0.
        /// </summary>
        /// <value>The decimal places.</value>
        byte DecimalPlaces { get; set; }

        /// <summary>
        /// Gets the field writer.
        /// </summary>
        /// <returns>A IFieldWriter.</returns>
        IFieldWriter GetFieldWriter(IDBFExportFormatFactory exportFormatFactory);
    }

    public abstract class FieldBase : IField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSkeleton"/> class with data source field name the same as the field name.
        /// </summary>
        /// <param name="name">The field name and the data source field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places for numeric types. For other types it should be 0.</param>
        protected FieldBase(string name, byte totalSize, byte decimalPlaces)
            : this(name, totalSize, decimalPlaces, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSkeleton"/> class.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="totalSize">The field total size.</param>
        /// <param name="decimalPlaces">The field decimal places for numeric types. For other types it should be 0.</param>
        /// <param name="sourceName">Name of the source data field name.</param>
        protected FieldBase(string name, byte totalSize, byte decimalPlaces, string sourceName)
        {
            DbfCommon.Against<ArgumentNullException>(name == null, "name");
            DbfCommon.Against<ArgumentNullException>(sourceName == null, "sourceName");
            DbfCommon.Against<ArgumentException>(totalSize <= decimalPlaces, "totalSize shoud be bigger than decimalPlaces");
            DbfCommon.Against<ArgumentException>(decimalPlaces > DbfConstants.FieldMaximumDecimals, "decimalPlaces should be maximum 15");

            this.Name = name;
            this.TotalSize = totalSize;
            this.DecimalPlaces = decimalPlaces;
            this.SourceName = sourceName;
        }

        /// <summary>
        /// Gets or sets the DBF field header name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source name.
        /// </summary>
        /// <value>The source name.</value>
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the total size of the field values. For numeric type this include the . decimal separator.
        /// </summary>
        /// <value>The total size.</value>
        public byte TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the decimal places for numeric types. For other types the value is 0.
        /// </summary>
        /// <value>The decimal places.</value>
        public byte DecimalPlaces { get; set; }

        /// <summary>
        /// Gets the field writer instance. Should be overriden to return a new instance of the field writer. The instance is cached by the <see cref="FieldSkeleton"/> class.
        /// </summary>
        /// <returns>A new instance of a <see cref="IFieldWriter"/> implementation.</returns>
        protected abstract IFieldWriter GetFieldWriterInstance(IDBFExportFormatFactory exportFormatFactory);

        /// <summary>
        /// Gets the cached field writer.
        /// </summary>
        /// <returns>A IFieldWriter.</returns>
        public IFieldWriter GetFieldWriter(IDBFExportFormatFactory exportFormatFactory)
        {
            if (null == _fieldWriter || exportFormatFactory != _cachedFormat)
            {
                _cachedFormat = exportFormatFactory;
                _fieldWriter = GetFieldWriterInstance(exportFormatFactory);
            }
            return _fieldWriter;
        }

        private IFieldWriter _fieldWriter;
        private IDBFExportFormatFactory _cachedFormat;
    }

    public interface IFieldWriter
    {
        /// <summary>
        /// Writes the field header.
        /// </summary>
        /// <param name="offsetInRecord">The offset in record.</param>
        void WriteHeader(int offsetInRecord);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void WriteValue(object value);
    }

    public interface IDataFileWriter : IDisposable
    {
        /// <summary>
        /// Writes the data to the specified stream.
        /// </summary>
        /// <param name="dataFileExport">The data file export definition.</param>
        void Write(DBFExport dataFileExport);
    }

    public interface IDBFExportFormatFactory
    {
        /// <summary>
        /// Creates a data file export writer.
        /// </summary>
        /// <param name="stream">The output stream to which the writer will write to.</param>
        /// <returns>A data file writer.</returns>
        IDataFileWriter CreateWriter(Stream stream);

        /// <summary>
        /// Creates a text field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A text field writer</returns>
        IFieldWriter CreateTextFieldWriter(IField field);

        /// <summary>
        /// Creates a numeric field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A numeric field writer</returns>
        IFieldWriter CreateNumericFieldWriter(IField field);

        /// <summary>
        /// Creates a date field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A date field writer.</returns>
        IFieldWriter CreateDateFieldWriter(IField field);

        /// <summary>
        /// Creates a boolean field writer.
        /// </summary>
        /// <param name="field">The field for which to create the field writer.</param>
        /// <returns>A boolean field writer.</returns>
        IFieldWriter CreateBooleanFieldWriter(IField field);

        /// <summary>
        /// Creates a generic field writer.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>A generic field writer.</returns>
        IFieldWriter CreateGenericFieldWriter(IField field);
    }

    class DbfConstants
    {
        public static readonly byte HeaderTerminator = 0x0D;
        public static readonly int HeaderTerminatorSize = sizeof(byte);

        public static readonly byte RowsEndTerminator = 0x1A;
        public static readonly int RowsEndTerminatorSize = sizeof(byte);

        public static readonly char FieldNamePaddingChar = (char)0;
        public static readonly char FieldValuePaddingChar = ' ';

        public static readonly int FieldNameLength = 11;

        public static readonly byte DateFieldLength = 8;
        public static readonly byte LogicalFieldLength = 1;

        public static readonly byte DbfVersionDBase3 = 0x03;

        public static readonly byte FieldMaximumLength = 255;
        public static readonly byte FieldMaximumDecimals = 15;
    }

    struct DbfHeaderStructure
    {
        public byte VersionNumber;
        public byte LastUpdateYear;
        public byte LastUpdateMonth;
        public byte LastUpdateDay;
        public int NumberOfRecords;
        public short HeaderSize;
        public short RecordSize;
        public short Reserved1;
        public byte IncompleteTransaction;
        public byte EncryptionFlag;
        public int FreeRecordThreadReserved;
        public int ReservedMultiUser1;
        public int ReservedMultiUser2;
        public byte MdxFlag;
        public byte LanguageDriverId;
        public short Reserved2;

        public static readonly int StructureSize = 32; // eg: sizeof(DbfHeaderStructure)
    }

    struct DbfFieldHeaderStructure
    {
        public string FieldName;
        public char FieldType;
        public byte TotalSize;
        public int OffsetInRecord;
        public byte DecimalPlaces;
        public short Reserved1;
        public byte WorkAreaId;
        public short MultiUser;
        public byte SetField;
        public Int32 Reserved21;
        public Int16 Reserver22;
        public byte Reserved23;
        public byte IncludeInMdx;

        public static readonly int StructureSize = 32; // eg: sizeof(DbfFieldHeaderStructure)
    }

    class DbfLanguageDrivers
    {
        public static readonly byte DosUsa = 0x01;
        public static readonly byte DosMultilingual = 0x02;
        public static readonly byte WindowsAnsi = 0x03;
        public static readonly byte StandardMacintosh = 0x04;
        public static readonly byte EeMsDos = 0x64;
        public static readonly byte NordicMsDos = 0x65;
        public static readonly byte RussianMsDos = 0x66;
        public static readonly byte IcelandicMsDos = 0x67;
        public static readonly byte KamenickyCzechMsDos = 0x68;
        public static readonly byte MazoviaPolishMsDos = 0x69;
        public static readonly byte GreekMsDos437G = 0x6A;
        public static readonly byte TurkishMsDos = 0x6B;
        public static readonly byte RussianMacintosh = 0x96;
        public static readonly byte EasternEuropeanMacintosh = 0x97;
        public static readonly byte GreekMacintosh = 0x98;
        public static readonly byte WindowsEe = 0xC8;
        public static readonly byte RussianWindows = 0xC9;
        public static readonly byte TurkishWindows = 0xCA;
        public static readonly byte GreekWindows = 0xCB;
    }

    class DbfDeletedFlags
    {
        public static readonly byte Deleted = Encoding.ASCII.GetBytes("*")[0];
        public static readonly byte Valid = Encoding.ASCII.GetBytes(" ")[0];

        public static readonly int StructureSize = sizeof(byte);
    }

    class DbfLogicalValues
    {
        public const string True = "T";
        public const string False = "F";
    }

    static class DbfStructureBinaryWriter
    {
        public static void Write(BinaryWriter binaryWriter, DbfFieldHeaderStructure fieldHeaderStructure)
        {
            string fieldName = DbfCommon.GetFixedLengthString(fieldHeaderStructure.FieldName,
                                                          DbfConstants.FieldNameLength,
                                                          DbfConstants.FieldNamePaddingChar,
                                                          PaddingPositions.Right);
            binaryWriter.Write(Encoding.ASCII.GetBytes(fieldName));
            binaryWriter.Write(fieldHeaderStructure.FieldType);
            binaryWriter.Write(fieldHeaderStructure.OffsetInRecord);
            binaryWriter.Write(fieldHeaderStructure.TotalSize);
            binaryWriter.Write(fieldHeaderStructure.DecimalPlaces);
            binaryWriter.Write(fieldHeaderStructure.Reserved1);
            binaryWriter.Write(fieldHeaderStructure.WorkAreaId);
            binaryWriter.Write(fieldHeaderStructure.MultiUser);
            binaryWriter.Write(fieldHeaderStructure.SetField);
            binaryWriter.Write(fieldHeaderStructure.Reserved21);
            binaryWriter.Write(fieldHeaderStructure.Reserver22);
            binaryWriter.Write(fieldHeaderStructure.Reserved23);
            binaryWriter.Write(fieldHeaderStructure.IncludeInMdx);
        }

        public static void Write(BinaryWriter binaryWriter, DbfHeaderStructure headerStructure)
        {
            binaryWriter.Write(headerStructure.VersionNumber);
            binaryWriter.Write(headerStructure.LastUpdateYear);
            binaryWriter.Write(headerStructure.LastUpdateMonth);
            binaryWriter.Write(headerStructure.LastUpdateDay);
            binaryWriter.Write(headerStructure.NumberOfRecords);
            binaryWriter.Write(headerStructure.HeaderSize);
            binaryWriter.Write(headerStructure.RecordSize);
            binaryWriter.Write(headerStructure.Reserved1);
            binaryWriter.Write(headerStructure.IncompleteTransaction);
            binaryWriter.Write(headerStructure.EncryptionFlag);
            binaryWriter.Write(headerStructure.FreeRecordThreadReserved);
            binaryWriter.Write(headerStructure.ReservedMultiUser1);
            binaryWriter.Write(headerStructure.ReservedMultiUser2);
            binaryWriter.Write(headerStructure.MdxFlag);
            binaryWriter.Write(headerStructure.LanguageDriverId);
            binaryWriter.Write(headerStructure.Reserved2);
        }
    }

    public enum PaddingPositions
    {
        Left,
        Right
    }

    public class DbfCommon
    {
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion == false)
                return;
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        public static string GetFixedLengthString(string value, int length, char paddingChar, PaddingPositions paddingPosition)
        {
            Against<ArgumentNullException>(value == null, "value");
            Against<ArgumentException>(length < 0, "length < 0");

            string final;

            if (value.Length < length)
            {
                if (paddingPosition == PaddingPositions.Right)
                {
                    final = value.PadRight(length, paddingChar);
                }
                else if (paddingPosition == PaddingPositions.Left)
                {
                    final = value.PadLeft(length, paddingChar);
                }
                else
                {
                    throw new NotImplementedException(paddingPosition.ToString());
                }
            }
            else if (value.Length > length)
            {
                final = value.Substring(0, length);
            }
            else
            {
                final = value;
            }

            return final;
        }
    }
    #endregion
}
