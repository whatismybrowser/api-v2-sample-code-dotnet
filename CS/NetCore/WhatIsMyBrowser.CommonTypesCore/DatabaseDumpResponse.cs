using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class DatabaseDumpResponse : BaseResponse
    {
        [JsonProperty("user_agent_database_dump")]
        public UserAgentDatabaseDump UserAgentDatabaseDump { get; set; }
    }
}