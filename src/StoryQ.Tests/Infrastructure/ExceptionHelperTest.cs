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

namespace StoryQ.Tests.Infrastructure
{
    [TestClass]
    public class ExceptionHelperTest
    {
        [TestMethod]
        public void BuildException()
        {
            var exception = StoryQSettings.PendingExceptionBuilder("foo", null);
#if NUNIT
    //we use ignore not inconclusive for nunit (most runners turn this yellow, whereas inconclusive is red)
            Assert.IsAssignableFrom<IgnoreException>(exception);
#else
            Assert.IsTrue(exception is AssertInconclusiveException);
#endif

        }
    }
}