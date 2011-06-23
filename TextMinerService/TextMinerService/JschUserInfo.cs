using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh.jsch;

namespace TextMiner
{
    public class JschUserInfo : UserInfo
    {
        private String passwd;
        public String getPassword() { return passwd; }

        public bool promptYesNo(String str)
        {
            return true;
        }

        public String getPassphrase() { return null; }
        public bool promptPassphrase(String message) { return true; }
        public bool promptPassword(String message) { return true; }
        public void showMessage(String message) { }
    }
}
