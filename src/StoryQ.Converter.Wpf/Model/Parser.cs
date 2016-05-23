namespace StoryQ.Converter.Wpf.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using StoryQ.Infrastructure;

    public static class Parser
    {
        private const StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;

        public static object Parse(IEnumerable<string> lines, object root)
        {
            var lineNumber = 1;
            foreach (string untrimmedLine in lines)
            {
                string line = untrimmedLine.Trim();

                if (string.IsNullOrEmpty(line)) continue;


                var v = from m in GetMethods(root)
                        from s in GetNames(m)
                        orderby s.Length descending 
                        where line.StartsWith(s, IgnoreCase)
                        select new {Method = m, Name = s};

                var match = v.FirstOrDefault();
                if (match == null)
                {
                    string names = GetMethods(root).SelectMany(GetNames).Join(", ");
                    string s = string.Format("expected one of [{1}]\nbut found '{0}'\nat line {2}", line, names, lineNumber);
                    throw new ParseException(s);
                }

                string argument = line.Substring(match.Name.Length).Trim();
                root = match.Method.Invoke(root, new object[] { argument });
                lineNumber++;
            }

            return root;
        }

        private static IEnumerable<string> GetNames(MethodInfo methodInfo)
        {
            yield return methodInfo.Name.UnCamel();
            foreach (var a in Attribute.GetCustomAttributes(methodInfo).OfType<AliasAttribute>())
            {
                yield return a.Alias;
            }
        }

        public static IEnumerable<MethodInfo> GetMethods(object o) => from m in o.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                                                      where m.ReturnType != typeof(void)
                                                                      where m.GetCustomAttribute<DescriptionAttribute>() != null
                                                                      where m.GetParameters().Select(x => x.ParameterType).SequenceEqual(new[] { typeof(string) })
                                                                      orderby m.Name.Length descending
                                                                      select m;

        public class ParseException : ArgumentException
        {
            public ParseException(string message)
                : base(message)
            {
            }
        }
    }

}