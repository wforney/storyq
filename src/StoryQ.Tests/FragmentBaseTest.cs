using System;
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
    public class FragmentBaseTest
    {


        [TestMethod]
        public void CreateWithJustAnAction()
        {
            Action a = SimpleCamelCase;
            string s = Formatter.FormatMethod(a);

            Assert.AreEqual("simple camel case",s);
        }
        
        [TestMethod]
        public void CreateWithJustAnActionOnAnotherObject()
        {
            FragmentBaseTest t = new FragmentBaseTest();
            Action a = t.SimpleCamelCase;
            string s = Formatter.FormatMethod(a);

            Assert.AreEqual("simple camel case",s);
        }

        [TestMethod]
        public void ViaFluentInterface()
        {
            Scenario f = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = f.Given(SimpleCamelCase).Narrative;
            Assert.AreEqual("simple camel case", narrative.Text);
        }
        
        [TestMethod]
        public void ViaFluentInterfaceWithArgument()
        {
            Scenario s = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = s.Given(SimpleCamelCaseWithArgument, 5).Narrative;
            Assert.AreEqual("simple camel case with argument(5)", narrative.Text);
        }

        [TestMethod]
        public void ViaFluentInterfaceWithArgumentInline()
        {
            Scenario s = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = s.Given(AgeIs_Years, 5).Narrative;
            Assert.AreEqual("age is 5 years", narrative.Text);
        }

        [TestMethod]
        public void ViaFluentInterfaceWithFormattedBooleanArgumentInline()
        {
            Scenario s = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = s.Given(TheUser_BeNotified, false).Narrative;
            Assert.AreEqual("the user should not be notified", narrative.Text);
        }

        [TestMethod]
        public void ViaFluentInterfaceWithCustomFormatter()
        {
            Scenario s = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = s.Given(SomethingRandom).Narrative;
            Assert.AreEqual("4", narrative.Text);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ViaFluentInterfaceWithTooManyArgumentsInline()
        {
            Scenario s = new Scenario(new Narrative("", 1, "", () => { }));
            Narrative narrative = s.Given(Age_Is_Years, 5).Narrative;
            Assert.AreEqual("age is 5 years", narrative.Text);
        }

        [MyMethodFormat]
        private static void SomethingRandom()
        {
           
        }

        private void Age_Is_Years(int age)
        {
            
        }

        private void SimpleCamelCase()
        {
            
        }


        private static void TheUser_BeNotified([BooleanParameterFormat("should", "should not")]bool b)
        {

        }


        private static void SimpleCamelCaseWithArgument(int x)
        {
            
        }

        private static void AgeIs_Years(int age)
        {
            
        }

        private class MyMethodFormatAttribute : MethodFormatAttribute
        {
            public override string Format(MethodInfo method, IEnumerable<string> parameters)
            {
                return "4"; //randomly chosen via dice roll
            }
        }
    }
}
