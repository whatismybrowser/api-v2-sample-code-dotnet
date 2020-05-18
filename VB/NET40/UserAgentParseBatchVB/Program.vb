Imports System.Net
Imports Newtonsoft.Json
Imports RestSharp
Imports RestSharp.Serializers.NewtonsoftJson
Imports WhatIsMyBrowser.CommonTypesVB

Module Program

    Sub Main()

        ' Your API Key
        ' You can get your API Key by following these instructions:
        ' https'developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
        Const API_KEY = ""

        ' Some sample user agents to send in a batch
        Dim userAgentsToParse = New Dictionary(Of String, String)
        userAgentsToParse("1") = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.155 Safari/537.36"
        userAgentsToParse("2") = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36"
        userAgentsToParse("3") = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_4) AppleWebKit/600.7.12 (KHTML, like Gecko) Version/8.0.7 Safari/600.7.12"
        userAgentsToParse("4") = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36"
        userAgentsToParse("5") = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.5 Mobile/15E148 Safari/604.1"
        userAgentsToParse("6") = "Mozilla/5.0 (PlayStation 4 5.55) AppleWebKit/601.2 (KHTML, like Gecko)"
        userAgentsToParse("7") = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"
        userAgentsToParse("8") = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"

        ' Where will the request be sent to
        ' If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
        ' instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch And 
        ' RestSharp library will silently return empty response.
        Dim apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_parse_batch"

        ' -- Set up HTTP Headers
        Dim headers As New Dictionary(Of String, String)()
        headers.Add("X-API-KEY", API_KEY)

        ' -- prepare data for the API request
        ' This shows the `parse_options` key with some options you can choose to enable if you want
        ' https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-parse-options
        Dim postData As New UserAgentParseDataBatchRequest
        postData.UserAgents = userAgentsToParse
        postData.ParseOptions = New UserAgentParseDataRequestParseOptions
        'postData.ParseOptions.AllowServersToImpersonateDevices = True
        'postData.ParseOptions.ReturnMetadataForUserAgent = True
        'postData.ParseOptions.DoNotSanitize = True

        If (userAgentsToParse.Count > 500) Then
            Console.WriteLine("You are attempting to send more than the maximum number of user agents in one batch")
            Return
        End If

        Console.WriteLine("Processing {0:n0} user agents in one batch. Please be patient.", userAgentsToParse.Count)

        ' build rest client
        Dim client = New RestClient(apiUrl)
        client.UseNewtonsoftJson()

        ' build request
        Dim request = New RestRequest(Method.POST)

        For Each header In headers
            request.AddHeader(header.Key, header.Value)
        Next

        request.AddJsonBody(postData)

        ' -- Make the request
        Dim result = client.Execute(request)

        ' -- Try to decode the api response as json
        Dim response As UserAgentParseDataBatchResponse
        Try
            response = JsonConvert.DeserializeObject(Of UserAgentParseDataBatchResponse)(result.Content)
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

        ' -- Display some basic info about each parse result in the list
        For Each parseRecord In response.Parses

            ' -- get the whole result from the batch
            ' this includes the `parse` dict, as well as `result`

            If (parseRecord.Value Is Nothing Or parseRecord.Value.Result Is Nothing Or parseRecord.Value.Result.Code <> "success") Then
                Console.WriteLine("There was a problem parsing the user agent with the id {0}", parseRecord.Key)

                If (Not parseRecord.Value Is Nothing And Not parseRecord.Value.Result Is Nothing) Then
                    Console.WriteLine(parseRecord.Value.Result.Message)
                End If

                Continue For
            End If

            ' -- Print the individual parse result for this record
            Console.WriteLine(JsonConvert.SerializeObject(parseRecord.Value, Formatting.Indented))

            ' -- Now copy the actual parse result to a different variable for easier use
            Dim parse = parseRecord.Value.Parse

            ' You can now access the parse results in the `parse` dict And use them however you would Like.
            ' For example

            Console.WriteLine("{0}: [{1}/{2}] {3}", parseRecord.Key, parse.HardwareType, parse.SoftwareType, parse.SimpleSoftwareString)

        Next
    End Sub

End Module
