using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// stores the appropriate strings for each test framework
    /// </summary>
    internal class TestFrameworkData
    {
        public IEnumerable<string> Imports { set; get; }
        public string TestMethodAttribute { get; set; }
        public string TestClassAttribute { get; set; }
    }
}