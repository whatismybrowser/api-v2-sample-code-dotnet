using System;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentDatabaseDump
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("num_of_useragents")]
        public long NumberOfUserAgents { get; set; }

        [JsonProperty("file_format")]
        public string FileFormat { get; set; }

        [JsonProperty("sha_sum_256")]
        public string ShaSum256 { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }
}