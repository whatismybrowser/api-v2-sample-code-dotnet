using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class SearchMetaData
    {
        [JsonProperty("num_of_results_returned")]
        public long NumOfResultsReturned { get; set; }

        [JsonProperty("search_took_milliseconds")]
        public long SearchTookMilliseconds { get; set; }

        [JsonProperty("search_parameters")]
        public Dictionary<string, object> SearchParameters { get; set; }
    }
}