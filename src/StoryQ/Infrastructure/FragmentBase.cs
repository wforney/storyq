using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using StoryQ.Execution;
using StoryQ.Execution.Rendering;
using StoryQ.Execution.Rendering.RichHtml;
using StoryQ.Execution.Rendering.SimpleHtml;
using StoryQ.Formatting;

namespace StoryQ.Infrastructure
{
    /// <summary>
    /// A StoryQ infrastructure class that is the base for all fluent interface classes 
    /// </summary>
    public class FragmentBase : IStepContainer
    {
        private readonly Step step;
        private readonly IStepContainer parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="FragmentBase"/> class.
        /// </summary>
        public FragmentBase(Step step, IStepContainer parent)
        {
            this.step = step;
            this.parent = parent;
        }

        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        /// <value>The Step.</value>
        Step IStepContainer.Step
        {
            get
            {
                return step;
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        IStepContainer IStepContainer.Parent
        {
            get
            {
                return parent;
            }
        }

        /// <summary>
        /// Enumerates over this and each of its ancestors. Reverse the collection to go through the story in correct order
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStepContainer> IStepContainer.SelfAndAncestors()
        {
            for (IStepContainer f = this; f != null; f = f.Parent)
            {
                yield return f;
            }
        }

        /// <summary>
        /// Runs the current sequence of steps, printing the results in plain text to the console. 
        /// </summary>
        public void Execute()
        {
            ((IStepContainer)this).Execute(new TextRenderer(Console.Out));
        }

        /// <summary>
        /// Runs the current sequence of Steps, reporting to an xml(+xslt) file augmented with jQuery
        /// widget for interactive viewing of the results.  This method requires a reference to
        /// the "current" method in order to categorise results, you should pass in "MethodBase.GetCurrentMethod()".
        /// Reports are written to the current directory, look for an xml file beginning with "StoryQ".
        /// If you prefer to have a non interactive report (for example if you are using a legacy browser), set "StoryQSettings.ReportSupportsLegacyBrowsers = true"
        /// </summary>
        /// <param name="currentMethod">The current method (use "MethodBase.GetCurrentMethod()")</param>
        public void ExecuteWithReport(MethodBase currentMethod)
        {
            XmlFileManagerBase manager = StoryQSettings.ReportSupportsLegacyBrowsers
                ? (XmlFileManagerBase)SimpleHtmlFileManager.Instance
                : RichHtmlFileManager.Instance;

            ((IStepContainer)this).Execute(new TextRenderer(Console.Out), manager.Categoriser.GetRenderer(currentMethod));
        }

        /// <summary>
        /// This overload infers the current method with 'new StackFrame(1).GetMethod()'. If it doesn't work, call the overload that takes a method
        /// Runs the current sequence of Steps, reporting to an xml(+xslt) file augmented with jQuery
        /// widget for interactive viewing of the results.  This method requires a reference to
        /// the "current" method in order to categorise results, you should pass in "MethodBase.GetCurrentMethod()".
        /// Reports are written to the current directory, look for an xml file beginning with "StoryQ".
        /// If you prefer to have a non interactive report (for example if you are using a legacy browser), set "StoryQSettings.ReportSupportsLegacyBrowsers = true"
        /// </summary>
        public void ExecuteWithReport()
        {
            ExecuteWithReport(new StackFrame(1).GetMethod());
        }

        /// <summary>
        /// Runs the current sequence of Steps against a renderer
        /// </summary>
        /// <param name="renderers"></param>
        void IStepContainer.Execute(params IRenderer[] renderers)
        {
            List<Result> results = ((IStepContainer)this)
                                   .SelfAndAncestors()
                                   .Reverse()
                                   .Select(x => x.Step.Execute())
                                   .ToList();

            Array.ForEach(renderers, x => x.Render(results));

            var exception = Exceptions(results, ResultType.Failed)
                            .Concat(Exceptions(results, ResultType.Pending))
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

        #region Hiding Object's methods

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method has been hidden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType()
        {
            throw new NotSupportedException();
        }

        #endregion

    }
}
