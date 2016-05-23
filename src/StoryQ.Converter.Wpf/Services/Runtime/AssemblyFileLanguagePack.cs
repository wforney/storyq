namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using StoryQ.Infrastructure;
    using Enumerable = System.Linq.Enumerable;

    internal class AssemblyFileLanguagePack : ILocalLanguagePack
    {
        private readonly string file;
        private object parserEntryPoint;

        public AssemblyFileLanguagePack(string file)
        {
            this.file = file;
        }

        public string Name => Enumerable.Last<string>(Path.GetFileNameWithoutExtension(this.file).Split('.'));

        public IEnumerable<string> CountryCodes
        {
            get { yield return this.Name.Split('-').Last(); }
        }

        public object ParserEntryPoint => this.parserEntryPoint ?? (this.parserEntryPoint = this.BuildEntryPoint());

        private object BuildEntryPoint()
        {

            var type = Utilities.GetCustomAttribute<ParserEntryPointAttribute>(Assembly.LoadFile(Path.GetFullPath(this.file))).Target;
            return Activator.CreateInstance(type);
        }
    }
}