using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Activity;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Activities;
using Sai.UI.Api.ViewModel.Commission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class ActivitiesControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var activityServiceMock = new Mock<IActivityService>();

             containerMock.Setup(x => x.Resolve<IActivityService>()).Returns(activityServiceMock.Object);
            Container.Current = containerMock.Object;

            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();
            var userMock = new Mock<User>();

            var address = new string('x', 500);
            var areaBriefDescription = new string('x', 2);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryBriefDescription = new string('x', 2);
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationHours = random.Next(1, 10);
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var teacherProfile = new string('x', 500);

            // Act

            var activities = new List<Activity>() { new Activity(areaMock.Object,
                                                                categoryMock.Object,
                                                                caseFileNumber,
                                                                durationHours,
                                                                isUniqueInscriptionAllowed,
                                                                modalityType,
                                                                name,
                                                                observation,
                                                                participantProfile,
                                                                program,
                                                                userMock.Object)
        };

            activityServiceMock.Setup(x => x.GetAll()).Returns(activities);

            var activitiesController = new ActivitiesController(activityServiceMock.Object);

            // Act

            var allActivities = activitiesController.Get();

            // Assert

            activityServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allActivities, Is.Not.Null);
            Assert.That(allActivities, Is.InstanceOf<IEnumerable<Activity>>());
            Assert.That(allActivities.Count(), Is.EqualTo(1));
            Assert.That(allActivities, Has.Exactly(1).Property("Area").EqualTo(areaMock.Object));
            Assert.That(allActivities, Has.Exactly(1).Property("Category").EqualTo(categoryMock.Object));
            Assert.That(allActivities, Has.Exactly(1).Property("RlmNumber").EqualTo(caseFileNumber));
            Assert.That(allActivities, Has.Exactly(1).Property("Duration").EqualTo(durationHours));
            Assert.That(allActivities, Has.Exactly(1).Property("IsUniqueInscriptionAllowed").EqualTo(isUniqueInscriptionAllowed));
            Assert.That(allActivities, Has.Exactly(1).Property("ModalityType").EqualTo(modalityType));
            Assert.That(allActivities, Has.Exactly(1).Property("Name").EqualTo(name));
            Assert.That(allActivities, Has.Exactly(1).Property("Observation").EqualTo(observation));
            Assert.That(allActivities, Has.Exactly(1).Property("ParticipantProfile").EqualTo(participantProfile));
            Assert.That(allActivities, Has.Exactly(1).Property("Program").EqualTo(program));
            Assert.That(allActivities, Has.Exactly(1).Property("Status").EqualTo(ActivityStatus.DesignLoadedPendingDictation));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var activityDomainMock = new Mock<Activity>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();

            var address = new string('x', 500);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var caseFileNumber = new string('x', 25);
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationHours = random.Next(1, 10);
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);
            var equivalences = new List<int>();
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var userID = 1;

            var activityViewModelMock = new ActivityViewModel
            {
                AreaBriefDescription = areaMock.Object.BriefDescription,
                CategoryBriefDescription = categoryMock.Object.BriefDescription,
                RlmNumber = caseFileNumber,
                Duration = durationHours,
                IsUniqueInscriptionAllowed = isUniqueInscriptionAllowed,
                ModalityType = modalityType,
                Name = name,
                Observation = observation,
                ParticipantProfile = participantProfile,
                Program = program,
                Equivalences = equivalences,
                Teachers = teachers,
                Correlatives = correlatives,
                ResponsableActivityID = userID

            };

            activityServiceMock.Setup(x => x.Create(areaMock.Object.BriefDescription,
                                                    categoryMock.Object.BriefDescription,
                                                    caseFileNumber,
                                                    durationHours,
                                                    isUniqueInscriptionAllowed,
                                                    modalityTypeDescription,
                                                    name,
                                                    observation,
                                                    participantProfile,
                                                    programDescription,
                                                    equivalences,
                                                    teachers,
                                                    correlatives,
                                                    userID)).Returns(activityDomainMock.Object);

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = activitiesController.Post(activityViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            activityServiceMock.Verify(x => x.Create(areaMock.Object.BriefDescription,
                                                    categoryMock.Object.BriefDescription,
                                                    caseFileNumber,
                                                    durationHours,
                                                    isUniqueInscriptionAllowed,
                                                    modalityTypeDescription,
                                                    name,
                                                    observation,
                                                    participantProfile,
                                                    programDescription,
                                                    equivalences,
                                                    teachers,
                                                    correlatives,
                                                    userID), Times.Once);

        }

        [Test]
        public void Should_Post_Jurisdictional()
        {

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Arrange

            var random = new Random();

            var activityDomainMock = new Mock<JurisdictionalActivity>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var categoryMock = new Mock<Category>();

            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var sector = 1;
            var ambit = 1;
            var rlmNumber = new string('x', 10);
            var equivalences = new List<int>();
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var pac = 1;
            var pacObjectives = new string('x', 10);
            var pacAreas = new string('x', 10);
            var organismDescription = "abcd";
            var province = "provinceTest";
            var municipality = "municipalityTest";
            var userID = 1;


            var activityViewModelMock = new ActivityViewModel
            {
                CategoryBriefDescription = categoryMock.Object.BriefDescription,
                Duration = durationHours,
                IsUniqueInscriptionAllowed = isUniqueInscriptionAllowed,
                ModalityType = modalityType,
                Name = name,
                Observation = observation,
                ParticipantProfile = participantProfile,
                Sector = sector,
                Ambit = ambit,
                OrganismDescription = organismDescription,
                RlmNumber = rlmNumber,
                Equivalences = equivalences,
                Teachers = teachers,
                Correlatives = correlatives,
                Pac = pac,
                Province = province,
                Municipality = municipality,
                ResponsableActivityID = userID

            };

            activityServiceMock.Setup(x => x.JurisdictionalCreate(categoryMock.Object.BriefDescription,
                                                        durationHours,
                                                        isUniqueInscriptionAllowed,
                                                        modalityTypeDescription,
                                                        name,
                                                        observation,
                                                        participantProfile,
                                                        sector,
                                                        ambit,
                                                        rlmNumber,
                                                        organismDescription,
                                                        equivalences,
                                                        teachers,
                                                        correlatives,
                                                        pac,
                                                        province,
                                                        municipality,
                                                        userID)).Returns(activityDomainMock.Object);

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = activitiesController.JurisdictionalPost(activityViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            activityServiceMock.Verify(x => x.JurisdictionalCreate(categoryMock.Object.BriefDescription,
                                                    durationHours,
                                                    isUniqueInscriptionAllowed,
                                                    modalityTypeDescription,
                                                    name,
                                                    observation,
                                                    participantProfile,
                                                    sector,
                                                    ambit,
                                                    rlmNumber,
                                                    organismDescription,
                                                    equivalences,
                                                    teachers,
                                                    correlatives,
                                                    pac,
                                                    province,
                                                    municipality,
                                                    userID), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var activityDomainMock = new Mock<Activity>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();

            var address = new string('x', 500);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var caseFileNumber = new string('x', 25);
            var cityName = new string('x', 40);
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationHours = random.Next(1, 10);
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var teacherProfile = new string('x', 500);
            var equivalences = new List<int>();
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var userID = 1;



            var activityViewModelMock = new ActivityViewModel
            {
                AreaBriefDescription = areaMock.Object.BriefDescription,
                CategoryBriefDescription = categoryMock.Object.BriefDescription,
                RlmNumber = caseFileNumber,
                Duration = durationHours,
                IsUniqueInscriptionAllowed = isUniqueInscriptionAllowed,
                ModalityType = modalityType,
                Name = name,
                Observation = observation,
                ParticipantProfile = participantProfile,
                Program = program,
                Equivalences = equivalences,
                Teachers = teachers,
                Correlatives = correlatives
            };

            activityServiceMock.Setup(x => x.Create(areaMock.Object.BriefDescription,
                                                    categoryMock.Object.BriefDescription,
                                                    caseFileNumber,
                                                    durationHours,
                                                    isUniqueInscriptionAllowed,
                                                    modalityTypeDescription,
                                                    name,
                                                    observation,
                                                    participantProfile,
                                                    programDescription,
                                                    equivalences,
                                                    teachers,
                                                    correlatives,
                                                    userID)).Throws(new ExceptionBase());

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = activitiesController.Post(activityViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            activityServiceMock.Verify(x => x.Create(areaMock.Object.BriefDescription,
                                                    categoryMock.Object.BriefDescription,
                                                    caseFileNumber,
                                                    durationHours,
                                                    isUniqueInscriptionAllowed,
                                                    modalityTypeDescription,
                                                    name,
                                                    observation,
                                                    participantProfile,
                                                    programDescription,
                                                    equivalences,
                                                    teachers,
                                                    correlatives,
                                                    userID), Times.Once);

        }

        [Test]
        public void Should_Create_A_Dictation()
        {
            // Arrange

            var activityDomainMock = new Mock<Activity>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);

            var activityID = 1;
            var credits = 22;
            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsiderationViewModel>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            var commissionFactoryMock = new Mock<ICommissionFactory>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<ICommissionFactory>()).Returns(commissionFactoryMock.Object);
            Container.Current = containerMock.Object;

            activityServiceMock.Setup(x => x.GetById(activityID)).Returns(activityDomainMock.Object);

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var dictationViewModel = new DictationViewModel();
            dictationViewModel.Credits = credits;
            dictationViewModel.EvaluationCriteriaWithConsideration = evaluationCriteriaWithConsideration;

            // Act

            var response = activitiesController.Post(activityID, dictationViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Activity_Is_Not_Found()
        {
            // Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);

            var activityID = 1;
            var credits = 22;
            var evaluationCriteriaWithConsiderationDictionary = new Dictionary<int, int>();
            var evaluationCriteriaWithConsiderationViewModelCollection = new List<EvaluationCriteriaWithConsiderationViewModel>();
            activityServiceMock.Setup(x => x.AddDictation(activityID, credits, evaluationCriteriaWithConsiderationDictionary, It.IsAny<int>())).Throws(new NullActivityException());

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var dictationViewModel = new DictationViewModel();
            dictationViewModel.Credits = credits;
            dictationViewModel.EvaluationCriteriaWithConsideration = evaluationCriteriaWithConsiderationViewModelCollection;

            // Act

            var response = activitiesController.Post(activityID, dictationViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Should_Create_A_Dictation_But_Fails()
        {
            // Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var technicalFormNumber = new string('x', 40);

            var activityID = 1;
            var credits = 22;
            var evaluationCriteriaWithConsiderationDictionary = new Dictionary<int, int>();
            var evaluationCriteriaWithConsiderationViewModelCollection = new List<EvaluationCriteriaWithConsiderationViewModel>();

            activityServiceMock.Setup(x => x.AddDictation(activityID, credits, evaluationCriteriaWithConsiderationDictionary, It.IsAny<int>())).Throws(new ExceptionBase());
            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var dictationViewModel = new DictationViewModel();
            dictationViewModel.Credits = credits;
            dictationViewModel.EvaluationCriteriaWithConsideration = evaluationCriteriaWithConsiderationViewModelCollection;

            // Act

            var response = activitiesController.Post(activityID, dictationViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Should_Create_A_Commission()
        {

            // Arrange

            var activityDomainMock = new Mock<Activity>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var vacancy = 15;
            var oversold = 2;
            var activityID = 1;
            var commissionInscriptionType = 1;
            var enrollmentCloseHours = 1;
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            var commissionFactoryMock = new Mock<ICommissionFactory>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<ICommissionFactory>()).Returns(commissionFactoryMock.Object);
            Container.Current = containerMock.Object;

            var commissionMock = new Mock<Commission>();

            activityServiceMock.Setup(x => x.GetById(activityID)).Returns(activityDomainMock.Object);
            activityServiceMock.Setup(x => x.AddCommission(activityID, vacancy, oversold, (CommissionInscriptionType)commissionInscriptionType, enrollmentCloseHours)).Returns(commissionMock.Object);
            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var commissionViewModel = new CommissionViewModel();
            commissionViewModel.Vacancy = vacancy;
            commissionViewModel.Oversold = oversold;
            commissionViewModel.CommissionInscriptionType = commissionInscriptionType;
            commissionViewModel.EnrollmentCloseHours = enrollmentCloseHours;

            // Act

            var response = activitiesController.Post(activityID, commissionViewModel);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails_Because_Activity_Is_Not_Found()
        {
            // Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var vacancy = 15;
            var oversold = 2;
            var activityID = 1;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            activityServiceMock.Setup(x => x.AddCommission(activityID, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Throws(new NullActivityException());

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var commissionViewModel = new CommissionViewModel();
            commissionViewModel.Vacancy = vacancy;
            commissionViewModel.Oversold = oversold;
            commissionViewModel.CommissionInscriptionType = (int)commissionInscriptionType;
            commissionViewModel.EnrollmentCloseHours = enrollmentCloseHours;
            // Act

            var response = activitiesController.Post(activityID, commissionViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Should_Create_A_Commission_But_Fails()
        {
            // Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var vacancy = 15;
            var oversold = 2;
            var activityID = 1;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            activityServiceMock.Setup(x => x.AddCommission(activityID, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Throws(new ExceptionBase());
            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var commissionViewModel = new CommissionViewModel();
            commissionViewModel.Vacancy = vacancy;
            commissionViewModel.Oversold = oversold;
            commissionViewModel.CommissionInscriptionType = (int)commissionInscriptionType;
            commissionViewModel.EnrollmentCloseHours = enrollmentCloseHours;

            // Act

            var response = activitiesController.Post(activityID, commissionViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        #endregion Post


        [Test]
        public void Should_Accept_Dictation()
        {
            //Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var observation = new string('x', 20);
            var activityID = 1;
            var dictationNumber = "AA-2017-1-X-A";
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now;
            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            AcceptDictationViewModel AcceptDictationViewModel = new AcceptDictationViewModel()
            {
                Observation = observation,
                DictationNumber = dictationNumber,
                DateFrom = dateFrom,
                DateTo = dateTo

            };

            // Act
            var response = activitiesController.AcceptDictation(activityID, AcceptDictationViewModel);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Reject_Dictation()
        {
            //Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var observation = new RejectDictationViewModel();
            var activityID = 1;

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act
            var response = activitiesController.RejectDictation(activityID, observation);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Accept_Dictation_But_Fails()
        {
            //Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var observation = new string('x', 20);
            var activityID = 1;
            var dictationNumber = "AA-2017-1-X-A";
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now;

            AcceptDictationViewModel AcceptDictationViewModel = new AcceptDictationViewModel()
            {
                Observation = observation,
                DictationNumber = dictationNumber,
                DateFrom = dateFrom,
                DateTo = dateTo

            };


            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            activityServiceMock.Setup(x => x.AcceptDictation(activityID, observation, dictationNumber, dateFrom, dateTo)).Throws(new ExceptionBase());
            // Act
            var response = activitiesController.AcceptDictation(activityID, AcceptDictationViewModel);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [Test]
        public void Should_Reject_Dictation_But_Fails()
        {
            //Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var rejectViewModel = new RejectDictationViewModel();
            var observation = new string('x', 20);
            var activityID = 1;

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            activityServiceMock.Setup(x => x.RejectDictation(activityID, observation)).Throws(new ExceptionBase());
            // Act
            var response = activitiesController.RejectDictation(activityID, rejectViewModel);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [Test]
        public void Should_Add_Disposition()
        {
            //Arrange
            var activityMock = new Mock<Activity>();
            var activityServiceMock = new Mock<IActivityService>();
            activityServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(activityMock.Object);
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var disposition = new DispositionViewModel();
            var activityID = 1;

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            // Act
            var response = activitiesController.AddDisposition(activityID, disposition);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        }

        [Test]
        public void Should_Find_Activities()
        {
            //Arrange

            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(activityServiceMock.Object);
            var inapCode = "125";
            var description = "test";
            var listActivityMock = new List<Activity>();

            activityServiceMock.Setup(x => x.FindByDescription(It.IsAny<string>())).Returns(listActivityMock);
            activityServiceMock.Setup(x => x.FindByInapCode(It.IsAny<string>())).Returns(listActivityMock);

            activitiesController.Request = new HttpRequestMessage();
            activitiesController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            FindActivityViewModel FindActivityViewModel = new FindActivityViewModel()
            {
                InapCode = inapCode,
                Description = description
            };

            // Act
            var response = activitiesController.FindActivity(FindActivityViewModel);

            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.EqualTo(listActivityMock));

        }

    }


}
