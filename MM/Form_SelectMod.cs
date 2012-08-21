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
using aejw;

namespace MM
{
    public partial class Form_SelectMod : NewForm
    {
        public Form_SelectMod()
        {
            InitializeComponent();
        }

        public override void OnPageShow()
        {
            SetButtonStatus(NavigationButton.Prev, true);
            SetButtonStatus(NavigationButton.Next, false);

            modList.Items.Clear();
            modList.ClearSelected();
            modInfoText.Text = "";

            string[] modInfoFiles = null;

            try
            {
                modInfoFiles = Directory.GetFiles(Environment.CurrentDirectory + "/Mods", "ModInfo.ini", SearchOption.AllDirectories);
            }
            catch
            {

            }
            if (modInfoFiles == null)
            {
                MessageBox.Show("설치된 모드가 없습니다!", "MM!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ModInfo modInfo;

            string modScriptPath;

            foreach (string modInfoFile in modInfoFiles)
            {
                modScriptPath = new FileInfo(modInfoFile).Directory.ToString() + "/ModScript.mmm";
                if (File.Exists(modScriptPath))
                {   
                    modInfo = new ModInfo(modInfoFile);
                    if (File.Exists(Application.StartupPath + "/Mods/" + modInfo.DirName + "/ModInfo.ini") && File.Exists(Application.StartupPath + "/Mods/" + modInfo.DirName + "/ModScript.mmm"))
                    {
                        modList.Items.Add(modInfo);
                    }
                }
            }
            PrefsManager prefs = PrefsManager.GetInstance();

            if ( prefs.GetPrefString("AutopilotMode") == "true")
            {
                string currentMode = prefs.GetPrefStringArray("AutopilotMods")[prefs.GetPrefInt("AutopilotCurrent")];

                int i = 0;

                foreach (ModInfo item in modList.Items)
                {
                    if (item.DirName == currentMode)
                    {
                        modList.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
        }

        private void Form_SelectMod_Load(object sender, EventArgs e)
        {
            label_SelectMod.Text = lang.GetString("Select_Mod");
        }

        private void modList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modList.SelectedIndex == -1) return;

            PrefsManager prefManager = PrefsManager.GetInstance();

            ModInfo modInfo = (ModInfo)modList.Items[modList.SelectedIndex];
            string text = "";
            text += modInfo.Title + " " + modInfo.Version + " - by " + modInfo.Author + "\r\n\r\n";
            if (modInfo.Homepage != "") text += lang.GetString("Homepage") + ": \r\n" + modInfo.Homepage + "\r\n\r\n"; 
            text += lang.GetString("Modified_Files") + ": \r\n" + String.Join("\r\n",modInfo.ModifiedFiles) + "\r\n\r\n";
            text += lang.GetString("Required_Files") + ": \r\n" + String.Join("\r\n", modInfo.RequiredFiles) + "\r\n\r\n";
            text += lang.GetString("Description") + ": \r\n" + modInfo.Description;
            modInfoText.Text = text;

            prefManager.SetPrefString("SelectedMod", modInfo.DirName);

            SetButtonStatus(NavigationButton.Next, true);
        }
        private void modInfoText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

    }
}
