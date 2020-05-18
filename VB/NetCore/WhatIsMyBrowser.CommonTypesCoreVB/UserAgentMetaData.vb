Imports Newtonsoft.Json

Public Class UserAgentMetaData
    <JsonProperty("id")>
    Public Property Id As Long

    <JsonProperty("times_seen")>
    Public Property TimesSeen As Long

    <JsonProperty("first_seen_at")>
    Public Property FirstSeenAt As DateTimeOffset

    <JsonProperty("last_seen_at")>
    Public Property LastSeenAt As DateTimeOffset
End Class