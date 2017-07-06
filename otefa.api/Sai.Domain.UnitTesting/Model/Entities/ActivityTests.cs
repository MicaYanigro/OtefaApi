using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Exceptions.Activity.Dictation;
using Sai.Domain.Model.Exceptions.Activity.Duration;
using Sai.Domain.Model.Exceptions.Activity.Name;
using Sai.Domain.Model.Exceptions.Activity.Observation;
using Sai.Domain.Model.Exceptions.Activity.ParticipantProfile;
using Sai.Domain.Model.Exceptions.Area;
using Sai.Domain.Model.Exceptions.Category;
using Sai.Domain.Model.Exceptions.ModalityType;
using Sai.Domain.Model.Exceptions.Program;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class ActivityTests
    {

        [Test]
        public void Should_Create_An_Activity()
        {

            // Arrange

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();

            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);

            // Assert

            Assert.That(activity, Is.InstanceOf<Activity>());
            Assert.That(activity.Area, Is.EqualTo(areaMock.Object));
            Assert.That(activity.ResponsableActivity, Is.EqualTo(userMock.Object));
            Assert.That(activity.Category, Is.EqualTo(categoryMock.Object));
            Assert.That(activity.RlmNumber, Is.EqualTo(caseFileNumber));
            Assert.That(activity.Duration, Is.EqualTo(durationHours));
            Assert.That(activity.IsUniqueInscriptionAllowed, Is.EqualTo(isUniqueInscriptionAllowed));
            Assert.That(activity.ModalityType.Description, Is.EqualTo(modalityTypeDescription));
            Assert.That(activity.Name, Is.EqualTo(name));
            Assert.That(activity.Observation, Is.EqualTo(observation));
            Assert.That(activity.ParticipantProfile, Is.EqualTo(participantProfile));
            Assert.That(activity.Program.Description, Is.EqualTo(programDescription));
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.DesignLoadedPendingDictation));
        }


        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_durationHours_Is_Null()
        {
            // Arrange

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationHours = 0;
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

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullDurationException>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_Name_Length_Is_More_Than_500_Characters()
        {
            // Arrange

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var name = new string('x', 501);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityNameLengthException>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_Name_Is_Null()
        {
            // Arrange

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var name = (string)null;
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullActivityNameException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_Name_Is_Empty()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var name = "";
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyActivityName>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_Observation_Length_Is_More_Than_500_Characters()
        {
            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var observation = new string('x', 501);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityObservationLengthException>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_ParticipantProfile_Length_Is_More_Than_2500_Characters()
        {
            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var participantProfile = new string('x', 2501);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityParticipantProfileLengthException>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_ParticipantProfile_Is_Null()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var participantProfile = (string)null;
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullActivityParticipantProfileException>());
        }

        [Test]
        public void Should_Create_A_Category_But_Fails_Because_ParticipantProfile_Is_Empty()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var participantProfile = "";
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);



            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyActivityParticipantProfile>());
        }


        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_ModalityType_Is_Null()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var modalityType = (ModalityType)null;
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullModalityTypeDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_Program_Is_Null()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;


            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
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
            var program = (Program)null;
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));
            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullProgramDescriptionException>());
        }

        //[Test]
        //public void Should_Create_An_Activity_But_Fails_Because_Area_Is_Null()
        //{
        //    // Arrange

        //    var containerMock = new Mock<IContainer>();
        //    var userServiceMock = new Mock<IUserService>();
        //    containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
        //    Container.Current = containerMock.Object;

        //    var random = new Random();

        //    var address = new string('x', 500);
        //    var areaMock = (Area)null;
        //    var userMock = new Mock<User>();
        //    var attendanceAndApprovalRequirements = new string('x', 500);
        //    var bibliography = new string('x', 500);
        //    var categoryMock = new Mock<Category>();
        //    var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
        //    var cityName = new string('x', 40);
        //    var city = new string('x', 40);
        //    var province = new string('x', 40);
        //    var country = new string('x', 40);
        //    var content = new string('x', 500);
        //    var demandOrigin = new string('x', 500);
        //    var durationHours = random.Next(1, 10);
        //    var evaluationTools = new string('x', 500);
        //    var expectedContribution = new string('x', 500);
        //    var hasAGrant = true;
        //    var isUniqueInscriptionAllowed = true;
        //    var justification = new string('x', 500);
        //    var learningEvaluation = new string('x', 500);
        //    var methodologicalStrategiesAndDidacticResources = new string('x', 500);
        //    var modalityTypeDescription = new string('x', 30);
        //    var modalityType = new ModalityType(modalityTypeDescription);
        //    var name = new string('x', 500);
        //    var objectives = new string('x', 500);
        //    var observation = new string('x', 500);
        //    var participantProfile = new string('x', 500);
        //    var place = new string('x', 500);
        //    var programDescription = new string('x', 30);
        //    var program = new Program(programDescription);
        //    var status = new string('x', 500);
        //    var teacherProfile = new string('x', 500);

        //    // Act

        //    var caughtException = Assert.Catch(() => new Activity(areaMock,
        //                                categoryMock.Object,
        //                                caseFileNumber,
        //                                durationHours,
        //                                isUniqueInscriptionAllowed,
        //                                modalityType,
        //                                name,
        //                                observation,
        //                                participantProfile,
        //                                program,
        //                                userMock.Object));
        //    // Assert

        //    Assert.That(caughtException, Is.InstanceOf<NullAreaBriefDescriptionException>());
        //}

        [Test]
        public void Should_Create_An_Activity_But_Fails_Because_Category_Is_Null()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = (Category)null;
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
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
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Activity(areaMock.Object,
                                        categoryMock,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullCategoryBriefDescriptionException>());
        }

        [Test]
        public void Should_Get_Inap_Code()
        {


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Arrange
            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
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
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            // Act

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);

            activity.SaveInapCode();

            var inapCode = activity.GetInapCode();


            // Assert
            Assert.That(inapCode, Is.Not.Null);

        }

        #region Commission

        [Test]
        public void Should_Add_Commission()
        {


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Arrange
            var vacancy = 10;
            var oversold = 2;
            IEnumerable<Commission> commissionsResult;

            var random = new Random();

            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);

            // Act

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);



            var commissionMock = new Mock<Commission>();
            var commissionFactoryMock = new Mock<ICommissionFactory>();

            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            commissionFactoryMock.Setup(x => x.Create(activity, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours)).Returns(commissionMock.Object);

            containerMock.Setup(x => x.Resolve<ICommissionFactory>()).Returns(commissionFactoryMock.Object);

            Container.Current = containerMock.Object;

            //Act
            activity.AddCommission(vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);
            commissionsResult = activity.GetCommissions();

            // Assert
            Assert.That(commissionsResult, Is.Not.Null);
            Assert.That(commissionsResult.Count() == 1);
        }

        [Test]
        public void Shouldnt_Create_A_Commission_If_User_Doesnt_Belong_To_Commission_User_Role()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var vacancy = 22;
            var oversold = 10;

            var random = new Random();

            var address = new string('x', 500);
            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
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
            var status = new string('x', 500);
            var teacherProfile = new string('x', 500);

            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;
            // Act

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);


            userServiceMock.Setup(x => x.RequireBackendPermission(It.IsAny<BackendPermissions>())).Throws<AuthorizationException>();

            // Act
            Exception e = Assert.Catch(() => activity.AddCommission(vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert
            Assert.That(e, Is.InstanceOf<AuthorizationException>());
        }

        #endregion Commission


        [Test]
        public void Should_Create_A_Dictation()
        {

            var teachersRepositoryMock = new Mock<ITeachersRepository>();
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<ITeachersRepository>()).Returns(teachersRepositoryMock.Object);
            Container.Current = containerMock.Object;

            var dictation = new Mock<Dictation>();

            var credits = 22;

            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            var random = new Random();

            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherID = random.Next(1, 10);

            // Act

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);


            activity.AddTeacher(teacherID);

            // Act

            activity.AddDictation(credits, evaluationCriteriaWithConsideration, durationHours);

            // Assert

            Assert.That(activity.getPendingDictation(), Is.Not.Null);
            Assert.That(activity.getPendingDictation().Credits, Is.EqualTo(0)); //Credits no está disponible si no hay disposición.
            Assert.That(activity.getPendingDictation().GetEvaluationCriteriaWithConsiderations(), Is.SameAs(evaluationCriteriaWithConsideration));
        }


        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Activity_Already_Has_Dictation()
        {
            var teachersRepositoryMock = new Mock<ITeachersRepository>();
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<ITeachersRepository>()).Returns(teachersRepositoryMock.Object);
            Container.Current = containerMock.Object;

            var dictation = new Mock<Dictation>();

            var credits = 22;

            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            var random = new Random();

            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherID = random.Next(1, 10);


            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);



            activity.AddTeacher(teacherID);

            activity.AddDictation(credits, evaluationCriteriaWithConsideration, durationHours);



            // Act

            var caughtException = Assert.Catch(() => activity.AddDictation(credits, evaluationCriteriaWithConsideration, durationHours));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<PendingDictationAlreadyExistsException>());
        }

        [Test]
        public void Should_Add_Equivalence()
        {
            var random = new Random();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherID = random.Next(1, 10);



            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);


            var activityEquivalence = new Mock<Activity>();


            // Act

            activity.AddEquivalence(activityEquivalence.Object);
            var count = activity.GetEquivalences().Count();

            // Assert

            Assert.That(activity, Is.Not.Null);
            Assert.That(count, Is.Not.Null);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Should_Validate_Equivalences()
        {
            var random = new Random();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var areaMock = new Mock<Area>();
            var userMock = new Mock<User>();
            var categoryMock = new Mock<Category>();
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var teacherID = random.Next(1, 10);
            var technicalFormNumber = "EX-2016-02887279-APN-SECMA#MM";

            var activity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);

            ICollection<Tuple<int, IEnumerable<int>>> tuple = new List<Tuple<int, IEnumerable<int>>>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            activityRepositoryMock.Setup(x => x.GetActivityIdsWithEquivalences()).Returns(tuple);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            Container.Current = containerMock.Object;

            var targetActivity = new Activity(areaMock.Object,
                                        categoryMock.Object,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);

            activity.AddEquivalence(targetActivity);

            //Act

            var response = activity.IsEquivalentTo(targetActivity);

            activityRepositoryMock.Verify(x => x.GetActivityIdsWithEquivalences(), Times.Once);
            Assert.That(response == true);
        }
    }
}