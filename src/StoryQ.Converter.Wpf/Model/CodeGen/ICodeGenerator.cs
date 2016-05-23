namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    using System.Collections.Generic;
    using StoryQ.Infrastructure;

    /// <summary>
    /// Implementing classes will be capable of generating code from a collection of FragmentBases
    /// </summary>
    internal interface ICodeGenerator
    {
        void Generate(IEnumerable<IStepContainer> fragments, CodeWriter writer);
    }
}