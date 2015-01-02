using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TraktToGcal.Trakt {
    class TraktAuthorization {
        [JsonPropertyAttribute("clientid")]
        public virtual string ClientID { get; set; }
        [JsonPropertyAttribute("clientsecret")]
        public virtual string ClientSecret { get; set; }
        [JsonPropertyAttribute("accesstoken")]
        public virtual string AccessToken { get; set; }

        private TraktAuthorization() {
            this.ClientID = "";
            this.ClientSecret = "";
            this.AccessToken = "";
        }

        public async Task<bool> EnsureAccessTokenAsync(DefaultProperties Settings) {
            try {
                if (string.IsNullOrWhiteSpace(AccessToken)) {
                    // Launch authorization url.
                    Process.Start("https://trakt.tv/oauth/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=urn:ietf:wg:oauth:2.0:oob&username=" + Settings.TraktUser);
                    // Wait for user to inser code.
                    AuthCodeInputDialog b = new AuthCodeInputDialog();
                    b.ShowDialog();

                    // Create token request.
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.trakt.tv/oauth/token");
                    request.KeepAlive = true;
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    // Prepare post payload.
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes("{" +
                      "\"code\": \"" + b.AuthorizationCode.Text + "\"," +
                      "\"client_id\": \"" + ClientID + "\"," +
                      "\"client_secret\": \"" + ClientSecret + "\"," +
                      "\"redirect_uri\": \"urn:ietf:wg:oauth:2.0:oob\"," +
                      "\"grant_type\": \"authorization_code\"" +
                      "}"
                    );
                    request.ContentLength = byteArray.Length;
                    using (var writer = request.GetRequestStream()) { writer.Write(byteArray, 0, byteArray.Length); }
                    // Get authorization code.
                    using (var response = await request.GetResponseAsync() as System.Net.HttpWebResponse) {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream())) {
                            // Read return json and store access token.
                            AccessToken = (string)JObject.Parse(reader.ReadToEnd())["access_token"];
                        }
                    }

                    // Save file with access token for later use.
                    Save();
                }

                if (string.IsNullOrWhiteSpace(AccessToken))
                    return false;
                else
                    return true;
            }
            catch {
                return false;
            }
        }

        public static TraktAuthorization Load() {
            TraktAuthorization instance = new TraktAuthorization();

            // Make sure folder exists, create is otherwise.
            if (!Directory.Exists("settings"))
                Directory.CreateDirectory("settings");
            // Make sure file exists, save default one otherwise.
            if (!File.Exists("settings/traktauth.json"))
                instance.Save();

            instance = JsonConvert.DeserializeObject<TraktAuthorization>(File.ReadAllText("settings/traktauth.json"));

            return instance;
        }

        public void Save() {
            File.WriteAllText("settings/traktauth.json", JsonConvert.SerializeObject(this));
        }
    }
}
