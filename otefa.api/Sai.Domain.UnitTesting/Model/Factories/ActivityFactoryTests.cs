using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class ActivityFactoryTests
    {

        [Test]
        public void Should_Create()
        {
            // Arrange

            var random = new Random();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var activityServiceMock = new Mock<IActivityService>();
            var areaServiceMock = new Mock<IAreaService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var programServiceMock = new Mock<IProgramService>();

            activityRepositoryMock.Setup(x => x.GetLastActivityInapNumber()).Returns(1);

            var containerMock = new Mock<IContainer>();


            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);


            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IActivityService>()).Returns(activityServiceMock.Object);

            Container.Current = containerMock.Object;

            var areaMock = new Mock<Area>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(areaMock.Object);

            var categoryMock = new Mock<Category>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(categoryMock.Object);

            var modalityTypeMock = new Mock<ModalityType>();
            modalityTypeServiceMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(modalityTypeMock.Object);

            var programMock = new Mock<Program>();
            programServiceMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(programMock.Object);

            var userMock = new Mock<User>();
            var userRepositoryMock = new Mock<IUserRepository>();

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
            var userID = 1;

            var teacherProfile = new string('x', 500);

            var activityFactory = new ActivityFactory();

            activityFactory.AreaServices = areaServiceMock.Object;
            activityFactory.CategoryService = categoryServiceMock.Object;
            activityFactory.ModalityTypeService = modalityTypeServiceMock.Object;
            activityFactory.ProgramService = programServiceMock.Object;

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            // Act

            var activity = activityFactory.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object);

            // Assert

            Assert.That(activity, Is.InstanceOf<Activity>());
            Assert.That(activity.Area, Is.InstanceOf<Area>());
            Assert.That(activity.Category, Is.InstanceOf<Category>());
            Assert.That(activity.RlmNumber, Is.EqualTo(caseFileNumber));
            Assert.That(activity.Duration, Is.EqualTo(durationHours));
            Assert.That(activity.IsUniqueInscriptionAllowed, Is.EqualTo(isUniqueInscriptionAllowed));
            Assert.That(activity.ModalityType, Is.InstanceOf<ModalityType>());
            Assert.That(activity.Name, Is.EqualTo(name));
            Assert.That(activity.Observation, Is.EqualTo(observation));
            Assert.That(activity.ParticipantProfile, Is.EqualTo(participantProfile));
            Assert.That(activity.Program, Is.InstanceOf<Program>());
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.DesignLoadedPendingDictation));
        }

        [Test]
        public void Should_Create_Jurisdictional()
        {
            // Arrange

            var random = new Random();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var activityServiceMock = new Mock<IActivityService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var organismServiceMock = new Mock<IOrganismService>();

            activityRepositoryMock.Setup(x => x.GetLastActivityInapNumber()).Returns(1);

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IActivityService>()).Returns(activityServiceMock.Object);

            Container.Current = containerMock.Object;

            var categoryMock = new Mock<Category>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(categoryMock.Object);

            var modalityTypeMock = new Mock<ModalityType>();
            modalityTypeServiceMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(modalityTypeMock.Object);

            var organismMock = new Mock<Organism>();
            organismServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(organismMock.Object);

            var userMock = new Mock<User>();

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
            var province = "province";
            var municipality = "municipality";
            var userID = 1;

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            var activityFactory = new ActivityFactory();

            activityFactory.CategoryService = categoryServiceMock.Object;
            activityFactory.ModalityTypeService = modalityTypeServiceMock.Object;
            activityFactory.OrganismService = organismServiceMock.Object;

            // Act

            var activity = activityFactory.JurisdictionalCreate(categoryMock.Object.BriefDescription,
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
                                                  pac,
                                                  province,
                                                  municipality,
                                                  userMock.Object);

            // Assert

            Assert.That(activity, Is.InstanceOf<Activity>());
            Assert.That(activity.Category, Is.InstanceOf<Category>());
            Assert.That(activity.Duration, Is.EqualTo(durationHours));
            Assert.That(activity.IsUniqueInscriptionAllowed, Is.EqualTo(isUniqueInscriptionAllowed));
            Assert.That(activity.ModalityType, Is.InstanceOf<ModalityType>());
            Assert.That(activity.Name, Is.EqualTo(name));
            Assert.That(activity.Observation, Is.EqualTo(observation));
            Assert.That(activity.ParticipantProfile, Is.EqualTo(participantProfile));
        }

        [Test]
        public void Should_Generate_Inap_Number()
        {
            // Arrange

            var random = new Random();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var activityServiceMock = new Mock<IActivityService>();
            var areaServiceMock = new Mock<IAreaService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var programServiceMock = new Mock<IProgramService>();

            var lastInapNumber = 1;

            activityRepositoryMock.Setup(x => x.GetLastActivityInapNumber()).Returns(lastInapNumber);

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IActivityService>()).Returns(activityServiceMock.Object);

            Container.Current = containerMock.Object;

            var areaMock = new Mock<Area>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(areaMock.Object);

            var categoryMock = new Mock<Category>();
            categoryServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns(categoryMock.Object);

            var modalityTypeMock = new Mock<ModalityType>();
            modalityTypeServiceMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(modalityTypeMock.Object);

            var programMock = new Mock<Program>();
            programServiceMock.Setup(x => x.GetByDescription(It.IsAny<string>())).Returns(programMock.Object);

            var userMock = new Mock<User>();
            var userRepositoryMock = new Mock<IUserRepository>();


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
            var durationDetails = new string('x', 500);
            var duration = new Duration(durationHours, durationDetails);
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
            var userID = 1;

            var activityFactory = new ActivityFactory();

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            activityFactory.AreaServices = areaServiceMock.Object;
            activityFactory.CategoryService = categoryServiceMock.Object;
            activityFactory.ModalityTypeService = modalityTypeServiceMock.Object;
            activityFactory.ProgramService = programServiceMock.Object;
            // Act

            var activity = activityFactory.Create(areaBriefDescription,
                                                  categoryBriefDescription,
                                                  caseFileNumber,
                                                  durationHours,
                                                  isUniqueInscriptionAllowed,
                                                  modalityTypeDescription,
                                                  name,
                                                  observation,
                                                  participantProfile,
                                                  programDescription,
                                                  userMock.Object);

            Assert.That(activity.InapNumber, Is.EqualTo(lastInapNumber + 1));
        }

    }
}