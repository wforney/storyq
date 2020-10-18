namespace StoryQ.Infrastructure
{
    using StoryQ.Execution;
    using StoryQ.Execution.Rendering;
    using StoryQ.Execution.Rendering.RichHtml;
    using StoryQ.Execution.Rendering.SimpleHtml;
    using StoryQ.Formatting;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// A StoryQ infrastructure class that is the base for all fluent interface classes
    /// </summary>
    /// <seealso cref="StoryQ.Infrastructure.IStepContainer" />
    public class FragmentBase : IStepContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FragmentBase" /> class.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="parent">The parent.</param>
        public FragmentBase(Step step, IStepContainer parent)
        {
            this.Step = step;
            this.Parent = parent;
        }

        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        /// <value>The Step.</value>
        public Step Step { get; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public IStepContainer Parent { get; }

        /// <summary>
        /// Enumerates over this and each of its ancestors. Reverse the collection to go through the
        /// story in correct order
        /// </summary>
        /// <returns>IEnumerable&lt;IStepContainer&gt;.</returns>
        public IEnumerable<IStepContainer> SelfAndAncestors()
        {
            for (IStepContainer f = this; f != null; f = f.Parent)
            {
                yield return f;
            }
        }

        /// <summary>
        /// Runs the current sequence of steps, printing the results in plain text to the console.
        /// </summary>
        public void Execute() => ((IStepContainer)this).Execute(new TextRenderer(Console.Out));

        /// <summary>
        /// Runs the current sequence of Steps, reporting to an xml(+xslt) file augmented with
        /// jQuery widget for interactive viewing of the results. This method requires a reference
        /// to the "current" method in order to categorise results, you should pass in
        /// "MethodBase.GetCurrentMethod()". Reports are written to the current directory, look for
        /// an xml file beginning with "StoryQ". If you prefer to have a non interactive report (for
        /// example if you are using a legacy browser), set
        /// "StoryQSettings.ReportSupportsLegacyBrowsers = true"
        /// </summary>
        /// <param name="currentMethod">The current method (use "MethodBase.GetCurrentMethod()")</param>
        public void ExecuteWithReport(MethodBase currentMethod)
        {
            var manager = StoryQSettings.ReportSupportsLegacyBrowsers
                ? (XmlFileManagerBase)SimpleHtmlFileManager.Instance
                : RichHtmlFileManager.Instance;

            ((IStepContainer)this).Execute(new TextRenderer(Console.Out), manager.Categoriser.GetRenderer(currentMethod));
        }

        /// <summary>
        /// This overload infers the current method with 'new StackFrame(1).GetMethod()'. If it
        /// doesn't work, call the overload that takes a method Runs the current sequence of Steps,
        /// reporting to an xml(+xslt) file augmented with jQuery widget for interactive viewing of
        /// the results. This method requires a reference to the "current" method in order to
        /// categorise results, you should pass in "MethodBase.GetCurrentMethod()". Reports are
        /// written to the current directory, look for an xml file beginning with "StoryQ". If you
        /// prefer to have a non interactive report (for example if you are using a legacy browser),
        /// set "StoryQSettings.ReportSupportsLegacyBrowsers = true"
        /// </summary>
        public void ExecuteWithReport() => this.ExecuteWithReport(new StackFrame(1).GetMethod());

        /// <summary>
        /// Runs the current sequence of Steps against a renderer
        /// </summary>
        /// <param name="renderers">The renderers.</param>
        void IStepContainer.Execute(params IRenderer[] renderers)
        {
            var results = ((IStepContainer)this)
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
                ExceptionHelper.TryForceStackTracePermanence(
                    exception,
                    "-- End of original stack trace, test framework stack trace follows: --");

                throw exception;
            }
        }

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() => base.ToString();

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object" /> is equal to this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// This method has been overridden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// This method has been hidden to hide it from the Fluent Interface. Don't call it!
        /// </summary>
        /// <returns>The exact runtime type of the current instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() => base.GetType();

        /// <summary>
        /// Converts a method into text
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentException">
        /// Could not generate a name from special method: + method.Method;method
        /// </exception>
        protected static string MethodToText(Delegate method, params object[] arguments)
        {
            _ = method ?? throw new ArgumentNullException(nameof(method));

            if (method.Method.IsSpecialName)
            {
                throw new ArgumentException(
                    $"Could not generate a name from special method: {method.Method}",
                    nameof(method));
            }

            return Formatter.FormatMethod(method, arguments);
        }

        /// <summary>
        /// Exceptionses the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="type">The type.</param>
        /// <returns>IEnumerable&lt;Exception&gt;.</returns>
        private static IEnumerable<Exception?> Exceptions(IEnumerable<Result> results, ResultType type) =>
            results.Where(x => x.Type == type).Select(x => x.Exception);
    }
}
