namespace StoryQ.Converter.Wpf.Services.Designtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class DesigntimeLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            return new[]
                       {
                           new DesigntimeLanguagePack("English", "GB", "US", "NZ"),
                           new DesigntimeLanguagePack("Brazil", "BR"),
                       };
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks()
        {
            return new[]
                       {
                           new DesigntimeLanguagePack("South African", "ZA")
                       };
        }

     
        internal class DesigntimeLanguagePack : ILocalLanguagePack, IRemoteLanguagePack
        {
            public DesigntimeLanguagePack(string name, params string[] countryCodes)
            {
                Name = name;
                CountryCodes = countryCodes;
            }

            public string Name { get; set; }

            public IEnumerable<string> CountryCodes { get; set; }

            public void BeginDownloadAsync(Action<double> downloadProgress, Action<ILocalLanguagePack> downloadComplete)
            {
                downloadComplete(this);
            }

            public object ParserEntryPoint
            {
                get { return new object(); }
            }
        }
    }
}
