using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JoyStickTest
{
    public static class SaveLog
    {
        private static object locker = new object();
        private static string CreateDir(string filename)
        {
            string filepath = null;
            //string currentDay = DateTime.Now.ToString("yyyyMMdd");
            string path = Directory.GetCurrentDirectory() + @"\log\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            filepath = path + filename + ".log";
            return filepath;
        }
        private static bool WriteString(string msg, string filename)
        {
            bool result = false;
            lock (locker)
            {
                string path = CreateDir(filename);
                FileStream fs = null;
                try
                {
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine(msg);
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();
                    result = true;
                }
                catch { if (fs != null) fs.Close(); }
            }
            return result;
        }
        private static string GetCurrentString(string msg)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            return time + "  " + msg;
        }
        public static bool DeleteFile(int reservedDays)
        {
            DateTime dtNow = DateTime.Now;
            string path = Directory.GetCurrentDirectory() + @"\log\";
            if (!Directory.Exists(path))
            {
                return true;
            }
            else
            {
                DirectoryInfo dicInfo = new DirectoryInfo(path);
                TimeSpan ts = new TimeSpan();
                try
                {
                    foreach (DirectoryInfo item in dicInfo.GetDirectories())
                    {
                        ts = (TimeSpan)(dtNow - item.LastWriteTime);
                        if (ts.Days > reservedDays)
                        {
                            item.Delete(true);
                        }
                    }
                    foreach (FileInfo item in dicInfo.GetFiles())
                    {
                        ts = (TimeSpan)(dtNow - item.LastWriteTime);
                        if (ts.Days > reservedDays)
                        {
                            item.Delete();
                        }
                    }
                }
                catch { return false; }

            }

            return true;
        }
        public static bool WriteToFile(string msg, string filename)
        {
            bool result = false;
            if (filename == null || filename == "")
                return result;
            string str_msg = GetCurrentString(msg);
            result = WriteString(str_msg, filename);
            return result;
        }
        public static bool Log(string msg)
        {
            bool result = false;
            string filename = DateTime.Now.ToString("yyyyMMdd");
            if (filename == null || filename == "")
                return result;
            string str_msg = GetCurrentString(msg);
            result = WriteString(str_msg, filename);
            return result;
        }
    }

}
