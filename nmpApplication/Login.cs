using MahApps.Metro.Controls.Dialogs;
using System;

namespace nmpApplication
{
    class Login
    {
        private bool isAutologin = false;
        private bool isLogin = false;
        private string id = "";
        private string pw = "";

        LoginDialogData result = null;

        /**getter setter**/
        public bool IsAutologin
        {
            get
            {
                return isAutologin;
            }

            set
            {
                isAutologin = value;
            }
        }
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public string Pw
        {
            get
            {
                return pw;
            }

            set
            {
                pw = value;
            }
        }
        public bool IsLogin
        {
            get
            {
                return isLogin;
            }

            set
            {
                isLogin = value;
            }
        }

        public void InjectionResult(LoginDialogData _result)
        {
            result = _result;
        }

        private void ClickElement(mshtml.IHTMLElement element, string innerText = null)
        {
            mshtml.IHTMLElement2 _el = element as mshtml.IHTMLElement2;
            _el.focus();

            if (innerText != null)
                element.innerText = innerText;
            else
                Console.WriteLine("아이디 또는 비밀번호가 올바르지 않습니다.");
        }

        public void Process(mshtml.HTMLDocument document)
        {
            //MessageDialogResult messageResult = await ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            var userId = document.getElementById("userId");
            ClickElement(userId, result.Username);

            var userPW = document.getElementById("pw");
            ClickElement(userPW, result.Password);

            document.getElementById("loginSubmitBtn").click();
            isLogin = true;
        }
    }
}