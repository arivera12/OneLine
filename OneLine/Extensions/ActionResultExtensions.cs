using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OneLine.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult OutputJson(this ContentResult result, object data, JsonSerializerSettings jsonSerializerSettings)
        {
            result.Content = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            result.ContentType = "application/json";
            result.StatusCode = 200;
            return result;
        }
        public static IActionResult OutputJson(this ContentResult result, object data, bool ConverCamelCaseNaming = false)
        {
            if (ConverCamelCaseNaming)
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                result.Content = JsonConvert.SerializeObject(data, 
                    new JsonSerializerSettings() 
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                    ContractResolver = contractResolver, 
                    DefaultValueHandling = DefaultValueHandling.Ignore 
                });
            }
            else
            {
                result.Content = JsonConvert.SerializeObject(data, 
                    new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }
            result.ContentType = "application/json";
            result.StatusCode = 200;
            return result;
        }
    }
}

