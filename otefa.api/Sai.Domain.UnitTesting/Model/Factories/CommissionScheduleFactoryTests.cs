using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class CommissionScheduleFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            var userServiceMock = new Mock<IUserService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Arrange
            var commissionMock = new Mock<Commission>();
            var activityMock = new Mock<Activity>();
            var dictationMock = new Mock<Dictation>();
            DateTime dateFrom = new DateTime(1800, 01, 01);
            DateTime dateTo = new DateTime(2800, 01, 01);
            dictationMock.Setup(x => x.DateFrom).Returns(dateFrom);
            dictationMock.Setup(x => x.DateTo).Returns(dateTo);
            var dictationlist = new List<Dictation>();
            dictationlist.Add(dictationMock.Object);
            activityMock.SetupGet(x => x.Dictations).Returns(dictationlist);
            commissionMock.Setup(x => x.Activity).Returns(activityMock.Object);
            DateTime date = DateTime.Now;
            DateTime timeFrom = DateTime.Now;
            DateTime timeTo = DateTime.Now;
            List<int> teacherID = new List<int>() { 1, 2, 3 };
            var headquarterID = 1;

            var commissionScheduleFactory = new CommissionScheduleFactory();
            var teacherMock = new Mock<Teacher>();

            var teachersRepositoryMock = new Mock<ITeachersRepository>();
            teachersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(teacherMock.Object);

            var headquarterMock = new Mock<Headquarter>();

            var headquarterRepositoryMock = new Mock<IHeadquarterRepository>();
            headquarterRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(headquarterMock.Object);

            containerMock.Setup(x => x.Resolve<ITeachersRepository>()).Returns(teachersRepositoryMock.Object);
            containerMock.Setup(x => x.Resolve<IHeadquarterRepository>()).Returns(headquarterRepositoryMock.Object);

            Container.Current = containerMock.Object;
            // Act

            var schedule = commissionScheduleFactory.Create(commissionMock.Object, date, timeFrom, timeTo, teacherID, headquarterID);

            // Assert

            Assert.That(schedule, Is.InstanceOf<CommissionSchedule>());
            Assert.That(schedule.Date, Is.EqualTo(date));
            Assert.That(schedule.TimeFrom, Is.EqualTo(timeFrom));
            Assert.That(schedule.TimeTo, Is.EqualTo(timeTo));
            Assert.That(schedule.GetTeachers().Count() > 0);
            Assert.That(schedule.Headquarter, Is.EqualTo(headquarterMock.Object));
        }
    }
}