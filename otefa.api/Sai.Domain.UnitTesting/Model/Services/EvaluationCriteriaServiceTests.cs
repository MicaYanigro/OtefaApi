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
    public class EvaluationCriteriaServiceTests
    {

        #region Create

        [Test]
        public void Should_Create()
        {
            // Arrange

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaRepositoryMock = new Mock<IEvaluationCriteriaRepository>();
            evaluationCriteriaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var evaluationCriteriaService = new EvaluationCriteriaService();

            evaluationCriteriaService.EvaluationCriteriaRepository = evaluationCriteriaRepositoryMock.Object;

            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();

            var evaluationCriteriaFactoryMock = new Mock<IEvaluationCriteriaFactory>();
            evaluationCriteriaFactoryMock.Setup(x => x.Create(description, weighing)).Returns(evaluationCriteriaMock.Object);

            evaluationCriteriaService.EvaluationCriteriaFactory = evaluationCriteriaFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            evaluationCriteriaService.UserService = userServiceMock.Object;

            // Act

            evaluationCriteriaService.Create(description, weighing);

            // Assert

            evaluationCriteriaFactoryMock.Verify(x => x.Create(description, weighing), Times.Once);
            evaluationCriteriaRepositoryMock.Verify(x => x.Add(evaluationCriteriaMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }



        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaRepositoryMock = new Mock<IEvaluationCriteriaRepository>();
            evaluationCriteriaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var allEvaluationCriteria = new List<EvaluationCriteria>().AsQueryable();

            evaluationCriteriaRepositoryMock.Setup(x => x.All()).Returns(allEvaluationCriteria);

            EvaluationCriteriaService evaluationCriteriaService = new EvaluationCriteriaService();
            evaluationCriteriaService.EvaluationCriteriaRepository = evaluationCriteriaRepositoryMock.Object;

            // Act

            var evaluationCriteria = evaluationCriteriaService.GetAll();

            // Assert

            Assert.That(evaluationCriteria, Is.Not.Null);

            evaluationCriteriaRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

        #region GetByDescription

        [Test]
        public void Should_Get_By_Description()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaRepositoryMock = new Mock<IEvaluationCriteriaRepository>();
            evaluationCriteriaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var description = new string('x', 50);
            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();

            evaluationCriteriaRepositoryMock.Setup(x => x.GetByDescription(description)).Returns(evaluationCriteriaMock.Object);

            EvaluationCriteriaService evaluationCriteriaService = new EvaluationCriteriaService();
            evaluationCriteriaService.EvaluationCriteriaRepository = evaluationCriteriaRepositoryMock.Object;

            // Act

            var evaluationCriteria = evaluationCriteriaService.GetByDescription(description);

            // Assert

            Assert.That(evaluationCriteria, Is.SameAs(evaluationCriteriaMock.Object));

            evaluationCriteriaRepositoryMock.Verify(x => x.GetByDescription(description), Times.Once);
        }

        #endregion GetByDescription

        #region GetTotalWeighing

        [Test]
        public void Should_Get_Total_Weighing()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaRepositoryMock = new Mock<IEvaluationCriteriaRepository>();
            evaluationCriteriaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var allEvaluationCriteria = new List<EvaluationCriteria>().AsQueryable();

            evaluationCriteriaRepositoryMock.Setup(x => x.All()).Returns(allEvaluationCriteria);

            EvaluationCriteriaService evaluationCriteriaService = new EvaluationCriteriaService();
            evaluationCriteriaService.EvaluationCriteriaRepository = evaluationCriteriaRepositoryMock.Object;

            // Act

            var evaluationCriteria = evaluationCriteriaService.GetTotalWeighing();

            // Assert

            evaluationCriteriaRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetTotalWeighing

        #region GetById

        [Test]
        public void Should_Get_By_Id()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var evaluationCriteriaRepositoryMock = new Mock<IEvaluationCriteriaRepository>();
            evaluationCriteriaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var random = new Random();

            var evaluationCriteriaId = random.Next();

            var evaluationCriteriaMock = new Mock<EvaluationCriteria>();

            evaluationCriteriaRepositoryMock.Setup(x => x.GetById(evaluationCriteriaId)).Returns(evaluationCriteriaMock.Object);

            EvaluationCriteriaService evaluationCriteriaService = new EvaluationCriteriaService();
            evaluationCriteriaService.EvaluationCriteriaRepository = evaluationCriteriaRepositoryMock.Object;

            // Act

            var evaluationCriteria = evaluationCriteriaService.GetById(evaluationCriteriaId);

            // Assert

            Assert.That(evaluationCriteria, Is.Not.Null);
            Assert.That(evaluationCriteria, Is.SameAs(evaluationCriteriaMock.Object));

            evaluationCriteriaRepositoryMock.Verify(x => x.GetById(evaluationCriteriaId), Times.Once);
        }

        #endregion GetById

    }
}