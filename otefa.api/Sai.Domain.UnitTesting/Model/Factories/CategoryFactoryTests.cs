using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class CategoryFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var categoryFactory = new CategoryFactory();

            // Act

            var category = categoryFactory.Create(description, briefDescription);

            // Assert

            Assert.That(category, Is.InstanceOf<Category>());

            Assert.That(category.Description, Is.EqualTo(description));
            Assert.That(category.BriefDescription, Is.EqualTo(briefDescription));
        }
    }
}