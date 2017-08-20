using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmpApplication
{
    class Notification
    {
        System.Windows.Forms.NotifyIcon ni = null;
        MainWindow Window = MainWindow.Instance;

        public void On()
        { if(ni == null) RegisterNotificationIcon(); ni.Visible = true; }
        public void Off() { ni.Visible = false; if (ni != null) Release(); }

        private void Release()
        {
            ni = null;
        }

        public void SetSetting()
        {
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Window.Show();
                    Window.WindowState = System.Windows.WindowState.Normal;
                    Off();
                };

            ni.Click +=
                delegate (object sender, EventArgs e)
                {
                    //todo : 트레이 아이콘 재생
                };
        }

        private void RegisterNotificationIcon()
        {
            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = Properties.Resources.logo;
            ni.Text = "엠넷 플레이어 0.5";
        }
    }
}
