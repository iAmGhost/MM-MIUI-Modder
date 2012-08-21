using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MM
{
    public partial class Form_AdbFileWindow : NewForm
    {
        string adbMode = "pull";

        public Form_AdbFileWindow()
        {
            InitializeComponent();
        }

        private void Form_AdbFileGetWindow_Load(object sender, EventArgs e)
        {
            this.Text = lang.GetString("File_Manager");
            button_CheckAll.Text = lang.GetString("Select_All");
            button_UncheckAll.Text = lang.GetString("Deselect_All");
            label_Caution.Text = lang.GetString("File_Manager_Caution");
        }

        public override void OnPageShow()
        {
            debugTextBox.Text = "";
            fileListBox.Items.Clear();

            adbMode = PrefsManager.GetInstance().GetPrefString("AdbMode");

            if (adbMode == "pull")
            {
                button_PullPushFiles.Text = lang.GetString("Pull_Files");
            }
            else
            {
                button_PullPushFiles.Text = lang.GetString("Push_Files");
            }

            ProcessStartInfo adbStartInfo = new ProcessStartInfo(Application.StartupPath + "/bin/adb.exe");
            adbStartInfo.Arguments = "devices";
            adbStartInfo.UseShellExecute = false;
            adbStartInfo.RedirectStandardOutput = true;
            adbStartInfo.CreateNoWindow = true;
            Process adbDaemon = Process.Start(adbStartInfo);

            ModInfo modInfo = new ModInfo(Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModInfo.ini");
            string workspace = Application.StartupPath + "/Workspace";

            string[] allFiles = null;

            if (adbMode == "pull")
            {
                allFiles = new string[modInfo.RequiredFiles.Length + modInfo.ModifiedFiles.Length];
                modInfo.ModifiedFiles.CopyTo(allFiles, 0);
                modInfo.RequiredFiles.CopyTo(allFiles, modInfo.ModifiedFiles.Length);

            }
            else
            {
                if (PrefsManager.GetInstance().GetPrefString("AdbFileMode") == "All")
                {
                    string[] files = Directory.GetFiles(Application.StartupPath + "/Workspace", "*.*", SearchOption.AllDirectories);

                    allFiles = new string[files.Length];

                    int length = (Application.StartupPath + "/Workspace").Length;
                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i] = files[i].Substring(length).Replace("\\", "/");
                    }

                    files.CopyTo(allFiles, 0);
                }
                else
                {
                    allFiles = new string[modInfo.ModifiedFiles.Length];
                    modInfo.ModifiedFiles.CopyTo(allFiles, 0);
                }
            }

            List<String> allFilesList = new List<string>();

            foreach (string file in allFiles)
            {
                if (!allFilesList.Contains(file))
                {
                    allFilesList.Add(file);
                }
            }

            allFiles = allFilesList.ToArray();

            foreach (string file in allFiles)
            {
                bool chk = !File.Exists(workspace + "/" + file);
                if ( adbMode == "push" ) chk = true;
                if (file != "")
                {
                    fileListBox.Items.Add(file, chk);
                }
            }
        }
        private void button_CheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fileListBox.Items.Count; i++)
            {
                fileListBox.SetItemChecked(i, true);
            }
        }

        private void button_UncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fileListBox.Items.Count; i++)
            {
                fileListBox.SetItemChecked(i, false);
            }
        }

        private void button_PullFiles_Click(object sender, EventArgs e)
        {
            debugTextBox.Text = "";
            ProcessStartInfo adbStartInfo = new ProcessStartInfo(Application.StartupPath + "/bin/adb.exe");
            adbStartInfo.Arguments = "devices";
            adbStartInfo.RedirectStandardOutput = true;
            adbStartInfo.RedirectStandardError = true;
            adbStartInfo.UseShellExecute = false;
            adbStartInfo.CreateNoWindow = true;
            Process adbDaemon = Process.Start(adbStartInfo);

            adbDaemon.WaitForExit();

            AdbCommand("root");

            string output = adbDaemon.StandardOutput.ReadToEnd();
            if (!Regex.IsMatch(output, ".*device\r\n"))
            {
                debugTextBox.Text += lang.GetString("No_Device_Connected") + "\r\n";
                return;
            }
            else
            {
                debugTextBox.Text += lang.GetString("Device_Connected") + "\r\n";
            }

            string fileListItem, source, target = "";

            for (int i = 0; i < fileListBox.Items.Count; i++)
            {
                target = "";
                source = "";
                if (fileListBox.GetItemCheckState(i) == CheckState.Unchecked) continue;
                fileListItem = fileListBox.Items[i].ToString();

                if (adbMode == "pull")
                {
                    source = fileListItem;
                    target = Application.StartupPath + "/Workspace" + fileListItem;
                }
                else
                {
                    if (PrefsManager.GetInstance().GetPrefString("AdbFileMode") == "All")
                    {
                        source = Application.StartupPath + "/Workspace" + fileListItem;
                    }
                    else
                    {
                        source = Application.StartupPath + "/Out" + fileListItem;
                    }
                    string[] split = fileListItem.Split('/');
                    for ( int j = 0; j < split.Length - 1; j++ )
                    {
                        target += split[j] + '/';
                    }
                }
                debugTextBox.AppendText(adbMode + ": " + fileListItem + "\r\n");
                if (adbMode == "push")
                {
                    AdbCommand("-d remount");
                }
                AdbCommand(string.Format("{0} \"{1}\" \"{2}\"", adbMode, source, target));
                if (adbMode == "push")
                {
                    AdbCommand(string.Format("-d shell chmod 644 {0}", fileListItem));
                }
            }

            if (adbMode == "push")
            {
                DialogResult result = MessageBox.Show(lang.GetString("Push_Finished"), "MM!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    AdbCommand("-d shell am start -a android.intent.action.MAIN -n bdjnk.android.wakeydroid/.Wakey", false);
                    AdbCommand("-d shell stop;sleep 5;start");
                }

            }
            else
            {
                debugTextBox.AppendText(lang.GetString("Pull_Finished"));
            }


        }
        private void AdbCommand(string command, bool append = true)
        {
            ProcessStartInfo adbStartInfo = new ProcessStartInfo(Application.StartupPath + "/bin/adb.exe");
            adbStartInfo.Arguments = command;
            adbStartInfo.UseShellExecute = false;
            adbStartInfo.RedirectStandardOutput = true;
            adbStartInfo.RedirectStandardError = true;
            adbStartInfo.CreateNoWindow = true;
            Process adb = Process.Start(adbStartInfo);

            //adb.BeginOutputReadLine();
            //adb.OutputDataReceived += new DataReceivedEventHandler(adb_OutputDataReceived);
            //adb.ErrorDataReceived += new DataReceivedEventHandler(adb_OutputDataReceived);

            adb.WaitForExit();

            if (append)
            {
                debugTextBox.AppendText(adb.StandardOutput.ReadToEnd());
                debugTextBox.AppendText(adb.StandardError.ReadToEnd());
            }
        }

        private void adb_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                debugTextBox.AppendText(e.Data + "\r\n" ?? string.Empty);
            }));

        }

        private void Form_AdbFileGetWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form_AdbFileGetWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
