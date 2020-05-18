using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentParseDataBatchResponseItem : BaseResponse
    {
        [JsonProperty("parse")]
        public UserAgentParseResponseParse Parse { get; set; }

        [JsonProperty("version_check")]
        public VersionCheck VersionCheck { get; set; }
    }
}