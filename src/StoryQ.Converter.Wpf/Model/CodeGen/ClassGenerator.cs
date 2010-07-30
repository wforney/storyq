using System.Collections.Generic;
using System.Linq;
using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Generates the class definition
    /// </summary>
    class ClassGenerator : ICodeGenerator
    {
        private readonly ICodeGenerator child;
        private readonly TestFrameworkData testFrameworkData;

        public ClassGenerator(ICodeGenerator child, TestFrameworkData testFrameworkData)
        {
            this.child = child;
            this.testFrameworkData = testFrameworkData;
        }

        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
        {
            var imports = new[] { "System", "StoryQ" }.Concat(testFrameworkData.Imports);
            foreach (var import in imports)
            {
                writer.WriteLine("using "+import+";");
            }
            writer.WriteLine("");
            writer.WriteLine("["+testFrameworkData.TestClassAttribute+"]");
            writer.WriteLine("public class StoryQTestClass");
            using(writer.CodeBlock())
            {
                child.Generate(fragments, writer);
            }

        }
    }
}