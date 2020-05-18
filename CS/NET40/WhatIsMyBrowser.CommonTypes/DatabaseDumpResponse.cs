using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypes
{
    public class DatabaseDumpResponse : BaseResponse
    {
        [JsonProperty("user_agent_database_dump")]
        public UserAgentDatabaseDump UserAgentDatabaseDump { get; set; }
    }
}