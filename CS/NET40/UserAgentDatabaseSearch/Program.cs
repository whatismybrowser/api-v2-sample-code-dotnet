using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using WhatIsMyBrowser.CommonTypes;

namespace UserAgentDatabaseSearch
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Your API Key
            // You can get your API Key by following these instructions:
            // https://developers.whatismybrowser.com/api/docs/v2/integration-guide/#introduction-api-key
            const string API_KEY = "";

            // The various search parameters
            // This is a basic search for Safari user agents... but it includes
            // other sample parameters which have been commented out. Change the
            // parameters which get sent to fetch the results you need.
            // 
            // You can also use the Web Based form to experiment and see which
            // parameter values are valid:
            // https://developers.whatismybrowser.com/api/docs/v2/sample-code/database-search

            var parameters = new Dictionary<string, object>()
            {
                {"software_name", "Safari"}, // "Internet Explorer" "Chrome" "Firefox"
                //{"software_version", "71"},
                //{"software_version_min", 64},
                //{"software_version_max", 79},
                
                //{"operating_system_name", "macOS"}, //"OS X", "macOS", "Linux", "Android"
                //{"operating_system_version", "Snow Leopard"}, // Vista, 8.2
                
                //{"operating_platform", "iPhone"}, //"iPad", "iPhone 5", "Galaxy Gio", "Galaxy Note", "Galaxy S4"
                //{"operating_platform_code", "GT-S5660"},
                
                //{"software_type", "browser"}, // "bot" "application"
                //{"software_type_specific", "web-browser"}, // "analyser" "application" "bot" "crawler" etc
                
                //{"hardware_type", "computer"}, // "computer" "mobile" "server"
                //{"hardware_type_specific", "computer"}, // "phone", "tablet", "mobile", "ebook-reader", "game-console" etc
                
                //{"layout_engine_name", "NetFront"}, // Blink, Trident, EdgeHTML, Gecko, NetFront, Presto
                
                //{"order_by", "times_seen desc"}, // "times_seen asc" "first_seen_at asc" "first_seen_at desc" "last_seen_at desc" "last_seen_at asc" "software_version desc"
                //{"times_seen_min", 100},
                //{"times_seen_max", 1000},
                //{"limit", 250},
            };

            // Where will the request be sent to
            // If you are targeting a version .NET framework earlier than 4.7, you can use HTTP protocol
            // instead of HTTPS. Using HTTPS protocol will cause a TLS version mismatch and 
            // RestSharp library will silently return empty response.
            string apiUrl = "https://api.whatismybrowser.com/api/v2/user_agent_database_search";


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

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            // -- Make the request
            var result = client.Execute(request);

            // -- Try to decode the api response as json
            UserAgentSearchResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<UserAgentSearchResponse>(result.Content);
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


            // -- Display the user agent and times seen for each parse result in the list
            // Don't forget that all the parse data is included in each user agent record as well.
            foreach (var userAgentRecord in response.SearchResults.UserAgents)
            {
                Console.WriteLine("{0} - seen: {1:n0} times", userAgentRecord.UserAgent, userAgentRecord.UserAgentMetaData.TimesSeen);
            }
        }
    }
}
