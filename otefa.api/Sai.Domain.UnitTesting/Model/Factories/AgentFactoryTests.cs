using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class AgentFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var cuil = "20-12345678-3";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var organismID = 1;
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = 1;
            var municipality = 1;
            var contractingMode = 1;
            var workEmail = "test@123.com";
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = 1;
            var organismProvince = 1;
            var addressCity = new string('x', 250);

            var AgentFactory = new AgentFactory();

            var countryMock = new Mock<Country>();

            var CountryRepositoryMock = new Mock<ICountryRepository>();
            CountryRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(countryMock.Object);

            var provinceMock = new Mock<Province>();

            var ProvinceRepositoryMock = new Mock<IProvinceRepository>();
            ProvinceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(provinceMock.Object);

            var municipalityMock = new Mock<Municipality>();

            var municipalityRepositoryMock = new Mock<IMunicipalityRepository>();
            municipalityRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(municipalityMock.Object);

            var contractingModeMock = new Mock<ContractingMode>();

            var contractingModeRepositoryMock = new Mock<IContractingModeRepository>();
            contractingModeRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(contractingModeMock.Object);

            var studiesLevelMock = new Mock<StudiesLevel>();

            var StudiesLevelRepositoryMock = new Mock<IStudiesLevelRepository>();
            StudiesLevelRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(studiesLevelMock.Object);
            AgentFactory.CountryRepository = CountryRepositoryMock.Object;
            AgentFactory.StudiesLevelRepository = StudiesLevelRepositoryMock.Object;
            AgentFactory.ProvinceRepository = ProvinceRepositoryMock.Object;
            AgentFactory.ContractingModeRepository = contractingModeRepositoryMock.Object;


            var organismMock = new Mock<Organism>();
            var organismRepositoryMock = new Mock<IOrganismRepository>();

            organismRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(organismMock.Object);

            var agentMock = new Mock<Agent>();
            var agentServiceMock = new Mock<IAgentService>();
            var userServiceMock = new Mock<IUserService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAgentService>()).Returns(agentServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            AgentFactory.OrganismRepository = organismRepositoryMock.Object;
            AgentFactory.MunicipalityRepository = municipalityRepositoryMock.Object;

            // Act

            var Agent = AgentFactory.Create(cuil, lastName, name, email, organismID,
                            worksInGovernment, legajo, ambit, province, municipality,
                            contractingMode, workEmail, (Sex)sex, dateOfBirth,
                            lastLevelStudies, shortDescription,
                            numberPeopleInCharge, country, organismProvince, addressCity);

            // Assert

            Assert.That(Agent, Is.InstanceOf<Agent>());
            Assert.That(Agent.Cuil, Is.EqualTo(cuil));
            Assert.That(Agent.LastName, Is.EqualTo(lastName));
            Assert.That(Agent.Name, Is.EqualTo(name));
            Assert.That(Agent.Email, Is.EqualTo(email));
            Assert.That(Agent.Organism, Is.InstanceOf<Organism>());
        }
    }
}