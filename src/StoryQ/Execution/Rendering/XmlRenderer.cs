// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="XmlRenderer.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Execution.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Class XmlRenderer.
    /// </summary>
    /// <seealso cref="StoryQ.Execution.Rendering.IRenderer" />
    internal class XmlRenderer : IRenderer
    {
        /// <summary>
        /// The identifier source
        /// </summary>
        private readonly Func<int> idSource;

        /// <summary>
        /// The receptacle
        /// </summary>
        private readonly XElement receptacle;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRenderer"/> class.
        /// </summary>
        /// <param name="receptacle">The receptacle.</param>
        public XmlRenderer(XElement receptacle)
            : this(receptacle, AutoIncrementFrom(1))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRenderer"/> class.
        /// </summary>
        /// <param name="receptacle">The receptacle.</param>
        /// <param name="idSource">The identifier source.</param>
        public XmlRenderer(XElement receptacle, Func<int> idSource)
        {
            this.receptacle = receptacle;
            this.idSource = idSource;
        }

        /// <summary>
        /// Automatics the increment from.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns>Func&lt;System.Int32&gt;.</returns>
        public static Func<int> AutoIncrementFrom(int start) => () => start++;

        /// <summary>
        /// Renders the results.
        /// </summary>
        /// <param name="results">The results.</param>
        public void Render(IEnumerable<Result> results)
        {
            if (!results.Any())
            {
                return;
            }

            var v = from x in results
                    select new XElement("Result",
                                        x.Exception != null && x.Type == ResultType.Failed
                                            ? new XElement(
                                                "Exception",
                                                           new XAttribute("ID", this.idSource()),
                                                           new XAttribute("Message", x.Exception.Message),
                                                           new XCData(x.Exception.ToString()))
                                            : null,
                                        new XAttribute("IndentLevel", x.IndentLevel),
                                        new XAttribute("Prefix", x.Prefix),
                                        new XAttribute("Text", x.Text),
                                        new XAttribute("Type", x.Type),
                                        x.Tags.Select(tag => new XElement("Tag", tag)));

            this.receptacle.Add(new XElement("Story", new XAttribute("Name", results.First().Text), v));
        }
    }
}
