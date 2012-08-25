using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using LuaInterface;
using Ionic.Zip;

namespace MM
{
    enum FileUtilAppendMode
    {
        NoAppend, AppendBefore, AppendAfter
    }

    class MMScriptFunctionsArgs
    {
        public string Output;
        public int Value;

        public MMScriptFunctionsArgs(string output)
        {
            Output = output;
        }

        public MMScriptFunctionsArgs(int value)
        {
            Value = value;
        }
    }

    class MMScriptFunctions
    {
        protected static string outputBuffer = "";
        protected static LangManager langManager = LangManager.GetInstance();
        protected static PrefsManager prefsManager = PrefsManager.GetInstance();

        protected static void print(string output)
        {
            MMScriptBridge.GetInstance().Print(output);
        }

        protected virtual void setProgress(int value)
        {
            MMScriptBridge.GetInstance().SetProgress(value);
        }

        public static string GetFilePath(string mmFilePath, string adbFilePath)
        {
            return String.Format("{0}/{1}/{2}", Application.StartupPath, mmFilePath, adbFilePath).Replace("//", "/");
        }

        public static string RunProgram(string path, string args, string workingDirectory)
        {
            outputBuffer = "";

            if (!Directory.Exists(workingDirectory) && workingDirectory != "")
            {
                MyExtensions.CreateDirectory(new DirectoryInfo(workingDirectory));
            }

            Trace.WriteLine("[MM]Launching: " + path + " " + args);
            Trace.WriteLine("[MM]Working directory: " + workingDirectory);

            Process newProgram = new Process();

            newProgram.StartInfo.FileName = path;
            newProgram.StartInfo.Arguments = args;
            newProgram.StartInfo.CreateNoWindow = true;
            newProgram.StartInfo.UseShellExecute = false;
            newProgram.StartInfo.WorkingDirectory = workingDirectory;
            newProgram.StartInfo.RedirectStandardError = true;
            newProgram.StartInfo.RedirectStandardOutput = true;
            newProgram.OutputDataReceived += new DataReceivedEventHandler(newProgram_OutputDataReceived);
            newProgram.ErrorDataReceived += new DataReceivedEventHandler(newProgram_ErrorDataReceived);
            newProgram.Start();
            newProgram.BeginErrorReadLine();
            newProgram.BeginOutputReadLine();

            newProgram.WaitForExit();
            Trace.WriteLine("[MM]-----START OF OUTPUT-----");
            Trace.WriteLine(outputBuffer);
            Trace.WriteLine("[MM]-----END OF OUTPUT-----");

            return outputBuffer;
        }

        static void newProgram_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuffer += e.Data + "\n";
        }

