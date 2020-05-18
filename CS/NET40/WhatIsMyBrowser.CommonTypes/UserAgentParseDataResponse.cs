using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentParseDataResponse : BaseResponse
    {
        [JsonProperty("parse")]
        public UserAgentParseResponseParse Parse { get; set; }

        [JsonProperty("sanitization")]
        public Sanitization Sanitization { get; set; }

        [JsonProperty("version_check")]
        public VersionCheck VersionCheck { get; set; }
    }
}