using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class UserAgentSearchResponse: BaseResponse
    {
        [JsonProperty("search_results")]
        public SearchResults SearchResults { get; set; }
    }
}