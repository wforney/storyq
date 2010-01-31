using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryQ.Formatting.Parameters
{
    ///<summary>
    /// Use this attribute on a Parameter when you want StoryQ to ignore it.
    ///</summary>
    public class SilentAttribute : ParameterFormatAttribute
    {
        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <returns>An empty string</returns>
        public override string Format(object value)
        {
            return string.Empty;
        }
    }
}
