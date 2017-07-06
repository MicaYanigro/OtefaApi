using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class AgentsControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange
            string cuilCuit = "23-12345678-0";
            var agentServiceMock = new Mock<IAgentService>();
            var agentMock = new Mock<Agent>();
            agentServiceMock.Setup(x => x.FindAgentByCuitCuil(It.IsAny<string>())).Returns(agentMock.Object);

            var agentsController = new AgentsController(agentServiceMock.Object);

            // Act

            var agent = agentsController.Get(cuilCuit);

            // Assert

            agentServiceMock.Verify(x => x.FindAgentByCuitCuil(cuilCuit), Times.Once);

            Assert.That(agent, Is.Not.Null);
            Assert.That(agent, Is.EqualTo(agentMock.Object));

        }

        #endregion Get



    }
}