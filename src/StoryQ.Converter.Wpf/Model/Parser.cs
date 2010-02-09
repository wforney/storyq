using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace StoryQ.Converter.Wpf.Model
{
    public static class Parser
    {
        private const StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;

        public static object Parse(IEnumerable<string> lines, object root)
        {
            int lineNumber = 1;
            foreach (string untrimmedLine in lines)
            {
                string line = untrimmedLine.Trim();

                if (string.IsNullOrEmpty(line)) continue;


                var v = from m in GetOneStringMethods(root)
                        where line.StartsWith(UnCamel(m.Name), IgnoreCase)
                        select m;

                var match = v.FirstOrDefault();
                if (match == null)
                {
                    throw new ParseException(string.Format("expected one of [{1}]; but found '{0}' at line {2}", line, string.Join(", ", GetOneStringMethods(root).Select(x => UnCamel(x.Name)).ToArray()), lineNumber));
                }

                string argument = line.Substring(UnCamel(match.Name).Length).Trim();
                root = match.Invoke(root, new object[] { argument });
                lineNumber++;
            }

            return root;
        }


        public static IEnumerable<MethodInfo> GetOneStringMethods(object o)
        {
            var v = from m in o.GetType().GetMethods()
                    where m.ReturnType != typeof(void)
                    where m.GetParameters().Select(x => x.ParameterType).SequenceEqual(new[] { typeof(string) })
                    select m;

            //we care about the longest method names first
            v = v.OrderByDescending(m => m.Name.Length);
            return v;
        }

        public static string UnCamel(string name)
        {
            return Regex.Replace(name, "[A-Z_]", x => " " + x.Value.ToLowerInvariant()).Trim();
        }

        public class ParseException : ArgumentException
        {
            public ParseException(string message)
                : base(message)
            {
            }
        }
    }
}