using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class LocalLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks()
        {
            return Enumerable.Empty<IRemoteLanguagePack>();
        }
    }
}