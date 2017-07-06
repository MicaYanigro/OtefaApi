using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class AreaFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

        
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var areaFactory = new AreaFactory();

            // Act

            var area = areaFactory.Create(description, briefDescription);

            // Assert

            Assert.That(area, Is.InstanceOf<Area>());

            Assert.That(area.Description, Is.EqualTo(description));
            Assert.That(area.BriefDescription, Is.EqualTo(briefDescription));
        }
    }
}