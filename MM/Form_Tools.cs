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
using Utility.IO;


namespace MM
{
    public partial class Form_Tools : NewForm
    {
        NewForm zipWindow, adbWindow, autoPilotWindow;
        public Form_Tools()
        {
            InitializeComponent();
        }

        private void button_PackageAll_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().SetPrefString("ZipPackagingMode", "All");

            if (zipWindow == null || zipWindow.IsDisposed)
                zipWindow = new Form_ZipPackaging();
            zipWindow.Show();
            zipWindow.OnPageShow();
        }

        public override void OnPageShow()
        {
            PrefsManager.GetInstance().GladPrevPage = 0;
            PrefsManager.GetInstance().GladNextPage = 0;
            SetButtonStatus(NavigationButton.Prev, true);
            SetButtonStatus(NavigationButton.Next, false);
        }
        
        public override void OnPageClose()
        {
            if (zipWindow != null)
                zipWindow.Hide();

            if (adbWindow != null)
                adbWindow.Hide();

            if (autoPilotWindow != null)
                autoPilotWindow.Hide();
        }

        private void button_ShowWorkspace_Click(object sender, EventArgs e)
        {
            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Workspace"));
            Process.Start(Application.StartupPath + "/Workspace");
        }

        private void button_AdbAllPush_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().SetPrefString("AdbMode", "push");
            PrefsManager.GetInstance().SetPrefString("AdbFileMode", "All");
            if (adbWindow == null || adbWindow.IsDisposed)
                adbWindow = new Form_AdbFileWindow();
            adbWindow.Show();
            adbWindow.OnPageShow();
        }

        private void Form_Tools_Load(object sender, EventArgs e)
        {
            label_Tools.Text = lang.GetString("Tools");
            
            label_AdbFileWindow_Batch_Desc.Text = lang.GetString("AdbFileWindow_Batch_Desc");
            button_AdbAllPush.Text = lang.GetString("AdbFileWindow_Batch_Button");

            label_Open_Workspace_Desc.Text = lang.GetString("Open_Workspace_Desc");
            button_ShowWorkspace.Text = lang.GetString("Open_Workspace");

            label_ZipPackaging_Batch_Desc.Text = lang.GetString("ZipPackaging_Batch_Desc");
            button_PackageAll.Text = lang.GetString("ZipPackaging_Batch_Button");

            label_SaveFilesBatch_Desc.Text = lang.GetString("Workspace_Save_Previous");
            button_SaveFiles.Text = lang.GetString("Workspace_Save_Button");

            label_Autopilot.Text = lang.GetString("Autopilot_Desc");
            button_AutoPilot.Text = lang.GetString("Autopilot");
        }

        private void button_SaveFiles_Click(object sender, EventArgs e)
        {
            FileSystem.copyDirectory(Application.StartupPath + "/Out", Application.StartupPath + "/Workspace");
            MessageBox.Show(lang.GetString("Saved"), "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_AutoPilot_Click(object sender, EventArgs e)
        {
            if (autoPilotWindow == null || autoPilotWindow.IsDisposed)
                autoPilotWindow = new Form_AutoPilotWindow();
            autoPilotWindow.Show();
            autoPilotWindow.OnPageShow();
        }
    }
}
