using System;

namespace StoryQ.Formatting.Parameters
{
    /// <summary>
    /// Lets you provide alternative descriptions of "true" and "false" when formatting a StoryQ test method
    /// </summary>
    public class BooleanParameterFormatAttribute : ParameterFormatAttribute
    {
        private readonly string trueValue;
        private readonly string falseValue;

        /// <summary>
        /// Instantiates a new BooleanParameterFormatAttribute
        /// </summary>
        /// <param name="trueValue">the string to use when the parameter is true</param>
        /// <param name="falseValue">the string to use when the parameter is false</param>
        public BooleanParameterFormatAttribute(string trueValue, string falseValue)
        {
            this.trueValue = trueValue;
            this.falseValue = falseValue;
        }

        /// <summary>
        /// Returns the true value if the parameter was true, otherwise the false value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string Format(object value)
        {
            bool boolean = Convert.ToBoolean(value);
            return boolean ? trueValue : falseValue;
        }
    }
}