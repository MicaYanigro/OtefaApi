using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.Commission;
using Moq;
using Sai.Domain.Model.Entities.Activity;
using System.Linq;
using Sai.Infrastructure.IoC;
using Sai.Domain.Model.Services;
using System.Collections;
using System.Collections.Generic;
using System;
using Sai.Domain.Model.Exceptions.Enrollment;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Repositories;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class CommissionTests
    {

        [Test]
        public void Should_Create_A_Commission()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var commission = new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);

            // Assert

            Assert.That(commission.Vacancy, Is.EqualTo(vacancy));
            Assert.That(commission.Oversold, Is.EqualTo(oversold));
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Vacancy_Length_Is_More_Than_5_Characters()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 279882;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var caughtException = Assert.Catch(() => new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidCommissionVacancyLengthException>());
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Oversold_Length_Is_More_Than_4_Characters()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 22;
            var oversold = 44444;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var caughtException = Assert.Catch(() => new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidCommissionOversoldLengthException>());
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Vacancy_Is_Null()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 0;
            var oversold = 1;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var caughtException = Assert.Catch(() => new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullCommissionVacancyException>());
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Oversold_Is_Null()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 10;
            var oversold = 0;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var caughtException = Assert.Catch(() => new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullCommissionOversoldException>());
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Oversold_Is_Exceeded()
        {
            // Arrange

            var activityMock = new Mock<Activity>();

            var vacancy = 10;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            // Act

            var caughtException = Assert.Catch(() => new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<CommissionOversoldExceededException>());
        }

        [Test]
        public void Should_Enroll_Person()
        {
            // Arrange
            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var dateFrom = new DateTime(2020, 01, 01);

            var dictationMock = new Mock<Dictation>();
            dictationMock.SetupGet(x => x.DateFrom).Returns(dateFrom);

            var personMock = new Mock<Person>();
            var personEnrollmentMock = new Mock<PersonEnrollment>(personMock.Object);

            var activityMock = new Mock<Activity>();
            activityMock.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            // Act
            var commission = new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.OpenInscription();
            commission.Enroll(personEnrollmentMock.Object);
            var result = commission.GetEnrollments();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count() == 1);
        }

        [Test]
        public void Should_Enroll_Person_But_Fails_Because_Has_Approved_Equivalent_Activity()
        {
            // Arrange
            var random = new Random();
            var vacancy = 22;
            var oversold = 10;
            var equivalenceid = random.Next();
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var dateFrom = new DateTime(2020, 01, 01);

            var dictationMock = new Mock<Dictation>();
            dictationMock.SetupGet(x => x.DateFrom).Returns(dateFrom);

            var activityMock = new Mock<Activity>();
            var activityMockCommission = new Mock<Activity>();
            activityMockCommission.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMockCommission.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);

            IEnumerable<Activity> activityList = new List<Activity>() { activityMock.Object };
            var personMock = new Mock<Person>();
            var personEnrollmentMock = new Mock<PersonEnrollment>(personMock.Object);
            personEnrollmentMock.Setup(x => x.GetApprovedActivities()).Returns(activityList);
            activityMockCommission.Setup(x => x.IsEquivalentTo(activityMock.Object)).Returns(true);
            // Act
            var commission = new Commission(activityMockCommission.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.OpenInscription();
            var caughtException = Assert.Catch(() => commission.Enroll(personEnrollmentMock.Object));


            // Assert
            Assert.That(caughtException, Is.InstanceOf<HasApprovedEquivalentActivityException>());
        }

        [Test]
        public void Should_Enroll_Person_But_Fails_Because_Has_Enrolled_Equivalent_Activity()
        {
            // Arrange
            var random = new Random();
            var vacancy = 22;
            var oversold = 10;
            var equivalenceid = random.Next();
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var dateFrom = new DateTime(2020, 01, 01);

            var dictationMock = new Mock<Dictation>();
            dictationMock.SetupGet(x => x.DateFrom).Returns(dateFrom);

            var activityMock = new Mock<Activity>();
            var activityMockCommission = new Mock<Activity>();
            activityMockCommission.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMockCommission.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);

            IEnumerable<Activity> activityList = new List<Activity>() { activityMock.Object };
            var personMock = new Mock<Person>();
            var personEnrollmentMock = new Mock<PersonEnrollment>(personMock.Object);
            personEnrollmentMock.Setup(x => x.GetEnrolledActivities()).Returns(activityList);
            activityMockCommission.Setup(x => x.IsEquivalentTo(activityMock.Object)).Returns(true);
            // Act
            var commission = new Commission(activityMockCommission.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.OpenInscription();
            var caughtException = Assert.Catch(() => commission.Enroll(personEnrollmentMock.Object));


            // Assert
            Assert.That(caughtException, Is.InstanceOf<HasEnrolledEquivalentActivityException>());
        }

        [Test]
        public void Should_Enroll_Agent()
        {
            // Arrange
            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var dateFrom = new DateTime(2020,01,01);
            var agentMock = new Mock<Agent>();
            var dictationMock = new Mock<Dictation>();
            dictationMock.SetupGet(x => x.DateFrom).Returns(dateFrom);
            var agentEnrollmentMock = new Mock<AgentEnrollment>(agentMock.Object, null);
            var activityMock = new Mock<Activity>();
            activityMock.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);


            var activityMockCommission = new Mock<Activity>();
            IEnumerable<Activity> activityList = new List<Activity>() { activityMock.Object };

            agentEnrollmentMock.Setup(x => x.GetApprovedActivities()).Returns(activityList);
            agentEnrollmentMock.Setup(x => x.GetEnrolledActivities()).Returns(activityList);
            activityMockCommission.Setup(x => x.IsEquivalentTo(It.IsAny<Activity>())).Returns(false);

            // Act
            var commission = new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.OpenInscription();
            commission.Enroll(agentEnrollmentMock.Object);
            var result = commission.GetEnrollments();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count() == 1);
        }

        [Test]
        public void Should_Enroll_Agent_But_Fails_Because_Has_Equivalent_Activity()
        {
            // Arrange
            var random = new Random();
            var vacancy = 22;
            var oversold = 10;
            var equivalenceid = random.Next();
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var dateFrom = new DateTime(2020, 01, 01);
            var enrollmentCloseHours = 1;
            var dictationMock = new Mock<Dictation>();
            dictationMock.SetupGet(x => x.DateFrom).Returns(dateFrom);

            var activityMock = new Mock<Activity>();

            var activityMockCommission = new Mock<Activity>();
            activityMockCommission.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);
            activityMockCommission.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            IEnumerable<Activity> activityList = new List<Activity>() { activityMock.Object };
            var agentMock = new Mock<Agent>();
            var agentEnrollmentMock = new Mock<AgentEnrollment>(agentMock.Object, null);
            agentEnrollmentMock.Setup(x => x.GetApprovedActivities()).Returns(activityList);
            activityMockCommission.Setup(x => x.IsEquivalentTo(activityMock.Object)).Returns(true);
            // Act
            var commission = new Commission(activityMockCommission.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.OpenInscription();
            var caughtException = Assert.Catch(() => commission.Enroll(agentEnrollmentMock.Object));


            // Assert
            Assert.That(caughtException, Is.InstanceOf<HasApprovedEquivalentActivityException>());
        }

        [Test]
        public void Should_Get_Inap_Code()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();

            activityMock.Setup(x => x.Area).Returns(areaMock.Object);
            activityMock.Setup(x => x.Category).Returns(categoryMock.Object);

            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var inapCodeActivity = "InapCodeTest";
            // Act
            activityMock.Setup(x => x.GenerateInapCode(It.IsAny<int>())).Returns(inapCodeActivity);
            var commission = new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.SaveInapCode();
            var inapCode = commission.GetInapCode();

            // Assert

            Assert.That(inapCode, Is.Not.Null);

            activityMock.Verify(x => x.GenerateInapCode(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void Should_Close()
        {

            var activityMock = new Mock<Activity>();
            var rejectionReasonMock = new Mock<RejectionReason>();
            // Arrange
            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            var rejectionReasonRepositoryMock = new Mock<IRejectionReasonRepository>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IRejectionReasonRepository>()).Returns(rejectionReasonRepositoryMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var commission = new Commission(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commission.Close();

            Assert.That(commission.Status, Is.EqualTo(CommissionStatus.Conformed));
        }
    }
}