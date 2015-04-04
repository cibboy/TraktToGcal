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
        [JsonPropertyAttribute("refreshtoken")]
        public virtual string RefreshToken { get; set; }
        [JsonPropertyAttribute("authorization")]
        public virtual string Authorization { get; set; }
        [JsonPropertyAttribute("expiration")]
        public virtual DateTime Expiration { get; set; }

        private TraktAuthorization() {
            this.ClientID = "";
            this.ClientSecret = "";
            this.AccessToken = "";
            this.RefreshToken = "";
            this.Authorization = "";
            this.Expiration = DateTime.UtcNow;
        }

        public async Task<bool> EnsureAccessTokenAsync(DefaultProperties Settings) {
            try {
                if (string.IsNullOrWhiteSpace(AccessToken) || DateTime.UtcNow.CompareTo(this.Expiration) > 0) {
                    // Start with refresh token.
                    string code = this.Authorization;

                    // If refresh token is non-existent, ask for new authorization code.
                    if (string.IsNullOrWhiteSpace(this.Authorization)) {
                        // Launch authorization url.
                        Process.Start("https://trakt.tv/oauth/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=urn:ietf:wg:oauth:2.0:oob&username=" + Settings.TraktUser);
                        // Wait for user to insert code.
                        AuthCodeInputDialog b = new AuthCodeInputDialog();
                        b.ShowDialog();

                        code = b.AuthorizationCode.Text;
                    }

                    // Create token request.
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.trakt.tv/oauth/token");
                    request.KeepAlive = true;
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    // Prepare post payload.
                    string payload = "{" +
                        "\"code\": \"" + code + "\"," +
                        "\"client_id\": \"" + ClientID + "\"," +
                        "\"client_secret\": \"" + ClientSecret + "\"," +
                        "\"redirect_uri\": \"urn:ietf:wg:oauth:2.0:oob\",";
                    if (string.IsNullOrWhiteSpace(this.RefreshToken))
                        payload += "\"grant_type\": \"authorization_code\"";
                    else {
                        payload += "\"grant_type\": \"refresh_token\",";
                        payload += "\"refresh_token\": \"" + this.RefreshToken + "\"";
                    }
                    payload += "}";

                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(payload);
                    request.ContentLength = byteArray.Length;
                    using (var writer = request.GetRequestStream()) { writer.Write(byteArray, 0, byteArray.Length); }
                    // Get authorization code.
                    using (var response = await request.GetResponseAsync() as System.Net.HttpWebResponse) {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream())) {
                            JObject o = JObject.Parse(reader.ReadToEnd());
                            
                            // Read return json and store tokens and expiration.
                            AccessToken = (string)o["access_token"];
                            RefreshToken = (string)o["refresh_token"];
                            Authorization = code;

                            Expiration = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            Expiration = Expiration.AddSeconds((long)o["created_at"]);
                            Expiration = Expiration.AddSeconds((long)o["expires_in"]);
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

            // Make sure folder exists, create it otherwise.
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
