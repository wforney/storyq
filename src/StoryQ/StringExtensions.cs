using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StoryQ
{
    internal static class StringExtensions
    {
        public static string Camel(this string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }

        public static string UnCamel(this string name)
        {
            return Regex.Replace(name, "[A-Z_]", x => " " + x.Value.ToLowerInvariant()).Trim();
        }

        public static string Join(this IEnumerable<string> strings, string seperator)
        {
            return string.Join(seperator, strings.ToArray());
        }

    }
}