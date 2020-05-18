Imports Newtonsoft.Json

Public Class UserAgentParseDataRequest
    <JsonProperty("user_agent")>
    Public Property UserAgent As String

    <JsonProperty("parse_options")>
    Public Property ParseOptions As UserAgentParseDataRequestParseOptions
End Class