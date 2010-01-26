using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering
{
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

        internal XElement GetOrCreateElementForMethodInfo(MethodBase categoriser)
        {
            XElement e = rootElement;
            foreach (var pair in chain)
            {
                string value = pair.Value(categoriser);
                XElement match = e.Elements(pair.Key).SingleOrDefault(x => value == (string)x.Attribute(AttributeName));
                if (match == null)
                {
                    match = new XElement(pair.Key, new XAttribute(AttributeName, value));
                    e.Add(match);
                }
                e = match;
            }
            return e;
        }

        public XmlRenderer GetRenderer(MethodBase categoriser)
        {
            XElement element = GetOrCreateElementForMethodInfo(categoriser);
            return new XmlRenderer(element, CreateExceptionID);
        }

        private int CreateExceptionID()
        {
            return exceptionID++;
        }
    }
}
