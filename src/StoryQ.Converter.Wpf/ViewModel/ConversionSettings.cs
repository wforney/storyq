namespace StoryQ.Converter.Wpf.ViewModel
{
    using System.Collections.Generic;
    using StoryQ.Converter.Wpf.Model.CodeGen;

    public class ConversionSettings : ViewModelBase
    {
        private GenerationLevel level = GenerationLevel.Story;
        private TestFramework targetTestFramework = TestFramework.NUnit;
        private bool specialIndentation = true;

        private readonly Dictionary<TestFramework, TestFrameworkData> testFrameworks = new Dictionary<TestFramework, TestFrameworkData>
            {
                {TestFramework.MSTest, new TestFrameworkData
                    {
                        Imports = new[]{"Microsoft.VisualStudio.TestTools.UnitTesting"}, 
                        TestClassAttribute = "TestClass", 
                        TestMethodAttribute = "TestMethod"
                    }},
                {TestFramework.NUnit, new TestFrameworkData
                    {
                        Imports = new[]{"NUnit.Framework"}, 
                        TestClassAttribute = "TestFixture", 
                        TestMethodAttribute = "Test"
                    }}
            };

        public GenerationLevel Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                FirePropertyChanged("Level");
            }
        }

        public TestFramework TargetTestFramework
        {
            get
            {
                return targetTestFramework;
            }
            set
            {
                targetTestFramework = value;
                FirePropertyChanged("TargetTestFramework");
            }
        }

        public bool SpecialIndentation
        {
            get
            {
                return specialIndentation;
            }
            set
            {
                specialIndentation = value;
                FirePropertyChanged("SpecialIndentation");
            }
        }

        internal ICodeGenerator GetCodeGenerator()
        {
            ICodeGenerator gen = new StoryCodeGenerator(SpecialIndentation);
            TestFrameworkData testFramework = testFrameworks[TargetTestFramework];
            if (Level >= GenerationLevel.TestMethod)
            {
                gen = new TestMethodGenerator(gen, testFramework);
            }
            if (Level >= GenerationLevel.TestMethodAndStepStubs)
            {
                gen = new AggregateCodeGenerator(gen, new StepMethodsGenerator());
            }
            if (Level >= GenerationLevel.Class)
            {
                gen = new ClassGenerator(gen, testFramework);
            }
            return gen;
        }
    }

    public enum TestFramework
    {
        NUnit,
        MSTest
    }

    public enum GenerationLevel
    {
        Story = 1,
        TestMethod = 2,
        TestMethodAndStepStubs = 3,
        Class = 4,
    }
}
