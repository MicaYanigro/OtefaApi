using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.EvaluationCriteria;
using Sai.UI.Api.ViewModel.EvaluationCriteriaConsiderations;
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
    public class EvaluationCriteriaConsiderationsControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IEvaluationCriteriaConsiderationService>()).Returns(evaluationCriteriaConsiderationServiceMock.Object);

            Container.Current = containerMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var evaluationCriteriaConsideration = new List<EvaluationCriteriaConsideration>()
            {
                new EvaluationCriteriaConsideration(description, weighing)
            };

            evaluationCriteriaConsiderationServiceMock.Setup(x => x.GetAll()).Returns(evaluationCriteriaConsideration);

            var evaluationCriteriaConsiderationsController = new EvaluationCriteriaConsiderationsController();

            evaluationCriteriaConsiderationsController.EvaluationCriteriaConsiderationService = evaluationCriteriaConsiderationServiceMock.Object;

            // Act

            var allEvaluationCriteriaConsiderations = evaluationCriteriaConsiderationsController.Get();

            // Assert

            evaluationCriteriaConsiderationServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allEvaluationCriteriaConsiderations, Is.Not.Null);
            Assert.That(allEvaluationCriteriaConsiderations, Is.InstanceOf<IEnumerable<EvaluationCriteriaConsideration>>());
            Assert.That(allEvaluationCriteriaConsiderations.Count(), Is.EqualTo(1));
            Assert.That(allEvaluationCriteriaConsiderations, Has.Exactly(1).Property("Description").EqualTo(description));
            Assert.That(allEvaluationCriteriaConsiderations, Has.Exactly(1).Property("Weighing").EqualTo(weighing));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var evaluationCriteriaConsiderationsController = new EvaluationCriteriaConsiderationsController();

            evaluationCriteriaConsiderationsController.EvaluationCriteriaConsiderationService = evaluationCriteriaConsiderationServiceMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var evaluationCriteriaConsiderationViewModel = new EvaluationCriteriaConsiderationViewModel
            {
                Description = description,
                Weighing = weighing
            };

            evaluationCriteriaConsiderationServiceMock.Setup(x => x.Create(description, weighing));

            evaluationCriteriaConsiderationsController.Request = new HttpRequestMessage();
            evaluationCriteriaConsiderationsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = evaluationCriteriaConsiderationsController.Post(evaluationCriteriaConsiderationViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            evaluationCriteriaConsiderationServiceMock.Verify(x => x.Create(description, weighing), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var evaluationCriteriaConsiderationServiceMock = new Mock<IEvaluationCriteriaConsiderationService>();

            var evaluationCriteriaConsiderationsController = new EvaluationCriteriaConsiderationsController();

            evaluationCriteriaConsiderationsController.EvaluationCriteriaConsiderationService = evaluationCriteriaConsiderationServiceMock.Object;

            var random = new Random();

            var description = new string('x', 25);
            var weighing = random.Next(0, 100);

            var evaluationCriteriaConsiderationViewModel = new EvaluationCriteriaConsiderationViewModel
            {
                Description = description,
                Weighing = weighing
            };

            evaluationCriteriaConsiderationServiceMock.Setup(x => x.Create(description, weighing)).Throws(new ExceptionBase());

            evaluationCriteriaConsiderationsController.Request = new HttpRequestMessage();
            evaluationCriteriaConsiderationsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = evaluationCriteriaConsiderationsController.Post(evaluationCriteriaConsiderationViewModel);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            evaluationCriteriaConsiderationServiceMock.Verify(x => x.Create(description, weighing), Times.Once);

        }

        #endregion Post

    }
}