using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace JoyStickTest
{
    public class SaveForm
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr handle, ref ManagedWindowPlacement placement);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr handle, ref ManagedWindowPlacement placement);

        [Serializable]   //必须加上，否则后面会提示为非可序列化标记  
        public struct ManagedWindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        public static string RegPath = @"Software\WirelessFixture\";
        public static void SaveSize(System.Windows.Forms.Form frm)
        {
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey(RegPath + frm.Name);
            // Get the window placement.  
            ManagedWindowPlacement placement = new ManagedWindowPlacement();
            GetWindowPlacement(frm.Handle, ref placement);
            // Serialize it.  
            MemoryStream ms = new MemoryStream();
            BinaryFormatter f = new BinaryFormatter();
            f.Serialize(ms, placement);
            // Store it as a byte array.  
            key.SetValue("Placement", ms.ToArray());
        }

        public static void SetSize(System.Windows.Forms.Form frm)
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(RegPath + frm.Name);
            if (key != null)
            {
                MemoryStream ms = new MemoryStream((byte[])key.GetValue("Placement"));
                BinaryFormatter f = new BinaryFormatter();
                ManagedWindowPlacement placement = (ManagedWindowPlacement)
                  f.Deserialize(ms);
                SetWindowPlacement(frm.Handle, ref placement);
            }
        }
    }
}
