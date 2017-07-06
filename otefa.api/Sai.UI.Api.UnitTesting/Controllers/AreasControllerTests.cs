using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Areas;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class AreasControllerTests
    {

        #region Get

        [Test]
        public void Should_Get()
        {

            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;


            var areaServiceMock = new Mock<IAreaService>();
            areaServiceMock.Setup(x => x.GetByBriefDescription(It.IsAny<string>())).Returns((Area)null);

              containerMock.Setup(x => x.Resolve<IAreaService>()).Returns(areaServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var areas = new List<Area>()
            {
                new Area(description, briefDescription)
            };

            areaServiceMock.Setup(x => x.GetAll()).Returns(areas);

            var areasController = new AreasController(areaServiceMock.Object);

            // Act

            var allAreas = areasController.Get();

            // Assert

            areaServiceMock.Verify(x => x.GetAll(), Times.Once);

            Assert.That(allAreas, Is.Not.Null);
            Assert.That(allAreas, Is.InstanceOf<IEnumerable<Area>>());
            Assert.That(allAreas.Count(), Is.EqualTo(1));
            Assert.That(allAreas, Has.Exactly(1).Property("Description").EqualTo(description));
            Assert.That(allAreas, Has.Exactly(1).Property("BriefDescription").EqualTo(briefDescription));

        }

        #endregion Get

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var areaDomainMock = new Mock<Area>();
            var areaServiceMock = new Mock<IAreaService>();
            var areasController = new AreasController(areaServiceMock.Object);

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var areaViewModelMock = new AreaViewModel
            {
                Description = description,
                BriefDescription = briefDescription
            };

            areaServiceMock.Setup(x => x.Create(description, briefDescription)).Returns(areaDomainMock.Object);

            areasController.Request = new HttpRequestMessage();
            areasController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

           var response = areasController.Post(areaViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            areaServiceMock.Verify(x => x.Create(description, briefDescription), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var areaServiceMock = new Mock<IAreaService>();
            var areasController = new AreasController(areaServiceMock.Object);

            var description = new string('x', 30);
            var briefDescription = new string('x', 1);

            var areaViewModelMock = new AreaViewModel
            {
                Description = description,
                BriefDescription = briefDescription
            };

            areaServiceMock.Setup(x => x.Create(description, briefDescription)).Throws(new ExceptionBase());

            areasController.Request = new HttpRequestMessage();
            areasController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = areasController.Post(areaViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            areaServiceMock.Verify(x => x.Create(description, briefDescription), Times.Once);

        }

        #endregion Post

    }
}