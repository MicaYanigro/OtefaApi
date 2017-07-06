using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using System;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_A_Category()
        {
            // Arrange

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var categoryService = new CategoryService();

            categoryService.CategoryRepository = categoryRepositoryMock.Object;

            var categoryMock = new Mock<Category>();

            var categoryFactoryMock = new Mock<ICategoryFactory>();
            categoryFactoryMock.Setup(x => x.Create(description, briefDescription)).Returns(categoryMock.Object);

            categoryService.CategoryFactory = categoryFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            categoryService.UserService = userServiceMock.Object;

            // Act

            var createdCategory = categoryService.Create(description, briefDescription);

            // Assert

            categoryFactoryMock.Verify(x => x.Create(description, briefDescription), Times.Once);
            categoryRepositoryMock.Verify(x => x.Add(categoryMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }


        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_Categories()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            CategoryService categoryService = new CategoryService();
            categoryService.CategoryRepository = categoryRepositoryMock.Object;

            // Act

            var categories = categoryService.GetAll();

            // Assert

            Assert.That(categories, Is.InstanceOf<List<Category>>());
            Assert.That(categories, Is.Not.Null);

            categoryRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}