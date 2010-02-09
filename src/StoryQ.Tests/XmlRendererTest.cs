using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using StoryQ.Execution;
using StoryQ.Execution.Rendering;
using StoryQ.Formatting;
using StoryQ.Formatting.Methods;
using StoryQ.Formatting.Parameters;
using System.Collections.Generic;
using System.Reflection;

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
            c.GetOrCreateElementForMethodInfo(new Action(new NarrativeTest().ExcecuteFail).Method);

            const string expected = @"<root><Project Name=""StoryQ.Tests""><Namespace Name=""StoryQ.Tests""><Class Name=""XmlRendererTest""><Method Name=""TestCategoriser"" /><Method Name=""RenderSomeResults"" /></Class><Class Name=""NarrativeTest""><Method Name=""ExcecuteFail"" /></Class></Namespace></Project></root>";
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
                .Given("thing").Pass()
                .When("something").Pend()
                .Then("something else", () => { throw new Exception("moo"); });


            var results = v.SelfAndAncestors().Reverse().Select(x => x.Narrative.Execute());
            new XmlRenderer(e).Render(results);
            Assert.IsTrue(e.Descendants("Result").Count()==8);
            Assert.AreEqual("Failed", (string)e.Descendants("Result").Last().Attribute("Type"));
        }

       
    }
}