// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="XmlCategoriser.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Execution.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Class XmlCategoriser.
    /// </summary>
    internal class XmlCategoriser
    {
        /// <summary>
        /// The attribute name
        /// </summary>
        private const string AttributeName = "Name";

        /// <summary>
        /// The chain
        /// </summary>
        private static readonly Dictionary<string, Func<MethodBase, string>> chain = new Dictionary<string, Func<MethodBase, string>>
            {
                { "Project", info => info.DeclaringType.Assembly.GetName().Name},
                { "Namespace", info => info.DeclaringType.Namespace},
                { "Class", info => info.DeclaringType.Name},
                { "Method", info => info.Name},
            };

        /// <summary>
        /// The exception identifier
        /// </summary>
        private int exceptionID = 1;
        /// <summary>
        /// The root element
        /// </summary>
        private readonly XElement rootElement;



        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCategoriser"/> class.
        /// </summary>
        /// <param name="rootElement">The root element.</param>
        public XmlCategoriser(XElement rootElement)
        {
            this.rootElement = rootElement;
        }

        /// <summary>
        /// Gets the or create element for method information.
        /// </summary>
        /// <param name="categoriser">The categoriser.</param>
        /// <returns>XElement.</returns>
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

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <param name="categoriser">The categoriser.</param>
        /// <returns>XmlRenderer.</returns>
        public XmlRenderer GetRenderer(MethodBase categoriser)
        {
            var element = this.GetOrCreateElementForMethodInfo(categoriser);
            return new XmlRenderer(element, this.CreateExceptionID);
        }

        /// <summary>
        /// Creates the exception identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int CreateExceptionID() => this.exceptionID++;
    }
}
