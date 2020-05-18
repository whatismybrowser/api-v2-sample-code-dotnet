using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using WhatIsMyBrowser.CommonTypesCore;

namespace SoftwareVersionNumbersCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Your API Key
            // You can get your API Key by following these instructions:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
            const string API_KEY = "";

            // Where will the request be sent to
            // If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
            // instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch and 
            // RestSharp library will silently return empty response.
            var apiUrl = "https://api.whatismybrowser.com/api/v2/software_version_numbers/all";

            // -- Set up HTTP Headers
            var headers = new Dictionary<string, string>()
            {
                {"X-API-KEY", API_KEY}
            };

            // build rest client
            var client = new RestClient(apiUrl);

            // build request
            var request = new RestRequest(Method.GET);

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            // -- Make the request
            var result = client.Execute(request);

            // -- Try to decode the api response as json
            SoftwareVersionsResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<SoftwareVersionsResponse>(result.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(result.Content);
                Console.WriteLine("Couldn't decode the response as JSON: {0}", e);
                return;
            }

            // -- Check that the server responded with a "200/Success" code
            if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("ERROR: not a 200 result. instead got: {0} {1}", (int)result.StatusCode, result.StatusCode);
                Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
                return;
            }

            // -- Check the API request was successful
            if (response.Result.Code != "success")
            {
                Console.WriteLine("The API did not return a 'success' response. It said: result code: {0}, message_code: {1}, message: {2}",
                    response.Result.Code, response.Result.MessageCode, response.Result.MessageCode
                );
                //Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
                return;
            }

            // Now you have "result_json" and can store, display or process any part of the response.

            // -- Print the entire json dump for reference
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            var versionData = response.VersionData;

            // -- Loop over all the different software version data elements
            foreach (var software in versionData)
            {
                Console.WriteLine("Version data for {0}", software.Key);

                var softwareVersionData = software.Value;

                foreach (var streamCode in softwareVersionData)
                {
                    var softwareStream = streamCode.Value;

                    //Console.WriteLine(JsonConvert.SerializeObject(softwareStream, Formatting.Indented));

                    Console.WriteLine("  Stream: {0}", streamCode.Key);

                    Console.WriteLine("\tThe latest version number for {0} [{1}] is {2}",
                        software.Key, streamCode.Key, string.Join(".", softwareStream.LatestVersion)
                    );

                    if (!string.IsNullOrWhiteSpace(softwareStream.Update))
                        Console.WriteLine("\tUpdate no: {0}", softwareStream.Update);

                    if (softwareStream.UpdateUrl != null)
                        Console.WriteLine("\tUpdate URL: {0}", softwareStream.UpdateUrl);

                    if (softwareStream.DownloadUrl != null)
                        Console.WriteLine("\tDownload URL: {0}", softwareStream.DownloadUrl);

                    if (!string.IsNullOrWhiteSpace(softwareStream.ReleaseDate))
                        Console.WriteLine("\tRelease Date: {0}", softwareStream.ReleaseDate);

                    Console.WriteLine("  Some sample user agents with the latest version numbers:");

                    // if there are sample user agents (eg. Flash and Java can't have sample user agents..), display them
                    if (softwareStream.SampleUserAgents != null)
                    {
                        foreach (var sampleUserAgentGroup in softwareStream.SampleUserAgents)
                        {
                            Console.WriteLine("\tUser agents for {0} on {1} [{2}]", sampleUserAgentGroup.Key, software.Key, streamCode.Key);

                            foreach (var sampleUserAgent in sampleUserAgentGroup.Value)
                            {
                                Console.WriteLine("\t\t{0}", sampleUserAgent);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("---------------------------------");
        }
    }
}
