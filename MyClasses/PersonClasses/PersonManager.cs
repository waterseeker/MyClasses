using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses.PersonClasses
{
    public class PersonManager
    {
        public Person CreatePerson(
            string first, 
            string last,
            bool isSupervisor)
        {
            Person ret = null; //this is initialized as null because we want to return null if there is no name passed in. 

            if (!string.IsNullOrEmpty(first))
            {
                if (isSupervisor)
                {
                    ret = new Supervisor();
                }
                else
                {
                    ret = new Employee();
                }

                //Assign variables
                ret.FirstName = first;
                ret.LastName = last;
            }

            return ret;
        }

        public List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person() { FirstName = "John", LastName = "Dough" });
            people.Add(new Person() { FirstName = "Someone", LastName = "Else" });
            people.Add(new Person() { FirstName = "Another", LastName = "Person" });

            return people;
        }

        public List<Person> GetSupervisors()
        {
            List<Person> people = new List<Person>();

            people.Add(CreatePerson("John", "Dough", true));
            people.Add(CreatePerson("Someone", "Else", true));

            return people;
        }

    }
}
