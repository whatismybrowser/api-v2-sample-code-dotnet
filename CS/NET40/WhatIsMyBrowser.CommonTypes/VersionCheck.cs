using System;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class VersionCheck
    {
        [JsonProperty("is_checkable")]
        public bool IsCheckable { get; set; }

        [JsonProperty("is_up_to_date")]
        public bool IsUpToDate { get; set; }

        [JsonProperty("latest_version")]
        public string[] LatestVersion { get; set; }

        [JsonProperty("download_url")]
        public Uri DownloadUrl { get; set; }

        [JsonProperty("update_url")]
        public Uri UpdateUrl { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }
    }
}