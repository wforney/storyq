namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System.Collections.Generic;
    using StoryQ.Infrastructure;

    class EnglishLanguagePack : ILocalLanguagePack
    {
        public string Name
        {
            get { return "English"; }
        }

        public IEnumerable<string> CountryCodes
        {
            get { return "GB,US,NZ,AU,CA,IE,PH,TT,ZA".Split(','); }
        }

        public object ParserEntryPoint
        {
            get { return new StoryQEntryPoints(); }
        }
    }
}