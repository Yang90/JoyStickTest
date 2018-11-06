using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace JoyStickTest
{
    /// <summary>
    /// INI文件读写类。
    /// </summary>
    public class INIFile
    {
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(byte[] section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, byte[] val, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(byte[] section, string key, byte[] val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(byte[] section, string key, string defVal, Byte[] retVal, int size, string filePath);

        public static void WriteInisection(string section, string path)
        {
            StreamWriter sw2 = null;
            if (!File.Exists(path))
            {
                sw2 = new StreamWriter(path, true, Encoding.UTF8);
            }
            else
            {
                sw2 = new StreamWriter(path, true, Encoding.UTF8);
                sw2.WriteLine("[" + section + "]");
            }
            sw2.Flush();
            sw2.Close();
        }
        public static void WriteInivalue(string key, string value, string path)
        {
            StreamWriter sw2 = null;
            if (!File.Exists(path))
            {
                sw2 = new StreamWriter(path, true, Encoding.UTF8);
            }
            else
            {
                sw2 = new StreamWriter(path, true, Encoding.UTF8);
                sw2.WriteLine("" + key + "=" + value + "");
            }
            sw2.Flush();
            sw2.Close();
        }


        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding gb2312 = Encoding.GetEncoding("GB2312");
                byte[] unicodeBytes = gb2312.GetBytes(str + '\0');
                byte[] asciiBytes = Encoding.Convert(gb2312, utf8, unicodeBytes);
                char[] asciiChars = new char[utf8.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                utf8.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                string result = new string(asciiChars);
                return result;
            }
            catch
            {
                return "";
            }
        }

        private bool CheckStringChineseReg(string text)
        {
            bool res = false;
            if (Regex.IsMatch(text, @"[\u4e00-\u9fbb]"))
                res = true;
            return res;
        }


        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniWriteValueByUTF8(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Encoding.GetEncoding(_charSet).GetBytes(Section), Key, Encoding.GetEncoding(_charSet).GetBytes(Value), Path);
        }
        private string _charSet = "UTF-8";
        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string IniReadValueByUTF8(string Section, string Key)
        {
            byte[] byteArray = System.Text.Encoding.GetEncoding(_charSet).GetBytes(Section);
            byte[] byt = new byte[1024];
            int i = GetPrivateProfileString(byteArray, Key, null, byt, 1024, Path);
            return Encoding.GetEncoding(_charSet).GetString(byt, 0, i);
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            byte[] byteArray = System.Text.Encoding.GetEncoding(_charSet).GetBytes(Section);
            WritePrivateProfileString(byteArray, Key, Value, Path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, Path);
            return temp.ToString();
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValues(string Section, string Key, string Value)
        {

            WritePrivateProfileString(Section, Key, GB2312ToUTF8(Value), Path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string IniReadValues(string section, string key)
        {
            byte[] temp = new byte[65535];
            int i = GetPrivateProfileString(section, key, "", temp, 65535, Path);
            Encoding enc = Encoding.GetEncoding("UTF-8");
            string s = enc.GetString(temp).TrimEnd('\0');
            return s;
        }
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }
        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        public void ClearSection(string Section)
        {
            IniWriteValue(Section, null, null);
        }

    }

}
