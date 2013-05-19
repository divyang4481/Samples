using System.Collections.Generic;
using System.Linq;

namespace BusinessApplication.Core
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly IList<Person> personList; 

        public PersonRepository()
        {
            this.personList = new List<Person>
                {
                    new Person { Id = 0, FirstName = "John", LastName = "Doe"},
                    new Person { Id = 1, FirstName = "s", LastName = "XX"},
                    new Person { Id = 2, FirstName = "d", LastName = "ds"}
                };
        }

        public Person GetPerson(int id)
        {
            return personList.FirstOrDefault(p => p.Id == id);
        }
    }
}
