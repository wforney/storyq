namespace StoryQ.Converter.Wpf.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using StoryQ.Converter.Wpf.Services;
    using StoryQ.Converter.Wpf.ViewModel;

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
                .Given(ThatIHaveLaunchedStoryq)
                .When(ThereAreLanguagePacksAvailable)
                .Then(IShouldSeeTheLanguagePacksInAList)
                .ExecuteWithReport();
        }

        [Test]
        public void SelectingALocalLanguagePack()
        {
            Scenario
                .Given(ThereAreLanguagePacksInAList)
                .When(ISelectANewLocalLanguagePack)
                .Then(TheConverterShouldWorkWithTheNewLanguagePack)
                .ExecuteWithReport();
        }

        [Test]
        public void SelectingARemoteLanguagePack()
        {
            Scenario
                .Given(ThereAreLanguagePacksInAList)
                .When(ISelectANewRemoteLanguagePack)
                .Then(TheConverterShouldDownloadTheNewLanguagePack)
                .And(TheConverterShouldWorkWithTheNewLanguagePack)
                .ExecuteWithReport();
        }

        private ViewModel.Converter converter;
        Mock<IFileSavingService> fileSavingService;
        Mock<ILanguagePackProvider> languagePackProvider;
        List<Mock<ILocalLanguagePack>> localLanguagePacks;
        List<Mock<IRemoteLanguagePack>> remoteLanguagePacks;
        string expectedEntryPoint;

        private void ThatIHaveLaunchedStoryq()
        {
            fileSavingService = new Mock<IFileSavingService>();
            languagePackProvider = new Mock<ILanguagePackProvider>();
            languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(new[] { new Mock<ILocalLanguagePack>().Object });
            converter = new ViewModel.Converter(fileSavingService.Object, languagePackProvider.Object);
        }

        void TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose()
        {
            fileSavingService.Verify(x => x.CopyLibFiles("directory"));
        }

        void IClickTheSaveLibrariesButton()
        {
            fileSavingService.Setup(x => x.PromptForDirectory(It.IsAny<string>())).Returns("directory");

            converter.SaveLibrariesCommand.Execute(null);
        }

        void TheConverterShouldDownloadTheNewLanguagePack()
        {
            remoteLanguagePacks[0].Verify(x => x.BeginDownloadAsync(It.IsAny<Action<double>>(), It.IsAny<Action<ILocalLanguagePack>>()));
        }

        void ISelectANewRemoteLanguagePack()
        {
            var newLocal = new Mock<ILocalLanguagePack>();
            newLocal.Setup(x => x.ParserEntryPoint).Returns("newLocal");
            expectedEntryPoint = "newLocal";

            Action<Action<double>, Action<ILocalLanguagePack>> callback = (action1, action2) => action2(newLocal.Object);

            remoteLanguagePacks[0]
                .Setup(x => x.BeginDownloadAsync(It.IsAny<Action<double>>(), It.IsAny<Action<ILocalLanguagePack>>()))
                .Callback(callback);



            converter.CurrentLanguagePack = converter.LanguagePacks[3];
        }

        void TheConverterShouldWorkWithTheNewLanguagePack()
        {
            Assert.AreEqual(expectedEntryPoint, converter.CurrentParserEntryPoint);
        }

        void ISelectANewLocalLanguagePack()
        {
            converter.CurrentLanguagePack = converter.LanguagePacks[1];
            expectedEntryPoint = "pack 2";
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
            Assert.AreEqual("pack 4", converter.LanguagePacks.ElementAt(3).Text);
            Assert.IsFalse(converter.LanguagePacks.ElementAt(3).IsDownloaded);
        }

        void ThereAreLanguagePacksAvailable()
        {

            localLanguagePacks = new List<Mock<ILocalLanguagePack>>
                                     {
                                         new Mock<ILocalLanguagePack>(),
                                         new Mock<ILocalLanguagePack>(),
                                         new Mock<ILocalLanguagePack>()
                                     };

            Mock<ILocalLanguagePack> first = localLanguagePacks[0];
            first.Setup(x => x.Name).Returns("pack 1");
            first.Setup(x => x.ParserEntryPoint).Returns("pack 1");

            Mock<ILocalLanguagePack> second = localLanguagePacks[1];
            second.Setup(x => x.Name).Returns("pack 2");
            second.Setup(x => x.ParserEntryPoint).Returns("pack 2");

            remoteLanguagePacks = new List<Mock<IRemoteLanguagePack>>
                                      {
                                          new Mock<IRemoteLanguagePack>(),
                                          new Mock<IRemoteLanguagePack>(),
                                          new Mock<IRemoteLanguagePack>()
                                      };
            remoteLanguagePacks[0].Setup(x => x.Name).Returns("pack 4");


            languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(localLanguagePacks.Select(x => x.Object));
            languagePackProvider.Setup(x => x.GetRemoteLanguagePacks()).Returns(remoteLanguagePacks.Select(x => x.Object));

            converter = new ViewModel.Converter(fileSavingService.Object, languagePackProvider.Object);
        }



    }
}