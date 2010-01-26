using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StoryQ.Formatting.Methods
{
    /// <summary>
    /// Describes a method by un camel-casing the method, then replacing each 
    /// underscore in the method name with each parameter value, in sequential order.
    /// </summary>
    public class ParametersInlineMethodFormatAttribute : MethodFormatAttribute
    {
        /// <summary>
        /// Formats a method by putting parameters inline
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns></returns>
        public override string Format(MethodInfo method, IEnumerable<string> parameters)
        {
            Queue<string> argStrings = new Queue<string>(parameters);
            string s = UnCamel(method.Name);

            int underscoreCount = s.Count(x => x == '_');
            if (underscoreCount != argStrings.Count)
            {
                string message = String.Format(
                    "If you use {0} underscores in your method name, make sure there's {0} arguments (found {1})",
                    underscoreCount, argStrings.Count);
                throw new ArgumentException(message);
            }

            return Regex.Replace(s, "_", x => argStrings.Dequeue());
        }
    }
}