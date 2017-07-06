using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System.Linq;

namespace Sai.Infrastructure.IntegrationTesting
{
    [TestFixture]
    public class CategoryRepositoryTests
    {

        [Test]
        public void Should_Add_And_Get_A_Category()
        {
            // Arrange

            var categoryDomainMock = new Mock<Category>();
            var categoryServicesMock = new Mock<ICategoryService>();
            var containerMock = new Mock<IContainer>();

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            categoryServicesMock.Setup(x => x.Create(description, briefDescription)).Returns(categoryDomainMock.Object);
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServicesMock.Object);
            Container.Current = containerMock.Object;

            var category = new Category(description, briefDescription);

            var categoryRepository = CreateRepository();

            // Act

            var categories1 = categoryRepository.All().Count();

            categoryRepository.Add(category);

            var categories2 = categoryRepository.All().Count();

            // Assert

            Assert.That(categories2, Is.EqualTo(categories1 + 1));
        }

        private ICategoryRepository CreateRepository()
        {
            RepositoryContextEF repositoryContext = new RepositoryContextEF();
            return new CategoryRepositoryEF(repositoryContext);
        }

    }
}