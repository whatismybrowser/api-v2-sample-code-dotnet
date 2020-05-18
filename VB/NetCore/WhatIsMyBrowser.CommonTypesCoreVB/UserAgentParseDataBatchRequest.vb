Imports Newtonsoft.Json

Public Class UserAgentParseDataBatchRequest
    <JsonProperty("user_agents")>
    Public Property UserAgents As Dictionary(Of String, String)

    <JsonProperty("parse_options")>
    Public Property ParseOptions As UserAgentParseDataRequestParseOptions
End Class
