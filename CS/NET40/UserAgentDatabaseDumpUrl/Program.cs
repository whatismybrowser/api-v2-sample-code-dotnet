using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using WhatIsMyBrowser.CommonTypes;

namespace UserAgentDatabaseDumpUrl
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Your API Key
            // You can get your API Key by following these instructions:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
            const string API_KEY = "";

            var fileFormat = "mysql";
            //var fileFormat = "csv";
            //var fileFormat = "txt";
            
            // Where will the request be sent to
            // If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
            // instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch and 
            // RestSharp library will silently return empty response.
            string apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_database_dump_url?file_format=" + fileFormat;

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
            DatabaseDumpResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<DatabaseDumpResponse>(result.Content);
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

            var userAgentDatabaseDump = response.UserAgentDatabaseDump;

            Console.WriteLine("You requested the {0} data format.", fileFormat);
            Console.WriteLine("The latest data file contains {0:n0} user agents", userAgentDatabaseDump.NumberOfUserAgents);
            Console.WriteLine("You can download it from: {0}", userAgentDatabaseDump.Url);
        }
    }
}
