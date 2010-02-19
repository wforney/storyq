using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace StoryQ.Execution.Rendering.Html
{
    internal class HtmlFileManager:XmlFileManagerBase
    {
        private const string StyleSheetFileName = "StoryQ-Html.xslt";

        private static HtmlFileManager instance;

        private HtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        public static HtmlFileManager Instance
        {
            get
            {
                return instance ?? (instance = new HtmlFileManager());
            }
        }

        protected override void WriteDependantFiles(string directory)
        {
            File.WriteAllText(Path.Combine(directory, StyleSheetFileName), HtmlDependencies.Html, Encoding.UTF8);
            //images
            SaveImage("results.png", HtmlDependencies.results, directory);
            //treeview images
            SaveImage("minus.gif", HtmlDependencies.minus, directory);
            SaveImage("plus.gif", HtmlDependencies.plus, directory);
            SaveImage("treeview-black-line.gif", HtmlDependencies.treeview_black_line, directory);
            SaveImage("treeview-black.gif", HtmlDependencies.treeview_black, directory);
            SaveImage("treeview-default-line.gif", HtmlDependencies.treeview_default_line, directory);
            SaveImage("treeview-default.gif", HtmlDependencies.treeview_default, directory);
            SaveImage("treeview-famfamfam-line.gif", HtmlDependencies.treeview_famfamfam_line, directory);
            SaveImage("treeview-famfamfam.gif", HtmlDependencies.treeview_famfamfam, directory);
            SaveImage("treeview-gray-line.gif", HtmlDependencies.treeview_gray_line, directory);
            SaveImage("treeview-gray.gif", HtmlDependencies.treeview_gray, directory);
            SaveImage("treeview-red-line.gif", HtmlDependencies.treeview_red_line, directory);
            SaveImage("treeview-red.gif", HtmlDependencies.treeview_red, directory);
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
