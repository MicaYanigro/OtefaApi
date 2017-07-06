using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class CategoriesControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Category)null);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICategoryService>()).Returns(categoryServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var categories = new List<Category>()
            {
                new Category(description, briefDescription)
            };

            categoryServiceMock.Setup(x => x.GetAll()).Returns(categories);

            var categoriesController = new CategoriesController(categoryServiceMock.Object);

            // Act

            var allCategories = categoriesController.Get();

            // Assert

            categoryServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allCategories, Is.Not.Null);
            Assert.That(allCategories, Is.InstanceOf<IEnumerable<Category>>());
            Assert.That(allCategories.Count(), Is.EqualTo(1));
            Assert.That(allCategories, Has.Exactly(1).Property("Description").EqualTo(description));
            Assert.That(allCategories, Has.Exactly(1).Property("BriefDescription").EqualTo(briefDescription));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var categoryDomainMock = new Mock<Category>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object);

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var categoryViewModelMock = new CategoryViewModel
            {
                Description = description,
                BriefDescription = briefDescription
            };

            categoryServiceMock.Setup(x => x.Create(description, briefDescription)).Returns(categoryDomainMock.Object);

            categoriesController.Request = new HttpRequestMessage();
            categoriesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = categoriesController.Post(categoryViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            categoryServiceMock.Verify(x => x.Create(description, briefDescription), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var categoryServiceMock = new Mock<ICategoryService>();
            var categoriesController = new CategoriesController(categoryServiceMock.Object);

            var description = new string('x', 30);
            var briefDescription = new string('x', 1);

            var categoryViewModelMock = new CategoryViewModel
            {
                Description = description,
                BriefDescription = briefDescription
            };

            categoryServiceMock.Setup(x => x.Create(description, briefDescription)).Throws(new ExceptionBase());

            categoriesController.Request = new HttpRequestMessage();
            categoriesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = categoriesController.Post(categoryViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            categoryServiceMock.Verify(x => x.Create(description, briefDescription), Times.Once);

        }

        #endregion Post

    }
}