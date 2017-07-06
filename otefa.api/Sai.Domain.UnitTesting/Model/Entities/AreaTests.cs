using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.Area;
using Sai.Domain.Model.Exceptions.Category;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class AreaTests
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Should_Create_An_Area()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            // Act

            var area = new Area(description, briefDescription);

            // Assert

            Assert.That(area.Description, Is.EqualTo(description));
            Assert.That(area.BriefDescription, Is.EqualTo(briefDescription));
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Brief_Description_Already_Exists()
        {
            // Arrange

            var userServiceMock = new Mock<IUserService>();


            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var areaMock = new Mock<Area>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(briefDescription)).Returns(areaMock.Object);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantAreaBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Brief_Description_Length_Is_Not_2_Characters()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 3);

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidAreaBriefDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Description_Length_Is_More_Than_30_Characters()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 31);
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidAreaDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Description_Is_Null()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            string description = null;
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullAreaDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = "";
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyAreaDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Brief_Description_Is_Null()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            string briefDescription = null;

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullAreaBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Area_But_Fails_Because_Brief_Description_Is_Empty()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = "";

            // Act

            var caughtException = Assert.Catch(() => new Area(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyAreaBriefDescriptionException>());
        }

    }
}