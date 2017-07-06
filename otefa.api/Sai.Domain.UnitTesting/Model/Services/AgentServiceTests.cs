using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class AgentServiceTests
    {

        #region Get

        [Test]
        public void Should_Get_By_CuilCuit()
        {
            // Arrange
            string cuilcuit = string.Empty;
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var agentMock = new Mock<Agent>();
            var agentRepositoryMock = new Mock<IAgentRepository>();
            agentRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            agentRepositoryMock.Setup(x => x.GetByCuilCuit(It.IsAny<string>())).Returns(agentMock.Object);
            AgentService agentService = new AgentService();
            agentService.AgentRepository = agentRepositoryMock.Object;

            // Act

            var agent = agentService.FindAgentByCuitCuil(cuilcuit);

            // Assert

            Assert.That(agent, Is.Not.Null);
            Assert.That(agent, Is.EqualTo(agentMock.Object));

            agentRepositoryMock.Verify(x => x.GetByCuilCuit(cuilcuit), Times.Once);
        }

        #endregion Get

    }
}