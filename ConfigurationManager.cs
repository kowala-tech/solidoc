using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Solidoc
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "config.json");
            string contents = File.ReadAllText(path, new UTF8Encoding(false));

            Data = JsonConvert.DeserializeObject<IDictionary<string, string>>(contents);
        }

        private static IDictionary<string, string> Data { get; }

        public static string Get(string key)
        {
            return Data[key];
        }
    }
}