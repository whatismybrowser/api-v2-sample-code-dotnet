using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using WhatIsMyBrowser.CommonTypesCore;

namespace UserAgentParseCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Sample code for the WhatIsMyBrowser.com API - Version 2
            //
            // User Agent Parse
            // This sample code provides a very straightforward example of
            // sending an authenticated API request to parse a user agent
            // and display some basic results to the console.
            // 
            // It should be used as an example only, to help you get started
            // using the API. This code is in the public domain, feel free to
            // take it an integrate it with your system as you require.
            // Refer to the "LICENSE" file in this repository for legal information.
            //
            // For further documentation, please refer to the Integration Guide:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/
            // 
            // For support, please refer to our Support section:
            // https://developers.whatismybrowser.com/api/support/

            // Your API Key
            // You can get your API Key by following these instructions:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
            const string API_KEY = "";

            // An example user agent to parse:
            var userAgentToParse = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";

            // Where will the request be sent to
            // If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
            // instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch and 
            // RestSharp library will silently return empty response.
            string apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_parse";

            // -- Set up HTTP Headers
            var headers = new Dictionary<string, string>()
            {
                {"X-API-KEY", API_KEY}
            };

            // -- prepare data for the API request
            // This shows the `parse_options` key with some options you can choose to enable if you want
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-parse-options
            var postData = new UserAgentParseDataRequest
            {
                UserAgent = userAgentToParse,
                ParseOptions =
                {
                    // AllowServersToImpersonateDevices = true,
                    // ReturnMetadataForUserAgent = true,
                    // DoNotSanitize = true
                }
            };

            // build rest client
            var client = new RestClient(apiUrl);
            client.UseNewtonsoftJson();

            // build request
            var request = new RestRequest(Method.POST);

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            request.AddJsonBody(postData);

            // -- Make the request
            var result = client.Execute(request);

            // -- Try to decode the api response as json
            UserAgentParseDataResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<UserAgentParseDataResponse>(result.Content);
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

            // -- Copy the data to some variables for easier use
            var parse = response.Parse;
            var versionCheck = response.VersionCheck;

            // Now you can do whatever you need to do with the parse result
            // Print it to the console, store it in a database, etc
            // For example - printing to the console:

            if (!string.IsNullOrWhiteSpace(parse.SimpleSoftwareString))
                Console.WriteLine(parse.SimpleSoftwareString);
            else
                Console.WriteLine("Couldn't figure out what software they're using");

            if (!string.IsNullOrWhiteSpace(parse.SimpleSubDescriptionString))
                Console.Write(parse.SimpleSubDescriptionString);

            if (!string.IsNullOrWhiteSpace(parse.SimpleOperatingPlatformString))
                Console.WriteLine(parse.SimpleOperatingPlatformString);

            if (versionCheck != null)
            {
                // Your API account has access to version checking information

                if (versionCheck.IsCheckable)
                {
                    // This software will have information about whether it's up to date or not
                    if (versionCheck.IsUpToDate)
                        Console.WriteLine("{0} is up to date", parse.SoftwareName);
                    else
                    {
                        Console.WriteLine("{0} is out of date", parse.SoftwareName);

                        if (versionCheck.LatestVersion != null && versionCheck.LatestVersion.Length > 0)
                            Console.WriteLine("The latest version is {0}", string.Join(".", versionCheck.LatestVersion));

                        if (versionCheck.UpdateUrl != null)
                            Console.WriteLine("You can update here: {0}", versionCheck.UpdateUrl);
                    }
                }
            }

            // Refer to:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-field-definitions
            // for descriptions of all the fields.
        }
    }
}
