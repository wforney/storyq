namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    using System.Collections.Generic;
    using System.Linq;
    using StoryQ.Infrastructure;

    /// <summary>
    /// Generates the step methods for this story
    /// </summary>
    internal class StepMethodsGenerator : ICodeGenerator
    {
        
        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
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
                using (writer.CodeBlock())
                {
                    writer.WriteLine("throw new NotImplementedException();");
                }
            }
        }

        private static string GetMethodDeclaration(Step step) => MethodBuilder.ParseMethodDeclaration(step.Text).ToMethodDeclaration();
    }
}
