using System.Collections;
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
				var sb = new StringBuilder("[]");
				var enumerator = enumerable.GetEnumerator();
				while(enumerator.MoveNext()) sb.Insert(sb.Length - 1, string.Format("{0}, ", enumerator.Current));
				if(sb.Length > 3) sb.Remove(sb.Length - 3, 2);
				return sb.ToString();
			}
			
            return value == null ? "{NULL}" : value.ToString();
        }
    }
}