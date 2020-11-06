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

        public static async Task<List<Entry>> GetEpisodes(DefaultProperties Properties, DateTime From, int NumDays) {
            // Load trakt authorization from file.
            TraktAuthorization auth = TraktAuthorization.Load();
            // Get an access token if not already present.
            if (!(await auth.EnsureAccessTokenAsync(Properties)))
                throw new Exception("There was a problem getting an access token for the trakt.tv api.");

            // Convert from date for url. Use 1 day earlier due to trakt API v2, where local air time (i.e. U.S. Pacific) is used to compare from date.
            string date = From.AddDays(-1).ToString("yyyy-MM-dd");

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.trakt.tv/calendars/my/shows/" + date + "/" + NumDays + "?extended=full");
            request.KeepAlive = true;

            request.Method = "GET";

            // Add proper headers.
            request.ContentType = "application/json";
            request.Headers.Add("trakt-api-version", "2");
            request.Headers.Add("trakt-api-key", auth.ClientID);
            request.Headers.Add("Authorization", "Bearer " + auth.AccessToken);
            request.ContentLength = 0;

            // Load from trakt.
            WebResponse response = await request.GetResponseAsync();
            // Parse entries.
            List<Entry> parsed = await JsonConvert.DeserializeObjectAsync<List<Entry>>(new StreamReader(response.GetResponseStream()).ReadToEnd());

            return parsed;
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
