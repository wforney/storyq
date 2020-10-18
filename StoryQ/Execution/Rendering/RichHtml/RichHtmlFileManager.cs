namespace StoryQ.Execution.Rendering.RichHtml
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Class RichHtmlFileManager.
    /// </summary>
    /// <seealso cref="StoryQ.Execution.Rendering.XmlFileManagerBase" />
    internal class RichHtmlFileManager : XmlFileManagerBase
    {
        /// <summary>
        /// The style sheet file name
        /// </summary>
        private const string StyleSheetFileName = "StoryQ-html.xslt";

        /// <summary>
        /// Prevents a default instance of the <see cref="RichHtmlFileManager" /> class from being created.
        /// </summary>
        private RichHtmlFileManager()
            : base("StoryQ.xml", StyleSheetFileName)
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static RichHtmlFileManager Instance { get; } = new RichHtmlFileManager();

        /// <summary>
        /// Writes the dependant files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        protected override void WriteDependantFiles(string directory)
        {
            // merge all javascript and css into a giant XSLT file :D
            var xslt = new StringBuilder(Dependencies.RichHtml_xslt);
            xslt.Replace(@"<script src=""jquery-1.4.2.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_1_4_2_min_js));
            xslt.Replace(@"<script src=""jquery.tagcloud.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_tagcloud_min_js));
            xslt.Replace(@"<script src=""jquery.treeview.min.js"" type=""text/javascript""/>", Script(Dependencies.jquery_treeview_min_js));
            xslt.Replace(@"<script src=""storyq.js"" type=""text/javascript""/>", Script(Dependencies.storyq_js));
            xslt.Replace(@"<link href=""storyq.css"" rel=""stylesheet""/>", Style(Dependencies.storyq_css));
            WriteFile(directory, StyleSheetFileName, xslt.ToString());

            // images
            SaveImage("storyq-icons.png", Dependencies.storyq_icons, directory);

            // treeview images
            SaveImage("minus.gif", Dependencies.minus, directory);
            SaveImage("plus.gif", Dependencies.plus, directory);
            SaveImage("treeview-default-line.gif", Dependencies.treeview_default_line, directory);
            SaveImage("treeview-default.gif", Dependencies.treeview_default, directory);
        }

        /// <summary>
        /// wrap some css in a style tag
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.String.</returns>
        private static string Style(string other) => new XElement("style", new XAttribute("type", "text/css"), new XCData(other)).ToString();

        /// <summary>
        /// wrap some JS in a script tag
        /// </summary>
        /// <param name="js">The js.</param>
        /// <returns>System.String.</returns>
        private static string Script(string js) => new XElement("script", new XAttribute("type", "text/javascript"), new XCData(js)).ToString();

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="text">The text.</param>
        [SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "Because.")]
        private static void WriteFile(string directory, string fileName, string text) =>
            File.WriteAllText(Path.Combine(directory, fileName), text, Encoding.UTF8);

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="image">The image.</param>
        /// <param name="directory">The directory.</param>
        private static void SaveImage(string fileName, Image image, string directory)
        {
            using (image)
            {
                image.Save(ImagesDirectory(fileName, directory), GetEncoding(fileName));
            }
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>ImageFormat.</returns>
        /// <exception cref="System.ArgumentException">Couldn't get an encoding for +file;file</exception>
        private static ImageFormat GetEncoding(string file) =>
            (Path.GetExtension(file).ToUpperInvariant()) switch
            {
                ".GIF" => ImageFormat.Gif,
                ".PNG" => ImageFormat.Png,
                _ => throw new ArgumentException($"Couldn't get an encoding for {file}", nameof(file)),
            };

        /// <summary>
        /// Imageses the directory.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="directory">The directory.</param>
        /// <returns>System.String.</returns>
        [SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "Because.")]
        private static string ImagesDirectory(string fileName, string directory)
        {
            var images = Path.Combine(directory, "images");
            Directory.CreateDirectory(images);
            return Path.Combine(images, fileName);
        }
    }
}
