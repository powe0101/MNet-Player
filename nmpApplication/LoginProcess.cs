namespace nmpApplication
{
    internal class LoginProcess
    {
        private bool isAutologin = false;
        private string id = "";
        private string pw = "";

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

        public LoginProcess()
        {
            //todo : 로그인 프로세스
        }
    }
}