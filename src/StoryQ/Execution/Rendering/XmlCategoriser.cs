namespace StoryQ.Execution.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    internal class XmlCategoriser
    {
        private const string AttributeName = "Name";

        private static readonly Dictionary<string, Func<MethodBase, string>> chain = new Dictionary<string, Func<MethodBase, string>>
            {
                {"Project", info => info.DeclaringType.Assembly.GetName().Name},
                {"Namespace", info => info.DeclaringType.Namespace},
                {"Class", info => info.DeclaringType.Name},
                {"Method", info => info.Name},
            };

        private int exceptionID = 1;
        private readonly XElement rootElement;



        public XmlCategoriser(XElement rootElement)
        {
            this.rootElement = rootElement;
        }

        public XElement GetOrCreateElementForMethodInfo(MethodBase categoriser)
        {
            var e = this.rootElement;
            foreach (var pair in chain)
            {
                var value = pair.Value(categoriser);
                var match = e.Elements(pair.Key).SingleOrDefault(x => value == (string)x.Attribute(AttributeName));
                if (match == null)
                {
                    match = new XElement(pair.Key, new XAttribute(AttributeName, value ?? string.Empty));
                    e.Add(match);
                }
                e = match;
            }
            return e;
        }

        public XmlRenderer GetRenderer(MethodBase categoriser)
        {
            var element = this.GetOrCreateElementForMethodInfo(categoriser);
            return new XmlRenderer(element, this.CreateExceptionID);
        }

        private int CreateExceptionID()
        {
            return this.exceptionID++;
        }
    }
}
