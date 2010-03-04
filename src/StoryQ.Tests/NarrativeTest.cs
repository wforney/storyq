using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryQ.Execution;

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
namespace StoryQ.Tests
{
    [TestClass]
    public class StepTest
    {
        [TestMethod]
        public void ExcecutePending()
        {
            Step n = new Step("Given", 1, "a pending Step", Step.Pend);
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Pending, e.Type);
            Assert.IsNotNull(e.Exception);
        }

        [TestMethod]
        public void ExcecuteFail()
        {
            Step n = new Step("Given", 1, "a failing Step", () => Assert.Fail("Fail!"));
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Failed, e.Type);
            Assert.IsNotNull(e.Exception);
        }

        [TestMethod]
        public void ExcecutePass()
        {
            Step n = new Step("Given", 1, "a passing Step", () => { });
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Passed, e.Type);
            Assert.IsNull(e.Exception);
        }

        [TestMethod]
        public void ExcecuteNonExecutable()
        {
            Step n = new Step("As a", 1, "non executable Step", Step.DoNothing);
            Result e = n.Execute();
            Assert.AreEqual(ResultType.NotExecutable, e.Type);
            Assert.IsNull(e.Exception);
        }
    }
}
