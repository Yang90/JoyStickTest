using Microsoft.DirectX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace JoyStickTest
{
    public class JoyStick
    {
        [DllImport("winmm.dll")]
        private static extern UInt32 joyGetPos(UInt32 uJoyID, ref JoyInfo info);
        [DllImport("winmm.dll")]
        private static extern UInt32 joyReleaseCapture(UInt32 uJoyID);
        [DllImport("winmm.dll")]
        private static extern UInt32 joyGetThreshold(UInt32 uJoyID, ref UInt32 threshold);
        [DllImport("winmm.dll")]
        private static extern UInt32 joyGetNumDevs();
        [DllImport("winmm.dll")]
        private static extern UInt32 joySetCapture(IntPtr pWnd, UInt32 id, UInt32 peroid, bool changed);
        [StructLayout(LayoutKind.Sequential)]

        public struct JoyInfo
        {
            public uint wXpos;
            public uint wYpos;
            public uint wZpos;
            public uint wButtons;
        }
        public enum JoyInfoStatus
        {
            OK = 0,
            BaseErr = 160,
            ParamsErr = 165,        //参数错误
            NoCanDo = 166,          //无法正常工作
            NoConnect = 167,         //操纵杆未连接
            UndefinedErr
        }

        private static List<Device> m_Devices = new List<Device>();

        public static JoyInfoStatus GetJoyPos(UInt32 id, out JoyInfo info)
        {
            info = new JoyInfo();
            UInt32 result = joyGetPos(id, ref info);

            JoyInfoStatus status;
            if (Enum.IsDefined(typeof(JoyInfoStatus), (int)result))
            {
                status = (JoyInfoStatus)result;
            }
            else
            {
                status = JoyInfoStatus.UndefinedErr;
            }
            return (JoyInfoStatus)result;
        }
        public static UInt32 GetJoyNum()
        {
            UInt32 num = joyGetNumDevs();
            return num;
        }
        public static UInt32 GetJoyThreshold(UInt32 id)
        {
            UInt32 threshold = 1000;
            UInt32 result = joyGetThreshold(id, ref threshold);
            return threshold;
        }
        public static UInt32 SetJoyCapture(IntPtr pWnd, UInt32 id, UInt32 peroid, bool changed)
        {
            return joySetCapture(pWnd, id, peroid, changed);
        }
        public static UInt32 JoyReleaseCapture(UInt32 id)
        {
            return joyReleaseCapture(id);
        }
        public static List<Device> GetDevices()
        {
            List<Device> joySticks = new List<Device>();
            try
            {
                DeviceList devices = Manager.GetDevices(DeviceType.Joystick, EnumDevicesFlags.AttachedOnly);
                //if (devices.Count != m_Devices.Count)
                //{
                    foreach (DeviceInstance item in devices)
                    {
                        Device joy = new Device(item.InstanceGuid);
                        joy.SetDataFormat(DeviceDataFormat.Joystick);
                        joy.Acquire();

                        joySticks.Add(joy);
                    }
                    for (int i = 0; i < m_Devices.Count; i++)
                    {
                        m_Devices[i].Unacquire();
                        m_Devices[i].Dispose();
                    }
                    m_Devices.Clear();
                    m_Devices = joySticks;
                    for (int i = 0; i < joySticks.Count; i++)
                    {
                        Console.WriteLine("{2}---------id:{0},guid:{1}", joySticks[i].Properties.JoystickId, joySticks[i].DeviceInformation.InstanceGuid.ToString(), i);
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDevices Error:{0}\r\n{1}", ex.Message, ex.StackTrace);
            }

            return m_Devices;
        }

        public static bool GetJoyInfo(Device joyStick, out JoyInfo info)
        {
            info = new JoyInfo();
            try
            {
                joyStick.Poll();

                JoystickState state = joyStick.CurrentJoystickState;
                info.wXpos = (uint)state.X;
                info.wYpos = (uint)state.Y;
                int[] slide = state.GetSlider();
                info.wZpos = (uint)slide[0];

                byte[] btns = state.GetButtons();
                for (uint i = 0; i < 6; i++)
                {
                    if (btns[i] >= 128)
                    {
                        info.wButtons = i + 1;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetJoyInfo Error:{0}\r\n{1}", ex.Message, ex.StackTrace);
                return false;
            }

            return true;
        }

    }
}
