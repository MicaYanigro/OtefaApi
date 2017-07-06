using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Factories;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class CommissionFactoryTests
    {

        [Test]
        public void Should_Create()
        {
            // Arrange

            var commissionRepositoryMock = new Mock<ICommissionRepository>();

            commissionRepositoryMock.Setup(x => x.GetLatestCommissionInapNumber()).Returns(1);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICommissionRepository>()).Returns(commissionRepositoryMock.Object);

            Container.Current = containerMock.Object;

            var areaDescription = new string('x', 2);
            var categoryDescription = new string('x', 2);
            var vacancy = 220;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            var commissionFactory = new CommissionFactory();
            var activityMock = new Mock<Activity>();
            activityMock.SetupGet(x => x.Area.BriefDescription).Returns(areaDescription);
            activityMock.SetupGet(x => x.Category.BriefDescription).Returns(categoryDescription);
            // Act

            var commission = commissionFactory.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);

            // Assert

            Assert.That(commission, Is.InstanceOf<Commission>());
            Assert.That(commission.Vacancy, Is.EqualTo(vacancy));
            Assert.That(commission.Oversold, Is.EqualTo(oversold));
        }

        [Test]
        public void Should_Generate_Inap_Number()
        {
            // Arrange

            var lastInapNumber = 1;
            var areaMock = new Mock<Area>();
            var categoryMock = new Mock<Category>();
            var briefDescription = new string('x', 4);
            var commissionRepositoryMock = new Mock<ICommissionRepository>();

            commissionRepositoryMock.Setup(x => x.GetLatestCommissionInapNumber()).Returns(lastInapNumber);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ICommissionRepository>()).Returns(commissionRepositoryMock.Object);

            Container.Current = containerMock.Object;

            var vacancy = 220;
            var oversold = 10;
            var commissionInscriptionType = CommissionInscriptionType.Open;
            var enrollmentCloseHours = 1;

            var commissionFactory = new CommissionFactory();
            var activityMock = new Mock<Activity>();
            activityMock.Setup(x => x.Area).Returns(areaMock.Object);
            activityMock.Setup(x => x.Category).Returns(categoryMock.Object);
            areaMock.Setup(x => x.BriefDescription).Returns(briefDescription);
            categoryMock.Setup(x => x.BriefDescription).Returns(briefDescription);


            // Act

            var activity = commissionFactory.Create(activityMock.Object, vacancy, oversold, commissionInscriptionType, enrollmentCloseHours);

            Assert.That(activity.InapNumber, Is.EqualTo(lastInapNumber + 1));
        }

    }
}