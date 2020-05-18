Imports Newtonsoft.Json

Public Class UserAgentType
    <JsonProperty("user_agent")>
    Public Property UserAgent As String

    <JsonProperty("user_agent_meta_data")>
    Public Property UserAgentMetaData As UserAgentMetaData

    <JsonProperty("parse")>
    Public Property Parse As UserAgentSearchResponseParse
End Class