using System.Collections.Generic;
using System.Linq;
using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Calls each child generator
    /// </summary>
    class AggregateCodeGenerator : ICodeGenerator
    {
        readonly IEnumerable<ICodeGenerator> children;

        public AggregateCodeGenerator(params ICodeGenerator[] children)
            : this(children.AsEnumerable())
        {
        }

        public AggregateCodeGenerator(IEnumerable<ICodeGenerator> children)
        {
            this.children = children;
        }

        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
        {
            foreach (ICodeGenerator child in children)
            {
                child.Generate(fragments, writer);
            }
        }
    }
}