using System;
using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class ClickOnceLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            //var target = typeof(ParserEntryPointAttribute).Assembly.GetCustomAttribute<ParserEntryPointAttribute>().Target;
            yield return new EnglishLanguagePack();
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks()
        {
            //var target = typeof(ParserEntryPointAttribute).Assembly.GetCustomAttribute<ParserEntryPointAttribute>().Target;
            yield break;
        }
    }
}