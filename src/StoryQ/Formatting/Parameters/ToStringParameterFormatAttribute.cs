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
            return value == null ? "{NULL}" : value.ToString();
        }
    }
}