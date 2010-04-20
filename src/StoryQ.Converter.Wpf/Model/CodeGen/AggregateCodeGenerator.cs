using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Calles each child generator
    /// </summary>
    class AggregateCodeGenerator:ICodeGenerator
    {
        readonly IEnumerable<ICodeGenerator> children;

        public AggregateCodeGenerator(params ICodeGenerator[] children)
            :this(children.AsEnumerable())
        {
        }

        public AggregateCodeGenerator(IEnumerable<ICodeGenerator> children)
        {
            this.children = children;
        }

        public void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer)
        {
            foreach (var child in children)
            {
                child.Generate(fragments, writer);
            }
        }
    }
}
