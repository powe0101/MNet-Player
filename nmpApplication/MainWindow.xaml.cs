using System;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Media.Imaging;

namespace nmpApplication
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : MetroWindow
    {
        static MainWindow instance = null;
        static readonly object padlock = new object();
        string url = "http://www.mnet.com/player/aod/";
        System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

        BrowserEmulator browserEmulator = new BrowserEmulator(BrowserEmulator.BrowserEmulationVersion.Version11);

        public static MainWindow Instance
        {
            get
            {
                lock (padlock)
                    if (instance == null)
                        instance = new MainWindow();
                return instance;
            }
            
            private set{;}
        } // 인스턴스 생성용 게터/세터

        //static MainWindow()
        //{
        //    Instance = new MainWindow();
        //} 
        //todo : Static 생성자 사용해야 하는가?

        private MainWindow()
        {
            InitializeComponent();
        }


        private void trayIcon()
        {
            ni.Icon = Properties.Resources.logo;
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    ni.Visible = false;
                };
            ni.BalloonTipShown += 
                delegate (object sender, EventArgs e)
                {
                    ni.BalloonTipTitle = "test";
                };
        }
        
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
            ni.Visible = false;
        }

        private void WebBrowser_Initialized(object sender, EventArgs e)
        {
            mainBrowser.Navigate(url);
            
            //todo : 웹브라우저 이동
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            //todo : 설정버튼
            MessageBox.Show("설정 누름"); 
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //todo : 로그인 버튼
            MessageBox.Show("로그인 누름");
        }

        private void btnTray_Click(object sender, RoutedEventArgs e)
        {
            trayIcon();
            this.Hide();
        }

        private void titleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //todo : 투명도 조절
        }
    }
}
