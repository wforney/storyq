using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering.SimpleHtml
{
    internal static class SimpleHtmlFileManager
    {
        private static XmlCategoriser instance;

        private const string StyleSheetFileName = "StoryQ-SimpleHtml.xslt";

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
                            //write out the xslt
                            File.WriteAllText(SubDirectory(StyleSheetFileName), SimpleHtmlDependencies.SimpleHtml, Encoding.UTF8);
                        };
      
                    instance = new XmlCategoriser(doc.Root);
                }
                return instance;
            }
        }

        private static string SubDirectory(string fileName)
        {
            const string dir = "StoryQ_Report";
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);
        }

        private static string GetFileName()
        {
            return "StoryQ.xml";
//            //I think 5 million should be high enough
//            for (int i = 0; i < 5000000; i++)
//            {
//                string s = string.Format("StoryQ{0}.xml", (i == 0 ? "" : " (" + i + ")"));
//                if(!File.Exists(s))
//                {
//                    return s;
//                }
//            }
//            throw new Exception("Could not find a unique filename");
        }
    }
}
