using System;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentMetaData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("times_seen")]
        public long TimesSeen { get; set; }

        [JsonProperty("first_seen_at")]
        public DateTimeOffset FirstSeenAt { get; set; }

        [JsonProperty("last_seen_at")]
        public DateTimeOffset LastSeenAt { get; set; }
    }
}