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
                    this.locals.Add(new AssemblyFileLanguagePack(GroupNameToDllLocation(g)));
                }
                else
                {
                    this.remotes.Add(new RemotePack(g));
                }
            }
        }

        private static string GroupNameToDllLocation(string group) => string.Format("LanguagePacks\\StoryQ.{0}.dll", group);

        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
            foreach (var localProvider in this.locals)
            {
                yield return localProvider;
            }
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks() => this.remotes.Cast<IRemoteLanguagePack>();

        private class RemotePack : IRemoteLanguagePack
        {
            private readonly string groupName;
            private Downloader downloader;

            public RemotePack(string groupName)
            {
                this.groupName = groupName;
            }

            public string Name => this.groupName;

            public IEnumerable<string> CountryCodes
            {
                get { yield return this.Name.Split('-').Last(); }
            }

            public void BeginDownloadAsync(Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete)
            {
                if (this.downloader == null)
                {
                    this.downloader = new Downloader(this.groupName, downloadProgress, downloadComplete);
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
                    d.DownloadFileGroupProgressChanged += this.CurrentDeploymentOnDownloadFileGroupProgressChanged;
                    d.DownloadFileGroupCompleted += this.CurrentDeploymentOnDownloadFileGroupCompleted;
                    d.DownloadFileGroupAsync(groupName);
                }

                private void CurrentDeploymentOnDownloadFileGroupCompleted(object sender, DownloadFileGroupCompletedEventArgs args)
                {
                    if (args.Group == this.groupName)
                    {
                        this.downloadComplete(new AssemblyFileLanguagePack(GroupNameToDllLocation(this.groupName)));
                    }
                    var d = ApplicationDeployment.CurrentDeployment;
                    d.DownloadFileGroupProgressChanged -= this.CurrentDeploymentOnDownloadFileGroupProgressChanged;
                    d.DownloadFileGroupCompleted -= this.CurrentDeploymentOnDownloadFileGroupCompleted;
                }

                private void CurrentDeploymentOnDownloadFileGroupProgressChanged(object sender, DeploymentProgressChangedEventArgs args)
                {
                    if (args.Group == this.groupName)
                    {
                        this.downloadProgress((double)args.BytesCompleted / args.BytesTotal);
                    }
                }
            }
        }
    }
}