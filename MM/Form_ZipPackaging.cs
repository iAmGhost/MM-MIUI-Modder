using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;


namespace MM
{
    public partial class Form_ZipPackaging : NewForm
    {
        int checkedFileCount = 0;
        ModInfo modInfo;

        public Form_ZipPackaging()
        {
            InitializeComponent();
        }

        public override void OnPageShow()
        {
            modInfo = new ModInfo(Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModInfo.ini");
            string workspace = Application.StartupPath + "/Workspace";

            string[] allFiles = null;
            if (PrefsManager.GetInstance().GetPrefString("ZipPackagingMode") == "All")
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

            foreach (string file in allFiles)
            {
                fileListBox.Items.Add(file, true);
            }

            updatePatchInfo();

        }

        private void updatePatchInfo()
        {
            textBox_PatchInfo.Text = "";

            textBox_PatchInfo.AppendText("-------------------------------\r\n");

            if (PrefsManager.GetInstance().GetPrefString("ZipPackagingMode") == "All")
            {
                textBox_PatchInfo.AppendText("MM! Full Package\r\n");
            }
            else
            {
                textBox_PatchInfo.AppendText(string.Format("{0} {1}", modInfo.Title, modInfo.Version) + "\r\n");
                textBox_PatchInfo.AppendText(string.Format("by {0} {1}", modInfo.Author, modInfo.Homepage) + "\r\n");
            }

            textBox_PatchInfo.AppendText("Modified File(s):" + "\r\n");

            foreach (string file in fileListBox.CheckedItems)
            {
                textBox_PatchInfo.AppendText(file + "\r\n");
            }

            textBox_PatchInfo.AppendText("-------------------------------");
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            string zipFileNameSigned = "";

            if (PrefsManager.GetInstance().GetPrefString("ZipPackagingMode") == "All")
            {
                string.Format("{0}_{1}_Signed.zip", modInfo.Title.Replace(" ", "_"), modInfo.Version);
            }
            else
            {
                string.Format("MM_Full_Package_Signed.zip", modInfo.Title.Replace(" ", "_"), modInfo.Version);
                
            }
            
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.FileName = zipFileNameSigned;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string savefilePath = saveFileDialog1.FileName;

            StringBuilder updaterScript = new StringBuilder();

            string[] lines = textBox_PatchInfo.Text.Split('\n');

            foreach (string line in lines)
            {
                updaterScript.Append(string.Format("ui_print(\"{0}\");\n", line.Trim()));
            }

            updaterScript.Append("show_progress(0.1, 0);\n");
            updaterScript.Append("\n");
            updaterScript.Append("ui_print(\"Mounting /system...\");\n");

            updaterScript.Append("package_extract_file(\"siam\", \"/tmp/siam.sh\");");
            updaterScript.Append("set_perm(0, 0, 0777, \"/tmp/siam.sh\");");
            updaterScript.Append("run_program(\"/tmp/siam.sh\");");
            updaterScript.Append("assert(is_mounted(\"/system\"));");
            updaterScript.Append("\n");
            updaterScript.Append("ui_print(\"Extracting file(s)...\");\n");
            updaterScript.Append("package_extract_dir(\"system\", \"/system\");\n");
            updaterScript.Append("\n");
            updaterScript.Append("ui_print(\"Setting Permission(s)...\")\n;");

            string[] allFiles = null;

            allFiles = new string[modInfo.ModifiedFiles.Length];
            modInfo.ModifiedFiles.CopyTo(allFiles, 0);

            foreach (string file in allFiles)
            {
                updaterScript.Append(string.Format("set_perm(0, 0, 0644, \"{0}\");\n", file));
            }

            updaterScript.Append("\n");
            updaterScript.Append("ui_print(\"Unmounting /system...\");\n");
            updaterScript.Append("unmount(\"/system\");\n");
            updaterScript.Append("\n");
            updaterScript.Append("show_progress(0.1, 0);\n");
            updaterScript.Append("\n");
            updaterScript.Append("ui_print(\"-------------------------------\");\n");
            updaterScript.Append("ui_print(\"Installation Finished!\");\n");

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));

            StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "/Temp/updater-script", false, Encoding.GetEncoding(949));

            streamWriter.Write(updaterScript.ToString());

            streamWriter.Close();

            string zipFilePath = string.Format("{0}/Temp/{1}_{2}.zip", Application.StartupPath, modInfo.Title.Replace(" ", "_"), modInfo.Version);

            if (File.Exists(zipFilePath))
                File.Delete(zipFilePath);

            using (ZipFile zip = new ZipFile())
            {
                foreach (string path in fileListBox.CheckedItems)
                {
                    string upperPath = Path.GetDirectoryName(path);

                    if (PrefsManager.GetInstance().GetPrefString("ZipPackagingMode") == "All")
                    {
                        zip.AddFile(Application.StartupPath + "/Workspace" + path, upperPath);
                    }
                    else
                    {
                        zip.AddFile(Application.StartupPath + "/Out" + path, upperPath);
                    }
                }
                zip.AddFile(Application.StartupPath + "/Bin/siam", "/");
                zip.AddFile(Application.StartupPath + "/Bin/update-binary", "/META-INF/com/google/android");
                zip.AddFile(Application.StartupPath + "/Temp/updater-script", "/META-INF/com/google/android");
                zip.SaveProgress += new EventHandler<SaveProgressEventArgs>(zip_SaveProgress);
                zip.Save(zipFilePath);
            }
            
            RunProgram("java", "-jar signapk.jar testkey.x509.pem testkey.pk8 " +
                                        "\"" + zipFilePath + "\" " +
                                        "\"" + saveFileDialog1.FileName + "\"",
                                        Application.StartupPath + "/Bin");

            progressBar1.Value = progressBar1.Maximum;

            MessageBox.Show(lang.GetString("Saved"), "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        void zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                progressBar1.Maximum = e.EntriesTotal;
                progressBar1.Value = e.EntriesSaved;
            }
        }

        private string RunProgram(string path, string args, string workingDirectory)
        {
            string outString = "";

            path = path.Replace("/", "\\");
            args = args.Replace("/", "\\");

            //Debug.WriteLine(args);

            Process newProgram = new Process();
            newProgram.StartInfo.FileName = path;
            newProgram.StartInfo.Arguments = args;
            newProgram.StartInfo.CreateNoWindow = true;
            newProgram.StartInfo.UseShellExecute = false;
            newProgram.StartInfo.WorkingDirectory = workingDirectory;
            newProgram.StartInfo.RedirectStandardError = true;
            newProgram.Start();
            newProgram.WaitForExit();

            if (newProgram.StandardError.ReadToEnd().Length > 0)
            {
                outString = newProgram.StandardError.ReadToEnd();
            }

            return outString;
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

        private void Form_ZipPackaging_Load(object sender, EventArgs e)
        {
            label_Included_Files.Text = lang.GetString("Included_Files");
            label_Patch_Desc.Text = lang.GetString("Patch_Desc");
            button_CheckAll.Text = lang.GetString("Select_All");
            button_UncheckAll.Text = lang.GetString("Deselect_All");
            Button_Save.Text = lang.GetString("Save");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkedFileCount != fileListBox.CheckedItems.Count)
            {
                updatePatchInfo();
                checkedFileCount = fileListBox.CheckedItems.Count;
            }
        }
    }
}
