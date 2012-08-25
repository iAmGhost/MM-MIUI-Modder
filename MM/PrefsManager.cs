using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM
{
    class PrefsManager : Singleton<PrefsManager>
    {
        private Dictionary<string, string> prefsString = new Dictionary<string,string>();
        private Dictionary<string, int> prefsInt = new Dictionary<string, int>();
        private Dictionary<string, bool> prefsBool = new Dictionary<string, bool>();
        private Dictionary<string, string[]> prefsStringArray = new Dictionary<string, string[]>();

        public int GladNextPage = -1;
        public int GladPrevPage = -1;

        public PrefsManager()
        {
            SetPrefString("Version", "v3.4");
        }

        public void SetPrefString(string key, string value)
        {
            prefsString[key] = value;
        }

        public void SetPrefBool(string key, bool value)
        {
            prefsBool[key] = value;
        }

        public bool GetPrefBool(string key)
        {
            bool value;
            if (!prefsBool.TryGetValue(key, out value)) value = false;

            return value;
        }

        public void SetPrefInt(string key, int value)
        {
            prefsInt[key] = value;
        }

        public void SetPrefStringArray(string key, string[] value)
        {
            prefsStringArray[key] = value;
        }

        public string GetPrefString(string key)
        {
            string value;
            if (!prefsString.TryGetValue(key, out value)) value = "";

            return value;
        }

        public string[] GetPrefStringArray(string key)
        {
            string[] value;
            if (!prefsStringArray.TryGetValue(key, out value)) value = null;

            return value;
        }

        public int GetPrefInt(string key)
        {
            int value;
            if (!prefsInt.TryGetValue(key, out value)) value = -1;

            return value;
        }
    }
}
