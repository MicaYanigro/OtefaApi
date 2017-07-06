using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class EvaluationCriteriaConsiderationServiceTests
    {

        #region Create

        [Test]
        public void Should_Create()
        {
            // Arrange

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaConsiderationRepositoryMock = new Mock<IEvaluationCriteriaConsiderationRepository>();
            evaluationCriteriaConsiderationRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var evaluationCriteriaConsiderationService = new EvaluationCriteriaConsiderationService();

            evaluationCriteriaConsiderationService.EvaluationCriteriaConsiderationRepository = evaluationCriteriaConsiderationRepositoryMock.Object;

            var evaluationCriteriaConsiderationMock = new Mock<EvaluationCriteriaConsideration>();

            var evaluationCriteriaConsiderationFactoryMock = new Mock<IEvaluationCriteriaConsiderationFactory>();
            evaluationCriteriaConsiderationFactoryMock.Setup(x => x.Create(description, weighing)).Returns(evaluationCriteriaConsiderationMock.Object);

            evaluationCriteriaConsiderationService.EvaluationCriteriaConsiderationFactory = evaluationCriteriaConsiderationFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            evaluationCriteriaConsiderationService.UserService = userServiceMock.Object;

            // Act

            evaluationCriteriaConsiderationService.Create(description, weighing);

            // Assert

            evaluationCriteriaConsiderationFactoryMock.Verify(x => x.Create(description, weighing), Times.Once);
            evaluationCriteriaConsiderationRepositoryMock.Verify(x => x.Add(evaluationCriteriaConsiderationMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }


        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaConsiderationRepositoryMock = new Mock<IEvaluationCriteriaConsiderationRepository>();
            evaluationCriteriaConsiderationRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var allEvaluationCriteriaConsiderations = new List<EvaluationCriteriaConsideration>().AsQueryable();

            evaluationCriteriaConsiderationRepositoryMock.Setup(x => x.All()).Returns(allEvaluationCriteriaConsiderations);

            EvaluationCriteriaConsiderationService evaluationCriteriaConsiderationService = new EvaluationCriteriaConsiderationService();
            evaluationCriteriaConsiderationService.EvaluationCriteriaConsiderationRepository = evaluationCriteriaConsiderationRepositoryMock.Object;

            // Act

            var evaluationCriteriaConsideration = evaluationCriteriaConsiderationService.GetAll();

            // Assert

            Assert.That(evaluationCriteriaConsideration, Is.Not.Null);

            evaluationCriteriaConsiderationRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

        #region GetByDescription

        [Test]
        public void Should_Get_By_Description()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaConsiderationRepositoryMock = new Mock<IEvaluationCriteriaConsiderationRepository>();
            evaluationCriteriaConsiderationRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var description = new string('x', 25);
            var evaluationCriteriaConsiderationMock = new Mock<EvaluationCriteriaConsideration>();

            evaluationCriteriaConsiderationRepositoryMock.Setup(x => x.GetByDescription(description)).Returns(evaluationCriteriaConsiderationMock.Object);

            EvaluationCriteriaConsiderationService evaluationCriteriaConsiderationService = new EvaluationCriteriaConsiderationService();
            evaluationCriteriaConsiderationService.EvaluationCriteriaConsiderationRepository = evaluationCriteriaConsiderationRepositoryMock.Object;

            // Act

            var evaluationCriteriaConsideration = evaluationCriteriaConsiderationService.GetByDescription(description);

            // Assert

            Assert.That(evaluationCriteriaConsideration, Is.SameAs(evaluationCriteriaConsiderationMock.Object));

            evaluationCriteriaConsiderationRepositoryMock.Verify(x => x.GetByDescription(description), Times.Once);
        }

        #endregion GetByDescription

        #region GetTotalWeighing

        [Test]
        public void Should_Get_Total_Weighing()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaConsiderationRepositoryMock = new Mock<IEvaluationCriteriaConsiderationRepository>();
            evaluationCriteriaConsiderationRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var allEvaluationCriteriaConsiderations = new List<EvaluationCriteriaConsideration>().AsQueryable();

            evaluationCriteriaConsiderationRepositoryMock.Setup(x => x.All()).Returns(allEvaluationCriteriaConsiderations);

            EvaluationCriteriaConsiderationService evaluationCriteriaConsiderationService = new EvaluationCriteriaConsiderationService();
            evaluationCriteriaConsiderationService.EvaluationCriteriaConsiderationRepository = evaluationCriteriaConsiderationRepositoryMock.Object;

            // Act

            var evaluationCriteriaConsideration = evaluationCriteriaConsiderationService.GetTotalWeighing();

            // Assert

            evaluationCriteriaConsiderationRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetTotalWeighing

    }
}