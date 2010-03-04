using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryQ.Execution
{
    /// <summary>
    /// The result or outcome of a step being executed
    /// </summary>
    public class Result
    {
        private readonly string prefix;
        private readonly int indentLevel;
        private readonly string text;
        private readonly ResultType type;
        private readonly Exception exception;


        internal Result(string prefix, int indentLevel, string text, Exception exception, bool isPending)
        {
            this.prefix = prefix;
            this.indentLevel = indentLevel;
            this.text = text;
            this.exception = exception;
            this.type = isPending ? ResultType.Pending : ResultType.Failed;
        }

        internal Result(string prefix, int indentLevel, string text, ResultType type)
        {
            this.prefix = prefix;
            this.indentLevel = indentLevel;
            this.text = text;
            this.type = type;
        }

        /// <summary>
        /// Gets the prefix (the type of the step, such as "Given")
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get
            {
                return prefix;
            }

        }

        /// <summary>
        /// Gets the text (the content of the step)
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return text;
            }
        }

        /// <summary>
        /// Gets the result type (what was the outcome?)
        /// </summary>
        /// <value>The type.</value>
        public ResultType Type
        {
            get
            {
                return type;
            }
        }


        /// <summary>
        /// Gets the exception, if there was one
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get
            {
                return exception;
            }
        }

        /// <summary>
        /// Gets the indent level.
        /// </summary>
        /// <value>The indent level.</value>
        public int IndentLevel
        {
            get
            {
                return indentLevel;
            }
        }
    }
}