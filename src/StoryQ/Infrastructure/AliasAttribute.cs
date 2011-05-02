using System;

namespace StoryQ.Infrastructure
{
    ///<summary>
    /// When a StoryQ step method can be named different things, use aliases to ensure the converter can still parse the plaintext
    ///</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AliasAttribute : Attribute
    {
        ///<summary>
        ///</summary>
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }

        ///<summary>
        /// Gets the alias for this attribute
        ///</summary>
        public string Alias { get; private set; }
    }

}