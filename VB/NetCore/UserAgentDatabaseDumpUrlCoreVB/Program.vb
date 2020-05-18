Imports System.Net
Imports Newtonsoft.Json
Imports RestSharp
Imports WhatIsMyBrowser.CommonTypesCoreVB

Module Program

    Sub Main()
        ' Your API Key
        ' You can get your API Key by following these instructions:
        ' https'developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
        Const API_KEY = ""

        Dim fileFormat = "mysql"
        'Dim fileFormat = "csv"
        'Dim fileFormat = "txt"

        ' Where will the request be sent to
        ' If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
        ' instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch And 
        ' RestSharp library will silently return empty response.
        Dim apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_database_dump_url?file_format=" + fileFormat

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

        ' -- Make the request
        Dim result = client.Execute(request)

        ' -- Try to decode the api response as json
        Dim response As DatabaseDumpResponse
        Try
            response = JsonConvert.DeserializeObject(Of DatabaseDumpResponse)(result.Content)
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

        Dim userAgentDatabaseDump = response.UserAgentDatabaseDump

        Console.WriteLine("You requested the {0} data format.", fileFormat)
        Console.WriteLine("The latest data file contains {0:n0} user agents", userAgentDatabaseDump.NumberOfUserAgents)
        Console.WriteLine("You can download it from: {0}", userAgentDatabaseDump.Url)
    End Sub

End Module
