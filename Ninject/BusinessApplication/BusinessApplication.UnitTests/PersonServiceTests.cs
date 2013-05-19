using BusinessApplication.Core;
using Moq;
using Ninject;
using Xunit;

namespace BusinessApplication.UnitTests
{
    public class PersonServiceTests
    {
        [Fact]
        public void should_be_able_to_call_PersonService_and_get_person()
        {
            // Arrange
            var expected = new Person {Id = 0, FirstName = "John", LastName = "Doe"};
            
            // Using Ninject
            //var kernel = new StandardKernel(new CoreModule());
            //var target = kernel.Get<IPersonService>();

            // Using Moq
            var personRepositoryMock = new Mock<IPersonRepository>();
            personRepositoryMock
                .Setup(pr => pr.GetPerson(0))
                .Returns(new Person {Id = 0, FirstName = "John", LastName = "Doe"});

            var target = new PersonService(personRepositoryMock.Object);

            // Act
            var result = target.GetPerson(0);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.FirstName,  result.FirstName);
            Assert.Equal(expected.LastName, result.LastName);
        }
    }
}
