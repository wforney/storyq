using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using StoryQ.Converter.Wpf.Services;
using StoryQ.Converter.Wpf.ViewModel;

namespace StoryQ.Converter.Wpf.Specifications
{
    [TestFixture]
    public class ConvertingTextFromDifferentLanguages : SpecificationBase
    {
        protected override Feature DescribeStory(Story story)
        {
            return story
                .InOrderTo("be able to write storyq test specifications")
                .AsA("clickonce user")
                .IWant("to be able to save the storyq dll directly from the converter ui")
                .And("to be able to pick up language packs via clickonce via the StoryQ Converter UI");
        }

        [Test]
        public void SavingTheStoryQDllFromTheConverter()
        {
            Scenario
                .Given(ThatIHaveLaunchedStoryq)
                .When(IClickTheSaveLibrariesButton)
                .Then(TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose)
                .ExecuteWithReport();
        }

        [Test]
        public void ListingAvailableLanguages()
        {
            Scenario
                .Given(ThereAreLanguagePacksAvailable)
                .When(ILaunchStoryQ)
                .Then(IShouldSeeTheLanguagePacksInAList)
                .ExecuteWithReport();
        }

        [Test]
        public void DownloadingLanguagePacks()
        {
            Scenario
                .Given(ThereAreLanguagePacksInAList)
                .When(ISelectANewLanguagePack)
                .Then(TheConverterShouldWorkWithTheNewLanguagePack)
                .ExecuteWithReport();
        }

        [Test]
        public void SavingLanguagePacks()
        {
            Scenario
                .Given(IHaveDownloadedSomeLanguagePacks)
                .When(IClickTheSaveLibrariesButton)
                .Then(TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose)
                .And(AllTheLanguagePacksShouldBeSavedIntoTheDirectoryIChoose)
                .ExecuteWithReport();
        }

        private ViewModel.Converter converter;
        Mock<IFileSavingService> fileSavingService;
        Mock<ILanguagePackProvider> languagePackProvider;
        List<Mock<ILocalLanguagePack>> localLanguagePacks;
        List<Mock<IRemoteLanguagePack>> remoteLanguagePacks;

        private void ThatIHaveLaunchedStoryq()
        {
            fileSavingService = new Mock<IFileSavingService>();
            languagePackProvider = new Mock<ILanguagePackProvider>();
            converter = new ViewModel.Converter(fileSavingService.Object, languagePackProvider.Object);
        }

        void ILaunchStoryQ()
        {
            ThatIHaveLaunchedStoryq();
        }

        void AllTheLanguagePacksShouldBeSavedIntoTheDirectoryIChoose()
        {
            throw new NotImplementedException();
        }

        void TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose()
        {
            fileSavingService.Verify(x => x.CopyLibFiles("directory"));
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


      
    }
}