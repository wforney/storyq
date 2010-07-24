﻿using Moq;
using StoryQ.Converter.Wpf.Services;
using StoryQ.Converter.Wpf.ViewModel;
using vm = StoryQ.Converter.Wpf.ViewModel;

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
    [TestClass]
    public class ConvertingTextIntoCode : SpecificationBase
    {
        protected override Feature DescribeStory(Story story)
        {
            return story
                .InOrderTo("create executable tests from plain text")
                .AsA("developer")
                .IWant("to use the converter to change text stories into C# StoryQ code");
        }


        [TestMethod]
        public void ConvertingLinesIntoStringCallsAndCode()
        {
            Scenario
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                    .And(ITypeInSomeScenarioText)
                    .Then(IShouldSeeThatTextConvertedIntoMixedStoryqCalls)
                    .ExecuteWithReport();
        }

        [TestMethod]
        public void ConvertingLinesAndVariablesIntoCode()
        {
            Scenario
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                    .And(ITypeInSomeScenarioTextWithADollarSymbolBeforeANumberOrWord)
                    .Then(IShouldSeeTheNumbersAndWordsPassedAsParametersToTheStoryqMethod)
                    .ExecuteWithReport();
        }

        [TestMethod]
        public void ConvertingLinesWithComplexVariablesIntoCode()
        {
            Scenario
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                    .And(ITypeInSomeScenarioTextWithDatesAndStringsInCurlyBraces)
                    .Then(IShouldSeeTheDatesAndWordsPassedAsParametersToTheStoryqMethod)
                    .ExecuteWithReport();
        }


        [TestMethod]
        public void GeneratingTestMethods()
        {
            Scenario
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.TestMethod)
                .Then(IShouldHaveMyMethodGenerated)
                .ExecuteWithReport();
        }

        [TestMethod]
        public void GeneratingTestMethodsAndStepStubs()
        {
            Scenario
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.TestMethodAndStepStubs)
                .Then(IShouldHaveMyMethodAndStepsGenerated)
                .ExecuteWithReport();
        }

        [TestMethod]
        public void GeneratingEntireClasses()
        {
            Scenario
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.Class)
                .Then(IShouldHaveMyClassGenerated)
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
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.Class)
                .And(ISetTheTestFrameworkTo, TestFramework.NUnit)
                .Then(IShouldHaveMyNUnitClassGenerated)

                .WithScenario("generating classes for MSTest")
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.Class)
                .And(ISetTheTestFrameworkTo, TestFramework.MSTest)
                .Then(IShouldHaveMyMSTestClassGenerated)

             .ExecuteWithReport();
        }

        private ViewModel.Converter converter;
        Mock<IFileSavingService> fileSavingService;
        Mock<ILanguagePackProvider> languagePackProvider;

        private void IShouldHaveMyMSTestClassGenerated()
        {
            Expect(
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
            IShouldHaveMyClassGenerated();
        }

        private void ISetTheTestFrameworkTo(TestFramework testFramework)
        {
            converter.Settings.TargetTestFramework = testFramework;
        }

        private void IShouldHaveMyMethodGenerated()
        {
            Expect(@"[Test]
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
            Expect(@"[Test]
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
            Expect(@"using System;
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
            converter.Settings.Level = level;
        }

        private void ThatIHaveStoryAndScenarioText()
        {
            ThatIHaveLaunchedStoryq();
            ITypeInSomeStoryText();
            ITypeInSomeScenarioText();
        }


        private void ThatIHaveLaunchedStoryq()
        {
            fileSavingService = new Mock<IFileSavingService>();
            languagePackProvider = new Mock<ILanguagePackProvider>();
            converter = new vm.Converter(fileSavingService.Object, languagePackProvider.Object);
        }

        private void ITypeInSomeStoryText()
        {
            converter.PlainText +=
@"story is story name
in order to get some benefit
as a person in some role
i want to use some software function";
        }

        private void ITypeInSomeScenarioText()
        {
            converter.PlainText +=
@"
with scenario scenario name
given that I have some initial state
when I do something to the system
then I should get a result";
        }

        private void ITypeInSomeScenarioTextWithADollarSymbolBeforeANumberOrWord()
        {

            converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system $1 times
then I should get a $result";

        }

        private void ITypeInSomeScenarioTextWithDatesAndStringsInCurlyBraces()
        {

            converter.PlainText +=
                @"
with scenario scenario name
given that I have some initial state
when I do something to the system from $2008-4-1 to {2008-4-1 12:59 GMT}
then I should get a {nice result}";

        }

        private void IShouldSeeThatTextConvertedIntoMixedStoryqCalls()
        {
            Expect(@"new Story(""story name"")
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
            Expect(
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

        private void IShouldSeeTheDatesAndWordsPassedAsParametersToTheStoryqMethod()
        {
            Expect(
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
            Assert.AreEqual(expected.Trim(), converter.ConvertedText.Trim());
        }

    }
}
