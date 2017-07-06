using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class OrganismFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);


            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            var name = new string('x', 30);
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            int ctc = 1;
            int province = 1;
            int municipality = 1;
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            var OrganismFactory = new OrganismFactory();

            var municipalityMock = new Mock<Municipality>();
            var municipalityRepositoryMock = new Mock<IMunicipalityRepository>();

            municipalityRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(municipalityMock.Object);

            var userMock = new Mock<User>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(userMock.Object);

            var provinceMock = new Mock<Province>();
            var provinceRepositoryMock = new Mock<IProvinceRepository>();

            provinceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(provinceMock.Object);


            OrganismFactory.MunicipalityRepository = municipalityRepositoryMock.Object;
            OrganismFactory.ProvinceRepository = provinceRepositoryMock.Object;
            OrganismFactory.UserRepository = userRepositoryMock.Object;
            // Act

            var Organism = OrganismFactory.Create(name, briefDescription, sector, level, ctc, province, municipality, commissionWithoutPAC, attendanceConstancyWithoutPAC);

            // Assert

            Assert.That(Organism, Is.InstanceOf<Organism>());

            Assert.That(Organism.Name, Is.EqualTo(name));
            Assert.That(Organism.BriefDescription, Is.EqualTo(briefDescription));
        }
    }
}