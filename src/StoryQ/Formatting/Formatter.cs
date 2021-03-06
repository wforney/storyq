﻿// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="Formatter.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Formatting
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using StoryQ.Formatting.Methods;
    using StoryQ.Formatting.Parameters;
    using StoryQ.Infrastructure;

    /// <summary>
    /// A StoryQ infrastructure class that can format a given StoryQ Test method into a human-friendly (even if the human
    /// in question isn't a developer!) string
    /// </summary>
    public static class Formatter
    {
        /// <summary>
        /// Formats a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>a string representing the method's description</returns>
        public static string FormatMethod(Delegate method, params object[] arguments)
        {
            var parameterInfos = method.Method.GetParameters();
            Debug.Assert(parameterInfos.Length == arguments.Length, "Wrong number of parameters supplied to FormatMethod");

            var argsAsStrings = from i in Enumerable.Range(0, arguments.Length)
                                let p = parameterInfos[i]
                                let a = arguments[i]
                                where !p.IsDefined(typeof(SilentAttribute), true)
                                select FormatParameter(parameterInfos[i], a);

            var formatter = GetFormatter(method);
            return formatter.Format(method.Method, argsAsStrings);
        }

        /// <summary>
        /// Gets the formatter.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>MethodFormatAttribute.</returns>
        private static MethodFormatAttribute GetFormatter(Delegate method) => method.Method.GetCustomAttribute<MethodFormatAttribute>() ?? StoryQSettings.DefaultMethodFormatSelector(method.Method);

        /// <summary>
        /// Formats the parameter.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private static string FormatParameter(ParameterInfo info, object value)
        {
            var a = info.GetCustomAttribute<ParameterFormatAttribute>() ?? StoryQSettings.DefaultParameterFormatter(info);

            return a.Format(value);
        }
    }
}
