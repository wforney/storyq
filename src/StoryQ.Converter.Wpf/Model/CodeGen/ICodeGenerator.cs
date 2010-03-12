using System;
using System.Collections.Generic;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Implementing classes will be capable of generating code from a collection of FragmentBases
    /// </summary>
    interface ICodeGenerator
    {
        void Generate(IEnumerable<FragmentBase> fragments, CodeWriter writer);
    }
}