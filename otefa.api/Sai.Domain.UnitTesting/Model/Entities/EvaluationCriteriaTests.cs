using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.EvaluationCriteria;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class EvaluationCriteriaTests
    {

        [Test]
        public void Should_Create_An_Evaluation_Criteria()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            // Act

            var evaluationCriteria = new EvaluationCriteria(description, weighing);

            // Assert

            Assert.That(evaluationCriteria.Description, Is.EqualTo(description));
            Assert.That(evaluationCriteria.Weighing, Is.EqualTo(weighing));
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Description_Is_Null()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            string description = null;
            var weighing = random.Next(1, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<NullEvaluationCriteriaDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = "";
            var weighing = random.Next(1, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EmptyEvaluationCriteriaDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Description_Length_Is_More_Than_50_Characters()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 51);
            var weighing = random.Next(1, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<InvalidEvaluationCriteriaDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Description_Already_Exists()
        {
            // Arrange

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();
            evaluationCriteriaServiceMock.Setup(x => x.GetByDescription(description)).Returns(evaluationCriteriaMock.Object);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<ExistantEvaluationCriteriaDescriptionException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Weighing_Is_Less_Than_1()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = 0;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EvaluationCriteriaWeighingOutOfRangeException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Weighing_Is_More_Than_100()
        {
            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = 101;

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EvaluationCriteriaWeighingOutOfRangeException>());
        }

        [Test]
        public void Should_Create_An_Evaluation_Criteria_But_Fails_Because_Total_Weighing_Is_More_Than_100()
        {
            // Arrange

            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();
            evaluationCriteriaServiceMock.Setup(x => x.GetTotalWeighing()).Returns(101);

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            // Act

            var exception = Assert.Catch(() => new EvaluationCriteria(description, weighing));

            // Assert

            Assert.That(exception, Is.InstanceOf<EvaluationCriteriaTotalWeighingOutOfRangeException>());
        }

    }
}