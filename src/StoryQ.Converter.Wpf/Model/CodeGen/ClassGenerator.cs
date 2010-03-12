using System.Linq;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    class ClassGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator child;
        private readonly TestFrameworkData testFrameworkData;

        public ClassGenerator(ICodeGenerator child, TestFrameworkData testFrameworkData)
        {
            this.child = child;
            this.testFrameworkData = testFrameworkData;
        }

        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            var imports = new[] { "System", "StoryQ" }.Concat(testFrameworkData.Imports);
            foreach (var import in imports)
            {
                writer.WriteLine("using "+import+";");
            }
            writer.WriteLine("");
            writer.WriteLine("["+testFrameworkData.TestClassAttribute+"]");
            writer.WriteLine("public class StoryQTestClass");
            writer.WriteLine("{");
            writer.IndentLevel++;
            child.Generate(fragment, writer);
            writer.IndentLevel--;
            writer.WriteLine("}");

        }
    }
}