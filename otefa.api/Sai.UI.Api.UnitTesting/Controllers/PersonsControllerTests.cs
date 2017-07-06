using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class PersonsControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange
            string cuilCuit = "23-12345678-0";
            var PersonServiceMock = new Mock<IPersonService>();
            var PersonMock = new Mock<Person>();
            PersonServiceMock.Setup(x => x.FindPersonByCuitCuil(It.IsAny<string>())).Returns(PersonMock.Object);


            var personsController = new PersonsController(PersonServiceMock.Object);

            // Act

            var Person = personsController.Get(cuilCuit);

            // Assert

            PersonServiceMock.Verify(x => x.FindPersonByCuitCuil(cuilCuit), Times.Once);

            Assert.That(Person, Is.Not.Null);
            Assert.That(Person, Is.EqualTo(PersonMock.Object));

        }

        #endregion Get



    }
}