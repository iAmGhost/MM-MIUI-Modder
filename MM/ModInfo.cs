using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using aejw;


namespace MM
{
    public class ModInfo
    {
        public string Title;
        public string Author;
        public string Homepage;
        public string Version;
        public string Description;
        public string[] RequiredFiles;
        public string[] ModifiedFiles;
        public string DirName;

        public ModInfo(string filePath)
        {
            cIni ini = new cIni(filePath);
            ini.BufferLen = 1024 * 1024;
            Title = ini.ReadValue("ModInfo", "Title");
            Author = ini.ReadValue("ModInfo", "Author");
            Homepage = ini.ReadValue("ModInfo", "Homepage");
            Version = ini.ReadValue("ModInfo", "Version");
            RequiredFiles = ini.ReadValue("ModInfo", "RequiredFiles").Split(',');
            ModifiedFiles = ini.ReadValue("ModInfo", "ModifiedFiles").Split(',');
            Description = ini.ReadValue("ModInfo", "Description").Replace("\\r\\n","\r\n");
            DirName = Directory.GetParent(filePath).Name;
        }

        public override string ToString()
        {
            return DirName;
        }
    }
}
