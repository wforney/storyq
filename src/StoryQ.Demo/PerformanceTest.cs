using System;
using System.Diagnostics;
using System.Reflection;
using StoryQ.Formatting.Parameters;
using StoryQ.TextualSteps;

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
                .Given(TheMethodToCall)
                .When(TheMethodToCall)
                .Then(TheMethodToCall);

            using (new CodeTimer("creating 1000 scenarios"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    outcome = outcome.WithScenario("Blah " + i)
                        .Given(TheMethodToCall)
                        .When(TheMethodToCall)
                        .Then(TheMethodToCall);
                }
            }
            using (new CodeTimer("running 1000 scenarios"))
            {
                outcome.Execute();
            }
        }

        void TheMethodToCall()
        {

        }
    }

    public class CodeTimer : IDisposable
    {
        private readonly string _message;
        private readonly Stopwatch _sw = new Stopwatch();
        public CodeTimer(string message)
        {
            _message = message;
            _sw.Start();
        }

        public void Dispose()
        {
            _sw.Stop();
            Console.Out.WriteLine("{0} took {1} msec", _message, _sw.ElapsedMilliseconds);
        }
    }

}