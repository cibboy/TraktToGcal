using System.IO;
using Newtonsoft.Json;

namespace TraktToGcal {
    class CustomCreds {
        [Newtonsoft.Json.JsonPropertyAttribute("googleuser")]
        public virtual string GoogleUser { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("traktapikey")]
        public virtual string TraktApiKey { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("traktuser")]
        public virtual string TraktUser { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("traktpassword")]
        public virtual string TraktPassword { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("trakthash")]
        public virtual string TraktHash { get; set; }

        private static CustomCreds _instance = null;

        public static CustomCreds GetInstance() {
            if (_instance != null)
                return _instance;

            return JsonConvert.DeserializeObject<CustomCreds>(File.ReadAllText("credentials.cred/custom.creds.json"));
        }
    }
}
