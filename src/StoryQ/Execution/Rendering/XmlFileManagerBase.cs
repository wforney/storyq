// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="XmlFileManagerBase.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Execution.Rendering
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    /// Looks after writing out an XML file, post testrun
    /// </summary>
    internal abstract class XmlFileManagerBase
    {
        /// <summary>
        /// The output directory
        /// </summary>
        private const string OutputDirectory = "StoryQ_Report";

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileManagerBase"/> class.
        /// </summary>
        /// <param name="xmlFileName">Name of the XML file.</param>
        /// <param name="styleSheetFileName">Name of the style sheet file.</param>
        protected XmlFileManagerBase(string xmlFileName, string styleSheetFileName)
        {
            var outputDir = new DirectoryInfo(OutputDirectory);
            outputDir.Create();
            var fullPath = outputDir.FullName;

            XProcessingInstruction instruction = null;
            if (styleSheetFileName != null)
            {
                var stylesheet = string.Format("href=\"{0}\" type=\"text/xsl\"", styleSheetFileName);
                instruction = new XProcessingInstruction("xml-stylesheet", stylesheet);
            }

            var doc = new XDocument(instruction, new XElement("StoryQRun"));

            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
            {
                Directory.CreateDirectory(fullPath);
                doc.Save(Path.Combine(fullPath, xmlFileName));
                this.WriteDependantFiles(fullPath);
            };

            this.Categoriser = new XmlCategoriser(doc.Root);
        }

        /// <summary>
        /// Gets the categoriser.
        /// </summary>
        /// <value>The categoriser.</value>
        public XmlCategoriser Categoriser { get; private set; }

        /// <summary>
        /// Writes the dependant files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        protected abstract void WriteDependantFiles(string directory);
    }
}
