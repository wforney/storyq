using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace StoryQ.Execution.Rendering
{
    internal class XmlRenderer : IRenderer
    {
        private readonly XElement receptacle;
        private readonly Func<int> idSource;

        public XmlRenderer(XElement receptacle) : this(receptacle, AutoIncrementFrom(1)) { }

        public XmlRenderer(XElement receptacle, Func<int> idSource)
        {
            this.receptacle = receptacle;
            this.idSource = idSource;
        }

        public void Render(IEnumerable<Result> results)
        {
            if (!results.Any())
            {
                return;
            }

            var v = from x in results
                    select new XElement("Result",
                                        x.Exception != null && x.Type == ResultType.Failed
                                            ? new XElement("Exception",
                                                           new XAttribute("ID", idSource()),
                                                           new XAttribute("Message", x.Exception.Message),
                                                           new XCData(x.Exception.ToString()))
                                            : null,
                                        new XAttribute("IndentLevel", x.IndentLevel),
                                        new XAttribute("Prefix", x.Prefix),
                                        new XAttribute("Text", x.Text),
                                        new XAttribute("Type", x.Type));

            receptacle.Add(new XElement("Story", new XAttribute("Name", results.First().Text), v));
        }

        public static Func<int> AutoIncrementFrom(int start)
        {
            return () => start++;
        }
    }
}
