namespace StoryQ.Demo
{
    using System;
    using System.Reflection;
    using NUnit.Framework;
    using StoryQ.Formatting.Parameters;
    using StoryQ.TextualSteps;

    [TestFixture]
    public class MethodPerScenarioDemoTest
    {
        private Feature story = new Story("demonstrating splitting scenarios across methods")
            .InOrderTo("see more granularity in my test runner")
            .AsA("developer")
            .IWant("to share the same story, but have different scenarios in each test method");

        #region Test methods

        [Test]
        public void PassingExample()
        {
            //these steps all take strings because they are NEVER for execution
            story
                .WithScenario("Passing shopping cart example")
                //these steps all take Methods because they are meant to be exectuable. Steps that don't throw exceptions will pass
                .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(IClickThe_Button, "Buy")
                .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)
                .Then(TheForm_BePostedOverHttps, true).Tag("sprint 1")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        [Test]
        public void PendingExample()
        {
            story
                .WithScenario("Pending shopping cart example")
                .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(IClickThe_Button, "Buy")
                .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)

                // because the following method throws NotImplementedException, this step counts as pending:
                .Then(TheForm_BePostedOverHttpsPending, true).Tag("this one ought to pend")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());

        }

        [Test]
        public void PendingDueToStringExample()
        {
            story
                .WithScenario("Pending shopping cart example")
                .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(IClickThe_Button, "Buy")
                .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)

                // because it's passing a string into an excecutable step (which normallly expects a method is expected), this step counts as pending:
                .Then("The form should be posted over https").Tag("this one ought to pend")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());

        }

        [Test]
        public void FailingExample()
        {
            story
                .WithScenario("Failing shopping cart example")
                .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)

                // because it throws an exception, this step counts as a failure
                .When(IClickThe_Button, "non existent").Tag("this one should fail")
                .And(TheBrowserPostsMyCreditCardNumberOverTheInternet).Tag("sprint 1")
                .Then(TheForm_BePostedOverHttps, true).Tag("Nice formatting").Tag("sprint 1")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        #endregion

        #region Step methods

        private void TheForm_BePostedOverHttps([BooleanParameterFormat("should", "should not")]bool isHttps)
        {
        }


        private void TheBrowserPostsMyCreditCardNumberOverTheInternet()
        {
        }

        private void IHaveTypedMyCreditCardNumberIntoTheCheckoutPage()
        {
        }

        private void IClickThe_Button(string buttonName)
        {
            if (buttonName != "Buy")
            {
                throw new Exception("No button with that name found!");
            }
        }

        private void TheForm_BePostedOverHttpsPending(bool obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}