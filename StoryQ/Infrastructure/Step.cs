namespace StoryQ.Infrastructure
{
    using StoryQ.Execution;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A StoryQ infrastructure class that represents single a line of a story. Some steps can be
    /// executed, while others are just descriptive
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Previously released.")]
    public class Step
    {
        /// <summary>
        /// use this Action when a Step is supposed to be not executable
        /// </summary>
        public static readonly Action DoNothing = () => { };

        private const string StepPendingMessage = "Pending";

        /// <summary>
        /// Initializes a new instance of the <see cref="Step" /> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="indentLevel">The indent level.</param>
        /// <param name="text">The text.</param>
        /// <param name="action">The action.</param>
        public Step(string prefix, int indentLevel, string text, Action action)
        {
            this.Prefix = prefix;
            this.IndentLevel = indentLevel;
            this.Text = text;
            this.Action = action;
        }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix { get; private set; }

        /// <summary>
        /// Gets or sets the indent level.
        /// </summary>
        /// <value>The indent level.</value>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public Action Action { get; private set; }

        /// <summary>
        /// Gets the list of tags that have been applied to this step
        /// </summary>
        public List<string> Tags { get; } = new List<string>();

        /// <summary>
        /// Gets a value indicating whether this instance is executable.
        /// </summary>
        /// <value><c>true</c> if this instance is executable; otherwise, <c>false</c>.</value>
        public bool IsExecutable => this.Action != DoNothing;


        /// <summary>
        /// Executes this Step.
        /// </summary>
        /// <returns>the resulting result</returns>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Because.")]
        public Result Execute()
        {
            if (!this.IsExecutable)
            {
                return Result.ForResultType(this.Prefix, this.IndentLevel, this.Text, this.Tags, ResultType.NotExecutable);
            }

            try
            {
                this.Action();
                return Result.ForResultType(this.Prefix, this.IndentLevel, this.Text, this.Tags, ResultType.Passed);
            }
            catch (NotImplementedException ex)
            {
                // transform any NotImplementedException into a unit test specific "pending" exception
                var message = ex.Message == StepPendingMessage
                                     ? "Pending"
                                     : $"Pending due to {Environment.NewLine}{ex}";

                var pex = StoryQSettings.PendingExceptionBuilder(message, ex);
                return Result.ForException(this.Prefix, this.IndentLevel, this.Text, this.Tags, pex, true);
            }
            catch (Exception ex)
            {
                return Result.ForException(this.Prefix, this.IndentLevel, this.Text, this.Tags, ex, false);
            }
        }
    }
}
