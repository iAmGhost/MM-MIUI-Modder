using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM
{
    public partial class Form_Main : NewForm
    {        
        List<NewForm> forms = new List<NewForm>();
        NewForm currentForm;

        int page = 0;

        public Form_Main()
        {

            InitializeComponent();
            forms.Add(new Form_Welcome());
            forms.Add(new Form_SelectMod());
            forms.Add(new Form_CheckFiles());
            forms.Add(new Form_RunModScript());
            forms.Add(new Form_Finallize());
            forms.Add(new Form_Tools());
            ChangeForm(0);

            FormManager.GetInstance().SetMainForm(this);
        }

        private void button_Next_Click(object sender, EventArgs e)
        {

            if (PrefsManager.GetInstance().GladNextPage != -1)
            {
                page = PrefsManager.GetInstance().GladNextPage;
                PrefsManager.GetInstance().GladNextPage = -1;
                PrefsManager.GetInstance().GladPrevPage = -1;
                ChangeForm(page);

            }
            else if (ChangeForm(page + 1))
            {
                page++;
            }
        }

        private void button_Prev_Click(object sender, EventArgs e)
        {
            if (PrefsManager.GetInstance().GladPrevPage != -1)
            {
                page = PrefsManager.GetInstance().GladPrevPage;
                PrefsManager.GetInstance().GladNextPage = -1;
                PrefsManager.GetInstance().GladPrevPage = -1;
                ChangeForm(page);
            }
            else if (ChangeForm(page - 1))
            {
                page--;
            }
        }

        private bool ChangeForm(int page)
        {
            if (page < forms.Count && page > -1)
            {
                if (currentForm != null) currentForm.OnPageClose();
                NewForm newForm = forms[page];
                newForm.SetButtonStatus = ChangeButtonStatus;
                newForm.TopLevel = false;
                newForm.Visible = true;
                newForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                newForm.OnPageShow();
                panel.Controls.Clear();
                panel.Controls.Add(newForm);
                currentForm = newForm;
                return true;
            }
            return false;
        }


        public void ChangeButtonStatus(NavigationButton button, bool enabled)
        {

            if ( button == NavigationButton.Prev )
            {
                button_Prev.Enabled = enabled;
            }
            else if (button == NavigationButton.Next)
            {
                button_Next.Enabled = enabled;
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Text += " " + PrefsManager.GetInstance().GetPrefString("Version");
            button_Next.Text = lang.GetString("Next");
            button_Prev.Text = lang.GetString("Prev");
        }

        private void autoPilotTimer_Tick(object sender, EventArgs e)
        {
            PrefsManager prefs = PrefsManager.GetInstance();
            if (prefs.GetPrefString("AutopilotMode") == "true")
            {
                button_Next.PerformClick();
            }
            if (prefs.GetPrefString("AutopilotModeWantsBack") == "true")
            {
                button_Prev.PerformClick();
                prefs.SetPrefString("AutopilotModeWantsBack", "false");
            }

            if(prefs.GetPrefString("AutopilotModeFinished") == "true")
            {
                string result = lang.GetFormattedString("Autopilot_Finished", String.Join("\r\n", prefs.GetPrefStringArray("AutopilotMods")));
                prefs.SetPrefString("AutopilotModeFinished", "false");
                MessageBox.Show(result, "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
