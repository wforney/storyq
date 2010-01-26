using System;
using StoryQ.Execution;

namespace StoryQ
{
    /// <summary>
    ///  A StoryQ infrastructure class that represents single a line of a story. Some narratives can be executed, while others are just descriptive
    /// </summary>
    public class Narrative
    {
        private const string NarrativePendingMessage = "Pending";
        /// <summary>
        /// use this Action when a narrative is supposed to "pend"
        /// </summary>
        public static readonly Action Pend = () => { throw new NotImplementedException(NarrativePendingMessage); };

        /// <summary>
        /// use this Action when a narrative is supposed to be not executable
        /// </summary>
        public static readonly Action DoNothing = () => { };

        internal Narrative(string prefix, int indentLevel, string text, Action action)
        {
            Prefix = prefix;
            IndentLevel = indentLevel;
            Text = text;
            Action = action;
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
        public Action Action { get; internal set; }


        /// <summary>
        /// Executes this narrative.
        /// </summary>
        /// <returns>the resulting result</returns>
        public Result Execute()
        {
            if (!IsExecutable)
            {
                return new Result(Prefix, IndentLevel, Text, ResultType.NotExecutable);
            }

            try
            {
                Action();
                return new Result(Prefix, IndentLevel, Text, ResultType.Passed);
            }
            catch (NotImplementedException ex)
            {
                //transform any NotImplementedException into a unit test specific "pending" exception
                string message = ex.Message == NarrativePendingMessage
                                     ? "Pending"
                                     : "Pending due to " + Environment.NewLine + ex;

                var pex = ExceptionHelper.PendingExceptionBuilder(message, ex);
                return new Result(Prefix, IndentLevel, Text, pex, true);
            }
            catch (Exception ex)
            {
                return new Result(Prefix, IndentLevel, Text, ex, Action == Pend);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is executable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is executable; otherwise, <c>false</c>.
        /// </value>
        public bool IsExecutable
        {
            get { return Action != DoNothing; }
        }
    }
}