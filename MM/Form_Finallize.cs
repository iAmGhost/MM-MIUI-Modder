using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Utility.IO;

namespace MM
{
    public partial class Form_Finallize : NewForm
    {
        NewForm adbWindow, zipWindow;
        public Form_Finallize()
        {
            InitializeComponent();
        }

        public override void OnPageShow()
        {
            PrefsManager prefs = PrefsManager.GetInstance();

            prefs.GladPrevPage = 0;
            prefs.GladNextPage = 0;
            SetButtonStatus(NavigationButton.Prev, true);
            SetButtonStatus(NavigationButton.Next, false);

            if (prefs.GetPrefString("AutopilotMode") == "true")
            {
                FileSystem.copyDirectory(Application.StartupPath + "/Out", Application.StartupPath + "/Workspace");
                int maxCount = prefs.GetPrefInt("AutopilotMax");
                int currentCount = prefs.GetPrefInt("AutopilotCurrent");

                if (currentCount == maxCount - 1)
                {
                    prefs.SetPrefString("AutopilotMode", "false");
                    prefs.SetPrefString("AutopilotModeWantsBack", "true");
                    prefs.SetPrefString("AutopilotModeFinished", "true");
                }
                else
                {
                    prefs.SetPrefInt("AutopilotCurrent", currentCount+1);
                    prefs.SetPrefString("AutopilotModeWantsBack", "true");
                }
            }
        }

        private void button_sendWithAdb_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().SetPrefString("AdbMode", "push");
            PrefsManager.GetInstance().SetPrefString("AdbFileMode", "Partial");
            if (adbWindow == null || adbWindow.IsDisposed)
                adbWindow = new Form_AdbFileWindow();
            adbWindow.Show();
            adbWindow.OnPageShow();
        }
        public override void OnPageClose()
        {
            if (zipWindow != null)
                zipWindow.Hide();

            if (adbWindow != null)
                adbWindow.Hide();
        }

        private void button_Packaging_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().SetPrefString("ZipPackagingMode", "Partial");

            if (zipWindow == null || zipWindow.IsDisposed)
                zipWindow = new Form_ZipPackaging();
            zipWindow.Show();
            zipWindow.OnPageShow();
        }

        private void button_SaveFiles_Click(object sender, EventArgs e)
        {
            FileSystem.copyDirectory(Application.StartupPath + "/Out", Application.StartupPath + "/Workspace");
            MessageBox.Show(lang.GetString("Saved"), "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form_Finallize_Load(object sender, EventArgs e)
        {
            label_ModdingFinished.Text = lang.GetString("Modding_Finished");
            label_ModdingFinishedCaption.Text = lang.GetString("Modding_Finished_Caption");
            
            button_sendWithAdb.Text = lang.GetString("AdbFileWindow_Button");
            label_AdbFileWindow_Desc.Text = lang.GetString("AdbFileWindow_Desc");

            button_Packaging.Text = lang.GetString("ZipPackaging_Button");
            label_ZipPackaging_Desc.Text = lang.GetString("ZipPackaging_Desc");

            button_SaveFiles.Text = lang.GetString("Workspace_Save_Button");
            label_Workspace_Save_Desc.Text = lang.GetString("Workspace_Save_Desc");
        }
    }
}
