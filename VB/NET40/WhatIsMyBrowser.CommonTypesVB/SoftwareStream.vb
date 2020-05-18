Imports Newtonsoft.Json

Public Class SoftwareStream
    <JsonProperty("latest_version")>
    Public Property LatestVersion As Integer()

    <JsonProperty("download_url")>
    Public Property DownloadUrl As Uri

    <JsonProperty("update")>
    Public Property Update As String

    <JsonProperty("update_url")>
    Public Property UpdateUrl As Uri

    <JsonProperty("release_date")>
    Public Property ReleaseDate As String

    <JsonProperty("package_type")>
    Public Property PackageType As String

    <JsonProperty("sample_user_agents")>
    Public Property SampleUserAgents As Dictionary(Of String, String())
End Class
