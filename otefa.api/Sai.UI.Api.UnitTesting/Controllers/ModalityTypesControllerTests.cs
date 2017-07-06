using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.ModalityTypes;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class ModalityTypesControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange

            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var modalityTypesController = new ModalityTypesController(modalityTypeServiceMock.Object);

            var description = new string('x', 30);

            var modalityType = new List<ModalityType>()
            {
                new ModalityType(description)
            };

            modalityTypeServiceMock.Setup(x => x.GetAll()).Returns(modalityType);

            // Act

            var allModalityTypes = modalityTypesController.Get();

            // Assert

            modalityTypeServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allModalityTypes, Is.Not.Null);
            Assert.That(allModalityTypes, Is.InstanceOf<IEnumerable<ModalityType>>());
            Assert.That(allModalityTypes.Count(), Is.EqualTo(1));
            Assert.That(allModalityTypes, Has.Exactly(1).Property("Description").EqualTo(description));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var modalityTypeDomainMock = new Mock<ModalityType>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var modalityTypesController = new ModalityTypesController(modalityTypeServiceMock.Object);

            var description = new string('x', 30);

            var modalityTypeViewModelMock = new ModalityTypeViewModel
            {
                Description = description
            };

            modalityTypeServiceMock.Setup(x => x.Create(description)).Returns(modalityTypeDomainMock.Object);

            modalityTypesController.Request = new HttpRequestMessage();
            modalityTypesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = modalityTypesController.Post(modalityTypeViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            modalityTypeServiceMock.Verify(x => x.Create(description), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var modalityTypeDomainMock = new Mock<ModalityType>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var modalityTypesController = new ModalityTypesController(modalityTypeServiceMock.Object);

            var description = new string('x', 31);

            var modalityTypeViewModelMock = new ModalityTypeViewModel
            {
                Description = description
            };

            modalityTypeServiceMock.Setup(x => x.Create(description)).Throws(new ExceptionBase());

            modalityTypesController.Request = new HttpRequestMessage();
            modalityTypesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = modalityTypesController.Post(modalityTypeViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            modalityTypeServiceMock.Verify(x => x.Create(description), Times.Once);

        }

        #endregion Post

    }
}