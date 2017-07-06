using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class PersonServiceTests
    {

        #region Get

        [Test]
        public void Should_Get_By_CuilCuit()
        {
            // Arrange
            string cuilcuit = string.Empty;
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IPersonRepository>();
            personRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            personRepositoryMock.Setup(x => x.GetByCuilCuit(It.IsAny<string>())).Returns(personMock.Object);
            PersonService PersonService = new PersonService();
            PersonService.PersonRepository = personRepositoryMock.Object;

            // Act

            var Person = PersonService.FindPersonByCuitCuil(cuilcuit);

            // Assert

            Assert.That(Person, Is.Not.Null);
            Assert.That(Person, Is.EqualTo(personMock.Object));

            personRepositoryMock.Verify(x => x.GetByCuilCuit(cuilcuit), Times.Once);
        }

        #endregion Get

    }
}