using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Programs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class ProgramsControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Arrange

            var programServiceMock = new Mock<IProgramService>();
            var programsController = new ProgramsController(programServiceMock.Object);

            var description = new string('x', 40);

            var programs = new List<Program>()
            {
                new Program(description)
            };

            programServiceMock.Setup(x => x.GetAll()).Returns(programs);

            // Act

            var allPrograms = programsController.Get();

            // Assert

            programServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allPrograms, Is.Not.Null);
            Assert.That(allPrograms, Is.InstanceOf<IEnumerable<Program>>());
            Assert.That(allPrograms.Count(), Is.EqualTo(1));
            Assert.That(allPrograms, Has.Exactly(1).Property("Description").EqualTo(description));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var programDomainMock = new Mock<Program>();
            var programServiceMock = new Mock<IProgramService>();
            var programsController = new ProgramsController(programServiceMock.Object);

            var description = new string('x', 40);

            var programViewModelMock = new ProgramViewModel
            {
                Description = description
            };

            programServiceMock.Setup(x => x.Create(description)).Returns(programDomainMock.Object);

            programsController.Request = new HttpRequestMessage();
            programsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = programsController.Post(programViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            programServiceMock.Verify(x => x.Create(description), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var programDomainMock = new Mock<Program>();
            var programServiceMock = new Mock<IProgramService>();
            var programsController = new ProgramsController(programServiceMock.Object);

            var description = new string('x', 41);

            var programViewModelMock = new ProgramViewModel
            {
                Description = description
            };

            programServiceMock.Setup(x => x.Create(description)).Throws(new ExceptionBase());

            programsController.Request = new HttpRequestMessage();
            programsController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = programsController.Post(programViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            programServiceMock.Verify(x => x.Create(description), Times.Once);

        }

        #endregion Post

    }
}