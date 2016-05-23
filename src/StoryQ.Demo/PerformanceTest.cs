
#if NUNIT
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace StoryQ.Demo
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using StoryQ.Formatting.Parameters;
    using StoryQ.TextualSteps;

    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        [TestCategory("Performance")]
        public void Test1()
        {

            Outcome outcome = new Story("Blah")
                .InOrderTo("Blah")
                .AsA("Blah")
                .IWant("Blah")
                .WithScenario("Blah")
                .Given(this.TheMethodToCall)
                .When(this.TheMethodToCall)
                .Then(this.TheMethodToCall);

            using (new CodeTimer("creating 1000 scenarios"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    outcome = outcome.WithScenario("Blah " + i)
                        .Given(this.TheMethodToCall)
                        .When(this.TheMethodToCall)
                        .Then(this.TheMethodToCall);
                }
            }
            using (new CodeTimer("running 1000 scenarios"))
            {
                outcome.Execute();
            }
        }

        private void TheMethodToCall()
        {

        }
    }

    public class CodeTimer : IDisposable
    {
        private readonly string _message;
        private readonly Stopwatch _sw = new Stopwatch();
        public CodeTimer(string message)
        {
            this._message = message;
            this._sw.Start();
        }

        public void Dispose()
        {
            this._sw.Stop();
            Console.Out.WriteLine("{0} took {1} msec", this._message, this._sw.ElapsedMilliseconds);
        }
    }

}