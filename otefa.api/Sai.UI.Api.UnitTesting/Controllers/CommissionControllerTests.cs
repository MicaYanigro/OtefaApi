using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Commission;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Agent;
using Sai.UI.Api.ViewModel.Commission;
using Sai.UI.Api.ViewModel.Enrollment;
using Sai.UI.Api.ViewModel.Person;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class CommissionControllerTests
    {
        #region Post

        [Test]
        public void Should_Post_Persons()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            var personViewModelMock = new PersonViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email
            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Post(commissionID, personViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Post_Persons_But_Fails_Because_Not_Found()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var relatedTasksDescription = new string('x', 250);
            var numberPeopleInCharge = 1;
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.EnrollPerson(commissionID, cuil, lastName, name, email, sex, dateOfBirth,
                         lastLevelStudies, relatedTasksDescription,
                         numberPeopleInCharge, country, province, addressCity)).Throws(new NullCommissionException());

            var personViewModelMock = new PersonViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                LastLevelStudies = lastLevelStudies,
                RelatedTasksDescription = relatedTasksDescription,
                NumberPeopleInCharge = numberPeopleInCharge,
                Country = country,
                Province = province,
                AddressCity = addressCity

            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Post(commissionID, personViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Should_Post_Persons_But_Fails_Because_Bad_Request()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var relatedTasksDescription = new string('x', 50);
             var numberPeopleInCharge = 1;
            var country = 1;
            var province = 1;
            var addressCity = new string('x', 250);

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.EnrollPerson(commissionID, cuil, lastName, name, email, sex, dateOfBirth,
                         lastLevelStudies, relatedTasksDescription,
                         numberPeopleInCharge, country, province, addressCity)).Throws(new ExceptionBase());

            var personViewModelMock = new PersonViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                LastLevelStudies = lastLevelStudies,
                RelatedTasksDescription = relatedTasksDescription,
                NumberPeopleInCharge = numberPeopleInCharge,
                Country = country,
                Province = province,
                AddressCity = addressCity
            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Post(commissionID, personViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [Test]
        public void Should_Post_Agents()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var organismID = 1;
            var workEmail = new string('x', 50);
            var sex = 1;
            var dateOfBirth = new DateTime();
            var lastLevelStudies = 1;
            var shortDescription = new string('x', 50);
            var numberPeopleInCharge = new int();
            var country = 1;
            var organismProvince = 1;
            var addressCity = new string('x', 250);

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            var agentsViewModelMock = new AgentViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email,
                OrganismID = organismID,
                WorkEmail = workEmail,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                LastLevelStudies = lastLevelStudies,
                RelatedTasksDescription = shortDescription,
                NumberPeopleInCharge = numberPeopleInCharge,
                Country = country,
                OrganismProvinceID = organismProvince,
                AddressCity = addressCity
            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act
            var response = commissionsController.Post(commissionID, agentsViewModelMock);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Post_Agents_But_Fails_Because_Not_Found()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
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

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.EnrollAgent(commissionID, cuil, lastName, name, email, organismID, worksInGovernment,
                                                legajo, ambit, province, municipality, contractingMode, workEmail,
                                                sex, dateOfBirth, lastLevelStudies,
                                                shortDescription, numberPeopleInCharge,
                                                country, organismProvince, addressCity, null)).Throws(new NullCommissionException());

            var agentsViewModelMock = new AgentViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email,
                OrganismID = organismID,
                WorksInGovernment = worksInGovernment,
                Legajo = legajo,
                Ambit = ambit,
                Province = province,
                Municipality = municipality,
                ContractingModeID = contractingMode,
                WorkEmail = workEmail,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                LastLevelStudies = lastLevelStudies,
                RelatedTasksDescription = shortDescription,
                NumberPeopleInCharge = numberPeopleInCharge,
                Country = country,
                OrganismProvinceID = organismProvince,
                AddressCity = addressCity
            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Post(commissionID, agentsViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Should_Post_Agents_But_Fails_Because_Bad_Request()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
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

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.EnrollAgent(commissionID, cuil, lastName, name, email, organismID, worksInGovernment,
                                                legajo, ambit, province, municipality, contractingMode, workEmail,
                                                sex, dateOfBirth, lastLevelStudies,
                                                shortDescription, numberPeopleInCharge,
                                                country, organismProvince, addressCity, null)).Throws(new ExceptionBase());

            var agentsViewModelMock = new AgentViewModel
            {
                Cuil = cuil,
                LastName = lastName,
                Name = name,
                Email = email,
                OrganismID = organismID,
                WorksInGovernment = worksInGovernment,
                Legajo = legajo,
                Ambit = ambit,
                Province = province,
                Municipality = municipality,
                ContractingModeID = contractingMode,
                WorkEmail = workEmail,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                LastLevelStudies = lastLevelStudies,
                RelatedTasksDescription = shortDescription,
                NumberPeopleInCharge = numberPeopleInCharge,
                Country = country,
                OrganismProvinceID = organismProvince,
                AddressCity = addressCity

    };


            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Post(commissionID, agentsViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Should_Post_Vacancies()
        {

            // Arrange

            var commissionID = 1;
            var userID = 1;
            var vacancies = 2;

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.AddVacancies(commissionID, userID, vacancies));

            var commissionVacanciesViewModel = new CommissionVacanciesViewModel
            {
                UserID = userID,
                Vacancies = vacancies
            };


            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.AddVacancies(commissionID, commissionVacanciesViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Post_Links()
        {

            // Arrange

            var commissionID = 1;
            var userID = 1;

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            commissionServiceMock.Setup(x => x.GenerateLink(commissionID, userID));

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.AddLink(commissionID, userID);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        #endregion Post

        #region put

        [Test]
        public void Should_Approve_Enrollment()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var ctcOrganism = new string('x', 30);
            var enrollmentID = new List<int>() { 123 };
            var commissionServiceMock = new Mock<ICommissionService>();
            var enrollmentServiceMock = new Mock<IEnrollmentService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);
            commissionsController.EnrollmentService = enrollmentServiceMock.Object;

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.ApproveEnrollment(commissionID, enrollmentID);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_PreApprove_Enrollment()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var ctcOrganism = new string('x', 30);
            var enrollmentID = new List<int>() { 123 };
            var commissionServiceMock = new Mock<ICommissionService>();
            var enrollmentServiceMock = new Mock<IEnrollmentService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);
            commissionsController.EnrollmentService = enrollmentServiceMock.Object;

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.PreApproveEnrollment(commissionID, enrollmentID);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Reject_Enrollment()
        {

            // Arrange

            var commissionID = 1;
            var rejectionReasonID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var ctcOrganism = new string('x', 30);
            var enrollmentID = 123;
            var commissionServiceMock = new Mock<ICommissionService>();
            var enrollmentServiceMock = new Mock<IEnrollmentService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);
            commissionsController.EnrollmentService = enrollmentServiceMock.Object;

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var rejectionDictionary = new Dictionary<int, int>();
            rejectionDictionary.Add(enrollmentID, rejectionReasonID);

            var rejectViewModel = new RejectViewModel()
            {
                rejectionDictionary = rejectionDictionary
            };
            // Act

            var response = commissionsController.RejectEnrollment(commissionID, rejectViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Close()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var ctcOrganism = new string('x', 30);
            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);


            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Close(commissionID);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Finalize()
        {

            // Arrange

            var commissionID = 1;
            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var ctcOrganism = new string('x', 30);
            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);


            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Finalize(commissionID);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }


        [Test]
        public void Should_Evaluate()
        {

            // Arrange

            var commissionID = 1;

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            var dictionary = new Dictionary<int, int>();
            dictionary.Add(1, 1);
            dictionary.Add(0, 1);

            var EvaluationsViewModelmock = new EvaluationsViewModel
            {
                values = dictionary
            };

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.Evaluations(commissionID, EvaluationsViewModelmock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Add_Schedule()
        {

            // Arrange

            var commissionID = 1;

            var commissionServiceMock = new Mock<ICommissionService>();
            var commissionsController = new CommissionsController(commissionServiceMock.Object);

            DateTime date = DateTime.Now;
            DateTime timeFrom = DateTime.Now;
            DateTime timeTo = DateTime.Now;
            List<int> teacherID = new List<int>();
            var headquarterID = 0;

            var CommissionScheduleViewModelMock = new CommissionScheduleViewModel
            {
                Date = date,
                TimeFrom = timeFrom,
                TimeTo = timeTo,
                TeacherID = teacherID,
                HeadQuarterID = headquarterID

            };

            var list = new List<CommissionScheduleViewModel>();
            list.Add(CommissionScheduleViewModelMock);

            commissionsController.Request = new HttpRequestMessage();
            commissionsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = commissionsController.AddSchedule(commissionID, list);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        #endregion put

    }
}