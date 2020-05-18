# api-v2-sample-code-dotnet

## WhatIsMyBrowser Sample Project .Net Collection

This folder contains several projects to demostrate how to use common Nuget libraries to call WhatIsMyBrowser REST APIs.

Samples are provided in C# and VB.NET for .Net framework 4.7 and .Net Core 3.1, but can be configured easily to work with other versions of the rspective frameworks.

Samples use RestSharp (106.11.3) for REST calls, Newtonsoft.Json (12.0.3) for Json for JSON serialization/deserialization and RestSharp.Serializers.NewtonsoftJson (106.11.3) to set Newtonsoft.Json for default JSON serializer for RestSharp.

For versions earlier than .Net Framework 4.7 may have trouble accessing REST API Https endpoint because of TLS 1.2. In this case you can either access Http endpoint or refer to following Microsoft pages for further information

- https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls
- https://docs.microsoft.com/en-us/mem/configmgr/core/plan-design/security/enable-tls-1-2-client

## Folder Structure

You can find C# samples in CS folder and VB.NET samples in VB folder. Each folder will have two folders, NET40 for .Net Framework and NetCore for .Net Core.

## Notes

- You have to get an API key from the Accounts System test the API.
    - https://accounts.whatismybrowser.com
- A sample library code WhatIsMyBrowser.CommonTypes is also provided to easily build requests or navigate through response data. Sample code is provided for convenience only, changes in the API may break deserialization.
