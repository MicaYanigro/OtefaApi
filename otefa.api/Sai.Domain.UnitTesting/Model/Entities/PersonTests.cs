using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Person;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class PersonTests
    {

        [Test]
        public void Should_Create_A_Person()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);
            // Act

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            var duration = new Person(cuil, lastName, name, email, (Sex) sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity);

            // Assert

            Assert.That(duration.Cuil, Is.EqualTo(cuil));
            Assert.That(duration.LastName, Is.EqualTo(lastName));
            Assert.That(duration.Name, Is.EqualTo(name));
            Assert.That(duration.Email, Is.EqualTo(email));

        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Cuil_Is_Null()
        {
            // Arrange
            var cuil = (string)null;
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullCuilException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Cuil_Is_Empty()
        {
            // Arrange
            var cuil = "";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyCuilException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Cuil_Format_Is_Invalid()
        {
            // Arrange
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidPersonCuilFormatException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_LastName_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = (string)null;
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullLastNameException>());
        }


        [Test]
        public void Should_Create_A_Person_But_Fails_Because_LastName_Is_Empty()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = "";
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyLastNameException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_LastName_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 51);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidPersonLastNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Name_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = (string)null;
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullNameException>());
        }


        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Name_Is_Empty()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = "";
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyNameException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Name_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 51);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidPersonNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Email_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = (string)null;
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullEmailException>());
        }


        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Email_Is_Empty()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyEmailException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Email_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 51);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidEmailLenghtException>());
        }

        [Test]
        public void Should_Create_A_Person_But_Fails_Because_Email_Format_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidEmailFormatException>());
        }

        [Test]
        public void Should_Create_Person_But_Fails_Because_Cuil_Already_Exists()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = new Mock<Country>();
            var province = new Mock<Province>();
            var addressCity = new string('x', 250);

            var personMock = new Mock<Person>();
            var personServiceMock = new Mock<IPersonService>();
            personServiceMock.Setup(x => x.IsExistentCuil(cuil)).Returns(true);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IPersonService>()).Returns(personServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Person(cuil, lastName, name, email, (Sex)sex, dateOfBirth,
                         lastLevelStudies.Object, relatedTasksDescription,
                         numberPeopleInCharge, country.Object, province.Object, addressCity));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantPersonCuilException>());
        }
    }
}