using System;
using System.Collections.Generic;

namespace TraktToGcal.Trakt {
    class EntryId {
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
        public virtual EntryId Ids { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("first_aired")]
        public virtual DateTime Aired { get; set; }
    }

    class Show {
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("runtime")]
        public virtual int? Runtime { get; set; }
    }

    class EpisodeEntry {
        [Newtonsoft.Json.JsonPropertyAttribute("show")]
        public virtual Show Show { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("episode")]
        public virtual Episode Episode { get; set; }
    }

    class Movie {
        [Newtonsoft.Json.JsonPropertyAttribute("title")]
        public virtual string Title { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("year")]
        public virtual int? Year { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("ids")]
        public virtual EntryId Ids { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("tagline")]
        public virtual string Tagline { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("overview")]
        public virtual string Overview { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("runtime")]
        public virtual int? Runtime { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("certification")]
        public virtual string Certification { get; set; }
    }

    class MovieEntry {
        [Newtonsoft.Json.JsonPropertyAttribute("released")]
        public virtual DateTime Released { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("movie")]
        public virtual Movie Movie { get; set; }
    }
}
