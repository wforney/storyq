using System;
using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Services
{
    public interface ILanguagePackProvider
    {
        IEnumerable<ILanguagePack> GetAvailableLanguagePacks();
    }

    public interface ILanguagePack
    {
        void BeginDownload(Action<ILanguagePack, double> downloadProgress, Action<ILanguagePack> downloadComplete);
        
    }
}