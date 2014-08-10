using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TraktToGcal.Trakt {
    class TraktAccess {
        public static async Task<List<Entry>> GetEpisodes(DateTime From, int NumDays) {
            // Convert from date for url.
            string date = From.ToString("yyyyMMdd");

            CustomCreds creds = CustomCreds.GetInstance();

            // Load from trakt.
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://api.trakt.tv/user/calendar/shows.json/" + creds.TraktApiKey + "/" + creds.TraktUser + "/" + date + "/" + NumDays);
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
