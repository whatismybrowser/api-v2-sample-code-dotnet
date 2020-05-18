Imports Newtonsoft.Json

Public Class UserAgentDatabaseDump
    <JsonProperty("url")>
    Public Property Url As Uri

    <JsonProperty("num_of_useragents")>
    Public Property NumberOfUserAgents As Long

    <JsonProperty("file_format")>
    Public Property FileFormat As String

    <JsonProperty("sha_sum_256")>
    Public Property ShaSum256 As String

    <JsonProperty("created_at")>
    Public Property CreatedAt As DateTimeOffset
End Class