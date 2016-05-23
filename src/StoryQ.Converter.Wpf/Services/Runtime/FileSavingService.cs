namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;

    internal class FileSavingService : IFileSavingService
    {
        private readonly IErrorhandler errorhandler;
        private FolderBrowserDialog dialog;

        public FileSavingService(IErrorhandler errorhandler)
        {
            this.errorhandler = errorhandler;

            this.dialog = new FolderBrowserDialog
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                ShowNewFolderButton = true
            };
        }

        public string PromptForDirectory(string message)
        {
            this.dialog.Description = message;

            var hwndSource = (System.Windows.Interop.HwndSource)PresentationSource.FromVisual(System.Windows.Application.Current.MainWindow);
            DialogResult result = hwndSource == null ? this.dialog.ShowDialog() : this.dialog.ShowDialog(new OldWindow(hwndSource.Handle));

            return result == DialogResult.OK ? this.dialog.SelectedPath : null;
        }

        public void CopyLibFiles(string targetDirectory)
        {
            try
            {
                var files = SearchForFiles("*.dll").Concat(SearchForFiles("*.xml"));
                foreach (var file in files)
                {
                    string destFileName = Path.Combine(targetDirectory, Path.GetFileName(file));
                    File.Copy(file, destFileName, true);
                }
                Process.Start(targetDirectory);
            }
            catch (IOException e)
            {
                this.errorhandler.HandleExpectedError(e);
            }
        }

        private static IEnumerable<string> SearchForFiles(string searchPattern) => Directory.GetFiles(Environment.CurrentDirectory, searchPattern, SearchOption.AllDirectories);

        private class OldWindow : IWin32Window
        {
            public OldWindow(IntPtr handle)
            {
                this.Handle = handle;
            }

            public IntPtr Handle { get; private set; }
        }
    }
}