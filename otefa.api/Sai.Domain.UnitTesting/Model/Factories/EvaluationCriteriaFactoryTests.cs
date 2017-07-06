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
    public class EvaluationCriteriaFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var evaluationCriteriaFactory = new EvaluationCriteriaFactory();

            // Act

            var evaluationCriteria = evaluationCriteriaFactory.Create(description, weighing);

            // Assert

            Assert.That(evaluationCriteria, Is.InstanceOf<EvaluationCriteria>());

            Assert.That(evaluationCriteria.Description, Is.EqualTo(description));
            Assert.That(evaluationCriteria.Weighing, Is.EqualTo(weighing));
        }
    }
}