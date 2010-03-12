using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    internal class TestFrameworkData
    {
        public IEnumerable<string> Imports { set; get; }
        public string TestMethodAttribute { get; set; }
        public string TestClassAttribute { get; set; }
    }
}