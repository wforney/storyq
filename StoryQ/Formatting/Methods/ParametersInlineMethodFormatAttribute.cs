namespace StoryQ.Formatting.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Describes a method by un camel-casing the method, then replacing each underscore in the
    /// method name with each parameter value, in sequential order.
    /// </summary>
    public class ParametersInlineMethodFormatAttribute : MethodFormatAttribute
    {
        /// <summary>
        /// Formats a method by putting parameters inline
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentException"></exception>
        public override string Format(MethodInfo method, IEnumerable<string> parameters)
        {
            _ = method ?? throw new ArgumentNullException(nameof(method));

            var argStrings = new Queue<string>(parameters);
            var s = UnCamel(method.Name);

            var underscoreCount = s.Count(x => x == '_');
            if (underscoreCount != argStrings.Count)
            {
                var message = 
                    $"If you use {underscoreCount} underscores in your method name, make sure there's {underscoreCount} arguments (found {argStrings.Count})";

                throw new ArgumentException(message);
            }

            return Regex.Replace(s, "_", x => argStrings.Dequeue());
        }
    }
}
