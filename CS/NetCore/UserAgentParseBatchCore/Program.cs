using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using WhatIsMyBrowser.CommonTypesCore;

namespace UserAgentParseBatchCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Your API Key
            // You can get your API Key by following these instructions:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
            const string API_KEY = "";

            // Some sample user agents to send in a batch
            var userAgentsToParse = new Dictionary<string, string>
            {
                {"1", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.155 Safari/537.36"},
                {"2", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36"},
                {"3", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_4) AppleWebKit/600.7.12 (KHTML, like Gecko) Version/8.0.7 Safari/600.7.12"},
                {"4", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36"},
                {"5", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.5 Mobile/15E148 Safari/604.1"},
                {"6", "Mozilla/5.0 (PlayStation 4 5.55) AppleWebKit/601.2 (KHTML, like Gecko)"},
                {"7", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"},
                {"8", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"},
            };


            // Where will the request be sent to
            // If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
            // instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch and 
            // RestSharp library will silently return empty response.
            string apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_parse_batch";

            // -- Set up HTTP Headers
            var headers = new Dictionary<string, string>()
            {
                {"X-API-KEY", API_KEY}
            };


            // -- prepare data for the API request
            // This shows the `parse_options` key with some options you can choose to enable if you want
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#user-agent-parse-parse-options
            var postData = new UserAgentParseDataBatchRequest
            {
                UserAgents = userAgentsToParse,
                ParseOptions =
                {
                    // AllowServersToImpersonateDevices = true,
                    // ReturnMetadataForUserAgent = true,
                    // DoNotSanitize = true
                }
            };

            if (userAgentsToParse.Count > 500)
            {
                Console.WriteLine("You are attempting to send more than the maximum number of user agents in one batch");
                return;
            }

            Console.WriteLine("Processing {0:n0} user agents in one batch. Please be patient.", userAgentsToParse.Count);


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
            UserAgentParseDataBatchResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<UserAgentParseDataBatchResponse>(result.Content);
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

            // -- Display some basic info about each parse result in the list
            foreach (var parseRecord in response.Parses)
            {
                // -- get the whole result from the batch
                // this includes the `parse` dict, as well as `result`

                if (parseRecord.Value == null || parseRecord.Value.Result == null || parseRecord.Value.Result.Code != "success")
                {
                    Console.WriteLine("There was a problem parsing the user agent with the id {0}", parseRecord.Key);

                    if (parseRecord.Value != null && parseRecord.Value.Result != null)
                        Console.WriteLine(parseRecord.Value.Result.Message);

                    continue;  // to the next record in the batch
                }

                // -- Print the individual parse result for this record
                Console.WriteLine(JsonConvert.SerializeObject(parseRecord.Value, Formatting.Indented));

                // -- Now copy the actual parse result to a different variable for easier use
                var parse = parseRecord.Value.Parse;

                // You can now access the parse results in the `parse` dict and use them however you would like.
                // For example:

                Console.WriteLine("{0}: [{1}/{2}] {3}", parseRecord.Key, parse.HardwareType, parse.SoftwareType, parse.SimpleSoftwareString);
            }
        }
    }
}
