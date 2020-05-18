Imports Newtonsoft.Json

Public Class SoftwareVersionsResponse
    Inherits BaseResponse

    <JsonProperty("version_data")>
    Public Property VersionData As Dictionary(Of String, Dictionary(Of String, SoftwareStream))

End Class