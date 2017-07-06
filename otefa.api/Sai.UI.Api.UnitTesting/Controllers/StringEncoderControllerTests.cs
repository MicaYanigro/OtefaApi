using NUnit.Framework;
using Sai.UI.Api.Controllers;
using System;
using Moq;
using Sai.Domain.Model.Services;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Net;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class StringEncoderControllerTests
    {

        #region Post

        [Test]
        public void Should_Post()
        {
            // Arrange

            var stringEncoderController = new StringEncoderController();

            var plainText = Guid.NewGuid().ToString();

            var stringEncodingServiceMock = new Mock<IStringEncodingService>();

            stringEncoderController.StringEncodingService = stringEncodingServiceMock.Object;

            stringEncoderController.Request = new HttpRequestMessage();
            stringEncoderController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var httpResponseMessage = stringEncoderController.Post(plainText);

            // Assert

            stringEncodingServiceMock.Verify(x => x.Encode(plainText), Times.Once);

            Assert.That(httpResponseMessage, Is.Not.Null);
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Post_But_Fails_Because_Argument_Is_Invalid()
        {
            // Arrange

            var stringEncoderController = new StringEncoderController();

            var plainText = Guid.NewGuid().ToString();

            var stringEncodingServiceMock = new Mock<IStringEncodingService>();

            stringEncodingServiceMock.Setup(x => x.Encode(plainText)).Throws<ArgumentException>();

            stringEncoderController.StringEncodingService = stringEncodingServiceMock.Object;

            stringEncoderController.Request = new HttpRequestMessage();
            stringEncoderController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var httpResponseMessage = stringEncoderController.Post(plainText);

            // Assert

            stringEncodingServiceMock.Verify(x => x.Encode(plainText), Times.Once);

            Assert.That(httpResponseMessage, Is.Not.Null);
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        #endregion Post

    }
}