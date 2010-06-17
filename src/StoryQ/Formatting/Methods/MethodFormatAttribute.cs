using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using StoryQ.Infrastructure;

namespace StoryQ.Formatting.Methods
{
    /// <summary>
    /// A MethodFormatAttribute can be used to provide a custom format of a StoryQ test method.
    /// Existing MethodFormatAttributes include the default formatters 
    /// <see cref="ParametersInlineMethodFormatAttribute"/>, and <see cref="ParameterSuffixedMethodFormatAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class MethodFormatAttribute : Attribute
    {
        static readonly Dictionary<string, string> commonStringReplacements = new Dictionary<string, string>
            {
                {" i ", " I "},
                {" cant ", " can't "},
                {" wont ", " won't "},
                {" shouldnt ", " shouldn't "},
                {" mustnt ", " mustn't "},
            };


        /// <summary>
        /// Override this method to provide a human friendly description of a method.
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns></returns>
        public abstract string Format(MethodInfo method, IEnumerable<string> parameters);

        /// <summary>
        /// Turns "SomeMethodName" into "Some method name". 
        /// </summary>
        /// <param name="camelText"></param>
        /// <returns></returns>
        protected static string UnCamel(string camelText)
        {
            return commonStringReplacements
                .Aggregate(
                    " " + camelText.UnCamel() + " ", 
                    (current, p) => current.Replace(p.Key, p.Value))
                .Trim();
        }
    }
}