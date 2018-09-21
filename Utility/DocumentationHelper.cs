using System;

namespace Solidoc.Utility
{
    public static class DocumentationHelper
    {
        public static string Get(string contents, string key)
        {
            contents = contents ?? "";

            var split = contents.Split("@", StringSplitOptions.RemoveEmptyEntries);

            foreach (string entry in split)
            {
                if (entry.StartsWith(key))
                {
                    return entry.Substring(key.Length, entry.Length - key.Length).Trim();
                }
            }

            return string.Empty;
        }

        public static string GetNotice(string contents)
        {
            string title = Get(contents, "notice");

            return string.IsNullOrWhiteSpace(title) ? Get(contents, "dev") : title;
        }
    }
}