using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class EvaluationCriteriaWithConsiderationTests
    {
        [Test]
        public void Should_Create_An_Evaluation_Criteria_With_Condireation()
        {
            // Arrange

            var evaluationCriteria = new Mock<EvaluationCriteria>();
            var evaluationCriteriaConsideration = new Mock<EvaluationCriteriaConsideration>();

            // Act

            var evaluationCriteriaWithConsideration = new EvaluationCriteriaWithConsideration(evaluationCriteria.Object, evaluationCriteriaConsideration.Object);

            // Assert

            Assert.That(evaluationCriteriaWithConsideration.EvaluationCriteria, Is.SameAs(evaluationCriteria.Object));
            Assert.That(evaluationCriteriaWithConsideration.EvaluationCriteriaConsideration, Is.SameAs(evaluationCriteriaConsideration.Object));
        }
    }
}