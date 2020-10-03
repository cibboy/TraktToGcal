using System;
using System.Collections.Generic;

namespace TraktToGcal.Trakt {
    class EpisodeId {
        [Newtonsoft.Json.JsonPropertyAttribute("trakt")]
        public virtual string TraktId { get; set; }
    }

    class Episode {
        [Newtonsoft.Json.JsonPropertyAttribute("season")]
        public virtual int Season { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("number")]
        public virtual int Number { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("overview")]
        public virtual string Overview { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("ids")]
        public virtual EpisodeId Ids { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("first_aired")]
        public virtual DateTime Aired { get; set; }
    }

    class Show {
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("runtime")]
        public virtual int? Runtime { get; set; }
    }

    class Entry {
        [Newtonsoft.Json.JsonPropertyAttribute("show")]
        public virtual Show Show { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("episode")]
        public virtual Episode Episode { get; set; }
    }
}
