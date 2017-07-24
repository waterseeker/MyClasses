using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using MyClasses.PersonClasses;
using System.Collections.Generic;

namespace MyClassesTest
{
    [TestClass]
    public class CollectionAssertClassTest
    {
        /// <summary>
        /// NOTE: We want this test to fail to illustrate Equality means collections reference the same object
        /// </summary>
        [TestMethod]
        [Owner("CollectionsAssertClassTest Owner")]
        public void AreCollectionsEqualFailsBecauseNoComparerTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person() { FirstName = "John", LastName = "Dough" });
            peopleExpected.Add(new Person() { FirstName = "Someone", LastName = "Else" });
            peopleExpected.Add(new Person() { FirstName = "Another", LastName = "Person" });

            peopleActual = mgr.GetPeople(); //this is calling a method we defined on the PersonManager that creates a list exactly like
            //the one we created on peopleExpected above. 
            //by default it compares the person objects to see if they are exactly equal, i.e. the same object. This test fails because 
            //even though both lists contain exactly the same data, they are different objects. 
            CollectionAssert.AreEqual(peopleExpected, peopleActual, "This test is expected to fail because we're comparing different objects for equality");
        }

        [TestMethod]
        [Owner("CollectionsAssertClassTest Owner")]
        public void AreCollectionsEqualWithComparerTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person() { FirstName = "John", LastName = "Dough" });
            peopleExpected.Add(new Person() { FirstName = "Someone", LastName = "Else" });
            peopleExpected.Add(new Person() { FirstName = "Another", LastName = "Person" });

            peopleActual = mgr.GetPeople();

            //provide your own "Comparer" to determine equality
            CollectionAssert.AreEqual(peopleExpected, peopleActual,
                Comparer<Person>.Create((x, y) =>
                    x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
        }

        [TestMethod]
        [Owner("CollectionsAssertClassTest Owner")]
        public void AreCollectionsEqualTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleActual = mgr.GetPeople();
            peopleExpected = peopleActual;

            CollectionAssert.AreEqual(peopleExpected, peopleActual); //this will pass because both references are pointing to the same object. 
        }

        [TestMethod]
        [Owner("CollectionsAssertClassTest Owner")]
        public void AreCollectionsAreEquivalentTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleActual = mgr.GetPeople();
            //adds the same Person objects to peopleExpected, but in a different order. 

            peopleExpected.Add(peopleActual[1]);
            peopleExpected.Add(peopleActual[0]);
            peopleExpected.Add(peopleActual[2]);

            //checks for same objects, but in any order.
            CollectionAssert.AreEquivalent(peopleExpected, peopleActual); 
        }

        [TestMethod]
        [Owner("CollectionsAssertClassTest Owner")]
        public void IsCollectionOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleActual = new List<Person>();

            peopleActual = mgr.GetSupervisors(); //this is calling the GetSupervisors we defined in PersonManager 
            //which will return a list of supervisors

            //checks to see if all items in the collection are of type Supervisor
            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));

        }
    }
}
