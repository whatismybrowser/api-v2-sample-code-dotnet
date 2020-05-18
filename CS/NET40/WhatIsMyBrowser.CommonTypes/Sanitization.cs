using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class Sanitization
    {
        [JsonProperty("user_agent_sanitized")]
        public string UserAgentSanitized { get; set; }
    }
}