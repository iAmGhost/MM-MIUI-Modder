using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MM
{
    public partial class Form_Welcome : NewForm
    {
        public Form_Welcome()
        {
            InitializeComponent();
        }
        public override void OnPageShow()
        {
            SetButtonStatus(NavigationButton.Prev, false);
            SetButtonStatus(NavigationButton.Next, true);
            PrefsManager.GetInstance().GladNextPage = 1;
        }

        private void button_Tools_Click(object sender, EventArgs e)
        {
            PrefsManager.GetInstance().GladNextPage = 5;
            MessageBox.Show(lang.GetString("Press_Next_Button"), "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button_Tools.Text = lang.GetString("Tools");
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
