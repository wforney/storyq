namespace StoryQ.Formatting.Parameters
{
    using StoryQ.Infrastructure;

    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Formats a parameter by calling "toString" on it (nulls are formatter as {NULL}, for visibility)
    /// </summary>
    public class ToStringParameterFormatAttribute : ParameterFormatAttribute
    {
        /// <summary>
        /// Formats the parameter using its toString
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <returns>A custom description of that parameter's value</returns>
        public override string Format(object value)
        {
            if (value is IEnumerable enumerable && !(value is string))
            {
                return $"[{enumerable.Cast<object>().Select(x => this.Format(x)).Join(", ")}]";
            }

            if (value is null)
            {
                return "{NULL}";
            }

            if (value is string s)
            {
                if (s.Length == 0)
                {
                    return "\"\"";
                }

                if (s.Trim().Length == 0)
                {
                    return "\" \"";
                }
            }

            return value.ToString();
        }
    }
}
