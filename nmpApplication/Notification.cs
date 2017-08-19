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
        MainWindow Window = null;

        public Notification(MainWindow mainWindow)
        {
            this.Window = mainWindow;
        }

        public void On()
        { ni.Visible = true; }
        public void Off() { ni.Visible = false; if(ni != null) ni = null; }

        public void SettingNotify()
        {
            if(ni == null)
                ni = new System.Windows.Forms.NotifyIcon();

            ni.Icon = Properties.Resources.logo;
            ni.Text = "엠넷 플레이어 0.5";

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
    }
}
