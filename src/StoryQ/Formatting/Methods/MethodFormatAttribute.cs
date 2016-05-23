namespace StoryQ.Formatting.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using StoryQ.Infrastructure;

    /// <summary>
    /// A MethodFormatAttribute can be used to provide a custom format of a StoryQ test method.
    /// Existing MethodFormatAttributes include the default formatters
    /// <see cref="ParametersInlineMethodFormatAttribute"/>, and <see cref="ParameterSuffixedMethodFormatAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class MethodFormatAttribute : Attribute
    {
        /// <summary>
        /// The common string replacements
        /// </summary>
        private static readonly Dictionary<string, string> CommonStringReplacements = new Dictionary<string, string>
            {
                {" i ", " I " },
                {" cant ", " can't " },
                {" wont ", " won't " },
                {" shouldnt ", " shouldn't " },
                {" mustnt ", " mustn't " },
            };


        /// <summary>
        /// Override this method to provide a human friendly description of a method.
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns>System.String.</returns>
        public abstract string Format(MethodInfo method, IEnumerable<string> parameters);

        /// <summary>
        /// Turns "SomeMethodName" into "Some method name".
        /// </summary>
        /// <param name="camelText">The camel text.</param>
        /// <returns>System.String.</returns>
        protected static string UnCamel(string camelText) => CommonStringReplacements.Aggregate($" {camelText.UnCamel()} ", (current, p) => current.Replace(p.Key, p.Value)).Trim();
    }
}