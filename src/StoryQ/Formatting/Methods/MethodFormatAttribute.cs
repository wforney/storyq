using System;
using System.Collections.Generic;
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
            return camelText.UnCamel().Replace(" i ", " I ").Trim();
        }
    }
}