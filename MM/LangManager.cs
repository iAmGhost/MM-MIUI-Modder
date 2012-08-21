using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Diagnostics;
using System.Globalization;

namespace MM
{
    public class LangManager : Singleton<LangManager>
    {
        private CultureInfo cultureInfo;
        private ResourceManager rm;
        
        public LangManager()
        {
            cultureInfo = CultureInfo.CurrentCulture;
            rm = new ResourceManager("MM.Strings", typeof(Form_Main).Assembly);
        }
        public string GetString(string strName)
        {
            return rm.GetString(strName, cultureInfo);
        }

        public string GetFormattedString(string strName, params object[] args)
        {
            return string.Format(GetString(strName), args);
        }
    }
}
