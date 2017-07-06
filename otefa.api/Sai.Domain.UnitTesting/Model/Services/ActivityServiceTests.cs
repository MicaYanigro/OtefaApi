using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Activity;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class ActivityServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_An_Activity()
        {
            // Arrange

            var address = new string('x', 500);
            var areaBriefDescription = new string('x', 2);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryBriefDescription = new string('x', 2);
            var caseFileNumber = new string('x', 25);
            var cityName = new string('x', 40);
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationDetails = new string('x', 500);
            var durationHours = 1;
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);
            var equivalences = new List<int>();
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var userID = 1;


            // Act

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var programRepositoryMock = new Mock<IProgramRepository>();
            var areaRepositoryMock = new Mock<IAreaRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);


            var activityService = new ActivityService();

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.ProgramRepository = programRepositoryMock.Object;
            activityService.CategoryRepository = categoryRepositoryMock.Object;
            activityService.AreaRepository = areaRepositoryMock.Object;
            activityService.UserRepository = userRepositoryMock.Object;

            var activityMock = new Mock<Activity>();
            var programMock = new Mock<Program>();
            var categoryMock = new Mock<Category>();
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();

            areaRepositoryMock.Setup(x => x.GetByBriefDescription(areaBriefDescription)).Returns(areaMock.Object);
            areaMock.Setup(x => x.IsActive).Returns(true);

            programRepositoryMock.Setup(x => x.GetByDescription(programDescription)).Returns(programMock.Object);
            programMock.Setup(x => x.IsActive).Returns(true);

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            var activityFactoryMock = new Mock<IActivityFactory>();
            var categoryFactoryMock = new Mock<ICategoryFactory>();
            //var userFactoryMock = new Mock<IUserFactory>();

            categoryRepositoryMock.Setup(x => x.GetByBriefDescription(categoryBriefDescription)).Returns(categoryMock.Object);
            categoryMock.Setup(x => x.IsActive).Returns(true);




            activityFactoryMock.Setup(x => x.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object)).Returns(activityMock.Object);

            activityService.ActivityFactory = activityFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();

            activityService.UserService = userServiceMock.Object;

            // Act
            var createdActivity = activityService.Create(areaBriefDescription,
                                                  categoryBriefDescription,
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
                                                  userID);

            // Assert

            activityFactoryMock.Verify(x => x.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object), Times.Once);

            activityRepositoryMock.Verify(x => x.Add(activityMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void Should_Create_A_JurisdictionalActivity()
        {
            // Arrange
            var random = new Random();
            var categoryMock = new Mock<Category>();
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var sector = 1;
            var Ambit = 1;
            var rlmNumber = new string('x', 10);
            var equivalences = new List<int>();
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var pac = 1;
            var pacObjectives = new string('x', 10);
            var pacAreas = new string('x', 10);
            var organismDescription = "abcd";
            var province = "province";
            var municipality = "municipality";
            var userID = 1;

            // Act

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var activityService = new ActivityService();

            activityService.ActivityRepository = activityRepositoryMock.Object;

            var activityMock = new Mock<JurisdictionalActivity>();
            var userMock = new Mock<User>();
            var activityFactoryMock = new Mock<IActivityFactory>();

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            activityFactoryMock.Setup(x => x.JurisdictionalCreate(categoryMock.Object.BriefDescription,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  sector,
                                                  Ambit,
                                                  rlmNumber,
                                                  organismDescription,
                                                  pac,
                                                  province,
                                                  municipality,
                                                  userMock.Object)).Returns(activityMock.Object);

            activityService.ActivityFactory = activityFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();

            activityService.UserService = userServiceMock.Object;

            // Act
            var createdActivity = activityService.JurisdictionalCreate(categoryMock.Object.BriefDescription,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  sector,
                                                  Ambit,
                                                  rlmNumber,
                                                  organismDescription,
                                                  equivalences,
                                                  teachers,
                                                  correlatives,
                                                  pac,
                                                  province,
                                                  municipality,
                                                  userID);

            // Assert

            activityFactoryMock.Verify(x => x.JurisdictionalCreate(categoryMock.Object.BriefDescription,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  sector,
                                                  Ambit,
                                                  rlmNumber,
                                                  organismDescription,
                                                  pac,
                                                  province,
                                                  municipality,
                                                  userMock.Object), Times.Once);

            activityRepositoryMock.Verify(x => x.Add(activityMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }




        [Test]
        public void Should_Create_An_Activity_With_Equivalences()
        {
            // Arrange

            var address = new string('x', 500);
            var areaBriefDescription = new string('x', 2);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryBriefDescription = new string('x', 2);
            var caseFileNumber = new string('x', 25);
            var cityName = new string('x', 40);
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationDetails = new string('x', 500);
            var durationHours = 1;
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var teacherProfile = new string('x', 500);
            var equivalences = new List<int>() { 1, 2 };
            var teachers = new List<int>();
            var correlatives = new List<int>();
            var userID = 1;


            // Act

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityMock = new Mock<Activity>();
            var programMock = new Mock<Program>();
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();
            var userMock = new Mock<User>();
            activityMock.Setup(x => x.AddEquivalence(It.IsAny<Activity>()));

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var areaRepositoryMock = new Mock<IAreaRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var programRepositoryMock = new Mock<IProgramRepository>();
            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(activityMock.Object);


            categoryRepositoryMock.Setup(x => x.GetByBriefDescription(categoryBriefDescription)).Returns(categoryMock.Object);
            categoryMock.Setup(x => x.IsActive).Returns(true);

            var activityService = new ActivityService();

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.ProgramRepository = programRepositoryMock.Object;
            activityService.UserRepository = userRepositoryMock.Object;

            programRepositoryMock.Setup(x => x.GetByDescription(programDescription)).Returns(programMock.Object);
            programMock.Setup(x => x.IsActive).Returns(true);
            activityService.CategoryRepository = categoryRepositoryMock.Object;
            activityService.AreaRepository = areaRepositoryMock.Object;

            areaRepositoryMock.Setup(x => x.GetByBriefDescription(areaBriefDescription)).Returns(areaMock.Object);
            areaMock.Setup(x => x.IsActive).Returns(true);

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            var activityFactoryMock = new Mock<IActivityFactory>();

            activityFactoryMock.Setup(x => x.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object)).Returns(activityMock.Object);

            activityService.ActivityFactory = activityFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();

            activityService.UserService = userServiceMock.Object;

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var createdActivity = activityService.Create(areaBriefDescription,
                                                  categoryBriefDescription,
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
                                                  userID);

            // Assert

            activityFactoryMock.Verify(x => x.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object), Times.Once);

            activityRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Exactly(2));
            activityRepositoryMock.Verify(x => x.Add(activityMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }


        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var allActivities = new List<Activity>().AsQueryable();

            activityRepositoryMock.Setup(x => x.All()).Returns(allActivities);

            ActivityService activityService = new ActivityService();
            activityService.ActivityRepository = activityRepositoryMock.Object;

            // Act

            var activity = activityService.GetAll();

            // Assert

            Assert.That(activity, Is.Not.Null);

            activityRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

        #region GetById

        [Test]
        public void Should_Get_By_Id()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var random = new Random();

            var activityId = random.Next();

            var activityMock = new Mock<Activity>();

            activityRepositoryMock.Setup(x => x.GetById(activityId)).Returns(activityMock.Object);

            var activityService = new ActivityService();
            activityService.ActivityRepository = activityRepositoryMock.Object;

            // Act

            var activity = activityService.GetById(activityId);

            // Assert

            Assert.That(activity, Is.Not.Null);
            Assert.That(activity, Is.SameAs(activityMock.Object));

            activityRepositoryMock.Verify(x => x.GetById(activityId), Times.Once);
        }

        #endregion GetById

        #region Find

        [Test]
        public void Should_Find_Activities_By_InapCode()
        {
            // Arrange
            var inapCode = "125";
            var description = (string)null;

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var activityMock = new Mock<Activity>();
            var activityList = new List<Activity>() { activityMock.Object };

            activityRepositoryMock.Setup(x => x.GetByInapCode(It.IsAny<string>())).Returns(activityList);

            var activityService = new ActivityService();
            activityService.ActivityRepository = activityRepositoryMock.Object;

            // Act

            var activity = activityService.FindByInapCode(inapCode);

            // Assert

            Assert.That(activity, Is.Not.Null);
            Assert.That(activity, Is.SameAs(activityList));

            activityRepositoryMock.Verify(x => x.GetByInapCode(inapCode), Times.Once);
        }

        [Test]
        public void Should_Find_Activities_By_Description()
        {
            // Arrange
            var inapCode = (string)null;
            var description = "Test";

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var activityMock = new Mock<Activity>();
            var activityList = new List<Activity>() { activityMock.Object };

            activityRepositoryMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(activityList);

            var activityService = new ActivityService();
            activityService.ActivityRepository = activityRepositoryMock.Object;

            // Act

            var activity = activityService.FindByDescription(description);

            // Assert

            Assert.That(activity, Is.Not.Null);
            Assert.That(activity, Is.SameAs(activityList));

            activityRepositoryMock.Verify(x => x.GetByDescription(description), Times.Once);
        }

        #endregion Find

        #region AddCommission

        [Test]
        public void Should_Add_A_Commission()
        {
            // Arrange

            var vacancy = 15;
            var oversold = 2;
            var activityID = 0;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var activityMock = new Mock<Activity>();

            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var commissionRepositoryMock = new Mock<ICommissionRepository>();

            var commissionMock = new Mock<Commission>();
            var commissionFactoryMock = new Mock<ICommissionFactory>();
            var containerMock = new Mock<IContainer>();

            commissionMock.Setup(x => x.GenerateLink());
            activityMock.Setup(x => x.AddCommission(vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Returns(commissionMock.Object);

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns(activityMock.Object);
            commissionRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.CommissionRepository = commissionRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;

            commissionFactoryMock.Setup(x => x.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Returns(commissionMock.Object);

            containerMock.Setup(x => x.Resolve<ICommissionFactory>()).Returns(commissionFactoryMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //Act

            activityService.AddCommission(activityID, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);

            //Assert

            activityMock.Verify(x => x.AddCommission(vacancy, oversold, commissionInscriptionType, enrollmentCloseHours), Times.Once);
            activityRepositoryMock.Verify(x => x.GetById(activityID), Times.Once);
            activityRepositoryMock.Verify(x => x.Update(activityMock.Object), Times.Once);
            activityRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_Add_A_Commission_But_Fails_Because_Activity_Is_Not_Found()
        {
            var vacancy = 15;
            var oversold = 2;
            var activityID = 0;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            var activityMock = new Mock<Activity>();

            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var commissionMock = new Mock<Commission>();
            var commissionFactoryMock = new Mock<ICommissionFactory>();
            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns((Activity)null);

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;

            commissionFactoryMock.Setup(x => x.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Returns(commissionMock.Object);

            containerMock.Setup(x => x.Resolve<ICommissionFactory>()).Returns(commissionFactoryMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //Act
            Exception e = Assert.Catch(() => activityService.AddCommission(activityID, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert
            Assert.That(e, Is.InstanceOf<NullActivityException>());

        }

        #endregion AddCommission

        #region AddDictation

        [Test]
        public void Should_Add_A_Dictation()
        {
            // Assert

            var activityID = 0;
            var credits = 22;

            var activityDuration = 2;
            var random = new Random();

            var evaluationCriteriaId = random.Next();
            var evaluationCriteriaConsiderationId = random.Next();

            var evaluationCriteriaWithConsideration = new Dictionary<int, int>() { { evaluationCriteriaId, evaluationCriteriaConsiderationId } };

            var activityMock = new Mock<Activity>();

            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns(activityMock.Object);

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();
            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();
            var evaluationCriteriaConsiderationMock = new Mock<EvaluationCriteriaConsideration>();

            evaluationCriteriaServiceMock.Setup(x => x.GetById(evaluationCriteriaId)).Returns(evaluationCriteriaMock.Object);
            evaluationCriteriaConsiderationServiceMock.Setup(x => x.GetById(evaluationCriteriaConsiderationId)).Returns(evaluationCriteriaConsiderationMock.Object);

            activityService.EvaluationCriteriaService = evaluationCriteriaServiceMock.Object;
            activityService.EvaluationCriteriaConsiderationService = evaluationCriteriaConsiderationServiceMock.Object;

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;

            activityMock.Setup(x => x.AddDictation(credits, It.IsAny<IEnumerable<EvaluationCriteriaWithConsideration>>(), activityDuration));
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //Act

            activityService.AddDictation(activityID, credits, evaluationCriteriaWithConsideration, activityDuration);

            //Assert

            activityRepositoryMock.Verify(x => x.GetById(activityID), Times.Once);
            evaluationCriteriaServiceMock.Verify(x => x.GetById(evaluationCriteriaId), Times.Once);
            evaluationCriteriaConsiderationServiceMock.Verify(x => x.GetById(evaluationCriteriaConsiderationId), Times.Once);
            activityRepositoryMock.Verify(x => x.Update(activityMock.Object), Times.Once);
            activityRepositoryMock.Verify(x => x.Context, Times.Once);
        }

        [Test]
        public void Should_Add_A_Dictation_But_Fails_Because_Activity_Is_Not_Found()
        {
            // Assert

            var activityID = 0;
            var credits = 22;
            var evaluationCriteriaWithConsideration = new Dictionary<int, int>();
            var activityDuration = 2;

            var activityMock = new Mock<Activity>();

            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns((Activity)null);

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;

            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //Act

            Exception e = Assert.Catch(() => activityService.AddDictation(activityID, credits, evaluationCriteriaWithConsideration, activityDuration));

            // Assert

            Assert.That(e, Is.InstanceOf<NullActivityException>());
        }

        #endregion AddDictation

        #region Dictation
        [Test]
        public void Should_Reject_Dictation()
        {
            var activityID = 0;
            var credits = 2;
            var activityMock = new Mock<Activity>();
            var dictationMock = new Mock<Dictation>();
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();
            var technicalFormNumber = "EX-2016-02887279-APN-SECMA#MM";
            var activityDuration = 2;

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            string observation = new string('x', 20);
            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns(activityMock.Object);

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;

            activityMock.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);

            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //Act
            activityService.RejectDictation(activityID, observation);

            // Assert

            activityRepositoryMock.Verify((x => x.Context), Times.Once);
            activityRepositoryMock.Verify(x => x.GetById(activityID), Times.Once);
        }

        [Test]
        public void Should_Accept_Dictation()
        {
            var activityID = 0;
            var activityMock = new Mock<Activity>();
            var dictationMock = new Mock<Dictation>();
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();
            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            var dictationNumber = "AA-2017-1-X-A";
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now;

            string observation = new string('x', 20);
            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();

            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var activityFactoryMock = new Mock<IActivityFactory>();

            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns(activityMock.Object);

            activityService.ActivityRepository = activityRepositoryMock.Object;

            activityService.UserService = userServiceMock.Object;


            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            //dictationMock.SetupGet(x => x.Activity).Returns(activityMock.Object);

            activityMock.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);
            //Act
            activityService.AcceptDictation(activityID, observation, dictationNumber, dateFrom, dateTo);

            // Assert

            activityRepositoryMock.Verify((x => x.Context), Times.Once);
            activityRepositoryMock.Verify(x => x.GetById(activityID), Times.Once);
        }
        #endregion Dictation

        #region Disposition
        [Test]
        public void Should_Add_Disposition()
        {
            var activityID = 0;
            var disposition = "DI-2017-12457281-APN-INAP";
            var activityMock = new Mock<Activity>();
            var dictationMock = new Mock<Dictation>();
            dictationMock.Setup(x => x.Status).Returns(DictationStatus.Accepted);
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();
            var technicalFormNumber = new string('x', 40);
            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            string observation = new string('x', 20);
            ActivityService activityService = new ActivityService();

            var userServiceMock = new Mock<IUserService>();
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            var containerMock = new Mock<IContainer>();

            activityRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            activityRepositoryMock.Setup(x => x.GetById(activityID)).Returns(activityMock.Object);

            activityService.ActivityRepository = activityRepositoryMock.Object;
            activityService.UserService = userServiceMock.Object;


            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            activityMock.Setup(x => x.getActiveDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getPendingDictation()).Returns(dictationMock.Object);
            activityMock.Setup(x => x.getAcceptedDictation()).Returns(dictationMock.Object);
            //Act
            activityService.AddDisposition(activityID, disposition);

            // Assert

            activityRepositoryMock.Verify((x => x.Context), Times.Once);
            activityRepositoryMock.Verify(x => x.GetById(activityID), Times.Once);
        }
        #endregion Disposition
    }
}