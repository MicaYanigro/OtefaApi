using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Link;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Link;
using Sai.UI.Api.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class LinksControllerTests
    {
        #region Post

        [Test]
        public void Should_GetByHash()
        {
            //Arrange

            var linkServiceMock = new Mock<ILinkService>();
            var controller = new LinksController(linkServiceMock.Object);
            var linkMock = new Mock<Link>();

            var hash = Guid.NewGuid().ToString();

            linkServiceMock.Setup(x => x.GetByHash(hash)).Returns(linkMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            //Act

            var response = controller.GetByHash(hash);

            //Assert
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            linkServiceMock.Verify(x => x.GetByHash(hash), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails_Because_Not_Found()
        {
            var linkServiceMock = new Mock<ILinkService>();
            var controller = new LinksController(linkServiceMock.Object);

            var hash = Guid.NewGuid().ToString();

            linkServiceMock.Setup(x => x.GetByHash(hash)).Throws(new NullLinkException());

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            //Act

            var response = controller.GetByHash(hash);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

      
        #endregion Post


    }

}
