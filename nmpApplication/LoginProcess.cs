﻿using MahApps.Metro.Controls.Dialogs;
using System;

namespace nmpApplication
{
    class LoginProcess
    {
        private bool isAutologin = false;
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

        public void Data(LoginDialogData _result)
        {
            result = _result;
        }

        private void ClickElement(mshtml.IHTMLElement element, string innerText = null)
        {
            mshtml.IHTMLElement2 _el = element as mshtml.IHTMLElement2;
            _el.focus();

            if (innerText != null)
                element.innerText = innerText;
        }

        public void Process(mshtml.HTMLDocument document)
        {
            //MessageDialogResult messageResult = await ShowMessageAsync("Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            var userId = document.getElementById("userId");
            ClickElement(userId, result.Username);

            var userPW = document.getElementById("pw");
            ClickElement(userPW, result.Password);

            document.getElementById("loginSubmitBtn").click();
        }
    }
}