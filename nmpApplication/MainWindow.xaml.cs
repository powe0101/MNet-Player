using System;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;

//https://user.interest.me/common/login/login.html?siteCode=S20&returnURL=http://www.mnet.com/player/aod/#
//http://search.api.mnet.com/search/song?q=%EB%8F%84%EA%B9%A8%EB%B9%84&domainCd=0&sort=r&pageNum=1&callback=angular.callbacks._0

namespace nmpApplication
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : MetroWindow
    {
        const int SEARCH_WIDTH = 0; //Search Form Width
        const string url = "http://www.mnet.com/player/aod/";
        const string loginUrl = "https://user.interest.me/common/login/login.html?siteCode=S20&returnURL=http://www.mnet.com/player/aod/#";

        static MainWindow uniQueInstance = null; 
        static readonly object padlock = new object();

        Notification notification = null;
        LoginProcess lp = null;
        mshtml.IHTMLElementCollection elementTagList = null;

        private MainWindow()
        {
            InitializeComponent();
        }

        public static MainWindow Instance
        {
            get
            {
                if(uniQueInstance == null) 
                    lock (padlock)
                        if (uniQueInstance == null)
                            uniQueInstance = new MainWindow();
                    return uniQueInstance;
            }
            
            private set{;}
        } // Singleton instance call by App
     
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(notification != null)
                notification.Off();
            uniQueInstance = null;
        }


        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            //todo : 설정버튼
        }

        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Hidden;
            LoginDialogData result = await this.ShowLoginAsync("로그인", "아이디와 비밀번호를 입력해주세요.", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "" });
            
            if (result == null)
            {
                MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            lp = new LoginProcess();
            lp.Data(result);
            
            mainBrowser.Navigate(loginUrl);
            //todo : 로그인 버튼
          
        }

        private void btnTray_Click(object sender, RoutedEventArgs e)
        {
            if (notification == null)
                notification = new Notification();

            notification.On();
            notification.SetSetting();

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
            mainBrowser.Navigate(url);
            mainBrowser.LoadCompleted += MainBrowser_LoadCompleted;
        }

        private void MainBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var document = mainBrowser.Document as mshtml.HTMLDocument;

            if(document.url == url)
                MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Visible;
            else if (document.url == loginUrl && lp != null)
                lp.Process(document);
            //로그인에 대한 정보가 생성되었을 경우.
            
            (sender as System.Windows.Controls.WebBrowser).InvokeScript("eval", "$(document).contextmenu(function() {    return false;        });");
            Element.DeleteBrowserElementByClassName("A", "btnLogin", "로그인", sender as System.Windows.Controls.WebBrowser);

        }

        private void testList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void SongSearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
