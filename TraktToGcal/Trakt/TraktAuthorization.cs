using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
                        Process.Start("https://trakt.tv/oauth/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=urn:ietf:wg:oauth:2.0:oob");
                        // Wait for user to insert code.
                        AuthCodeInputDialog b = new AuthCodeInputDialog();
                        b.ShowDialog();

                        code = b.AuthorizationCode.Text;
                    }

                    // Create token request.
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri("https://api.trakt.tv/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("User-Agent", "TraktToGcal");

                        // Prepare post payload.
                        string payload;
                        if (string.IsNullOrWhiteSpace(RefreshToken)) {
                            payload = JsonConvert.SerializeObject(new {
                                code,
                                client_id = ClientID,
                                client_secret = ClientSecret,
                                redirect_uri = "urn:ietf:wg:oauth:2.0:oob",
                                grant_type = "authorization_code"
                            });
                        }
                        else {
                            payload = JsonConvert.SerializeObject(new {
                                code,
                                client_id = ClientID,
                                client_secret = ClientSecret,
                                redirect_uri = "urn:ietf:wg:oauth:2.0:oob",
                                grant_type = "refresh_token",
                                refresh_token = RefreshToken
                            });
                        }

                        HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("oauth/token", content);

                        if (response.IsSuccessStatusCode) {
                            string res = await response.Content.ReadAsStringAsync();
                            JObject o = JObject.Parse(res);

                            // Read return json and store tokens and expiration.
                            AccessToken = (string)o["access_token"];
                            RefreshToken = (string)o["refresh_token"];
                            Authorization = code;

                            Expiration = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            Expiration = Expiration.AddSeconds((long)o["created_at"]);
                            Expiration = Expiration.AddSeconds((long)o["expires_in"]);

                            // Save file with access token for later use.
                            Save();
                        }
                    }
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
