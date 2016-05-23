
#if NUNIT
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace StoryQ.Demo
{
    using System;
    using System.Reflection;
    using StoryQ.Formatting.Parameters;
    using StoryQ.TextualSteps;

    [TestClass]
    public class DemoTest
    {
        #region Test methods

        [TestMethod]
        public void PassingExample()
        {
            // these steps all take strings because they are NEVER for execution
            new Story("Data Safety").Tag("Sprint 1")
                .InOrderTo("Keep my data safe")
                .AsA("User")
                .IWant("All credit card numbers to be encrypted")
                .WithScenario("submitting shopping cart")

                // these steps all take Methods because they are meant to be exectuable. Steps that don't throw exceptions will pass
                .Given(this.IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(this.IClickThe_Button, "Buy")
                .And(this.TheBrowserPostsMyCreditCardNumberOverTheInternet)
                .Then(this.TheForm_BePostedOverHttps, true).Tag("sprint 1")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        [TestMethod]
        public void PendingExample()
        {
            new Story("Data Safety").Tag("this one ought to pend")
                .InOrderTo("Keep my data safe")
                .AsA("User").Tag("sprint 1")
                .IWant("All credit card numbers to be encrypted")
                .WithScenario("submitting shopping cart")
                .Given(this.IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(this.IClickThe_Button, "Buy")
                .And(this.TheBrowserPostsMyCreditCardNumberOverTheInternet)

                // because the following method throws NotImplementedException, this step counts as pending:
                .Then(this.TheForm_BePostedOverHttpsPending, true).Tag("this one ought to pend")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());

        }

        [TestMethod]
        public void PendingDueToStringExample()
        {
            new Story("Data Safety").Tag("this one ought to pend")
                .InOrderTo("Keep my data safe")
                .AsA("User").Tag("sprint 1")
                .IWant("All credit card numbers to be encrypted")
                .WithScenario("submitting shopping cart")
                .Given(this.IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                .When(this.IClickThe_Button, "Buy")
                .And(this.TheBrowserPostsMyCreditCardNumberOverTheInternet)

                // because it's passing a string into an excecutable step (which normallly expects a method is expected), this step counts as pending:
                .Then("The form should be posted over https").Tag("this one ought to pend")
                .ExecuteWithReport(MethodBase.GetCurrentMethod());

        }

        [TestMethod]
        public void FailingExample()
        {
            new Story("Data Safety")
                .InOrderTo("Keep my data safe")
                .AsA("User")
                .IWant("All credit card numbers to be encrypted")
                .WithScenario("submitting shopping cart")
                .Given(this.IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)

                // because it throws an exception, this step counts as a failure
                .When(this.IClickThe_Button, "non existent").Tag("this one should fail")
                .And(this.TheBrowserPostsMyCreditCardNumberOverTheInternet).Tag("sprint 1")
                .Then(this.TheForm_BePostedOverHttps, true).Tag("Nice formatting").Tag("sprint 1")
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
