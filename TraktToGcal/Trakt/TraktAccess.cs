using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public static async Task<List<Entry>> GetEpisodes(DateTime From, int NumDays) {
            // Convert from date for url.
            string date = From.ToString("yyyyMMdd");

            CustomCreds creds = CustomCreds.GetInstance();

            // Load from trakt.
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.trakt.tv/user/calendar/shows.json/" + creds.TraktApiKey + "/" + creds.TraktUser + "/" + date + "/" + NumDays);
            // First check if there's a hash saved.
            string hash = creds.TraktHash;
            // If not, compute it from username and password, if present.
            if (hash == "" && creds.TraktPassword != "")
                hash = Convert.ToBase64String(Encoding.ASCII.GetBytes(creds.TraktUser + ":" + creds.TraktPassword));
            // If hash has been computed, use it, otherwise go blind, the user may be asking for public data.
            if (hash != "")
                request.Headers.Add("Authorization:Basic " + hash);
            request.Method = "GET";
            
            // Parse entries.
            WebResponse response = await request.GetResponseAsync();
            List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(new StreamReader(response.GetResponseStream()).ReadToEnd());
            
            return entries;
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
