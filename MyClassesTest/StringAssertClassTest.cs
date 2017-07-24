using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    /// <summary>
    /// Summary description for StringAssertClassTest
    /// </summary>
    [TestClass]
    public class StringAssertClassTest
    {
        [TestMethod]
        [Owner("StringAssertClass Owner")]
        public void ContainsTest()
        {
            string str1 = "John Dough";
            string str2 = "n D";

            StringAssert.Contains(str1, str2); //test to see if str1 contains str2
        }

        [TestMethod]
        [Owner("StringAssertClass Owner")]
        public void StartsWithTest()
        {
            string str1 = "John Dough";
            string str2 = "Jo";

            StringAssert.StartsWith(str1, str2); //test to see if str1 begins with str2
        }

        [TestMethod]
        [Owner("StringAssertClass Owner")]
        public void IsAllLowerCaseTest()
        {
            Regex r = new Regex(@"^([^A-Z])+$"); //creates a var that's set to regex for all lower case letters

            StringAssert.Matches("i only contain lower case letters", r); //compares "all lower case" with the rules of the regex
        }

        [TestMethod]
        [Owner("StringAssertClass Owner")]
        public void IsNotAllLowerCaseTest()
        {
            Regex r = new Regex(@"^([^A-Z])+$"); //regex rules for all lower case

            StringAssert.DoesNotMatch("I contain some Upper case letters", r);
        }
    }
}
