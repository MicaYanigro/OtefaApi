using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.EvaluationCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class EvaluationCriteriaControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaService>()).Returns(evaluationCriteriaServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var evaluationCriteria = new List<EvaluationCriteria>()
            {
                new EvaluationCriteria(description, weighing)
            };

            evaluationCriteriaServiceMock.Setup(x => x.GetAll()).Returns(evaluationCriteria);

            var evaluationCriteriaController = new EvaluationCriteriaController();

            evaluationCriteriaController.EvaluationCriteriaService = evaluationCriteriaServiceMock.Object;

            // Act

            var allEvaluationCriteria = evaluationCriteriaController.Get();

            // Assert

            evaluationCriteriaServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allEvaluationCriteria, Is.Not.Null);
            Assert.That(allEvaluationCriteria, Is.InstanceOf<IEnumerable<EvaluationCriteria>>());
            Assert.That(allEvaluationCriteria.Count(), Is.EqualTo(1));
            Assert.That(allEvaluationCriteria, Has.Exactly(1).Property("Description").EqualTo(description));
            Assert.That(allEvaluationCriteria, Has.Exactly(1).Property("Weighing").EqualTo(weighing));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var evaluationCriteriaController = new EvaluationCriteriaController();

            evaluationCriteriaController.EvaluationCriteriaService = evaluationCriteriaServiceMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var evaluationCriteriaViewModel = new EvaluationCriteriaViewModel
            {
                Description = description,
                Weighing = weighing
            };

            evaluationCriteriaServiceMock.Setup(x => x.Create(description, weighing));

            evaluationCriteriaController.Request = new HttpRequestMessage();
            evaluationCriteriaController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = evaluationCriteriaController.Post(evaluationCriteriaViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            evaluationCriteriaServiceMock.Verify(x => x.Create(description, weighing), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var evaluationCriteriaServiceMock = new Mock<IEvaluationCriteriaService>();

            var evaluationCriteriaController = new EvaluationCriteriaController();

            evaluationCriteriaController.EvaluationCriteriaService = evaluationCriteriaServiceMock.Object;

            var random = new Random();

            var description = new string('x', 50);
            var weighing = random.Next(1, 100);

            var evaluationCriteriaViewModel = new EvaluationCriteriaViewModel
            {
                Description = description,
                Weighing = weighing
            };

            evaluationCriteriaServiceMock.Setup(x => x.Create(description, weighing)).Throws(new ExceptionBase());

            evaluationCriteriaController.Request = new HttpRequestMessage();
            evaluationCriteriaController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = evaluationCriteriaController.Post(evaluationCriteriaViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            evaluationCriteriaServiceMock.Verify(x => x.Create(description, weighing), Times.Once);

        }

        #endregion Post

    }
}