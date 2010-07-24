using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Linq;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class FileSavingService : IFileSavingService
    {
        readonly IErrorhandler errorhandler;
        FolderBrowserDialog dialog;

        public FileSavingService(IErrorhandler errorhandler)
        {
            this.errorhandler = errorhandler;

            dialog = new FolderBrowserDialog
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                ShowNewFolderButton = true
            };
        }

        public string PromptForDirectory(string message)
        {
            dialog.Description = message;

            var hwndSource = (System.Windows.Interop.HwndSource)PresentationSource.FromVisual(System.Windows.Application.Current.MainWindow);
            DialogResult result = hwndSource == null ? dialog.ShowDialog() : dialog.ShowDialog(new OldWindow(hwndSource.Handle));

            return result == DialogResult.OK ? dialog.SelectedPath : null;
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
                errorhandler.HandleExpectedError(e);
            }
        }

        static IEnumerable<string> SearchForFiles(string searchPattern)
        {
            return Directory.GetFiles(Environment.CurrentDirectory, searchPattern, SearchOption.AllDirectories);
        }

        class OldWindow : IWin32Window
        {
            public OldWindow(IntPtr handle)
            {
                Handle = handle;
            }

            public IntPtr Handle { get; private set; }
        }
    }
}