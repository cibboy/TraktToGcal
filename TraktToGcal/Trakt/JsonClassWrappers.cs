using System.Collections.Generic;

namespace TraktToGcal.Trakt {
    class Episode {
        [Newtonsoft.Json.JsonPropertyAttribute("season")]
        public virtual int Season { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("number")]
        public virtual int Number { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("overview")]
        public virtual string Overview { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("url")]
        public virtual string Url { get; set; }
    }

    class Show {
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
    }

    class EpisodeContainer {
        [Newtonsoft.Json.JsonPropertyAttribute("show")]
        public virtual Show Show { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("episode")]
        public virtual Episode Episode { get; set; }
    }

    class Entry {
        [Newtonsoft.Json.JsonPropertyAttribute("date")]
        public virtual string Date { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("episodes")]
        public virtual List<EpisodeContainer> Episodes { get; set; }
    }
}
