using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentType
    {
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        [JsonProperty("user_agent_meta_data")]
        public UserAgentMetaData UserAgentMetaData { get; set; }

        [JsonProperty("parse")]
        public UserAgentSearchResponseParse Parse { get; set; }
    }
}