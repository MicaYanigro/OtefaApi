using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.EvaluationCriteriaConsideration;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class EvaluationCriteriaConsiderationTests
    {

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            // Act

            var evaluationCriteriaConsideration = new EvaluationCriteriaConsideration(description, weighing);

            // Assert

            Assert.That(evaluationCriteriaConsideration.Description, Is.EqualTo(description));
            Assert.That(evaluationCriteriaConsideration.Weighing, Is.EqualTo(weighing));
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Description_Is_Null()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            string description = null;
            var weighing = random.Next(0, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<NullEvaluationCriteriaConsiderationDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = "";
            var weighing = random.Next(0, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EmptyEvaluationCriteriaConsiderationDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Description_Length_Is_More_Than_25_Characters()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 26);
            var weighing = random.Next(0, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<InvalidEvaluationCriteriaConsiderationDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Description_Already_Exists()
        {
            // Arrange

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var evaluationCriteriaConsiderationMock = new Mock<EvaluationCriteriaConsideration>();

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();
            evaluationCriteriaConsiderationServiceMock.Setup(x => x.GetByDescription(description)).Returns(evaluationCriteriaConsiderationMock.Object);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<ExistantEvaluationCriteriaConsiderationDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Weighing_Is_Less_Than_0()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = -1;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EvaluationCriteriaConsiderationWeighingOutOfRangeException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_Consideration_But_Fails_Because_Weighing_Is_More_Than_100()
        {
            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = 101;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteriaConsideration(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EvaluationCriteriaConsiderationWeighingOutOfRangeException>());
        }

    }
}