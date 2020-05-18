Imports Newtonsoft.Json

Public Class UserAgentParseDataResponse
    Inherits BaseResponse

    <JsonProperty("parse")>
    Public Property Parse As UserAgentParseResponseParse

    <JsonProperty("sanitization")>
    Public Property Sanitization As Sanitization

    <JsonProperty("version_check")>
    Public Property VersionCheck As VersionCheck
End Class