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

        ' Where will the request be sent to
        ' If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
        ' instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch And 
        ' RestSharp library will silently return empty response.
        Dim apiUrl = "https://api.whatismybrowser.com/api/v2/software_version_numbers/all"

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
        Dim response As SoftwareVersionsResponse
        Try
            response = JsonConvert.DeserializeObject(Of SoftwareVersionsResponse)(result.Content)
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

        Dim versionData = response.VersionData

        ' -- Loop over all the different software version data elements
        For Each software In versionData

            Console.WriteLine("Version data for {0}", software.Key)

            Dim softwareVersionData = software.Value

            For Each streamCode In softwareVersionData

                Dim softwareStream = streamCode.Value

                'Console.WriteLine(JsonConvert.SerializeObject(softwareStream, Formatting.Indented));

                Console.WriteLine("  Stream: {0}", streamCode.Key)

                Console.WriteLine(Chr(9) + "The latest version number for {0} [{1}] is {2}",
                                  software.Key, streamCode.Key, String.Join(".", softwareStream.LatestVersion)
                                  )

                If (Not String.IsNullOrWhiteSpace(softwareStream.Update)) Then
                    Console.WriteLine(Chr(9) + "Update no: {0}", softwareStream.Update)
                End If

                If (Not softwareStream.UpdateUrl Is Nothing) Then
                    Console.WriteLine(Chr(9) + "Update URL: {0}", softwareStream.UpdateUrl)
                End If

                If (Not softwareStream.DownloadUrl Is Nothing) Then
                    Console.WriteLine(Chr(9) + "Download URL: {0}", softwareStream.DownloadUrl)
                End If

                If (Not String.IsNullOrWhiteSpace(softwareStream.ReleaseDate)) Then
                    Console.WriteLine(Chr(9) + "Release Date: {0}", softwareStream.ReleaseDate)
                End If

                Console.WriteLine("  Some sample user agents with the latest version numbers:")

                ' if there are sample user agents (eg. Flash And Java can't have sample user agents..), display them
                If (Not softwareStream.SampleUserAgents Is Nothing) Then

                    For Each sampleUserAgentGroup In softwareStream.SampleUserAgents
                        Console.WriteLine(Chr(9) + "User agents for {0} on {1} [{2}]", sampleUserAgentGroup.Key, software.Key, streamCode.Key)

                        For Each sampleUserAgent In sampleUserAgentGroup.Value
                            Console.WriteLine(Chr(9) + Chr(9) + "{0}", sampleUserAgent)
                        Next
                    Next

                End If
            Next
        Next

        Console.WriteLine("---------------------------------")
    End Sub
End Module
