using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TraktToGcal.Trakt {
    class TraktAccess {
        // Function to compute SHA1 hash, not used in basic authentication.
        private static string GetSHA1(string text) {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA1Managed hashString = new SHA1Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue) {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static async Task<List<Entry>> GetEpisodes(Credentials Creds, DateTime From, int NumDays) {
            // Convert from date for url. Use 1 day earlier due to trakt API v2, where local air time (i.e. U.S. Pacific) is used to compare from date.
            string date = From.AddDays(-1).ToString("yyyyMMdd");

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.trakt.tv/calendars/shows/" + date + "/" + NumDays + "?extended=full");
            request.KeepAlive = true;

            request.Method = "GET";

            // Add proper headers.
            request.ContentType = "application/json";
            request.Headers.Add("trakt-api-version", "2");
            request.Headers.Add("trakt-api-key", "");
            request.Headers.Add("Authorization", "Bearer ");
            request.ContentLength = 0;

            /*var request = System.Net.WebRequest.Create("https://api.trakt.tv/oauth/token") as System.Net.HttpWebRequest;
            request.KeepAlive = true;

            request.Method = "POST";

            request.ContentType = "application/json";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes("{" +
              "\"code\": \"\"," +
              "\"client_id\": \"\"," +
              "\"client_secret\": \"\"," +
              "\"redirect_uri\": \"urn:ietf:wg:oauth:2.0:oob\"," +
              "\"grant_type\": \"refresh_token\"" +
              "}"
            );
            request.ContentLength = byteArray.Length;
            using (var writer = request.GetRequestStream()){writer.Write(byteArray, 0, byteArray.Length);}
            string responseContent=null;
            using (var response = request.GetResponse() as System.Net.HttpWebResponse) {
              using (var reader = new System.IO.StreamReader(response.GetResponseStream())) {
                responseContent = reader.ReadToEnd();
              }
            }*/

            List<Entry> ret = new List<Entry>();
            // Load from trakt.
            WebResponse response = await request.GetResponseAsync();
            // Parse entries.
            Dictionary<string, List<Entry>> parsed = await JsonConvert.DeserializeObjectAsync<Dictionary<string, List<Entry>>>(new StreamReader(response.GetResponseStream()).ReadToEnd());
            foreach (List<Entry> l in parsed.Values)
                ret.AddRange(l);

            return ret;
        }

        public static string CleanSeriesTitle(string Title) {
            int index = Title.LastIndexOf(" (");
            if (index > 0)
                // Clean title from year.
                return Title.Substring(0, index);

            return Title;
        }

        public static string GetProperEpisodeNumber(int Number) {
            // Return 01, 02, 03 etc if single digit.
            if (Number < 10)
                return "0" + Number;

            return Number.ToString();
        }
    }
}
