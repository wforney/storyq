using System.Collections;
using System.Linq;
using System.Text;

﻿namespace StoryQ.Formatting.Parameters
{
    /// <summary>
    /// Formats a parameter by calling "toString" on it (nulls are formatter as {NULL}, for visibility)
    /// </summary>
    public class ToStringParameterFormatAttribute : ParameterFormatAttribute
    {
        /// <summary>
        /// Formats the parameter using its toString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string Format(object value)
        {
			var enumerable = value as IEnumerable;
			
			if(enumerable != null)
			{
			    var items = enumerable.Cast<object>().Select(x=>Format(x)).ToArray();
			    return string.Format("[{0}]", string.Join(", ", items));
			}

            if (value == null)
            {
                return "{NULL}";
            }
            
            return value.ToString();
        }
    }
}