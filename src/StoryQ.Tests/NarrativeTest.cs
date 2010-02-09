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
    public class NarrativeTest
    {
        [TestMethod]
        public void ExcecutePending()
        {
            Narrative n = new Narrative("Given", 1, "a pending narrative", Narrative.Pend);
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Pending, e.Type);
            Assert.IsNotNull(e.Exception);
        }

        [TestMethod]
        public void ExcecuteFail()
        {
            Narrative n = new Narrative("Given", 1, "a failing narrative", () => Assert.Fail("Fail!"));
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Failed, e.Type);
            Assert.IsNotNull(e.Exception);
        }

        [TestMethod]
        public void ExcecutePass()
        {
            Narrative n = new Narrative("Given", 1, "a passing narrative", () => { });
            Result e = n.Execute();
            Assert.AreEqual(ResultType.Passed, e.Type);
            Assert.IsNull(e.Exception);
        }

        [TestMethod]
        public void ExcecuteNonExecutable()
        {
            Narrative n = new Narrative("As a", 1, "non executable narrative", Narrative.DoNothing);
            Result e = n.Execute();
            Assert.AreEqual(ResultType.NotExecutable, e.Type);
            Assert.IsNull(e.Exception);
        }
    }
}
