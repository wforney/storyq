using System;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    interface ICodeGenerator
    {
        void Generate(FragmentBase fragment, CodeWriter writer);
    }
}