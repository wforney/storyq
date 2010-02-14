using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering.Html
{
    internal static class HtmlFileManager
    {
        private static XmlCategoriser instance;

        private const string StyleSheetFileName = "StoryQ-Html.xslt";
        private const string baseDir = "StoryQ_Report";

        public static XmlCategoriser AutoSavingCategoriser
        {
            get
            {
                if (instance == null)
                {
                    string stylesheet = string.Format("href=\"{0}\" type=\"text/xsl\"", StyleSheetFileName);
                    XDocument doc = new XDocument(
                            new XProcessingInstruction("xml-stylesheet", stylesheet),
                            new XElement("StoryQRun"/*, new XAttribute("Started", DateTime.Now.ToString())*/));

                    AppDomain.CurrentDomain.DomainUnload += (sender, args) =>
                        {
                            //save the xml
                            string fileName = SubDirectory(GetFileName());
                            doc.Save(fileName);
                            //write out all the files
                            //xslt
                            File.WriteAllText(SubDirectory(StyleSheetFileName), HtmlDependencies.Html, Encoding.UTF8);
                            //css
                            File.WriteAllText(CssDirectory("jquery.treeview.css"), HtmlDependencies.screen_treeview, Encoding.UTF8);
                            File.WriteAllText(CssDirectory("screen.storyq.css"), HtmlDependencies.screen_storyq, Encoding.UTF8);
                            //js
                            File.WriteAllText(SubDirectory("jquery.storyq.js"), HtmlDependencies.jquery_storyq, Encoding.UTF8);
                            File.WriteAllText(SubDirectory("jquery.treeview.js"), HtmlDependencies.jquery_treeview, Encoding.UTF8);
                            //images
                            SavePngImage("failed.png", HtmlDependencies.failed);
                            SavePngImage("failedproject.png", HtmlDependencies.failedproject);
                            SavePngImage("failedscenario.png", HtmlDependencies.failedscenario);
                            SavePngImage("passed.png", HtmlDependencies.passed);
                            SavePngImage("passedproject.png", HtmlDependencies.passedproject);
                            SavePngImage("passedscenario.png", HtmlDependencies.passedscenario);
                            SavePngImage("pending.png", HtmlDependencies.pending);
                            SavePngImage("pendingproject.png", HtmlDependencies.pendingproject);
                            SavePngImage("pendingscenario.png", HtmlDependencies.pendingscenario);
                        };
      
                    instance = new XmlCategoriser(doc.Root);
                }
                return instance;
            }
        }

        private static void SavePngImage(string fileName, Image image)
        {
            using (image)
            {
                image.Save(ImagesDirectory(fileName), System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static string CssDirectory(string fileName)
        {
            return CreateDirectory(string.Format("{0}\\css", baseDir), fileName);
        }

        private static string ImagesDirectory(string fileName)
        {
            return CreateDirectory(string.Format("{0}\\images", baseDir), fileName);
        }

        private static string SubDirectory(string fileName)
        {
            return CreateDirectory(baseDir, fileName);
        }

        private static string CreateDirectory(string dir, string fileName)
        {
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);           
        }

        private static string GetFileName()
        {
            return "StoryQ.xml";
        }
    }
}
