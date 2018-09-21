using System.IO;
using System.Reflection;
using System.Text;

namespace Solidoc.Utility
{
    public static class TemplateHelper
    {
        public static string Contract => Get("contract.md");
        public static string Function => Get("function.md");
        public static string Modifier => Get("modifier.md");
        public static string TableHeader => Get("table-header.md");

        public static string Get(string file)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Templates", file);

            string contents = File.ReadAllText(path, new UTF8Encoding(false));
            return contents;
        }
    }
}