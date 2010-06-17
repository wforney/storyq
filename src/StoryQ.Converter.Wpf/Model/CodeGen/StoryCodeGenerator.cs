using System.Collections.Generic;
using System.Text.RegularExpressions;

using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Generates the code that constructs a StoryQ story
    /// </summary>
    class StoryCodeGenerator : ICodeGenerator
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
                bool first = f.Parent == null;

                var indentLevel = indentSteps ? f.Step.IndentLevel : (first ? 0 : 1);
                
                if (indentLevel == 3)
                {
                    writer.WriteLine("");
                }

                using(writer.IncreaseIndent(indentLevel))
                {
                    string s = first
                                   ? string.Format("new {0}({1})", f.GetType().Name, CreateStepArgs(f.Step))
                                   : string.Format(".{0}({1})", Camel(f.Step.Prefix), CreateStepArgs(f.Step));
                    writer.WriteLine(s);
                }
            }

            using (writer.IncreaseIndent(1))
            {
                writer.WriteLine(".Execute();");
            }
        }

        private static string CreateStepArgs(Step n)
        {
            return n.IsExecutable ? StringToMethodCall(n.Text) : '"' + n.Text + '"';
        }

        private static string StringToMethodCall(string text)
        {
            return MethodBuilder.ParseMethodDeclaration(text).ToStepParameters();
        }

        private static string Camel(string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }
    }
}