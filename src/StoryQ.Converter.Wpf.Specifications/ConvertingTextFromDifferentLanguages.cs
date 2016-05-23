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
        protected override Feature DescribeStory(Story story) => story
                .InOrderTo("be able to write storyq test specifications")
                .AsA("clickonce user")
                .IWant("to be able to save the storyq dll directly from the converter ui")
                .And("to be able to pick up language packs via clickonce via the StoryQ Converter UI");

        [Test]
        public void SavingTheStoryQDllFromTheConverter()
        {
            this.Scenario
                .Given(this.ThatIHaveLaunchedStoryq)
                .When(this.IClickTheSaveLibrariesButton)
                .Then(this.TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose)
                .ExecuteWithReport();
        }

        [Test]
        public void ListingAvailableLanguages()
        {
            this.Scenario
                .Given(this.ThatIHaveLaunchedStoryq)
                .When(this.ThereAreLanguagePacksAvailable)
                .Then(this.IShouldSeeTheLanguagePacksInAList)
                .ExecuteWithReport();
        }

        [Test]
        public void SelectingALocalLanguagePack()
        {
            this.Scenario
                .Given(this.ThereAreLanguagePacksInAList)
                .When(this.ISelectANewLocalLanguagePack)
                .Then(this.TheConverterShouldWorkWithTheNewLanguagePack)
                .ExecuteWithReport();
        }

        [Test]
        public void SelectingARemoteLanguagePack()
        {
            this.Scenario
                .Given(this.ThereAreLanguagePacksInAList)
                .When(this.ISelectANewRemoteLanguagePack)
                .Then(this.TheConverterShouldDownloadTheNewLanguagePack)
                .And(this.TheConverterShouldWorkWithTheNewLanguagePack)
                .ExecuteWithReport();
        }

        private ViewModel.Converter converter;
        private Mock<IFileSavingService> fileSavingService;
        private Mock<ILanguagePackProvider> languagePackProvider;
        private List<Mock<ILocalLanguagePack>> localLanguagePacks;
        private List<Mock<IRemoteLanguagePack>> remoteLanguagePacks;
        private string expectedEntryPoint;

        private void ThatIHaveLaunchedStoryq()
        {
            this.fileSavingService = new Mock<IFileSavingService>();
            this.languagePackProvider = new Mock<ILanguagePackProvider>();
            this.languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(new[] { new Mock<ILocalLanguagePack>().Object });
            this.converter = new ViewModel.Converter(this.fileSavingService.Object, this.languagePackProvider.Object);
        }

        private void TheStoryQDllShouldBeSavedIntoTheDirectoryIChoose()
        {
            this.fileSavingService.Verify(x => x.CopyLibFiles("directory"));
        }

        private void IClickTheSaveLibrariesButton()
        {
            this.fileSavingService.Setup(x => x.PromptForDirectory(It.IsAny<string>())).Returns("directory");

            this.converter.SaveLibrariesCommand.Execute(null);
        }

        private void TheConverterShouldDownloadTheNewLanguagePack()
        {
            this.remoteLanguagePacks[0].Verify(x => x.BeginDownloadAsync(It.IsAny<Action<double>>(), It.IsAny<Action<ILocalLanguagePack>>()));
        }

        private void ISelectANewRemoteLanguagePack()
        {
            var newLocal = new Mock<ILocalLanguagePack>();
            newLocal.Setup(x => x.ParserEntryPoint).Returns("newLocal");
            this.expectedEntryPoint = "newLocal";

            Action<Action<double>, Action<ILocalLanguagePack>> callback = (action1, action2) => action2(newLocal.Object);

            this.remoteLanguagePacks[0]
                .Setup(x => x.BeginDownloadAsync(It.IsAny<Action<double>>(), It.IsAny<Action<ILocalLanguagePack>>()))
                .Callback(callback);



            this.converter.CurrentLanguagePack = this.converter.LanguagePacks[3];
        }

        private void TheConverterShouldWorkWithTheNewLanguagePack()
        {
            Assert.AreEqual(this.expectedEntryPoint, this.converter.CurrentParserEntryPoint);
        }

        private void ISelectANewLocalLanguagePack()
        {
            this.converter.CurrentLanguagePack = this.converter.LanguagePacks[1];
            this.expectedEntryPoint = "pack 2";
        }

        private void ThereAreLanguagePacksInAList()
        {
            this.ThatIHaveLaunchedStoryq();
            this.ThereAreLanguagePacksAvailable();
            this.IShouldSeeTheLanguagePacksInAList();
        }

        private void IShouldSeeTheLanguagePacksInAList()
        {
            Assert.AreEqual(6, this.converter.LanguagePacks.Count);
            Assert.AreEqual("pack 1", this.converter.LanguagePacks.First().Text);
            Assert.IsTrue(this.converter.LanguagePacks.First().IsDownloaded);
            Assert.AreEqual("pack 4", this.converter.LanguagePacks.ElementAt(3).Text);
            Assert.IsFalse(this.converter.LanguagePacks.ElementAt(3).IsDownloaded);
        }

        private void ThereAreLanguagePacksAvailable()
        {

            this.localLanguagePacks = new List<Mock<ILocalLanguagePack>>
                                     {
                                         new Mock<ILocalLanguagePack>(),
                                         new Mock<ILocalLanguagePack>(),
                                         new Mock<ILocalLanguagePack>()
                                     };

            Mock<ILocalLanguagePack> first = this.localLanguagePacks[0];
            first.Setup(x => x.Name).Returns("pack 1");
            first.Setup(x => x.ParserEntryPoint).Returns("pack 1");

            Mock<ILocalLanguagePack> second = this.localLanguagePacks[1];
            second.Setup(x => x.Name).Returns("pack 2");
            second.Setup(x => x.ParserEntryPoint).Returns("pack 2");

            this.remoteLanguagePacks = new List<Mock<IRemoteLanguagePack>>
                                      {
                                          new Mock<IRemoteLanguagePack>(),
                                          new Mock<IRemoteLanguagePack>(),
                                          new Mock<IRemoteLanguagePack>()
                                      };
            this.remoteLanguagePacks[0].Setup(x => x.Name).Returns("pack 4");


            this.languagePackProvider.Setup(x => x.GetLocalLanguagePacks()).Returns(this.localLanguagePacks.Select(x => x.Object));
            this.languagePackProvider.Setup(x => x.GetRemoteLanguagePacks()).Returns(this.remoteLanguagePacks.Select(x => x.Object));

            this.converter = new ViewModel.Converter(this.fileSavingService.Object, this.languagePackProvider.Object);
        }



    }
}