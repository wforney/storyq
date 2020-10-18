namespace StoryQ.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Utility methods for StoryQ's use
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Camel-cases the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Camel case =&gt; CamelCase</returns>
        public static string Camel(this string input) =>
            Regex.Replace(" " + input, " \\w|_", match => match.Value.Trim().ToUpperInvariant());


        /// <summary>
        /// UnCamel-cases the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>CamelCase =&gt; camel case</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Globalization", "CA1308:Normalize strings to uppercase", Justification = "This rule doesn't apply here.")]
        public static string UnCamel(this string input) =>
            Regex.Replace(input, @"[\p{Lu}_]", x => " " + x.Value.ToLowerInvariant()).Trim();

        /// <summary>
        /// Joins the specified strings.
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>a single, seperator-delimited string</returns>
        public static string Join(this IEnumerable<string> strings, string seperator) =>
            string.Join(seperator, strings.ToArray());

        /// <summary>
        /// Finds and returns an attribute on a MemberInfo, Type or Assembly
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="attributeProvider">The attribute provider.</param>
        /// <returns>T.</returns>
        public static T? GetCustomAttribute<T>(this ICustomAttributeProvider attributeProvider)
            where T : Attribute
            => attributeProvider?.GetCustomAttributes(true).OfType<T>().FirstOrDefault();
    }
}
