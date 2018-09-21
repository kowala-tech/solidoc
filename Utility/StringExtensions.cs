using System.Text;

namespace Solidoc.Utility
{
    public static class StringExtensions
    {
        public static string Copy(this string s, int n)
        {
            return new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
        }
    }
}