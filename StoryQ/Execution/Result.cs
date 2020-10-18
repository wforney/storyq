namespace StoryQ.Execution
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The result or outcome of a step being executed
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="exception">The exception.</param>
        private Result(
            string prefix,
            int indentLevel,
            string text,
            ResultType type,
            IEnumerable<string> tags,
            Exception? exception)
        {
            this.Prefix = prefix;
            this.Text = text;
            this.Type = type;
            this.Exception = exception;
            this.IndentLevel = indentLevel;
            this.Tags = tags;
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
        public Exception? Exception { get; private set; }

        /// <summary>
        /// Gets the indent level.
        /// </summary>
        /// <value>The indent level.</value>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Gets the tags associated with this step
        /// </summary>
        /// <value>The tags.</value>
        public IEnumerable<string> Tags { get; private set; }

        /// <summary>
        /// Fors the type of the result.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="text">The text.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="type">The type.</param>
        /// <returns>Result.</returns>
        internal static Result ForResultType(
            string prefix,
            int indentLevel,
            string text,
            IEnumerable<string> tags,
            ResultType type) => new Result(prefix, indentLevel, text, type, tags, null);

        /// <summary>
        /// Fors the exception.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="text">The text.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="isPending">if set to <c>true</c> [is pending].</param>
        /// <returns>Result.</returns>
        internal static Result ForException(
            string prefix,
            int indentLevel,
            string text,
            IEnumerable<string> tags,
            Exception? exception,
            bool isPending) =>
            new Result(
                prefix,
                indentLevel,
                text,
                isPending ? ResultType.Pending : ResultType.Failed,
                tags,
                exception);
    }
}
