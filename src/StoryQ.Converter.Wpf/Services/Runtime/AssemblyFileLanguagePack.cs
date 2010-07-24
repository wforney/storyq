using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using StoryQ.Infrastructure;
using Enumerable = System.Linq.Enumerable;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class AssemblyFileLanguagePack : ILocalLanguagePack
    {
        readonly string file;
        object parserEntryPoint;

        public AssemblyFileLanguagePack(string file)
        {
            this.file = file;
        }

        public string Name
        {
            get { return Enumerable.Last<string>(Path.GetFileNameWithoutExtension(file).Split('.')); }
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

            var type = Utilities.GetCustomAttribute<ParserEntryPointAttribute>(Assembly.LoadFile(Path.GetFullPath(file))).Target;
            return Activator.CreateInstance(type);
        }
    }
}