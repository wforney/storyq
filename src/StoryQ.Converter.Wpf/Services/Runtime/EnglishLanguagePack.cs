namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System.Collections.Generic;
    using StoryQ.Infrastructure;

    internal class EnglishLanguagePack : ILocalLanguagePack
    {
        public string Name => "English";

        public IEnumerable<string> CountryCodes => "GB,US,NZ,AU,CA,IE,PH,TT,ZA".Split(',');

        public object ParserEntryPoint => new StoryQEntryPoints();
    }
}