using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryQ.Formatting.Parameters;
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
    public class StoryQConverterSpecifications
    {
        private ViewModel.Converter converter;

        [TestMethod]
        public void ConvertingTextIntoCode()
        {
            new Story("converting text into code")
              .InOrderTo("create StoryQ specifications from plain text")
              .AsA("developer")
              .IWant("to convert plain text stories into C# StoryQ code")

                  .WithScenario("converting lines into string calls")
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                    .Then(IShouldSeeThatTextConvertedIntoStringPassingStoryqCalls)

                  .WithScenario("converting lines into string calls and code")
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                      .And(ITypeInSomeScenarioText)
                    .Then(IShouldSeeThatTextConvertedIntoMixedStoryqCalls)

                  .WithScenario("converting lines with variables into code")
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                      .And(ITypeInSomeScenarioTextWithADollarSymbolBeforeANumberOrWord)
                    .Then(IShouldSeeTheNumbersAndWordsPassedAsParametersToTheStoryqMethod)
             .Execute();
        }

        [TestMethod]
        public void CustomisingConverterIndentation()
        {
            new Story("customising converter output")
              .InOrderTo("handle different formatting requirements")
              .AsA("developer")
              .IWant("to be able to tweak the output code's indentation from the converter")

                  .WithScenario("turning off indentation")
                    .Given(ThatIHaveStoryAndScenarioText)
                    .When(ITurnOffIndentation)
                    .Then(TheOutputCodeShouldHaveNoIndentation)

                  .WithScenario("increasing the total indent")
                    .Given(ThatIHaveStoryAndScenarioText)
                    .When(IIncreaseTheIntialIndent)
                    .Then(TheOutputCodeShouldHaveTheRightAmountOfExtraWhitespace)

             .Execute();
        }

        private void TheOutputCodeShouldHaveTheRightAmountOfExtraWhitespace()
        {
            const string expected =
@"    new Story(""story name"")
      .InOrderTo(""get some benefit"")
      .AsA(""person in some role"")
      .IWant(""to use some software function"")
          .WithScenario(""scenario name"")
            .Given(ThatIHaveSomeInitialState)
            .When(IDoSomethingToTheSystem)
            .Then(IShouldGetAResult);";

            Assert.AreEqual(expected, converter.ConvertedText);
        }

        private void IIncreaseTheIntialIndent()
        {
            converter.InitialIndent = 2;
        }

        private void TheOutputCodeShouldHaveNoIndentation()
        {
            const string expected =
@"new Story(""story name"")
.InOrderTo(""get some benefit"")
.AsA(""person in some role"")
.IWant(""to use some software function"")
.WithScenario(""scenario name"")
.Given(ThatIHaveSomeInitialState)
.When(IDoSomethingToTheSystem)
.Then(IShouldGetAResult);";

            Assert.AreEqual(expected, converter.ConvertedText);
        }

        private void ITurnOffIndentation()
        {
            converter.OutputAnyIndent = false;
        }

        private void ThatIHaveStoryAndScenarioText()
        {
            ThatIHaveLaunchedStoryq();
            ITypeInSomeStoryText();
            ITypeInSomeScenarioText();
        }

        private void ThatIHaveLaunchedStoryq()
        {
            converter = new vm.Converter();
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

        private void IShouldSeeThatTextConvertedIntoStringPassingStoryqCalls()
        {
            const string expected =
@"new Story(""story name"")
  .InOrderTo(""get some benefit"")
  .AsA(""person in some role"")
  .IWant(""to use some software function"");";

            Assert.AreEqual(expected, converter.ConvertedText);
        }

        private void IShouldSeeThatTextConvertedIntoMixedStoryqCalls()
        {
            const string expected =
@"new Story(""story name"")
  .InOrderTo(""get some benefit"")
  .AsA(""person in some role"")
  .IWant(""to use some software function"")
      .WithScenario(""scenario name"")
        .Given(ThatIHaveSomeInitialState)
        .When(IDoSomethingToTheSystem)
        .Then(IShouldGetAResult);";

            Assert.AreEqual(expected, converter.ConvertedText);
        }
        private void IShouldSeeTheNumbersAndWordsPassedAsParametersToTheStoryqMethod()
        {
            const string expected =
@"new Story(""story name"")
  .InOrderTo(""get some benefit"")
  .AsA(""person in some role"")
  .IWant(""to use some software function"")
      .WithScenario(""scenario name"")
        .Given(ThatIHaveSomeInitialState)
        .When(IDoSomethingToTheSystem_Times, 1)
        .Then(IShouldGetA_, ""result"");";

            Assert.AreEqual(expected, converter.ConvertedText);
        }
    }
}
