
#if NUNIT
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
namespace StoryQ.Converter.Wpf.Specifications
{
    using Moq;
    using StoryQ.Converter.Wpf.Services;
    using StoryQ.Converter.Wpf.ViewModel;
    using StoryQ.Infrastructure;
    using vm = StoryQ.Converter.Wpf.ViewModel;

    [TestClass]
    public class ConvertingTextIntoCode : SpecificationBase
    {
        protected override Feature DescribeStory(Story story) => story
                .InOrderTo("create executable tests from plain text")
                .AsA("developer")
                .IWant("to use the converter to change text stories into C# StoryQ code");

        [TestMethod]
        public void ConvertingLinesIntoStringCallsAndCode()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryText)
                    .And(this.ITypeInSomeScenarioText)
                    .Then(this.IShouldSeeThatTextConvertedIntoMixedStoryqCalls)
                    .ExecuteWithReport();
        }

        [TestMethod]
        public void ConvertingLinesIntoStringCallsAndCodeWithAliases()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryTextWithAliases)
                    .And(this.ITypeInSomeScenarioTextWithAliases)
                    .Then(this.IShouldSeeThatTextConvertedIntoMixedStoryqCalls)
                    .ExecuteWithReport();
        }

        [TestMethod]
        public void ConvertingLinesAndVariablesIntoCode()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryText)
                    .And(this.ITypeInSomeScenarioTextWithADollarSymbolBeforeANumberOrWord)
                    .Then(this.IShouldSeeTheNumbersAndWordsPassedAsParametersToTheStoryqMethod)
                    .ExecuteWithReport();
        }
	
	 [TestMethod]
        public void ConvertingNegativeIntegerIntoCode()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryText)
                    .And(this.ITypeInSomeScenarioTextWithANegativeInteger)
                    .Then(this.IShouldSeeTheIntegerPassedAsAIntegerParameterToTheStoryqMethod)
                    .ExecuteWithReport();
        }
	
	 [TestMethod]
        public void ConvertingNegativeDoubleIntoCode()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryText)
                    .And(this.ITypeInSomeScenarioTextWithANegativeDouble)
                    .Then(this.IShouldSeeTheDoublePassedAsADoubleParameterToTheStoryqMethod)
                    .ExecuteWithReport();
        }
	

        [TestMethod]
        public void ConvertingLinesWithComplexVariablesIntoCode()
        {
            this.Scenario
                    .Given(this.ThatIHaveLaunchedStoryq)
                    .When(this.ITypeInSomeStoryText)
                    .And(this.ITypeInSomeScenarioTextWithDatesAndStringsInCurlyBraces)
                    .Then(this.IShouldSeeTheDatesAndWordsPassedAsParametersToTheStoryqMethod)
                    .ExecuteWithReport();
        }


        [TestMethod]
        public void GeneratingTestMethods()
        {
            this.Scenario
                .Given(this.ThatIHaveStoryAndScenarioText)
                .When(this.ISetTheOutputTypeTo_, GenerationLevel.TestMethod)
                .Then(this.IShouldHaveMyMethodGenerated)
                .ExecuteWithReport();
        }

        [TestMethod]
        public void GeneratingTestMethodsAndStepStubs()
        {
            this.Scenario
                .Given(this.ThatIHaveStoryAndScenarioText)
                .When(this.ISetTheOutputTypeTo_, GenerationLevel.TestMethodAndStepStubs)
                .Then(this.IShouldHaveMyMethodAndStepsGenerated)
                .ExecuteWithReport();
        }

        [TestMethod]
        public void GeneratingEntireClasses()
        {
            this.Scenario
                .Given(this.ThatIHaveStoryAndScenarioText)
                .When(this.ISetTheOutputTypeTo_, GenerationLevel.Class)
                .Then(this.IShouldHaveMyClassGenerated)
                .ExecuteWithReport();
        }

        [TestMethod]
        public void GeneratingForDifferentTestFrameworks()
        {
            new Story("creating classes for different test frameworks").Tag("WorkItemId=16092")
                .InOrderTo("create StoryQ specifications from plain text")
                .AsA("developer")
                .IWant("to convert plain text stories into entire C# files")

                .WithScenario("generating classes for NUnit")
                .Given(this.ThatIHaveStoryAndScenarioText)
                .When(this.ISetTheOutputTypeTo_, GenerationLevel.Class)
                .And(this.ISetTheTestFrameworkTo, TestFramework.NUnit)
                .Then(this.IShouldHaveMyNUnitClassGenerated)

                .WithScenario("generating classes for MSTest")
                .Given(this.ThatIHaveStoryAndScenarioText)
                .When(this.ISetTheOutputTypeTo_, GenerationLevel.Class)
                .And(this.ISetTheTestFrameworkTo, TestFramework.MSTest)
                .Then(this.IShouldHaveMyMSTestClassGenerated)

             .ExecuteWithReport();
        }

        private ViewModel.Converter converter;
        private Mock<IFileSavingService> fileSavingService;
        private Mock<ILanguagePackProvider> languagePackProvider;

        private void ThatIHaveLaunchedStoryq()
        {
            this.fileSavingService = new Mock<IFileSavingService>();
            this.languagePackProvider = new Mock<ILanguagePackProvider>();
            var languagePack = new Mock<ILocalLanguagePack>();
            languagePack.Setup(x => x.ParserEntryPoint).Returns(new StoryQEntryPoints());
            this.languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(new[] { languagePack.Object });
            this.converter = new vm.Converter(this.fileSavingService.Object, this.languagePackProvider.Object);
        }


        private void IShouldHaveMyMSTestClassGenerated()
        {
            this.Expect(
                @"using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StoryQTestClass
{
    [TestMethod]
    public void StoryName()
    {
        new Story(""story name"")
            .InOrderTo(""get some benefit"")
            .AsA(""person in some role"")
            .IWant(""to use some software function"")

                    .WithScenario(""scenario name"")
                        .Given(ThatIHaveSomeInitialState)
                        .When(IDoSomethingToTheSystem)
                        .Then(IShouldGetAResult)
            .Execute();
    }

    private void ThatIHaveSomeInitialState()
    {
        throw new NotImplementedException();
    }

    private void IDoSomethingToTheSystem()
    {
        throw new NotImplementedException();
    }

    private void IShouldGetAResult()
    {
        throw new NotImplementedException();
    }
}");
        }

        private void IShouldHaveMyNUnitClassGenerated()
        {
            this.IShouldHaveMyClassGenerated();
        }

        private void ISetTheTestFrameworkTo(TestFramework testFramework)
        {
            this.converter.Settings.TargetTestFramework = testFramework;
        }

        private void IShouldHaveMyMethodGenerated()
        {
            this.Expect(@"[Test]
public void StoryName()
{
    new Story(""story name"")
        .InOrderTo(""get some benefit"")
        .AsA(""person in some role"")
        .IWant(""to use some software function"")

                .WithScenario(""scenario name"")
                    .Given(ThatIHaveSomeInitialState)
                    .When(IDoSomethingToTheSystem)
                    .Then(IShouldGetAResult)
        .Execute();
}");
        }

        private void IShouldHaveMyMethodAndStepsGenerated()
        {
            this.Expect(@"[Test]
public void StoryName()
{
    new Story(""story name"")
        .InOrderTo(""get some benefit"")
        .AsA(""person in some role"")
        .IWant(""to use some software function"")

                .WithScenario(""scenario name"")
                    .Given(ThatIHaveSomeInitialState)
                    .When(IDoSomethingToTheSystem)
                    .Then(IShouldGetAResult)
        .Execute();
}

private void ThatIHaveSomeInitialState()
{
    throw new NotImplementedException();
}

private void IDoSomethingToTheSystem()
{
    throw new NotImplementedException();
}

private void IShouldGetAResult()
{
    throw new NotImplementedException();
}
");
        }

        private void IShouldHaveMyClassGenerated()
        {
            this.Expect(@"using System;
using StoryQ;
using NUnit.Framework;

[TestFixture]
public class StoryQTestClass
{
    [Test]
    public void StoryName()
    {
        new Story(""story name"")
            .InOrderTo(""get some benefit"")
            .AsA(""person in some role"")
            .IWant(""to use some software function"")

                    .WithScenario(""scenario name"")
                        .Given(ThatIHaveSomeInitialState)
                        .When(IDoSomethingToTheSystem)
                        .Then(IShouldGetAResult)
            .Execute();
    }

    private void ThatIHaveSomeInitialState()
    {
        throw new NotImplementedException();
    }

    private void IDoSomethingToTheSystem()
    {
        throw new NotImplementedException();
    }

    private void IShouldGetAResult()
    {
        throw new NotImplementedException();
    }
}
");
        }

        private void ISetTheOutputTypeTo_(GenerationLevel level)
        {
            this.converter.Settings.Level = level;
        }

        private void ThatIHaveStoryAndScenarioText()
        {
            this.ThatIHaveLaunchedStoryq();
            this.ITypeInSomeStoryText();
            this.ITypeInSomeScenarioText();
        }


       
        private void ITypeInSomeStoryText()
        {
            this.converter.PlainText +=
@"story is story name
in order to get some benefit
as a person in some role
i want to use some software function";
        }

        private void ITypeInSomeStoryTextWithAliases()
        {
            this.converter.PlainText +=
@"feature:story name
in order to get some benefit
as a person in some role
i want to use some software function";
        }

        private void ITypeInSomeScenarioText()
        {
            this.converter.PlainText +=
@"
with scenario scenario name
given that I have some initial state
when I do something to the system
then I should get a result";
        }

        private void ITypeInSomeScenarioTextWithAliases()
        {
            this.converter.PlainText +=
@"
scenario: scenario name
given that I have some initial state
when I do something to the system
then I should get a result";
        }

        private void ITypeInSomeScenarioTextWithADollarSymbolBeforeANumberOrWord()
        {

            this.converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system $1 times
then I should get a $result";

        }
	
	private void ITypeInSomeScenarioTextWithANegativeInteger()
        {

            this.converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system $-1 times
then I should get a {-1}";

        }
	
	private void ITypeInSomeScenarioTextWithANegativeDouble()
        {

            this.converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system $-1.5 times
then I should get a {-1.5}";

        }

        private void ITypeInSomeScenarioTextWithDatesAndStringsInCurlyBraces()
        {

            this.converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system from $2008-4-1 to {2008-4-1 12:59 GMT}
then I should get a {nice result}";

        }

        private void IShouldSeeThatTextConvertedIntoMixedStoryqCalls()
        {
            this.Expect(@"new Story(""story name"")
    .InOrderTo(""get some benefit"")
    .AsA(""person in some role"")
    .IWant(""to use some software function"")

            .WithScenario(""scenario name"")
                .Given(ThatIHaveSomeInitialState)
                .When(IDoSomethingToTheSystem)
                .Then(IShouldGetAResult)
    .Execute();
");
        }

        private void IShouldSeeTheNumbersAndWordsPassedAsParametersToTheStoryqMethod()
        {
            this.Expect(
                @"new Story(""story name"")
    .InOrderTo(""get some benefit"")
    .AsA(""person in some role"")
    .IWant(""to use some software function"")

            .WithScenario(""scenario name"")
                .Given(ThatIHaveSomeInitialState)
                .When(IDoSomethingToTheSystem_Times, 1)
                .Then(IShouldGetA_, ""result"")
    .Execute();");
        }
	
        private void IShouldSeeTheIntegerPassedAsAIntegerParameterToTheStoryqMethod()
        {
            this.Expect(
                @"new Story(""story name"")
    .InOrderTo(""get some benefit"")
    .AsA(""person in some role"")
    .IWant(""to use some software function"")

            .WithScenario(""scenario name"")
                .Given(ThatIHaveSomeInitialState)
                .When(IDoSomethingToTheSystem_Times, -1)
                .Then(IShouldGetA_, -1)
    .Execute();");
        }	

	private void IShouldSeeTheDoublePassedAsADoubleParameterToTheStoryqMethod()
        {
            this.Expect(
                @"new Story(""story name"")
    .InOrderTo(""get some benefit"")
    .AsA(""person in some role"")
    .IWant(""to use some software function"")

            .WithScenario(""scenario name"")
                .Given(ThatIHaveSomeInitialState)
                .When(IDoSomethingToTheSystem_Times, -1.5)
                .Then(IShouldGetA_, -1.5)
    .Execute();");
        }
        private void IShouldSeeTheDatesAndWordsPassedAsParametersToTheStoryqMethod()
        {
            this.Expect(
                @"new Story(""story name"")
    .InOrderTo(""get some benefit"")
    .AsA(""person in some role"")
    .IWant(""to use some software function"")

            .WithScenario(""scenario name"")
                .Given(ThatIHaveSomeInitialState)
                .When(IDoSomethingToTheSystemFrom_To_, DateTime.Parse(""2008-4-1""), DateTime.Parse(""2008-4-1 12:59 GMT""))
                .Then(IShouldGetA_, ""nice result"")
    .Execute();");
        }

        private void Expect(string expected)
        {
            Assert.AreEqual(expected.Trim(), this.converter.ConvertedText.Trim());
        }

    }
}
