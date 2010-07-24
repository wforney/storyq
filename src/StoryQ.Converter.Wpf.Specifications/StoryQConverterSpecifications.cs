using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
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
    public class StoryQConverterSpecifications
    {
       [TestMethod]
        public void ConvertingTextIntoStoryCode()
        {
            new Story("converting text into code")
              .InOrderTo("create StoryQ specifications from plain text")
              .AsA("developer")
              .IWant("to convert plain text stories into C# StoryQ code")

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
                    
                    .WithScenario("converting lines with complex variables into code")
                    .Given(ThatIHaveLaunchedStoryq)
                    .When(ITypeInSomeStoryText)
                      .And(ITypeInSomeScenarioTextWithDatesAndStringsInCurlyBraces)
                    .Then(IShouldSeeTheDatesAndWordsPassedAsParametersToTheStoryqMethod)
             .Execute();
        }


        [TestMethod]
        public void ConvertingTextIntoTestClasses()
        {
            new Story("converting text into entire class files").Tag("WorkItemId=16092")
                .InOrderTo("create StoryQ specifications from plain text")
                .AsA("developer")
                .IWant("to convert plain text stories into entire C# files")

                .WithScenario("generating test methods")
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.TestMethod)
                .Then(IShouldHaveMyMethodGenerated)
                
                .WithScenario("generating test methods and step stubs")
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.TestMethodAndStepStubs)
                .Then(IShouldHaveMyMethodAndStepsGenerated)

                .WithScenario("generating entire classes")
                .Given(ThatIHaveStoryAndScenarioText)
                .When(ISetTheOutputTypeTo_, GenerationLevel.Class)
                .Then(IShouldHaveMyClassGenerated)
             .Execute();
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
              
             .Execute();
        }

        [TestMethod]
        public void SavingTheStoryQDllFromTheConverter()
        {
            new Story("creating classes for different test frameworks")
                .InOrderTo("be able to write storyq test specifications")
                .AsA("clickonce user")
                .IWant("to be able to save the storyq dll directly from the converter ui")

                .WithScenario("Saving Language packs")
                .Given(ThatIHaveLaunchedStoryq)
                .When(IClickTheSaveLibrariesButton)
                .Then(TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose)
             .Execute();
        }
        
        [TestMethod]
        public void DeployingLanguagePacksViaClickOnce()
        {
            new Story("creating classes for different test frameworks").Tag("WorkItemId=16100")
                .InOrderTo("create StoryQ specifications in my native language")
                .AsA("stakeholder")
                .IWant("to be able to pick up language packs via clickonce via the StoryQ Converter UI")

                .WithScenario("listing available language packs")
                .Given(ThatIHaveLaunchedStoryq)
                .When(ThereAreLanguagePacksAvailable)
                .Then(IShouldSeeTheLanguagePacksInAList)

                .WithScenario("downloading language packs")
                .Given(ThereAreLanguagePacksInAList)
                .When(ISelectANewLanguagePack)
                .Then(TheConverterShouldWorkWithTheNewLanguagePack)

                .WithScenario("Saving Language packs")
                .Given(IHaveDownloadedSomeLanguagePacks)
                .When(IClickTheSaveLibrariesButton)
                .Then(TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose)
                .And(AllTheLanguagePacksShouldBeSavedIntoTheDirectoryIChoose)
             .Execute();
        }

        private ViewModel.Converter converter;
        Mock<IFileSavingService> fileSavingService;
        Mock<ILanguagePackProvider> languagePackProvider;
        List<Mock<ILocalLanguagePack>> localLanguagePacks;
        List<Mock<IRemoteLanguagePack>> remoteLanguagePacks;


        void AllTheLanguagePacksShouldBeSavedIntoTheDirectoryIChoose()
        {
            throw new NotImplementedException();
        }

        void TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose()
        {
            fileSavingService.Verify(x=>x.CopyLibFiles("directory"));
        }

        void IClickTheSaveLibrariesButton()
        {
            fileSavingService.Setup(x => x.PromptForDirectory(It.IsAny<string>())).Returns("directory");

            this.converter.SaveLibrariesCommand.Execute(null);
        }

        void IHaveDownloadedSomeLanguagePacks()
        {
            throw new NotImplementedException();
        }

        void TheConverterShouldWorkWithTheNewLanguagePack()
        {
            throw new NotImplementedException();
        }

        void ISelectANewLanguagePack()
        {
            throw new NotImplementedException();
        }

        void ThereAreLanguagePacksInAList()
        {
            ThatIHaveLaunchedStoryq();
            ThereAreLanguagePacksAvailable();
            IShouldSeeTheLanguagePacksInAList();
        }

        void IShouldSeeTheLanguagePacksInAList()
        {
            Assert.AreEqual(6, converter.LanguagePacks.Count);
            Assert.AreEqual("pack 1", converter.LanguagePacks.First().Text);
            Assert.IsTrue(converter.LanguagePacks.First().IsDownloaded);
            Assert.AreEqual("pack 4", converter.LanguagePacks.ElementAt(4).Text);
            Assert.IsFalse(converter.LanguagePacks.First().IsDownloaded);
        }

        void ThereAreLanguagePacksAvailable()
        {
            localLanguagePacks = new List<Mock<ILocalLanguagePack>>()
                                {
                                    new Mock<ILocalLanguagePack>(),
                                    new Mock<ILocalLanguagePack>(),
                                    new Mock<ILocalLanguagePack>()
                                };
            localLanguagePacks.First().Setup(x => x.Name).Returns("pack 1");

            remoteLanguagePacks = new List<Mock<IRemoteLanguagePack>>()
                                {
                                    new Mock<IRemoteLanguagePack>(),
                                    new Mock<IRemoteLanguagePack>(),
                                    new Mock<IRemoteLanguagePack>()
                                };
            remoteLanguagePacks.First().Setup(x => x.Name).Returns("pack 4");


            languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(localLanguagePacks.Select(x => x.Object));
            languagePackProvider.Setup(x => x.GetRemoteLanguagePacks()).Returns(remoteLanguagePacks.Select(x => x.Object));
        }

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
            converter.Settings.TargetTestFramework=testFramework;
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
