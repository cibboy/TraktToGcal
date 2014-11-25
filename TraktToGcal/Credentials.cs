using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TraktToGcal {
    class Credentials {
        [JsonPropertyAttribute("googleuser")]
        public virtual string GoogleUser { get; set; }

        [JsonPropertyAttribute("traktapikey")]
        public virtual string TraktApiKey { get; set; }
        [JsonPropertyAttribute("traktuser")]
        public virtual string TraktUser { get; set; }
        [JsonPropertyAttribute("trakthash")]
        public virtual string TraktHash { get; set; }

        public Credentials() {
            GoogleUser = "";
            TraktApiKey = "";
            TraktUser = "";
            TraktHash = "";
        }

        public static Credentials Load() {
            Credentials instance = new Credentials();

            // Make sure folder exists, create is otherwise.
            if (!Directory.Exists("settings"))
                Directory.CreateDirectory("settings");
            // Make sure file exists, save default one otherwise.
            if (!File.Exists("settings/credentials.json"))
                instance.Save();

            instance = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText("settings/credentials.json"));

            return instance;
        }

        public void Save() {
            File.WriteAllText("settings/credentials.json", JsonConvert.SerializeObject(this));
        }

        public static string ComputeHash(string Username, string Password) {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + Password));
        }
    }
}
