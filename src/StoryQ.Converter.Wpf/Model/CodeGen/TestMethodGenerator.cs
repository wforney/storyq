using System.Linq;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    class TestMethodGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator child;
        private readonly TestFrameworkData testFrameworkData;

        public TestMethodGenerator(ICodeGenerator child, TestFrameworkData testFrameworkData)
        {
            this.child = child;
            this.testFrameworkData = testFrameworkData;
        }

        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            FragmentBase first = fragment.SelfAndAncestors().Last();
            string s = Regex.Replace(" " + first.Step.Text, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
            writer.WriteLine("["+testFrameworkData.TestMethodAttribute+"]");
            writer.WriteLine("public void "+s+"()");
            writer.WriteLine("{");
            writer.IndentLevel++;
            child.Generate(fragment, writer);
            writer.IndentLevel--;
            writer.WriteLine("}");
        }
    }
}