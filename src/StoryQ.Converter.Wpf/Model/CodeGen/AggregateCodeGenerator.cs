using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
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

        public void Generate(FragmentBase fragment, CodeWriter writer)
        {
            foreach (var child in children)
            {
                child.Generate(fragment, writer);
            }
        }
    }
}
