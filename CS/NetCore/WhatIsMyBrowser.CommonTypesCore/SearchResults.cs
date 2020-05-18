using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class SearchResults
    {
        [JsonProperty("user_agents")]
        public UserAgentType[] UserAgents { get; set; }

        [JsonProperty("search_meta_data")]
        public SearchMetaData SearchMetaData { get; set; }
    }
}