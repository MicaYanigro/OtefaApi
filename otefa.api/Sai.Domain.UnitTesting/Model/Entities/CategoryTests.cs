using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.Category;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class CategoryTests
    {

        [Test]
        public void Should_Create_A_Category()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            // Act

            var category = new Category(description, briefDescription);

            // Assert

            Assert.That(category.Description, Is.EqualTo(description));
            Assert.That(category.BriefDescription, Is.EqualTo(briefDescription));
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Brief_Description_Already_Exists()
        {
            // Arrange

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var categoryMock = new Mock<Category>();

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(briefDescription)).Returns(categoryMock.Object);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantCategoryBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Brief_Description_Length_Is_Not_2_Characters()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 3);

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidCategoryBriefDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Description_Length_Is_More_Than_30_Characters()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 31);
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidCategoryDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Description_Is_Null()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            string description = null;
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullCategoryDescriptionException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = "";
            var briefDescription = new string('x', 2);

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyCategoryDescriptionException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Brief_Description_Is_Null()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            string briefDescription = null;

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullCategoryBriefDescriptionException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Brief_Description_Is_Empty()
        {
            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = "";

            // Act

            var caughtException = Assert.Catch(() => new Category(description, briefDescription));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyCategoryBriefDescriptionException>());
        }


        #region UPDATE

        //public void Should_Update_A_Category()
        //{
        //    // Arrange

        //    var categoryServiceMock = new Mock<ICategoryService>();
        //    categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

        //    var containerMock = new Mock<IContainer>();
        //    containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
        //    Container.Current = containerMock.Object;

        //    var description = new string('x', 30);
        //    var briefDescription = new string('x', 2);

        //    // Act

        //    var category = new Category(description, briefDescription);

        //    // Assert

        //    Assert.That(category.Description, Is.EqualTo(description));
        //    Assert.That(category.BriefDescription, Is.EqualTo(briefDescription));
        //}

        #endregion
    }
}