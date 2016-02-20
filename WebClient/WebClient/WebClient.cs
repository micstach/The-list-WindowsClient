using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebClient
{
    public partial class WebClient : Form
    {
        public WebClient()
        {
            InitializeComponent();
        }

        private void WebClient_Load(object sender, EventArgs e)
        {
            string user = "";
            string password = "pwd";
            string host = "http://localhost";

            if (string.IsNullOrEmpty(user))
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
    }
}
