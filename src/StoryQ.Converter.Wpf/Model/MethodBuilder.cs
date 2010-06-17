using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model
{
    public static class MethodBuilder
    {
        private static readonly Dictionary<string, string> literalTypes = new Dictionary<string, string>
            {
                {"(true)|(false)", "bool"},
                {@"(0x?)?\d+", "int"},
                {@"[0-9.]+", "float"},
            };

        public static Method ParseMethodDeclaration(string text)
        {
            List<Parameter> args = new List<Parameter>();
            string methodName = Regex.Replace(text, @"\$(?:(?<name>[a-z]\w*):)?(?<value>\S+)|{(?:(?<name>[a-z]\w*):)?(?<value>[^}]+)}", match =>
            {
                string value = match.Groups["value"].Value;
                string name = match.Groups["name"].Value;
                if(string.IsNullOrEmpty(name))
                {
                    name = "arg" + (args.Count + 1);
                }
                string type = GetArgType(ref value);
                args.Add(new Parameter(type, name, value));
                return "_";
            });
            return new Method(methodName, args);
        }

        private static string GetArgType(ref string value)
        {
            foreach (var p in literalTypes)
            {
                if (Regex.IsMatch(value, "^" + p.Key + "$"))
                {
                    return p.Value;
                }
            }
            DateTime result;
            if(DateTime.TryParse(value, out result))
            {
                value = "DateTime.Parse(\"" + value + "\")";

                return "DateTime";
            }
            value = '"' + value + '"';
            return "string";
        }

        public class Method
        {
            public Method(string name, IEnumerable<Parameter> parameters)
            {
                Name = name;
                Parameters = parameters;
            }

            public string Name { get; private set; }
            public IEnumerable<Parameter> Parameters { get; private set; }

            public string ToMethodDeclaration()
            {
                return Name.Camel() + "(" + Parameters.Select(x => x.Type + " " + x.Name).Join(", ") + ")";
            }

            public string ToStepParameters()
            {
                return Name.Camel() + Parameters.Select(x => ", " + x.LiteralValue).Join("");
            }
        }

        public class Parameter
        {
            public Parameter(string type, string name, string literalValue)
            {
                Type = type;
                Name = name;
                LiteralValue = literalValue;
            }

            public string Type { get; private set; }
            public string Name { get; private set; }
            public string LiteralValue { get; private set; }
        }
    }
}
