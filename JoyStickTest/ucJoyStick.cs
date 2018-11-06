using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.DirectX.DirectInput;

namespace JoyStickTest
{
    public partial class ucJoyStick : UserControl
    {
        public ucJoyStick(uint id)
        {
            InitializeComponent();
            m_Id = id;
            m_JoyStickName = (id + 1).ToString();
            grpJoyStick.Text = m_JoyStickName + " OFF";
        }

        #region fields
        private string m_JoyStickName;

        private uint m_Id;
        private Device m_Device = null;

        private bool m_IsButton = false;
        private uint m_buttonVal = 4;

        private Brush m_brushLight = new SolidBrush(Color.FromArgb(255, 0, 0));
        private Brush m_brushDefault = new SolidBrush(Color.FromArgb(128, 0, 0));
        private Brush m_brushStr = new SolidBrush(Color.Black);

        private bool m_IsLastOK = false;

        #endregion

        #region props
        public uint Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        public Device Device
        {
            get { return m_Device; }
            set { m_Device = value; }
        }
        public string JoyStickName
        {
            get { return m_JoyStickName; }
            set
            {
                m_JoyStickName = value;
                grpJoyStick.Text = m_JoyStickName;
            }
        }
        #endregion

        private void ucJoyStick_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(ListenJoyStick));
            th.IsBackground = true;
            th.Start();
        }
        private void ListenJoyStick()
        {
            while (this.Visible)
            {
                JoyStick.JoyInfo info = new JoyStick.JoyInfo();
                //JoyStick.JoyInfoStatus status = JoyStick.GetJoyPos(m_Id, out info);
                bool isOK = false;// (status == JoyStick.JoyInfoStatus.OK);
                if (m_Device == null) { isOK = false; Thread.Sleep(100); }
                else { isOK = JoyStick.GetJoyInfo(m_Device, out info); }
                if (isOK)
                {
                    if (!m_IsLastOK)
                    {
                        SetGroupBoxText(grpJoyStick, string.Format("{0} ON", (m_Id + 1).ToString()));
                    }
                    int val = C1Round(info.wZpos, 258) - 127;
                    SetTextBoxText(txtJoyStickButton, val.ToString());
                    if (info.wButtons != 0)
                    {
                        m_buttonVal = info.wButtons;
                        if (!m_IsButton)
                        {
                            m_IsButton = true;
                            picCircle.Invalidate();
                        }
                    }
                    else
                    {
                        if (m_IsButton)
                        {
                            m_IsButton = false;
                            picCircle.Invalidate();
                        }
                    }
                    SetTextBoxText(txtJoyStickPos, string.Format("xPos:{0}\r\nyPos:{1}\r\nzPos:{2}", info.wXpos, info.wYpos, info.wZpos));
                }
                else if (m_IsLastOK)
                {
                    SetGroupBoxText(grpJoyStick, string.Format("{0} OFF", (m_Id + 1).ToString()));
                    JoyStick.JoyReleaseCapture(m_Id);
                    m_IsButton = false;
                    SetTextBoxText(txtJoyStickButton, string.Empty);
                    SetTextBoxText(txtJoyStickPos, string.Empty);
                }

                m_IsLastOK = isOK;
                Application.DoEvents();
                Thread.Sleep(50);
            }
        }
        private int C1Round(uint v1, int v2)
        {
            int result;
            double val = (double)v1 / (double)v2;
            result = (int)Math.Floor(val);
            if (val - result >= 0.5)
            {
                return result + 1;
            }
            else
            {
                return result;
            }
        }
        private delegate void DelSetLabelText(Label tbox, string text);
        public static void SetLabelText(Label tbox, string text)
        {
            if (tbox.InvokeRequired)
            {
                tbox.BeginInvoke(new DelSetLabelText(SetLabelText), new object[] { tbox, text });
            }
            else
            {
                tbox.Text = text;
            }
        }
        private delegate void DelSetTextBoxText(TextBox tbox, string text);
        public static void SetTextBoxText(TextBox tbox, string text)
        {
            if (tbox.InvokeRequired)
            {
                tbox.BeginInvoke(new DelSetTextBoxText(SetTextBoxText), new object[] { tbox, text });
            }
            else
            {
                tbox.Text = text;
            }
        }
        private delegate void DelSetGroupBoxText(GroupBox tbox, string text);
        public static void SetGroupBoxText(GroupBox tbox, string text)
        {
            if (tbox.InvokeRequired)
            {
                tbox.BeginInvoke(new DelSetGroupBoxText(SetGroupBoxText), new object[] { tbox, text });
            }
            else
            {
                tbox.Text = text;
            }
        }

        private void picCircle_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, picCircle.Width - 1, picCircle.Height - 1);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (m_IsButton)
            {
                g.FillEllipse(m_brushLight, rect);
                g.DrawString(m_buttonVal.ToString(), new Font("Arial", 15), m_brushStr,
                    new RectangleF(0, 0, picCircle.Width - 1, picCircle.Height + 1),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                //g.FillEllipse(m_brushDefault, rect);
            }
            else
            {
                //Thread.Sleep(50);
            }
        }

        private void grpJoyStick_Paint(object sender, PaintEventArgs e)
        {
            Font font = new System.Drawing.Font("黑体", 15);
            Graphics graphics = e.Graphics;
            GroupBox grp = sender as GroupBox;
            graphics.Clear(grp.BackColor);


            graphics.DrawString(grp.Text, font, grp.Text.Contains("ON") ? Brushes.LimeGreen : Brushes.Red, 30, -1);

            Pen pen = new Pen(Color.Black);
            float width = graphics.MeasureString(grp.Text, font).Width;
            graphics.DrawLine(pen, 1, 7, 30, 7);
            graphics.DrawLine(pen, width + 31, 7, grp.Width - 2, 7);
            graphics.DrawLine(pen, 1, 7, 1, grp.Height - 2);
            graphics.DrawLine(pen, 1, grp.Height - 2, grp.Width - 2, grp.Height - 2);
            graphics.DrawLine(pen, grp.Width - 2, 7, grp.Width - 2, grp.Height - 2);
        }
    }
}
