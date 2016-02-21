using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebClient
{
    public partial class WebClient : Form
    {
        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);
        const int URLMON_OPTION_USERAGENT = 0x10000001;

        public void ChangeUserAgent(string Agent)
        {
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, Agent, Agent.Length, 0);
        }

        DesktopClientBridge _bridge = new DesktopClientBridge();

        public WebClient()
        {
            InitializeComponent();

            ChangeUserAgent("desktop client");
        }

        private void WebClient_Load(object sender, EventArgs e)
        {
            Credentials credentials = new Credentials();
            credentials.Load();

            string user = credentials.User;
            string password = credentials.Password;

            string localHost = "http://localhost";
            string remoteHost = "http://todo-micstach.rhcloud.com";
            string host = (Properties.Settings.Default.Environment == "production") ? remoteHost : localHost;

            browser.ObjectForScripting = _bridge;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                browser.Url = new Uri(host);
            }
            else
            {
                string url = host + "/login";

                String postdata = "user=" + user + "&pwd=" + password;
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                byte[] bytes = encoding.GetBytes(postdata);

                browser.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                // (!) Inject javascript code
                HtmlDocument doc = browser.Document;
                HtmlElement script = doc.CreateElement("script");
                script.InnerText = "function desktopClient_SetUserCredentials(user, password) { window.external.SetUserCredentials(user, password); }";
                doc.GetElementsByTagName("head")[0].AppendChild(script);
            }
            catch
            {
            }
        }
    }
}
