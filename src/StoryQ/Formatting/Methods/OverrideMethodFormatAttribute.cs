// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="OverrideMethodFormatAttribute.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Formatting.Methods
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Lets you specify how you want a method formatted with a literal string
    /// </summary>
    /// <seealso cref="StoryQ.Formatting.Methods.MethodFormatAttribute" />
    public class OverrideMethodFormatAttribute : MethodFormatAttribute
    {
        /// <summary>
        /// The text
        /// </summary>
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverrideMethodFormatAttribute" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public OverrideMethodFormatAttribute(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// Override this method to provide a human friendly description of a method.
        /// </summary>
        /// <param name="method">The method to describe</param>
        /// <param name="parameters">the parameters currently being passed to the method</param>
        /// <returns>the overidden text</returns>
        public override string Format(MethodInfo method, IEnumerable<string> parameters) => this.text;
    }
}