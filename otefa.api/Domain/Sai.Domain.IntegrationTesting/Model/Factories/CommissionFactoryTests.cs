using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions.Commission.InapNumber;
using Sai.Domain.Model.Factories;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System;
using System.Configuration;

namespace Sai.Domain.IntegrationTesting.Model.Factories
{
    [TestFixture]
    public class CommissionFactoryTests
    {

        [Test]
        public void Should_Get_Latest_Inap_Number_By_AppSettings()
        {

            // Arrange

            var firstValue = "1234";

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings["commission:inapnumber"] == null)
            {
                config.AppSettings.Settings.Add("commission:inapnumber", firstValue);
            }
            else
            {
                config.AppSettings.Settings["commission:inapnumber"].Value = firstValue;
            }

            config.Save();

            ConfigurationManager.RefreshSection("appSettings");

            var random = new Random();

            var commissionRepositoryMock = new Mock<ICommissionRepository>();

            commissionRepositoryMock.Setup(x => x.GetLatestCommissionInapNumber()).Returns(0);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICommissionRepository>()).Returns(commissionRepositoryMock.Object);

            Container.Current = containerMock.Object;

            var activityMock = new Mock<Activity>();
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();

            activityMock.Setup(x => x.Area).Returns(areaMock.Object);
            activityMock.Setup(x => x.Category).Returns(categoryMock.Object);

            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            var commissionFactory = new CommissionFactory();

            // Act

            var commission = commissionFactory.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);

            // Assert

            Assert.That(commission.InapNumber, Is.EqualTo(int.Parse(firstValue) + 1));

        }

        [Test]
        public void Should_Get_Latest_Inap_Number_By_AppSettings_But_Fails_Because_Its_Not_Configured()
        {
            // Arrange

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("commission:inapnumber");
            config.Save();

            ConfigurationManager.RefreshSection("appSettings");

            var random = new Random();

            var commissionRepositoryMock = new Mock<ICommissionRepository>();

            commissionRepositoryMock.Setup(x => x.GetLatestCommissionInapNumber()).Returns(0);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICommissionRepository>()).Returns(commissionRepositoryMock.Object);

            Container.Current = containerMock.Object;

            var vacancy = 22;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            var commissionFactory = new CommissionFactory();
            var activityMock = new Mock<Activity>();

            // Act

            Exception e = Assert.Catch(() => commissionFactory.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours));

            // Assert

            Assert.That(e, Is.InstanceOf<InapNumberNotConfiguredException>());
        }

    }
}