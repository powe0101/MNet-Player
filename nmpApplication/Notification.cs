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

        public void On()  { if (ni == null) RegisterNotificationIcon(); ni.Visible = true; }
        public void Off() { if (ni == null) return; ni.Visible = false;  Release(); }

        private void Release(){ ni = null; }

        public void SetSetting()
        {
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    MainWindow.Instance.Show();
                    MainWindow.Instance.WindowState = System.Windows.WindowState.Normal;
                    Off();
                };

            ni.Click +=
                delegate (object sender, EventArgs e)
                {
                    //todo : 트레이 아이콘 재생
                };
        }

        public void SetTitleText(string _text)
        {
            if (ni != null)
                ni.Text = _text;
        }
        
        private void RegisterNotificationIcon()
        {
            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = Properties.Resources.logo;
        }
    }
}
