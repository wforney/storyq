using System;
using System.Collections.Generic;

namespace StoryQ.Execution
{
    /// <summary>
    /// The result or outcome of a step being executed
    /// </summary>
    public class Result
    {
        internal Result(string prefix, int indentLevel, string text, IEnumerable<string> tags, Exception exception, bool isPending)
        {
            Prefix = prefix;
            IndentLevel = indentLevel;
            Text = text;
            Exception = exception;
            Tags = tags;
            Type = isPending ? ResultType.Pending : ResultType.Failed;
        }

        internal Result(string prefix, int indentLevel, string text, IEnumerable<string> tags, ResultType type)
        {
            Prefix = prefix;
            IndentLevel = indentLevel;
            Text = text;
            Tags = tags;
            Type = type;
        }

        /// <summary>
        /// Gets the prefix (the type of the step, such as "Given")
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix { get; private set; }

        /// <summary>
        /// Gets the text (the content of the step)
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the result type (what was the outcome?)
        /// </summary>
        /// <value>The type.</value>
        public ResultType Type { get; private set; }


        /// <summary>
        /// Gets the exception, if there was one
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the indent level.
        /// </summary>
        /// <value>The indent level.</value>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Gets the tags associated with this step
        /// </summary>
        public IEnumerable<string> Tags { get; private set; }
    }
}