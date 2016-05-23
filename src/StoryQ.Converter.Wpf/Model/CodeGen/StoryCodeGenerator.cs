namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using StoryQ.Infrastructure;

    /// <summary>
    /// Generates the code that constructs a StoryQ story
    /// </summary>
    internal class StoryCodeGenerator : ICodeGenerator
    {
        private readonly bool indentSteps;

        public StoryCodeGenerator(bool indentSteps)
        {
            this.indentSteps = indentSteps;
        }

        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
        {
            foreach (IStepContainer f in fragments)
            {
                var first = f.Parent == null;

                var indentLevel = this.indentSteps ? f.Step.IndentLevel : (first ? 0 : 1);
                
                if (indentLevel == 3)
                {
                    writer.WriteLine("");
                }

                using (writer.IncreaseIndent(indentLevel))
                {
                    string s = first
                                   ? string.Format("new {0}({1})", f.GetType().Name, CreateStepArgs(f.Step))
                                   : string.Format(".{0}({1})", f.Step.Prefix.Camel(), CreateStepArgs(f.Step));
                    writer.WriteLine(s);
                }
            }

            using (writer.IncreaseIndent(1))
            {
                writer.WriteLine(".Execute();");
            }
        }

        private static string CreateStepArgs(Step n) => n.IsExecutable ? StringToMethodCall(n.Text) : '"' + n.Text + '"';

        private static string StringToMethodCall(string text) => MethodBuilder.ParseMethodDeclaration(text).ToStepParameters();
    }
}