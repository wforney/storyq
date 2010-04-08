using System.Collections;
using System.Linq;
using System.Text;

namespace StoryQ.Formatting.Parameters
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
            if (value is IEnumerable && !(value is string))
			{
			    var enumerable = (IEnumerable)value;
			    return string.Format("[{0}]", enumerable.Cast<object>().Select(x => Format(x)).Join(", "));
			}

            if (value == null)
            {
                return "{NULL}";
            }

            if(value is string)
            {
                var s = (string) value;
                if(s.Length == 0)
                {
                    return "\"\"";
                }
                if(s.Trim().Length == 0)
                {
                    return "\" \"";
                }
            }
            
            return value.ToString();
        }
    }
}