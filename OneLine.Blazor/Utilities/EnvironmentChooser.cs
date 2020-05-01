using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneLine.Blazor.Utilities
{
    public sealed class EnvironmentChooser
    {
        private const string QueryStringKey = "Environment";
        private readonly Dictionary<string, Tuple<string, bool>> HostMapping = new Dictionary<string, Tuple<string, bool>>();
        public string DefaultEnvironment { get; }
        public EnvironmentChooser(string defaultEnvironment)
        {
            if (string.IsNullOrWhiteSpace(defaultEnvironment))
            {
                throw new ArgumentException("message", nameof(defaultEnvironment));
            }
            DefaultEnvironment = defaultEnvironment;
        }
        public EnvironmentChooser Add(string hostName, string env, bool queryCanOverride = false)
        {
            HostMapping.Add(hostName, new Tuple<string, bool>(env, queryCanOverride));
            return this;
        }
        public string GetCurrent(Uri url)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(url.Query);
            bool urlContainsEnvironment = parsedQueryString.AllKeys.Contains(QueryStringKey);
            if (HostMapping.ContainsKey(url.Authority))
            {
                Tuple<string, bool> hostMapping = HostMapping[url.Authority];
                if (hostMapping.Item2 && urlContainsEnvironment)
                {
                    return parsedQueryString.GetValues(QueryStringKey).First();
                }
                return hostMapping.Item1;
            }
            if (urlContainsEnvironment)
            {
                return parsedQueryString.GetValues(QueryStringKey).First();
            }
            return DefaultEnvironment;
        }
    }
}