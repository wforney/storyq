// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="ParameterSuffixedMethodFormatAttribute.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Formatting.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using StoryQ.Infrastructure;

    /// <summary>
    /// Describes a method by un camel-casing the method, then putting the parameters at the end (like a method call)
    /// Used by default when a method's name contains no underscores
    /// </summary>
    /// <seealso cref="StoryQ.Formatting.Methods.MethodFormatAttribute" />
    public class ParameterSuffixedMethodFormatAttribute : MethodFormatAttribute
    {

        /// <summary>
        /// Formats a method by putting the parameters at the end (like a method call)
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns>The formatted string.</returns>
        public override string Format(MethodInfo method, IEnumerable<string> parameters)
        {
            var s = parameters.Join(", ");
            if (!string.IsNullOrEmpty(s))
            {
                s = string.Format("({0})", s);
            }

            return UnCamel(method.Name) + s;
        }
    }
}