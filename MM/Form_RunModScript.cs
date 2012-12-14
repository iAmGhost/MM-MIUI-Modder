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
using System.Threading;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace MM
{
    public partial class Form_RunModScript : NewForm
    {
        Dictionary<string, string> varStorage = new Dictionary<string, string>();

        public Form_RunModScript()
        {
            InitializeComponent();
        }

        public override void OnPageShow()
        {
            varStorage["ProgramPath"] = Application.StartupPath;
            varStorage["ModName"] = PrefsManager.GetInstance().GetPrefString("SelectedMod");

            SetButtonStatus(NavigationButton.Prev, false);
            SetButtonStatus(NavigationButton.Next, false);
            debugClear();

            timer1.Enabled = false;

            debugOut(lang.GetString("Script_Begin"));

            timer1.Enabled = true;

            PrefsManager prefs = PrefsManager.GetInstance();
        }

        private string VariableReplacer(Match match)
        {
            string returnValue;
            if (varStorage.TryGetValue(match.ToString().Substring(1, match.Length - 2), out returnValue))
            {
                return returnValue;
            }

            return "";
        }

        private string parseLine(string line)
        {

            line = line.Replace("\\<", "{LT}");
            line = line.Replace("\\>", "{GT}");
            line = Regex.Replace(line, "(\\<)(.*?)(\\>)", new MatchEvaluator(VariableReplacer));
            line = line.Replace("$ProgramPath", Application.StartupPath);
            line = line.Replace("{COMMA}", ",");
            line = line.Replace("{LT}", "<");
            line = line.Replace("{GT}", ">");
            line = line.Replace("\\r\\n", "\r\n");

            return line;
        }

        private void parseScript()
        {
            string resultString = "";
            string javaDir = "";

            string system32Directory = Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "system32");
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
            {
                // For 32-bit processes on 64-bit systems, %windir%\system32 folder
                // can only be accessed by specifying %windir%\sysnative folder.
                system32Directory = Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "sysnative");
            }

            if (File.Exists(system32Directory + "/java.exe"))
            {
                javaDir = system32Directory + "/java.exe";
            }
            else if (File.Exists(Environment.SystemDirectory + "/java.exe"))
            {
                javaDir = Environment.SystemDirectory + "/java.exe";
            }
            else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/system32/java.exe"))
            {
                javaDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/system32/java.exe";
            }
            else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/syswow64/java.exe"))
            {
                javaDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/syswow64/java.exe";
            }
            else
            {
                javaDir = "java.exe";
                resultString = lang.GetString("Missing_Java_Runtime");
            }

            try
            {
                System.IO.Directory.Delete(Application.StartupPath + "/Temp", true);
            }
            catch
            {
                //do nothing, it's fine.
            }
            try
            {
                System.IO.Directory.Delete(Application.StartupPath + "/Out", true);
            }
            catch
            {
                //do nothing, it's fine.
            }

            setProgressBar(0);
            ModInfo modInfo = new ModInfo(Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModInfo.ini");
            string modScriptPath = Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModScript.mmm";
            string modLuaPath = Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod") + "/ModScript.lua";
            string modPath = Application.StartupPath + "/Mods/" + PrefsManager.GetInstance().GetPrefString("SelectedMod");

            StreamReader streamReader = new StreamReader(modScriptPath, Encoding.GetEncoding(949));
            string line, command;
            string[] split, arg = null;

            string filePathDirOnly = "";
            string[] _split;

            ZipFile zipFile = null;

            foreach (string file in modInfo.RequiredFiles)
            {
                if (file == "/system/framework/framework-res.apk" || file == "system/framework/framework-res.apk")
                {
                    string frameworkPath = Application.StartupPath + "/Workspace/system/framework/framework-res.apk";
                    if (File.Exists(frameworkPath))
                    {
                        RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar if \"" + frameworkPath + "\"", Application.StartupPath + "/Bin");
                    }
                }
            }

            bool useNewParser = false;
            #region OldParser
            while ((line = streamReader.ReadLine()) != null)
            {
                if (line == "#MM_USE_LUA_SCRIPT")
                {
                    useNewParser = true;
                    break;
                }
                if (resultString != "")
                {
                    break;
                }

                filePathDirOnly = "";

                split = line.Split('|');
                if (split.Length > 0)
                {
                    command = split[0];
                    if (split.Length > 1)
                    {
                        split[1] = split[1].Replace("\\,", "{COMMA}");
                        arg = split[1].Split(',');
                        for (int i = 0; i < arg.Length; i++)
                        {
                            arg[i] = parseLine(arg[i]);
                        }
                    }

                }
                else
                {
                    command = line;
                }

                switch (command)
                {
                    case "IF":
                        if (arg[0] == arg[1]) varStorage[arg[2]] = arg[3];
                        break;

                    case "ASK":
                        string value = "";
                        if (InputBox("MM!", arg[0], ref value) == DialogResult.OK)
                        {
                            varStorage[arg[1]] = value;
                        }
                        break;

                    case "ASK_YESNO":
                        DialogResult _result = MessageBox.Show(arg[0], "MM!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (_result == System.Windows.Forms.DialogResult.Yes)
                        {
                            varStorage[arg[1]] = "Yes";
                        }
                        else
                        {
                            varStorage[arg[1]] = "No";
                        }
                        break;

                    case "ZIP_OPEN":
                        if (!File.Exists(Application.StartupPath + "/" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_ZIP_OPEN"), Application.StartupPath + "/" + arg[0]));
                            continue;
                        }
                        zipFile = ZipFile.Read(Application.StartupPath + "/" + arg[0]);
                        Thread.Sleep(1000);
                        break;

                    case "ZIP_EXTRACT":
                        if (zipFile == null)
                        {
                            debugOut(lang.GetString("Debug_ZIP_EXTRACT"));
                            continue;
                        }

                        zipFile.ExtractAll(Application.StartupPath + "/" + arg[0], ExtractExistingFileAction.OverwriteSilently);
                        Thread.Sleep(1000);

                        break;

                    case "ZIP_ADDFILE":
                        if (!File.Exists(Application.StartupPath + "/" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("ZIP_ADDFILE"), Application.StartupPath + "/" + arg[0]));
                            continue;
                        }
                        zipFile.AddFile(arg[0], arg[1]);
                        break;

                    case "ZIP_ADDDIRECTORY":
                        if (!Directory.Exists(Application.StartupPath + "/" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("ZIP_ADDDIRECTORY"), Application.StartupPath + "/" + arg[0]));
                            continue;
                        }
                        zipFile.AddDirectory(arg[0], arg[1]);
                        break;

                    case "ZIP_UPDATE":
                        if (!File.Exists(Application.StartupPath + "/" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_ZIP_UPDATE"), Application.StartupPath + "/" + arg[0]));
                            continue;
                        }
                        zipFile.UpdateFile(arg[0], arg[1]);
                        break;

                    case "ZIP_SAVE":
                        zipFile.Save(arg[0]);
                        Thread.Sleep(1000);
                        break;

                    case "COPY":
                        if (!File.Exists(Application.StartupPath + "/" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_COPY"), Application.StartupPath + "/" + arg[0]));
                            continue;
                        }
                        MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(Application.StartupPath + "/" + arg[1]).ToString()));
                        File.Copy(Application.StartupPath + "/" + arg[0], Application.StartupPath + "/" + arg[1], true);
                        Thread.Sleep(1000);
                        break;

                    case "DEBUG_DONTDELETETEMP":
                        break;

                    case "SET":
                        varStorage[arg[0]] = arg[1];
                        break;

                    case "OPEN":
                        if (!File.Exists(Application.StartupPath + "/" + arg[1]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_OPEN"), Application.StartupPath + "/" + arg[1]));
                            continue;
                        }

                        using (StreamReader strReader = new StreamReader(Application.StartupPath + "/" + arg[1]))
                        {
                            varStorage[arg[0]] = strReader.ReadToEnd();
                        }

                        break;

                    case "PARSE":
                        varStorage[arg[0]] = parseLine(varStorage[arg[0]]);

                        break;

                    case "SAVE":
                        //MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(Application.StartupPath + "/" + arg[1]).ToString()));
                        using (StreamWriter strWriter = new StreamWriter(Application.StartupPath + "/" + arg[1]))
                        {
                            strWriter.Write(varStorage[arg[0]]);
                        }
                        Thread.Sleep(1000);
                        break;

                    case "STRING_TRIM":
                        varStorage[arg[0]] = varStorage[arg[0]].Trim();
                        break;

                    case "STRING_REPLACE":
                        varStorage[arg[0]] = varStorage[arg[0]].Replace(arg[1], arg[2]);
                        break;

                    case "SEARCHANDREPLACE":

                        using (StringReader stringReader = new StringReader(varStorage[arg[0]]))
                        {
                            bool searchIgnore = false;
                            StringWriter stringWriter = new StringWriter();
                            string _line;
                            while ((_line = stringReader.ReadLine()) != null)
                            {
                                if (!searchIgnore && Regex.IsMatch(_line, Regex.Escape(arg[1])))
                                {
                                    searchIgnore = true;
                                    stringWriter.WriteLine(arg[2]);

                                }
                                if (!searchIgnore) stringWriter.WriteLine(_line);
                                if (searchIgnore && Regex.IsMatch(_line, Regex.Escape(arg[3])))
                                {
                                    searchIgnore = false;
                                }
                            }


                            varStorage[arg[0]] = stringWriter.ToString();
                        }
                        break;

                    case "SEARCH":

                        using (StringReader stringReader = new StringReader(varStorage[arg[0]]))
                        {
                            bool searchFound = false;
                            StringWriter stringWriter = new StringWriter();
                            string _line;
                            while ((_line = stringReader.ReadLine()) != null)
                            {
                                if (!searchFound && Regex.IsMatch(_line, Regex.Escape(arg[1])))
                                {
                                    searchFound = true;
                                    varStorage[arg[3] + "_SearchHeader"] = _line;
                                }
                                if (searchFound) stringWriter.WriteLine(_line);

                                if (searchFound && Regex.IsMatch(_line, Regex.Escape(arg[2])))
                                {
                                    searchFound = false;
                                    varStorage[arg[3] + "_SearchFooter"] = _line;
                                }
                            }


                            varStorage[arg[3]] = stringWriter.ToString();
                        }
                        break;

                    case "PRINT":
                        debugOut(arg[0]);
                        break;

                    case "EXTRACT_WITHRES":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar d -f " +
                            "\"../Workspace" + arg[0] + "\"" + " " +
                            "\"../Temp" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "EXTRACT_F":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar d -f -r " +
                            "\"../Workspace" + arg[0] + "\"" + " " +
                            "\"../Temp" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "EXTRACT":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar d -f -r " +
                            "\"../Workspace" + arg[0] + "\"" + " " +
                            "\"../Temp" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "EXTRACT_BIG":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar d -f -r " +
                            "\"../Workspace" + arg[0] + "\"" + " " +
                            "\"../Temp" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Console.WriteLine("-Xms32m -Xmx1024m -jar apktool.jar d -f -r " +
                            "\"../Workspace" + arg[0] + "\"" + " " +
                            "\"../Temp" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "BUILD":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/Unsigned"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar b " +
                            "\"../Temp" + arg[0] + "\"" + " " +
                            "\"../Temp/Unsigned" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "BUILD_BIG":
                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/Unsigned"));
                        resultString = RunProgram(javaDir, "-Xms32m -Xmx1024m -jar apktool.jar b " +
                            "\"../Temp" + arg[0] + "\"" + " " +
                            "\"../Temp/Unsigned" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "SIGN":
                        if (!File.Exists(Application.StartupPath + "/Temp/Unsigned" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_SIGN"), Application.StartupPath + "/Temp/Unsigned" + arg[0]));
                            continue;
                        }

                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Out"));
                        filePathDirOnly = "";
                        _split = arg[0].Split('/');
                        for (int j = 0; j < _split.Length - 1; j++)
                        {
                            filePathDirOnly += _split[j] + '/';
                        }

                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Out/" + filePathDirOnly));
                        resultString = RunProgram(javaDir, "-jar signapk.jar testkey.x509.pem testkey.pk8 " +
                            "\"../Temp/Unsigned" + arg[0] + "\" " +
                            "\"../Out" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");
                        Thread.Sleep(1000);
                        break;

                    case "COPYSIGN":
                        if (!File.Exists(Application.StartupPath + "/Temp/Unsigned" + arg[0]))
                        {
                            debugOut(String.Format(lang.GetString("Debug_COPYSIGN"), Application.StartupPath + "/Temp/Unsigned" + arg[0]));
                            continue;
                        }

                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp"));
                        filePathDirOnly = "";
                        _split = arg[0].Split('/');
                        for (int j = 0; j < _split.Length - 1; j++)
                        {
                            filePathDirOnly += _split[j] + '/';
                        }

                        MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Out/" + filePathDirOnly));

                        using (ZipFile originalZip = ZipFile.Read(Application.StartupPath + "/Workspace" + arg[0]))
                        {
                            originalZip.ExtractSelectedEntries("name = AndroidManifest.xml", "", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                            originalZip.ExtractSelectedEntries("name = CERT.RSA", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                            originalZip.ExtractSelectedEntries("name = CERT.SF", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                            originalZip.ExtractSelectedEntries("name = MANIFEST.MF", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                        }

                        using (ZipFile newZip = ZipFile.Read(Application.StartupPath + "/Temp/Unsigned" + arg[0]))
                        {
                            newZip.UpdateFile(Application.StartupPath + "/Temp/AndroidManifest.xml", "");
                            newZip.UpdateFile(Application.StartupPath + "/Temp/META-INF/CERT.RSA", "META-INF/");
                            newZip.UpdateFile(Application.StartupPath + "/Temp/META-INF/CERT.SF", "META-INF/");
                            newZip.UpdateFile(Application.StartupPath + "/Temp/META-INF/MANIFEST.MF", "META-INF/");
                            newZip.Save(Application.StartupPath + "/Out" + arg[0]);
                        }
                        /*resultString = RunProgram("java", "-jar signapk.jar testkey.x509.pem testkey.pk8 " +
                            "\"../Temp/Unsigned" + arg[0] + "\" " +
                            "\"../Out" + arg[0] + "\"",
                            Application.StartupPath + "/Bin");*/
                        Thread.Sleep(1000);
                        break;

                    case "RUN":
                        if (!File.Exists(modPath + "/" + arg[0]) && !File.Exists(modPath + "/" + arg[0] + ".exe"))
                        {
                            debugOut(String.Format(lang.GetString("Debug_RUN"), modPath + "/" + arg[0]));
                            continue;
                        }
                        resultString = RunProgram(modPath + "/" + arg[0], arg[1], modPath);
                        Thread.Sleep(1000);
                        break;

                    case "COPYRES":
                        if (File.Exists(Application.StartupPath + "/Workspace/system/framework/framework-res.apk"))
                        {
                            try
                            {
                                File.Copy(Application.StartupPath + "/Workspace/system/framework/framework-res.apk",
                                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/apktool/framework/1.apk", true);
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            resultString = String.Format(lang.GetString("File_Not_Found"), "/system/framework/framework-res.apk");
                        }
                        break;

                    case "DELRES":
                        if (File.Exists(Application.StartupPath + "/Workspace/system/framework/framework-res.apk"))
                        {
                            File.Delete(Application.StartupPath + "/Workspace/system/framework/framework-res.apk");
                        }
                        break;

                    case "SETPROGRESS":
                        setProgressBar(Convert.ToInt16(arg[0]));
                        break;

                    case "CHECKVERSION":
                        int checkVersion = Convert.ToInt16(arg[0].Replace("v", "").Replace(".", ""));
                        int thisVersion = Convert.ToInt16(PrefsManager.GetInstance().GetPrefString("Version").Replace("v", "").Replace(".", ""));
                        if (checkVersion > thisVersion)
                        {
                            resultString = String.Format(lang.GetString("Version_Required"), arg[0]);
                        }
                        break;
                }

            }
            #endregion

            if (useNewParser == true)
            {
                MMScriptFunctions functions = new MMScriptFunctions();

                PrefsManager prefs = PrefsManager.GetInstance();

                prefs.SetPrefString("JavaDir", javaDir);
                prefs.SetPrefBool("DebugMode", false);

                MMScriptParser parser = new MMScriptParser(modLuaPath, functions);

                parser.Parse();
            }

            if (resultString != "")
            {
                StringReader stringReader = new StringReader(resultString);
                debugOut(lang.GetString("Error_Caused"));
                while ((line = stringReader.ReadLine()) != null)
                {

                    debugOut(line);
                }
            }
            else
            {
                debugOut(lang.GetString("Script_End"));

                if (!PrefsManager.GetInstance().GetPrefBool("DebugMode"))
                {
                    try
                    {
                        System.IO.Directory.Delete(Application.StartupPath + "/Temp", true);
                    }
                    catch
                    {
                        //do nothing, it's fine.
                    }
                }
            }

            int counter = 0;
            foreach (string modFile in modInfo.ModifiedFiles)
            {
                if (!File.Exists(Application.StartupPath + "/Out" + modFile))
                {
                    debugOut(String.Format(lang.GetString("File_Output_Not_Found"), modFile));
                    counter++;
                }
            }

            if (counter == 0)
            {
                if (!PrefsManager.GetInstance().GetPrefBool("DebugMode"))
                {
                    try
                    {
                        System.IO.Directory.Delete(Application.StartupPath + "/Temp", true);
                    }
                    catch
                    {
                        //do nothing, it's fine.
                    }
                }
                SetButtonStatusFromThread(NavigationButton.Next, true);
            }
            else
            {
                debugOut(lang.GetString("Script_Has_Problem"));
                SetButtonStatusFromThread(NavigationButton.Prev, true);
                SetButtonStatusFromThread(NavigationButton.Next, false);
            }

            streamReader.Close();
        }

        void parser_OnSetProgress(object sender, MMScriptFunctionsArgs e)
        {
            setProgressBar(e.Value);
        }

        void parser_OnOutput(object sender, MMScriptFunctionsArgs e)
        {
            debugOut(e.Output);
        }

        private void debugOut(string content)
        {
            var action = new Action<string>(x => { debugList.AppendText(x + "\r\n"); });

            debugList.Invoke(action, content);
        }

        private void setProgressBar(int value)
        {
            var action = new Action<int>(x => { progressBar1.Value = x; });

            progressBar1.Invoke(action, value);

        }

        private void debugClear()
        {
            debugList.ResetText();
        }

        private void SetButtonStatusFromThread(NavigationButton button, bool enabled)
        {
            var action = new Action<NavigationButton, bool>((x, y) =>
            {
                SetButtonStatus(x, y);
            });
            Invoke(action, button, enabled);
        }

        private string RunProgram(string path, string args, string workingDirectory)
        {
            string outString = "";

            path = path.Replace("/", "\\");
            args = args.Replace("/", "\\");

            Process newProgram = new Process();
            newProgram.StartInfo.FileName = path;
            newProgram.StartInfo.Arguments = args;
            Console.WriteLine(path +" " +  args);
            newProgram.StartInfo.CreateNoWindow = true;
            newProgram.StartInfo.UseShellExecute = false;
            newProgram.StartInfo.WorkingDirectory = workingDirectory;
            //newProgram.StartInfo.RedirectStandardError = true;
            newProgram.Start();

            newProgram.WaitForExit();

            //if (newProgram.StandardError.ReadToEnd().Length > 0)
            //{
            //outString = newProgram.StandardError.ReadToEnd();
            //}

            return outString;
        }

        private void Form_RunModScript_Load(object sender, EventArgs e)
        {
            MMScriptBridge bridge = MMScriptBridge.GetInstance();
            bridge.OnOutput += new MMScriptBridge.OutputHandler(parser_OnOutput);
            bridge.OnSetProgress += new MMScriptBridge.OutputHandler(parser_OnSetProgress);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ThreadStart worker = new ThreadStart(parseScript);
            Thread t = new Thread(worker);
            t.IsBackground = true;
            t.Start();
            //parseScript();
            timer1.Enabled = false;
        }
    }
}
