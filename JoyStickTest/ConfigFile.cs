using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoyStickTest
{
    public class ConfigFile
    {
        private static string m_ConfigPath = Application.StartupPath + "\\Config\\Config.ini";
        private static INIFile m_IniFile = new INIFile();


        public static string CopyFileName1
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;
                return m_IniFile.IniReadValueByUTF8("Copy", "FileName1");
            }
        }
        public static string CopyFileName2
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;
                return m_IniFile.IniReadValueByUTF8("Copy", "FileName2");
            }
        }
        public static bool RenameUSB
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;
                string rename = m_IniFile.IniReadValueByUTF8("UName", "Rename").Trim();
                return rename != "0";
            }
        }
        public static string U1Name
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;
                string name = m_IniFile.IniReadValueByUTF8("UName", "U1");
                name = string.IsNullOrEmpty(name) ? "品管1号" : name;
                return name;
            }
        }
        public static string U2Name
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;

                string name = m_IniFile.IniReadValueByUTF8("UName", "U2");
                name = string.IsNullOrEmpty(name) ? "品管2号" : name;
                return name;
            }
        }

        public static int MinSpeed
        {
            get
            {
                m_IniFile.Path = m_ConfigPath;

                int minSpeed;
                if (!int.TryParse(m_IniFile.IniReadValueByUTF8("Speed", "Min"), out minSpeed))
                {
                    minSpeed = 8;
                }
                return minSpeed;
            }
        }
    }

}
