using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TraktToGcal {
    class DefaultProperties {
        [JsonPropertyAttribute("lookaheaddays")]
        public virtual int LookaheadDays { get; set; }
        [JsonPropertyAttribute("exclusions")]
        public virtual List<string> Exclusions { get; set; }
        [JsonPropertyAttribute("calendarname")]
        public virtual string CalendarName { get; set; }
        [JsonPropertyAttribute("includespecials")]
        public virtual bool IncludeSpecials { get; set; }
        [JsonPropertyAttribute("createalldayevents")]
        public virtual bool CreateAllDayEvents { get; set; }

        public DefaultProperties() {
            LookaheadDays = 7;
            Exclusions = new List<string>();
            CalendarName = "";
            IncludeSpecials = false;
            CreateAllDayEvents = false;
        }

        public static DefaultProperties Load() {
            DefaultProperties instance = new DefaultProperties();

            // Make sure folder exists, create is otherwise.
            if (!Directory.Exists("settings"))
                Directory.CreateDirectory("settings");
            // Make sure file exists, save default one otherwise.
            if (!File.Exists("settings/settings.json"))
                instance.Save();

            instance = JsonConvert.DeserializeObject<DefaultProperties>(File.ReadAllText("settings/settings.json"));

            return instance;
        }

        public void Save() {
            File.WriteAllText("settings/settings.json", JsonConvert.SerializeObject(this));
        }
    }
}
