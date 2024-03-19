using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCrypto.Helpers
{
    public class IniParser
    {
        private string path;
        private string DllName = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string defaultValie, StringBuilder RetVal, int Size, string FilePath);

        public IniParser(string iniPath = null)
        {
            path = new FileInfo(iniPath ?? DllName + ".ini").FullName;
        }
        public string Read(string key, object defultValue = null)
        {
            return Read(DllName, key, defultValue);
        }
        public string Read(string section, string key, object defultValue = null)
        {
            if (defultValue is not null && !KeyExists(key, section))
            {
                Write(section, key, defultValue);
            }

            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(section ?? DllName, key, "", RetVal, 255, path);
            return RetVal.ToString();
        }

        public void Write(string key, string value)
        {
            Write(DllName, key, value);
        }

        public void Write(string key, object value)
        {
            Write(DllName, key, value.ToString());
        }

        public void Write(string section ,string key, object value )
        {
            Write(section, key, value.ToString());
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public void DeleteKey(string key)
        {
            Write(DllName, key, null);
        }
        public void DeleteKey(string section, string key)
        {
            Write(section, key, null);
        }

        public void DeleteSection(string section = null)
        {
            Write(null, null, section ?? DllName);
        }

        public bool KeyExists(string key)
        {
            return KeyExists(key, DllName);
        }

        public bool KeyExists(string section, string key)
        {
            return string.IsNullOrEmpty(Read(section, key));
        }
    }
}
