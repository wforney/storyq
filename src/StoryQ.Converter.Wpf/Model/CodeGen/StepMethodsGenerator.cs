using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Generates the step methods for this story
    /// </summary>
    class StepMethodsGenerator : ICodeGenerator
    {
        

        public void Generate(IEnumerable<FragmentBase> fragments, CodeWriter writer)
        {
            var methods = fragments
                .Select(x => x.Step)
                .Where(x => x.IsExecutable)
                .Select(x => GetMethodDeclaration(x))
                .Distinct();


            foreach (var step in methods)
            {
                writer.WriteLine("");
                writer.WriteLine("private void " + step);
                using(writer.CodeBlock())
                {
                    writer.WriteLine("throw new NotImplementedException();");
                }
            }
        }

        private static string GetMethodDeclaration(Step step)
        {
            return MethodBuilder.ParseMethodDeclaration(step.Text).ToMethodDeclaration();
        }
    }
}
