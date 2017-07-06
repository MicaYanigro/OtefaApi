using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Exceptions.Activity.InapNumber;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System;
using System.Configuration;
using Sai.Domain.Model.Repositories;

namespace Sai.Domain.IntegrationTesting.Model.Factories
{
    [TestFixture]
    public class ActivityFactoryTests
    {

        [Test]
        public void Should_Get_Last_Inap_Number_By_AppSettings()
        {

            // Arrange

            var firstValue = "1234";

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings["activity:inapnumber"] == null)
            {
                config.AppSettings.Settings.Add("activity:inapnumber", firstValue);
            }
            else
            {
                config.AppSettings.Settings["activity:inapnumber"].Value = firstValue;
            }

            config.Save();

            ConfigurationManager.RefreshSection("appSettings");

            var random = new Random();

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var areaServiceMock = new Mock<IAreaService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var programServiceMock = new Mock<IProgramService>();
            var userServiceMock = new Mock<IUserService>();

            activityRepositoryMock.Setup(x => x.GetLastActivityInapNumber()).Returns(0);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

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

            var areaBriefDescription = new string('x', 2);
            var categoryBriefDescription = new string('x', 2);
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var durationDetails = new string('x', 500);
            var duration = new Duration(durationHours, durationDetails);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
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

            // Assert

            Assert.That(activity.InapNumber, Is.EqualTo(int.Parse(firstValue) + 1));

        }


        [Test]
        public void Should_Get_Last_Inap_Number_By_AppSettings_But_Fails_Because_Its_Not_Configurated()
        {

            // Arrange

            var random = new Random();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("activity:inapnumber");
            config.Save();

            ConfigurationManager.RefreshSection("appSettings");

            var activityRepositoryMock = new Mock<IActivityRepository>();
            var areaServiceMock = new Mock<IAreaService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            var modalityTypeServiceMock = new Mock<IModalityTypeService>();
            var programServiceMock = new Mock<IProgramService>();
            var userServiceMock = new Mock<IUserService>();

            activityRepositoryMock.Setup(x => x.GetLastActivityInapNumber()).Returns(0);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IActivityRepository>()).Returns(activityRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

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

            var areaBriefDescription = new string('x', 2);
            var categoryBriefDescription = new string('x', 2);
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var durationHours = random.Next(1, 10);
            var durationDetails = new string('x', 500);
            var duration = new Duration(durationHours, durationDetails);
            var isUniqueInscriptionAllowed = true;
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var status = new string('x', 500);
            var userID = 1;

            var activityFactory = new ActivityFactory();

            userRepositoryMock.Setup(x => x.GetById(userID)).Returns(userMock.Object);

            activityFactory.AreaServices = areaServiceMock.Object;
            activityFactory.CategoryService = categoryServiceMock.Object;
            activityFactory.ModalityTypeService = modalityTypeServiceMock.Object;
            activityFactory.ProgramService = programServiceMock.Object;

            // Act

            Exception e = Assert.Catch(() => activityFactory.Create(areaBriefDescription,
                                                                    categoryBriefDescription,
                                                                    caseFileNumber,
                                                                    durationHours,
                                                                    isUniqueInscriptionAllowed,
                                                                    modalityTypeDescription,
                                                                    name,
                                                                    observation,
                                                                    participantProfile,
                                                                    programDescription,
                                                                    userMock.Object));

            // Assert

            Assert.That(e, Is.InstanceOf<InapNumberNotConfiguredException>());

        }

    }
}