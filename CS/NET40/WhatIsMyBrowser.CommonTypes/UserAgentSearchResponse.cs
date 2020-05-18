using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class UserAgentSearchResponse: BaseResponse
    {
        [JsonProperty("search_results")]
        public SearchResults SearchResults { get; set; }
    }
}