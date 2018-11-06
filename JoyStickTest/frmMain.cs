using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Microsoft.DirectX.DirectInput;

namespace JoyStickTest
{
    public partial class frmMain : Form
    {
        private List<int> m_currentConnectedIds;

        const int WM_DEVICECHANGE = 0x219;//U盘插入后,OS的底层自动检测到,然后向应用程序发送"硬件设备状态改变"的消息
        const int DBT_DEVICEARRIVAL = 0x8000;//表示U盘可用
        const int DBT_DEVICEREMOVECOMPLETE = 0x8004;//一个设备或媒体片已被删除
        const int DBT_USB = 0x0007;//USB输入设备有变化

        protected override void WndProc(ref Message m)
        {
            bool msgValid = true;
            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    case DBT_DEVICEARRIVAL://U盘插入
                        msgValid = false;
                        Console.WriteLine("-------------U盘插入------------");
                        break;
                    case DBT_DEVICEREMOVECOMPLETE://U盘卸载
                        msgValid = false;
                        Console.WriteLine("----------U盘拔出------------");
                        break;
                    case DBT_USB:
                        msgValid = false;
                        UpdateJoySticks();
                        Console.WriteLine("----------USB输入设备变化------------");
                        break;
                    default:
                        Console.WriteLine("----------未知设备变化------------");
                        break;
                }
            }
            if (msgValid)
                base.WndProc(ref m);
        }

        public frmMain()
        {
            InitializeComponent();
            this.Text = string.Format("JoyStick测试 - V{0}", Application.ProductVersion);


            decimal num = Properties.Settings.Default.JoyStickNum;
            if (num < nudJoyStickNum.Minimum) nudJoyStickNum.Value = nudJoyStickNum.Minimum;
            else if (num > nudJoyStickNum.Maximum) nudJoyStickNum.Value = nudJoyStickNum.Maximum;
            else nudJoyStickNum.Value = num;

            nudJoyStickNum.MouseWheel += nudJoyStickNum_MouseWheel;

            m_currentConnectedIds = new List<int>();
        }
        //禁用鼠标中间键
        void nudJoyStickNum_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            flpJoyStick.Controls.Clear();
            for (uint id = 0; id < nudJoyStickNum.Value; id++)
            {
                flpJoyStick.Controls.Add(new ucJoyStick(id));
            }
        }

        private void nudJoyStickNum_ValueChanged(object sender, EventArgs e)
        {
            flpJoyStick.Controls.Clear();
            for (uint id = 0; id < nudJoyStickNum.Value; id++)
            {
                flpJoyStick.Controls.Add(new ucJoyStick(id));
            }
            label1.Focus();
            Properties.Settings.Default.JoyStickNum = nudJoyStickNum.Value;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            SaveForm.SetSize(this);
            label1.Focus();
            UpdateJoySticks();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveForm.SaveSize(this);
            Properties.Settings.Default.Save();
            this.Visible = false;
            Thread.Sleep(100);
        }
        private void UpdateJoySticks()
        {

            List<Device> devices = JoyStick.GetDevices();
            List<int> m_ids = new List<int>();
            if (devices != null)
            {
                foreach (Device item in devices)
                {
                    int id = item.Properties.JoystickId;
                    //string guid = item.DeviceInformation.InstanceGuid.ToString();
                    if (flpJoyStick.Controls.Count >= id)
                    {
                        ucJoyStick ctr = flpJoyStick.Controls[id] as ucJoyStick;
                        ctr.Device = item;
                        Console.WriteLine("Set Device:{0}", id);
                    }
                    m_ids.Add(id);
                }
            }

            foreach (int id in m_currentConnectedIds)
            {
                if (m_ids.IndexOf(id) < 0 && flpJoyStick.Controls.Count >= id)
                {
                    ucJoyStick ctr = flpJoyStick.Controls[id] as ucJoyStick;
                    ctr.Device = null;
                    Console.WriteLine("Set null:{0}", id);
                }
            }

            m_currentConnectedIds = m_ids;
            //Thread.Sleep(200);

        }
    }
}
