namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class LocalLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
            foreach (var file in Directory.GetFiles("LanguagePacks", "*.dll"))
            {
                yield return new AssemblyFileLanguagePack(file);
            }
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks() => Enumerable.Empty<IRemoteLanguagePack>();
    }
}