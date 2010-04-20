using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Generates the test method
    /// </summary>
    class TestMethodGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator child;
        private readonly TestFrameworkData testFrameworkData;

        public TestMethodGenerator(ICodeGenerator child, TestFrameworkData testFrameworkData)
        {
            this.child = child;
            this.testFrameworkData = testFrameworkData;
        }

        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
        {
            IStepContainer first = fragments.First();
            string s = Regex.Replace(" " + first.Step.Text, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
            writer.WriteLine("["+testFrameworkData.TestMethodAttribute+"]");
            writer.WriteLine("public void "+s+"()");
            using(writer.CodeBlock())
            {
                child.Generate(fragments, writer);
            }
           
        }
    }
}