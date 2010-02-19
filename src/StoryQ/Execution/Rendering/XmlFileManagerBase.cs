using System;
using System.IO;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering
{
    /// <summary>
    /// Looks after writing out an XML file, post testrun
    /// </summary>
    abstract class XmlFileManagerBase
    {
        private const string OutputDirectory = "StoryQ_Report";

        protected XmlFileManagerBase(string xmlFileName, string styleSheetFileName)
        {
            DirectoryInfo outputDir = new DirectoryInfo(OutputDirectory);
            outputDir.Create();
            string fullPath = outputDir.FullName;

            XProcessingInstruction instruction = null;
            if (styleSheetFileName != null)
            {
                string stylesheet = string.Format("href=\"{0}\" type=\"text/xsl\"", styleSheetFileName);
                instruction = new XProcessingInstruction("xml-stylesheet", stylesheet);
            }

            XDocument doc = new XDocument(instruction, new XElement("StoryQRun"));

            AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
            {
                doc.Save(Path.Combine(fullPath, xmlFileName));
                WriteDependantFiles(fullPath);
            };

            Categoriser = new XmlCategoriser(doc.Root);
        }

        public XmlCategoriser Categoriser { get; private set; }

        protected abstract void WriteDependantFiles(string directory);
    }
}
