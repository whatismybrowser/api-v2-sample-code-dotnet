Imports System.Net
Imports Newtonsoft.Json
Imports RestSharp
Imports RestSharp.Serializers.NewtonsoftJson
Imports WhatIsMyBrowser.CommonTypesCoreVB

Module Program

    Sub Main()
        ' Sample code for the WhatIsMyBrowser.com API - Version 2
        '
        ' User Agent Parse
        ' This sample code provides a very straightforward example of
        ' sending an authenticated API request to parse a user agent
        ' And display some basic results to the console.
        ' 
        ' It should be used as an example only, to help you get started
        ' using the API. This code Is in the public domain, feel free to
        ' take it an integrate it with your system as you require.
        ' Refer to the "LICENSE" file in this repository for legal information.
        '
        ' For further documentation, please refer to the Integration Guide:
        ' https//developers.whatismybrowser.com/api/docs/v2/integration-guide/
        ' 
        ' For support, please refer to our Support section:
        ' https//developers.whatismybrowser.com/api/support/

        ' Your API Key
        ' You can get your API Key by following these instructions:
        ' https'developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
        Const API_KEY = ""

        ' An example user agent to parse:
        Dim userAgentToParse = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"

        ' Where will the request be sent to
        ' If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
        ' instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch And 
        ' RestSharp library will silently return empty response.
        Dim apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_parse"


        ' -- Set up HTTP Headers
        Dim headers As New Dictionary(Of String, String)()
        headers.Add("X-API-KEY", API_KEY)

        ' -- prepare data for the API request
        ' This shows the `parse_options` key with some options you can choose to enable if you want
        ' https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-parse-options
        Dim postData As New UserAgentParseDataRequest
        postData.UserAgent = userAgentToParse
        postData.ParseOptions = New UserAgentParseDataRequestParseOptions
        'postData.ParseOptions.AllowServersToImpersonateDevices = True
        'postData.ParseOptions.ReturnMetadataForUserAgent = True
        'postData.ParseOptions.DoNotSanitize = True

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
        Dim response As UserAgentParseDataResponse
        Try
            response = JsonConvert.DeserializeObject(Of UserAgentParseDataResponse)(result.Content)
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

        ' -- Copy the data to some variables for easier use
        Dim parse = response.Parse
        Dim versionCheck = response.VersionCheck

        ' Now you can do whatever you need to do with the parse result
        ' Print it to the console, store it in a database, etc
        ' For example - printing to the console

        If (Not String.IsNullOrWhiteSpace(parse.SimpleSoftwareString)) Then
            Console.WriteLine(parse.SimpleSoftwareString)
        Else
            Console.WriteLine("Couldn't figure out what software they're using")
        End If

        If (Not String.IsNullOrWhiteSpace(parse.SimpleSubDescriptionString)) Then
            Console.Write(parse.SimpleSubDescriptionString)
        End If

        If (Not String.IsNullOrWhiteSpace(parse.SimpleOperatingPlatformString)) Then
            Console.WriteLine(parse.SimpleOperatingPlatformString)
        End If

        If (Not versionCheck Is Nothing) Then

            ' Your API account has access to version checking information

            If (versionCheck.IsCheckable) Then

                If (versionCheck.IsUpToDate) Then
                    Console.WriteLine("{0} is up to date", parse.SoftwareName)
                Else
                    Console.WriteLine("{0} is out of date", parse.SoftwareName)

                    If (Not versionCheck.LatestVersion Is Nothing And versionCheck.LatestVersion.Length > 0) Then
                        Console.WriteLine("The latest version is {0}", String.Join(".", versionCheck.LatestVersion))
                    End If

                    If (Not versionCheck.UpdateUrl Is Nothing) Then
                        Console.WriteLine("You can update here: {0}", versionCheck.UpdateUrl)
                    End If

                End If

            End If

        End If

        ' Refer to
        ' https//developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-field-definitions
        ' for descriptions of all the fields.
    End Sub

End Module
