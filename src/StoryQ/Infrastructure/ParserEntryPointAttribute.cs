namespace StoryQ.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Tells the parser which class in this assembly is the right entrypoint class
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ParserEntryPointAttribute : Attribute
    {
        /// <summary>
        /// Constructs a ParserEntryPointAttribute
        /// </summary>
        public ParserEntryPointAttribute(Type target)
        {
            this.Target = target;
        }

        /// <summary>
        /// The type to that is the Parser entry point
        /// </summary>
        public Type Target { get; private set; }
    }
}
