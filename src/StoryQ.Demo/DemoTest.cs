﻿using System;
using System.Collections.Specialized;
using System.Reflection;
using StoryQ.Formatting.Parameters;

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

namespace StoryQ.Demo
{
    [TestClass]
    public class DemoTest
    {

        [TestMethod]
        public void PassingExample()
        {
            new Story("Data Safety")
              .InOrderTo("Keep my data safe")
              .AsA("User")
              .IWant("All credit card numbers to be encrypted")
                  .WithScenario("submitting shopping cart")
                    .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                    .When(IClickThe_Button, "Buy")
                      .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)
                    .Then(TheForm_BePostedOverHttps, true)
                    .ExecuteWithSimpleReport(MethodBase.GetCurrentMethod());

        }

        private void TheForm_BePostedOverHttps([BooleanParameterFormat("should", "should not")]bool isHttps)
        {
        }


        private void TheBrowserPostsMyCreditCardNumberOverTheInternet()
        {
        }

        private void IHaveTypedMyCreditCardNumberIntoTheCheckoutPage()
        {
        }

        [TestMethod]
        public void PendingExample()
        {
            new Story("Data Safety")
                .InOrderTo("Keep my data safe")
                .AsA("User")
                .IWant("All credit card numbers to be encrypted")

                .WithScenario("submitting shopping cart")
                    .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                    .When(IClickThe_Button, "Buy")
                        .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)
                    .Then(TheForm_BePostedOverHttpsPending, true).Tag("this one ought to pend")
                .ExecuteWithSimpleReport(MethodBase.GetCurrentMethod());

        }

        private void TheForm_BePostedOverHttpsPending(bool obj)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FailingExample()
        {
            new Story("Data Safety")
                .InOrderTo("Keep my data safe")
                .AsA("User")
                .IWant("All credit card numbers to be encrypted")

                .WithScenario("submitting shopping cart")
                    .Given(IHaveTypedMyCreditCardNumberIntoTheCheckoutPage)
                    .When(IClickThe_Button, "non existent")                            .Tag("this one should fail")
                        .And(TheBrowserPostsMyCreditCardNumberOverTheInternet)
                    .Then(TheForm_BePostedOverHttps, true)                             .Tag("Nice formatting").Tag("sprint 1")
            .ExecuteWithSimpleReport(MethodBase.GetCurrentMethod());
        }

        private void IClickThe_Button(string buttonName)
        {
            if (buttonName != "Buy")
            {
                throw new Exception("No button with that name found!");
            }
        }



        
    }
}
