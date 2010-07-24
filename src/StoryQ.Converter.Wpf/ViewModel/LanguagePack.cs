using System;
using System.Collections.Generic;
using StoryQ.Converter.Wpf.Services;

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class LanguagePack : ViewModelBase
    {
        double downloadProgress;
        readonly Converter converter;
        readonly IRemoteLanguagePack remoteLanguagePack;
        ILocalLanguagePack localLanguagePack;

        public LanguagePack(Converter converter, ILocalLanguagePack localLanguagePack)
        {
            this.converter = converter;
            this.localLanguagePack = localLanguagePack;
            Text = localLanguagePack.Name;
            CountryCodes = localLanguagePack.CountryCodes;
            downloadProgress = 1;
        }

        public LanguagePack(Converter converter, IRemoteLanguagePack remoteLanguagePack)
        {
            this.converter = converter;
            this.remoteLanguagePack = remoteLanguagePack;
            Text = remoteLanguagePack.Name;
            CountryCodes = remoteLanguagePack.CountryCodes;
            downloadProgress = 0;
        }

        public IEnumerable<string> CountryCodes { get; private set; }

        public string Text { get; private set; }

        public bool IsDownloaded
        {
            get
            {
                return localLanguagePack != null;
            }
        }

        public double DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                downloadProgress = value;
                FirePropertyChanged("DownloadProgress");
            }
        }

        public void SetCurrent()
        {
            if (localLanguagePack != null)
            {
                converter.CurrentParserEntryPoint = localLanguagePack.ParserEntryPoint;
            }
            else
            {
                remoteLanguagePack.BeginDownloadAsync(x => DownloadProgress=x, DownloadComplete);
            }
        }

        void DownloadComplete(ILocalLanguagePack download)
        {
            localLanguagePack = download;
            if(converter.CurrentLanguagePack == this)
            {
                converter.CurrentParserEntryPoint = download.ParserEntryPoint;
            }
            DownloadProgress = 1;
            FirePropertyChanged("IsDownloaded");
            
        }
    }
}