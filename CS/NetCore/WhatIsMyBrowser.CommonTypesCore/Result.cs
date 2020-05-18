using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class Result
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message_code")]
        public string MessageCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}