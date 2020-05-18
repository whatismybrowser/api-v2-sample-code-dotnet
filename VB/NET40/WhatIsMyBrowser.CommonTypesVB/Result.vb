Imports Newtonsoft.Json

Public Class Result
    <JsonProperty("code")>
    Public Property Code As String

    <JsonProperty("message_code")>
    Public Property MessageCode As String

    <JsonProperty("message")>
    Public Property Message As String
End Class