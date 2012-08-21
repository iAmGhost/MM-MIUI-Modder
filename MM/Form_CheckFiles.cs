using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MM
{
    public partial class Form_CheckFiles : NewForm
    {
        public ModInfo modInfo;
        Form_AdbFileWindow adbWindow = null;

        public Form_CheckFiles()
        {
            InitializeComponent();
        }

        public override void OnPageShow()
        {
            SetButtonStatus(NavigationButton.Prev, true);
            SetButtonStatus(NavigationButton.Next, false);

            modInfo = new ModInfo(Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModInfo.ini");

            modTitleLabel.Text = modInfo.Title + " " + modInfo.Version + " - by " + modInfo.Author + "\r\n\r\n";

            CheckFiles();
        }

        private void CheckFiles()
        {
            debugClear();
            debugOut(lang.GetString("Required_Files"));

            string status;
            int missingFileCount = 0;

            string[] allFiles = new string[modInfo.RequiredFiles.Length + modInfo.ModifiedFiles.Length];
            modInfo.RequiredFiles.CopyTo(allFiles, 0);
            modInfo.ModifiedFiles.CopyTo(allFiles, modInfo.RequiredFiles.Length);

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
                if (file == "") continue;
                if (File.Exists(Application.StartupPath + "/Workspace/" + file))
                {
                    status = "(O)";
                }
                else
                {
                    status = "(X)";
                    missingFileCount++;
                }
                debugOut(file + " - " + status);
            }

            debugOut("");
            if (missingFileCount != 0)
            {
                debugOut(String.Format(lang.GetString("Missing_Files"),missingFileCount.ToString()));
                SetButtonStatus(NavigationButton.Next, false);
            }
            else
            {
                debugOut(lang.GetString("All_Files_Checked"));
                SetButtonStatus(NavigationButton.Next, true);
            }
        }

        private void debugOut(string content)
        {
            debugList.Items.Add(content);
        }

        private void debugClear()
        {
            debugList.Items.Clear();
        }

        private void Form_CheckFiles_Load(object sender, EventArgs e)
        {
            button_Check.Text = lang.GetString("Check_Files");
            button_GetFromPhone.Text = lang.GetString("Get_Files_From_Phone");
            button_OpenWorkspace.Text = lang.GetString("Open_Workspace");
        }

        private void button_OpenWorkspace_Click(object sender, EventArgs e)
        {
            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Workspace"));
            Process.Start(Application.StartupPath + "/Workspace");
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            CheckFiles();
        }

        private void button_GetFromPhone_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().SetPrefString("AdbMode", "pull");
            if (adbWindow == null)
                adbWindow = new Form_AdbFileWindow();
            adbWindow.Show();
            adbWindow.OnPageShow();
        }

        public override void OnPageClose()
        {
            if (adbWindow != null)
                adbWindow.Hide();
        }
    }
}
