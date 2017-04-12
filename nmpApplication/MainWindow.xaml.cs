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
        const int SEARCH_WIDTH = 0;

        static MainWindow instance = null;
        static readonly object padlock = new object();
        static System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        BrowserEmulator browserEmulator = new BrowserEmulator(BrowserEmulator.BrowserEmulationVersion.Version11);

        ControlBrowser controlBrowser;
        ControlTray controlTray;

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
            controlBrowser = new ControlBrowser(mainBrowser);
            controlTray = new ControlTray(ni);
            TrayIcon();
        }

        private void TrayIcon()
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

            ni.Click +=
                delegate (object sender, EventArgs e)
                {
                    //todo : 트레이 아이콘 재생
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
            this.Hide();
        }
        
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Width += SEARCH_WIDTH;
            searchFlyout.IsOpen ^= true;
        }

        private void yourMahAppFlyout_ClosingFinished(object sender, RoutedEventArgs e)
        {
            this.Width -= SEARCH_WIDTH;
        }


        private void yourMahAppFlyout_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void WebBrowser_Initialized(object sender, EventArgs e)
        {
            mainBrowser.Navigate(controlBrowser.url);
            //todo : 웹브라우저 이동
            mainBrowser.LoadCompleted += MainBrowser_LoadCompleted;
        }

        private void MainBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var document = mainBrowser.Document as mshtml.HTMLDocument;
            var inputs = document.getElementsByTagName("button");
            foreach (mshtml.IHTMLElement element in inputs)
            {
                testList.Items.Add(element.innerText);
            }
        }
    }
}
