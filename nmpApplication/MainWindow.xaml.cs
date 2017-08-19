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
        const int SEARCH_WIDTH = 0; //Search Form Width
        const string url = "http://www.mnet.com/player/aod/";

        static MainWindow uniQueInstance = null; 
        static readonly object padlock = new object();

        Notification notification = null;
        mshtml.IHTMLElementCollection elementTagList = null;

        public static MainWindow Instance
        {
            get
            {
                lock (padlock)
                    if (uniQueInstance == null)
                        uniQueInstance = new MainWindow();
                return uniQueInstance;
            }
            
            private set{;}
        } // Singleton instance call by App
     

        private MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            notification.Off();
            uniQueInstance = null;
        }


        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            //todo : 설정버튼
            MessageBox.Show("설정 누름");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //todo : 로그인 버튼
            mainBrowser.Navigate("https://user.interest.me/common/login/login.html?siteCode=S20&returnURL=http://www.mnet.com/player/aod/#");
        }

       

        private void btnTray_Click(object sender, RoutedEventArgs e)
        {
            notification = new Notification(this);
            notification.SettingNotify();
            notification.On();
            
            this.Hide();
        }
        
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Width += SEARCH_WIDTH;

            testList.Items.Clear();
            elementTagList = null;
            var document = mainBrowser.Document as mshtml.HTMLDocument;
            elementTagList = document.getElementsByTagName("span");

            foreach (mshtml.IHTMLElement element in elementTagList)
            {
                testList.Items.Add(element.innerText);
            }

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
            //todo : 웹브라우저 이동
            mainBrowser.LoadCompleted += MainBrowser_LoadCompleted;
        }

        private void MainBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ((System.Windows.Controls.WebBrowser)sender).InvokeScript("eval", "$(document).contextmenu(function() {    return false;        });");
            DeleteBrowserElementByClassName("a","btnLogin");
        }

        private void DeleteBrowserElementByClassName(string _tagName, string _className)
        {
            var document = mainBrowser.Document as mshtml.HTMLDocument;
            var tagList = document.getElementsByTagName(_tagName);
            foreach (mshtml.IHTMLElement element in tagList)
            {
                if (element.className == _className)
                {
                    element.outerHTML = "";
                }
            }
        }

        private void testList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show(testList.SelectedIndex.ToString());
            mshtml.IHTMLElement trayClickElement = elementTagList.item(testList.SelectedIndex);
            trayClickElement.click();
        }

        private void songSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            testList.Items.Clear();
            elementTagList = null;
            string searchText = searchTextBox.Text;

            
            searchBrowser.Navigate("http://search.mnet.com/search/song.asp?q="+searchText);
            var document = searchBrowser.Document as mshtml.HTMLDocument;
            elementTagList = document.getElementsByTagName("span");

            foreach (mshtml.IHTMLElement element in elementTagList)
            {
                testList.Items.Add(element.innerText);
            }
        }

    }
}


//https://user.interest.me/common/login/login.html?siteCode=S20&returnURL=http://www.mnet.com/player/aod/#


//http://search.api.mnet.com/search/song?q=%EB%8F%84%EA%B9%A8%EB%B9%84&domainCd=0&sort=r&pageNum=1&callback=angular.callbacks._0