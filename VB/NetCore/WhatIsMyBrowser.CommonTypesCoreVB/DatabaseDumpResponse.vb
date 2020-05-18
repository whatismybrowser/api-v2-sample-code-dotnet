Imports Newtonsoft.Json

Public Class DatabaseDumpResponse
    Inherits BaseResponse

    <JsonProperty("user_agent_database_dump")>
    Public Property UserAgentDatabaseDump As UserAgentDatabaseDump

End Class
