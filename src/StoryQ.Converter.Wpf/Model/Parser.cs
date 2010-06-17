using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using StoryQ.Infrastructure;

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


                var v = from m in GetMethods(root)
                        where line.StartsWith(m.Name.UnCamel(), IgnoreCase)
                        select m;

                var match = v.FirstOrDefault();
                if (match == null)
                {
                    throw new ParseException(string.Format("expected one of [{1}]; but found '{0}' at line {2}", line, GetMethods(root).Select(x => x.Name.UnCamel()).Join(", "), lineNumber));
                }

                string argument = line.Substring(match.Name.UnCamel().Length).Trim();
                root = match.Invoke(root, new object[] { argument });
                lineNumber++;
            }

            return root;
        }

        public static IEnumerable<MethodInfo> GetMethods(object o)
        {
            return from m in o.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                   where m.ReturnType != typeof(void)
                   where m.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() != null
                   where m.GetParameters().Select(x => x.ParameterType).SequenceEqual(new[] { typeof(string) })
                   orderby m.Name.Length descending
                   select m;
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