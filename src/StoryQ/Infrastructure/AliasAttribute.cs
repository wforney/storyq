// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="AliasAttribute.cs" company="">
//     2010 robfe & toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Infrastructure
{
    using System;

    /// <summary>
    /// When a StoryQ step method can be named different things, use aliases to ensure the converter can still parse the plaintext
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AliasAttribute"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        public AliasAttribute(string alias)
        {
            this.Alias = alias;
        }

        /// <summary>
        /// Gets the alias for this attribute
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; private set; }
    }

}