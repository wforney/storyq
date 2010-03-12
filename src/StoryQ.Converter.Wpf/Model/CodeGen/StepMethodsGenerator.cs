using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    class StepMethodsGenerator : ICodeGenerator
    {
        private static Dictionary<string, string> literalTypes = new Dictionary<string, string>()
            {
                {"(true)|(false)", "bool"},
                {@"(0x?)?\d+", "int"},
                {@"[0-9.]+", "float"},
            };

        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            var methods = fragment
                .SelfAndAncestors()
                .Select(x => x.Step)
                .Reverse()
                .Where(x => x.IsExecutable)
                .Select(x => GetMethodDeclaration(x))
                .Distinct();


            foreach (var step in methods)
            {
                writer.WriteLine("");
                writer.WriteLine("private void " + step);
                writer.WriteLine("{");
                writer.IndentLevel++;
                writer.WriteLine("throw new NotImplementedException();");
                writer.IndentLevel--;
                writer.WriteLine("}");

            }
        }

        private static string GetMethodDeclaration(Step step)
        {
            string text = step.Text;
            List<string> argValues = new List<string>();
            string methodName = Regex.Replace(text, "\\$\\S*", match =>
            {
                argValues.Add(match.Value.Substring(1));
                return "_";
            });

            var argLiterals = argValues.Select((x, i) => GetArgType(x) + " arg" + (i + 1)).ToArray();


            return Camel(methodName) + "(" + string.Join(", ", argLiterals) + ")";
        }

        private static string GetArgType(string s)
        {
            foreach (var p in literalTypes)
            {
                if (Regex.IsMatch(s, "^" + p.Key + "$"))
                {
                    return p.Value;
                }
            }
            return "string";
        }

        private static string Camel(string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
        }
    }
}
