using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering.RichHtml
{
    internal class RichHtmlFileManager:XmlFileManagerBase
    {
        private const string StyleSheetFileName = "StoryQ-html.xslt";

        private static RichHtmlFileManager instance;

        private RichHtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        public static RichHtmlFileManager Instance
        {
            get
            {
                return instance ?? (instance = new RichHtmlFileManager());
            }
        }

        protected override void WriteDependantFiles(string directory)
        {
            //merge all javascript and css into a giant XSLT file :D
            StringBuilder xslt = new StringBuilder(Dependencies.RichHtml_xslt);
            xslt.Replace(@"<script src=""jquery-1.4.2.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_1_4_2_min_js));
            xslt.Replace(@"<script src=""jquery.tagcloud.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_tagcloud_min_js));
            xslt.Replace(@"<script src=""jquery.treeview.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_treeview_min_js));
            xslt.Replace(@"<script src=""storyq.js"" type=""text/javascript""/>", Script(Dependencies.storyq_js));
            xslt.Replace(@"<link href=""storyq.css"" rel=""stylesheet""/>", Style(Dependencies.storyq_css));
            WriteFile(directory, StyleSheetFileName, xslt.ToString());

            //images
            SaveImage("storyq-icons.png", Dependencies.storyq_icons, directory);
            //treeview images
            SaveImage("minus.gif", Dependencies.minus, directory);
            SaveImage("plus.gif", Dependencies.plus, directory);
            SaveImage("treeview-default-line.gif", Dependencies.treeview_default_line, directory);
            SaveImage("treeview-default.gif", Dependencies.treeview_default, directory);


        }

        /// <summary>
        /// wrap some css in a style tag
        /// </summary>
        private string Style(string other)
        {
            return new XElement("style", new XAttribute("type", "text/css"), new XCData(other)).ToString();
        }

        /// <summary>
        /// wrap some JS in a script tag
        /// </summary>
        private string Script(string js)
        {
            return new XElement("script", new XAttribute("type", "text/javascript"), new XCData(js)).ToString();
        }

        private void WriteFile(string directory, string fileName, string text)
        {
            File.WriteAllText(Path.Combine(directory, fileName), text, Encoding.UTF8);
        }

        private static void SaveImage(string fileName, Image image, string directory)
        {
            using (image)
            {
                image.Save(ImagesDirectory(fileName, directory), GetEncoding(fileName));
            }
        }

        private static ImageFormat GetEncoding(string file)
        {
            switch(Path.GetExtension(file).ToLowerInvariant())
            {
                case ".gif":
                    return ImageFormat.Gif;
                case ".png":
                    return ImageFormat.Png;
            }
            throw new ArgumentException("Couldn't get an encoding for "+file, "file");
        }

        private static string ImagesDirectory(string fileName, string directory)
        {
            string images = Path.Combine(directory, "images");
            Directory.CreateDirectory(images);
            return Path.Combine(images, fileName);
        }
    }
}