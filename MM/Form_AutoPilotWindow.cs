using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;



namespace MM
{
    public partial class Form_AutoPilotWindow : NewForm
    {
        public Form_AutoPilotWindow()
        {
            InitializeComponent();
        }
        public override void OnPageShow()
        {
            modList.Items.Clear();
            modList.ClearSelected();

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
                    modList.Items.Add(modInfo);
                }
            }
        }

        private void button_SetAutopilotMode_Click(object sender, EventArgs e)
        {
            string[] selectedMods = new string[modList.Items.Count];

            int i = 0;

            foreach (ModInfo item in modList.Items)
            {
                selectedMods[i] = item.DirName;
                i++;
            }

            PrefsManager prefs = PrefsManager.GetInstance();

            prefs.SetPrefStringArray("AutopilotMods", selectedMods);
            prefs.SetPrefString("AutopilotMode", "true");
            prefs.SetPrefInt("AutopilotCurrent", 0);
            prefs.SetPrefInt("AutopilotMax", selectedMods.Length);

            MessageBox.Show(lang.GetString("Autopilot_Start"), "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void modList_SelectedIndexChanged(object sender, EventArgs e)
        {
            modExcludeList.ClearSelected();
        }

        private void modExcludeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            modList.ClearSelected();
        }

        private void button_IncludeExclude_Click(object sender, EventArgs e)
        {
            int currentIndex = modList.SelectedIndex;

            if (currentIndex >= 0)
            {
                modExcludeList.Items.Add(modList.SelectedItem);
                modList.Items.Remove(modList.SelectedItem);

                if (modList.Items.Count - 1 > currentIndex)
                {
                    modList.SelectedIndex = currentIndex;
                }
                else
                {
                    modList.SelectedIndex = modList.Items.Count - 1;
                }
            }
            currentIndex = modExcludeList.SelectedIndex;

            if (currentIndex >= 0)
            {
                modList.Items.Add(modExcludeList.SelectedItem);
                modExcludeList.Items.Remove(modExcludeList.SelectedItem);

                if (modExcludeList.Items.Count - 1 > currentIndex)
                {
                    modExcludeList.SelectedIndex = currentIndex;
                }
                else
                {
                    modExcludeList.SelectedIndex = modExcludeList.Items.Count - 1;
                }
            }
        }

        private void Form_AutoPilotWindow_Load(object sender, EventArgs e)
        {
            LangManager lang = LangManager.GetInstance();
            label_Autopilot_Info.Text = lang.GetString("Autopilot_Info");
            label_IncludeModList.Text = lang.GetString("Autopilot_Include");
            label_ExcludeModList.Text = lang.GetString("Autopilot_Exclude");
            button_SetAutopilotMode.Text = lang.GetString("OK");
            this.Text = lang.GetString("Autopilot");
        }
    }
}
