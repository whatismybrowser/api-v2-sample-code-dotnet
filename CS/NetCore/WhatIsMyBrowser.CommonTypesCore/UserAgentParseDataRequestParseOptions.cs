using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class UserAgentParseDataRequestParseOptions
    {
        [JsonProperty("allow_servers_to_impersonate_devices", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowServersToImpersonateDevices { get; set; }

        [JsonProperty("return_metadata_for_useragent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReturnMetadataForUserAgent { get; set; }

        [JsonProperty("dont_sanitize", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DoNotSanitize { get; set; }
    }
}