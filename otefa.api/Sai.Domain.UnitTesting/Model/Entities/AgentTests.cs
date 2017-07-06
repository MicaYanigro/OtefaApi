using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Agent;
using Sai.Domain.Model.Exceptions.Person;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class AgentTests
    {

        [Test]
        public void Should_Create_An_Agent()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var agent = new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity);
            // Assert

            Assert.That(agent.Cuil, Is.EqualTo(cuil));
            Assert.That(agent.LastName, Is.EqualTo(lastName));
            Assert.That(agent.Name, Is.EqualTo(name));
            Assert.That(agent.Email, Is.EqualTo(email));

        }

        [Test]
        public void Should_Create_An_Agent_But_Fails_Because_Cuil_Is_Null()
        {
            // Arrange
            var cuil = (string)null;
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullCuilException>());
        }

        [Test]
        public void Should_Create_An_Agent_But_Fails_Because_Cuil_Is_Empty()
        {
            // Arrange
            var cuil = "";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyCuilException>());
        }

        [Test]
        public void Should_Create_An_Agent_But_Fails_Because_Cuil_Format_Is_Invalid()
        {
            // Arrange
            var cuil = "20-1235445645678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidCuilFormatException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_LastName_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = (string)null;
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullLastNameException>());
        }


        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_LastName_Is_Empty()
        {
            // Arrange
            var cuil = "20-12345678-3";
            var lastName = "";
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyLastNameException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_LastName_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 54);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidLastNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Name_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = (string)null;
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullNameException>());
        }


        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Name_Is_Empty()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = "";
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyNameException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Name_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 54);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Email_Is_Null()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = (string)null;
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullEmailException>());
        }


        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Email_Is_Empty()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyEmailException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Email_Lenght_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 55);
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidEmailLenghtException>());
        }

        [Test]
        public void Should_Create_A_Agent_But_Fails_Because_Email_Format_Is_Invalid()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);


            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));
            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidEmailFormatException>());
        }

        [Test]
        public void Should_Create_Agent_But_Fails_Because_Cuil_Already_Exists()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organism = new Mock<Organism>();
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            var contractingMode = new Mock<ContractingMode>();
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = new Mock<StudiesLevel>();
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = new Mock<Country>();
            var organismProvince = new Mock<Province>();
            var addressCity = new string('x', 250);


            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            agentServiceMock.Setup(x => x.IsExistentCuil(cuil)).Returns(true);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Agent(cuil, lastName, name, email, organism.Object,
                            worksInGovernment, legajo, (Ambit)ambit, province.Object, municipality.Object,
                            contractingMode.Object, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies.Object, shortDescription,
                            numberPeopleInCharge, country.Object, organismProvince.Object, addressCity));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantAgentCuilException>());
        }

    }
}