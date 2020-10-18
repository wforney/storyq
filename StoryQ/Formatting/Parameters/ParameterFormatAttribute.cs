namespace StoryQ.Formatting.Parameters
{
    using System;

    /// <summary>
    /// Allows you to override the formatting of a parameter to a StoryQ method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParameterFormatAttribute : Attribute
    {
        /// <summary>
        /// Override this method to provide a custom format for any given parameter value.
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <returns>A custom description of that parameter's value</returns>
        public abstract string Format(object value);
    }
}
