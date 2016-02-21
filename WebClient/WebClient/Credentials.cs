using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TheListClient
{
    class Credentials
    {
        private const string KEY_USER = "user";
        private const string KEY_PASSWORD = "password";

        public string User
        {
            private set;
            get;
        }

        public string Password
        {
            private set;
            get;
        }

        public Credentials()
        {

        }

        public Credentials(string user, string password)
        {
            User = user;
            Password = password;
        }

        public void Save()
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            credentials.Add(KEY_USER, User);
            credentials.Add(KEY_PASSWORD, Password);

            // save json file
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(credentials);

            var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

            var base64 = System.Convert.ToBase64String(bytes);

            System.IO.File.WriteAllText("credentials.json", base64);
        }

        public void Load()
        {
            try
            {
                var base64 = System.IO.File.ReadAllText("credentials.json");

                var bytes = System.Convert.FromBase64String(base64);

                var jsonString = System.Text.Encoding.UTF8.GetString(bytes);

                var serializer = new JavaScriptSerializer();
                var json = (Dictionary<string, string>)serializer.Deserialize(jsonString, typeof(Dictionary<string, string>));

                User = json[KEY_USER];
                Password = json[KEY_PASSWORD];
            }
            catch
            { }
        }
    }

}
