Imports Newtonsoft.Json

Public Class SearchMetaData
    <JsonProperty("num_of_results_returned")>
    Public Property NumOfResultsReturned As Long

    <JsonProperty("search_took_milliseconds")>
    Public Property SearchTookMilliseconds As Long

    <JsonProperty("search_parameters")>
    Public Property SearchParameters As Dictionary(Of String, Object)
End Class