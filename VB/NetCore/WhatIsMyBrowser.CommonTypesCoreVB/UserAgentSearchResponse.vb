Imports Newtonsoft.Json

Public Class UserAgentSearchResponse
    Inherits BaseResponse

    <JsonProperty("search_results")>
    Public Property SearchResults As SearchResults
End Class