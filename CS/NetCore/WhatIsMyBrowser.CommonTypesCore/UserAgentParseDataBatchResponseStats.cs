using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class UserAgentParseDataBatchResponseStats
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("error")]
        public int Error { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}