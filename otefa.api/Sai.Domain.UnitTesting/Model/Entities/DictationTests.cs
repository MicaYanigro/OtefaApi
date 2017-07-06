using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Exceptions.Dictation.Credits;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System.Collections.Generic;
using Sai.Domain.Model.Exceptions.Dictation;
using Moq;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class DictationTests
    {

        [Test]
        public void Should_Create_A_Dictation()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            // Act

            var dictation = new Dictation (credits, evaluationCriteriaWithConsideration);

            // Assert

            Assert.That(dictation.Credits, Is.EqualTo(0)); //Credits no está disponible si no hay disposición.
            Assert.That(dictation.GetEvaluationCriteriaWithConsiderations(), Is.SameAs(evaluationCriteriaWithConsideration));
        }

        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Credits_Is_Less_Than_Zero()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = -1;

            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            // Act

            var caughtException = Assert.Catch(() => new Dictation(credits, evaluationCriteriaWithConsideration));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NoCreditsException>());
        }

        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Credits_Length_Is_More_Than_3_Digits()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 2442;

            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };

            // Act

            var caughtException = Assert.Catch(() => new Dictation(credits, evaluationCriteriaWithConsideration));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidCreditLengthException>());
        }

        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Evaluation_Criteria_With_Consideration_Is_Null()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;

            List<EvaluationCriteriaWithConsideration> evaluationCriteriaWithConsideration = null;

            // Act

            var caughtException = Assert.Catch(() => new Dictation(credits, evaluationCriteriaWithConsideration));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullEvaluationCriteriaWithConsiderationException>());
        }

        [Test]
        public void Should_Accept()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();
            var dictationNumber = "AA-2017-1-X-A";
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now;
            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            var dictation = new Dictation(credits, evaluationCriteriaWithConsideration);
            var observation = new string('x', 20);



            var userServiceMock = new Mock<IUserService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act
            dictation.Accept(observation, dictationNumber, dateFrom, dateTo);

            // Assert

            Assert.That(dictation.Status == DictationStatus.Accepted);

        }

        [Test]
        public void Should_Reject()
        {

            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            var dictation = new Dictation(credits, evaluationCriteriaWithConsideration);
            var observation = new string('x', 20);

            var userServiceMock = new Mock<IUserService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            dictation.Reject(observation);

            // Assert

            Assert.That(dictation.Status == DictationStatus.Rejected);

        }
        [Test]
        public void Should_Create_A_Dictation_But_Fails_Because_Evaluation_Criteria_With_Consideration_Is_Empty()
        {
            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>();

            // Act

            var caughtException = Assert.Catch(() => new Dictation(credits, evaluationCriteriaWithConsideration));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyEvaluationCriteriaWithConsiderationException>());
        }

        [Test]
        public void Should_AddDisposition()
        {

            var userServiceMock = new Mock<IUserService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            var dictation = new Dictation(credits, evaluationCriteriaWithConsideration);
            dictation.Accept("observation", "DI-2017-12457891-APN-INAP", DateTime.Now, DateTime.Now);
            var disposition = "DI-2017-12457891-APN-INAP";



            // Act
            dictation.AddDisposition(disposition);

            // Assert

            Assert.That(dictation.Disposition, Is.Not.Null);
            Assert.That(dictation.Disposition, Is.EqualTo(disposition));
        }

        [Test]
        public void Should_AddDisposition_But_Fails()
        {

            // Arrange

            var activityMock = new Mock<Activity>();
            var credits = 22;
            var evaluationCriteriaWithConsiderationMock = new Mock<EvaluationCriteriaWithConsideration>();

            var evaluationCriteriaWithConsideration = new List<EvaluationCriteriaWithConsideration>() { evaluationCriteriaWithConsiderationMock.Object };
            var dictation = new Dictation(credits, evaluationCriteriaWithConsideration);
            var disposition = "DI-2017-12457891-APN-INAP";

            var userServiceMock = new Mock<IUserService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            userServiceMock.Setup(x => x.Authorize(It.IsAny<Roles>())).Throws(new UnauthorizedAccessException());
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => dictation.AddDisposition(disposition));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<UnauthorizedAccessException>());
        }
    }
}