Imports Newtonsoft.Json

Public Class UserAgentParseDataBatchResponse
    Inherits BaseResponse
    <JsonProperty("parses")>
    Public Property Parses As Dictionary(Of String, UserAgentParseDataBatchResponseItem)

    <JsonProperty("parse_stats")>
    Public Property ParseStats As UserAgentParseDataBatchResponseStats
End Class

Public Class UserAgentParseDataBatchResponseStats
    <JsonProperty("success")>
    Public Property SuccessCount As Integer

    <JsonProperty("error")>
    Public Property ErrorCount As Integer

    <JsonProperty("total")>
    Public Property TotalCount As Integer
End Class

Public Class UserAgentParseDataBatchResponseItem
    Inherits BaseResponse
    <JsonProperty("parse")>
    Public Property Parse As UserAgentParseResponseParse

    <JsonProperty("version_check")>
    Public Property VersionCheck As VersionCheck
End Class
