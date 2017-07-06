using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions.Enrollment;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Entities
{

    [TestFixture]
    public class AgentEnrollmentTests
    {

        [Test]
        public void Should_Create_An_AgentEnrollment()
        {
            // Arrange
            var agentMock = new Mock<Agent>();

            // Act

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);

            // Assert

            Assert.That(agentEnrollment.Agent, Is.EqualTo(agentMock.Object));
            Assert.That(agentEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Registered));

        }

        [Test]
        public void Should_Create_An_AgentEnrollment_But_Fails_Because_Agent_Is_Null()
        {
            // Arrange
            var agentMock = new Mock<Agent>();

            // Act

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);

            // Assert

            Assert.That(agentEnrollment.Agent, Is.EqualTo(agentMock.Object));
            Assert.That(agentEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Registered));

        }

        [Test]
        public void Should_PreApprove_An_AgentEnrollment()
        {

            // Arrange
            var organismMock = new Mock<Organism>();
            organismMock.Setup(x => x.Level).Returns(Level.National);
            var agentMock = new Mock<Agent>();
            agentMock.Setup(x => x.Organism).Returns(organismMock.Object);

            // Act

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);
            agentEnrollment.PreApprove();

            // Assert

            Assert.That(agentEnrollment.Agent, Is.EqualTo(agentMock.Object));
            Assert.That(agentEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.PreApproved));

        }


        [Test]
        public void Should_Approve_An_AgentEnrollment()
        {
            var vacancy = 12;
            var commissionMock = new Mock<Commission>();
            commissionMock.SetupGet(x => x.Vacancy).Returns(vacancy);
            // Arrange
            var agentMock = new Mock<Agent>();
            var listCount = 1;
            var commissionrepositoryMock = new Mock<ICommissionRepository>();
            commissionrepositoryMock.Setup(x => x.GetApprovedEnrollmentsByCommission(It.IsAny<int>())).Returns(listCount);

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<ICommissionRepository>()).Returns(commissionrepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);
            agentEnrollment.Commission = commissionMock.Object;

            agentEnrollment.Approve();

            // Assert

            Assert.That(agentEnrollment.Agent, Is.EqualTo(agentMock.Object));
            Assert.That(agentEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Approved));

        }

        [Test]
        public void Should_Reject_An_AgentEnrollment()
        {

            // Arrange
            var agentMock = new Mock<Agent>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Act

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);
            agentEnrollment.Reject(null, null);

            // Assert

            Assert.That(agentEnrollment.Agent, Is.EqualTo(agentMock.Object));
            Assert.That(agentEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Rejected));

        }


        [Test]
        public void Should_Evaluate()
        {

            // Arrange
            var agentMock = new Mock<Agent>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            var evaluation = Evaluation.Approved;
            // Act

            var agentEnrollment = new AgentEnrollment(agentMock.Object, null);

            agentEnrollment.Evaluate(evaluation);


            // Assert

            Assert.That(agentEnrollment.GetEvaluation() == Evaluation.Approved);

        }
    }
}
