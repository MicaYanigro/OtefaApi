using NUnit.Framework;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Emails;
using System;
using System.Collections.Generic;
using Moq;
using Sai.Domain.Model.Services;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Net;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class EmailsControllerTests
    {

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var emailsController = new EmailsController();

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            var to = new List<string>();

            var emailViewModel = new EmailViewModel
            {
                Subject = subject,
                Body = body,
                To = to
            };

            var emailSendingServiceMock = new Mock<IEmailSendingService>();

            emailsController.EmailSendingService = emailSendingServiceMock.Object;

            emailsController.Request = new HttpRequestMessage();
            emailsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var httpResponseMessage = emailsController.Post(emailViewModel);

            // Assert

            emailSendingServiceMock.Verify(x => x.Send(emailViewModel.Subject, emailViewModel.Body, emailViewModel.To), Times.Once);

            Assert.That(httpResponseMessage, Is.Not.Null);
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Post_But_Fails_Because_An_Argument_Is_Invalid()
        {

            // Arrange

            var emailsController = new EmailsController();

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            var to = new List<string>();

            var emailViewModel = new EmailViewModel
            {
                Subject = subject,
                Body = body,
                To = to
            };

            var emailSendingServiceMock = new Mock<IEmailSendingService>();

            emailSendingServiceMock.Setup(x => x.Send(emailViewModel.Subject, emailViewModel.Body, emailViewModel.To)).Throws<ArgumentException>();

            emailsController.EmailSendingService = emailSendingServiceMock.Object;

            emailsController.Request = new HttpRequestMessage();
            emailsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var httpResponseMessage = emailsController.Post(emailViewModel);

            // Assert

            emailSendingServiceMock.Verify(x => x.Send(emailViewModel.Subject, emailViewModel.Body, emailViewModel.To), Times.Once);

            Assert.That(httpResponseMessage, Is.Not.Null);
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [Test]
        public void Should_Post_But_Fails_Because_An_Internal_Server_Error_Occurred()
        {

            // Arrange

            var emailsController = new EmailsController();

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            var to = new List<string>();

            var emailViewModel = new EmailViewModel
            {
                Subject = subject,
                Body = body,
                To = to
            };

            var emailSendingServiceMock = new Mock<IEmailSendingService>();

            emailSendingServiceMock.Setup(x => x.Send(emailViewModel.Subject, emailViewModel.Body, emailViewModel.To)).Throws<Exception>();

            emailsController.EmailSendingService = emailSendingServiceMock.Object;

            emailsController.Request = new HttpRequestMessage();
            emailsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var httpResponseMessage = emailsController.Post(emailViewModel);

            // Assert

            emailSendingServiceMock.Verify(x => x.Send(emailViewModel.Subject, emailViewModel.Body, emailViewModel.To), Times.Once);

            Assert.That(httpResponseMessage, Is.Not.Null);
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

        }

        #endregion Post

    }
}