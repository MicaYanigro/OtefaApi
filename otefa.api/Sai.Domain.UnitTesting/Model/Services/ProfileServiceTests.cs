using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class ProfileServiceTests
    {

        #region Get

        [Test]
        public void Should_Send_Link()
        {

            // Arrange

            string subject = "SubjectTest";
            string body = "bodyTest";
            string mailTo = null;
            string cuilcuit = "17-37371234-1";

            var agentMock = new Mock<Agent>();
            var personMock = new Mock<Person>();
            var emailServiceMock = new Mock<IEmailSendingService>();
            var emailTemplateServiceMock = new Mock<IEmailTemplateService>();
            var agentRepositoryMock = new Mock<IAgentRepository>();
            var personRepositoryMock = new Mock<IPersonRepository>();

            emailTemplateServiceMock.Setup(x => x.RenderBody(It.IsAny<string>(), It.IsAny<object>())).Returns(body);
            emailServiceMock.Setup(x => x.Send(subject, body, mailTo));
            agentRepositoryMock.Setup(x => x.GetByCuilCuit(It.IsAny<string>())).Returns(agentMock.Object);
            personRepositoryMock.Setup(x => x.GetByCuilCuit(It.IsAny<string>())).Returns(personMock.Object);

            ProfileService profileService = new ProfileService();

            profileService.EmailSendingService = emailServiceMock.Object;
            profileService.EmailTemplateService = emailTemplateServiceMock.Object;
            profileService.AgentRepository = agentRepositoryMock.Object;
            profileService.PersonRepository = personRepositoryMock.Object;

            // Act

            profileService.SendLink(cuilcuit, mailTo);

            // Assert

            emailServiceMock.Verify(x => x.Send(subject, body, mailTo), Times.Once);
            emailTemplateServiceMock.Verify(x => x.RenderBody(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            agentRepositoryMock.Verify(x => x.GetByCuilCuit(cuilcuit), Times.Once);

        }

        #endregion Get

    }
}