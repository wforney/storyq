namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    using System.Collections.Generic;
    using System.Linq;
    using StoryQ.Infrastructure;

    /// <summary>
    /// Calls each child generator
    /// </summary>
    internal class AggregateCodeGenerator : ICodeGenerator
    {
        private readonly IEnumerable<ICodeGenerator> children;

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