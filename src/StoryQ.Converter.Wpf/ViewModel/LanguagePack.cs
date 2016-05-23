namespace StoryQ.Converter.Wpf.ViewModel
{
    using System;
    using System.Collections.Generic;
    using StoryQ.Converter.Wpf.Services;

    public class LanguagePack : ViewModelBase
    {
        private double downloadProgress;
        private readonly Converter converter;
        private readonly IRemoteLanguagePack remoteLanguagePack;
        private ILocalLanguagePack localLanguagePack;

        public LanguagePack(Converter converter, ILocalLanguagePack localLanguagePack)
        {
            this.converter = converter;
            this.localLanguagePack = localLanguagePack;
            this.Text = localLanguagePack.Name;
            this.CountryCodes = localLanguagePack.CountryCodes;
            this.downloadProgress = 1;
        }

        public LanguagePack(Converter converter, IRemoteLanguagePack remoteLanguagePack)
        {
            this.converter = converter;
            this.remoteLanguagePack = remoteLanguagePack;
            this.Text = remoteLanguagePack.Name;
            this.CountryCodes = remoteLanguagePack.CountryCodes;
            this.downloadProgress = 0;
        }

        public IEnumerable<string> CountryCodes { get; private set; }

        public string Text { get; private set; }

        public bool IsDownloaded => this.localLanguagePack != null;

        public double DownloadProgress
        {
            get { return this.downloadProgress; }
            set
            {
                this.downloadProgress = value;
                this.FirePropertyChanged("DownloadProgress");
            }
        }

        public void SetCurrent()
        {
            if (this.localLanguagePack != null)
            {
                this.converter.CurrentParserEntryPoint = this.localLanguagePack.ParserEntryPoint;
            }
            else
            {
                this.converter.CurrentParserEntryPoint = null;
                this.remoteLanguagePack.BeginDownloadAsync(x => this.DownloadProgress = x, this.DownloadComplete);
            }
        }

        private void DownloadComplete(ILocalLanguagePack download)
        {
            this.localLanguagePack = download;
            if (this.converter.CurrentLanguagePack == this)
            {
                this.converter.CurrentParserEntryPoint = download.ParserEntryPoint;
            }
            this.DownloadProgress = 1;
            this.FirePropertyChanged("IsDownloaded");
            
        }
    }
}