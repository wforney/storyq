namespace StoryQ.Converter.Wpf.Services
{
    using System;
    using System.Collections.Generic;

    public interface ILanguagePackProvider
    {
        IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks();
        IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks();
    }

    public interface ILanguagePack
    {
        string Name { get; }
        IEnumerable<string> CountryCodes { get; }
    }

    public interface ILocalLanguagePack : ILanguagePack
    {
        object ParserEntryPoint { get; }
    }

    public interface IRemoteLanguagePack:ILanguagePack
    {
        void BeginDownloadAsync(Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete);
    }
}