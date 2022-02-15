using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace easySave___Graphic.ViewModel
{
    class iniFile
    {
        string path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int Size, string filePath);

        public iniFile(string iniPath)
        {
            path = new FileInfo(iniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string read(string key, string section = null)
        {
            var retVal = new StringBuilder(255);

            GetPrivateProfileString(section ?? EXE, key, "", retVal, 255, path);

            return retVal.ToString();
        }

        public void write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section ?? EXE, key, value, path);
        }
    }
}
