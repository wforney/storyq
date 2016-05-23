// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="SimpleHtmlFileManager.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Execution.Rendering.SimpleHtml
{
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
        /// The instance
        /// </summary>
        private static SimpleHtmlFileManager instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="SimpleHtmlFileManager"/> class from being created.
        /// </summary>
        private SimpleHtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SimpleHtmlFileManager Instance => instance ?? (instance = new SimpleHtmlFileManager());

        /// <summary>
        /// Writes the dependant files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        protected override void WriteDependantFiles(string directory)
        {
            File.WriteAllText(Path.Combine(directory, StyleSheetFileName), SimpleHtmlDependencies.SimpleHtml, Encoding.UTF8);
        }
    }
}
