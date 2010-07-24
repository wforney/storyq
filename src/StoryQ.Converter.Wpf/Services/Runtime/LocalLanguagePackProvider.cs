using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class LocalLanguagePackProvider : ILanguagePackProvider
    {
        public IEnumerable<ILocalLanguagePack> GetLocalLanguagePacks()
        {
            yield return new EnglishLanguagePack();
            foreach (var file in Directory.GetFiles("LanguagePacks", "*.dll"))
            {
                yield return new AssemblyFile(file);
            }
        }

        public IEnumerable<IRemoteLanguagePack> GetRemoteLanguagePacks()
        {
            return Enumerable.Empty<IRemoteLanguagePack>();
        }

        class AssemblyFile : ILocalLanguagePack
        {
            readonly string file;
            object parserEntryPoint;

            public AssemblyFile(string file)
            {
                this.file = file;
            }

            public string Name
            {
                get { return Path.GetFileNameWithoutExtension(file).Split('.').Last(); }
            }

            public IEnumerable<string> CountryCodes
            {
                get { yield return Name.Split('-').Last(); }
            }

            public object ParserEntryPoint
            {
                get
                {
                    //lazy for launch speed
                    return parserEntryPoint ?? (parserEntryPoint = BuildEntryPoint());
                }
            }

            object BuildEntryPoint()
            {

                var type = Assembly.LoadFile(Path.GetFullPath(file)).GetCustomAttribute<ParserEntryPointAttribute>().Target;
                return Activator.CreateInstance(type);
            }
        }
    }
}