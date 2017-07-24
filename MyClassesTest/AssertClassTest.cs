using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using MyClasses.PersonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassesTest

{
    [TestClass]
    public class AssertClassTest
    {
        #region AreEqual/AreNotEqual Tests
        [TestMethod]
        [Owner("AssertClass Owner")]

        public void AreEqualTest()
        {
            string str1 = "Paul";
            string str2 = "Paul";

            Assert.AreEqual(str1, str2); //by default the string AreEqual method is case sensitive
        }

        [TestMethod]
        [Owner("AssertClass Owner")]
        [ExpectedException(typeof(AssertFailedException))]

        public void AreEqualCaseSensitiveTest()
        {
            string str1 = "Paul";
            string str2 = "paul";

            Assert.AreEqual(str1, str2, false); //adding the third param of "false" here makes the comparison case insensitive
        }

        [TestMethod]
        [Owner("AssertClass Owner")]
public void AreNotEqualTest()
        {
            string str1 = "Paul";
            string str2 = "Leilani";

            Assert.AreNotEqual(str1, str2);
        }
        #endregion

        #region AreSame/AdreNotSame Tests
        [TestMethod]
        [Owner("AreSame/AreNotSame Test Owner")]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y); //since both of these vars point to the same object, this assert will succeed. 
        }

        [TestMethod]
        [Owner("AreSame/AreNotSame Test Owner")]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y); //since these are different objects, they are not the same and this test will succeed. They are the
            //same type but not the same object. 
        }
        #endregion

        #region IsInstanceOfType Test
        [TestMethod]
        [Owner("IsInstanceOfType Owner")]

        public void IsInstanceOfTypeTest()
        {
            PersonManager mgr = new PersonManager(); //instantiates a PersonManager
            Person per; //creates a person

            per = mgr.CreatePerson("John", "Doe", true); //calls the CreatePerson method of the PersonManager and passes it args
            //for firstname, lastname, and whether or not the person is a supervisor. 

            Assert.IsInstanceOfType(per, typeof(Supervisor));
        }

        [TestMethod]
        [Owner("IsInstanceOfType Owner")]

        public void IsNotInstanceOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("John", "Doe", false);

            Assert.IsNotInstanceOfType(per, typeof(Supervisor));
        }
        #endregion

        #region IsNull Test
        [TestMethod]
        [Owner("IsNull Owner")]

        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("", "Doe", false);

            Assert.IsNull(per); //since we passed in an empty string for the firstname, we should return a null from the constructor
        }
        #endregion
    }
}
