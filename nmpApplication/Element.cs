using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmpApplication
{
    static class Element
    {
        static bool isInit = false;
        
        static public void DeleteBrowserElementByClassName(string _tagName, string _className,string _innerText, System.Windows.Controls.WebBrowser _browser)
        {
            var document = _browser.Document as mshtml.HTMLDocument;
            var tagList = document.getElementsByTagName(_tagName);
            
            foreach (mshtml.IHTMLElement element in tagList)
            {
                if (element.className == _className && element.tagName == _tagName && element.innerText == _innerText)
                {
                    element.outerHTML = "";
                }
            }
        }

    }
}
