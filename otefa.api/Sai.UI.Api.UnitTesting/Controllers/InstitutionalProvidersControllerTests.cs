using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.InstitutionalProvider;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class InstitutionalProvidersControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange
            string cuilCuit = "33-69345023-9";
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            InstitutionalProviderServiceMock.Setup(x => x.FindInstitutionalProviderByCuitCuil(It.IsAny<string>())).Returns(InstitutionalProviderMock.Object);

            var InstitutionalProvidersController = new InstitutionalProvidersController(InstitutionalProviderServiceMock.Object);

            // Act

            var InstitutionalProvider = InstitutionalProvidersController.Get(cuilCuit);

            // Assert

            InstitutionalProviderServiceMock.Verify(x => x.FindInstitutionalProviderByCuitCuil(cuilCuit), Times.Once);

            Assert.That(InstitutionalProvider, Is.Not.Null);
            Assert.That(InstitutionalProvider, Is.EqualTo(InstitutionalProviderMock.Object));

        }

        #endregion Get


        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var InstitutionalProvidersController = new InstitutionalProvidersController(InstitutionalProviderServiceMock.Object);

            string cuilCuit = "33-69345023-9";
            var name = new string('x', 30);
            var rlmNumber = new string('x', 2);

            var InstitutionalProviderViewModelMock = new InstitutionalProviderViewModel
            {
                Cuil = cuilCuit,
                RlmNumber = rlmNumber,
                Name = name
            };

            InstitutionalProviderServiceMock.Setup(x => x.Create(cuilCuit, rlmNumber, name)).Returns(InstitutionalProviderMock.Object.GetId());

            InstitutionalProvidersController.Request = new HttpRequestMessage();
            InstitutionalProvidersController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = InstitutionalProvidersController.Post(InstitutionalProviderViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            InstitutionalProviderServiceMock.Verify(x => x.Create(cuilCuit, rlmNumber, name), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var InstitutionalProvidersController = new InstitutionalProvidersController(InstitutionalProviderServiceMock.Object);

            string cuilCuit = "33-69345023-9";
            var name = new string('x', 30);
            var rlmNumber = new string('x', 2);

            var InstitutionalProviderViewModelMock = new InstitutionalProviderViewModel
            {
                Cuil = cuilCuit,
                RlmNumber = rlmNumber,
                Name = name
            };


            InstitutionalProviderServiceMock.Setup(x => x.Create(cuilCuit, rlmNumber, name)).Throws(new ExceptionBase());

            InstitutionalProvidersController.Request = new HttpRequestMessage();
            InstitutionalProvidersController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = InstitutionalProvidersController.Post(InstitutionalProviderViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            InstitutionalProviderServiceMock.Verify(x => x.Create(cuilCuit, rlmNumber, name), Times.Once);

        }

        #endregion Post

    }
}