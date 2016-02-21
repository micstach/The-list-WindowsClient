using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebClient
{
    [ComVisible(true)]
    public class DesktopClientBridge
    {
        public DesktopClientBridge()
        {
        }

        public void SetUserCredentials(string user, string password)
        {
            (new Credentials(user, password)).Save();
        }
    }
}
