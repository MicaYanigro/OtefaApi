using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions.Person;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class PersonFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = 1;
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var personFactory = new PersonFactory();
            var countryMock = new Mock<Country>();

            var CountryRepositoryMock = new Mock<ICountryRepository>();
            CountryRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(countryMock.Object);

            var provinceMock = new Mock<Province>();

            var ProvinceRepositoryMock = new Mock<IProvinceRepository>();
            ProvinceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(provinceMock.Object);

            var studiesLevelMock = new Mock<StudiesLevel>();

            var StudiesLevelRepositoryMock = new Mock<IStudiesLevelRepository>();
            StudiesLevelRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(studiesLevelMock.Object);
            personFactory.CountryRepository = CountryRepositoryMock.Object;
            personFactory.StudiesLevelRepository = StudiesLevelRepositoryMock.Object;
            personFactory.ProvinceRepository = ProvinceRepositoryMock.Object;

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var person = personFactory.Create(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity);

            // Assert

            Assert.That(person, Is.InstanceOf<Person>());
            Assert.That(person.Cuil, Is.EqualTo(cuil));
            Assert.That(person.LastName, Is.EqualTo(lastName));
            Assert.That(person.Name, Is.EqualTo(name));
            Assert.That(person.Email, Is.EqualTo(email));
        }

        [Test]
        public void Should_Create_But_Fails_Because_Existant_Cuil()
        {
            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = 1;
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var personFactory = new PersonFactory();

            var personMock = new Mock<Person>();
            var countryRepositoryMock = new Mock<ICountryRepository>();
            var provinceRepositoryMock = new Mock<IProvinceRepository>();

            var personServiceMock = new Mock<IPersonService>();
            var countryServiceMock = new Mock<ICountryService>();
            var provinceServiceMock = new Mock<IProvinceService>();


            personServiceMock.Setup(x => x.IsExistentCuil(It.IsAny<string>())).Returns(true);
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            containerMock.Setup(x => x.Resolve<ICountryService>()).Returns(countryServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IProvinceService>()).Returns(provinceServiceMock.Object);

            containerMock.Setup(x => x.Resolve<ICountryRepository>()).Returns(countryRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IProvinceRepository>()).Returns(provinceRepositoryMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => personFactory.Create(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<ExistantPersonCuilException>());

        }
    }
}