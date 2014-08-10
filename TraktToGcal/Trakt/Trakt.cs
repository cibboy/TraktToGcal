using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TraktToGcal.Trakt {
    class Trakt {
        private string apiKey = "";
        private string userName = "";

        public List<string> Excludes { get; set; }

        public Trakt() {
            this.Excludes = new List<string>();
            //TODO: load api key and username from file
        }

        public void AddNewExclude(string Value) {
            string val = Value.ToLowerInvariant();
            if (!this.Excludes.Contains(val))
                this.Excludes.Add(val);
        }

        //TODO: make it async
        public List<Entry> Get(DateTime from, int NumDays) {
            // Convert from date for url.
            string date = from.ToString("yyyyMMdd");

            Excludes = new List<string>();

            // Load from trakt.
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://api.trakt.tv/user/calendar/shows.json/" + this.apiKey + "/" + userName + "/" + date + "/" + NumDays);
            request.Method = "GET";
            
            // Parse entries and return.
            return JsonConvert.DeserializeObject<List<Entry>>(new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd());
        }
    }
}
