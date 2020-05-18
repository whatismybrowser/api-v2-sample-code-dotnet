Imports Newtonsoft.Json

Public Class SearchResults
    <JsonProperty("user_agents")>
    Public Property UserAgents As UserAgentType()

    <JsonProperty("search_meta_data")>
    Public Property SearchMetaData As SearchMetaData
End Class