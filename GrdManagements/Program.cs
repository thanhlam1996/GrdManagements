using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Linq;

namespace GrdManagements
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                GrdUI.User._UserID = "";
                GrdUI.User._UserPass = "";
                GrdUI.User._UserGroup = "";
                GrdUI.User._ParaFromUIS = new string[] { };

                List<CommandLine> cmdLine = new List<CommandLine>();
                if (args != null)
                {
                    if (args.Contains("?") || args.Contains("-help"))
                    {
                        MessageBox.Show("-f : File thực thi\n"
                            + "-u: Username đăng nhập tự động vào ứng dụng\n"
                            + "-p: Password đăng nhập tự động vào ứng dụng\n"
                            + "-g: Nhóm sử dụng\n"
                            + "-form: Form tự động mở sau khi đăng nhập\n"
                            );
                        return;
                    }
                    if (args.Any(p => new string[] { "-u", "-p", "-form" }.Any(q => p.ToLower() == q)))
                    {
                        foreach (var cmd in args)
                        {
                            switch (cmd)
                            {
                                case "-f":
                                case "-u":
                                case "-p":
                                case "-g":
                                case "-form":
                                    {
                                        cmdLine.Add(new CommandLine { Name = cmd, Value = "" });
                                    }
                                    break;
                                default:
                                    {
                                        if (cmdLine.Count == 0)
                                        {
                                            cmdLine.Add(new CommandLine { Name = "-form", Value = "" });
                                        }

                                        cmdLine[cmdLine.Count - 1].Value += cmd;
                                    }
                                    break;
                            }
                        }
                        CommandLine cmd1 = cmdLine.FirstOrDefault(c => c.Name.ToLower() == "-u");
                        if (cmd1 != null)
                            GrdUI.User._UserID = cmd1.Value;
                        cmd1 = cmdLine.FirstOrDefault(c => c.Name.ToLower() == "-p");
                        if (cmd1 != null)
                            GrdUI.User._UserPass = cmd1.Value;
                        cmd1 = cmdLine.FirstOrDefault(c => c.Name.ToLower() == "-form");
                        if (cmd1 != null)
                            GrdUI.User._ParaFromUIS = new string[] { cmd1.Value };
                        cmd1 = cmdLine.FirstOrDefault(c => c.Name.ToLower() == "-g");
                        if (cmd1 != null)
                            GrdUI.User._UserGroup = cmd1.Value;
                    }
                    else
                    {
                        if (args.Length > 0)
                            GrdUI.User._UserID = args[0];
                        else
                            GrdUI.User._UserID = "";
                        if (args.Length > 1)
                            GrdUI.User._UserPass = args[1];
                        else
                            GrdUI.User._UserPass = "";
                        if (args.Length > 2)
                            GrdUI.User._UserGroup = args[2];
                        else
                            GrdUI.User._UserGroup = "";
                        if (GrdUI.User._ParaFromUIS == null)
                            GrdUI.User._ParaFromUIS = new string[] { };
                        if (args.Length > 3)
                        {
                            for (int i = 3; i < args.Length; i++)
                            {
                                if (args[i].Trim() != "")
                                {
                                    if (GrdUI.User._ParaFromUIS.Length == 0)
                                    {
                                        Array.Resize(ref GrdUI.User._ParaFromUIS, GrdUI.User._ParaFromUIS.Length + 1);
                                    }
                                    GrdUI.User._ParaFromUIS[0] = args[i];
                                }
                            }
                        }
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.UserSkins.BonusSkins.Register();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

                if (Form.ModifierKeys == Keys.Shift)
                {
                    GrdUI.frm_Grd_ConnectDatabase frm = new GrdUI.frm_Grd_ConnectDatabase();
                    frm.ShowDialog();
                }

                Application.Run(new GrdUI.rfrm_Grd_Main());
            }
            catch (Exception ex) { }
        }

        class CommandLine
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}