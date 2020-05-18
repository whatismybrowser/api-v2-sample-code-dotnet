using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class SoftwareVersionsResponse : BaseResponse
    {
        [JsonProperty("version_data")]
        public Dictionary<string, Dictionary<string, SoftwareStream>> VersionData { get; set; }
    }
}