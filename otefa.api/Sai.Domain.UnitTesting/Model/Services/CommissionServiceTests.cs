using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions.Commission;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class CommissionServiceTests
    {

        [Test]
        public void Should_Enroll_Person()
        {
            CommissionService commissionService = new CommissionService();
            // Arrange
            var commissionID = 1;
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var commissionMock = new Mock<Commission>();
            var personMock = new Mock<Person>();
            var personFactoryMock = new Mock<IPersonFactory>();
            var personRepositoryMock = new Mock<IPersonRepository>();
            personFactoryMock.Setup(x => x.Create(cuil, lastName, name, email, (Sex) sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity)).Returns(personMock.Object);

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);
            personRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            commissionService.CommissionRepository = commissionRepositoryMock.Object;
            commissionService.PersonFactory = personFactoryMock.Object;
            commissionService.PersonRepository = personRepositoryMock.Object;

            var containerMock = new Mock<IContainer>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            Container.Current = containerMock.Object;
            // Act
            commissionService.EnrollPerson(commissionID, cuil, lastName, name, email, sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity);

            // Assert

            personFactoryMock.Verify(x => x.Create(cuil, lastName, name, email, (Sex) sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity), Times.Once);

        }

        [Test]
        public void Should_Enroll_Agent()
        {
            // Arrange
            var commissionID = 1;
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var organismID = 1;
            var worksInGovernment = false;
            var legajo = "123";
            var ambit = 1;
            var province = 1;
            var municipality = 1;
            var contractingMode = 1;
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = 1;
            var organismProvince = 1;
            var addressCity = new string('x', 250);

            var commissionMock = new Mock<Commission>();
            var agentMock = new Mock<Agent>();
            var agentFactoryMock = new Mock<IAgentFactory>();
            agentFactoryMock.Setup(x => x.Create(cuil, lastName, name, email, organismID, worksInGovernment,
                                                legajo, ambit, province, municipality, contractingMode, workEmail,
                                                (Sex)sex, dateOfBirth, lastLevelStudies,
                                                shortDescription, numberPeopleInCharge,
                                                country, organismProvince, addressCity)).Returns(agentMock.Object);

            CommissionService commissionService = new CommissionService();

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);

            commissionService.CommissionRepository = commissionRepositoryMock.Object;
            commissionService.AgentFactory = agentFactoryMock.Object;

            var containerMock = new Mock<IContainer>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            Container.Current = containerMock.Object;

            // Act
            commissionService.EnrollAgent(commissionID, cuil, lastName, name, email, organismID, worksInGovernment,
                                            legajo, ambit, province, municipality, contractingMode,
                                            workEmail, sex, dateOfBirth, lastLevelStudies,
                                            shortDescription, numberPeopleInCharge,
                                            country, organismProvince, addressCity, null);

            // Assert
            commissionRepositoryMock.Verify(x => x.GetById(commissionID), Times.Once);
            agentFactoryMock.Verify(x => x.Create(cuil, lastName, name, email, organismID, worksInGovernment,
                                                legajo, ambit, province, municipality, contractingMode, workEmail,
                                                (Sex)sex, dateOfBirth, lastLevelStudies,
                                                shortDescription, numberPeopleInCharge,
                                                country, organismProvince, addressCity), Times.Once);
            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }


        [Test]
        public void Should_Enroll_But_Fails_Because_Commission_Is_Null()
        {
            // Arrange
            var commissionID = 1;
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var commissionMock = new Mock<Commission>();
            var personMock = new Mock<Person>();
            var personFactoryMock = new Mock<IPersonFactory>();
            personFactoryMock.Setup(x => x.Create(cuil, lastName, name, email, (Sex) sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity)).Returns(personMock.Object);

            CommissionService commissionService = new CommissionService();

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns((Commission)null);

            commissionService.CommissionRepository = commissionRepositoryMock.Object;
            commissionService.PersonFactory = personFactoryMock.Object;

            // Act
            Exception e = Assert.Catch(() => commissionService.EnrollPerson(commissionID, cuil, lastName, name, email, sex, dateOfBirth,
                         lastLevelStudies, shortDescription,
                         numberPeopleInCharge, country, province, addressCity));

            // Assert
            commissionRepositoryMock.Verify(x => x.GetById(commissionID), Times.Once);
            Assert.That(e, Is.InstanceOf<NullCommissionException>());

        }

        [Test]
        public void Should_Close()
        {
            // Arrange
            var commissionID = 1;
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var enrollmentMock = new Mock<Enrollment>();
            var commissionMock = new Mock<Commission>();
            commissionMock.Setup(x => x.Close());

            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);

            commissionService.CommissionRepository = commissionRepositoryMock.Object;

            // Act
            commissionService.Close(commissionID);

            // Assert
            commissionMock.Verify(x => x.Close());
            commissionRepositoryMock.Verify(x => x.GetById(commissionID), Times.Once);
            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_Finalize()
        {
            // Arrange
            var commissionID = 1;
            var cuil = new string('x', 50);
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);

            var commissionMock = new Mock<Commission>();
            commissionMock.Setup(x => x.FinalizeCommission());
            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);

            commissionService.CommissionRepository = commissionRepositoryMock.Object;

            // Act
            commissionService.Close(commissionID);
            commissionService.FinalizeCommission(commissionID);

            // Assert
            commissionMock.Verify(x => x.FinalizeCommission());
            commissionRepositoryMock.Verify(x => x.GetById(commissionID), Times.Exactly(2));
            commissionRepositoryMock.Verify(x => x.Context, Times.Exactly(2));
        }

        [Test]
        public void Should_Evaluate()
        {
            // Arrange

            var personMock = new Mock<Person>();
            var personEnrollmentMock = new Mock<PersonEnrollment>(personMock.Object);

            var commissionID = 1;

            var dictionary = new Dictionary<int, int>();
            dictionary.Add(1, 1);
            dictionary.Add(0, 1);

            var commissionMock = new Mock<Commission>();
            commissionMock.Setup(x => x.Status).Returns(CommissionStatus.InProgressLoadResults);

            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);
            enrollmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(personEnrollmentMock.Object);
            commissionService.CommissionRepository = commissionRepositoryMock.Object;
            commissionService.EnrollmentRepository = enrollmentRepositoryMock.Object;
            // Act
            commissionService.Evaluate(commissionID, dictionary);

            // Assert

            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_AddSchedule()
        {
            // Arrange

            var personMock = new Mock<Person>();
            var personEnrollmentMock = new Mock<PersonEnrollment>(personMock.Object);

            var commissionID = 1;

            DateTime date = DateTime.Now;
            DateTime timeFrom = DateTime.Now;
            DateTime timeTo = DateTime.Now;
            List<int> teacherID = new List<int>();
            var headquarterID = 0;

            var commissionMock = new Mock<Commission>();

            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var commissionScheduleFactoryMock = new Mock<ICommissionScheduleFactory>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();

            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);
            enrollmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(personEnrollmentMock.Object);
            commissionService.CommissionRepository = commissionRepositoryMock.Object;
            commissionService.CommissionScheduleFactory = commissionScheduleFactoryMock.Object;
            commissionService.EnrollmentRepository = enrollmentRepositoryMock.Object;

            // Act
            commissionService.AddSchedule(commissionID, date, timeFrom, timeTo, teacherID, headquarterID);

            // Assert

            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_AddVacancies()
        {
            // Arrange

            var commissionID = 1;
            var userID = 1;
            var vacancies = 2;

            var commissionMock = new Mock<Commission>();
            commissionMock.Setup(x => x.AddVacancies(userID, vacancies));

            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);
            commissionRepositoryMock.Setup(x => x.Update(commissionMock.Object));
            commissionService.CommissionRepository = commissionRepositoryMock.Object;

            // Act
            commissionService.AddVacancies(commissionID, userID, vacancies);

            // Assert
            commissionRepositoryMock.Verify(x => x.Update(commissionMock.Object), Times.Once);
            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_Generate_Link()
        {
            // Arrange

            var commissionID = 1;
            var userID = 1;

            var commissionMock = new Mock<Commission>();
            var linkMock = new Mock<Link>();

            commissionMock.Setup(x => x.GenerateLink(userID)).Returns(linkMock.Object);

            CommissionService commissionService = new CommissionService();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();
            var enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            commissionRepositoryMock.Setup(x => x.GetById(commissionID)).Returns(commissionMock.Object);
            commissionRepositoryMock.Setup(x => x.Update(commissionMock.Object));
            commissionService.CommissionRepository = commissionRepositoryMock.Object;

            // Act
            var response = commissionService.GenerateLink(commissionID, userID);

            // Assert
            Assert.That(response, Is.Not.Null);
            commissionRepositoryMock.Verify(x => x.Update(commissionMock.Object), Times.Once);
            commissionRepositoryMock.Verify(x => x.Context, Times.Once);
        }

    }
}