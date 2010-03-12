using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    class StoryCodeGenerator : ICodeGenerator
    {
        private readonly bool indentSteps;

        public StoryCodeGenerator(bool indentSteps)
        {
            this.indentSteps = indentSteps;
        }

        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            foreach (var f in fragment.SelfAndAncestors().Reverse())
            {
                bool first = f.Parent == null;

                var indentLevel = indentSteps ? f.Step.IndentLevel : (first ? 0 : 1);
                
                if (indentLevel == 3)
                {
                    writer.WriteLine("");
                }

                writer.IndentLevel += indentLevel;
                if (first)
                {
                    writer.WriteLine(string.Format("new {0}({1})", f.GetType().Name, CreateStepArgs(f.Step)));
                }
                else
                {
                    writer.WriteLine(string.Format(".{0}({1})", Camel(f.Step.Prefix), CreateStepArgs(f.Step)));
                }
                

                writer.IndentLevel -= indentLevel;
            }
            writer.IndentLevel++;
            writer.WriteLine(".Execute();");
            writer.IndentLevel--;
        }

        private static string CreateStepArgs(Step n)
        {
            return n.IsExecutable ? StringToMethodCall(n.Text) : '"' + n.Text + '"';
        }

        private static string StringToMethodCall(string text)
        {
            List<string> args = new List<string>();
            string methodName = Regex.Replace(text, "\\$\\S*", match =>
                {
                    args.Add(match.Value.Substring(1));
                    return "_";
                });

            var argLiterals = args
                .Select(x => Regex.IsMatch(x, "^((true)|(false)|([0-9.]*))$") ? x : '"' + x + '"')
                .Aggregate(new StringBuilder(), (sb, s) => sb.Append(", " + s));

            return Camel(methodName) + argLiterals;
        }

        private static string Camel(string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }
    }
}