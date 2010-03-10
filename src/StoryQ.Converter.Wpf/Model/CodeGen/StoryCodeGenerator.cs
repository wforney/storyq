using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    class StoryCodeGenerator : ICodeGenerator
    {
        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            var v = fragment.SelfAndAncestors().Reverse();
            bool first = true;
            foreach (var f in v)
            {
                writer.IndentLevel += f.Step.IndentLevel;
                if (first)
                {
                    writer.WriteLine(string.Format("new {0}({1})", f.GetType().Name, CamelContent(f.Step)));
                    first = false;
                }
                else
                {
                    writer.WriteLine(string.Format(".{0}({1})", Camel(f.Step.Prefix), CamelContent(f.Step)));
                }
                writer.IndentLevel -= f.Step.IndentLevel;
            }
        }

        private static string CamelContent(Step n)
        {
            if (!n.IsExecutable)
            {
                return string.Format("\"{0}\"", n.Text);
            }

            List<string> args = new List<string>();
            string s = Regex.Replace(n.Text, "\\$\\S*", match =>
                {
                    args.Add(match.Value.Substring(1));
                    return "_";
                });

            var argLiterals = args.Select(x => Regex.IsMatch(x, "^((true)|(false)|([0-9.]*))$") ? x : '"' + x + '"');

            return string.Join(", ", new[] { Camel(s) }.Concat(argLiterals).ToArray());
        }

        private static string Camel(string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }
    }
}