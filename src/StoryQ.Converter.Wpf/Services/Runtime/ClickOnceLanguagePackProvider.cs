using System;
using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class ClickOnceLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks()
        {
            yield break;
        }
    }
}