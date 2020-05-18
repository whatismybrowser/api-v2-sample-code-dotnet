Imports Newtonsoft.Json

Public Class UserAgentParseDataRequestParseOptions
    <JsonProperty("allow_servers_to_impersonate_devices", NullValueHandling:=NullValueHandling.Ignore)>
    Public Property AllowServersToImpersonateDevices As Nullable(Of Boolean)

    <JsonProperty("return_metadata_for_useragent", NullValueHandling:=NullValueHandling.Ignore)>
    Public Property ReturnMetadataForUserAgent As Nullable(Of Boolean)

    <JsonProperty("dont_sanitize", NullValueHandling:=NullValueHandling.Ignore)>
    Public Property DoNotSanitize As Nullable(Of Boolean)
End Class