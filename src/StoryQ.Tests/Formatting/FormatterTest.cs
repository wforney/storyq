﻿using System;
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

namespace StoryQ.Tests.Formatting
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FormatterTest
    {
        [TestMethod]
        public void NoArguments()
        {
            Action a = SimpleCamelCase;
            Assert.AreEqual("simple camel case", Formatter.FormatMethod(a));
        }
        
        [TestMethod]
        public void SuffixedArgument()
        {
            Action<int> a = SimpleCamelCaseWithArgument;
            Assert.AreEqual("simple camel case with argument(5)", Formatter.FormatMethod(a, 5));
        }

        [TestMethod]
        public void InlineArgument()
        {
            Action<int> a = AgeIs_Years;
            Assert.AreEqual("age is 5 years", Formatter.FormatMethod(a, 5));
        }

        [TestMethod]
        public void FormattedInlineArgument()
        {
            Action<bool> a = TheUser_BeNotified;
            Assert.AreEqual("the user should not be notified", Formatter.FormatMethod(a, false));
        }

        [TestMethod]
        public void CustomMethodFormatter()
        {
            Action a = SomethingRandom;
            Assert.AreEqual("4", Formatter.FormatMethod(a));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManyArgumentsInline()
        {
            Action<int> a = Age_Is_Years;
            Formatter.FormatMethod(a, 5);
        }

        [TestMethod]
        public void SilentArgument()
        {
            Action<string, int> a = SilenceTheFirstArgument_;
            Assert.AreEqual("silence the first argument 1", Formatter.FormatMethod(a, "hellooo?", 1));
        }

        [MyMethodFormat]
        private static void SomethingRandom()
        {
           
        }

        private static void Age_Is_Years(int age)
        {
            
        }

        private static void SimpleCamelCase()
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

        private static void SilenceTheFirstArgument_([Silent] string s, int i)
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