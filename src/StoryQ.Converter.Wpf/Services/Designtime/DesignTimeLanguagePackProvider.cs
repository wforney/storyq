namespace StoryQ.Converter.Wpf.Services.Designtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class DesigntimeLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks() => new[]
                       {
                           new DesigntimeLanguagePack("English", "GB", "US", "NZ"),
                           new DesigntimeLanguagePack("Brazil", "BR"),
                       };

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks() => new[]
                       {
                           new DesigntimeLanguagePack("South African", "ZA")
                       };

        internal class DesigntimeLanguagePack : ILocalLanguagePack, IRemoteLanguagePack
        {
            public DesigntimeLanguagePack(string name, params string[] countryCodes)
            {
                this.Name = name;
                this.CountryCodes = countryCodes;
            }

            public string Name { get; set; }

            public IEnumerable<string> CountryCodes { get; set; }

            public void BeginDownloadAsync(Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete)
            {
                downloadComplete(this);
            }

            public object ParserEntryPoint => new object();
        }
    }
}
