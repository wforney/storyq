
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
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using StoryQ.Formatting;
    using StoryQ.Formatting.Methods;
    using StoryQ.Formatting.Parameters;

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
        public void EnumerableArgumentWithMultipleElements()
        {
            Action<int[]> a = EnumerableArgument;
            Assert.AreEqual("enumerable argument([1, 2, 3])", Formatter.FormatMethod(a, new [] {1, 2, 3}));
        }
		
		[TestMethod]
        public void EnumerableArgumentWithSingleElement()
        {
            Action<int[]> a = EnumerableArgument;
            Assert.AreEqual("enumerable argument([1])", Formatter.FormatMethod(a, new [] {1}));
        }
		
		[TestMethod]
        public void EnumerableArgumentWithNoElements()
        {
            Action<int[]> a = EnumerableArgument;
            Assert.AreEqual("enumerable argument([])", Formatter.FormatMethod(a, new int [0]));
        }
		
		[TestMethod]
        public void NestedEnumerable()
        {
		    var v = new[]
		                {
		                    new[] {1, 2, 3},
		                    new[] {4, 5, 6},
		                    new[] {7, 8, 9},
		                };

            var arguments = new object[] { v }; // because of the params argument to FormatMethod
            Assert.AreEqual("two dimensional array([[1, 2, 3], [4, 5, 6], [7, 8, 9]])", Formatter.FormatMethod((Action<int[][]>)TwoDimensionalArray, arguments));
        }	
	
		[TestMethod]
        public void StringNotEnumerable()
        {
            Assert.AreEqual("pass a string(blah)", Formatter.FormatMethod((Action<string>)PassAString, "blah"));
        }	
	
		[TestMethod]
        public void EmptyString()
        {
            Assert.AreEqual("pass a string(\"\")", Formatter.FormatMethod((Action<string>)PassAString, ""));
        }	

		[TestMethod]
        public void WhiteSpaceString()
        {
            Assert.AreEqual("pass a string(\" \")", Formatter.FormatMethod((Action<string>)PassAString, "   \n\t "));
        }	
	
		[TestMethod]
        public void NullString()
        {
            Assert.AreEqual("pass a string({NULL})", Formatter.FormatMethod((Action<string>)PassAString, new object[]{null}));
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
        
        [TestMethod]
        public void Apostrophes()
        {
            Action a = ThisWontFormat;
            Assert.AreEqual("this won't format", Formatter.FormatMethod(a));
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

        private static void EnumerableArgument<T>(IEnumerable<T> enumerable)
        {
            
        }

        private static void TwoDimensionalArray(int[][] array)
        {
            
        }

        private static void AgeIs_Years(int age)
        {
            
        }

        private static void PassAString(string s)
        {
            
        }

        private static void SilenceTheFirstArgument_([Silent] string s, int i)
        {
            
        }

        private static void ThisWontFormat()
        {
            
        }

        private class MyMethodFormatAttribute : MethodFormatAttribute
        {
            public override string Format(MethodInfo method, IEnumerable<string> parameters) => "4";
        }
    }
}
