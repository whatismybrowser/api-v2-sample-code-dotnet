Imports System.Net
Imports Newtonsoft.Json
Imports RestSharp
Imports WhatIsMyBrowser.CommonTypesVB

Module Program
    Sub Main()
        ' Your API Key
        ' You can get your API Key by following these instructions:
        ' https'developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
        Const API_KEY = ""

        ' The various search parameters
        ' This Is a basic search for Safari user agents... but it includes
        ' other sample parameters which have been commented out. Change the
        ' parameters which get sent to fetch the results you need.
        ' 
        ' You can also use the Web Based form to experiment And see which
        ' parameter values are valid:
        ' https//developers.whatismybrowser.com/api/docs/v2/sample-code/database-search

        Dim parameters As New Dictionary(Of String, Object)
        parameters("software_name") = "safari" ' "Internet Explorer" "Chrome" "Firefox"
        'parameters("software_version") = "71"
        'parameters("software_version_min") = 64
        'parameters("software_version_max") = 79

        'parameters("operating_system_name") = "macOS" '"OS X", "macOS", "Linux", "Android"
        'parameters("operating_system_version") = "Snow Leopard" ' Vista, 8.2

        'parameters("operating_platform") = "iPhone" '"iPad", "iPhone 5", "Galaxy Gio", "Galaxy Note", "Galaxy S4"
        'parameters("operating_platform_code") = "GT-S5660"

        'parameters("software_type") = "browser" ' "bot" "application"
        'parameters("software_type_specific") = "web-browser" ' "analyser" "application" "bot" "crawler" etc

        'parameters("hardware_type") = "computer" ' "computer" "mobile" "server"
        'parameters("hardware_type_specific") = "computer" ' "phone", "tablet", "mobile", "ebook-reader", "game-console" etc

        'parameters("layout_engine_name") = "NetFront" ' Blink, Trident, EdgeHTML, Gecko, NetFront, Presto

        'parameters("order_by") = "times_seen desc" ' "times_seen asc" "first_seen_at asc" "first_seen_at desc" "last_seen_at desc" "last_seen_at asc" "software_version desc"
        'parameters("times_seen_min") = 100
        'parameters("times_seen_max") = 1000
        'parameters("limit") = 250


        ' Where will the request be sent to
        ' If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
        ' instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch And 
        ' RestSharp library will silently return empty response.
        Dim apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_database_search"


        ' -- Set up HTTP Headers
        Dim headers As New Dictionary(Of String, String)()
        headers.Add("X-API-KEY", API_KEY)

        ' build rest client
        Dim client = New RestClient(apiUrl)

        ' build request
        Dim request = New RestRequest(Method.GET)

        For Each header In headers
            request.AddHeader(header.Key, header.Value)
        Next

        For Each parameter In parameters
            request.AddParameter(parameter.Key, parameter.Value)
        Next


        ' -- Make the request
        Dim result = client.Execute(request)

        ' -- Try to decode the api response as json
        Dim response As UserAgentSearchResponse
        Try
            response = JsonConvert.DeserializeObject(Of UserAgentSearchResponse)(result.Content)
        Catch e As Exception

            Console.WriteLine(result.Content)
            Console.WriteLine("Couldn't decode the response as JSON: {0}", e)
            Return
        End Try

        ' -- Check that the server responded with a "200/Success" code
        If (result.StatusCode <> HttpStatusCode.OK) Then
            Console.WriteLine("ERROR: not a 200 result. instead got: {0} {1}", CInt(result.StatusCode),
                              result.StatusCode)
            Console.WriteLine(JsonConvert.SerializeObject(response, Xml.Formatting.Indented))
            Return
        End If

        ' -- Check the API request was successful
        If (response.Result.Code <> "success") Then
            Console.WriteLine(
                "The API did not return a 'success' response. It said: result code: {0}, message_code: {1}, message: {2}",
                response.Result.Code, response.Result.MessageCode, response.Result.MessageCode
                )
            'Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
            Return
        End If

        ' Now you have "result_json" And can store, display Or process any part of the response.

        ' -- Print the entire json dump for reference
        Console.WriteLine(JsonConvert.SerializeObject(response, Xml.Formatting.Indented))

        ' -- Display the user agent And times seen for each parse result in the list
        ' Don't forget that all the parse data is included in each user agent record as well.
        For Each userAgentRecord In response.SearchResults.UserAgents
            Console.WriteLine("{0} - seen: {1:n0} times", userAgentRecord.UserAgent, userAgentRecord.UserAgentMetaData.TimesSeen)
        Next

    End Sub
End Module
