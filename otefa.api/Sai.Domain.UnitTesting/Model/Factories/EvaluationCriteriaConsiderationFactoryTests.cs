using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class EvaluationCriteriaConsiderationFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var evaluationCriteriaConsiderationFactory = new EvaluationCriteriaConsiderationFactory();

            // Act

            var evaluationCriteriaConsideration = evaluationCriteriaConsiderationFactory.Create(description, weighing);

            // Assert

            Assert.That(evaluationCriteriaConsideration, Is.InstanceOf<EvaluationCriteriaConsideration>());

            Assert.That(evaluationCriteriaConsideration.Description, Is.EqualTo(description));
            Assert.That(evaluationCriteriaConsideration.Weighing, Is.EqualTo(weighing));
        }
    }
}