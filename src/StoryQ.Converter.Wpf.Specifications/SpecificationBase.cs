namespace StoryQ.Converter.Wpf.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using StoryQ.Infrastructure;

    public abstract class SpecificationBase
    {
        private Feature feature;

        protected SpecificationBase()
        {
            Story s = new Story(GetType().Name.UnCamel());
            feature = DescribeStory(s);
        }

        protected abstract Feature DescribeStory(Story story);

        protected Scenario Scenario => feature.WithScenario(new StackFrame(1).GetMethod().Name.UnCamel());
    }
}
