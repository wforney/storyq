using System;
using System.Linq;
using System.Xml.Linq;
using StoryQ.Execution.Rendering;
using System.Reflection;

using StoryQ.Infrastructure;

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

namespace StoryQ.Tests.Execution.Rendering
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class XmlRendererTest
    {


        [TestMethod]
        public void TestCategoriser()
        {
            XElement e = new XElement("root");
            XmlCategoriser c = new XmlCategoriser(e);
            c.GetOrCreateElementForMethodInfo(MethodBase.GetCurrentMethod());
            c.GetOrCreateElementForMethodInfo(new Action(RenderSomeResults).Method);

            const string expected = @"<root><Project Name=""StoryQ.Tests""><Namespace Name=""StoryQ.Tests.Execution.Rendering""><Class Name=""XmlRendererTest""><Method Name=""TestCategoriser"" /><Method Name=""RenderSomeResults"" /></Class></Namespace></Project></root>";
            Assert.AreEqual(expected, e.ToString(SaveOptions.DisableFormatting));
        }

        [TestMethod]
        public void RenderSomeResults()
        {
            XElement e = new XElement("Story", new XAttribute("Name", "Story"));
            var v = new Story("Story")
                .InOrderTo("benefit")
                .AsA("role")
                .IWant("feature")
                .WithScenario("scenario")
                .Given(Thing)
                .When(Something)
                .Then(SomethingElse);


            var results = ((IStepContainer)v).SelfAndAncestors().Reverse().Select(x => x.Step.Execute());
            new XmlRenderer(e).Render(results);
            
            Assert.IsTrue(e.Descendants("Result").Count()==8);
            Assert.AreEqual("Failed", (string)e.Descendants("Result").Last().Attribute("Type"));
        }

        private void SomethingElse()
        {
            throw new Exception("moo");
        }

        private void Something()
        {
            throw new NotImplementedException();
        }

        private void Thing()
        {
            

        }
    }
}