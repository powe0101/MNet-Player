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
        const string mainUrl = "http://www.mnet.com/player/aod/";
        const string loginUrl = "https://user.interest.me/common/login/login.html?siteCode=S20&returnURL=http://www.mnet.com/player/aod/#";

        static MainWindow uniQueInstance = null; 
        static readonly object padlock = new object();

        Notification notification = null;
        Login lp = null;

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

        // 로그인 버튼 클릭시 웹 브라우저 숨기고 로그인 다이얼로그 표시
        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Hidden;
            LoginDialogData result = await this.ShowLoginAsync("로그인", "아이디와 비밀번호를 입력해주세요.", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "", RememberCheckBoxVisibility = Visibility.Visible, UsernameWatermark = "아이디" , PasswordWatermark = "비밀번호"});
            
            if (result == null){ MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Visible;return;}

            if(lp == null) lp = new Login();
            lp.InjectionResult(result);
            
            mainBrowser.Navigate(loginUrl);
            
          
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
            mainBrowser.Navigate(mainUrl);
            mainBrowser.LoadCompleted += MainBrowser_LoadCompleted;
        }

        private void MainBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var document = mainBrowser.Document as mshtml.HTMLDocument;

            if (document.url == mainUrl)
                MainWindow.Instance.mainBrowser.Visibility = System.Windows.Visibility.Visible;
            //로그인에 대한 정보가 생성되었을 경우.
            else if (document.url == loginUrl && lp != null)
            {
                btnLogin.Content = "로그아웃";
                lp.Process(document);
            }
           
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
