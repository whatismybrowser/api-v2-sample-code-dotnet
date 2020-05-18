using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class UserAgentParseDataBatchRequest
    {
        [JsonProperty("user_agents")]
        public Dictionary<string, string> UserAgents { get; set; }

        [JsonProperty("parse_options")]
        public UserAgentParseDataRequestParseOptions ParseOptions { get; set; }
    }
}