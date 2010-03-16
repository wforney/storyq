using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace StoryQ.Execution.Rendering.RichHtml
{
    internal class RichHtmlFileManager:XmlFileManagerBase
    {
        private const string StyleSheetFileName = "RichHtml.xslt";

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
            writeFile(directory, StyleSheetFileName, Dependencies.RichHtml_xslt);
            writeFile(directory, "jquery-1.4.2.min.js", Dependencies.jquery_1_4_2_min_js);
            writeFile(directory, "jquery.tagcloud.min.js", Dependencies.jquery_tagcloud_min_js);
            writeFile(directory, "jquery.treeview.min.js", Dependencies.jquery_treeview_min_js);
            writeFile(directory, "storyq.js", Dependencies.storyq_js);
            writeFile(directory, "storyq.css", Dependencies.storyq_css);
            //images
            SaveImage("storyq-icons.png", Dependencies.storyq_icons, directory);
            //treeview images
            SaveImage("minus.gif", Dependencies.minus, directory);
            SaveImage("plus.gif", Dependencies.plus, directory);
            SaveImage("treeview-default-line.gif", Dependencies.treeview_default_line, directory);
            SaveImage("treeview-default.gif", Dependencies.treeview_default, directory);
            //javascript

        }

        private void writeFile(string directory, string fileName, string text)
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