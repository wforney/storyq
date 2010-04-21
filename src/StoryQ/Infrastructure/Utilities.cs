using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StoryQ.Infrastructure
{
    /// <summary>
    /// Utility methods for StoryQ's use
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Camel-cases the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Camel case => CamelCase</returns>
        public static string Camel(this string input)
        {
            return Regex.Replace(" " + input, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }

        /// <summary>
        /// UnCamel-cases the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>CamelCase => camel case</returns>
        public static string UnCamel(this string input)
        {
            return Regex.Replace(input, "[A-Z_]", x => " " + x.Value.ToLowerInvariant()).Trim();
        }

        /// <summary>
        /// Joins the specified strings.
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>a single, seperator-delimited string</returns>
        public static string Join(this IEnumerable<string> strings, string seperator)
        {
            return string.Join(seperator, strings.ToArray());
        }

    }
}