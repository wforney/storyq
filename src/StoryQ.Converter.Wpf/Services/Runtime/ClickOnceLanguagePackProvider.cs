namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    internal class ClickOnceLanguagePackProvider : ILanguagePackProvider
    {
        private List<AssemblyFileLanguagePack> locals = new List<AssemblyFileLanguagePack>();
        private List<RemotePack> remotes = new List<RemotePack>();
        
        public ClickOnceLanguagePackProvider()
        {
            ApplicationDeployment d = ApplicationDeployment.CurrentDeployment;

            var doc = XDocument.Load(Assembly.GetEntryAssembly().Location + ".manifest");
            var ns = XNamespace.Get("urn:schemas-microsoft-com:asm.v2");
            var files = doc.Root.Descendants(ns + "file");
            var groups = files.Where(x => x.Attribute("group") != null).Select(x => x.Attribute("group").Value).Distinct().OrderBy(x => x.ToLowerInvariant());

            foreach (var g in groups)
            {
                if (d.IsFileGroupDownloaded(g))
                {
                    locals.Add(new AssemblyFileLanguagePack(GroupNameToDllLocation(g)));
                }
                else
                {
                    remotes.Add(new RemotePack(g));
                }
            }
        }

        private static string GroupNameToDllLocation(string group) => string.Format("LanguagePacks\\StoryQ.{0}.dll", group);

        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
            foreach (var localProvider in locals)
            {
                yield return localProvider;
            }
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks() => remotes.Cast<IRemoteLanguagePack>();

        private class RemotePack : IRemoteLanguagePack
        {
            private readonly string groupName;
            private Downloader downloader;

            public RemotePack(string groupName)
            {
                this.groupName = groupName;
            }

            public string Name => groupName;

            public IEnumerable<string> CountryCodes
            {
                get { yield return Name.Split('-').Last(); }
            }

            public void BeginDownloadAsync(Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete)
            {
                if (downloader == null)
                {
                    downloader = new Downloader(groupName, downloadProgress, downloadComplete);
                }
            }

            private class Downloader
            {
                private readonly string groupName;
                private readonly Action<double> downloadProgress;
                private readonly Action<ILocalLanguagePack> downloadComplete;

                public Downloader(string groupName, Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete)
                {
                    this.groupName = groupName;
                    this.downloadProgress = downloadProgress;
                    this.downloadComplete = downloadComplete;

                    var d = ApplicationDeployment.CurrentDeployment;
                    d.DownloadFileGroupProgressChanged += CurrentDeploymentOnDownloadFileGroupProgressChanged;
                    d.DownloadFileGroupCompleted += CurrentDeploymentOnDownloadFileGroupCompleted;
                    d.DownloadFileGroupAsync(groupName);
                }

                private void CurrentDeploymentOnDownloadFileGroupCompleted(object sender, DownloadFileGroupCompletedEventArgs args)
                {
                    if (args.Group == groupName)
                    {
                        downloadComplete(new AssemblyFileLanguagePack(GroupNameToDllLocation(groupName)));
                    }
                    var d = ApplicationDeployment.CurrentDeployment;
                    d.DownloadFileGroupProgressChanged -= CurrentDeploymentOnDownloadFileGroupProgressChanged;
                    d.DownloadFileGroupCompleted -= CurrentDeploymentOnDownloadFileGroupCompleted;
                }

                private void CurrentDeploymentOnDownloadFileGroupProgressChanged(object sender, DeploymentProgressChangedEventArgs args)
                {
                    if (args.Group == groupName)
                    {
                        downloadProgress((double)args.BytesCompleted / args.BytesTotal);
                    }
                }
            }
        }
    }
}