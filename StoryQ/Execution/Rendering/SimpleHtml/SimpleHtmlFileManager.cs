namespace StoryQ.Execution.Rendering.SimpleHtml
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class SimpleHtmlFileManager.
    /// </summary>
    /// <seealso cref="StoryQ.Execution.Rendering.XmlFileManagerBase" />
    internal class SimpleHtmlFileManager : XmlFileManagerBase
    {
        /// <summary>
        /// The style sheet file name
        /// </summary>
        private const string StyleSheetFileName = "StoryQ-SimpleHtml.xslt";

        /// <summary>
        /// Prevents a default instance of the <see cref="SimpleHtmlFileManager" /> class from being created.
        /// </summary>
        private SimpleHtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SimpleHtmlFileManager Instance { get; } = new SimpleHtmlFileManager();

        /// <summary>
        /// Writes the dependant files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        [SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "Because.")]
        protected override void WriteDependantFiles(string directory) =>
            File.WriteAllText(
                Path.Combine(directory, StyleSheetFileName),
                SimpleHtmlDependencies.SimpleHtml,
                Encoding.UTF8);
    }
}
