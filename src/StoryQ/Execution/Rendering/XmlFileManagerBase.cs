namespace StoryQ.Execution.Rendering
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    /// Looks after writing out an XML file, post testrun
    /// </summary>
    abstract class XmlFileManagerBase
    {
        private const string OutputDirectory = "StoryQ_Report";

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

        public XmlCategoriser Categoriser { get; private set; }

        protected abstract void WriteDependantFiles(string directory);
    }
}
