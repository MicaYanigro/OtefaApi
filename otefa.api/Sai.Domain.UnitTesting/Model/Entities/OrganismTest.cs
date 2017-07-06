using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.Organism;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class OrganismTests
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Should_Create_An_Organism()
        {
            // Arrange

            var userServiceMock = new Mock<IUserService>();

            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();

            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            Container.Current = containerMock.Object;

            var name = new string('x', 30);
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act

            var Organism = new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC);

            // Assert

            Assert.That(Organism.Name, Is.EqualTo(name));
            Assert.That(Organism.BriefDescription, Is.EqualTo(briefDescription));
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Brief_Description_Already_Exists()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var name = new string('x', 30);
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            var OrganismMock = new Mock<Organism>();

            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(briefDescription)).Returns(OrganismMock.Object);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantOrganismBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Brief_Description_Length_Is_Not_5_Characters()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            var name = new string('x', 30);
            var briefDescription = new string('x', 5);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidOrganismBriefDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Description_Length_Is_More_Than_30_Characters()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            var name = new string('x', 31);
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidOrganismNameLengthException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Description_Is_Null()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;


            string name = null;
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act
            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullOrganismNameException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            string name = "";
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyOrganismNameException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Brief_Description_Is_Null()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            var name = new string('x', 30);
            string briefDescription = null;
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;


            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullOrganismBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Organism_But_Fails_Because_Brief_Description_Is_Empty()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var OrganismServiceMock = new Mock<IOrganismService>();
            OrganismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Organism)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IOrganismService>()).Returns(OrganismServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            var name = new string('x', 30);
            var briefDescription = "";
            int sector = 1;
            int level = 1;
            var ctc = new Mock<User>();
            var province = new Mock<Province>();
            var municipality = new Mock<Municipality>();
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            // Act

            var caughtException = Assert.Catch(() => new Organism(name, briefDescription, (Sector)sector, (Level)level, ctc.Object, province.Object, municipality.Object, commissionWithoutPAC, attendanceConstancyWithoutPAC));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyOrganismBriefDescriptionException>());
        }

    }
}