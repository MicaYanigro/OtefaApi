using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class EnrollmentServiceTests
    {

        [Test]
        public void Should_Update_Status_To_Registered()
        {
            // Arrange

            var enrollmentID = new List<int>() { 1 };

            var enrollmentMock = new Mock<Enrollment>();
            var commissionMock = new Mock<Commission>();
            var personMock = new Mock<Person>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            EnrollmentService enrollmentService = new EnrollmentService();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            enrollmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(enrollmentMock.Object);
            enrollmentRepositoryMock.Setup(x => x.Context.Commit());
            enrollmentRepositoryMock.Setup(x => x.Update(It.IsAny<Enrollment>()));

            enrollmentService.EnrollmentRepository = enrollmentRepositoryMock.Object;
            enrollmentService.UserService = userServiceMock.Object;

            // Act
            enrollmentService.UpdateStatusToRegistered(enrollmentID);

            // Assert
            enrollmentRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            enrollmentMock.Verify(x => x.UpdateToRegistered());

        }

        [Test]
        public void Should_Update_Status_To_Approve()
        {
            // Arrange

            var enrollmentID = new List<int>() { 1 };

            var enrollmentMock = new Mock<Enrollment>();
            var commissionMock = new Mock<Commission>();
            var personMock = new Mock<Person>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            EnrollmentService enrollmentService = new EnrollmentService();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            enrollmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(enrollmentMock.Object);
            enrollmentRepositoryMock.Setup(x => x.Context.Commit());
            enrollmentRepositoryMock.Setup(x => x.Update(It.IsAny<Enrollment>()));

            enrollmentService.EnrollmentRepository = enrollmentRepositoryMock.Object;
            enrollmentService.UserService = userServiceMock.Object;

            // Act
            enrollmentService.UpdateStatusToApproved(enrollmentID);

            // Assert
            enrollmentRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            enrollmentMock.Verify(x => x.Approve());

        }

        [Test]
        public void Should_Update_Status_To_PreApprove()
        {
            // Arrange

            var enrollmentID = new List<int>() { 1 };
            var agent = new Mock<Agent>();
            var enrollmentMock = new Mock<AgentEnrollment>(agent.Object, null);
            var commissionMock = new Mock<Commission>();


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            EnrollmentService enrollmentService = new EnrollmentService();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            enrollmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(enrollmentMock.Object);
            enrollmentRepositoryMock.Setup(x => x.Context.Commit());
            enrollmentRepositoryMock.Setup(x => x.Update(It.IsAny<Enrollment>()));

            enrollmentService.EnrollmentRepository = enrollmentRepositoryMock.Object;
            enrollmentService.UserService = userServiceMock.Object;

            // Act
            enrollmentService.UpdateStatusToPreApproved(enrollmentID);

            // Assert
            enrollmentRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            enrollmentMock.Verify(x => x.PreApprove());

        }

        [Test]
        public void Should_Update_Status_To_Reject()
        {
            // Arrange

            var enrollmentID = 1;
            var rejectionReasonID = 1;

            var rejectionDictionary = new Dictionary<int, int>();
            rejectionDictionary.Add(enrollmentID, rejectionReasonID);
            var enrollmentMock = new Mock<Enrollment>();
            var commissionMock = new Mock<Commission>();
            var personMock = new Mock<Person>();

            var rejectionReasonMock = new Mock<RejectionReason>();
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            EnrollmentService enrollmentService = new EnrollmentService();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            var rejectionReasontRepositoryMock = new Mock<IRejectionReasonRepository>();

            enrollmentRepositoryMock.Setup(x => x.GetById(enrollmentID)).Returns(enrollmentMock.Object);
            enrollmentRepositoryMock.Setup(x => x.Context.Commit());
            enrollmentRepositoryMock.Setup(x => x.Update(It.IsAny<Enrollment>()));
            rejectionReasontRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(rejectionReasonMock.Object);

            enrollmentService.RejectionReasonRepository = rejectionReasontRepositoryMock.Object;
            enrollmentService.EnrollmentRepository = enrollmentRepositoryMock.Object;
            enrollmentService.UserService = userServiceMock.Object;

            // Act
            enrollmentService.UpdateStatusToRejected(rejectionDictionary, null);

            // Assert

            rejectionReasontRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            enrollmentRepositoryMock.Verify(x => x.GetById(enrollmentID), Times.Once);
            enrollmentMock.Verify(x => x.Reject(rejectionReasonMock.Object, null));

        }

    }
}