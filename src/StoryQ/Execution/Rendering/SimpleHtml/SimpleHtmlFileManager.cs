namespace StoryQ.Execution.Rendering.SimpleHtml
{
    using System.IO;
    using System.Text;

    internal class SimpleHtmlFileManager : XmlFileManagerBase
    {
        private const string StyleSheetFileName = "StoryQ-SimpleHtml.xslt";

        private static SimpleHtmlFileManager instance;

        private SimpleHtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        public static SimpleHtmlFileManager Instance
        {
            get
            {
                return instance ?? (instance = new SimpleHtmlFileManager());
            }
        }

        protected override void WriteDependantFiles(string directory)
        {
            File.WriteAllText(Path.Combine(directory, StyleSheetFileName), SimpleHtmlDependencies.SimpleHtml, Encoding.UTF8);
        }
    }
}
