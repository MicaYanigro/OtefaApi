using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class ProfileControllerTests
    {
        #region Post
        /*
        [Test]
        public void Should_Post()
        {
            var profileServiceMock = new Mock<IProfileService>();
            var controller = new ProfileController(profileServiceMock.Object);
            //Arrange
            var cuilCuit = string.Empty;
            var to = string.Empty;

            var profileViewModel = new ProfileLinkViewModel()
            {
                CuilCuit = cuilCuit,
                MailTo = to
            };

            profileServiceMock.Setup(x => x.SendLink(cuilCuit, to));

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            //Act

            controller.SendProfileLink(profileViewModel);

            //Assert
            profileServiceMock.Verify(x => x.SendLink(cuilCuit, to), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails_Because_Input_Data_Is_Not_Valid()
        {
            var profileServiceMock = new Mock<IProfileService>();
            var controller = new ProfileController(profileServiceMock.Object);
            //Arrange
            var cuilCuit = string.Empty;
            var to = string.Empty;

            var profileViewModel = new ProfileLinkViewModel()
            {
                CuilCuit = cuilCuit,
                MailTo = to
            };

            profileServiceMock.Setup(x => x.SendLink(cuilCuit, to)).Throws<ExceptionBase>();

            //Act
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            controller.SendProfileLink(profileViewModel);

            // Assert

            var response = controller.SendProfileLink(profileViewModel);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

        [Test]
        public void Should_Post_But_Fails_Because_Internal_Server_Error()
        {
            var profileServiceMock = new Mock<IProfileService>();
            var controller = new ProfileController(profileServiceMock.Object);
            //Arrange
            var cuilCuit = string.Empty;
            var to = string.Empty;

            var profileViewModel = new ProfileLinkViewModel()
            {
                CuilCuit = cuilCuit,
                MailTo = to
            };

            profileServiceMock.Setup(x => x.SendLink(cuilCuit, to)).Throws<Exception>();

            //Act
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            controller.SendProfileLink(profileViewModel);

            // Assert

            var response = controller.SendProfileLink(profileViewModel);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.InternalServerError));
        }
        */
        #endregion Post


    }

}
