using System;
using System.Windows.Controls;

namespace nmpApplication
{
    internal class ControlBrowser
    {
        WebBrowser mainBrowser;
        public string url = "http://www.mnet.com/player/aod/";

        public ControlBrowser(WebBrowser wb)
        {
            mainBrowser = wb;
        }
        
    }
}