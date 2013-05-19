namespace BusinessApplication.Core
{
    public interface IPersonService
    {
        Person GetPerson(int id);
    }

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public Person GetPerson(int id)
        {
            return personRepository.GetPerson(id);
        }
    }
}
