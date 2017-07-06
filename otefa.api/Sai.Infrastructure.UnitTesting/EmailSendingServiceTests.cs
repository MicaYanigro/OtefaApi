using System;
using NUnit.Framework;
using Moq;
using Sai.Infrastructure.EmailSending;
using Sai.Domain.Model.Services;
using System.Collections.Generic;

namespace Sai.Infrastructure.UnitTesting
{
    [TestFixture]
    public class EmailSendingServiceTests
    {

        #region Send

        [Test]
        public void Should_Send_To_A_Single_Receiver()
        {

            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();

            var to = Guid.NewGuid().ToString();

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            emailSendingService.Send(subject, body, to);

            // Assert

            smtpClientWrapperMock.Verify(x => x.Send(subject, body, to));

        }

        [Test]
        public void Should_Send_To_A_Single_Receiver_But_Fails_Because_To_Is_Null()
        {
            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            string to = null;

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            var exception = Assert.Catch(() => emailSendingService.Send(subject, body, to));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("To cannot be null"));
        }

        [Test]
        public void Should_Send_To_A_Single_Receiver_But_Fails_Because_To_Is_Empty()
        {
            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            var to = "";

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            var exception = Assert.Catch(() => emailSendingService.Send(subject, body, to));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("To cannot be empty"));
        }

        [Test]
        public void Should_Send_To_Multiple_Receivers()
        {

            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();

            var to = new List<string>()
            {
                Guid.NewGuid().ToString()
            };

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            emailSendingService.Send(subject, body, to);

            // Assert

            smtpClientWrapperMock.Verify(x => x.Send(subject, body, to));

        }

        [Test]
        public void Should_Send_To_Multiple_Receivers_But_Fails_Because_To_Is_Null()
        {
            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            List<string> to = null;

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            var exception = Assert.Catch(() => emailSendingService.Send(subject, body, to));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("To cannot be null"));
        }

        [Test]
        public void Should_Send_To_Multiple_Receivers_But_Fails_Because_To_Is_Empty()
        {
            // Arrange

            var subject = Guid.NewGuid().ToString();
            var body = Guid.NewGuid().ToString();
            var to = new List<String>();

            var emailSendingService = new EmailSendingService();

            var smtpClientWrapperMock = new Mock<ISmtpClientWrapper>();

            smtpClientWrapperMock.Setup(x => x.Send(subject, body, to));

            emailSendingService.SmtpClientWrapper = smtpClientWrapperMock.Object;

            // Act

            var exception = Assert.Catch(() => emailSendingService.Send(subject, body, to));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("To cannot be empty"));
        }

        #endregion Send

    }
}