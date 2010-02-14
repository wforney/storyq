using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StoryQ.Execution;
using StoryQ.Execution.Rendering;
using StoryQ.Execution.Rendering.Html;
using StoryQ.Execution.Rendering.SimpleHtml;
using StoryQ.Formatting;

namespace StoryQ
{
    /// <summary>
    /// A StoryQ infrastructure class that is the base for all fluent interface classes 
    /// </summary>
    public class FragmentBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FragmentBase"/> class.
        /// </summary>
        /// <param name="narrative">The narrative.</param>
        protected FragmentBase(Narrative narrative)
        {
            Narrative = narrative;
        }

        /// <summary>
        /// Gets or sets the narrative.
        /// </summary>
        /// <value>The narrative.</value>
        public Narrative Narrative { get; private set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public FragmentBase Parent { get; internal set; }

        /// <summary>
        /// Enumerates over this and each of its ancestors. Reverse the collection to go through the story in correct order
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FragmentBase> SelfAndAncestors()
        {
            for (FragmentBase f = this; f != null; f = f.Parent)
            {
                yield return f;
            }
        }

        /// <summary>
        /// Runs the current sequence of narratives, printing the results in plain text to the console. 
        /// </summary>
        public void Execute()
        {
            Execute(new TextRenderer(Console.Out));
        }

        /// <summary>
        /// Runs the current sequence of narratives, reporting to an xml(+xslt) file. This method requires a reference to
        /// the "current" method in order to categorise results, you should pass in "MethodBase.GetCurrentMethod()".
        /// Reports are written to the current directory, look for an xml file beginning with "StoryQ"
        /// </summary>
        /// <param name="currentMethod">The current method (use "MethodBase.GetCurrentMethod()")</param>
        public void ExecuteWithSimpleReport(MethodBase currentMethod)
        {
            Execute(new TextRenderer(Console.Out), SimpleHtmlFileManager.AutoSavingCategoriser.GetRenderer(currentMethod));
        }

        /// <summary>
        /// Runs the current sequence of narratives, reporting to an xml(+xslt) file augmented with jQuery.StoryQ
        /// widget for interactive viewing of the results.  This method requires a reference to
        /// the "current" method in order to categorise results, you should pass in "MethodBase.GetCurrentMethod()".
        /// Reports are written to the current directory, look for an xml file beginning with "StoryQ"
        /// </summary>
        /// <param name="currentMethod">The current method (use "MethodBase.GetCurrentMethod()")</param>
        public void ExecuteWithReport(MethodBase currentMethod)
        {
            Execute(new TextRenderer(Console.Out), HtmlFileManager.AutoSavingCategoriser.GetRenderer(currentMethod));
        }

        /// <summary>
        /// Runs the current sequence of narratives against a renderer
        /// </summary>
        /// <param name="renderers"></param>
        internal void Execute(params IRenderer[] renderers)
        {
            var v = SelfAndAncestors().Reverse().Select(x => x.Narrative.Execute()).ToList();
            Array.ForEach(renderers, x => x.Render(v));

            var exception = Exceptions(v, ResultType.Failed)
                            .Concat(Exceptions(v, ResultType.Pending))
                            .FirstOrDefault();

            if (exception != null)
            {
                ExceptionHelper.TryForceStackTracePermanence(exception, "-- End of original stack trace, test framework stack trace follows: --");
                throw exception;
            }
        }

        private static IEnumerable<Exception> Exceptions(IEnumerable<Result> results, ResultType type)
        {
            return results.Where(x => x.Type == type).Select(x => x.Exception);
        }

        /// <summary>
        /// Converts a method into text
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected static string MethodToText(Delegate method, params object[] arguments)
        {
            if (method.Method.IsSpecialName)
            {
                throw new ArgumentException("Could not generate a name from special method: " + method.Method, "method");
            }
            return Formatter.FormatMethod(method, arguments);
        }
    }
}
