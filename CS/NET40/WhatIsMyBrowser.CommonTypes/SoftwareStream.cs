using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class SoftwareStream
    {
        [JsonProperty("latest_version")]
        public int[] LatestVersion { get; set; }

        [JsonProperty("download_url")]
        public Uri DownloadUrl { get; set; }

        [JsonProperty("update")]
        public string Update { get; set; }

        [JsonProperty("update_url")]
        public Uri UpdateUrl { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("package_type")]
        public string PackageType { get; set; }

        [JsonProperty("sample_user_agents")]
        public Dictionary<string, string[]> SampleUserAgents { get; set; }
    }
}