        static void newProgram_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            outputBuffer += e.Data + "\n";
        }
    }

    class APKTool : MMScriptFunctions
    {
        string adbFilePath = "";

        public APKTool(string adbFilePath)
        {
            this.adbFilePath = adbFilePath;
        }

        protected new static void print(string output)
        {
            MMScriptBridge.GetInstance().Print("[APKTool] " + output);
        }

        public string GetSubFilePath(string subPath = "", string mmPath = "Temp")
        {
            return String.Format("{0}/{1}/{2}/{3}", Application.StartupPath, mmPath, adbFilePath, subPath).Replace("//", "/");
        }

        public void ExtractResourceOnly()
        {
            string workspaceFilePath = GetFilePath("Workspace", adbFilePath);
            if (!File.Exists(workspaceFilePath))
            {
                print(LangManager.GetInstance().GetFormattedString("File_Not_Found", workspaceFilePath));
                return;
            }

            print(LangManager.GetInstance().GetFormattedString("File_Extracting", adbFilePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/"));


            RunProgram(prefsManager.GetPrefString("JavaDir"), "-Xms32m -Xmx1024m -jar apktool.jar d -t MM --force --no-src" +
                " \"" + workspaceFilePath + "\"" + " " +
                "\"" + GetFilePath("Temp", adbFilePath) + "\"",
                Application.StartupPath + "/Bin");
            Thread.Sleep(1500);
        }

        public void Extract(bool extractResourceToo = false)
        {
            string workspaceFilePath = GetFilePath("Workspace", adbFilePath);
            if (!File.Exists(workspaceFilePath))
            {
                print(LangManager.GetInstance().GetFormattedString("File_Not_Found", workspaceFilePath));
                return;
            }

            print(LangManager.GetInstance().GetFormattedString("File_Extracting", adbFilePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/"));

            string extractRes = "";

            if (!extractResourceToo)
            {
                extractRes = "--no-res ";
            }

            RunProgram(prefsManager.GetPrefString("JavaDir"), "-Xms32m -Xmx1024m -jar apktool.jar d -t MM --force " + extractRes +
                " \"" + workspaceFilePath + "\"" + " " +
                "\"" + GetFilePath("Temp", adbFilePath) + "\"",
                Application.StartupPath + "/Bin");
            Thread.Sleep(1500);
        }

        public void ExtractWithResources()
        {
            Extract(true);
        }

        public static void InstallDummyFrameworks()
        {
            print(LangManager.GetInstance().GetFormattedString("InstallingDummyFramework"));

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/Unsigned"));

            for (int i = 1; i <= 6; i++)
            {
                RunProgram(prefsManager.GetPrefString("JavaDir"), "-Xms32m -Xmx1024m -jar apktool.jar if " +
                   "\"" + GetFilePath("Bin/dummy_frameworks", String.Format("{0}.apk", i)) + "\" MM",
                   Application.StartupPath + "/Bin");
            }

            Thread.Sleep(1500);
        }

        public static string InstallFramework(string filePath)
        {
            print(LangManager.GetInstance().GetFormattedString("InstallingFramework", filePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/Unsigned"));

            string result = RunProgram(prefsManager.GetPrefString("JavaDir"), "-Xms32m -Xmx1024m -jar apktool.jar if " +
                "\"" + GetFilePath("Workspace", filePath) + "\" MM",
                Application.StartupPath + "/Bin");
            Thread.Sleep(1500);

            return result;
        }

        public void Build()
        {
            print(LangManager.GetInstance().GetFormattedString("File_Packing", adbFilePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Application.StartupPath + "/Temp/Unsigned"));

            RunProgram(prefsManager.GetPrefString("JavaDir"), "-Xms32m -Xmx1024m -jar apktool.jar b " +
                "\"" + GetFilePath("Temp", adbFilePath) + "\"" + " " +
                "\"" + GetFilePath("Temp/Unsigned", adbFilePath) + "\"",
                Application.StartupPath + "/Bin");
            Thread.Sleep(1500);
        }

        public void CopySign()
        {
            if (!File.Exists(GetFilePath("Temp/Unsigned", adbFilePath)))
            {
                print(langManager.GetFormattedString("File_Not_Found", "Temp/Unsigned/" + adbFilePath));
                return;
            }

            print(langManager.GetFormattedString("Copying_Signature", adbFilePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(Application.StartupPath + "/Out/" + adbFilePath).ToString()));

            using (ZipFile originalZip = ZipFile.Read(GetFilePath("Workspace", adbFilePath)))
            {
                originalZip.ExtractSelectedEntries("name = AndroidManifest.xml", "", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                originalZip.ExtractSelectedEntries("name = CERT.RSA", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                originalZip.ExtractSelectedEntries("name = CERT.SF", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                originalZip.ExtractSelectedEntries("name = MANIFEST.MF", "META-INF/", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                originalZip.ExtractSelectedEntries("name = preloaded-classes", "", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
                originalZip.ExtractSelectedEntries("name = p", "", Application.StartupPath + "/Temp", ExtractExistingFileAction.OverwriteSilently);
            }

            using (ZipFile newZip = ZipFile.Read(GetFilePath("Temp/Unsigned", adbFilePath)))
            {

                string filePath = Application.StartupPath + "/Temp/AndroidManifest.xml";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "");
                }

                filePath = Application.StartupPath + "/Temp/META-INF/CERT.RSA";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "META-INF/");
                }

                filePath = Application.StartupPath + "/Temp/META-INF/CERT.SF";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "META-INF/");
                }

                filePath = Application.StartupPath + "/Temp/META-INF/MANIFEST.MF";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "META-INF/");
                }

                filePath = Application.StartupPath + "/Temp/preloaded-classes";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "");
                }

                filePath = Application.StartupPath + "/Temp/p";

                if (File.Exists(filePath))
                {
                    newZip.UpdateFile(filePath, "");
                }

                newZip.Save(GetFilePath("Out", adbFilePath));
            }

            Thread.Sleep(1500);
        }

        public void Sign()
        {
            if (!File.Exists(GetFilePath("Temp/Unsigned", adbFilePath)))
            {
                langManager.GetFormattedString("File_Not_Found", "Temp/Unsigned/" + adbFilePath);
            }

            print(langManager.GetFormattedString("Signing", adbFilePath));

            MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(GetFilePath("Out", adbFilePath)).ToString()));

            RunProgram(prefsManager.GetPrefString("JavaDir"), "-jar signapk.jar testkey.x509.pem testkey.pk8 " +
                "\"" + GetFilePath("Temp/Unsigned", adbFilePath) + "\" " +
                "\"" + GetFilePath("Out", adbFilePath) + "\"",
                Application.StartupPath + "/Bin");
            Thread.Sleep(1500);
        }

    }
    class FileUtil : MMScriptFunctions
    {
        protected string filePath;
        protected string data = "";

        public FileUtil()
        {

        }
        public FileUtil(string arg = "", bool argumentIsData = false)
        {
            if (!argumentIsData)
            {
                if (arg != "" && !File.Exists(arg))
                {
                    print(String.Format(LangManager.GetInstance().GetString("File_Not_Found"), arg));
                    return;
                }

                this.filePath = arg;

                using (StreamReader streamReader = new StreamReader(arg))
                {
                    data = streamReader.ReadToEnd();
                }
            }
            else
            {
                data = arg;
            }
        }
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        protected new static void print(string output)
        {
            MMScriptFunctions.print("[FileUtil] " + output.Replace(Application.StartupPath, ""));
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
        }
        public override string ToString()
        {
            return this.data;
        }

        public String Escape(string data)
        {
            return Regex.Escape(data);
        }

        public SearchResult SearchLine(string pattern, bool dupeCheck = false)
        {
            SearchResult result = null;
            using (StringReader stringReader = new StringReader(data))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    if (Regex.IsMatch(line, pattern))
                    {
                        result = new SearchResult(line, filePath, pattern, line);
                        break;
                    }
                }
            }

            if (result == null && !dupeCheck)
            {
                print(String.Format(LangManager.GetInstance().GetString("Cant_Find_String_With_Pattern"), pattern));
            }
            return result;
        }

        public bool ReplaceLine(string pattern, string replacement, string originalText = "", int appendMode = (int)FileUtilAppendMode.NoAppend)
        {
            bool result = false;

            using (StringReader stringReader = new StringReader(data))
            using (StringWriter stringWriter = new StringWriter())
            {
                string line;
                bool foundOnce = false;
                while ((line = stringReader.ReadLine()) != null)
                {

                    if (Regex.IsMatch(line, pattern))
                    {
                        foundOnce = true;

                        if (appendMode == (int)FileUtilAppendMode.AppendAfter)
                        {
                            stringWriter.WriteLine(originalText);
                        }

                        stringWriter.WriteLine(replacement);

                        if (appendMode == (int)FileUtilAppendMode.AppendBefore)
                        {

                            stringWriter.WriteLine(originalText);
                        }
                    }
                    else
                    {
                        stringWriter.WriteLine(line);
                    }
                }
                if (foundOnce)
                {
                    data = stringWriter.ToString();
                }
            }

            return result;
        }
        public bool ReplaceLine(SearchResult searchResult, int appendMode = (int)FileUtilAppendMode.NoAppend)
        {
            return ReplaceLine(searchResult.Pattern, searchResult.Data, searchResult.Start, appendMode);
        }
        public SearchResult SearchRange(string startLinePattern, string endLinePattern)
        {
            SearchResult result = null;

            using (StringReader stringReader = new StringReader(data))
            using (StringWriter stringWriter = new StringWriter())
            {
                string line;
                string startLine = "", endLine = "";
                bool found = false;
                bool foundOnce = false;

                while ((line = stringReader.ReadLine()) != null)
                {
                    if (!found && Regex.IsMatch(line, startLinePattern, RegexOptions.IgnorePatternWhitespace))
                    {
                        found = true;
                        foundOnce = true;
                        startLine = line;
                    }
                    else if (found && Regex.IsMatch(line, endLinePattern, RegexOptions.IgnorePatternWhitespace))
                    {
                        found = false;
                        endLine = line;
                    }
                    else if (found)
                    {
                        stringWriter.WriteLine(line);
                    }
                }

                if (foundOnce)
                {
                    result = new SearchResult(stringWriter.ToString(), filePath, startLinePattern, startLine, endLinePattern, endLine);
                }
            }

            return result;
        }
        public bool ReplaceRange(string startPattern, string endPattern, string startLine, string endLine, string replacement)
        {
            bool result = false;


            using (StringReader stringReader = new StringReader(data))
            using (StringWriter stringWriter = new StringWriter())
            {
                string line;
                bool found = false;
                bool foundOnce = false;
                while ((line = stringReader.ReadLine()) != null)
                {
                    if (!found && Regex.IsMatch(line, startPattern))
                    {
                        found = true;
                        foundOnce = true;
                        stringWriter.WriteLine(startLine);
                    }
                    else if (found && Regex.IsMatch(line, endPattern))
                    {
                        found = false;
                        stringWriter.WriteLine(replacement);
                        stringWriter.WriteLine(endLine);
                    }
                    else if (!found)
                    {
                        stringWriter.WriteLine(line);
                    }
                }

                if (foundOnce)
                {
                    result = true;
                    data = stringWriter.ToString();
                }
            }

            return result;
        }
        public bool ReplaceRange(SearchResult searchResult)
        {
            return ReplaceRange(searchResult.StartPattern, searchResult.EndPattern, searchResult.Start, searchResult.End, searchResult.Data);
        }

        public bool DupeCheck(string pattern)
        {
            bool result = false;

            SearchResult searchResult = SearchLine(pattern, true);
            if (searchResult != null)
            {
                print(String.Format(LangManager.GetInstance().GetString("File_Is_Already_Patched"), filePath));
                result = true;
            }

            return result;
        }

        public void Save(string savePath = "")
        {
            if (savePath == "")
            {
                savePath = filePath;
            }
            MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(savePath).ToString()));
            using (StreamWriter streamWriter = new StreamWriter(savePath, false))
            {
                streamWriter.Write(data);
            }
            Thread.Sleep(1500);
        }

        public static void Copy(string file, string destination)
        {
            if (!File.Exists(file))
            {
                print(langManager.GetFormattedString("File_Not_Found", file));
                return;
            }

            MyExtensions.CreateDirectory(new DirectoryInfo(Directory.GetParent(destination).ToString()));

            File.Copy(file, destination, true);
            Thread.Sleep(1500);
        }

        public static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(sourceFolder))
                return;

            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            string[] folders = Directory.GetDirectories(sourceFolder);

            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                try
                {
                    File.Copy(file, dest);
                }
                catch (IOException)
                {

                }
            }

            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }

        }

        public static void DeleteFolder(string folder)
        {
            try
            {
                System.IO.Directory.Delete(folder, true);
            }
            catch
            {
                //do nothing, it's fine.
            }
        }

        public bool Success
        {
            get
            {
                if (data != "")
                {
                    return true;
                }
                return false;
            }
        }
    }
    class SearchResult : FileUtil
    {
        string start, end, startPattern, endPattern;

        public SearchResult(string data, string filePath, string startPattern = "", string start = "", string endPattern = "", string end = "")
            : base(data, true)
        {
            this.start = start;
            this.end = end;
            this.startPattern = startPattern;
            this.endPattern = endPattern;
            this.filePath = filePath;
        }

        public string Pattern
        {
            get
            {
                return startPattern;
            }
            set
            {
                startPattern = value;
            }
        }

        public string StartPattern
        {
            get
            {
                return startPattern;
            }
            set
            {
                startPattern = value;
            }
        }

        public string EndPattern
        {
            get
            {
                return endPattern;
            }
            set
            {
                endPattern = value;
            }
        }

        public bool Found
        {
            get
            {
                bool result = false;

                if (data != "")
                {
                    result = true;
                }
                return result;
            }
        }

        public string Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public string End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public override string ToString()
        {
            return data;
        }
    }
    class MMGlobals : MMScriptFunctions
    {
        public string InputBox(string title, string promptText, string value = "")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "확인";
            buttonCancel.Text = "취소";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new System.Drawing.Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new System.Drawing.Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult;
            FormManager.GetInstance().GetMainForm().Invoke(
                new Action(() => { dialogResult = form.ShowDialog(FormManager.GetInstance().GetMainForm()); }));

            value = textBox.Text;
            return value;
        }

        public void ShowMessageBox(String message)
        {
            FormManager.GetInstance().GetMainForm().Invoke(
                new Action(() => { MessageBox.Show(message, "MM!", MessageBoxButtons.OK, MessageBoxIcon.Information); }));
        }

        public bool YesNoQuestion(string question)
        {
            bool result = false;
            DialogResult _result = MessageBox.Show(question, "MM!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (_result == System.Windows.Forms.DialogResult.Yes)
            {
                result = true;
            }

            return result;
        }

        public void CheckVersion(string version)
        {
            int checkVersion = Convert.ToInt16(version.Replace("v", "").Replace(".", ""));
            int thisVersion = Convert.ToInt16(prefsManager.GetPrefString("Version").Replace("v", "").Replace(".", ""));
            if (checkVersion > thisVersion)
            {
                print(LangManager.GetInstance().GetFormattedString("Version_Required", checkVersion));
            }
        }

        public string Culture
        {
            get
            {
                return CultureInfo.CurrentCulture.ToString();
            }
        }
        public string GetModFilePath(string filePath)
        {
            return String.Format("{0}/Mods/{1}/{2}", ApplicationPath, ModName, filePath).Replace("//", "/");
        }

        public string GetFilePath(string filePath)
        {
            return String.Format("{0}/{1}", ApplicationPath, filePath).Replace("//", "/");
        }

        public string ApplicationPath
        {
            get
            {
                return Application.StartupPath;
            }
        }

        public string ModName
        {
            get
            {
                return prefsManager.GetPrefString("SelectedMod");
            }
        }

        public void Print(string output)
        {
            print(output);
        }

        public void SetProgress(int value)
        {
            setProgress(value);
        }

        public void SetDebug()
        {
            prefsManager.SetPrefBool("DebugMode", true);
        }
    }

    class MMScriptBridge : Singleton<MMScriptBridge>
    {
        public delegate void OutputHandler(object sender, MMScriptFunctionsArgs e);

        public event OutputHandler OnOutput = null;

        public event OutputHandler OnSetProgress = null;

        public void Print(string output)
        {
            fireOnOutput(output);
        }

        public void SetProgress(int value)
        {
            fireOnSetProgress(value);
        }

        protected void fireOnSetProgress(int value)
        {
            if (OnSetProgress != null)
            {
                OnSetProgress(this, new MMScriptFunctionsArgs(value));
            }
        }

        protected void fireOnOutput(string output)
        {
            if (OnOutput != null)
            {
                OnOutput(this, new MMScriptFunctionsArgs(output));
            }
        }
    }

    class MMScriptParser
    {
        protected string scriptPath;
        MMScriptFunctions functions;
        Lua lua;


        public MMScriptParser(string scriptPath, MMScriptFunctions functions)
        {
            this.scriptPath = scriptPath;
            this.functions = functions;

            string modDir = String.Format("{0}\\Mods\\{1}", Application.StartupPath, PrefsManager.GetInstance().GetPrefString("SelectedMod"));

            lua = new Lua();
            Console.WriteLine();
            registerFunction("print");
            LuaTable table = lua.GetTable("package");
            table["path"] += String.Format(";{0}/?.lua", modDir);
            lua.Push(table);
            lua.DoString("luanet.load_assembly(\"MM\");");
            lua.DoString("APKTool = luanet.import_type(\"MM.APKTool\");");
            lua.DoString("FileUtil = luanet.import_type(\"MM.FileUtil\");");
            lua.DoString("MMGlobals = luanet.import_type(\"MM.MMGlobals\");");
            lua.DoString("FILEUTIL_NOAPPEND = 0;");
            lua.DoString("FILEUTIL_APPENDBEFORE = 1;");
            lua.DoString("FILEUTIL_APPENDAFTER = 2;");
            lua.DoString("MM = MMGlobals();");
        }

        public void print(string output)
        {
            Console.WriteLine(output);
        }

        public void Parse()
        {
            try
            {
                lua.DoFile(scriptPath);
            }
            catch (Exception e)
            {
                MMScriptBridge.GetInstance().Print("[Exception] " + e.Message);
                MMScriptBridge.GetInstance().Print("[InnerException] " + e.InnerException);
            }

        }

        protected void registerFunction(string functionName)
        {
            lua.RegisterFunction(functionName, this, this.GetType().GetMethod(functionName));
        }
    }
}


