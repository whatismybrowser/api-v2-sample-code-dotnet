Imports Newtonsoft.Json

Public Class VersionCheck
    <JsonProperty("is_checkable")>
    Public Property IsCheckable As Boolean

    <JsonProperty("is_up_to_date")>
    Public Property IsUpToDate As Boolean

    <JsonProperty("latest_version")>
    Public Property LatestVersion As String()

    <JsonProperty("download_url")>
    Public Property DownloadUrl As Uri

    <JsonProperty("update_url")>
    Public Property UpdateUrl As Uri

    <JsonProperty("release_date")>
    Public Property ReleaseDate As DateTimeOffset
End Class